using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.CustomDashboards
{
	public class CustomGadgetDetailForm : Form
	{
		private CustomGadgetData currentData;

		private CustomGadget currentGadget;

		private const string TABLENAME_CONST = "Custom_Gadget";

		private const string IDFIELD_CONST = "CustomGadgetID";

		private bool isNewRecord = true;

		private List<string> MyList = new List<string>();

		private ScreenAccessRight screenRight;

		private IContainer components;

		private DataSet dataSetData;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private UltraTabPageControl tabPageDetails;

		private UltraTabPageControl tabPageContacts;

		private Label label2;

		private Label label1;

		private ListBox listBoxRelations;

		private ListBox listBoxTables;

		private Button buttonAddTable;

		private MMTextBox textBoxName;

		private MMTextBox textBoxCode;

		private MMLabel mmLabel1;

		private MMLabel labelCode;

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

		private FormManager formManager;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonDelete;

		private XPButton xpButton1;

		private XPButton buttonNew;

		private XPButton buttonSave;

		private Button buttonDeleteRelation;

		private Button buttonEditRelation;

		private Button buttonAddRelation;

		private Button buttonDeleteTable;

		private Button buttonEditTable;

		private Button buttonDeleteParameter;

		private Button buttonEditParameter;

		private Button buttonAddParameter;

		private Label label3;

		private ListBox listBoxParameters;

		private DataEntryGrid dataGridControls;

		private OpenFileDialog openFileDialog1;

		private SaveFileDialog saveFileDialog1;

		private ToolStripButton toolStripButton1;

		private ToolStripButton toolStripButtonImport;

		private MMLabel mmLabel2;

		private System.Windows.Forms.ComboBox comboBoxType;

		private ImageList imageList1;

		private System.Windows.Forms.ComboBox comboBoxGroup;

		private Label label5;

		private Label label6;

		private System.Windows.Forms.ComboBox comboBoxFilterOption;

		private UltraTabPageControl ultraTabPageControl1;

		private Label label7;

		private System.Windows.Forms.ComboBox comboBoxDrillDown;

		private Panel panelActionTransaction;

		private CheckBox checkBoxTransactionPreview;

		private TextBox textBoxActionTRVoucher;

		private Label label8;

		private TextBox textBoxActionTRSysDocID;

		private Label label9;

		private Panel panelActionCard;

		private CardTypesComboBox comboBoxCardTypes;

		private Label label10;

		private TextBox textBoxDrilCardID;

		private Label label11;

		private Panel panelActionSmartList;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private SubSmartListComboBox comboBoxSubReport;

		private Label label13;

		private TextBox textBoxParm4;

		private TextBox textBoxParm3;

		private Label label12;

		private Label label14;

		private TextBox textBoxParm2;

		private Label label15;

		private TextBox textBoxParm1;

		private Label label16;

		private CheckBox checkBoxAutoRefresh;

		private Label label17;

		private NumericUpDown numericUpDownRefreshInterval;

		private UltraTabPageControl tabPageChart;

		private MMLabel mmLabel6;

		private MMTextBox textBoxArgColumn;

		private MMLabel mmLabel4;

		private CheckBox checkBoxLegend;

		private CheckBox checkBoxColorEach;

		private DataEntryGrid dataGridChartSeries;

		private CheckBox checkBoxShowAxisY;

		private CheckBox checkBoxRotated;

		private MMTextBox textBoxAxisYPattern;

		private MMLabel mmLabel9;

		private MMTextBox textBoxAxisYTitle;

		private MMLabel mmLabel8;

		private MMTextBox textBoxAxisXTitle;

		private MMLabel mmLabel7;

		private MMTextBox textBoxTopNOther;

		private NumericUpDown numericUpDownNCount;

		private CheckBox checkBoxShowOthers;

		private CheckBox checkBoxShowTopN;

		private MMTextBox textBoxLegendPattern;

		private MMLabel mmLabel10;

		private MMLabel mmLabel11;

		private System.Windows.Forms.ComboBox comboBoxColorPalette;

		private MMLabel mmLabel12;

		private NumericUpDown numericUpDownStartColor;

		private UltraTabPageControl tabPageNumber;

		private MMLabel mmLabel16;

		private MMTextBox textBoxNumberValueColumn;

		private MMLabel mmLabel15;

		private MMTextBox textBoxNumIndTextVal;

		private MMTextBox textBoxNumIndicatorValueColumn;

		private MMLabel mmLabel14;

		private MMLabel mmLabel13;

		private CheckBox checkBoxNumShowIndicator;

		private ColorEdit colorEditNumTextColor;

		private CheckBox checkBoxNumShowIndText;

		private MMTextBox textBoxNumIndTextFormat;

		private MMLabel mmLabel18;

		private MMTextBox textBoxNumValueFormat;

		private MMLabel mmLabel17;

		private UltraTabPageControl tabPageGauge;

		private MMLabel mmLabel19;

		private DataEntryGrid dataGridGauge;

		private MMTextBox textBoxGaugeValueColumn;

		private MMLabel mmLabel3;

		private CheckBox checkBoxGShowNeedle;

		private CheckBox checkBoxGShowGaugeText;

		private MMTextBox textBoxGaugeValueFormat;

		private MMLabel mmLabel5;

		private CheckBox checkBoxGaugeShowLabel;

		private CheckBox checkBoxGaugeRangeAsValue;

		private MMTextBox textBoxDescription;

		private Label label18;

		private MMTextBox textBoxDisplayName;

		private Label label4;

		private UltraFormattedLinkLabel linkRemovePicture;

		private UltraFormattedLinkLabel linkAddPicture;

		private UltraFormattedLinkLabel linkLoadImage;

		private PictureBox pictureBoxPhoto;

		private PictureBox pictureBoxNoImage;

		private ToolStripButton toolStripButtonShowPicture;

		private ToolStripButton toolStripButtonDuplicate;

		private UltraTabPageControl tabPageList;

		private MMLabel mmLabel20;

		private DataEntryGrid dataGridHiddenColumns;

		public ScreenAreas ScreenCustomGadget => ScreenAreas.General;

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
				if (value)
				{
					buttonNew.Text = "Clear";
					buttonDelete.Enabled = false;
					textBoxCode.ReadOnly = false;
					UltraFormattedLinkLabel ultraFormattedLinkLabel = linkLoadImage;
					UltraFormattedLinkLabel ultraFormattedLinkLabel2 = linkRemovePicture;
					bool flag2 = linkAddPicture.Enabled = false;
					bool visible = ultraFormattedLinkLabel2.Enabled = flag2;
					ultraFormattedLinkLabel.Visible = visible;
				}
				else
				{
					buttonNew.Text = "New";
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
					linkAddPicture.Enabled = true;
				}
				toolStripButtonDuplicate.Enabled = !value;
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

		public CustomGadgetDetailForm()
		{
			InitializeComponent();
			AddEvents();
			SetSecurity();
			comboBoxCardTypes.LoadData();
			comboBoxType.SelectedIndexChanged += comboBoxType_SelectedIndexChanged;
			comboBoxDrillDown.SelectedIndexChanged += comboBoxDrillDown_SelectedIndexChanged;
			dataGridChartSeries.ClickCellButton += DataGridChartSeries_ClickCellButton;
			dataGridGauge.ClickCellButton += DataGridGauge_ClickCellButton;
		}

		private void DataGridGauge_ClickCellButton(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "Setting")
			{
				GaugeRangeSettingDialog gaugeRangeSettingDialog = new GaugeRangeSettingDialog();
				gaugeRangeSettingDialog.GridRow = e.Cell.Row;
				gaugeRangeSettingDialog.ShowDialog();
			}
		}

		private void DataGridChartSeries_AfterCellUpdate(object sender, CellEventArgs e)
		{
		}

		private void DataGridGauge_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
		}

		private void DataGridChartSeries_ClickCellButton(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "ChartType")
			{
				try
				{
					AddChartSeriesDialog addChartSeriesDialog = new AddChartSeriesDialog();
					if (addChartSeriesDialog.ShowDialog() == DialogResult.OK)
					{
						dataGridChartSeries.ActiveCell.Value = (int)addChartSeriesDialog.ChartType;
					}
					base.DialogResult = DialogResult.None;
				}
				catch (Exception e2)
				{
					base.DialogResult = DialogResult.None;
					ErrorHelper.ProcessError(e2);
				}
			}
			else if (e.Cell.Column.Key == "Setting")
			{
				ChartSerieSettingDialog chartSerieSettingDialog = new ChartSerieSettingDialog();
				chartSerieSettingDialog.GridRow = e.Cell.Row;
				chartSerieSettingDialog.ShowDialog();
			}
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

		private void chartTypesCombo1_SelectedIndexChanged(object sender, EventArgs e)
		{
			formManager.IsForcedDirty = true;
		}

		private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
		{
			tabPageChart.Tab.Visible = (comboBoxType.SelectedIndex == 1);
			tabPageNumber.Tab.Visible = (comboBoxType.SelectedIndex == 3);
			tabPageGauge.Tab.Visible = (comboBoxType.SelectedIndex == 2);
			tabPageList.Tab.Visible = (comboBoxType.SelectedIndex == 0);
		}

		private void AddEvents()
		{
			base.Load += CustomGadgetDetailsForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new CustomGadgetData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.CustomGadgetTable.Rows[0] : currentData.CustomGadgetTable.NewRow();
				dataRow.BeginEdit();
				dataRow["CustomGadgetID"] = textBoxCode.Text.Trim();
				dataRow["CustomGadgetName"] = textBoxName.Text.Trim();
				dataRow["ChartArgColumn"] = ((!string.IsNullOrEmpty(textBoxArgColumn.Text)) ? ((IConvertible)textBoxArgColumn.Text) : ((IConvertible)DBNull.Value));
				dataRow["CategoryID"] = ((comboBoxGroup.SelectedIndex >= 0) ? ((object)checked(comboBoxGroup.SelectedIndex + 1)) : DBNull.Value);
				dataRow["FilterOption"] = comboBoxFilterOption.SelectedIndex;
				dataRow["DisplayName"] = textBoxDisplayName.Text.Trim();
				dataRow["Description"] = textBoxDescription.Text.Trim();
				if (comboBoxType.SelectedIndex == 1)
				{
					dataRow["GadgetStyle"] = 1;
					dataRow["ColorEach"] = checkBoxColorEach.Checked;
					dataRow["LegendTextPattern"] = textBoxLegendPattern.Text;
					dataRow["TopNCount"] = numericUpDownNCount.Value;
					dataRow["TopNOption"] = checkBoxShowTopN.Checked;
					dataRow["TopNOthersText"] = textBoxTopNOther.Text;
					dataRow["ShowTopNOther"] = checkBoxShowOthers.Checked;
					dataRow["ShowLegend"] = checkBoxLegend.Checked;
					dataRow["IsRotated"] = checkBoxRotated.Checked;
					dataRow["AxisYVisible"] = checkBoxShowAxisY.Checked;
					dataRow["AxisXTitle"] = textBoxAxisXTitle.Text;
					dataRow["AxisYTitle"] = textBoxAxisYTitle.Text;
					dataRow["AxisYTextPattern"] = textBoxAxisYPattern.Text;
					dataRow["ColorPaletteName"] = comboBoxColorPalette.SelectedItem.ToString();
					dataRow["ChartPallet"] = numericUpDownStartColor.Value;
					currentData.ChartSeriesTable.Rows.Clear();
					foreach (UltraGridRow row in dataGridChartSeries.Rows)
					{
						DataRow dataRow2 = currentData.ChartSeriesTable.NewRow();
						dataRow2["CustomGadgetID"] = textBoxCode.Text;
						dataRow2["SeriesID"] = row.Cells["Code"].Value.ToString();
						dataRow2["DisplayName"] = row.Cells["DisplayName"].Value.ToString();
						dataRow2["ChartVAlueColumn"] = row.Cells["ValueColumn"].Value.ToString();
						dataRow2["ChartType"] = row.Cells["ChartType"].Value.ToString();
						if (row.Cells["Color"].Value.IsNullOrEmpty())
						{
							dataRow2["Color"] = DBNull.Value;
						}
						else
						{
							dataRow2["Color"] = row.Cells["Color"].Value.ToString();
						}
						if (row.Cells["LabelVisible"].Value.IsDBNullOrEmpty())
						{
							dataRow2["LabelVisible"] = false;
						}
						else
						{
							dataRow2["LabelVisible"] = row.Cells["LabelVisible"].Value.ToString();
						}
						if (row.Cells["LabelPosition"].Value.IsNullOrEmpty() || row.Cells["LabelPosition"].Value.ToString() == "-1")
						{
							dataRow2["LabelPosition"] = DBNull.Value;
						}
						else
						{
							dataRow2["LabelPosition"] = row.Cells["LabelPosition"].Value.ToString();
						}
						if (row.Cells["LabelTextPattern"].Value.IsNullOrEmpty())
						{
							dataRow2["LabelTextPattern"] = DBNull.Value;
						}
						else
						{
							dataRow2["LabelTextPattern"] = row.Cells["LabelTextPattern"].Value.ToString();
						}
						currentData.ChartSeriesTable.Rows.Add(dataRow2);
					}
				}
				else if (comboBoxType.SelectedIndex == 2)
				{
					dataRow["GadgetStyle"] = 3;
					dataRow["ChartVAlueColumn"] = textBoxGaugeValueColumn.Text;
					dataRow["AxisYTextPattern"] = textBoxGaugeValueFormat.Text;
					dataRow["GShowGaugeText"] = checkBoxGShowGaugeText.Checked;
					dataRow["GShowNeedle"] = checkBoxGShowNeedle.Checked;
					dataRow["ShowLegend"] = checkBoxGaugeShowLabel.Checked;
					dataRow["TopNOption"] = checkBoxGaugeRangeAsValue.Checked;
					currentData.GaugeRangeTable.Rows.Clear();
					foreach (UltraGridRow row2 in dataGridGauge.Rows)
					{
						DataRow dataRow3 = currentData.GaugeRangeTable.NewRow();
						dataRow3["CustomReportID"] = textBoxCode.Text;
						dataRow3["RangeID"] = row2.Cells["Code"].Value.ToString();
						dataRow3["StartValue"] = row2.Cells["StartValue"].Value.ToString();
						dataRow3["EndValue"] = row2.Cells["EndValue"].Value.ToString();
						dataRow3["CustomReportType"] = 4;
						if (row2.Cells["Color"].Value.IsNullOrEmpty())
						{
							dataRow3["RangeColor"] = DBNull.Value;
						}
						else
						{
							dataRow3["RangeColor"] = row2.Cells["Color"].Value.ToString();
						}
						currentData.GaugeRangeTable.Rows.Add(dataRow3);
					}
				}
				else if (comboBoxType.SelectedIndex == 3)
				{
					dataRow["GadgetStyle"] = 5;
					dataRow["ChartVAlueColumn"] = textBoxNumberValueColumn.Text;
					dataRow["IndValueColumn"] = textBoxNumIndicatorValueColumn.Text;
					dataRow["IndTextValueColumn"] = textBoxNumIndTextVal.Text;
					dataRow["ShowIndicator"] = checkBoxNumShowIndicator.Checked;
					dataRow["ShowName"] = checkBoxNumShowIndText.Checked;
					dataRow["AxisYTextPattern"] = textBoxNumValueFormat.Text;
					dataRow["LegendTextPattern"] = textBoxNumIndTextFormat.Text;
					if (colorEditNumTextColor.Color.IsNullOrEmpty())
					{
						dataRow["TextColor"] = DBNull.Value;
					}
					else
					{
						dataRow["TextColor"] = colorEditNumTextColor.Color.ToArgb();
					}
				}
				else
				{
					dataRow["GadgetStyle"] = 2;
					currentData.ListHiddenFieldsTable.Rows.Clear();
					foreach (UltraGridRow row3 in dataGridHiddenColumns.Rows)
					{
						DataRow dataRow4 = currentData.ListHiddenFieldsTable.NewRow();
						dataRow4["CustomReportID"] = textBoxCode.Text;
						dataRow4["CustomReportType"] = 4;
						dataRow4["FieldID"] = row3.Cells["Field"].Value.ToString();
						currentData.ListHiddenFieldsTable.Rows.Add(dataRow4);
					}
				}
				dataRow["AutoRefresh"] = checkBoxAutoRefresh.Checked;
				dataRow["RefreshInterval"] = numericUpDownRefreshInterval.Value;
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
				CustomGadget customGadget = new CustomGadget();
				foreach (object item4 in listBoxTables.Items)
				{
					CustomGadgetTable item = item4 as CustomGadgetTable;
					customGadget.Tables.Add(item);
				}
				foreach (object item5 in listBoxRelations.Items)
				{
					GadgetRelation item2 = item5 as GadgetRelation;
					customGadget.Relations.Add(item2);
				}
				foreach (object item6 in listBoxParameters.Items)
				{
					GadgetParameter item3 = item6 as GadgetParameter;
					customGadget.Parameters.Add(item3);
				}
				foreach (UltraGridRow row4 in dataGridControls.Rows)
				{
					CustomGadgetControl customGadgetControl = new CustomGadgetControl();
					customGadgetControl.ControlType = (CRCTypes)int.Parse(row4.Cells["ControlType"].Value.ToString());
					customGadgetControl.ValueType = byte.Parse(row4.Cells["ValueType"].Value.ToString());
					customGadgetControl.Key = row4.Cells["Key"].Value.ToString();
					customGadgetControl.FieldName = row4.Cells["FieldName"].Value.ToString();
					customGadgetControl.DisplayText = row4.Cells["DisplayText"].Value.ToString();
					customGadget.Controls.Add(customGadgetControl);
				}
				MemoryStream memoryStream = Global.SerializeToStream(customGadget);
				dataRow["GadgetData"] = memoryStream.ToArray();
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CustomGadgetTable.Rows.Add(dataRow);
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
			base.DialogResult = DialogResult.None;
			SaveData();
			textBoxCode.Focus();
		}

		public void LoadData(string id)
		{
			try
			{
				if (!(id.Trim() == "") && CanClose())
				{
					currentData = Factory.CustomGadgetSystem.GetCustomGadgetByID(id.Trim());
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
						if (toolStripButtonShowPicture.Checked && linkLoadImage.Visible)
						{
							LoadPhoto();
						}
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
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables[0].Rows[0];
					textBoxCode.Text = dataRow["CustomGadgetID"].ToString();
					textBoxName.Text = dataRow["CustomGadgetName"].ToString();
					textBoxDisplayName.Text = dataRow["DisplayName"].ToString();
					textBoxDescription.Text = dataRow["Description"].ToString();
					textBoxArgColumn.Text = dataRow["ChartArgColumn"].ToString();
					comboBoxGroup.SelectedIndex = ((dataRow["CategoryID"] == DBNull.Value) ? (-1) : checked(int.Parse(dataRow["CategoryID"].ToString()) - 1));
					comboBoxFilterOption.SelectedIndex = ((dataRow["FilterOption"] != DBNull.Value) ? byte.Parse(dataRow["FilterOption"].ToString()) : 0);
					if (!dataRow["AutoRefresh"].IsDBNullOrEmpty())
					{
						checkBoxAutoRefresh.Checked = bool.Parse(dataRow["AutoRefresh"].ToString());
					}
					else
					{
						checkBoxAutoRefresh.Checked = false;
					}
					if (!dataRow["RefreshInterval"].IsDBNullOrEmpty())
					{
						numericUpDownRefreshInterval.Value = int.Parse(dataRow["RefreshInterval"].ToString());
					}
					else
					{
						numericUpDownRefreshInterval.Value = 300m;
					}
					if (!dataRow["GShowGaugeText"].IsDBNullOrEmpty())
					{
						checkBoxGShowGaugeText.Checked = bool.Parse(dataRow["GShowGaugeText"].ToString());
					}
					else
					{
						checkBoxGShowGaugeText.Checked = true;
					}
					if (!dataRow["GShowNeedle"].IsDBNullOrEmpty())
					{
						checkBoxGShowNeedle.Checked = bool.Parse(dataRow["GShowNeedle"].ToString());
					}
					else
					{
						checkBoxGShowNeedle.Checked = true;
					}
					dataRow["RefreshInterval"] = numericUpDownRefreshInterval.Value;
					comboBoxColorPalette.SelectedItem = dataRow["ColorPaletteName"].ToString();
					numericUpDownStartColor.Value = ((!dataRow["ChartPallet"].IsDBNullOrEmpty()) ? int.Parse(dataRow["ChartPallet"].ToString()) : 0);
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
					if (dataRow["HasPhoto"] != DBNull.Value)
					{
						bool flag = Convert.ToBoolean(byte.Parse(dataRow["HasPhoto"].ToString()));
						linkLoadImage.Visible = flag;
						linkRemovePicture.Enabled = flag;
						if (flag)
						{
							pictureBoxPhoto.Image = null;
						}
						else
						{
							pictureBoxPhoto.Image = pictureBoxNoImage.Image;
						}
					}
					else
					{
						linkLoadImage.Visible = false;
						pictureBoxPhoto.Image = pictureBoxNoImage.Image;
						linkRemovePicture.Enabled = false;
					}
					GadgetStyles gadgetStyles = (GadgetStyles)0;
					if (dataRow["GadgetStyle"] != DBNull.Value)
					{
						gadgetStyles = (GadgetStyles)int.Parse(dataRow["GadgetStyle"].ToString());
					}
					if (!dataRow["ColorEach"].IsDBNullOrEmpty())
					{
						checkBoxColorEach.Checked = bool.Parse(dataRow["ColorEach"].ToString());
					}
					else
					{
						checkBoxColorEach.Checked = false;
					}
					switch (gadgetStyles)
					{
					case GadgetStyles.Chart:
						comboBoxType.SelectedIndex = 1;
						checkBoxLegend.Checked = (!dataRow["ShowLegend"].IsDBNullOrEmpty() && bool.Parse(dataRow["ShowLegend"].ToString()));
						checkBoxRotated.Checked = (!dataRow["IsRotated"].IsDBNullOrEmpty() && bool.Parse(dataRow["IsRotated"].ToString()));
						checkBoxShowAxisY.Checked = (!dataRow["AxisYVisible"].IsDBNullOrEmpty() && bool.Parse(dataRow["AxisYVisible"].ToString()));
						textBoxAxisXTitle.Text = dataRow["AxisXTitle"].ToString();
						textBoxAxisYTitle.Text = dataRow["AxisYTitle"].ToString();
						textBoxAxisYPattern.Text = dataRow["AxisYTextPattern"].ToString();
						textBoxLegendPattern.Text = dataRow["LegendTextPattern"].ToString();
						numericUpDownNCount.Value = (dataRow["TopNCount"].IsDBNullOrEmpty() ? 5 : int.Parse(dataRow["TopNCount"].ToString()));
						checkBoxShowTopN.Checked = (!dataRow["TopNOption"].IsDBNullOrEmpty() && bool.Parse(dataRow["TopNOption"].ToString()));
						textBoxTopNOther.Text = dataRow["TopNOthersText"].ToString();
						checkBoxShowOthers.Checked = (!dataRow["ShowTopNOther"].IsDBNullOrEmpty() && bool.Parse(dataRow["ShowTopNOther"].ToString()));
						break;
					case GadgetStyles.Gauge:
						comboBoxType.SelectedIndex = 2;
						if (!dataRow["ShowLegend"].IsDBNullOrEmpty())
						{
							checkBoxGaugeShowLabel.Checked = bool.Parse(dataRow["ShowLegend"].ToString());
						}
						textBoxGaugeValueColumn.Text = dataRow["ChartVAlueColumn"].ToString();
						textBoxGaugeValueFormat.Text = dataRow["AxisYTextPattern"].ToString();
						if (!dataRow["TopNOption"].IsDBNullOrEmpty())
						{
							checkBoxGaugeRangeAsValue.Checked = bool.Parse(dataRow["TopNOption"].ToString());
						}
						break;
					case GadgetStyles.Number:
						comboBoxType.SelectedIndex = 3;
						textBoxNumberValueColumn.Text = dataRow["ChartVAlueColumn"].ToString();
						textBoxNumIndicatorValueColumn.Text = dataRow["IndValueColumn"].ToString();
						textBoxNumIndTextVal.Text = dataRow["IndTextValueColumn"].ToString();
						checkBoxNumShowIndicator.Checked = (!dataRow["ShowIndicator"].IsDBNullOrEmpty() && bool.Parse(dataRow["ShowIndicator"].ToString()));
						checkBoxNumShowIndText.Checked = (!dataRow["ShowName"].IsDBNullOrEmpty() && bool.Parse(dataRow["ShowName"].ToString()));
						colorEditNumTextColor.Color = (dataRow["TextColor"].IsDBNullOrEmpty() ? Color.Transparent : Color.FromArgb(int.Parse(dataRow["TextColor"].ToString())));
						textBoxNumValueFormat.Text = dataRow["AxisYTextPattern"].ToString();
						textBoxNumIndTextFormat.Text = dataRow["LegendTextPattern"].ToString();
						if (colorEditNumTextColor.Color.IsNullOrEmpty())
						{
							dataRow["TextColor"] = DBNull.Value;
						}
						else
						{
							dataRow["TextColor"] = colorEditNumTextColor.Color.ToArgb();
						}
						break;
					default:
						comboBoxType.SelectedIndex = 0;
						break;
					}
					DataTable dataTable = dataGridChartSeries.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in currentData.ChartSeriesTable.Rows)
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["Code"] = row["SeriesID"];
						dataRow3["ChartType"] = row["ChartType"];
						dataRow3["DisplayName"] = row["DisplayName"];
						dataRow3["ValueColumn"] = row["ChartVAlueColumn"];
						dataRow3["LabelVisible"] = (!row["LabelVisible"].IsDBNullOrEmpty() && bool.Parse(row["LabelVisible"].ToString()));
						dataRow3["LabelPosition"] = (row["LabelPosition"].IsDBNullOrEmpty() ? (-1) : int.Parse(row["LabelPosition"].ToString()));
						dataRow3["LabelTextPattern"] = row["LabelTextPattern"].ToString();
						dataTable.Rows.Add(dataRow3);
					}
					if (dataGridChartSeries.Rows.Count == 0 && dataRow["ChartType"] != DBNull.Value)
					{
						DataRow dataRow4 = dataTable.NewRow();
						dataRow4["Code"] = "Serie1";
						dataRow4["ChartType"] = byte.Parse(dataRow["ChartType"].ToString());
						dataRow4["DisplayName"] = "";
						dataRow4["ValueColumn"] = dataRow["ChartVAlueColumn"].ToString();
						dataTable.Rows.Add(dataRow4);
					}
					dataTable.AcceptChanges();
					dataTable = (dataGridGauge.DataSource as DataTable);
					dataTable.Rows.Clear();
					foreach (DataRow row2 in currentData.GaugeRangeTable.Rows)
					{
						DataRow dataRow6 = dataTable.NewRow();
						dataRow6["Code"] = row2["RangeID"];
						dataRow6["StartValue"] = row2["StartValue"];
						dataRow6["EndValue"] = row2["EndValue"];
						dataRow6["Color"] = row2["RangeColor"];
						dataTable.Rows.Add(dataRow6);
					}
					dataTable.AcceptChanges();
					dataTable = (dataGridHiddenColumns.DataSource as DataTable);
					dataTable.Rows.Clear();
					foreach (DataRow row3 in currentData.ListHiddenFieldsTable.Rows)
					{
						DataRow dataRow8 = dataTable.NewRow();
						dataRow8["Field"] = row3["FieldID"];
						dataTable.Rows.Add(dataRow8);
					}
					dataTable.AcceptChanges();
					byte[] streamBytes = (byte[])dataRow["GadgetData"];
					currentGadget = (CustomGadget)Global.DeserializeFromStream(streamBytes);
					listBoxTables.Items.Clear();
					if (currentGadget.Tables.Count > 0)
					{
						foreach (CustomGadgetTable table in currentGadget.Tables)
						{
							listBoxTables.Items.Add(table);
						}
					}
					listBoxRelations.Items.Clear();
					if (currentGadget.Relations != null && currentGadget.Relations.Count > 0)
					{
						foreach (GadgetRelation relation in currentGadget.Relations)
						{
							listBoxRelations.Items.Add(relation);
						}
					}
					listBoxParameters.Items.Clear();
					if (currentGadget.Parameters.Count > 0)
					{
						foreach (GadgetParameter parameter in currentGadget.Parameters)
						{
							listBoxParameters.Items.Add(parameter);
						}
					}
					DataTable dataTable2 = dataGridControls.DataSource as DataTable;
					dataTable2?.Rows.Clear();
					if (currentGadget.Controls != null && currentGadget.Controls.Count > 0)
					{
						foreach (CustomGadgetControl control in currentGadget.Controls)
						{
							DataRow dataRow9 = dataTable2.NewRow();
							dataRow9["ControlType"] = (int)control.ControlType;
							dataRow9["ValueType"] = control.ValueType;
							dataRow9["Key"] = control.Key;
							dataRow9["FieldName"] = control.FieldName;
							dataRow9["DisplayText"] = control.DisplayText;
							dataTable2.Rows.Add(dataRow9);
						}
					}
				}
			}
			catch (Exception e)
			{
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
				switch (ErrorHelper.QuestionMessageYesNoCancel("Do you want to save the changes?"))
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
				bool flag = (!isNewRecord) ? Factory.CustomGadgetSystem.UpdateCustomGadget(currentData) : Factory.CustomGadgetSystem.CreateCustomGadget(currentData);
				if (!flag)
				{
					ErrorHelper.ErrorMessage("Unable to save data.");
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
			try
			{
				if (!screenRight.New && isNewRecord)
				{
					ErrorHelper.WarningMessage("Access denied!");
					return false;
				}
				if (!screenRight.Edit && !isNewRecord)
				{
					ErrorHelper.WarningMessage("Access denied!");
					return false;
				}
				if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0)
				{
					ErrorHelper.InformationMessage("Please enter required fields.");
					return false;
				}
				if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Custom_Gadget", "CustomGadgetID", textBoxCode.Text.Trim()))
				{
					ErrorHelper.InformationMessage("Code already exist.");
					textBoxCode.Focus();
					return false;
				}
				foreach (UltraGridRow row in dataGridChartSeries.Rows)
				{
					if (row.Cells["Code"].Value.IsNullOrEmpty())
					{
						ErrorHelper.InformationMessage("Please enter a name for the chart serie.");
						row.Activate();
						return false;
					}
					if (row.Cells["ChartType"].Value.IsNullOrEmpty())
					{
						ErrorHelper.InformationMessage("Please select the chart type for the serie.");
						row.Activate();
						return false;
					}
					if (row.Cells["ValueColumn"].Value.IsNullOrEmpty())
					{
						ErrorHelper.InformationMessage("Please the value column name the serie.");
						row.Activate();
						return false;
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

		private void buttonNew_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.None;
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Custom_Gadget", "CustomGadgetID");
			comboBoxColorPalette.SelectedIndex = 0;
			numericUpDownStartColor.Value = 0m;
			textBoxGaugeValueColumn.Clear();
			textBoxNumberValueColumn.Clear();
			textBoxNumIndicatorValueColumn.Clear();
			textBoxNumIndTextVal.Clear();
			textBoxNumIndTextFormat.Clear();
			textBoxNumValueFormat.Clear();
			checkBoxNumShowIndicator.Checked = false;
			checkBoxNumShowIndText.Checked = false;
			colorEditNumTextColor.EditValue = null;
			checkBoxGShowNeedle.Checked = true;
			checkBoxGShowGaugeText.Checked = true;
			textBoxGaugeValueFormat.Clear();
			textBoxName.Clear();
			listBoxTables.Items.Clear();
			listBoxRelations.Items.Clear();
			listBoxParameters.Items.Clear();
			checkBoxAutoRefresh.Checked = false;
			checkBoxGaugeShowLabel.Checked = true;
			textBoxDrilCardID.Clear();
			comboBoxDrillDown.SelectedIndex = 0;
			comboBoxFilterOption.SelectedIndex = 0;
			textBoxArgColumn.Clear();
			checkBoxColorEach.Checked = false;
			checkBoxLegend.Checked = false;
			checkBoxRotated.Checked = false;
			checkBoxShowAxisY.Checked = false;
			textBoxAxisXTitle.Text = "";
			textBoxAxisYTitle.Text = "";
			textBoxAxisYPattern.Text = "";
			checkBoxShowTopN.Checked = false;
			checkBoxShowOthers.Checked = false;
			comboBoxType.SelectedIndex = 0;
			linkLoadImage.Visible = false;
			pictureBoxPhoto.Image = null;
			textBoxDescription.Clear();
			textBoxDisplayName.Clear();
			IsNewRecord = true;
			(dataGridControls.DataSource as DataTable)?.Rows.Clear();
			(dataGridChartSeries.DataSource as DataTable).Rows.Clear();
			(dataGridHiddenColumns.DataSource as DataTable).Rows.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void CustomGadgetGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void CustomGadgetGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (Delete())
			{
				ClearForm();
			}
		}

		private bool Delete()
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo("Are you sure to delete?") == DialogResult.No)
				{
					return false;
				}
				return Factory.CustomGadgetSystem.DeleteCustomGadget(textBoxCode.Text);
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
			LoadData(DatabaseHelper.GetNextID("Custom_Gadget", "CustomGadgetID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Custom_Gadget", "CustomGadgetID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Custom_Gadget", "CustomGadgetID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Custom_Gadget", "CustomGadgetID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Custom_Gadget", "CustomGadgetID", toolStripTextBoxFind.Text.Trim()))
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
					switch (ErrorHelper.QuestionMessageYesNoCancel("Do you want to save?"))
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

		private void CustomGadgetDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				LoadMenuComboBox();
				SetupGrid();
				SetupChartSeriesGrid();
				SetupGaugeRangesGrid();
				SetupListHiddenColumnsGrid();
				if (!base.IsDisposed && textBoxCode.Text == "")
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

		private void LoadMenuComboBox()
		{
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				Close();
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.CustomGadget);
		}

		private void buttonAddTable_Click(object sender, EventArgs e)
		{
			AddTableDialog addTableDialog = new AddTableDialog();
			if (addTableDialog.ShowDialog() == DialogResult.OK)
			{
				CustomGadgetTable customGadgetTable = new CustomGadgetTable();
				customGadgetTable.tableName = addTableDialog.TableName;
				customGadgetTable.query = addTableDialog.Query;
				listBoxTables.Items.Add(customGadgetTable);
			}
		}

		private void buttonAddRelation_Click(object sender, EventArgs e)
		{
			CustomGadget customGadget = new CustomGadget();
			foreach (object item2 in listBoxTables.Items)
			{
				CustomGadgetTable item = item2 as CustomGadgetTable;
				customGadget.Tables.Add(item);
			}
			AddRelationDialog addRelationDialog = new AddRelationDialog();
			addRelationDialog.LoadGadgetData(customGadget);
			if (addRelationDialog.ShowDialog() == DialogResult.OK)
			{
				listBoxRelations.Items.Add(addRelationDialog.Relationship);
			}
		}

		private void buttonDeleteRelation_Click(object sender, EventArgs e)
		{
			if (listBoxRelations.SelectedItem != null && ErrorHelper.QuestionMessageYesNo("Delete the relationship?") == DialogResult.Yes)
			{
				listBoxRelations.Items.Remove(listBoxRelations.SelectedItem);
			}
		}

		private void buttonEditRelation_Click(object sender, EventArgs e)
		{
			if (listBoxRelations.SelectedItem != null)
			{
				CustomGadget customGadget = new CustomGadget();
				foreach (object item2 in listBoxTables.Items)
				{
					CustomGadgetTable item = item2 as CustomGadgetTable;
					customGadget.Tables.Add(item);
				}
				AddRelationDialog addRelationDialog = new AddRelationDialog();
				addRelationDialog.LoadGadgetData(customGadget);
				addRelationDialog.EditRelation((GadgetRelation)listBoxRelations.SelectedItem);
				if (addRelationDialog.ShowDialog() == DialogResult.OK)
				{
					listBoxRelations.Items.Remove(listBoxRelations.SelectedItem);
					listBoxRelations.Items.Add(addRelationDialog.Relationship);
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

		private void buttonEditTable_Click(object sender, EventArgs e)
		{
			if (listBoxTables.SelectedItem != null)
			{
				AddTableDialog addTableDialog = new AddTableDialog();
				addTableDialog.EditTable((CustomGadgetTable)listBoxTables.SelectedItem);
				if (addTableDialog.ShowDialog() == DialogResult.OK)
				{
					listBoxTables.Items.Remove(listBoxTables.SelectedItem);
					listBoxTables.Items.Add(addTableDialog.GadgetTable);
				}
			}
		}

		private void SetupGaugeRangesGrid()
		{
			try
			{
				dataGridGauge.SetupUI();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Code", typeof(string));
				dataTable.Columns.Add("StartValue", typeof(decimal));
				dataTable.Columns.Add("EndValue", typeof(decimal));
				dataTable.Columns.Add("Setting");
				dataTable.Columns.Add("Color", typeof(int));
				dataGridGauge.DataSource = dataTable;
				dataGridGauge.DisplayLayout.Bands[0].Columns["Color"].Hidden = true;
				dataGridGauge.DisplayLayout.Bands[0].Columns["Setting"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
				dataGridGauge.DisplayLayout.Bands[0].Columns["Setting"].MaxWidth = 20;
				dataGridGauge.DisplayLayout.Bands[0].Columns["Setting"].Width = 20;
				dataGridGauge.DisplayLayout.Bands[0].Columns["Setting"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
				dataGridGauge.DisplayLayout.Bands[0].Columns["Setting"].CellButtonAppearance.Image = imageList1.Images[6];
				dataGridGauge.AllowAddNew = true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void SetupListHiddenColumnsGrid()
		{
			try
			{
				dataGridHiddenColumns.SetupUI();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Field", typeof(string));
				dataGridHiddenColumns.DataSource = dataTable;
				dataGridHiddenColumns.AllowAddNew = true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void SetupChartSeriesGrid()
		{
			try
			{
				dataGridChartSeries.SetupUI();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Code", typeof(string));
				dataTable.Columns.Add("ChartType", typeof(int));
				dataTable.Columns.Add("DisplayName", typeof(string));
				dataTable.Columns.Add("ValueColumn");
				dataTable.Columns.Add("Setting");
				dataTable.Columns.Add("Color", typeof(int));
				dataTable.Columns.Add("LabelVisible", typeof(bool));
				dataTable.Columns.Add("LabelPosition", typeof(int));
				dataTable.Columns.Add("LabelTextPattern", typeof(string));
				dataGridChartSeries.DataSource = dataTable;
				UltraGridColumn ultraGridColumn = dataGridChartSeries.DisplayLayout.Bands[0].Columns["Color"];
				UltraGridColumn ultraGridColumn2 = dataGridChartSeries.DisplayLayout.Bands[0].Columns["LabelVisible"];
				UltraGridColumn ultraGridColumn3 = dataGridChartSeries.DisplayLayout.Bands[0].Columns["LabelTextPattern"];
				bool flag2 = dataGridChartSeries.DisplayLayout.Bands[0].Columns["LabelPosition"].Hidden = true;
				bool flag4 = ultraGridColumn3.Hidden = flag2;
				bool hidden = ultraGridColumn2.Hidden = flag4;
				ultraGridColumn.Hidden = hidden;
				ValueList valueList = new ValueList();
				valueList.ValueListItems.Add(0, "Bar");
				valueList.ValueListItems.Add(1, "Stacked Bar");
				valueList.ValueListItems.Add(2, "Full Stacked Bar");
				valueList.ValueListItems.Add(30, "Side By Side Range Bar");
				valueList.ValueListItems.Add(4, "Side By Side Full Stacked Bar");
				valueList.ValueListItems.Add(44, "Bar 3D");
				valueList.ValueListItems.Add(45, "Stacked Bar 3D");
				valueList.ValueListItems.Add(47, "Manhattan Bar");
				valueList.ValueListItems.Add(9, "Point");
				valueList.ValueListItems.Add(10, "Bubble");
				valueList.ValueListItems.Add(11, "Line");
				valueList.ValueListItems.Add(53, "Line 3D");
				valueList.ValueListItems.Add(5, "Pie");
				valueList.ValueListItems.Add(6, "Doughnut");
				valueList.ValueListItems.Add(50, "Pie 3D");
				valueList.ValueListItems.Add(51, "Doughnut 3D");
				valueList.ValueListItems.Add(8, "Funnel");
				valueList.ValueListItems.Add(52, "Funnel 3D");
				valueList.ValueListItems.Add(18, "Area");
				valueList.ValueListItems.Add(21, "Stacked Area");
				dataGridChartSeries.DisplayLayout.Bands[0].Columns["Setting"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
				dataGridChartSeries.DisplayLayout.Bands[0].Columns["Setting"].MaxWidth = 20;
				dataGridChartSeries.DisplayLayout.Bands[0].Columns["Setting"].Width = 20;
				dataGridChartSeries.DisplayLayout.Bands[0].Columns["Setting"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
				dataGridChartSeries.DisplayLayout.Bands[0].Columns["Setting"].CellButtonAppearance.Image = imageList1.Images[6];
				dataGridChartSeries.DisplayLayout.Bands[0].Columns["ChartType"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.EditButton;
				dataGridChartSeries.DisplayLayout.Bands[0].Columns["ChartType"].Header.Caption = "Chart Type";
				dataGridChartSeries.DisplayLayout.Bands[0].Columns["DisplayName"].Header.Caption = "Display Name";
				dataGridChartSeries.DisplayLayout.Bands[0].Columns["ChartType"].CellActivation = Activation.NoEdit;
				dataGridChartSeries.DisplayLayout.Bands[0].Columns["ChartType"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
				dataGridChartSeries.DisplayLayout.Bands[0].Columns["ChartType"].CellButtonAppearance.Image = imageList1.Images[1];
				dataGridChartSeries.DisplayLayout.Bands[0].Columns["ChartType"].ValueList = valueList;
				dataGridChartSeries.AllowAddNew = true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataGridControls.SetupUI();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("ControlType", typeof(string));
				dataTable.Columns.Add("ValueType", typeof(string));
				dataTable.Columns.Add("DisplayText", typeof(string));
				dataTable.Columns.Add("FieldName");
				dataTable.Columns.Add("Key");
				dataGridControls.DataSource = dataTable;
				ValueList valueList = new ValueList();
				CRCTypes[] array = (CRCTypes[])Enum.GetValues(typeof(CRCTypes));
				for (int i = 0; i < array.Length; i++)
				{
					CRCTypes cRCTypes = array[i];
					valueList.ValueListItems.Add((int)cRCTypes, cRCTypes.ToString());
				}
				dataGridControls.DisplayLayout.Bands[0].Columns["ControlType"].ValueList = valueList;
				dataGridControls.DisplayLayout.Bands[0].Columns["ControlType"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
				valueList = new ValueList();
				valueList.ValueListItems.Add(1, "Parameter");
				valueList.ValueListItems.Add(2, "Subquery");
				dataGridControls.DisplayLayout.Bands[0].Columns["ValueType"].ValueList = valueList;
				dataGridControls.DisplayLayout.Bands[0].Columns["ValueType"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonAddParameter_Click(object sender, EventArgs e)
		{
			AddParameterDialog addParameterDialog = new AddParameterDialog();
			if (addParameterDialog.ShowDialog() == DialogResult.OK)
			{
				GadgetParameter gadgetParameter = new GadgetParameter();
				gadgetParameter.ParameterName = addParameterDialog.ParameterName;
				gadgetParameter.ParameterType = addParameterDialog.DataType;
				listBoxParameters.Items.Add(gadgetParameter);
			}
		}

		private void buttonDeleteParameter_Click(object sender, EventArgs e)
		{
			if (listBoxParameters.SelectedItem != null && ErrorHelper.QuestionMessageYesNo("Delete the parameter?") == DialogResult.Yes)
			{
				listBoxParameters.Items.Remove(listBoxParameters.SelectedItem);
			}
		}

		private void buttonEditParameter_Click(object sender, EventArgs e)
		{
			if (listBoxParameters.SelectedItem != null)
			{
				AddParameterDialog addParameterDialog = new AddParameterDialog();
				addParameterDialog.EditParameter((GadgetParameter)listBoxParameters.SelectedItem);
				if (addParameterDialog.ShowDialog() == DialogResult.OK)
				{
					listBoxParameters.Items.Remove(listBoxParameters.SelectedItem);
					listBoxParameters.Items.Add(addParameterDialog.GadgetParameter);
				}
			}
		}

		private void toolStripButtonLayout_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0)
				{
					saveFileDialog1.AddExtension = true;
					saveFileDialog1.Filter = "Axolon Gadgets|*.axg";
					saveFileDialog1.FileName = currentData.CustomGadgetTable.Rows[0]["CustomGadgetID"].ToString();
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
				openFileDialog1.Filter = "Axolon Gadgets|*.axg";
				openFileDialog1.FileName = "";
				if (openFileDialog1.ShowDialog() == DialogResult.OK)
				{
					currentData = new CustomGadgetData();
					currentData.ReadXml(openFileDialog1.FileName, XmlReadMode.ReadSchema);
					if (!currentData.CustomGadgetTable.Columns.Contains("HasPhoto"))
					{
						currentData.CustomGadgetTable.Columns.Add("HasPhoto");
						currentData.CustomGadgetTable.Rows[0]["HasPhoto"] = 0;
					}
					FillData();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void checkBoxAutoRefresh_CheckedChanged(object sender, EventArgs e)
		{
			numericUpDownRefreshInterval.Enabled = checkBoxAutoRefresh.Checked;
		}

		private void buttonAddSerie_Click(object sender, EventArgs e)
		{
		}

		private void checkBoxShowOthers_CheckedChanged(object sender, EventArgs e)
		{
			textBoxTopNOther.Enabled = checkBoxShowOthers.Checked;
		}

		private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure to duplicate this document?") == DialogResult.Yes)
			{
				string text = textBoxCode.Text;
				if (!IsDirty)
				{
					IsNewRecord = true;
					textBoxCode.Text = PublicFunctions.GetNextCardNumber("Custom_Gadget", "CustomGadgetID");
				}
				else if (CanClose())
				{
					LoadData(text);
					IsNewRecord = true;
					textBoxCode.Text = PublicFunctions.GetNextCardNumber("Custom_Gadget", "CustomGadgetID");
				}
			}
		}

		private void linkAddPicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					Image image = Image.FromFile(openFileDialog1.FileName);
					if (PublicFunctions.AddGadgetPhoto(textBoxCode.Text, image, isMatrixParent: false))
					{
						pictureBoxPhoto.Image = image;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Cannot add picture.");
			}
		}

		private void linkRemovePicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Are you sure to remove the item image?") == DialogResult.Yes)
				{
					if (Factory.ProductSystem.RemoveProductPhoto(textBoxCode.Text))
					{
						pictureBoxPhoto.Image = pictureBoxNoImage.Image;
					}
					else
					{
						ErrorHelper.ErrorMessage("Cannot remove the image.");
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Cannot remove image.");
			}
		}

		private void LoadPhoto()
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord)
				{
					pictureBoxPhoto.Image = PublicFunctions.GetGadgetThumbnailImage(textBoxCode.Text);
					linkLoadImage.Visible = false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void linkLoadImage_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			LoadPhoto();
		}

		private void toolStripButtonDuplicate_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure to duplicate this document?") == DialogResult.Yes)
			{
				string text = textBoxCode.Text;
				if (!IsDirty)
				{
					IsNewRecord = true;
					textBoxCode.Text = PublicFunctions.GetNextCardNumber("Custom_Gadget", "CustomGadgetID");
				}
				else if (CanClose())
				{
					LoadData(text);
					IsNewRecord = true;
					textBoxCode.Text = PublicFunctions.GetNextCardNumber("Custom_Gadget", "CustomGadgetID");
				}
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.CustomDashboards.CustomGadgetDetailForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			linkLoadImage = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			pictureBoxPhoto = new System.Windows.Forms.PictureBox();
			pictureBoxNoImage = new System.Windows.Forms.PictureBox();
			linkRemovePicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkAddPicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxDescription = new Micromind.UISupport.MMTextBox();
			label18 = new System.Windows.Forms.Label();
			textBoxDisplayName = new Micromind.UISupport.MMTextBox();
			label4 = new System.Windows.Forms.Label();
			label17 = new System.Windows.Forms.Label();
			numericUpDownRefreshInterval = new System.Windows.Forms.NumericUpDown();
			checkBoxAutoRefresh = new System.Windows.Forms.CheckBox();
			label6 = new System.Windows.Forms.Label();
			comboBoxFilterOption = new System.Windows.Forms.ComboBox();
			label5 = new System.Windows.Forms.Label();
			comboBoxGroup = new System.Windows.Forms.ComboBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			comboBoxType = new System.Windows.Forms.ComboBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			tabPageChart = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			numericUpDownStartColor = new System.Windows.Forms.NumericUpDown();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			comboBoxColorPalette = new System.Windows.Forms.ComboBox();
			textBoxTopNOther = new Micromind.UISupport.MMTextBox();
			numericUpDownNCount = new System.Windows.Forms.NumericUpDown();
			checkBoxShowOthers = new System.Windows.Forms.CheckBox();
			checkBoxShowTopN = new System.Windows.Forms.CheckBox();
			textBoxLegendPattern = new Micromind.UISupport.MMTextBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			textBoxAxisYPattern = new Micromind.UISupport.MMTextBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxAxisYTitle = new Micromind.UISupport.MMTextBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxAxisXTitle = new Micromind.UISupport.MMTextBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			checkBoxShowAxisY = new System.Windows.Forms.CheckBox();
			checkBoxRotated = new System.Windows.Forms.CheckBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			textBoxArgColumn = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			checkBoxLegend = new System.Windows.Forms.CheckBox();
			checkBoxColorEach = new System.Windows.Forms.CheckBox();
			dataGridChartSeries = new Micromind.DataControls.DataEntryGrid();
			tabPageNumber = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxNumIndTextFormat = new Micromind.UISupport.MMTextBox();
			mmLabel18 = new Micromind.UISupport.MMLabel();
			textBoxNumValueFormat = new Micromind.UISupport.MMTextBox();
			mmLabel17 = new Micromind.UISupport.MMLabel();
			mmLabel16 = new Micromind.UISupport.MMLabel();
			colorEditNumTextColor = new DevExpress.XtraEditors.ColorEdit();
			textBoxNumberValueColumn = new Micromind.UISupport.MMTextBox();
			mmLabel15 = new Micromind.UISupport.MMLabel();
			textBoxNumIndTextVal = new Micromind.UISupport.MMTextBox();
			textBoxNumIndicatorValueColumn = new Micromind.UISupport.MMTextBox();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			checkBoxNumShowIndText = new System.Windows.Forms.CheckBox();
			checkBoxNumShowIndicator = new System.Windows.Forms.CheckBox();
			tabPageGauge = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			checkBoxGaugeRangeAsValue = new System.Windows.Forms.CheckBox();
			checkBoxGaugeShowLabel = new System.Windows.Forms.CheckBox();
			textBoxGaugeValueFormat = new Micromind.UISupport.MMTextBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			checkBoxGShowNeedle = new System.Windows.Forms.CheckBox();
			checkBoxGShowGaugeText = new System.Windows.Forms.CheckBox();
			textBoxGaugeValueColumn = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel19 = new Micromind.UISupport.MMLabel();
			dataGridGauge = new Micromind.DataControls.DataEntryGrid();
			tabPageList = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel20 = new Micromind.UISupport.MMLabel();
			dataGridHiddenColumns = new Micromind.DataControls.DataEntryGrid();
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
			label7 = new System.Windows.Forms.Label();
			comboBoxDrillDown = new System.Windows.Forms.ComboBox();
			panelActionTransaction = new System.Windows.Forms.Panel();
			checkBoxTransactionPreview = new System.Windows.Forms.CheckBox();
			textBoxActionTRVoucher = new System.Windows.Forms.TextBox();
			label8 = new System.Windows.Forms.Label();
			textBoxActionTRSysDocID = new System.Windows.Forms.TextBox();
			label9 = new System.Windows.Forms.Label();
			panelActionCard = new System.Windows.Forms.Panel();
			comboBoxCardTypes = new Micromind.DataControls.CardTypesComboBox();
			label10 = new System.Windows.Forms.Label();
			textBoxDrilCardID = new System.Windows.Forms.TextBox();
			label11 = new System.Windows.Forms.Label();
			panelActionSmartList = new System.Windows.Forms.Panel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSubReport = new Micromind.DataControls.SubSmartListComboBox();
			label13 = new System.Windows.Forms.Label();
			textBoxParm4 = new System.Windows.Forms.TextBox();
			textBoxParm3 = new System.Windows.Forms.TextBox();
			label12 = new System.Windows.Forms.Label();
			label14 = new System.Windows.Forms.Label();
			textBoxParm2 = new System.Windows.Forms.TextBox();
			label15 = new System.Windows.Forms.Label();
			textBoxParm1 = new System.Windows.Forms.TextBox();
			label16 = new System.Windows.Forms.Label();
			tabPageContacts = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridControls = new Micromind.DataControls.DataEntryGrid();
			dataSetData = new System.Data.DataSet();
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
			toolStripButtonDuplicate = new System.Windows.Forms.ToolStripButton();
			toolStripButtonShowPicture = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			imageList1 = new System.Windows.Forms.ImageList(components);
			formManager = new Micromind.DataControls.FormManager();
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownRefreshInterval).BeginInit();
			tabPageChart.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownStartColor).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownNCount).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridChartSeries).BeginInit();
			tabPageNumber.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)colorEditNumTextColor.Properties).BeginInit();
			tabPageGauge.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridGauge).BeginInit();
			tabPageList.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridHiddenColumns).BeginInit();
			tabPageDetails.SuspendLayout();
			ultraTabPageControl1.SuspendLayout();
			panelActionTransaction.SuspendLayout();
			panelActionCard.SuspendLayout();
			panelActionSmartList.SuspendLayout();
			tabPageContacts.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridControls).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataSetData).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(linkLoadImage);
			tabPageGeneral.Controls.Add(pictureBoxPhoto);
			tabPageGeneral.Controls.Add(pictureBoxNoImage);
			tabPageGeneral.Controls.Add(linkRemovePicture);
			tabPageGeneral.Controls.Add(linkAddPicture);
			tabPageGeneral.Controls.Add(textBoxDescription);
			tabPageGeneral.Controls.Add(label18);
			tabPageGeneral.Controls.Add(textBoxDisplayName);
			tabPageGeneral.Controls.Add(label4);
			tabPageGeneral.Controls.Add(label17);
			tabPageGeneral.Controls.Add(numericUpDownRefreshInterval);
			tabPageGeneral.Controls.Add(checkBoxAutoRefresh);
			tabPageGeneral.Controls.Add(label6);
			tabPageGeneral.Controls.Add(comboBoxFilterOption);
			tabPageGeneral.Controls.Add(label5);
			tabPageGeneral.Controls.Add(comboBoxGroup);
			tabPageGeneral.Controls.Add(mmLabel2);
			tabPageGeneral.Controls.Add(comboBoxType);
			tabPageGeneral.Controls.Add(textBoxName);
			tabPageGeneral.Controls.Add(textBoxCode);
			tabPageGeneral.Controls.Add(mmLabel1);
			tabPageGeneral.Controls.Add(labelCode);
			tabPageGeneral.Location = new System.Drawing.Point(2, 21);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(709, 404);
			linkLoadImage.AutoSize = true;
			linkLoadImage.Location = new System.Drawing.Point(607, 76);
			linkLoadImage.Name = "linkLoadImage";
			linkLoadImage.Size = new System.Drawing.Size(66, 14);
			linkLoadImage.TabIndex = 65;
			linkLoadImage.TabStop = true;
			linkLoadImage.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLoadImage.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLoadImage.Value = "Load Picture";
			appearance.ForeColor = System.Drawing.Color.Blue;
			linkLoadImage.VisitedLinkAppearance = appearance;
			linkLoadImage.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLoadImage_LinkClicked);
			pictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxPhoto.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxPhoto.Location = new System.Drawing.Point(578, 28);
			pictureBoxPhoto.Name = "pictureBoxPhoto";
			pictureBoxPhoto.Size = new System.Drawing.Size(128, 128);
			pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxPhoto.TabIndex = 64;
			pictureBoxPhoto.TabStop = false;
			pictureBoxNoImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxNoImage.Image = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.Location = new System.Drawing.Point(648, 99);
			pictureBoxNoImage.Name = "pictureBoxNoImage";
			pictureBoxNoImage.Size = new System.Drawing.Size(49, 48);
			pictureBoxNoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxNoImage.TabIndex = 68;
			pictureBoxNoImage.TabStop = false;
			pictureBoxNoImage.Visible = false;
			linkRemovePicture.AutoSize = true;
			linkRemovePicture.Location = new System.Drawing.Point(617, 160);
			linkRemovePicture.Name = "linkRemovePicture";
			linkRemovePicture.Size = new System.Drawing.Size(45, 14);
			linkRemovePicture.TabIndex = 67;
			linkRemovePicture.TabStop = true;
			linkRemovePicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkRemovePicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkRemovePicture.Value = "Remove";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			linkRemovePicture.VisitedLinkAppearance = appearance2;
			linkRemovePicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkRemovePicture_LinkClicked);
			linkAddPicture.AutoSize = true;
			linkAddPicture.Location = new System.Drawing.Point(578, 160);
			linkAddPicture.Name = "linkAddPicture";
			linkAddPicture.Size = new System.Drawing.Size(23, 14);
			linkAddPicture.TabIndex = 66;
			linkAddPicture.TabStop = true;
			linkAddPicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkAddPicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkAddPicture.Value = "Add";
			appearance3.ForeColor = System.Drawing.Color.Blue;
			linkAddPicture.VisitedLinkAppearance = appearance3;
			linkAddPicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkAddPicture_LinkClicked);
			textBoxDescription.BackColor = System.Drawing.Color.White;
			textBoxDescription.CustomReportFieldName = "";
			textBoxDescription.CustomReportKey = "";
			textBoxDescription.CustomReportValueType = 1;
			textBoxDescription.IsComboTextBox = false;
			textBoxDescription.IsModified = false;
			textBoxDescription.Location = new System.Drawing.Point(116, 143);
			textBoxDescription.MaxLength = 64;
			textBoxDescription.Multiline = true;
			textBoxDescription.Name = "textBoxDescription";
			textBoxDescription.Size = new System.Drawing.Size(436, 63);
			textBoxDescription.TabIndex = 21;
			label18.AutoSize = true;
			label18.Location = new System.Drawing.Point(16, 143);
			label18.Name = "label18";
			label18.Size = new System.Drawing.Size(63, 13);
			label18.TabIndex = 20;
			label18.Text = "Description:";
			textBoxDisplayName.BackColor = System.Drawing.Color.White;
			textBoxDisplayName.CustomReportFieldName = "";
			textBoxDisplayName.CustomReportKey = "";
			textBoxDisplayName.CustomReportValueType = 1;
			textBoxDisplayName.IsComboTextBox = false;
			textBoxDisplayName.IsModified = false;
			textBoxDisplayName.Location = new System.Drawing.Point(116, 119);
			textBoxDisplayName.MaxLength = 64;
			textBoxDisplayName.Name = "textBoxDisplayName";
			textBoxDisplayName.Size = new System.Drawing.Size(436, 20);
			textBoxDisplayName.TabIndex = 19;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(16, 123);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(75, 13);
			label4.TabIndex = 18;
			label4.Text = "Display Name:";
			label17.AutoSize = true;
			label17.Location = new System.Drawing.Point(498, 99);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(47, 13);
			label17.TabIndex = 17;
			label17.Text = "seconds";
			numericUpDownRefreshInterval.Enabled = false;
			numericUpDownRefreshInterval.Location = new System.Drawing.Point(420, 96);
			numericUpDownRefreshInterval.Maximum = new decimal(new int[4]
			{
				3600,
				0,
				0,
				0
			});
			numericUpDownRefreshInterval.Name = "numericUpDownRefreshInterval";
			numericUpDownRefreshInterval.Size = new System.Drawing.Size(71, 20);
			numericUpDownRefreshInterval.TabIndex = 8;
			numericUpDownRefreshInterval.Value = new decimal(new int[4]
			{
				300,
				0,
				0,
				0
			});
			checkBoxAutoRefresh.AutoSize = true;
			checkBoxAutoRefresh.Location = new System.Drawing.Point(263, 96);
			checkBoxAutoRefresh.Name = "checkBoxAutoRefresh";
			checkBoxAutoRefresh.Size = new System.Drawing.Size(148, 17);
			checkBoxAutoRefresh.TabIndex = 7;
			checkBoxAutoRefresh.Text = "Auto refresh gadget every";
			checkBoxAutoRefresh.UseVisualStyleBackColor = true;
			checkBoxAutoRefresh.CheckedChanged += new System.EventHandler(checkBoxAutoRefresh_CheckedChanged);
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(301, 69);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(66, 13);
			label6.TabIndex = 14;
			label6.Text = "Filter Option:";
			comboBoxFilterOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxFilterOption.FormattingEnabled = true;
			comboBoxFilterOption.Items.AddRange(new object[2]
			{
				"None",
				"Date Period"
			});
			comboBoxFilterOption.Location = new System.Drawing.Point(373, 66);
			comboBoxFilterOption.Name = "comboBoxFilterOption";
			comboBoxFilterOption.Size = new System.Drawing.Size(179, 21);
			comboBoxFilterOption.TabIndex = 3;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(16, 69);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(39, 13);
			label5.TabIndex = 12;
			label5.Text = "Group:";
			comboBoxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxGroup.FormattingEnabled = true;
			comboBoxGroup.Items.AddRange(new object[7]
			{
				"General",
				"Accounts",
				"Sales & Customers",
				"Purchase & Vendors",
				"Inventory",
				"HR & Admin",
				"Miscellaneous"
			});
			comboBoxGroup.Location = new System.Drawing.Point(116, 66);
			comboBoxGroup.Name = "comboBoxGroup";
			comboBoxGroup.Size = new System.Drawing.Size(179, 21);
			comboBoxGroup.TabIndex = 2;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(16, 96);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(84, 13);
			mmLabel2.TabIndex = 4;
			mmLabel2.Text = "Gadget Type:";
			comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxType.FormattingEnabled = true;
			comboBoxType.Items.AddRange(new object[4]
			{
				"List",
				"Chart",
				"Gauge",
				"Number"
			});
			comboBoxType.Location = new System.Drawing.Point(116, 93);
			comboBoxType.Name = "comboBoxType";
			comboBoxType.Size = new System.Drawing.Size(121, 21);
			comboBoxType.TabIndex = 5;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(116, 43);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(436, 20);
			textBoxName.TabIndex = 1;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(116, 21);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(179, 20);
			textBoxCode.TabIndex = 0;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(16, 43);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(88, 13);
			mmLabel1.TabIndex = 7;
			mmLabel1.Text = "Gadget Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(16, 21);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(85, 13);
			labelCode.TabIndex = 5;
			labelCode.Text = "Gadget Code:";
			tabPageChart.Controls.Add(mmLabel12);
			tabPageChart.Controls.Add(numericUpDownStartColor);
			tabPageChart.Controls.Add(mmLabel11);
			tabPageChart.Controls.Add(comboBoxColorPalette);
			tabPageChart.Controls.Add(textBoxTopNOther);
			tabPageChart.Controls.Add(numericUpDownNCount);
			tabPageChart.Controls.Add(checkBoxShowOthers);
			tabPageChart.Controls.Add(checkBoxShowTopN);
			tabPageChart.Controls.Add(textBoxLegendPattern);
			tabPageChart.Controls.Add(mmLabel10);
			tabPageChart.Controls.Add(textBoxAxisYPattern);
			tabPageChart.Controls.Add(mmLabel9);
			tabPageChart.Controls.Add(textBoxAxisYTitle);
			tabPageChart.Controls.Add(mmLabel8);
			tabPageChart.Controls.Add(textBoxAxisXTitle);
			tabPageChart.Controls.Add(mmLabel7);
			tabPageChart.Controls.Add(checkBoxShowAxisY);
			tabPageChart.Controls.Add(checkBoxRotated);
			tabPageChart.Controls.Add(mmLabel6);
			tabPageChart.Controls.Add(textBoxArgColumn);
			tabPageChart.Controls.Add(mmLabel4);
			tabPageChart.Controls.Add(checkBoxLegend);
			tabPageChart.Controls.Add(checkBoxColorEach);
			tabPageChart.Controls.Add(dataGridChartSeries);
			tabPageChart.Location = new System.Drawing.Point(-10000, -10000);
			tabPageChart.Name = "tabPageChart";
			tabPageChart.Size = new System.Drawing.Size(709, 404);
			mmLabel12.AutoSize = true;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(15, 245);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(59, 13);
			mmLabel12.TabIndex = 34;
			mmLabel12.Text = "Start Color:";
			numericUpDownStartColor.Location = new System.Drawing.Point(130, 241);
			numericUpDownStartColor.Maximum = new decimal(new int[4]
			{
				3,
				0,
				0,
				0
			});
			numericUpDownStartColor.Name = "numericUpDownStartColor";
			numericUpDownStartColor.Size = new System.Drawing.Size(75, 20);
			numericUpDownStartColor.TabIndex = 3;
			mmLabel11.AutoSize = true;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(15, 219);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(70, 13);
			mmLabel11.TabIndex = 32;
			mmLabel11.Text = "Color Palette:";
			comboBoxColorPalette.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxColorPalette.FormattingEnabled = true;
			comboBoxColorPalette.Items.AddRange(new object[21]
			{
				"Default",
				"Black and White",
				"Blue",
				"Blue Green",
				"Blue Warm",
				"Civic",
				"Equity",
				"Flow",
				"Green",
				"Green Yellow",
				"Marquee",
				"Median",
				"Metro",
				"Mixed",
				"MOdule",
				"Office",
				"Office 2013",
				"Orange",
				"Orange Red",
				"Oriel",
				"Origin"
			});
			comboBoxColorPalette.Location = new System.Drawing.Point(130, 215);
			comboBoxColorPalette.Name = "comboBoxColorPalette";
			comboBoxColorPalette.Size = new System.Drawing.Size(168, 21);
			comboBoxColorPalette.TabIndex = 2;
			textBoxTopNOther.BackColor = System.Drawing.Color.White;
			textBoxTopNOther.CustomReportFieldName = "";
			textBoxTopNOther.CustomReportKey = "";
			textBoxTopNOther.CustomReportValueType = 1;
			textBoxTopNOther.Enabled = false;
			textBoxTopNOther.IsComboTextBox = false;
			textBoxTopNOther.IsModified = false;
			textBoxTopNOther.Location = new System.Drawing.Point(483, 330);
			textBoxTopNOther.MaxLength = 30;
			textBoxTopNOther.Name = "textBoxTopNOther";
			textBoxTopNOther.Size = new System.Drawing.Size(157, 20);
			textBoxTopNOther.TabIndex = 13;
			numericUpDownNCount.Location = new System.Drawing.Point(483, 304);
			numericUpDownNCount.Name = "numericUpDownNCount";
			numericUpDownNCount.Size = new System.Drawing.Size(94, 20);
			numericUpDownNCount.TabIndex = 12;
			checkBoxShowOthers.AutoSize = true;
			checkBoxShowOthers.Location = new System.Drawing.Point(368, 331);
			checkBoxShowOthers.Name = "checkBoxShowOthers";
			checkBoxShowOthers.Size = new System.Drawing.Size(85, 17);
			checkBoxShowOthers.TabIndex = 28;
			checkBoxShowOthers.Text = "Show others";
			checkBoxShowOthers.UseVisualStyleBackColor = true;
			checkBoxShowOthers.CheckedChanged += new System.EventHandler(checkBoxShowOthers_CheckedChanged);
			checkBoxShowTopN.AutoSize = true;
			checkBoxShowTopN.Location = new System.Drawing.Point(368, 306);
			checkBoxShowTopN.Name = "checkBoxShowTopN";
			checkBoxShowTopN.Size = new System.Drawing.Size(109, 17);
			checkBoxShowTopN.TabIndex = 28;
			checkBoxShowTopN.Text = "Show top N items";
			checkBoxShowTopN.UseVisualStyleBackColor = true;
			textBoxLegendPattern.BackColor = System.Drawing.Color.White;
			textBoxLegendPattern.CustomReportFieldName = "";
			textBoxLegendPattern.CustomReportKey = "";
			textBoxLegendPattern.CustomReportValueType = 1;
			textBoxLegendPattern.IsComboTextBox = false;
			textBoxLegendPattern.IsModified = false;
			textBoxLegendPattern.Location = new System.Drawing.Point(480, 267);
			textBoxLegendPattern.MaxLength = 30;
			textBoxLegendPattern.Name = "textBoxLegendPattern";
			textBoxLegendPattern.Size = new System.Drawing.Size(157, 20);
			textBoxLegendPattern.TabIndex = 11;
			mmLabel10.AutoSize = true;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(365, 271);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(83, 13);
			mmLabel10.TabIndex = 27;
			mmLabel10.Text = "Legend Pattern:";
			textBoxAxisYPattern.BackColor = System.Drawing.Color.White;
			textBoxAxisYPattern.CustomReportFieldName = "";
			textBoxAxisYPattern.CustomReportKey = "";
			textBoxAxisYPattern.CustomReportValueType = 1;
			textBoxAxisYPattern.IsComboTextBox = false;
			textBoxAxisYPattern.IsModified = false;
			textBoxAxisYPattern.Location = new System.Drawing.Point(480, 241);
			textBoxAxisYPattern.MaxLength = 30;
			textBoxAxisYPattern.Name = "textBoxAxisYPattern";
			textBoxAxisYPattern.Size = new System.Drawing.Size(157, 20);
			textBoxAxisYPattern.TabIndex = 10;
			mmLabel9.AutoSize = true;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(365, 245);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(95, 13);
			mmLabel9.TabIndex = 25;
			mmLabel9.Text = "Axis Y text pattern:";
			textBoxAxisYTitle.BackColor = System.Drawing.Color.White;
			textBoxAxisYTitle.CustomReportFieldName = "";
			textBoxAxisYTitle.CustomReportKey = "";
			textBoxAxisYTitle.CustomReportValueType = 1;
			textBoxAxisYTitle.IsComboTextBox = false;
			textBoxAxisYTitle.IsModified = false;
			textBoxAxisYTitle.Location = new System.Drawing.Point(480, 215);
			textBoxAxisYTitle.MaxLength = 30;
			textBoxAxisYTitle.Name = "textBoxAxisYTitle";
			textBoxAxisYTitle.Size = new System.Drawing.Size(157, 20);
			textBoxAxisYTitle.TabIndex = 9;
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(365, 219);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(58, 13);
			mmLabel8.TabIndex = 25;
			mmLabel8.Text = "Axis Y title:";
			textBoxAxisXTitle.BackColor = System.Drawing.Color.White;
			textBoxAxisXTitle.CustomReportFieldName = "";
			textBoxAxisXTitle.CustomReportKey = "";
			textBoxAxisXTitle.CustomReportValueType = 1;
			textBoxAxisXTitle.IsComboTextBox = false;
			textBoxAxisXTitle.IsModified = false;
			textBoxAxisXTitle.Location = new System.Drawing.Point(480, 189);
			textBoxAxisXTitle.MaxLength = 30;
			textBoxAxisXTitle.Name = "textBoxAxisXTitle";
			textBoxAxisXTitle.Size = new System.Drawing.Size(157, 20);
			textBoxAxisXTitle.TabIndex = 8;
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(365, 193);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(58, 13);
			mmLabel7.TabIndex = 23;
			mmLabel7.Text = "Axis X title:";
			checkBoxShowAxisY.AutoSize = true;
			checkBoxShowAxisY.Location = new System.Drawing.Point(17, 343);
			checkBoxShowAxisY.Name = "checkBoxShowAxisY";
			checkBoxShowAxisY.Size = new System.Drawing.Size(84, 17);
			checkBoxShowAxisY.TabIndex = 7;
			checkBoxShowAxisY.Text = "Show axis Y";
			checkBoxShowAxisY.UseVisualStyleBackColor = true;
			checkBoxRotated.AutoSize = true;
			checkBoxRotated.Location = new System.Drawing.Point(17, 319);
			checkBoxRotated.Name = "checkBoxRotated";
			checkBoxRotated.Size = new System.Drawing.Size(64, 17);
			checkBoxRotated.TabIndex = 6;
			checkBoxRotated.Text = "Rotated";
			checkBoxRotated.UseVisualStyleBackColor = true;
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(15, 8);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(125, 13);
			mmLabel6.TabIndex = 20;
			mmLabel6.Text = "Enter chart series details:";
			textBoxArgColumn.BackColor = System.Drawing.Color.White;
			textBoxArgColumn.CustomReportFieldName = "";
			textBoxArgColumn.CustomReportKey = "";
			textBoxArgColumn.CustomReportValueType = 1;
			textBoxArgColumn.IsComboTextBox = false;
			textBoxArgColumn.IsModified = false;
			textBoxArgColumn.Location = new System.Drawing.Point(130, 189);
			textBoxArgColumn.MaxLength = 30;
			textBoxArgColumn.Name = "textBoxArgColumn";
			textBoxArgColumn.Size = new System.Drawing.Size(168, 20);
			textBoxArgColumn.TabIndex = 1;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(15, 192);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(93, 13);
			mmLabel4.TabIndex = 19;
			mmLabel4.Text = "Argument Column:";
			checkBoxLegend.AutoSize = true;
			checkBoxLegend.Location = new System.Drawing.Point(17, 295);
			checkBoxLegend.Name = "checkBoxLegend";
			checkBoxLegend.Size = new System.Drawing.Size(88, 17);
			checkBoxLegend.TabIndex = 5;
			checkBoxLegend.Text = "Show legend";
			checkBoxLegend.UseVisualStyleBackColor = true;
			checkBoxColorEach.AutoSize = true;
			checkBoxColorEach.Location = new System.Drawing.Point(17, 271);
			checkBoxColorEach.Name = "checkBoxColorEach";
			checkBoxColorEach.Size = new System.Drawing.Size(176, 17);
			checkBoxColorEach.TabIndex = 4;
			checkBoxColorEach.Text = "Use different color for each item";
			checkBoxColorEach.UseVisualStyleBackColor = true;
			dataGridChartSeries.AllowAddNew = false;
			dataGridChartSeries.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance4.BackColor = System.Drawing.SystemColors.Window;
			appearance4.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridChartSeries.DisplayLayout.Appearance = appearance4;
			dataGridChartSeries.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridChartSeries.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance5.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance5.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance5.BorderColor = System.Drawing.SystemColors.Window;
			dataGridChartSeries.DisplayLayout.GroupByBox.Appearance = appearance5;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridChartSeries.DisplayLayout.GroupByBox.BandLabelAppearance = appearance6;
			dataGridChartSeries.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance7.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance7.BackColor2 = System.Drawing.SystemColors.Control;
			appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridChartSeries.DisplayLayout.GroupByBox.PromptAppearance = appearance7;
			dataGridChartSeries.DisplayLayout.MaxColScrollRegions = 1;
			dataGridChartSeries.DisplayLayout.MaxRowScrollRegions = 1;
			appearance8.BackColor = System.Drawing.SystemColors.Window;
			appearance8.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridChartSeries.DisplayLayout.Override.ActiveCellAppearance = appearance8;
			appearance9.BackColor = System.Drawing.SystemColors.Highlight;
			appearance9.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridChartSeries.DisplayLayout.Override.ActiveRowAppearance = appearance9;
			dataGridChartSeries.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridChartSeries.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridChartSeries.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance10.BackColor = System.Drawing.SystemColors.Window;
			dataGridChartSeries.DisplayLayout.Override.CardAreaAppearance = appearance10;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			appearance11.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridChartSeries.DisplayLayout.Override.CellAppearance = appearance11;
			dataGridChartSeries.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridChartSeries.DisplayLayout.Override.CellPadding = 0;
			appearance12.BackColor = System.Drawing.SystemColors.Control;
			appearance12.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance12.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance12.BorderColor = System.Drawing.SystemColors.Window;
			dataGridChartSeries.DisplayLayout.Override.GroupByRowAppearance = appearance12;
			appearance13.TextHAlignAsString = "Left";
			dataGridChartSeries.DisplayLayout.Override.HeaderAppearance = appearance13;
			dataGridChartSeries.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridChartSeries.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance14.BackColor = System.Drawing.SystemColors.Window;
			appearance14.BorderColor = System.Drawing.Color.Silver;
			dataGridChartSeries.DisplayLayout.Override.RowAppearance = appearance14;
			dataGridChartSeries.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance15.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridChartSeries.DisplayLayout.Override.TemplateAddRowAppearance = appearance15;
			dataGridChartSeries.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridChartSeries.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridChartSeries.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridChartSeries.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridChartSeries.ExitEditModeOnLeave = false;
			dataGridChartSeries.IncludeLotItems = false;
			dataGridChartSeries.LoadLayoutFailed = false;
			dataGridChartSeries.Location = new System.Drawing.Point(14, 24);
			dataGridChartSeries.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridChartSeries.Name = "dataGridChartSeries";
			dataGridChartSeries.ShowClearMenu = true;
			dataGridChartSeries.ShowDeleteMenu = true;
			dataGridChartSeries.ShowInsertMenu = true;
			dataGridChartSeries.ShowMoveRowsMenu = true;
			dataGridChartSeries.Size = new System.Drawing.Size(678, 159);
			dataGridChartSeries.TabIndex = 0;
			dataGridChartSeries.Text = "dataEntryGrid1";
			tabPageNumber.Controls.Add(textBoxNumIndTextFormat);
			tabPageNumber.Controls.Add(mmLabel18);
			tabPageNumber.Controls.Add(textBoxNumValueFormat);
			tabPageNumber.Controls.Add(mmLabel17);
			tabPageNumber.Controls.Add(mmLabel16);
			tabPageNumber.Controls.Add(colorEditNumTextColor);
			tabPageNumber.Controls.Add(textBoxNumberValueColumn);
			tabPageNumber.Controls.Add(mmLabel15);
			tabPageNumber.Controls.Add(textBoxNumIndTextVal);
			tabPageNumber.Controls.Add(textBoxNumIndicatorValueColumn);
			tabPageNumber.Controls.Add(mmLabel14);
			tabPageNumber.Controls.Add(mmLabel13);
			tabPageNumber.Controls.Add(checkBoxNumShowIndText);
			tabPageNumber.Controls.Add(checkBoxNumShowIndicator);
			tabPageNumber.Location = new System.Drawing.Point(-10000, -10000);
			tabPageNumber.Name = "tabPageNumber";
			tabPageNumber.Size = new System.Drawing.Size(709, 404);
			textBoxNumIndTextFormat.BackColor = System.Drawing.Color.White;
			textBoxNumIndTextFormat.CustomReportFieldName = "";
			textBoxNumIndTextFormat.CustomReportKey = "";
			textBoxNumIndTextFormat.CustomReportValueType = 1;
			textBoxNumIndTextFormat.IsComboTextBox = false;
			textBoxNumIndTextFormat.IsModified = false;
			textBoxNumIndTextFormat.Location = new System.Drawing.Point(161, 133);
			textBoxNumIndTextFormat.MaxLength = 30;
			textBoxNumIndTextFormat.Name = "textBoxNumIndTextFormat";
			textBoxNumIndTextFormat.Size = new System.Drawing.Size(168, 20);
			textBoxNumIndTextFormat.TabIndex = 28;
			mmLabel18.AutoSize = true;
			mmLabel18.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel18.IsFieldHeader = false;
			mmLabel18.IsRequired = false;
			mmLabel18.Location = new System.Drawing.Point(13, 136);
			mmLabel18.Name = "mmLabel18";
			mmLabel18.PenWidth = 1f;
			mmLabel18.ShowBorder = false;
			mmLabel18.Size = new System.Drawing.Size(110, 13);
			mmLabel18.TabIndex = 29;
			mmLabel18.Text = "Indicator Text Format:";
			textBoxNumValueFormat.BackColor = System.Drawing.Color.White;
			textBoxNumValueFormat.CustomReportFieldName = "";
			textBoxNumValueFormat.CustomReportKey = "";
			textBoxNumValueFormat.CustomReportValueType = 1;
			textBoxNumValueFormat.IsComboTextBox = false;
			textBoxNumValueFormat.IsModified = false;
			textBoxNumValueFormat.Location = new System.Drawing.Point(161, 107);
			textBoxNumValueFormat.MaxLength = 30;
			textBoxNumValueFormat.Name = "textBoxNumValueFormat";
			textBoxNumValueFormat.Size = new System.Drawing.Size(168, 20);
			textBoxNumValueFormat.TabIndex = 26;
			mmLabel17.AutoSize = true;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(13, 110);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(72, 13);
			mmLabel17.TabIndex = 27;
			mmLabel17.Text = "Value Format:";
			mmLabel16.AutoSize = true;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(11, 242);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(58, 13);
			mmLabel16.TabIndex = 25;
			mmLabel16.Text = "Text Color:";
			colorEditNumTextColor.EditValue = System.Drawing.Color.Empty;
			colorEditNumTextColor.Location = new System.Drawing.Point(75, 239);
			colorEditNumTextColor.Name = "colorEditNumTextColor";
			colorEditNumTextColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			colorEditNumTextColor.Size = new System.Drawing.Size(158, 20);
			colorEditNumTextColor.TabIndex = 24;
			textBoxNumberValueColumn.BackColor = System.Drawing.Color.White;
			textBoxNumberValueColumn.CustomReportFieldName = "";
			textBoxNumberValueColumn.CustomReportKey = "";
			textBoxNumberValueColumn.CustomReportValueType = 1;
			textBoxNumberValueColumn.IsComboTextBox = false;
			textBoxNumberValueColumn.IsModified = false;
			textBoxNumberValueColumn.Location = new System.Drawing.Point(161, 14);
			textBoxNumberValueColumn.MaxLength = 30;
			textBoxNumberValueColumn.Name = "textBoxNumberValueColumn";
			textBoxNumberValueColumn.Size = new System.Drawing.Size(168, 20);
			textBoxNumberValueColumn.TabIndex = 22;
			mmLabel15.AutoSize = true;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = false;
			mmLabel15.Location = new System.Drawing.Point(13, 17);
			mmLabel15.Name = "mmLabel15";
			mmLabel15.PenWidth = 1f;
			mmLabel15.ShowBorder = false;
			mmLabel15.Size = new System.Drawing.Size(75, 13);
			mmLabel15.TabIndex = 23;
			mmLabel15.Text = "Value Column:";
			textBoxNumIndTextVal.BackColor = System.Drawing.Color.White;
			textBoxNumIndTextVal.CustomReportFieldName = "";
			textBoxNumIndTextVal.CustomReportKey = "";
			textBoxNumIndTextVal.CustomReportValueType = 1;
			textBoxNumIndTextVal.IsComboTextBox = false;
			textBoxNumIndTextVal.IsModified = false;
			textBoxNumIndTextVal.Location = new System.Drawing.Point(161, 66);
			textBoxNumIndTextVal.MaxLength = 30;
			textBoxNumIndTextVal.Name = "textBoxNumIndTextVal";
			textBoxNumIndTextVal.Size = new System.Drawing.Size(168, 20);
			textBoxNumIndTextVal.TabIndex = 20;
			textBoxNumIndicatorValueColumn.BackColor = System.Drawing.Color.White;
			textBoxNumIndicatorValueColumn.CustomReportFieldName = "";
			textBoxNumIndicatorValueColumn.CustomReportKey = "";
			textBoxNumIndicatorValueColumn.CustomReportValueType = 1;
			textBoxNumIndicatorValueColumn.IsComboTextBox = false;
			textBoxNumIndicatorValueColumn.IsModified = false;
			textBoxNumIndicatorValueColumn.Location = new System.Drawing.Point(161, 40);
			textBoxNumIndicatorValueColumn.MaxLength = 30;
			textBoxNumIndicatorValueColumn.Name = "textBoxNumIndicatorValueColumn";
			textBoxNumIndicatorValueColumn.Size = new System.Drawing.Size(168, 20);
			textBoxNumIndicatorValueColumn.TabIndex = 20;
			mmLabel14.AutoSize = true;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(13, 69);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(143, 13);
			mmLabel14.TabIndex = 21;
			mmLabel14.Text = "Indicator Text Value Column:";
			mmLabel13.AutoSize = true;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(13, 43);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(119, 13);
			mmLabel13.TabIndex = 21;
			mmLabel13.Text = "Indicator Value Column:";
			checkBoxNumShowIndText.AutoSize = true;
			checkBoxNumShowIndText.Location = new System.Drawing.Point(16, 217);
			checkBoxNumShowIndText.Name = "checkBoxNumShowIndText";
			checkBoxNumShowIndText.Size = new System.Drawing.Size(116, 17);
			checkBoxNumShowIndText.TabIndex = 6;
			checkBoxNumShowIndText.Text = "Show indicator text";
			checkBoxNumShowIndText.UseVisualStyleBackColor = true;
			checkBoxNumShowIndicator.AutoSize = true;
			checkBoxNumShowIndicator.Location = new System.Drawing.Point(16, 194);
			checkBoxNumShowIndicator.Name = "checkBoxNumShowIndicator";
			checkBoxNumShowIndicator.Size = new System.Drawing.Size(96, 17);
			checkBoxNumShowIndicator.TabIndex = 6;
			checkBoxNumShowIndicator.Text = "Show indicator";
			checkBoxNumShowIndicator.UseVisualStyleBackColor = true;
			tabPageGauge.Controls.Add(checkBoxGaugeRangeAsValue);
			tabPageGauge.Controls.Add(checkBoxGaugeShowLabel);
			tabPageGauge.Controls.Add(textBoxGaugeValueFormat);
			tabPageGauge.Controls.Add(mmLabel5);
			tabPageGauge.Controls.Add(checkBoxGShowNeedle);
			tabPageGauge.Controls.Add(checkBoxGShowGaugeText);
			tabPageGauge.Controls.Add(textBoxGaugeValueColumn);
			tabPageGauge.Controls.Add(mmLabel3);
			tabPageGauge.Controls.Add(mmLabel19);
			tabPageGauge.Controls.Add(dataGridGauge);
			tabPageGauge.Location = new System.Drawing.Point(-10000, -10000);
			tabPageGauge.Name = "tabPageGauge";
			tabPageGauge.Size = new System.Drawing.Size(709, 404);
			checkBoxGaugeRangeAsValue.AutoSize = true;
			checkBoxGaugeRangeAsValue.Location = new System.Drawing.Point(21, 328);
			checkBoxGaugeRangeAsValue.Name = "checkBoxGaugeRangeAsValue";
			checkBoxGaugeRangeAsValue.Size = new System.Drawing.Size(180, 17);
			checkBoxGaugeRangeAsValue.TabIndex = 6;
			checkBoxGaugeRangeAsValue.Text = "Use first range as value indicator";
			checkBoxGaugeRangeAsValue.UseVisualStyleBackColor = true;
			checkBoxGaugeShowLabel.AutoSize = true;
			checkBoxGaugeShowLabel.Location = new System.Drawing.Point(22, 305);
			checkBoxGaugeShowLabel.Name = "checkBoxGaugeShowLabel";
			checkBoxGaugeShowLabel.Size = new System.Drawing.Size(78, 17);
			checkBoxGaugeShowLabel.TabIndex = 5;
			checkBoxGaugeShowLabel.Text = "Show label";
			checkBoxGaugeShowLabel.UseVisualStyleBackColor = true;
			textBoxGaugeValueFormat.BackColor = System.Drawing.Color.White;
			textBoxGaugeValueFormat.CustomReportFieldName = "";
			textBoxGaugeValueFormat.CustomReportKey = "";
			textBoxGaugeValueFormat.CustomReportValueType = 1;
			textBoxGaugeValueFormat.IsComboTextBox = false;
			textBoxGaugeValueFormat.IsModified = false;
			textBoxGaugeValueFormat.Location = new System.Drawing.Point(393, 218);
			textBoxGaugeValueFormat.MaxLength = 30;
			textBoxGaugeValueFormat.Name = "textBoxGaugeValueFormat";
			textBoxGaugeValueFormat.Size = new System.Drawing.Size(168, 20);
			textBoxGaugeValueFormat.TabIndex = 2;
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(306, 220);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(72, 13);
			mmLabel5.TabIndex = 29;
			mmLabel5.Text = "Value Format:";
			checkBoxGShowNeedle.AutoSize = true;
			checkBoxGShowNeedle.Location = new System.Drawing.Point(22, 282);
			checkBoxGShowNeedle.Name = "checkBoxGShowNeedle";
			checkBoxGShowNeedle.Size = new System.Drawing.Size(88, 17);
			checkBoxGShowNeedle.TabIndex = 4;
			checkBoxGShowNeedle.Text = "Show needle";
			checkBoxGShowNeedle.UseVisualStyleBackColor = true;
			checkBoxGShowGaugeText.AutoSize = true;
			checkBoxGShowGaugeText.Location = new System.Drawing.Point(22, 259);
			checkBoxGShowGaugeText.Name = "checkBoxGShowGaugeText";
			checkBoxGShowGaugeText.Size = new System.Drawing.Size(106, 17);
			checkBoxGShowGaugeText.TabIndex = 3;
			checkBoxGShowGaugeText.Text = "Show gauge text";
			checkBoxGShowGaugeText.UseVisualStyleBackColor = true;
			textBoxGaugeValueColumn.BackColor = System.Drawing.Color.White;
			textBoxGaugeValueColumn.CustomReportFieldName = "";
			textBoxGaugeValueColumn.CustomReportKey = "";
			textBoxGaugeValueColumn.CustomReportValueType = 1;
			textBoxGaugeValueColumn.IsComboTextBox = false;
			textBoxGaugeValueColumn.IsModified = false;
			textBoxGaugeValueColumn.Location = new System.Drawing.Point(131, 218);
			textBoxGaugeValueColumn.MaxLength = 30;
			textBoxGaugeValueColumn.Name = "textBoxGaugeValueColumn";
			textBoxGaugeValueColumn.Size = new System.Drawing.Size(157, 20);
			textBoxGaugeValueColumn.TabIndex = 1;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(19, 220);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(75, 13);
			mmLabel3.TabIndex = 0;
			mmLabel3.Text = "Value Column:";
			mmLabel19.AutoSize = true;
			mmLabel19.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel19.IsFieldHeader = false;
			mmLabel19.IsRequired = false;
			mmLabel19.Location = new System.Drawing.Point(18, 10);
			mmLabel19.Name = "mmLabel19";
			mmLabel19.PenWidth = 1f;
			mmLabel19.ShowBorder = false;
			mmLabel19.Size = new System.Drawing.Size(103, 13);
			mmLabel19.TabIndex = 22;
			mmLabel19.Text = "Enter gauge ranges:";
			dataGridGauge.AllowAddNew = false;
			dataGridGauge.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance16.BackColor = System.Drawing.SystemColors.Window;
			appearance16.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridGauge.DisplayLayout.Appearance = appearance16;
			dataGridGauge.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridGauge.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance17.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance17.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance17.BorderColor = System.Drawing.SystemColors.Window;
			dataGridGauge.DisplayLayout.GroupByBox.Appearance = appearance17;
			appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridGauge.DisplayLayout.GroupByBox.BandLabelAppearance = appearance18;
			dataGridGauge.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance19.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance19.BackColor2 = System.Drawing.SystemColors.Control;
			appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridGauge.DisplayLayout.GroupByBox.PromptAppearance = appearance19;
			dataGridGauge.DisplayLayout.MaxColScrollRegions = 1;
			dataGridGauge.DisplayLayout.MaxRowScrollRegions = 1;
			appearance20.BackColor = System.Drawing.SystemColors.Window;
			appearance20.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridGauge.DisplayLayout.Override.ActiveCellAppearance = appearance20;
			appearance21.BackColor = System.Drawing.SystemColors.Highlight;
			appearance21.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridGauge.DisplayLayout.Override.ActiveRowAppearance = appearance21;
			dataGridGauge.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridGauge.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridGauge.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance22.BackColor = System.Drawing.SystemColors.Window;
			dataGridGauge.DisplayLayout.Override.CardAreaAppearance = appearance22;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			appearance23.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridGauge.DisplayLayout.Override.CellAppearance = appearance23;
			dataGridGauge.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridGauge.DisplayLayout.Override.CellPadding = 0;
			appearance24.BackColor = System.Drawing.SystemColors.Control;
			appearance24.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance24.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance24.BorderColor = System.Drawing.SystemColors.Window;
			dataGridGauge.DisplayLayout.Override.GroupByRowAppearance = appearance24;
			appearance25.TextHAlignAsString = "Left";
			dataGridGauge.DisplayLayout.Override.HeaderAppearance = appearance25;
			dataGridGauge.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridGauge.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance26.BackColor = System.Drawing.SystemColors.Window;
			appearance26.BorderColor = System.Drawing.Color.Silver;
			dataGridGauge.DisplayLayout.Override.RowAppearance = appearance26;
			dataGridGauge.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance27.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridGauge.DisplayLayout.Override.TemplateAddRowAppearance = appearance27;
			dataGridGauge.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridGauge.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridGauge.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridGauge.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridGauge.ExitEditModeOnLeave = false;
			dataGridGauge.IncludeLotItems = false;
			dataGridGauge.LoadLayoutFailed = false;
			dataGridGauge.Location = new System.Drawing.Point(17, 26);
			dataGridGauge.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridGauge.Name = "dataGridGauge";
			dataGridGauge.ShowClearMenu = true;
			dataGridGauge.ShowDeleteMenu = true;
			dataGridGauge.ShowInsertMenu = true;
			dataGridGauge.ShowMoveRowsMenu = true;
			dataGridGauge.Size = new System.Drawing.Size(678, 171);
			dataGridGauge.TabIndex = 21;
			dataGridGauge.Text = "dataEntryGrid1";
			tabPageList.Controls.Add(mmLabel20);
			tabPageList.Controls.Add(dataGridHiddenColumns);
			tabPageList.Location = new System.Drawing.Point(-10000, -10000);
			tabPageList.Name = "tabPageList";
			tabPageList.Size = new System.Drawing.Size(709, 404);
			mmLabel20.AutoSize = true;
			mmLabel20.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel20.IsFieldHeader = false;
			mmLabel20.IsRequired = false;
			mmLabel20.Location = new System.Drawing.Point(4, 6);
			mmLabel20.Name = "mmLabel20";
			mmLabel20.PenWidth = 1f;
			mmLabel20.ShowBorder = false;
			mmLabel20.Size = new System.Drawing.Size(115, 13);
			mmLabel20.TabIndex = 24;
			mmLabel20.Text = "Enter Hidden Columns:";
			dataGridHiddenColumns.AllowAddNew = false;
			dataGridHiddenColumns.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance28.BackColor = System.Drawing.SystemColors.Window;
			appearance28.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridHiddenColumns.DisplayLayout.Appearance = appearance28;
			dataGridHiddenColumns.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridHiddenColumns.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance29.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance29.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance29.BorderColor = System.Drawing.SystemColors.Window;
			dataGridHiddenColumns.DisplayLayout.GroupByBox.Appearance = appearance29;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridHiddenColumns.DisplayLayout.GroupByBox.BandLabelAppearance = appearance30;
			dataGridHiddenColumns.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance31.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance31.BackColor2 = System.Drawing.SystemColors.Control;
			appearance31.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridHiddenColumns.DisplayLayout.GroupByBox.PromptAppearance = appearance31;
			dataGridHiddenColumns.DisplayLayout.MaxColScrollRegions = 1;
			dataGridHiddenColumns.DisplayLayout.MaxRowScrollRegions = 1;
			appearance32.BackColor = System.Drawing.SystemColors.Window;
			appearance32.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridHiddenColumns.DisplayLayout.Override.ActiveCellAppearance = appearance32;
			appearance33.BackColor = System.Drawing.SystemColors.Highlight;
			appearance33.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridHiddenColumns.DisplayLayout.Override.ActiveRowAppearance = appearance33;
			dataGridHiddenColumns.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridHiddenColumns.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridHiddenColumns.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance34.BackColor = System.Drawing.SystemColors.Window;
			dataGridHiddenColumns.DisplayLayout.Override.CardAreaAppearance = appearance34;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			appearance35.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridHiddenColumns.DisplayLayout.Override.CellAppearance = appearance35;
			dataGridHiddenColumns.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridHiddenColumns.DisplayLayout.Override.CellPadding = 0;
			appearance36.BackColor = System.Drawing.SystemColors.Control;
			appearance36.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance36.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance36.BorderColor = System.Drawing.SystemColors.Window;
			dataGridHiddenColumns.DisplayLayout.Override.GroupByRowAppearance = appearance36;
			appearance37.TextHAlignAsString = "Left";
			dataGridHiddenColumns.DisplayLayout.Override.HeaderAppearance = appearance37;
			dataGridHiddenColumns.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridHiddenColumns.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance38.BackColor = System.Drawing.SystemColors.Window;
			appearance38.BorderColor = System.Drawing.Color.Silver;
			dataGridHiddenColumns.DisplayLayout.Override.RowAppearance = appearance38;
			dataGridHiddenColumns.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance39.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridHiddenColumns.DisplayLayout.Override.TemplateAddRowAppearance = appearance39;
			dataGridHiddenColumns.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridHiddenColumns.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridHiddenColumns.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridHiddenColumns.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridHiddenColumns.ExitEditModeOnLeave = false;
			dataGridHiddenColumns.IncludeLotItems = false;
			dataGridHiddenColumns.LoadLayoutFailed = false;
			dataGridHiddenColumns.Location = new System.Drawing.Point(3, 22);
			dataGridHiddenColumns.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridHiddenColumns.Name = "dataGridHiddenColumns";
			dataGridHiddenColumns.ShowClearMenu = true;
			dataGridHiddenColumns.ShowDeleteMenu = true;
			dataGridHiddenColumns.ShowInsertMenu = true;
			dataGridHiddenColumns.ShowMoveRowsMenu = true;
			dataGridHiddenColumns.Size = new System.Drawing.Size(678, 171);
			dataGridHiddenColumns.TabIndex = 23;
			dataGridHiddenColumns.Text = "dataEntryGrid1";
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
			tabPageDetails.Size = new System.Drawing.Size(709, 404);
			buttonDeleteParameter.Location = new System.Drawing.Point(614, 85);
			buttonDeleteParameter.Name = "buttonDeleteParameter";
			buttonDeleteParameter.Size = new System.Drawing.Size(75, 23);
			buttonDeleteParameter.TabIndex = 12;
			buttonDeleteParameter.Text = "Delete";
			buttonDeleteParameter.UseVisualStyleBackColor = true;
			buttonDeleteParameter.Click += new System.EventHandler(buttonDeleteParameter_Click);
			buttonEditParameter.Location = new System.Drawing.Point(614, 56);
			buttonEditParameter.Name = "buttonEditParameter";
			buttonEditParameter.Size = new System.Drawing.Size(75, 23);
			buttonEditParameter.TabIndex = 11;
			buttonEditParameter.Text = "Edit";
			buttonEditParameter.UseVisualStyleBackColor = true;
			buttonEditParameter.Click += new System.EventHandler(buttonEditParameter_Click);
			buttonAddParameter.Location = new System.Drawing.Point(614, 27);
			buttonAddParameter.Name = "buttonAddParameter";
			buttonAddParameter.Size = new System.Drawing.Size(75, 23);
			buttonAddParameter.TabIndex = 10;
			buttonAddParameter.Text = "Add";
			buttonAddParameter.UseVisualStyleBackColor = true;
			buttonAddParameter.Click += new System.EventHandler(buttonAddParameter_Click);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(387, 11);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(63, 13);
			label3.TabIndex = 9;
			label3.Text = "Parameters:";
			listBoxParameters.FormattingEnabled = true;
			listBoxParameters.Location = new System.Drawing.Point(390, 27);
			listBoxParameters.Name = "listBoxParameters";
			listBoxParameters.Size = new System.Drawing.Size(218, 160);
			listBoxParameters.TabIndex = 8;
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
			ultraTabPageControl1.Controls.Add(label7);
			ultraTabPageControl1.Controls.Add(comboBoxDrillDown);
			ultraTabPageControl1.Controls.Add(panelActionTransaction);
			ultraTabPageControl1.Controls.Add(panelActionCard);
			ultraTabPageControl1.Controls.Add(panelActionSmartList);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(709, 404);
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(17, 14);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(91, 13);
			label7.TabIndex = 8;
			label7.Text = "Drill Down Action:";
			comboBoxDrillDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxDrillDown.FormattingEnabled = true;
			comboBoxDrillDown.Items.AddRange(new object[4]
			{
				"None",
				"Open Card",
				"Open Transaction",
				"Open Smart List"
			});
			comboBoxDrillDown.Location = new System.Drawing.Point(122, 11);
			comboBoxDrillDown.Name = "comboBoxDrillDown";
			comboBoxDrillDown.Size = new System.Drawing.Size(137, 21);
			comboBoxDrillDown.TabIndex = 7;
			panelActionTransaction.Controls.Add(checkBoxTransactionPreview);
			panelActionTransaction.Controls.Add(textBoxActionTRVoucher);
			panelActionTransaction.Controls.Add(label8);
			panelActionTransaction.Controls.Add(textBoxActionTRSysDocID);
			panelActionTransaction.Controls.Add(label9);
			panelActionTransaction.Location = new System.Drawing.Point(11, 38);
			panelActionTransaction.Name = "panelActionTransaction";
			panelActionTransaction.Size = new System.Drawing.Size(356, 106);
			panelActionTransaction.TabIndex = 9;
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
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(6, 37);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(92, 13);
			label8.TabIndex = 6;
			label8.Text = "Voucher No Field:";
			textBoxActionTRSysDocID.Location = new System.Drawing.Point(111, 11);
			textBoxActionTRSysDocID.Name = "textBoxActionTRSysDocID";
			textBoxActionTRSysDocID.Size = new System.Drawing.Size(137, 20);
			textBoxActionTRSysDocID.TabIndex = 5;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(6, 14);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(75, 13);
			label9.TabIndex = 4;
			label9.Text = "Sys Doc Field:";
			panelActionCard.Controls.Add(comboBoxCardTypes);
			panelActionCard.Controls.Add(label10);
			panelActionCard.Controls.Add(textBoxDrilCardID);
			panelActionCard.Controls.Add(label11);
			panelActionCard.Location = new System.Drawing.Point(11, 38);
			panelActionCard.Name = "panelActionCard";
			panelActionCard.Size = new System.Drawing.Size(356, 106);
			panelActionCard.TabIndex = 10;
			panelActionCard.Visible = false;
			comboBoxCardTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxCardTypes.FormattingEnabled = true;
			comboBoxCardTypes.Location = new System.Drawing.Point(111, 35);
			comboBoxCardTypes.Name = "comboBoxCardTypes";
			comboBoxCardTypes.Size = new System.Drawing.Size(137, 21);
			comboBoxCardTypes.TabIndex = 7;
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(6, 37);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(59, 13);
			label10.TabIndex = 6;
			label10.Text = "Card Type:";
			textBoxDrilCardID.Location = new System.Drawing.Point(111, 11);
			textBoxDrilCardID.Name = "textBoxDrilCardID";
			textBoxDrilCardID.Size = new System.Drawing.Size(137, 20);
			textBoxDrilCardID.TabIndex = 5;
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(6, 14);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(85, 13);
			label11.TabIndex = 4;
			label11.Text = "Card Code Field:";
			panelActionSmartList.Controls.Add(ultraFormattedLinkLabel1);
			panelActionSmartList.Controls.Add(comboBoxSubReport);
			panelActionSmartList.Controls.Add(label13);
			panelActionSmartList.Controls.Add(textBoxParm4);
			panelActionSmartList.Controls.Add(textBoxParm3);
			panelActionSmartList.Controls.Add(label12);
			panelActionSmartList.Controls.Add(label14);
			panelActionSmartList.Controls.Add(textBoxParm2);
			panelActionSmartList.Controls.Add(label15);
			panelActionSmartList.Controls.Add(textBoxParm1);
			panelActionSmartList.Controls.Add(label16);
			panelActionSmartList.Location = new System.Drawing.Point(11, 39);
			panelActionSmartList.Name = "panelActionSmartList";
			panelActionSmartList.Size = new System.Drawing.Size(356, 211);
			panelActionSmartList.TabIndex = 11;
			panelActionSmartList.Visible = false;
			appearance40.FontData.BoldAsString = "False";
			appearance40.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance40;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(9, 5);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(88, 15);
			ultraFormattedLinkLabel1.TabIndex = 135;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Detail Smart List:";
			appearance41.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance41;
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
			label12.AutoSize = true;
			label12.Location = new System.Drawing.Point(6, 139);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(48, 13);
			label12.TabIndex = 9;
			label12.Text = "@Parm4";
			label14.AutoSize = true;
			label14.Location = new System.Drawing.Point(6, 116);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(51, 13);
			label14.TabIndex = 8;
			label14.Text = "@Parm3:";
			textBoxParm2.Location = new System.Drawing.Point(111, 89);
			textBoxParm2.Name = "textBoxParm2";
			textBoxParm2.Size = new System.Drawing.Size(137, 20);
			textBoxParm2.TabIndex = 2;
			label15.AutoSize = true;
			label15.Location = new System.Drawing.Point(6, 92);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(48, 13);
			label15.TabIndex = 6;
			label15.Text = "@Parm2";
			textBoxParm1.Location = new System.Drawing.Point(111, 66);
			textBoxParm1.Name = "textBoxParm1";
			textBoxParm1.Size = new System.Drawing.Size(137, 20);
			textBoxParm1.TabIndex = 1;
			label16.AutoSize = true;
			label16.Location = new System.Drawing.Point(6, 69);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(51, 13);
			label16.TabIndex = 4;
			label16.Text = "@Parm1:";
			tabPageContacts.Controls.Add(dataGridControls);
			tabPageContacts.Location = new System.Drawing.Point(-10000, -10000);
			tabPageContacts.Name = "tabPageContacts";
			tabPageContacts.Size = new System.Drawing.Size(709, 404);
			dataGridControls.AllowAddNew = false;
			dataGridControls.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance42.BackColor = System.Drawing.SystemColors.Window;
			appearance42.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridControls.DisplayLayout.Appearance = appearance42;
			dataGridControls.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridControls.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance43.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance43.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance43.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance43.BorderColor = System.Drawing.SystemColors.Window;
			dataGridControls.DisplayLayout.GroupByBox.Appearance = appearance43;
			appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridControls.DisplayLayout.GroupByBox.BandLabelAppearance = appearance44;
			dataGridControls.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance45.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance45.BackColor2 = System.Drawing.SystemColors.Control;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridControls.DisplayLayout.GroupByBox.PromptAppearance = appearance45;
			dataGridControls.DisplayLayout.MaxColScrollRegions = 1;
			dataGridControls.DisplayLayout.MaxRowScrollRegions = 1;
			appearance46.BackColor = System.Drawing.SystemColors.Window;
			appearance46.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridControls.DisplayLayout.Override.ActiveCellAppearance = appearance46;
			appearance47.BackColor = System.Drawing.SystemColors.Highlight;
			appearance47.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridControls.DisplayLayout.Override.ActiveRowAppearance = appearance47;
			dataGridControls.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridControls.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridControls.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance48.BackColor = System.Drawing.SystemColors.Window;
			dataGridControls.DisplayLayout.Override.CardAreaAppearance = appearance48;
			appearance49.BorderColor = System.Drawing.Color.Silver;
			appearance49.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridControls.DisplayLayout.Override.CellAppearance = appearance49;
			dataGridControls.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridControls.DisplayLayout.Override.CellPadding = 0;
			appearance50.BackColor = System.Drawing.SystemColors.Control;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			dataGridControls.DisplayLayout.Override.GroupByRowAppearance = appearance50;
			appearance51.TextHAlignAsString = "Left";
			dataGridControls.DisplayLayout.Override.HeaderAppearance = appearance51;
			dataGridControls.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridControls.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance52.BackColor = System.Drawing.SystemColors.Window;
			appearance52.BorderColor = System.Drawing.Color.Silver;
			dataGridControls.DisplayLayout.Override.RowAppearance = appearance52;
			dataGridControls.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance53.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridControls.DisplayLayout.Override.TemplateAddRowAppearance = appearance53;
			dataGridControls.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridControls.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridControls.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridControls.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridControls.ExitEditModeOnLeave = false;
			dataGridControls.IncludeLotItems = false;
			dataGridControls.LoadLayoutFailed = false;
			dataGridControls.Location = new System.Drawing.Point(8, 21);
			dataGridControls.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridControls.Name = "dataGridControls";
			dataGridControls.ShowClearMenu = true;
			dataGridControls.ShowDeleteMenu = true;
			dataGridControls.ShowInsertMenu = true;
			dataGridControls.ShowMoveRowsMenu = true;
			dataGridControls.Size = new System.Drawing.Size(694, 368);
			dataGridControls.TabIndex = 2;
			dataGridControls.Text = "dataEntryGrid1";
			dataSetData.DataSetName = "Report";
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Controls.Add(tabPageContacts);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(tabPageChart);
			ultraTabControl1.Controls.Add(tabPageNumber);
			ultraTabControl1.Controls.Add(tabPageGauge);
			ultraTabControl1.Controls.Add(tabPageList);
			ultraTabControl1.Location = new System.Drawing.Point(12, 37);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(713, 427);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 1;
			appearance54.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance54;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&General";
			ultraTab2.TabPage = tabPageChart;
			ultraTab2.Text = "Chart";
			ultraTab3.TabPage = tabPageNumber;
			ultraTab3.Text = "Number";
			ultraTab4.TabPage = tabPageGauge;
			ultraTab4.Text = "Gauge";
			ultraTab5.TabPage = tabPageList;
			ultraTab5.Text = "List";
			ultraTab6.TabPage = tabPageDetails;
			ultraTab6.Text = "&Data";
			ultraTab7.TabPage = ultraTabPageControl1;
			ultraTab7.Text = "Drill Down";
			ultraTab8.TabPage = tabPageContacts;
			ultraTab8.Text = "Controls";
			ultraTab8.Visible = false;
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[8]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4,
				ultraTab5,
				ultraTab6,
				ultraTab7,
				ultraTab8
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(709, 404);
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[15]
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
				toolStripButtonImport,
				toolStripButtonDuplicate,
				toolStripButtonShowPicture
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(737, 31);
			toolStrip1.TabIndex = 2;
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
			toolStripButtonDuplicate.Image = Micromind.ClientUI.Properties.Resources.duplicate1;
			toolStripButtonDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDuplicate.Name = "toolStripButtonDuplicate";
			toolStripButtonDuplicate.Size = new System.Drawing.Size(85, 28);
			toolStripButtonDuplicate.Text = "Duplicate";
			toolStripButtonDuplicate.Click += new System.EventHandler(toolStripButtonDuplicate_Click);
			toolStripButtonShowPicture.CheckOnClick = true;
			toolStripButtonShowPicture.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonShowPicture.Image");
			toolStripButtonShowPicture.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonShowPicture.Name = "toolStripButtonShowPicture";
			toolStripButtonShowPicture.Size = new System.Drawing.Size(104, 28);
			toolStripButtonShowPicture.Text = "Show Picture";
			toolStripButtonShowPicture.ToolTipText = "Auto load pictures";
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 476);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(737, 40);
			panelButtons.TabIndex = 18;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(737, 1);
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
			xpButton1.Location = new System.Drawing.Point(627, 8);
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
			openFileDialog1.FileName = "openFileDialog1";
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "BarSideBySide2DControl.Icon.png");
			imageList1.Images.SetKeyName(1, "BarStacked2DControl.Icon.png");
			imageList1.Images.SetKeyName(2, "BarFullStacked2DControl.Icon.png");
			imageList1.Images.SetKeyName(3, "BarSideBySideStacked2DControl.Icon.png");
			imageList1.Images.SetKeyName(4, "BarSideBySideFullStacked2DControl.Icon.png");
			imageList1.Images.SetKeyName(5, "Pie2DControl.Icon.png");
			imageList1.Images.SetKeyName(6, "next.png");
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 31);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 17;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(737, 516);
			base.Controls.Add(formManager);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "CustomGadgetDetailForm";
			Text = "Custom Gadget Designer";
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownRefreshInterval).EndInit();
			tabPageChart.ResumeLayout(false);
			tabPageChart.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownStartColor).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownNCount).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridChartSeries).EndInit();
			tabPageNumber.ResumeLayout(false);
			tabPageNumber.PerformLayout();
			((System.ComponentModel.ISupportInitialize)colorEditNumTextColor.Properties).EndInit();
			tabPageGauge.ResumeLayout(false);
			tabPageGauge.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridGauge).EndInit();
			tabPageList.ResumeLayout(false);
			tabPageList.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridHiddenColumns).EndInit();
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
			tabPageContacts.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridControls).EndInit();
			((System.ComponentModel.ISupportInitialize)dataSetData).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
