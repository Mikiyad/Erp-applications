using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinCalcManager;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Projects
{
	public class JobEstimationDetailsForm : Form, IForm
	{
		private bool supressInventoryMessage;

		private JobEstimationData currentData;

		private const string TABLENAME_CONST = "Job_Estimation";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private const string REVISIONNO_COST = "RevisionNo";

		private ItemSourceTypes sourceDocType;

		private string sourceSysDocID = "";

		private string sourceVoucherID = "";

		private int sourceSysDocType;

		private Dictionary<string, int> SelectedColumns = new Dictionary<string, int>();

		private int VisibleColumnCount;

		private bool chk1 = true;

		private bool chk2 = true;

		private bool chk3 = true;

		private bool chk4 = true;

		private bool chk5 = true;

		private bool chk6 = true;

		private bool rowval;

		private int parentrowindx;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

		private bool isVoid;

		private bool isloadrev;

		private bool isRevise;

		private decimal mohp;

		private decimal lohp;

		private decimal oohp;

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

		private ToolStripButton toolStripButtonFind;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private DateTimePicker dateTimePickerDate;

		private TextBox textBoxVoucherNumber;

		private MMLabel mmLabel1;

		private TextBox textBoxNote;

		private Label label3;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private Label labelVoided;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private ProductComboBox comboBoxGridItem;

		private LocationComboBox comboBoxLocation;

		private ProductUnitComboBox comboBoxGridProductUnit;

		private UltraCalcManager ultraCalcManager1;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator3;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem printListToolStripMenuItem;

		private UltraGridPrintDocument ultraGridPrintDocument1;

		private LocationComboBox comboBoxGridLocation;

		private CostCategoryComboBox comboBoxCostCategory;

		private JobComboBox comboBoxJob;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripButton toolStripButtonInformation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private TextBox textBoxJob;

		private JobFeeComboBox ComboBoxFee;

		private JobBOMComboBox ComboBoxJobBOM;

		private SysDocComboBox comboBoxSysDoc;

		private UltraTabControl ultraTabControl2;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage2;

		private UltraTabPageControl tabPageExpense;

		private UltraTabPageControl ultraTabPageControl3;

		private DataEntryGrid dataGridItems;

		private Button buttonApply;

		private MMTextBox textBoxC6;

		private MMTextBox textBoxC5;

		private MMTextBox textBoxC4;

		private MMTextBox textBoxC3;

		private Label LabelC6;

		private Label LabelC5;

		private Label LabelC4;

		private Label LabelC3;

		private MMTextBox textBoxC2;

		private Label LabelC2;

		private Label LabelC1;

		private MMTextBox textBoxC1;

		private PercentTextBox textBoxOtherOHP;

		private PercentTextBox textBoxLabourOHP;

		private PercentTextBox textBoxMaterialOHP;

		private Label label4;

		private Label label2;

		private Label label1;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem copyToolStripMenuItem;

		private TextBox textBoxCostCategory;

		private CostCategoryComboBox ComboBoxHeaderCostCategory;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private Label label5;

		private TextBox textBoxRef1;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripMenuItem reviseToolStripMenuItem;

		private NonDirtyPanel panelReviseDate;

		private MMTextBox textBoxLastReviseDate;

		private Label label11;

		private Label label6;

		private ComboBox comboBoxRevisionNo;

		public ScreenAreas ScreenArea => ScreenAreas.Products;

		public int ScreenID => 4002;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		private string SystemDocID => comboBoxSysDoc.SelectedID;

		private bool IsDirty
		{
			get
			{
				if (IsVoid)
				{
					return false;
				}
				return formManager.GetDirtyStatus();
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
				bool enabled;
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					XPButton xPButton = buttonDelete;
					enabled = (buttonVoid.Enabled = false);
					xPButton.Enabled = enabled;
					textBoxVoucherNumber.ReadOnly = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					enabled = (buttonVoid.Enabled = true);
					xPButton2.Enabled = enabled;
					textBoxVoucherNumber.ReadOnly = true;
				}
				ToolStripButton toolStripButton = toolStripButtonPrint;
				enabled = (toolStripButtonPreview.Enabled = !isNewRecord);
				toolStripButton.Enabled = enabled;
				toolStripButtonAttach.Enabled = !value;
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

		private bool IsVoid
		{
			get
			{
				return isVoid;
			}
			set
			{
				if (isVoid != value)
				{
					isVoid = value;
					panelDetails.Enabled = !value;
					dataGridItems.Enabled = !value;
					buttonSave.Enabled = !value;
					labelVoided.Visible = value;
					if (value)
					{
						buttonVoid.Text = UIMessages.Unvoid;
					}
					else
					{
						buttonVoid.Text = UIMessages.Void;
					}
				}
			}
		}

		public int SourceSysDocType
		{
			get
			{
				return sourceSysDocType;
			}
			set
			{
				sourceSysDocType = value;
			}
		}

		public string SourceSysDocID
		{
			get
			{
				return sourceSysDocID;
			}
			set
			{
				sourceSysDocID = value;
			}
		}

		public string SourceVoucherID
		{
			get
			{
				return sourceVoucherID;
			}
			set
			{
				sourceVoucherID = value;
			}
		}

		public bool IsRevised
		{
			get
			{
				return isRevise;
			}
			set
			{
				isRevise = value;
				panelReviseDate.Visible = isRevise;
				Text = (isRevise ? "Revise Estimation" : "Estimation");
			}
		}

		public decimal ConstMOHP
		{
			get
			{
				return mohp;
			}
			set
			{
				mohp = value;
			}
		}

		public decimal ConstLOHP
		{
			get
			{
				return lohp;
			}
			set
			{
				lohp = value;
			}
		}

		public decimal ConstOOHP
		{
			get
			{
				return oohp;
			}
			set
			{
				oohp = value;
			}
		}

		public JobEstimationDetailsForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.AllowCustomizeHeaders = true;
			checked
			{
				int num;
				for (num = 0; num < contextMenuStrip1.Items.Count; num++)
				{
					dataGridItems.DropDownMenu.Items.Add(contextMenuStrip1.Items[num]);
					num--;
				}
			}
		}

		private void AddEvents()
		{
			base.Load += JournalLeavesForm_Load;
			dataGridItems.CellDataError += dataGrid_CellDataError;
			dataGridItems.AfterRowActivate += dataGridItems_AfterRowActivate;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			dataGridItems.BeforeCellActivate += dataGridItems_BeforeCellActivate;
			dataGridItems.CellChange += dataGridItems_CellChange;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.AfterRowsDeleted += dataGridItems_AfterRowsDeleted;
			comboBoxGridItem.SelectedIndexChanged += comboBoxGridItem_SelectedIndexChanged;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			comboBoxGridProductUnit.SelectedIndexChanged += comboBoxGridProductUnit_SelectedIndexChanged;
			comboBoxLocation.SelectedIndexChanged += comboBoxLocation_SelectedIndexChanged;
			dateTimePickerDate.ValueChanged += dateTimePickerDate_ValueChanged;
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
			base.FormClosing += ItemGroupDetailsForm_FormClosing;
			dataGridItems.HeaderClicked += dataGridItems_HeaderClicked;
		}

		private void dataGridItems_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Quantity")
			{
				decimal result = -1m;
				decimal.TryParse(dataGridItems.ActiveCell.Text.ToString(), out result);
				if (result < 0m)
				{
					ErrorHelper.InformationMessage("Negative quantity is not allowed.", "Please enter a number greater than or equal to zero.");
					e.Cancel = true;
				}
			}
		}

		private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxGridProductUnit_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxGridItem_VisibleChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
			formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow == null || (!IsNewRecord && IsNewRecord))
			{
				return;
			}
			if (dataGridItems.ActiveRow.Index == 0 && dataGridItems.ActiveRow.Band.Index == 0)
			{
				activeRow.Cells["CostCategoryID"].Value = (ComboBoxHeaderCostCategory.SelectedID ?? null);
			}
			checked
			{
				if (dataGridItems.ActiveRow.Index > 0 && dataGridItems.ActiveRow.Band.Index == 0)
				{
					activeRow.Cells["CostCategoryID"].Value = (dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["CostCategoryID"].Value ?? null);
				}
				if (dataGridItems.ActiveRow.Index > 0 && dataGridItems.ActiveRow.Band.Index == 0)
				{
					activeRow.Cells["FeeID"].Value = (dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["FeeID"].Value ?? null);
				}
				if (dataGridItems.ActiveRow.Index <= 0 || dataGridItems.ActiveRow.Band.Index != 1)
				{
					return;
				}
				UltraGridRow activeRow2 = dataGridItems.ActiveRow;
				double result = 0.0;
				double num = 1.0;
				if (activeRow2.ParentRow != null)
				{
					foreach (UltraGridRow row in activeRow2.ParentRow.ChildBands[0].Rows)
					{
						double.TryParse(row.Cells["Hiddenboqquantity"].Value.ToString(), out result);
						if (result != 0.0)
						{
							break;
						}
					}
					if (result != 0.0 && num == 1.0)
					{
						activeRow.Cells["Hiddenboqquantity"].Value = result;
						activeRow.Cells["Quantity"].Value = num * result;
						textBoxNote.Text += "   ";
					}
				}
			}
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			checked
			{
				try
				{
					if (dataGridItems.ActiveRow != null)
					{
						if (e.Cell.Column.Key == "BOQ" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "BOQ")
						{
							dataGridItems.ActiveRow.Cells["Remarks"].Value = ComboBoxJobBOM.SelectedName;
							dataGridItems.ActiveRow.Cells["C1"].Value = 0;
							dataGridItems.ActiveRow.Cells["C2"].Value = 0;
							dataGridItems.ActiveRow.Cells["C3"].Value = 0;
							dataGridItems.ActiveRow.Cells["C4"].Value = 0;
							dataGridItems.ActiveRow.Cells["C5"].Value = 0;
							dataGridItems.ActiveRow.Cells["C6"].Value = 0;
							DataTable dataTable = (dataGridItems.DataSource as DataSet).Tables[0];
							decimal result = default(decimal);
							decimal result2 = default(decimal);
							decimal result3 = default(decimal);
							if (dataTable.Rows.Count > 0)
							{
								decimal.TryParse(dataTable.Rows[0]["MOHP"].ToString(), out result);
								dataGridItems.ActiveRow.Cells["MOHP"].Value = result;
								decimal.TryParse(dataTable.Rows[0]["LOHP"].ToString(), out result2);
								dataGridItems.ActiveRow.Cells["LOHP"].Value = result2;
								decimal.TryParse(dataTable.Rows[0]["OOHP"].ToString(), out result3);
								dataGridItems.ActiveRow.Cells["OOHP"].Value = result3;
							}
						}
						if (e.Cell.Column.Key == "Item Code" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Item Code")
						{
							dataGridItems.ActiveRow.Cells["Description"].Value = comboBoxGridItem.SelectedName;
							if (comboBoxGridItem.SelectedID != "")
							{
								decimal jobBOMProductPurchasePrice = Factory.ProductSystem.GetJobBOMProductPurchasePrice(comboBoxGridItem.SelectedID);
								dataGridItems.ActiveRow.Cells["HiddenCost"].Value = jobBOMProductPurchasePrice;
								dataGridItems.ActiveRow.Cells["ActualCost"].Value = jobBOMProductPurchasePrice;
								dataGridItems.ActiveRow.Cells["HiddenQuantity"].Value = 1;
								dataGridItems.ActiveRow.Cells["HiddenLabourCost"].Value = Factory.ProductSystem.GetJobBOMLabourCost(comboBoxGridItem.SelectedID);
								dataGridItems.ActiveRow.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(comboBoxGridItem.SelectedID);
								dataGridItems.ActiveRow.Cells["Unit"].Value = comboBoxGridItem.SelectedUnitID;
							}
						}
						if ((e.Cell.Column.Key == "CMarkup" || e.Cell.Column.Key == "OOHP" || e.Cell.Column.Key == "MOHP" || e.Cell.Column.Key == "LOHP" || e.Cell.Column.Key == "C1" || e.Cell.Column.Key == "C2" || e.Cell.Column.Key == "C3" || e.Cell.Column.Key == "C4" || e.Cell.Column.Key == "C5" || e.Cell.Column.Key == "C6" || e.Cell.Column.Key == "BOQ Quantity") && dataGridItems.ActiveRow.Cells["IsFilled"].Value.ToString() == "T" && !rowval && dataGridItems.ActiveRow.Band.Index == 0)
						{
							ReCalculatEstimate(allRows: true);
						}
						if ((e.Cell.Column.Key == "Quantity" || ((e.Cell.Column.Key == "HiddenQuantity" || e.Cell.Column.Key == "HiddenCost" || e.Cell.Column.Key == "HiddenLabourCost") && !rowval)) && dataGridItems.ActiveRow.Band.Index == 1)
						{
							ReCalculateRow(e.Cell.Column.Key);
						}
						_ = dataGridItems.DataSource;
						if (!dataGridItems.DisplayLayout.Bands[0].Columns["C1"].Hidden && !SelectedColumns.ContainsKey("C1"))
						{
							SelectedColumns.Add("C1", 1);
							VisibleColumnCount++;
						}
						if (!dataGridItems.DisplayLayout.Bands[0].Columns["C2"].Hidden && !SelectedColumns.ContainsKey("C2"))
						{
							SelectedColumns.Add("C2", 1);
							VisibleColumnCount++;
						}
						if (!dataGridItems.DisplayLayout.Bands[0].Columns["C3"].Hidden && !SelectedColumns.ContainsKey("C3"))
						{
							SelectedColumns.Add("C3", 1);
							VisibleColumnCount++;
						}
						if (!dataGridItems.DisplayLayout.Bands[0].Columns["C4"].Hidden && !SelectedColumns.ContainsKey("C4"))
						{
							SelectedColumns.Add("C4", 1);
							VisibleColumnCount++;
						}
						if (!dataGridItems.DisplayLayout.Bands[0].Columns["C5"].Hidden && !SelectedColumns.ContainsKey("C5"))
						{
							SelectedColumns.Add("C5", 1);
							VisibleColumnCount++;
						}
						if (!dataGridItems.DisplayLayout.Bands[0].Columns["C6"].Hidden && !SelectedColumns.ContainsKey("C6"))
						{
							SelectedColumns.Add("C6", 1);
							VisibleColumnCount++;
						}
						string key = e.Cell.Column.Key;
						decimal result4 = default(decimal);
						decimal result5 = default(decimal);
						decimal result6 = default(decimal);
						decimal result7 = default(decimal);
						decimal result8 = default(decimal);
						decimal result9 = default(decimal);
						decimal num = default(decimal);
						if (SelectedColumns.ContainsKey(key))
						{
							SelectedColumns[key] += 1;
							decimal.TryParse(dataGridItems.ActiveRow.Cells["C1"].Value.ToString(), out result4);
							decimal.TryParse(dataGridItems.ActiveRow.Cells["C2"].Value.ToString(), out result5);
							decimal.TryParse(dataGridItems.ActiveRow.Cells["C3"].Value.ToString(), out result6);
							decimal.TryParse(dataGridItems.ActiveRow.Cells["C4"].Value.ToString(), out result7);
							decimal.TryParse(dataGridItems.ActiveRow.Cells["C5"].Value.ToString(), out result8);
							decimal.TryParse(dataGridItems.ActiveRow.Cells["C6"].Value.ToString(), out result9);
							num = result4 + result5 + result6 + result7 + result8 + result9;
							dataGridItems.ActiveRow.Cells["BOQ Quantity"].Value = num;
						}
						if (dataGridItems.ActiveRow.Cells["IsFilled"].Value == "T")
						{
							bool flag = false;
							switch (key)
							{
							case "C1":
							case "C2":
							case "C3":
							case "C4":
							case "C5":
							case "C6":
								_ = SelectedColumns.Count;
								foreach (int value in SelectedColumns.Values)
								{
									if (value >= 2)
									{
										flag = true;
										break;
									}
								}
								break;
							}
							if (flag && dataGridItems.ActiveRow.Cells["IsFilled"].Value == "H")
							{
								string id = dataGridItems.ActiveRow.Cells["BOQ"].Value.ToString();
								DataSet jobBOMByID = Factory.JobBOMSystem.GetJobBOMByID(id);
								if (jobBOMByID.Tables[1].Rows.Count > 0)
								{
									decimal result10 = default(decimal);
									decimal result11 = default(decimal);
									decimal result12 = default(decimal);
									decimal result13 = default(decimal);
									decimal result14 = 5m;
									decimal result15 = default(decimal);
									decimal.TryParse(dataGridItems.ActiveRow.Cells["C1"].Value.ToString(), out result10);
									decimal.TryParse(dataGridItems.ActiveRow.Cells["C2"].Value.ToString(), out result11);
									decimal.TryParse(dataGridItems.ActiveRow.Cells["C3"].Value.ToString(), out result12);
									decimal.TryParse(dataGridItems.ActiveRow.Cells["C4"].Value.ToString(), out result13);
									decimal.TryParse(dataGridItems.ActiveRow.Cells["C5"].Value.ToString(), out result14);
									decimal.TryParse(dataGridItems.ActiveRow.Cells["C6"].Value.ToString(), out result15);
									_ = dataGridItems.ActiveRow.Index;
									foreach (DataRow row in jobBOMByID.Tables[1].Rows)
									{
										_ = row;
										_ = dataGridItems.DataSource;
									}
								}
							}
						}
						if (e.Cell.Column.Key == "BOQ Quantity" && !SelectedColumns.ContainsValue(1) && dataGridItems.ActiveRow.Cells["BOQ Quantity"].Value != null && dataGridItems.ActiveRow.Cells["BOQ Quantity"].Value.ToString() != "0")
						{
							decimal result16 = default(decimal);
							decimal result17 = default(decimal);
							decimal result18 = default(decimal);
							decimal result19 = default(decimal);
							decimal result20 = 5m;
							decimal result21 = default(decimal);
							string id2 = dataGridItems.ActiveRow.Cells["BOQ"].Value.ToString();
							dataGridItems.ActiveRow.Cells["CostCategoryID"].Value.ToString();
							dataGridItems.ActiveRow.Cells["FeeID"].Value.ToString();
							decimal.TryParse(dataGridItems.ActiveRow.Cells["C1"].Value.ToString(), out result16);
							decimal.TryParse(dataGridItems.ActiveRow.Cells["C2"].Value.ToString(), out result17);
							decimal.TryParse(dataGridItems.ActiveRow.Cells["C3"].Value.ToString(), out result18);
							decimal.TryParse(dataGridItems.ActiveRow.Cells["C4"].Value.ToString(), out result19);
							decimal.TryParse(dataGridItems.ActiveRow.Cells["C5"].Value.ToString(), out result20);
							decimal.TryParse(dataGridItems.ActiveRow.Cells["C6"].Value.ToString(), out result21);
							dataGridItems.ActiveRow.Cells["IsFilled"].Value = "T";
							dataGridItems.ActiveRow.Cells["NewRow"].Value = "1";
							JobBOMData jobBOMByID2 = Factory.JobBOMSystem.GetJobBOMByID(id2);
							int index = dataGridItems.ActiveRow.Index;
							dataGridItems.ActiveRow.Cells["RowRelation"].Value = index;
							double result22 = 0.0;
							double num2 = 0.0;
							double num3 = 0.0;
							double num4 = 0.0;
							double num5 = 0.0;
							double num6 = 0.0;
							double num7 = 0.0;
							double num8 = 0.0;
							double num9 = 0.0;
							double num10 = 0.0;
							double num11 = 0.0;
							double num12 = 0.0;
							double num13 = 0.0;
							double num14 = 0.0;
							double.TryParse(dataGridItems.ActiveRow.Cells["BOQ Quantity"].Value.ToString(), out result22);
							foreach (DataRow row2 in jobBOMByID2.Tables[1].Rows)
							{
								UltraGridRow ultraGridRow = dataGridItems.DisplayLayout.Bands[1].AddNew();
								ultraGridRow.Cells["NewRow"].Value = "1";
								ultraGridRow.Cells["Item Code"].Value = row2["ProductID"].ToString();
								ultraGridRow.Cells["Description"].Value = row2["Description"].ToString();
								num2 = double.Parse(row2["Quantity"].ToString());
								ultraGridRow.Cells["HiddenQuantity"].Value = num2;
								num5 = double.Parse(ultraGridRow.Cells["HiddenQuantity"].Value.ToString());
								num2 = result22 * num2;
								ultraGridRow.Cells["Quantity"].Value = num2;
								ultraGridRow.Cells["ActualCost"].Value = double.Parse(row2["Cost"].ToString());
								num3 = double.Parse(row2["Cost"].ToString());
								ultraGridRow.Cells["HiddenCost"].Value = num3;
								num6 = double.Parse(ultraGridRow.Cells["HiddenCost"].Value.ToString());
								num3 = result22 * num3;
								ultraGridRow.Cells["Cost"].Value = num3;
								ultraGridRow.Cells["Hiddenboqquantity"].Value = result22;
								ultraGridRow.Cells["RowRelation"].Value = index;
								num4 = double.Parse(row2["LabourCost"].ToString());
								ultraGridRow.Cells["HiddenLabourCost"].Value = num4;
								num7 = double.Parse(ultraGridRow.Cells["HiddenLabourCost"].Value.ToString());
								num4 = result22 * num4;
								ultraGridRow.Cells["Labour Cost"].Value = num4;
								ultraGridRow.Cells["Unit"].Value = row2["UnitID"].ToString();
								ultraGridRow.Cells["IsFilled"].Value = "T";
								ultraGridRow.Cells["BOMRowIndex"].Value = row2["RowIndex"].ToString();
								num8 = result22 * num5 * num7;
								num9 = result22 * num5 * num6;
								ultraGridRow.Cells["Labour Total"].Value = num8;
								ultraGridRow.Cells["Material Total"].Value = num9;
								double num15 = num9 * num11 / 100.0;
								double num16 = num8 * num12 / 100.0;
								double num17 = (num9 + num8) * num13 / 100.0;
								ultraGridRow.Cells["Material OH"].Value = num15;
								ultraGridRow.Cells["Labour OH"].Value = num16;
								ultraGridRow.Cells["Other OH"].Value = num17;
								num10 = num15 + num16 + num17;
								num10 = num10 + num8 + num9;
								ultraGridRow.Cells["Net Total"].Value = num10;
								num14 += num10;
								ultraGridRow.Update();
							}
							dataGridItems.ActiveRow.Cells["Total"].Value = num14;
							SelectedColumns.Clear();
							VisibleColumnCount = 0;
						}
						if (e.Cell.Column.Key == "BOQ Quantity")
						{
							double result23 = 0.0;
							double result24 = 0.0;
							double result25 = 0.0;
							double result26 = 0.0;
							double num18 = 0.0;
							double num19 = 0.0;
							double num20 = 0.0;
							double num21 = 0.0;
							double num22 = 0.0;
							double num23 = 0.0;
							if (dataGridItems.ActiveRow.Cells["IsFilled"].Value.ToString() == "H")
							{
								double.TryParse(dataGridItems.ActiveRow.Cells["BOQ Quantity"].Value.ToString(), out result23);
								double.TryParse(dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString(), out result24);
								double.TryParse(dataGridItems.ActiveRow.Cells["Cost"].Value.ToString(), out result25);
								double.TryParse(dataGridItems.ActiveRow.Cells["Labour Cost"].Value.ToString(), out result26);
								num18 = result23 * result24 * result26;
								num19 = result23 * result24 * result25;
								dataGridItems.ActiveRow.Cells["Labour Total"].Value = num18;
								dataGridItems.ActiveRow.Cells["Material Total"].Value = num19;
								double num24 = num19 * num21 / 100.0;
								double num25 = num18 * num22 / 100.0;
								double num26 = (num19 + num18) * num23 / 100.0;
								dataGridItems.ActiveRow.Cells["Material OH"].Value = num24;
								dataGridItems.ActiveRow.Cells["Labour OH"].Value = num25;
								dataGridItems.ActiveRow.Cells["Other OH"].Value = num26;
								num20 = num24 + num25 + num26;
								num20 = num20 + num18 + num19;
								dataGridItems.ActiveRow.Cells["Net Total"].Value = num20;
							}
						}
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private void ReCalculatEstimate(bool allRows)
		{
			if (allRows)
			{
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					ReCalculatEstimate(row);
				}
			}
			else if (dataGridItems.ActiveRow != null)
			{
				ReCalculatEstimate(dataGridItems.ActiveRow);
			}
		}

		private void ReCalculateRow(string key)
		{
			try
			{
				double num = 0.0;
				double num2 = 0.0;
				double num3 = 0.0;
				double num4 = 0.0;
				double num5 = 0.0;
				double num6 = 0.0;
				double num7 = 0.0;
				double num8 = 0.0;
				double num9 = 0.0;
				double num10 = 0.0;
				double num11 = 0.0;
				double num12 = 0.0;
				double num13 = 0.0;
				double num14 = 0.0;
				double num15 = 0.0;
				double num16 = 0.0;
				bool flag = false;
				_ = dataGridItems.ActiveRow.ListObject;
				if (dataGridItems.ActiveRow.Band.Index == 1)
				{
					flag = true;
				}
				if (flag)
				{
					if (dataGridItems.ActiveRow.Cells["HiddenQuantity"].Value != null && dataGridItems.ActiveRow.Cells["HiddenQuantity"].Value.ToString() != "")
					{
						num = double.Parse(dataGridItems.ActiveRow.Cells["HiddenQuantity"].Value.ToString());
					}
					if (dataGridItems.ActiveRow.Cells["HiddenCost"].Value != null && dataGridItems.ActiveRow.Cells["HiddenCost"].Value.ToString() != "")
					{
						num2 = double.Parse(dataGridItems.ActiveRow.Cells["HiddenCost"].Value.ToString());
					}
					if (dataGridItems.ActiveRow.Cells["HiddenLabourCost"].Value != null && dataGridItems.ActiveRow.Cells["HiddenLabourCost"].Value.ToString() != "")
					{
						num3 = double.Parse(dataGridItems.ActiveRow.Cells["HiddenLabourCost"].Value.ToString());
					}
					if (dataGridItems.ActiveRow.Cells["Hiddenboqquantity"].Value != null && dataGridItems.ActiveRow.Cells["Hiddenboqquantity"].Value.ToString() != "")
					{
						num10 = double.Parse(dataGridItems.ActiveRow.Cells["Hiddenboqquantity"].Value.ToString());
					}
					num16 = num10 * num;
					if (!rowval)
					{
						rowval = true;
						num14 = num * num2;
						dataGridItems.ActiveRow.Cells["Cost"].Value = num14;
						num15 = num * num3;
						dataGridItems.ActiveRow.Cells["Labour Cost"].Value = num15;
						dataGridItems.ActiveRow.Cells["Quantity"].Value = num16;
						rowval = false;
					}
					if (dataGridItems.ActiveRow.Cells["Hiddenboqquantity"].Value != null && dataGridItems.ActiveRow.Cells["Hiddenboqquantity"].Value.ToString() != "")
					{
						_ = (dataGridItems.ActiveRow.Cells["NewRow"].Value.ToString() == "");
					}
					num7 = num10 * num * num3;
					num8 = num10 * num * num2;
					dataGridItems.ActiveRow.Cells["Labour Total"].Value = num7;
					dataGridItems.ActiveRow.Cells["Material Total"].Value = num8;
					num11 = num8 * num4 / 100.0;
					num12 = num7 * num5 / 100.0;
					num13 = (num8 + num7) * num6 / 100.0;
					dataGridItems.ActiveRow.Cells["Material OH"].Value = num11;
					dataGridItems.ActiveRow.Cells["Labour OH"].Value = num12;
					dataGridItems.ActiveRow.Cells["Other OH"].Value = num13;
					num9 = num11 + num12 + num13;
					num9 = num9 + num7 + num8;
					dataGridItems.ActiveRow.Cells["Net Total"].Value = num9;
				}
				Total(dataGridItems.ActiveRow);
			}
			catch (InvalidCastException ex)
			{
				if (ex.Data == null)
				{
					throw;
				}
			}
		}

		private void Total(UltraGridRow iRow)
		{
			double result = 0.0;
			double num = 0.0;
			if (iRow.ParentRow != null)
			{
				foreach (UltraGridRow row in iRow.ParentRow.ChildBands[0].Rows)
				{
					double.TryParse(row.Cells["Net Total"].Value.ToString(), out result);
					num += result;
				}
				iRow.ParentRow.Cells["Total"].Value = num;
			}
		}

		private void ReCalculatEstimate(UltraGridRow parentRow)
		{
			if (parentRow == null)
			{
				return;
			}
			double result = 0.0;
			double result2 = 0.0;
			double result3 = 0.0;
			double result4 = 0.0;
			double result5 = 0.0;
			double result6 = 0.0;
			double num = 0.0;
			double result7 = 0.0;
			double result8 = 0.0;
			double result9 = 0.0;
			double result10 = 0.0;
			if (parentRow.Band.Index == 1)
			{
				parentRow = parentRow.ParentRow;
			}
			if (parentRow.Cells["C1"].Value != null && parentRow.Cells["C1"].Value.ToString() != "")
			{
				double.TryParse(parentRow.Cells["C1"].Value.ToString(), out result);
			}
			if (parentRow.Cells["C2"].Value != null && parentRow.Cells["C2"].Value.ToString() != "")
			{
				double.TryParse(parentRow.Cells["C2"].Value.ToString(), out result2);
			}
			if (parentRow.Cells["C3"].Value != null && parentRow.Cells["C3"].Value.ToString() != "")
			{
				double.TryParse(parentRow.Cells["C3"].Value.ToString(), out result3);
			}
			if (parentRow.Cells["C4"].Value != null && parentRow.Cells["C4"].Value.ToString() != "")
			{
				double.TryParse(parentRow.Cells["C4"].Value.ToString(), out result4);
			}
			if (parentRow.Cells["C5"].Value != null && parentRow.Cells["C5"].Value.ToString() != "")
			{
				double.TryParse(parentRow.Cells["C5"].Value.ToString(), out result5);
			}
			if (parentRow.Cells["C6"].Value != null && parentRow.Cells["C6"].Value.ToString() != "")
			{
				double.TryParse(parentRow.Cells["C6"].Value.ToString(), out result6);
			}
			if (parentRow.Cells["CMarkup"].Value != null && parentRow.Cells["CMarkup"].Value.ToString() != "")
			{
				double.TryParse(parentRow.Cells["CMarkup"].Value.ToString(), out result7);
			}
			if (parentRow.Cells["MOHP"].Value != null && parentRow.Cells["MOHP"].Value.ToString() != "")
			{
				double.TryParse(parentRow.Cells["MOHP"].Value.ToString(), out result8);
			}
			if (parentRow.Cells["LOHP"].Value != null && parentRow.Cells["LOHP"].Value.ToString() != "")
			{
				double.TryParse(parentRow.Cells["LOHP"].Value.ToString(), out result9);
			}
			if (parentRow.Cells["OOHP"].Value != null && parentRow.Cells["OOHP"].Value.ToString() != "")
			{
				double.TryParse(parentRow.Cells["OOHP"].Value.ToString(), out result10);
			}
			num = result + result2 + result3 + result4 + result5 + result6;
			double.TryParse(parentRow.Cells["BOQ Quantity"].Value.ToString(), out num);
			if (num != 0.0)
			{
				if (!rowval)
				{
					rowval = true;
					parentRow.Cells["BOQ Quantity"].Value = num;
					rowval = false;
				}
			}
			else
			{
				num = double.Parse(parentRow.Cells["BOQ Quantity"].Value.ToString());
			}
			double num2 = 0.0;
			double num3 = 0.0;
			double num4 = 0.0;
			double num5 = 0.0;
			double num6 = 0.0;
			double num7 = 0.0;
			double num8 = 0.0;
			double num9 = 0.0;
			double num10 = 0.0;
			double num11 = 0.0;
			double num12 = 0.0;
			double num13 = 0.0;
			double num14 = 0.0;
			double num15 = 0.0;
			double num16 = 0.0;
			double num17 = 0.0;
			double num18 = 0.0;
			double num19 = 0.0;
			double num20 = 0.0;
			foreach (UltraGridRow row in parentRow.ChildBands[0].Rows)
			{
				parentrowindx = parentRow.Index;
				num2 = double.Parse(row.Cells["HiddenQuantity"].Value.ToString());
				num15 = double.Parse(row.Cells["HiddenQuantity"].Value.ToString());
				num2 = num * num2;
				row.Cells["Quantity"].Value = num2;
				num3 = double.Parse(row.Cells["Cost"].Value.ToString());
				double.Parse(row.Cells["HiddenCost"].Value.ToString());
				num20 = double.Parse(row.Cells["ActualCost"].Value.ToString());
				num3 = num20 + num2 * (num20 * result7 / 100.0);
				row.Cells["Cost"].Value = num3;
				num19 = num20 * result7 / 100.0;
				row.Cells["HiddenCost"].Value = num20 + num19;
				num4 = double.Parse(row.Cells["HiddenLabourCost"].Value.ToString());
				num16 = double.Parse(row.Cells["HiddenLabourCost"].Value.ToString());
				num4 = num * num4;
				row.Cells["Labour Cost"].Value = num4;
				if (!rowval)
				{
					rowval = true;
					num17 = num20 + num20 * result7 / 100.0;
					row.Cells["Cost"].Value = num17;
					num18 = num15 * num16;
					row.Cells["Labour Cost"].Value = num18;
					rowval = false;
				}
				row.Cells["Hiddenboqquantity"].Value = num;
				num6 = result8;
				num7 = result9;
				num8 = result10;
				num13 = num2 * num16;
				num14 = num2 * (num20 + num20 * result7 / 100.0);
				row.Cells["Labour Total"].Value = num13;
				row.Cells["Material Total"].Value = num14;
				num9 = num14 * num6 / 100.0;
				num10 = num13 * num7 / 100.0;
				num11 = (num14 + num13) * num8 / 100.0;
				row.Cells["Material OH"].Value = num9;
				row.Cells["Labour OH"].Value = num10;
				row.Cells["Other OH"].Value = num11;
				num12 = num9 + num10 + num11;
				num12 = num12 + num13 + num14;
				row.Cells["Net Total"].Value = num12;
				num5 += num12;
				row.Cells["Total"].Value = num5;
			}
			parentRow.Cells["Total"].Value = num5;
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
			ReCalculatEstimate(allRows: true);
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			_ = dataGridItems.ActiveRow;
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			_ = e.Cell.Activated;
		}

		private void dataGridItems_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
			comboBoxGridItem.Clear();
		}

		private void comboBoxGridItem_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveCell.Column.Key == "Item Code" && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() == "")
			{
				dataGridItems.ActiveRow.Cells["Description"].Value = "";
			}
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Item Code")
			{
				e.RaiseErrorEvent = false;
				comboBoxGridItem.Text = dataGridItems.ActiveCell.Text;
				comboBoxGridItem.QuickAddItem();
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Quantity")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Invalid quantity. Please enter a numeric value.");
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Location")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Please select a valid location for the item.");
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new JobEstimationData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.JobEstimationTable.Rows[0] : currentData.JobEstimationTable.NewRow();
				if (dateTimePickerDate.Value > DateTime.Today)
				{
					dataRow["TransactionDate"] = dateTimePickerDate.Value;
				}
				else
				{
					DateTime value = dateTimePickerDate.Value;
					dataRow["TransactionDate"] = new DateTime(value.Year, value.Month, value.Day, 11, 59, 59);
				}
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				if (textBoxMaterialOHP.Text != "")
				{
					dataRow["MaterialOHP"] = textBoxMaterialOHP.Text;
				}
				else
				{
					dataRow["MaterialOHP"] = 0;
				}
				if (textBoxLabourOHP.Text != "")
				{
					dataRow["LabourOHP"] = textBoxLabourOHP.Text;
				}
				else
				{
					dataRow["LabourOHP"] = 0;
				}
				if (textBoxOtherOHP.Text != "")
				{
					dataRow["OtherOHP"] = textBoxOtherOHP.Text;
				}
				else
				{
					dataRow["OtherOHP"] = 0;
				}
				dataRow["JobID"] = comboBoxJob.SelectedID;
				dataRow["Note"] = textBoxNote.Text.Trim();
				dataRow["Reference"] = textBoxRef1.Text;
				if (textBoxC1.Text != "")
				{
					dataRow["LabelC1"] = textBoxC1.Text;
				}
				else
				{
					dataRow["LabelC1"] = "C1";
				}
				if (textBoxC2.Text != "")
				{
					dataRow["LabelC2"] = textBoxC2.Text;
				}
				else
				{
					dataRow["LabelC2"] = "C2";
				}
				if (textBoxC3.Text != "")
				{
					dataRow["LabelC3"] = textBoxC3.Text;
				}
				else
				{
					dataRow["LabelC3"] = "C3";
				}
				if (textBoxC4.Text != "")
				{
					dataRow["LabelC4"] = textBoxC4.Text;
				}
				else
				{
					dataRow["LabelC4"] = "C4";
				}
				if (textBoxC5.Text != "")
				{
					dataRow["LabelC5"] = textBoxC5.Text;
				}
				else
				{
					dataRow["LabelC5"] = "C5";
				}
				if (textBoxC6.Text != "")
				{
					dataRow["LabelC6"] = textBoxC6.Text;
				}
				else
				{
					dataRow["LabelC6"] = "C6";
				}
				dataRow["CostCategoryID"] = ComboBoxHeaderCostCategory.SelectedID;
				dataRow["LastRevisedDate"] = dateTimePickerDate.Value;
				if (textBoxLastReviseDate.Text != "")
				{
					dataRow["PreviousRevisedDate"] = DateTime.Parse(textBoxLastReviseDate.Text);
				}
				else
				{
					dataRow["PreviousRevisedDate"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.JobEstimationTable.Rows.Add(dataRow);
				}
				currentData.JobEstimationDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					string value2 = row.Cells["BOQ"].Value.ToString();
					DataRow dataRow2 = currentData.JobEstimationDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["SysDocID"] = selectedID;
					dataRow2["VoucherID"] = text;
					dataRow2["CostCategoryID"] = row.Cells["CostCategoryID"].Value.ToString();
					dataRow2["PhaseID"] = row.Cells["FeeID"].Value.ToString();
					dataRow2["BOQID"] = row.Cells["BOQ"].Value.ToString();
					dataRow2["BOQQuantity"] = row.Cells["BOQ Quantity"].Value.ToString();
					dataRow2["RowIndex"] = row.Index;
					dataRow2["Remarks"] = row.Cells["Remarks"].Value.ToString();
					dataRow2["Total"] = row.Cells["Total"].Value.ToString();
					dataRow2["RowRelation"] = row.Cells["RowRelation"].Value.ToString();
					if (row.Cells["C1"].Value.ToString() != "")
					{
						dataRow2["AttributeC1"] = row.Cells["C1"].Value.ToString();
					}
					else
					{
						dataRow2["AttributeC1"] = 0;
					}
					if (row.Cells["C2"].Value.ToString() != "")
					{
						dataRow2["AttributeC2"] = row.Cells["C2"].Value.ToString();
					}
					else
					{
						dataRow2["AttributeC2"] = 0;
					}
					if (row.Cells["C3"].Value.ToString() != "")
					{
						dataRow2["AttributeC3"] = row.Cells["C3"].Value.ToString();
					}
					else
					{
						dataRow2["AttributeC3"] = 0;
					}
					if (row.Cells["C4"].Value.ToString() != "")
					{
						dataRow2["AttributeC4"] = row.Cells["C4"].Value.ToString();
					}
					else
					{
						dataRow2["AttributeC4"] = 0;
					}
					if (row.Cells["C5"].Value.ToString() != "")
					{
						dataRow2["AttributeC5"] = row.Cells["C5"].Value.ToString();
					}
					else
					{
						dataRow2["AttributeC5"] = 0;
					}
					if (row.Cells["C6"].Value.ToString() != "")
					{
						dataRow2["AttributeC6"] = row.Cells["C6"].Value.ToString();
					}
					else
					{
						dataRow2["AttributeC6"] = 0;
					}
					if (row.Cells["CMarkup"].Value.ToString() != "")
					{
						dataRow2["CostMarkUp"] = row.Cells["CMarkup"].Value.ToString();
					}
					else
					{
						dataRow2["CostMarkUp"] = 0;
					}
					if (row.Cells["MOHP"].Value.ToString() != "")
					{
						dataRow2["MaterialOHP"] = row.Cells["MOHP"].Value.ToString();
					}
					else
					{
						dataRow2["MaterialOHP"] = 0;
					}
					if (row.Cells["LOHP"].Value.ToString() != "")
					{
						dataRow2["LabourOHP"] = row.Cells["LOHP"].Value.ToString();
					}
					else
					{
						dataRow2["LabourOHP"] = 0;
					}
					if (row.Cells["OOHP"].Value.ToString() != "")
					{
						dataRow2["OtherOHP"] = row.Cells["OOHP"].Value.ToString();
					}
					else
					{
						dataRow2["OtherOHP"] = 0;
					}
					dataRow2.EndEdit();
					currentData.JobEstimationDetailTable.Rows.Add(dataRow2);
					foreach (UltraGridRow row2 in row.ChildBands[0].Rows)
					{
						DataRow dataRow3 = currentData.JobEstimationDetailItemsTable.NewRow();
						dataRow3.BeginEdit();
						dataRow3["SysDocID"] = selectedID;
						dataRow3["VoucherID"] = text;
						dataRow3["BOQID"] = value2;
						dataRow3["ProductID"] = row2.Cells["Item Code"].Value.ToString();
						dataRow3["Description"] = row2.Cells["Description"].Value.ToString();
						dataRow3["Quantity"] = row2.Cells["Quantity"].Value.ToString();
						dataRow3["UnitID"] = row2.Cells["Unit"].Value.ToString();
						dataRow3["Cost"] = row2.Cells["Cost"].Value.ToString();
						dataRow3["ActualCost"] = row2.Cells["ActualCost"].Value.ToString();
						dataRow3["LabourCost"] = row2.Cells["Labour Cost"].Value.ToString();
						dataRow3["MaterialTotal"] = row2.Cells["Material Total"].Value.ToString();
						dataRow3["LabourTotal"] = row2.Cells["Labour Total"].Value.ToString();
						dataRow3["MaterialOH"] = row2.Cells["Material OH"].Value.ToString();
						dataRow3["LabourOH"] = row2.Cells["Labour OH"].Value.ToString();
						dataRow3["OtherOH"] = row2.Cells["Other OH"].Value.ToString();
						dataRow3["NetTotal"] = row2.Cells["Net Total"].Value.ToString();
						dataRow3["RowIndex"] = row2.Index;
						dataRow3["UnitQuantity"] = row2.Cells["HiddenQuantity"].Value.ToString();
						dataRow3["UnitLabourCost"] = row2.Cells["HiddenLabourCost"].Value.ToString();
						dataRow3["UnitCost"] = row2.Cells["HiddenCost"].Value.ToString();
						dataRow3["RowRelation"] = row2.Cells["RowRelation"].Value.ToString();
						dataRow3["Remarks"] = row2.Cells["Remarks"].Value.ToString();
						dataRow3.EndEdit();
						currentData.JobEstimationDetailItemsTable.Rows.Add(dataRow3);
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

		private void SelectColumns()
		{
			ColumnChooserDialog obj = new ColumnChooserDialog
			{
				Owner = this
			};
			UltraGridColumnChooser columnChooserControl = obj.ColumnChooserControl;
			columnChooserControl.SourceGrid = dataGridItems;
			columnChooserControl.CurrentBand = dataGridItems.DisplayLayout.Bands[0];
			SelectedColumns.Clear();
			columnChooserControl.Style = ColumnChooserStyle.AllColumnsAndChildBandsWithCheckBoxes;
			columnChooserControl.MultipleBandSupport = MultipleBandSupport.SingleBandOnly;
			obj.Size = new Size(150, 300);
			obj.Show();
		}

		private string IIf(bool Expression, string TruePart, string FalsePart)
		{
			if (!Expression)
			{
				return FalsePart;
			}
			return TruePart;
		}

		private void AdjustGridHeading()
		{
			if (isNewRecord)
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["C1"].Header.Caption = IIf(textBoxC1.Text == "", "C1", textBoxC1.Text);
				dataGridItems.DisplayLayout.Bands[0].Columns["C2"].Header.Caption = IIf(textBoxC2.Text == "", "C2", textBoxC2.Text);
				dataGridItems.DisplayLayout.Bands[0].Columns["C3"].Header.Caption = IIf(textBoxC3.Text == "", "C3", textBoxC3.Text);
				dataGridItems.DisplayLayout.Bands[0].Columns["C4"].Header.Caption = IIf(textBoxC4.Text == "", "C4", textBoxC4.Text);
				dataGridItems.DisplayLayout.Bands[0].Columns["C5"].Header.Caption = IIf(textBoxC5.Text == "", "C5", textBoxC5.Text);
				dataGridItems.DisplayLayout.Bands[0].Columns["C6"].Header.Caption = IIf(textBoxC6.Text == "", "C6", textBoxC6.Text);
			}
			UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["C1"];
			UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["C2"];
			UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["C3"];
			UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["C4"];
			UltraGridColumn ultraGridColumn5 = dataGridItems.DisplayLayout.Bands[0].Columns["C5"];
			bool flag2 = dataGridItems.DisplayLayout.Bands[0].Columns["C6"].Hidden = false;
			bool flag4 = ultraGridColumn5.Hidden = flag2;
			bool flag6 = ultraGridColumn4.Hidden = flag4;
			bool flag8 = ultraGridColumn3.Hidden = flag6;
			bool hidden = ultraGridColumn2.Hidden = flag8;
			ultraGridColumn.Hidden = hidden;
			if (!isNewRecord)
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["C1"].Header.Caption = IIf(textBoxC1.Text == "", "C1", textBoxC1.Text);
				dataGridItems.DisplayLayout.Bands[0].Columns["C2"].Header.Caption = IIf(textBoxC2.Text == "", "C2", textBoxC2.Text);
				dataGridItems.DisplayLayout.Bands[0].Columns["C3"].Header.Caption = IIf(textBoxC3.Text == "", "C3", textBoxC3.Text);
				dataGridItems.DisplayLayout.Bands[0].Columns["C4"].Header.Caption = IIf(textBoxC4.Text == "", "C4", textBoxC4.Text);
				dataGridItems.DisplayLayout.Bands[0].Columns["C5"].Header.Caption = IIf(textBoxC5.Text == "", "C5", textBoxC5.Text);
				dataGridItems.DisplayLayout.Bands[0].Columns["C6"].Header.Caption = IIf(textBoxC6.Text == "", "C6", textBoxC6.Text);
			}
		}

		private void SetupGrid()
		{
			try
			{
				DataSet dataSet = new DataSet();
				dataGridItems.Clear();
				dataGridItems.DataSource = null;
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable("BOMParent");
				dataTable.Columns.Add("NewRow");
				dataTable.Columns.Add("CostCategoryID");
				dataTable.Columns.Add("FeeID");
				dataTable.Columns.Add("BOQ");
				dataTable.Columns.Add("IsFilled");
				dataTable.Columns.Add("Remarks");
				dataTable.Columns.Add("CMarkup");
				dataTable.Columns.Add("MOHP");
				dataTable.Columns.Add("LOHP");
				dataTable.Columns.Add("OOHP");
				dataTable.Columns.Add("C1");
				dataTable.Columns.Add("C2");
				dataTable.Columns.Add("C3");
				dataTable.Columns.Add("C4");
				dataTable.Columns.Add("C5");
				dataTable.Columns.Add("C6");
				dataTable.Columns.Add("BOQ Quantity");
				dataTable.Columns.Add("Total");
				dataTable.Columns.Add("RowRelation");
				dataSet.Tables.Add(dataTable);
				DataTable dataTable2 = new DataTable("BOMChild");
				dataTable2.Columns.Add("NewRow");
				dataTable2.Columns.Add("BOQ");
				dataTable2.Columns.Add("Item Code");
				dataTable2.Columns.Add("Description");
				dataTable2.Columns.Add("ItemType", typeof(byte));
				dataTable2.Columns.Add("Unit");
				dataTable2.Columns.Add("HiddenQuantity", typeof(decimal));
				dataTable2.Columns.Add("HiddenCost", typeof(decimal));
				dataTable2.Columns.Add("ActualCost", typeof(decimal));
				dataTable2.Columns.Add("HiddenLabourCost", typeof(decimal));
				dataTable2.Columns.Add("Quantity", typeof(decimal));
				dataTable2.Columns.Add("Cost", typeof(decimal));
				dataTable2.Columns.Add("Labour Cost", typeof(decimal));
				dataTable2.Columns.Add("Material Total", typeof(decimal));
				dataTable2.Columns.Add("Labour Total", typeof(decimal));
				dataTable2.Columns.Add("Material OH", typeof(decimal));
				dataTable2.Columns.Add("Labour OH", typeof(decimal));
				dataTable2.Columns.Add("Other OH", typeof(decimal));
				dataTable2.Columns.Add("Net Total", typeof(decimal));
				dataTable2.Columns.Add("Remarks");
				dataTable2.Columns.Add("BOMRowIndex");
				dataTable2.Columns.Add("IsFilled");
				dataTable2.Columns.Add("Hiddenboqquantity");
				dataTable2.Columns.Add("Total");
				dataTable2.Columns.Add("RowRelation");
				dataSet.Tables.Add(dataTable2);
				dataSet.Relations.Add("REL", new DataColumn[2]
				{
					dataSet.Tables["BOMParent"].Columns["BOQ"],
					dataSet.Tables["BOMParent"].Columns["RowRelation"]
				}, new DataColumn[2]
				{
					dataSet.Tables["BOMChild"].Columns["BOQ"],
					dataSet.Tables["BOMChild"].Columns["RowRelation"]
				}, createConstraints: false);
				dataGridItems.DataSource = dataSet;
				AdjustGridHeading();
				dataGridItems.LoadLayout();
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["BOQ Quantity"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[1].Columns["Description"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[1].Columns["Item Code"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[1].Columns["Quantity"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[1].Columns["Cost"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[1].Columns["Unit"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["BOQ"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour Cost"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[1].Columns["Material Total"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour Total"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[1].Columns["Material OH"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour OH"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[1].Columns["Other OH"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[1].Columns["Net Total"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["NewRow"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsFilled"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[1].Columns["IsFilled"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[1].Columns["HiddenQuantity"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[1].Columns["Hiddenboqquantity"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["RowRelation"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsFilled"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[1].Columns["BOQ"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[1].Columns["Hiddenboqquantity"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[1].Columns["Cost"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour Cost"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[1].Columns["ActualCost"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["RowRelation"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[1].Columns["RowRelation"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[1].Columns["HiddenQuantity"].Header.Caption = "Qty(Unit)";
				dataGridItems.DisplayLayout.Bands[1].Columns["HiddenCost"].Header.Caption = "Cost(Unit)";
				dataGridItems.DisplayLayout.Bands[1].Columns["HiddenLabourCost"].Header.Caption = "Labour Cost(Unit)";
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].Header.Caption = "Description";
				dataGridItems.DisplayLayout.Bands[1].Columns["Quantity"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[1].Columns["Material Total"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour Total"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[1].Columns["Material OH"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour OH"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[1].Columns["Other OH"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[1].Columns["Net Total"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].MaxLength = 30;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].ValueList = comboBoxCostCategory;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].Header.Caption = "CostCategoryID";
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeID"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeID"].ValueList = ComboBoxFee;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeID"].Header.Caption = "FeeID";
				dataGridItems.DisplayLayout.Bands[0].Columns["BOQ"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["BOQ"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["BOQ"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["BOQ"].ValueList = ComboBoxJobBOM;
				dataGridItems.DisplayLayout.Bands[0].Columns["BOQ"].Header.Caption = "BOQ";
				dataGridItems.DisplayLayout.Bands[1].Columns["Item Code"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[1].Columns["Item Code"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[1].Columns["Item Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[1].Columns["Item Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[1].Columns["Item Code"].ValueList = comboBoxGridItem;
				dataGridItems.DisplayLayout.Bands[1].Columns["Unit"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[1].Columns["Unit"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[1].Columns["Unit"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[1].Columns["Unit"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[1].Columns["Unit"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["Unit"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["Unit"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[1].Columns["Unit"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[1].Columns["ActualCost"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[1].Columns["ActualCost"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["ActualCost"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["ActualCost"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[1].Columns["ActualCost"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[1].Columns["Cost"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[1].Columns["Cost"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[1].Columns["Cost"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["Cost"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour Cost"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour Cost"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour Cost"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour Cost"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[1].Columns["Material Total"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[1].Columns["Material Total"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[1].Columns["Material Total"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["Material Total"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour Total"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour Total"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour Total"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour Total"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[1].Columns["Material OH"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[1].Columns["Material OH"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[1].Columns["Material OH"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["Material OH"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour OH"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour OH"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour OH"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour OH"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[1].Columns["Other OH"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[1].Columns["Other OH"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[1].Columns["Other OH"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["Other OH"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[1].Columns["Net Total"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[1].Columns["Net Total"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[1].Columns["Net Total"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["Net Total"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["C1"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["C1"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["C1"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["C1"].Format = Format.NumberFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["NewRow"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[1].Columns["NewRow"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[1].Columns["BOMRowIndex"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[1].Columns["IsFilled"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[1].Columns["ItemType"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[1].Columns["Total"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[1].Columns["ItemType"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[1].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[1].Columns["Description"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[1].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["Quantity"].Format = "#,0.#####";
				dataGridItems.DisplayLayout.Bands[0].Columns["BOQ Quantity"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["BOQ Quantity"].Format = "#,0.#####";
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].Format = "#,0.#####";
				dataGridItems.DisplayLayout.Bands[1].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeID"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["BOQ"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].Width = checked(50 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["C1"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["C2"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["C3"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["C4"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["C5"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["C6"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["BOQ Quantity"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].Width = checked(40 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[1].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[1].Columns["Item Code"].Width = checked(18 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[1].Columns["Unit"].Width = checked(15 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[1].Columns["Quantity"].Width = checked(15 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[1].Columns["Cost"].Width = checked(15 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour Cost"].Width = checked(15 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[1].Columns["Material Total"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour Total"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[1].Columns["Material OH"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[1].Columns["Labour OH"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[1].Columns["Other OH"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[1].Columns["Net Total"].Width = checked(30 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[1].Columns["Remarks"].Width = checked(35 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Override.SummaryFooterAppearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Clear();
				dataGridItems.DisplayLayout.Bands[1].Summaries.Clear();
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Total", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Total"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[1].Summaries.Add("Labour Total", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[1].Columns["Labour Total"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[1].Summaries.Add("Material Total", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[1].Columns["Material Total"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[1].Summaries.Add("Material OH", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[1].Columns["Material OH"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[1].Summaries.Add("Labour OH", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[1].Columns["Labour OH"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[1].Summaries.Add("Other OH", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[1].Columns["Other OH"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.Rows.ExpandAll(recursive: true);
				dataGridItems.SetupUI();
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void dataGridItems_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (dataGridItems.ActiveRow != null && ultraGridColumn != null && ultraGridColumn.Key == "Item Code")
			{
				contextMenuStrip1.Show(dataGridItems, new Point(0, 20), ToolStripDropDownDirection.BelowRight);
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			dataGridItems.Focus();
		}

		public void LoadData(string voucherID, bool isRevised)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					int revisedNo = 0;
					if (IsRevised && comboBoxRevisionNo.Text != "")
					{
						revisedNo = int.Parse(comboBoxRevisionNo.SelectedValue.ToString());
					}
					currentData = Factory.JobEstimationSystem.GetJobEstimationByID(SystemDocID, voucherID, IsRevised, revisedNo);
					if (currentData != null)
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

		private void FillGridData(DataSet data)
		{
			DataSet dataSet = dataGridItems.DataSource as DataSet;
			dataSet.Tables[1].Rows.Clear();
			dataSet.Tables[0].Rows.Clear();
			DataTable dataTable = dataSet.Tables["BOMParent"];
			foreach (DataRow row in data.Tables["Job_Estimation_Detail"].Rows)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["BOQ"] = row["BOQID"];
				dataRow2["BOQ Quantity"] = decimal.Parse(row["BOQQuantity"].ToString()).ToString(Format.TotalAmountFormat);
				dataRow2["CostCategoryID"] = row["CostCategoryID"];
				dataRow2["FeeID"] = row["PhaseID"];
				dataRow2["C1"] = row["AttributeC1"];
				dataRow2["C2"] = row["AttributeC2"];
				dataRow2["C3"] = row["AttributeC3"];
				dataRow2["C4"] = row["AttributeC4"];
				dataRow2["C5"] = row["AttributeC5"];
				dataRow2["C6"] = row["AttributeC6"];
				dataRow2["IsFilled"] = "T";
				dataRow2["Remarks"] = row["Remarks"];
				dataRow2["Total"] = row["Total"];
				dataRow2["RowRelation"] = row["RowRelation"];
				dataRow2["CMarkup"] = row["CostMarkUp"];
				if (row["MaterialOHP"] != DBNull.Value)
				{
					dataRow2["MOHP"] = decimal.Parse(row["MaterialOHP"].ToString()).ToString(Format.TotalAmountFormat);
				}
				else
				{
					dataRow2["MOHP"] = ConstMOHP;
				}
				if (row["LabourOHP"] != DBNull.Value)
				{
					dataRow2["LOHP"] = decimal.Parse(row["LabourOHP"].ToString()).ToString(Format.TotalAmountFormat);
				}
				else
				{
					dataRow2["LOHP"] = ConstLOHP;
				}
				if (row["OtherOHP"] != DBNull.Value)
				{
					dataRow2["OOHP"] = decimal.Parse(row["OtherOHP"].ToString()).ToString(Format.TotalAmountFormat);
				}
				else
				{
					dataRow2["OOHP"] = ConstOOHP;
				}
				dataRow2.EndEdit();
				dataTable.Rows.Add(dataRow2);
			}
			dataTable.AcceptChanges();
			DataTable dataTable2 = dataSet.Tables["BOMChild"];
			foreach (DataRow row2 in data.Tables["Job_Estimation_Detail_Item"].Rows)
			{
				DataRow dataRow4 = dataTable2.NewRow();
				dataRow4["BOQ"] = row2["BOQID"];
				dataRow4["Item Code"] = row2["ProductID"];
				dataRow4["Description"] = row2["Description"];
				dataRow4["Unit"] = row2["UnitID"];
				dataRow4["Quantity"] = decimal.Parse(row2["Quantity"].ToString()).ToString(Format.TotalAmountFormat);
				dataRow4["Cost"] = decimal.Parse(row2["Cost"].ToString()).ToString(Format.TotalAmountFormat);
				decimal result = default(decimal);
				decimal.TryParse(row2["ActualCost"].ToString(), out result);
				if (result != 0m)
				{
					dataRow4["ActualCost"] = decimal.Parse(row2["ActualCost"].ToString()).ToString(Format.TotalAmountFormat);
				}
				else
				{
					dataRow4["ActualCost"] = decimal.Parse(row2["UnitCost"].ToString()).ToString(Format.TotalAmountFormat);
				}
				dataRow4["Labour Cost"] = decimal.Parse(row2["LabourCost"].ToString()).ToString(Format.TotalAmountFormat);
				dataRow4["Material Total"] = decimal.Parse(row2["MaterialTotal"].ToString()).ToString(Format.TotalAmountFormat);
				dataRow4["Labour Total"] = decimal.Parse(row2["LabourTotal"].ToString()).ToString(Format.TotalAmountFormat);
				dataRow4["Material OH"] = decimal.Parse(row2["MaterialOH"].ToString()).ToString(Format.TotalAmountFormat);
				dataRow4["Labour OH"] = decimal.Parse(row2["LabourOH"].ToString()).ToString(Format.TotalAmountFormat);
				dataRow4["Other OH"] = decimal.Parse(row2["OtherOH"].ToString()).ToString(Format.TotalAmountFormat);
				dataRow4["Net Total"] = decimal.Parse(row2["NetTotal"].ToString()).ToString(Format.TotalAmountFormat);
				dataRow4["BOMRowIndex"] = row2["RowIndex"];
				dataRow4["IsFilled"] = "T";
				dataRow4["RowRelation"] = row2["RowRelation"];
				dataRow4["Remarks"] = row2["Remarks"];
				dataRow4["HiddenQuantity"] = decimal.Parse(row2["UnitQuantity"].ToString()).ToString(Format.TotalAmountFormat);
				dataRow4["HiddenCost"] = decimal.Parse(row2["UnitCost"].ToString()).ToString(Format.TotalAmountFormat);
				dataRow4["HiddenLabourCost"] = decimal.Parse(row2["UnitLabourCost"].ToString()).ToString(Format.TotalAmountFormat);
				dataRow4["Hiddenboqquantity"] = decimal.Parse(row2["ActualBOQQuantity"].ToString()).ToString(Format.TotalAmountFormat);
				dataRow4.EndEdit();
				dataTable2.Rows.Add(dataRow4);
			}
			dataTable2.AcceptChanges();
		}

		private void FillData()
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables["Job_Estimation"].Rows[0];
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
					textBoxLabourOHP.Text = dataRow["LabourOHP"].ToString();
					textBoxMaterialOHP.Text = dataRow["MaterialOHP"].ToString();
					textBoxOtherOHP.Text = dataRow["OtherOHP"].ToString();
					comboBoxJob.SelectedID = dataRow["JobID"].ToString();
					ComboBoxHeaderCostCategory.SelectedID = dataRow["CostCategoryID"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					decimal.TryParse(dataRow["LabourOHP"].ToString(), out lohp);
					decimal.TryParse(dataRow["MaterialOHP"].ToString(), out mohp);
					decimal.TryParse(dataRow["OtherOHP"].ToString(), out oohp);
					textBoxC1.Text = dataRow["LabelC1"].ToString();
					textBoxC2.Text = dataRow["LabelC2"].ToString();
					textBoxC3.Text = dataRow["LabelC3"].ToString();
					textBoxC4.Text = dataRow["LabelC4"].ToString();
					textBoxC5.Text = dataRow["LabelC5"].ToString();
					textBoxC6.Text = dataRow["LabelC6"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					if (dataRow["LastRevisedDate"] != DBNull.Value)
					{
						textBoxLastReviseDate.Text = DateTime.Parse(dataRow["LastRevisedDate"].ToString()).ToShortDateString();
						dateTimePickerDate.Value = DateTime.Now;
					}
					else
					{
						textBoxLastReviseDate.Clear();
					}
					if (dataRow["IsVoid"] != DBNull.Value)
					{
						IsVoid = bool.Parse(dataRow["IsVoid"].ToString());
					}
					else
					{
						IsVoid = false;
					}
					AdjustGridHeading();
					DataSet obj = dataGridItems.DataSource as DataSet;
					obj.Tables[1].Rows.Clear();
					obj.Tables[0].Rows.Clear();
					if (currentData.Tables.Contains("Job_Estimation_Detail") && currentData.JobEstimationDetailTable.Rows.Count != 0)
					{
						FillGridData(currentData);
					}
				}
			}
			catch
			{
				throw;
			}
		}

		private bool SaveData()
		{
			return SaveData(clearAfter: true);
		}

		private bool SaveData(bool clearAfter)
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
				bool flag = Factory.JobEstimationSystem.CreateJobEstimation(currentData, !isNewRecord, IsRevised);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					bool result = false;
					bool result2 = false;
					bool.TryParse(comboBoxSysDoc.GetSelectedCellValue("DoPrint").ToString(), out result);
					if (result)
					{
						bool.TryParse(comboBoxSysDoc.GetSelectedCellValue("PrintAfterSave").ToString(), out result2);
						if (result2)
						{
							Print(isPrint: true, showPrintDialog: true, saveChanges: false);
						}
						else
						{
							Print(isPrint: false, showPrintDialog: true, saveChanges: false);
						}
					}
					if (clearAfter)
					{
						ClearForm();
						IsNewRecord = true;
					}
					else
					{
						formManager.ResetDirty();
					}
				}
				return flag;
			}
			catch (CompanyException ex)
			{
				if (ex.Number == 1046)
				{
					string nextVoucherNumber = GetNextVoucherNumber();
					if (nextVoucherNumber == textBoxVoucherNumber.Text)
					{
						ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
						return false;
					}
					if (nextVoucherNumber != "")
					{
						textBoxVoucherNumber.Text = nextVoucherNumber;
					}
					formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
					return SaveData();
				}
				ErrorHelper.ProcessError(ex);
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool ValidateData()
		{
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Job_Estimation", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to (SecurityRoleID:116).");
				return false;
			}
			if (!Factory.SystemDocumentSystem.HasuserAccess(comboBoxSysDoc.SelectedID, Global.DefaultLocationID) && !Global.IsUserAdmin && !AllowEditTransDiffLocation)
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:117).");
				return false;
			}
			DateTime t = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
			int num = 0;
			num = Security.AllowedDays(GeneralSecurityRoles.EnterBackDatedTransaction);
			DateTime value = dateTimePickerDate.Value;
			TimeSpan timeSpan = t.Add(TimeSpan.FromDays(1.0)) - value;
			bool flag = false;
			checked
			{
				if (timeSpan.Days <= num + 1)
				{
					flag = true;
				}
				else if (Global.isUserAdmin)
				{
					flag = true;
				}
				else if (num == 0)
				{
					flag = true;
				}
				if (isNewRecord && dateTimePickerDate.Value < t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EnterBackDatedTransaction))
				{
					ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions.");
					return false;
				}
				if (!flag)
				{
					ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions not more than " + num + " days.");
					return false;
				}
				if (isNewRecord && dateTimePickerDate.Value > t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
				{
					ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
					return false;
				}
				if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "" || comboBoxJob.SelectedID == "" || ComboBoxHeaderCostCategory.SelectedID == "")
				{
					ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
					return false;
				}
				for (int i = 0; i < dataGridItems.Rows.Count; i++)
				{
					if (!dataGridItems.HasRowAnyValue(dataGridItems.Rows[i]))
					{
						dataGridItems.Rows[i].Delete(displayPrompt: false);
						continue;
					}
					if (dataGridItems.Rows[i].Cells["BOQ"].Value.ToString() == "" && dataGridItems.Rows[i].Cells["Item Code"].Value.ToString() == "")
					{
						ErrorHelper.InformationMessage("Please select an item.");
						dataGridItems.Rows[i].Activate();
						return false;
					}
					if (dataGridItems.Rows[i].Cells["BOQ Quantity"].Value.ToString() == "" && dataGridItems.Rows[i].Cells["NewRow"].Value.ToString() != "1")
					{
						ErrorHelper.InformationMessage("Please insert Quantity for item.");
						dataGridItems.Rows[i].Activate();
						return false;
					}
				}
				if (dataGridItems.Rows.Count == 0)
				{
					ErrorHelper.InformationMessage("There should be at least one item row.");
					return false;
				}
				if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Job_Estimation", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
					textBoxVoucherNumber.Focus();
					return false;
				}
				return true;
			}
		}

		public void ShowForApproval(string sysDocID, string voucherID, int approvalTaskID)
		{
			EditDocument(sysDocID, voucherID);
			panelButtons.Visible = false;
			toolStrip1.Enabled = false;
			formManager.ShowApprovalPanel(approvalTaskID, "Job_Estimation", "VoucherID");
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
			try
			{
				textBoxNote.Clear();
				textBoxJob.Clear();
				textBoxLabourOHP.Clear();
				textBoxMaterialOHP.Clear();
				textBoxOtherOHP.Clear();
				comboBoxJob.Clear();
				ComboBoxHeaderCostCategory.Clear();
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxGridItem.Clear();
				textBoxC1.Clear();
				textBoxC2.Clear();
				textBoxC3.Clear();
				textBoxC4.Clear();
				textBoxC5.Clear();
				textBoxC6.Clear();
				textBoxCostCategory.Clear();
				textBoxRef1.Clear();
				SelectedColumns.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				DataSet obj = dataGridItems.DataSource as DataSet;
				obj.Tables[1].Rows.Clear();
				obj.Tables[0].Rows.Clear();
				AdjustGridHeading();
				IsRevised = false;
				formManager.ResetDirty();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void JournalLeaveGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void JournalLeaveGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.JobEstimationSystem.DeleteJobEstimation(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Job_Estimation", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID, IsRevised);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Job_Estimation", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID, IsRevised);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Job_Estimation", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID, IsRevised);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Job_Estimation", "VoucherID", "SysDocID", SystemDocID);
			if (!(firstID == ""))
			{
				LoadData(firstID, IsRevised);
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
				else
				{
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Job_Estimation", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
					if (text != "")
					{
						LoadData(text, IsRevised);
					}
					else
					{
						ErrorHelper.InformationMessage(UIMessages.DocumentNotFound);
						toolStripTextBoxFind.SelectAll();
						toolStripTextBoxFind.Focus();
					}
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

		private void ItemGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
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

		private void JournalLeavesForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridItems.SetupUI();
				SetupGrid();
				comboBoxSysDoc.FilterByType(SysDocTypes.JobEstimation);
				dateTimePickerDate.Value = DateTime.Now;
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
				}
			}
			catch (Exception e2)
			{
				dataGridItems.LoadLayoutFailed = true;
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
				return;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeDocNumber))
			{
				textBoxVoucherNumber.ReadOnly = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransaction))
			{
				AllowEditTransaction = false;
			}
			else
			{
				AllowEditTransaction = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransDiffLocation))
			{
				AllowEditTransDiffLocation = false;
			}
			else
			{
				AllowEditTransDiffLocation = true;
			}
		}

		private void dataGridItems_AfterCellActivate(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				_ = dataGridItems.ActiveCell;
			}
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
		}

		private string GetNextVoucherNumber()
		{
			try
			{
				return Factory.SystemDocumentSystem.GetNextDocumentNumber(SystemDocID);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private void buttonVoid_Click(object sender, EventArgs e)
		{
			if (Void(!IsVoid))
			{
				IsVoid = !IsVoid;
			}
		}

		private bool Void(bool isVoid)
		{
			try
			{
				DialogResult dialogResult = (!isVoid) ? ErrorHelper.QuestionMessageYesNo(UIMessages.WantToUnvoid) : ErrorHelper.QuestionMessageYesNo(UIMessages.WantToVoid);
				if (dialogResult == DialogResult.No)
				{
					return false;
				}
				return Factory.JobEstimationSystem.VoidJobEstimation(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.JobEstimation);
		}

		private void saveAsDraftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveDraft();
		}

		private bool SaveDraft()
		{
			try
			{
				if (GetData())
				{
					EnterNameDialog enterNameDialog = new EnterNameDialog();
					if (enterNameDialog.ShowDialog() == DialogResult.OK)
					{
						return Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.JobInventoryIssue);
					}
				}
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool LoadDraft()
		{
			try
			{
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 71.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					DataSet dataSet = Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.JobInventoryIssue);
					currentData = (dataSet as JobEstimationData);
					FillData();
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void loadDraftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (CanClose())
			{
				LoadDraft();
			}
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void Print()
		{
			Print(isPrint: true, showPrintDialog: false, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false)))
				{
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					if (selectedID == "" || text == "")
					{
						ErrorHelper.InformationMessage("Please select a document to print!");
					}
					else
					{
						DataSet dataSet = null;
						dataSet = (IsRevised ? Factory.JobEstimationSystem.GetJobEstimationRevToPrint(selectedID, text, int.Parse(comboBoxRevisionNo.SelectedValue.ToString())) : Factory.JobEstimationSystem.GetJobEstimationToPrint(selectedID, text));
						if (dataSet == null || dataSet.Tables.Count == 0)
						{
							ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
						}
						else
						{
							PrintHelper.PrintDocument(dataSet, selectedID, "Project Estimation", SysDocTypes.JobEstimation, isPrint, showPrintDialog);
						}
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.JobEstimationListFormObj);
		}

		public void EditDocument(string sysDocID, string voucherID)
		{
			if (!comboBoxSysDoc.Enabled && sysDocID != comboBoxSysDoc.SelectedID)
			{
				ErrorHelper.ErrorMessage("Cannot edit this document because you do not have access to this document.");
				return;
			}
			comboBoxSysDoc.SelectedID = sysDocID;
			LoadData(voucherID, IsRevised);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
		{
			if (dataGridItems.AllowAddNew)
			{
				copyToolStripMenuItem.Enabled = !IsNewRecord;
				reviseToolStripMenuItem.Enabled = !IsNewRecord;
			}
			else
			{
				copyToolStripMenuItem.Enabled = false;
				reviseToolStripMenuItem.Enabled = false;
			}
		}

		private void buttonApply_Click(object sender, EventArgs e)
		{
			AdjustGridHeading();
			ReCalculatEstimate(allRows: true);
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure to duplicate this document?") == DialogResult.Yes)
			{
				string text = textBoxVoucherNumber.Text;
				if (!IsDirty)
				{
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
				else if (CanClose())
				{
					LoadData(text, IsRevised);
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
			}
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditJob(comboBoxJob.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCostCategory(ComboBoxHeaderCostCategory.SelectedID);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxVoucherNumber.Text.Trim();
					docManagementForm.EntitySysDocID = comboBoxSysDoc.SelectedID;
					docManagementForm.EntityName = comboBoxSysDoc.SelectedID;
					docManagementForm.EntityType = EntityTypesEnum.Transactions;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private decimal GetTransactionBalance()
		{
			decimal result = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (row.Cells["Total"].Value != null && row.Cells["Total"].Value.ToString() != "")
				{
					result += decimal.Parse(row.Cells["Total"].Value.ToString());
				}
			}
			return result;
		}

		private void dataGridItems_AfterRowInsert(object sender, RowEventArgs e)
		{
		}

		private void reviseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure to revise this document?") == DialogResult.Yes)
			{
				IsRevised = true;
				IsRevised = true;
				if (IsRevised)
				{
					LoadRevsionCombo();
				}
			}
		}

		private void LoadRevsionCombo()
		{
			DataSet dataSet = new DataSet();
			dataSet = Factory.JobEstimationSystem.GetLoadRevisionCombo(SystemDocID, textBoxVoucherNumber.Text);
			if (!isloadrev)
			{
				isloadrev = true;
				comboBoxRevisionNo.DataSource = dataSet.Tables[0];
				comboBoxRevisionNo.DisplayMember = "RevisionNo";
				comboBoxRevisionNo.ValueMember = "RevisionNoInt";
				isloadrev = false;
			}
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void comboBoxRevisionNo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isloadrev)
			{
				LoadData(textBoxVoucherNumber.Text, IsRevised);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Projects.JobEstimationDetailsForm));
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
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
			Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance74 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance75 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance76 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance77 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance78 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance79 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance80 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance81 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance82 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance83 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance84 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance85 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance86 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance87 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance88 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance89 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance90 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance91 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance92 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance93 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance94 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance95 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance96 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance97 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance98 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance99 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance100 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance101 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance102 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance103 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance104 = new Infragistics.Win.Appearance();
			tabPageExpense = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			labelVoided = new System.Windows.Forms.Label();
			comboBoxGridItem = new Micromind.DataControls.ProductComboBox();
			ultraCalcManager1 = new Infragistics.Win.UltraWinCalcManager.UltraCalcManager(components);
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxOtherOHP = new Micromind.UISupport.PercentTextBox();
			textBoxLabourOHP = new Micromind.UISupport.PercentTextBox();
			textBoxMaterialOHP = new Micromind.UISupport.PercentTextBox();
			label4 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			buttonApply = new System.Windows.Forms.Button();
			textBoxC6 = new Micromind.UISupport.MMTextBox();
			textBoxC5 = new Micromind.UISupport.MMTextBox();
			textBoxC4 = new Micromind.UISupport.MMTextBox();
			textBoxC3 = new Micromind.UISupport.MMTextBox();
			LabelC6 = new System.Windows.Forms.Label();
			LabelC5 = new System.Windows.Forms.Label();
			LabelC4 = new System.Windows.Forms.Label();
			LabelC3 = new System.Windows.Forms.Label();
			textBoxC2 = new Micromind.UISupport.MMTextBox();
			LabelC2 = new System.Windows.Forms.Label();
			LabelC1 = new System.Windows.Forms.Label();
			textBoxC1 = new Micromind.UISupport.MMTextBox();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			reviseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			panelDetails = new System.Windows.Forms.Panel();
			panelReviseDate = new Micromind.UISupport.NonDirtyPanel(components);
			comboBoxRevisionNo = new System.Windows.Forms.ComboBox();
			label6 = new System.Windows.Forms.Label();
			textBoxLastReviseDate = new Micromind.UISupport.MMTextBox();
			label11 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxCostCategory = new System.Windows.Forms.TextBox();
			ComboBoxHeaderCostCategory = new Micromind.DataControls.CostCategoryComboBox();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			textBoxJob = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxJob = new Micromind.DataControls.JobComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			printListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			ultraTabControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage2 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			ComboBoxJobBOM = new Micromind.DataControls.JobBOMComboBox();
			ComboBoxFee = new Micromind.DataControls.JobFeeComboBox();
			formManager = new Micromind.DataControls.FormManager();
			comboBoxLocation = new Micromind.DataControls.LocationComboBox();
			comboBoxGridLocation = new Micromind.DataControls.LocationComboBox();
			comboBoxCostCategory = new Micromind.DataControls.CostCategoryComboBox();
			comboBoxGridProductUnit = new Micromind.DataControls.ProductUnitComboBox();
			tabPageExpense.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			ultraTabPageControl3.SuspendLayout();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			panelReviseDate.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ComboBoxHeaderCostCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl2).BeginInit();
			ultraTabControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ComboBoxJobBOM).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxFee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridProductUnit).BeginInit();
			SuspendLayout();
			tabPageExpense.Controls.Add(labelVoided);
			tabPageExpense.Controls.Add(comboBoxGridItem);
			tabPageExpense.Controls.Add(dataGridItems);
			tabPageExpense.Location = new System.Drawing.Point(1, 23);
			tabPageExpense.Name = "tabPageExpense";
			tabPageExpense.Size = new System.Drawing.Size(1080, 399);
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(3, 188);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(1077, 81);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			comboBoxGridItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxGridItem.AlwaysInEditMode = true;
			comboBoxGridItem.Assigned = false;
			comboBoxGridItem.CalcManager = ultraCalcManager1;
			comboBoxGridItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridItem.CustomReportFieldName = "";
			comboBoxGridItem.CustomReportKey = "";
			comboBoxGridItem.CustomReportValueType = 1;
			comboBoxGridItem.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridItem.DisplayLayout.Appearance = appearance;
			comboBoxGridItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxGridItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxGridItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridItem.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridItem.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridItem.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxGridItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridItem.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxGridItem.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxGridItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridItem.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxGridItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxGridItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridItem.Editable = true;
			comboBoxGridItem.FilterCustomerID = "";
			comboBoxGridItem.FilterString = "";
			comboBoxGridItem.FilterSysDocID = "";
			comboBoxGridItem.HasAllAccount = false;
			comboBoxGridItem.HasCustom = false;
			comboBoxGridItem.IsDataLoaded = false;
			comboBoxGridItem.Location = new System.Drawing.Point(664, 43);
			comboBoxGridItem.MaxDropDownItems = 12;
			comboBoxGridItem.Name = "comboBoxGridItem";
			comboBoxGridItem.Show3PLItems = true;
			comboBoxGridItem.ShowInactiveItems = false;
			comboBoxGridItem.ShowQuickAdd = true;
			comboBoxGridItem.Size = new System.Drawing.Size(74, 20);
			comboBoxGridItem.TabIndex = 118;
			comboBoxGridItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridItem.Visible = false;
			ultraCalcManager1.ContainingControl = this;
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridItems.CalcManager = ultraCalcManager1;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance13;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance20;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance22;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance23;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(3, 1);
			dataGridItems.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(1077, 398);
			dataGridItems.TabIndex = 2;
			dataGridItems.Text = "dataEntryGrid1";
			dataGridItems.AfterRowInsert += new Infragistics.Win.UltraWinGrid.RowEventHandler(dataGridItems_AfterRowInsert);
			ultraTabPageControl3.Controls.Add(textBoxOtherOHP);
			ultraTabPageControl3.Controls.Add(textBoxLabourOHP);
			ultraTabPageControl3.Controls.Add(textBoxMaterialOHP);
			ultraTabPageControl3.Controls.Add(label4);
			ultraTabPageControl3.Controls.Add(label2);
			ultraTabPageControl3.Controls.Add(label1);
			ultraTabPageControl3.Controls.Add(buttonApply);
			ultraTabPageControl3.Controls.Add(textBoxC6);
			ultraTabPageControl3.Controls.Add(textBoxC5);
			ultraTabPageControl3.Controls.Add(textBoxC4);
			ultraTabPageControl3.Controls.Add(textBoxC3);
			ultraTabPageControl3.Controls.Add(LabelC6);
			ultraTabPageControl3.Controls.Add(LabelC5);
			ultraTabPageControl3.Controls.Add(LabelC4);
			ultraTabPageControl3.Controls.Add(LabelC3);
			ultraTabPageControl3.Controls.Add(textBoxC2);
			ultraTabPageControl3.Controls.Add(LabelC2);
			ultraTabPageControl3.Controls.Add(LabelC1);
			ultraTabPageControl3.Controls.Add(textBoxC1);
			ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl3.Name = "ultraTabPageControl3";
			ultraTabPageControl3.Size = new System.Drawing.Size(1080, 399);
			textBoxOtherOHP.CustomReportFieldName = "";
			textBoxOtherOHP.CustomReportKey = "";
			textBoxOtherOHP.CustomReportValueType = 1;
			textBoxOtherOHP.IsComboTextBox = false;
			textBoxOtherOHP.IsModified = false;
			textBoxOtherOHP.Location = new System.Drawing.Point(777, 21);
			textBoxOtherOHP.Name = "textBoxOtherOHP";
			textBoxOtherOHP.Size = new System.Drawing.Size(71, 20);
			textBoxOtherOHP.TabIndex = 131;
			textBoxOtherOHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxOtherOHP.Visible = false;
			textBoxLabourOHP.CustomReportFieldName = "";
			textBoxLabourOHP.CustomReportKey = "";
			textBoxLabourOHP.CustomReportValueType = 1;
			textBoxLabourOHP.IsComboTextBox = false;
			textBoxLabourOHP.IsModified = false;
			textBoxLabourOHP.Location = new System.Drawing.Point(589, 22);
			textBoxLabourOHP.Name = "textBoxLabourOHP";
			textBoxLabourOHP.Size = new System.Drawing.Size(71, 20);
			textBoxLabourOHP.TabIndex = 130;
			textBoxLabourOHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxLabourOHP.Visible = false;
			textBoxMaterialOHP.CustomReportFieldName = "";
			textBoxMaterialOHP.CustomReportKey = "";
			textBoxMaterialOHP.CustomReportValueType = 1;
			textBoxMaterialOHP.IsComboTextBox = false;
			textBoxMaterialOHP.IsModified = false;
			textBoxMaterialOHP.Location = new System.Drawing.Point(409, 23);
			textBoxMaterialOHP.Name = "textBoxMaterialOHP";
			textBoxMaterialOHP.Size = new System.Drawing.Size(71, 20);
			textBoxMaterialOHP.TabIndex = 129;
			textBoxMaterialOHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxMaterialOHP.Visible = false;
			label4.AutoSize = true;
			label4.BackColor = System.Drawing.SystemColors.Window;
			label4.Location = new System.Drawing.Point(501, 25);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(83, 13);
			label4.TabIndex = 134;
			label4.Text = "Labour OHP % :";
			label4.Visible = false;
			label2.AutoSize = true;
			label2.BackColor = System.Drawing.SystemColors.Window;
			label2.Location = new System.Drawing.Point(695, 25);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(76, 13);
			label2.TabIndex = 133;
			label2.Text = "Other OHP % :";
			label2.Visible = false;
			label1.AutoSize = true;
			label1.BackColor = System.Drawing.SystemColors.Window;
			label1.Location = new System.Drawing.Point(316, 27);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(87, 13);
			label1.TabIndex = 132;
			label1.Text = "Material OHP % :";
			label1.Visible = false;
			buttonApply.Location = new System.Drawing.Point(234, 194);
			buttonApply.Name = "buttonApply";
			buttonApply.Size = new System.Drawing.Size(75, 23);
			buttonApply.TabIndex = 20;
			buttonApply.Text = "Apply";
			buttonApply.UseVisualStyleBackColor = true;
			buttonApply.Click += new System.EventHandler(buttonApply_Click);
			textBoxC6.CustomReportFieldName = "";
			textBoxC6.CustomReportKey = "";
			textBoxC6.CustomReportValueType = 1;
			textBoxC6.IsComboTextBox = false;
			textBoxC6.IsModified = false;
			textBoxC6.Location = new System.Drawing.Point(62, 137);
			textBoxC6.Name = "textBoxC6";
			textBoxC6.Size = new System.Drawing.Size(200, 20);
			textBoxC6.TabIndex = 19;
			textBoxC5.CustomReportFieldName = "";
			textBoxC5.CustomReportKey = "";
			textBoxC5.CustomReportValueType = 1;
			textBoxC5.IsComboTextBox = false;
			textBoxC5.IsModified = false;
			textBoxC5.Location = new System.Drawing.Point(62, 110);
			textBoxC5.Name = "textBoxC5";
			textBoxC5.Size = new System.Drawing.Size(200, 20);
			textBoxC5.TabIndex = 18;
			textBoxC4.CustomReportFieldName = "";
			textBoxC4.CustomReportKey = "";
			textBoxC4.CustomReportValueType = 1;
			textBoxC4.IsComboTextBox = false;
			textBoxC4.IsModified = false;
			textBoxC4.Location = new System.Drawing.Point(62, 83);
			textBoxC4.Name = "textBoxC4";
			textBoxC4.Size = new System.Drawing.Size(200, 20);
			textBoxC4.TabIndex = 17;
			textBoxC3.CustomReportFieldName = "";
			textBoxC3.CustomReportKey = "";
			textBoxC3.CustomReportValueType = 1;
			textBoxC3.IsComboTextBox = false;
			textBoxC3.IsModified = false;
			textBoxC3.Location = new System.Drawing.Point(62, 55);
			textBoxC3.Name = "textBoxC3";
			textBoxC3.Size = new System.Drawing.Size(200, 20);
			textBoxC3.TabIndex = 16;
			LabelC6.AutoSize = true;
			LabelC6.BackColor = System.Drawing.SystemColors.Window;
			LabelC6.Location = new System.Drawing.Point(25, 141);
			LabelC6.Name = "LabelC6";
			LabelC6.Size = new System.Drawing.Size(20, 13);
			LabelC6.TabIndex = 15;
			LabelC6.Text = "C6";
			LabelC5.AutoSize = true;
			LabelC5.BackColor = System.Drawing.SystemColors.Window;
			LabelC5.Location = new System.Drawing.Point(24, 114);
			LabelC5.Name = "LabelC5";
			LabelC5.Size = new System.Drawing.Size(20, 13);
			LabelC5.TabIndex = 14;
			LabelC5.Text = "C5";
			LabelC4.AutoSize = true;
			LabelC4.BackColor = System.Drawing.SystemColors.Window;
			LabelC4.Location = new System.Drawing.Point(24, 87);
			LabelC4.Name = "LabelC4";
			LabelC4.Size = new System.Drawing.Size(20, 13);
			LabelC4.TabIndex = 13;
			LabelC4.Text = "C4";
			LabelC3.AutoSize = true;
			LabelC3.BackColor = System.Drawing.SystemColors.Window;
			LabelC3.Location = new System.Drawing.Point(24, 61);
			LabelC3.Name = "LabelC3";
			LabelC3.Size = new System.Drawing.Size(20, 13);
			LabelC3.TabIndex = 12;
			LabelC3.Text = "C3";
			textBoxC2.CustomReportFieldName = "";
			textBoxC2.CustomReportKey = "";
			textBoxC2.CustomReportValueType = 1;
			textBoxC2.IsComboTextBox = false;
			textBoxC2.IsModified = false;
			textBoxC2.Location = new System.Drawing.Point(62, 31);
			textBoxC2.Name = "textBoxC2";
			textBoxC2.Size = new System.Drawing.Size(200, 20);
			textBoxC2.TabIndex = 9;
			LabelC2.AutoSize = true;
			LabelC2.BackColor = System.Drawing.SystemColors.Window;
			LabelC2.Location = new System.Drawing.Point(24, 35);
			LabelC2.Name = "LabelC2";
			LabelC2.Size = new System.Drawing.Size(20, 13);
			LabelC2.TabIndex = 8;
			LabelC2.Text = "C2";
			LabelC1.AutoSize = true;
			LabelC1.BackColor = System.Drawing.SystemColors.Window;
			LabelC1.Location = new System.Drawing.Point(24, 12);
			LabelC1.Name = "LabelC1";
			LabelC1.Size = new System.Drawing.Size(20, 13);
			LabelC1.TabIndex = 0;
			LabelC1.Text = "C1";
			textBoxC1.CustomReportFieldName = "";
			textBoxC1.CustomReportKey = "";
			textBoxC1.CustomReportValueType = 1;
			textBoxC1.IsComboTextBox = false;
			textBoxC1.IsModified = false;
			textBoxC1.Location = new System.Drawing.Point(62, 9);
			textBoxC1.Name = "textBoxC1";
			textBoxC1.Size = new System.Drawing.Size(200, 20);
			textBoxC1.TabIndex = 6;
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[16]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator4,
				toolStripButtonAttach,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator3,
				toolStripButtonInformation,
				toolStripDropDownButton1
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(1098, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First Record";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous Record";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next Record";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last Record";
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
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripTextBoxFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxFind_KeyPress);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Click += new System.EventHandler(toolStripButtonPrint_Click);
			toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPreview.Image = Micromind.ClientUI.Properties.Resources.preview;
			toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPreview.Name = "toolStripButtonPreview";
			toolStripButtonPreview.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPreview.Text = "Preview";
			toolStripButtonPreview.ToolTipText = "Preview";
			toolStripButtonPreview.Click += new System.EventHandler(toolStripButtonPreview_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2]
			{
				copyToolStripMenuItem,
				reviseToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			toolStripDropDownButton1.DropDownOpening += new System.EventHandler(toolStripDropDownButton1_DropDownOpening);
			copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			copyToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			copyToolStripMenuItem.Text = "Copy";
			copyToolStripMenuItem.Click += new System.EventHandler(copyToolStripMenuItem_Click);
			reviseToolStripMenuItem.Name = "reviseToolStripMenuItem";
			reviseToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			reviseToolStripMenuItem.Text = "Revise";
			reviseToolStripMenuItem.ToolTipText = "Revise";
			reviseToolStripMenuItem.Click += new System.EventHandler(reviseToolStripMenuItem_Click);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 580);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(1098, 40);
			panelButtons.TabIndex = 2;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(328, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(96, 24);
			buttonVoid.TabIndex = 2;
			buttonVoid.Text = "&Void";
			buttonVoid.UseVisualStyleBackColor = false;
			buttonVoid.Click += new System.EventHandler(buttonVoid_Click);
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 3;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
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
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(1098, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(988, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 4;
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
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(580, 3);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(92, 20);
			dateTimePickerDate.TabIndex = 3;
			textBoxVoucherNumber.Location = new System.Drawing.Point(353, 4);
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(139, 20);
			textBoxVoucherNumber.TabIndex = 2;
			textBoxNote.Location = new System.Drawing.Point(91, 81);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(727, 20);
			textBoxNote.TabIndex = 9;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(15, 84);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance25.FontData.BoldAsString = "True";
			appearance25.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance25;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(246, 8);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel2.TabIndex = 2;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance26.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance26;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			panelDetails.Controls.Add(panelReviseDate);
			panelDetails.Controls.Add(label5);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(ultraFormattedLinkLabel3);
			panelDetails.Controls.Add(textBoxCostCategory);
			panelDetails.Controls.Add(ComboBoxHeaderCostCategory);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(textBoxJob);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(comboBoxJob);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Location = new System.Drawing.Point(9, 35);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(1083, 112);
			panelDetails.TabIndex = 0;
			panelReviseDate.Controls.Add(comboBoxRevisionNo);
			panelReviseDate.Controls.Add(label6);
			panelReviseDate.Controls.Add(textBoxLastReviseDate);
			panelReviseDate.Controls.Add(label11);
			panelReviseDate.Location = new System.Drawing.Point(796, 3);
			panelReviseDate.Name = "panelReviseDate";
			panelReviseDate.Size = new System.Drawing.Size(278, 50);
			panelReviseDate.TabIndex = 162;
			panelReviseDate.Visible = false;
			comboBoxRevisionNo.FormattingEnabled = true;
			comboBoxRevisionNo.Location = new System.Drawing.Point(126, 25);
			comboBoxRevisionNo.Name = "comboBoxRevisionNo";
			comboBoxRevisionNo.Size = new System.Drawing.Size(87, 21);
			comboBoxRevisionNo.TabIndex = 67;
			comboBoxRevisionNo.SelectedIndexChanged += new System.EventHandler(comboBoxRevisionNo_SelectedIndexChanged);
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(38, 28);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(66, 13);
			label6.TabIndex = 66;
			label6.Text = "Revised No:";
			textBoxLastReviseDate.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLastReviseDate.CustomReportFieldName = "";
			textBoxLastReviseDate.CustomReportKey = "";
			textBoxLastReviseDate.CustomReportValueType = 1;
			textBoxLastReviseDate.ForeColor = System.Drawing.Color.Black;
			textBoxLastReviseDate.IsComboTextBox = false;
			textBoxLastReviseDate.IsModified = false;
			textBoxLastReviseDate.Location = new System.Drawing.Point(126, 3);
			textBoxLastReviseDate.MaxLength = 255;
			textBoxLastReviseDate.Name = "textBoxLastReviseDate";
			textBoxLastReviseDate.ReadOnly = true;
			textBoxLastReviseDate.Size = new System.Drawing.Size(142, 20);
			textBoxLastReviseDate.TabIndex = 7;
			textBoxLastReviseDate.TabStop = false;
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(6, 6);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(98, 13);
			label11.TabIndex = 65;
			label11.Text = "Last Revised Date:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(452, 60);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(60, 13);
			label5.TabIndex = 161;
			label5.Text = "Reference:";
			textBoxRef1.Location = new System.Drawing.Point(519, 57);
			textBoxRef1.MaxLength = 20;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(153, 20);
			textBoxRef1.TabIndex = 160;
			appearance27.FontData.BoldAsString = "True";
			appearance27.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance27;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(3, 59);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(86, 15);
			ultraFormattedLinkLabel3.TabIndex = 159;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Cost Category:";
			appearance28.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance28;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			textBoxCostCategory.Location = new System.Drawing.Point(222, 56);
			textBoxCostCategory.MaxLength = 255;
			textBoxCostCategory.Name = "textBoxCostCategory";
			textBoxCostCategory.ReadOnly = true;
			textBoxCostCategory.Size = new System.Drawing.Size(174, 20);
			textBoxCostCategory.TabIndex = 158;
			ComboBoxHeaderCostCategory.Assigned = false;
			ComboBoxHeaderCostCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxHeaderCostCategory.CalcManager = ultraCalcManager1;
			ComboBoxHeaderCostCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxHeaderCostCategory.CustomReportFieldName = "";
			ComboBoxHeaderCostCategory.CustomReportKey = "";
			ComboBoxHeaderCostCategory.CustomReportValueType = 1;
			ComboBoxHeaderCostCategory.DescriptionTextBox = textBoxCostCategory;
			ComboBoxHeaderCostCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxHeaderCostCategory.Editable = true;
			ComboBoxHeaderCostCategory.FilterString = "";
			ComboBoxHeaderCostCategory.HasAllAccount = false;
			ComboBoxHeaderCostCategory.HasCustom = false;
			ComboBoxHeaderCostCategory.IsDataLoaded = false;
			ComboBoxHeaderCostCategory.Location = new System.Drawing.Point(91, 56);
			ComboBoxHeaderCostCategory.MaxDropDownItems = 12;
			ComboBoxHeaderCostCategory.Name = "ComboBoxHeaderCostCategory";
			ComboBoxHeaderCostCategory.ShowInactiveItems = false;
			ComboBoxHeaderCostCategory.ShowQuickAdd = true;
			ComboBoxHeaderCostCategory.Size = new System.Drawing.Size(125, 20);
			ComboBoxHeaderCostCategory.TabIndex = 157;
			ComboBoxHeaderCostCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSysDoc.AlwaysInEditMode = true;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CalcManager = ultraCalcManager1;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance29;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance30;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance32.BackColor2 = System.Drawing.SystemColors.Control;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance33;
			appearance34.BackColor = System.Drawing.SystemColors.Highlight;
			appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance34;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance35;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance36;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance37.BackColor = System.Drawing.SystemColors.Control;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance37;
			appearance38.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance38;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance39;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
			comboBoxSysDoc.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSysDoc.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSysDoc.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSysDoc.DivisionID = "";
			comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDoc.Editable = true;
			comboBoxSysDoc.ExcludeFromSecurity = false;
			comboBoxSysDoc.FilterString = "";
			comboBoxSysDoc.HasAllAccount = false;
			comboBoxSysDoc.HasCustom = false;
			comboBoxSysDoc.IsDataLoaded = false;
			comboBoxSysDoc.Location = new System.Drawing.Point(91, 4);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(109, 20);
			comboBoxSysDoc.TabIndex = 1;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxJob.Location = new System.Drawing.Point(222, 31);
			textBoxJob.MaxLength = 255;
			textBoxJob.Name = "textBoxJob";
			textBoxJob.ReadOnly = true;
			textBoxJob.Size = new System.Drawing.Size(450, 20);
			textBoxJob.TabIndex = 8;
			appearance41.FontData.BoldAsString = "True";
			appearance41.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance41;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(12, 34);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(48, 15);
			ultraFormattedLinkLabel1.TabIndex = 131;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Project:";
			appearance42.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance42;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			appearance43.FontData.BoldAsString = "True";
			appearance43.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance43;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(13, 7);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 0;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance44.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance44;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxJob.Assigned = false;
			comboBoxJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxJob.CalcManager = ultraCalcManager1;
			comboBoxJob.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxJob.CustomReportFieldName = "";
			comboBoxJob.CustomReportKey = "";
			comboBoxJob.CustomReportValueType = 1;
			comboBoxJob.DescriptionTextBox = textBoxJob;
			comboBoxJob.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxJob.Editable = true;
			comboBoxJob.FilterString = "";
			comboBoxJob.HasAllAccount = false;
			comboBoxJob.HasCustom = false;
			comboBoxJob.IsDataLoaded = false;
			comboBoxJob.Location = new System.Drawing.Point(91, 31);
			comboBoxJob.MaxDropDownItems = 12;
			comboBoxJob.Name = "comboBoxJob";
			comboBoxJob.ShowInactiveItems = false;
			comboBoxJob.ShowQuickAdd = true;
			comboBoxJob.Size = new System.Drawing.Size(125, 20);
			comboBoxJob.TabIndex = 7;
			comboBoxJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(536, 6);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Date:";
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				printListToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(130, 26);
			printListToolStripMenuItem.Name = "printListToolStripMenuItem";
			printListToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
			printListToolStripMenuItem.Text = "Print List...";
			ultraGridPrintDocument1.DocumentName = "Inventory Adjustment - List";
			ultraTabControl2.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl2.Controls.Add(ultraTabSharedControlsPage2);
			ultraTabControl2.Controls.Add(tabPageExpense);
			ultraTabControl2.Controls.Add(ultraTabPageControl3);
			ultraTabControl2.Location = new System.Drawing.Point(11, 149);
			ultraTabControl2.Name = "ultraTabControl2";
			ultraTabControl2.SharedControlsPage = ultraTabSharedControlsPage2;
			ultraTabControl2.Size = new System.Drawing.Size(1084, 425);
			ultraTabControl2.TabIndex = 133;
			ultraTab.TabPage = tabPageExpense;
			ultraTab.Text = "Estimation Details";
			ultraTab2.TabPage = ultraTabPageControl3;
			ultraTab2.Text = "Settings";
			ultraTabControl2.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabSharedControlsPage2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage2.Name = "ultraTabSharedControlsPage2";
			ultraTabSharedControlsPage2.Size = new System.Drawing.Size(1080, 399);
			ComboBoxJobBOM.Assigned = false;
			ComboBoxJobBOM.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxJobBOM.CalcManager = ultraCalcManager1;
			ComboBoxJobBOM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxJobBOM.CustomReportFieldName = "";
			ComboBoxJobBOM.CustomReportKey = "";
			ComboBoxJobBOM.CustomReportValueType = 1;
			ComboBoxJobBOM.DescriptionTextBox = null;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxJobBOM.DisplayLayout.Appearance = appearance45;
			ComboBoxJobBOM.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxJobBOM.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance46.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance46.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance46.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxJobBOM.DisplayLayout.GroupByBox.Appearance = appearance46;
			appearance47.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxJobBOM.DisplayLayout.GroupByBox.BandLabelAppearance = appearance47;
			ComboBoxJobBOM.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance48.BackColor2 = System.Drawing.SystemColors.Control;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance48.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxJobBOM.DisplayLayout.GroupByBox.PromptAppearance = appearance48;
			ComboBoxJobBOM.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxJobBOM.DisplayLayout.MaxRowScrollRegions = 1;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxJobBOM.DisplayLayout.Override.ActiveCellAppearance = appearance49;
			appearance50.BackColor = System.Drawing.SystemColors.Highlight;
			appearance50.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxJobBOM.DisplayLayout.Override.ActiveRowAppearance = appearance50;
			ComboBoxJobBOM.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxJobBOM.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxJobBOM.DisplayLayout.Override.CardAreaAppearance = appearance51;
			appearance52.BorderColor = System.Drawing.Color.Silver;
			appearance52.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxJobBOM.DisplayLayout.Override.CellAppearance = appearance52;
			ComboBoxJobBOM.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxJobBOM.DisplayLayout.Override.CellPadding = 0;
			appearance53.BackColor = System.Drawing.SystemColors.Control;
			appearance53.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance53.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance53.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance53.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxJobBOM.DisplayLayout.Override.GroupByRowAppearance = appearance53;
			appearance54.TextHAlignAsString = "Left";
			ComboBoxJobBOM.DisplayLayout.Override.HeaderAppearance = appearance54;
			ComboBoxJobBOM.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxJobBOM.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.Color.Silver;
			ComboBoxJobBOM.DisplayLayout.Override.RowAppearance = appearance55;
			ComboBoxJobBOM.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxJobBOM.DisplayLayout.Override.TemplateAddRowAppearance = appearance56;
			ComboBoxJobBOM.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxJobBOM.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxJobBOM.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxJobBOM.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxJobBOM.Editable = true;
			ComboBoxJobBOM.FilterString = "";
			ComboBoxJobBOM.HasAllAccount = false;
			ComboBoxJobBOM.HasCustom = false;
			ComboBoxJobBOM.IsDataLoaded = false;
			ComboBoxJobBOM.Location = new System.Drawing.Point(269, 199);
			ComboBoxJobBOM.MaxDropDownItems = 12;
			ComboBoxJobBOM.Name = "ComboBoxJobBOM";
			ComboBoxJobBOM.ShowInactiveItems = false;
			ComboBoxJobBOM.ShowQuickAdd = true;
			ComboBoxJobBOM.Size = new System.Drawing.Size(100, 20);
			ComboBoxJobBOM.TabIndex = 127;
			ComboBoxJobBOM.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxJobBOM.Visible = false;
			ComboBoxFee.Assigned = false;
			ComboBoxFee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxFee.CalcManager = ultraCalcManager1;
			ComboBoxFee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxFee.CustomReportFieldName = "";
			ComboBoxFee.CustomReportKey = "";
			ComboBoxFee.CustomReportValueType = 1;
			ComboBoxFee.DescriptionTextBox = null;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxFee.DisplayLayout.Appearance = appearance57;
			ComboBoxFee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxFee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance58.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance58.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance58.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance58.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxFee.DisplayLayout.GroupByBox.Appearance = appearance58;
			appearance59.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxFee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance59;
			ComboBoxFee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance60.BackColor2 = System.Drawing.SystemColors.Control;
			appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance60.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxFee.DisplayLayout.GroupByBox.PromptAppearance = appearance60;
			ComboBoxFee.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxFee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxFee.DisplayLayout.Override.ActiveCellAppearance = appearance61;
			appearance62.BackColor = System.Drawing.SystemColors.Highlight;
			appearance62.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxFee.DisplayLayout.Override.ActiveRowAppearance = appearance62;
			ComboBoxFee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxFee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxFee.DisplayLayout.Override.CardAreaAppearance = appearance63;
			appearance64.BorderColor = System.Drawing.Color.Silver;
			appearance64.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxFee.DisplayLayout.Override.CellAppearance = appearance64;
			ComboBoxFee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxFee.DisplayLayout.Override.CellPadding = 0;
			appearance65.BackColor = System.Drawing.SystemColors.Control;
			appearance65.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance65.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance65.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance65.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxFee.DisplayLayout.Override.GroupByRowAppearance = appearance65;
			appearance66.TextHAlignAsString = "Left";
			ComboBoxFee.DisplayLayout.Override.HeaderAppearance = appearance66;
			ComboBoxFee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxFee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.BorderColor = System.Drawing.Color.Silver;
			ComboBoxFee.DisplayLayout.Override.RowAppearance = appearance67;
			ComboBoxFee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance68.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxFee.DisplayLayout.Override.TemplateAddRowAppearance = appearance68;
			ComboBoxFee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxFee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxFee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxFee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxFee.Editable = true;
			ComboBoxFee.FilteredJobID = null;
			ComboBoxFee.FilterString = "";
			ComboBoxFee.HasAllAccount = false;
			ComboBoxFee.HasCustom = false;
			ComboBoxFee.IsDataLoaded = false;
			ComboBoxFee.Location = new System.Drawing.Point(464, 211);
			ComboBoxFee.MaxDropDownItems = 12;
			ComboBoxFee.Name = "ComboBoxFee";
			ComboBoxFee.ShowInactiveItems = false;
			ComboBoxFee.ShowQuickAdd = true;
			ComboBoxFee.Size = new System.Drawing.Size(100, 20);
			ComboBoxFee.TabIndex = 126;
			ComboBoxFee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxFee.Visible = false;
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
			comboBoxLocation.AlwaysInEditMode = true;
			comboBoxLocation.Assigned = false;
			comboBoxLocation.CalcManager = ultraCalcManager1;
			comboBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLocation.CustomReportFieldName = "";
			comboBoxLocation.CustomReportKey = "";
			comboBoxLocation.CustomReportValueType = 1;
			comboBoxLocation.DescriptionTextBox = null;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLocation.DisplayLayout.Appearance = appearance69;
			comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance70.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance70.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance70.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance70.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance70;
			appearance71.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance71;
			comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance72.BackColor2 = System.Drawing.SystemColors.Control;
			appearance72.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance72.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance72;
			comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance73;
			appearance74.BackColor = System.Drawing.SystemColors.Highlight;
			appearance74.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance74;
			comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance75;
			appearance76.BorderColor = System.Drawing.Color.Silver;
			appearance76.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance76;
			comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
			appearance77.BackColor = System.Drawing.SystemColors.Control;
			appearance77.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance77.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance77.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance77.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance77;
			appearance78.TextHAlignAsString = "Left";
			comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance78;
			comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.BorderColor = System.Drawing.Color.Silver;
			comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance79;
			comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance80.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance80;
			comboBoxLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLocation.Editable = true;
			comboBoxLocation.FilterString = "";
			comboBoxLocation.HasAllAccount = false;
			comboBoxLocation.HasCustom = false;
			comboBoxLocation.IsDataLoaded = false;
			comboBoxLocation.Location = new System.Drawing.Point(601, 225);
			comboBoxLocation.MaxDropDownItems = 12;
			comboBoxLocation.Name = "comboBoxLocation";
			comboBoxLocation.ShowAll = false;
			comboBoxLocation.ShowConsignIn = false;
			comboBoxLocation.ShowConsignOut = false;
			comboBoxLocation.ShowDefaultLocationOnly = false;
			comboBoxLocation.ShowInactiveItems = false;
			comboBoxLocation.ShowNormalLocations = true;
			comboBoxLocation.ShowPOSOnly = false;
			comboBoxLocation.ShowQuickAdd = true;
			comboBoxLocation.ShowWarehouseOnly = false;
			comboBoxLocation.Size = new System.Drawing.Size(139, 20);
			comboBoxLocation.TabIndex = 5;
			comboBoxLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxLocation.Visible = false;
			comboBoxGridLocation.AlwaysInEditMode = true;
			comboBoxGridLocation.Assigned = false;
			comboBoxGridLocation.CalcManager = ultraCalcManager1;
			comboBoxGridLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridLocation.CustomReportFieldName = "";
			comboBoxGridLocation.CustomReportKey = "";
			comboBoxGridLocation.CustomReportValueType = 1;
			comboBoxGridLocation.DescriptionTextBox = null;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			appearance81.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridLocation.DisplayLayout.Appearance = appearance81;
			comboBoxGridLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance82.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance82.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance82.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.GroupByBox.Appearance = appearance82;
			appearance83.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance83;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance84.BackColor2 = System.Drawing.SystemColors.Control;
			appearance84.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance84.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance84;
			comboBoxGridLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveCellAppearance = appearance85;
			appearance86.BackColor = System.Drawing.SystemColors.Highlight;
			appearance86.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveRowAppearance = appearance86;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.CardAreaAppearance = appearance87;
			appearance88.BorderColor = System.Drawing.Color.Silver;
			appearance88.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridLocation.DisplayLayout.Override.CellAppearance = appearance88;
			comboBoxGridLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridLocation.DisplayLayout.Override.CellPadding = 0;
			appearance89.BackColor = System.Drawing.SystemColors.Control;
			appearance89.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance89.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance89.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance89.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.GroupByRowAppearance = appearance89;
			appearance90.TextHAlignAsString = "Left";
			comboBoxGridLocation.DisplayLayout.Override.HeaderAppearance = appearance90;
			comboBoxGridLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridLocation.DisplayLayout.Override.RowAppearance = appearance91;
			comboBoxGridLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance92.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance92;
			comboBoxGridLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridLocation.Editable = true;
			comboBoxGridLocation.FilterString = "";
			comboBoxGridLocation.HasAllAccount = false;
			comboBoxGridLocation.HasCustom = false;
			comboBoxGridLocation.IsDataLoaded = false;
			comboBoxGridLocation.Location = new System.Drawing.Point(626, 251);
			comboBoxGridLocation.MaxDropDownItems = 12;
			comboBoxGridLocation.Name = "comboBoxGridLocation";
			comboBoxGridLocation.ShowAll = false;
			comboBoxGridLocation.ShowConsignIn = false;
			comboBoxGridLocation.ShowConsignOut = false;
			comboBoxGridLocation.ShowDefaultLocationOnly = false;
			comboBoxGridLocation.ShowInactiveItems = false;
			comboBoxGridLocation.ShowNormalLocations = true;
			comboBoxGridLocation.ShowPOSOnly = false;
			comboBoxGridLocation.ShowQuickAdd = true;
			comboBoxGridLocation.ShowWarehouseOnly = false;
			comboBoxGridLocation.Size = new System.Drawing.Size(114, 20);
			comboBoxGridLocation.TabIndex = 123;
			comboBoxGridLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridLocation.Visible = false;
			comboBoxCostCategory.Assigned = false;
			comboBoxCostCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCostCategory.CalcManager = ultraCalcManager1;
			comboBoxCostCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCostCategory.CustomReportFieldName = "";
			comboBoxCostCategory.CustomReportKey = "";
			comboBoxCostCategory.CustomReportValueType = 1;
			comboBoxCostCategory.DescriptionTextBox = null;
			comboBoxCostCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCostCategory.Editable = true;
			comboBoxCostCategory.FilterString = "";
			comboBoxCostCategory.HasAllAccount = false;
			comboBoxCostCategory.HasCustom = false;
			comboBoxCostCategory.IsDataLoaded = false;
			comboBoxCostCategory.Location = new System.Drawing.Point(640, 199);
			comboBoxCostCategory.MaxDropDownItems = 12;
			comboBoxCostCategory.Name = "comboBoxCostCategory";
			comboBoxCostCategory.ShowInactiveItems = false;
			comboBoxCostCategory.ShowQuickAdd = true;
			comboBoxCostCategory.Size = new System.Drawing.Size(100, 20);
			comboBoxCostCategory.TabIndex = 125;
			comboBoxCostCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCostCategory.Visible = false;
			comboBoxGridProductUnit.AlwaysInEditMode = true;
			comboBoxGridProductUnit.Assigned = false;
			comboBoxGridProductUnit.CalcManager = ultraCalcManager1;
			comboBoxGridProductUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridProductUnit.CustomReportFieldName = "";
			comboBoxGridProductUnit.CustomReportKey = "";
			comboBoxGridProductUnit.CustomReportValueType = 1;
			comboBoxGridProductUnit.DescriptionTextBox = null;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridProductUnit.DisplayLayout.Appearance = appearance93;
			comboBoxGridProductUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridProductUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance94.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance94.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance94.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.Appearance = appearance94;
			appearance95.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance95;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance96.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance96.BackColor2 = System.Drawing.SystemColors.Control;
			appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance96.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance96;
			comboBoxGridProductUnit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridProductUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridProductUnit.DisplayLayout.Override.ActiveCellAppearance = appearance97;
			appearance98.BackColor = System.Drawing.SystemColors.Highlight;
			appearance98.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridProductUnit.DisplayLayout.Override.ActiveRowAppearance = appearance98;
			comboBoxGridProductUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridProductUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.Override.CardAreaAppearance = appearance99;
			appearance100.BorderColor = System.Drawing.Color.Silver;
			appearance100.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridProductUnit.DisplayLayout.Override.CellAppearance = appearance100;
			comboBoxGridProductUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridProductUnit.DisplayLayout.Override.CellPadding = 0;
			appearance101.BackColor = System.Drawing.SystemColors.Control;
			appearance101.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance101.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance101.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance101.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.Override.GroupByRowAppearance = appearance101;
			appearance102.TextHAlignAsString = "Left";
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderAppearance = appearance102;
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			appearance103.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridProductUnit.DisplayLayout.Override.RowAppearance = appearance103;
			comboBoxGridProductUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance104.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridProductUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance104;
			comboBoxGridProductUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridProductUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridProductUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridProductUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridProductUnit.Editable = true;
			comboBoxGridProductUnit.FilterString = "";
			comboBoxGridProductUnit.IsDataLoaded = false;
			comboBoxGridProductUnit.Location = new System.Drawing.Point(676, 74);
			comboBoxGridProductUnit.MaxDropDownItems = 12;
			comboBoxGridProductUnit.Name = "comboBoxGridProductUnit";
			comboBoxGridProductUnit.Size = new System.Drawing.Size(74, 20);
			comboBoxGridProductUnit.TabIndex = 120;
			comboBoxGridProductUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridProductUnit.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(1098, 620);
			base.Controls.Add(ultraTabControl2);
			base.Controls.Add(ComboBoxJobBOM);
			base.Controls.Add(ComboBoxFee);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(comboBoxLocation);
			base.Controls.Add(comboBoxGridLocation);
			base.Controls.Add(comboBoxCostCategory);
			base.Controls.Add(comboBoxGridProductUnit);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(649, 366);
			base.Name = "JobEstimationDetailsForm";
			Text = "Estimation";
			tabPageExpense.ResumeLayout(false);
			tabPageExpense.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ultraTabPageControl3.ResumeLayout(false);
			ultraTabPageControl3.PerformLayout();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			panelReviseDate.ResumeLayout(false);
			panelReviseDate.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ComboBoxHeaderCostCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl2).EndInit();
			ultraTabControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ComboBoxJobBOM).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxFee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridProductUnit).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
