using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class SelectFeesDialog : Form
	{
		public bool CanClose = true;

		private bool isMultiSelect;

		private List<string> hiddenColumns = new List<string>();

		public DataTable SelectedItemsTable;

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Line linePanelDown;

		private XPButton buttonCancel;

		private DataEntryGrid dataGridItems;

		private Panel panelDetails;

		private TextBox textBoxProjectCode;

		private Label label2;

		private TextBox textBoxProjectName;

		private TextBox textBoxEntityCode;

		private Label label4;

		private TextBox textBoxEntityName;

		public bool IsMultiSelect
		{
			get
			{
				return isMultiSelect;
			}
			set
			{
				isMultiSelect = value;
				if (value && dataGridItems.DataSource != null)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns.Insert(0, "C");
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].DataType = typeof(bool);
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellActivation = Activation.AllowEdit;
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellClickAction = CellClickAction.Edit;
				}
			}
		}

		public string ProjectID
		{
			get
			{
				return textBoxProjectCode.Text;
			}
			set
			{
				textBoxProjectCode.Text = value;
			}
		}

		public string ProjectName
		{
			get
			{
				return textBoxProjectName.Text;
			}
			set
			{
				textBoxProjectName.Text = value;
			}
		}

		public string CustomerID
		{
			get
			{
				return textBoxEntityCode.Text;
			}
			set
			{
				textBoxEntityCode.Text = value;
			}
		}

		public string CustomerName
		{
			get
			{
				return textBoxEntityName.Text;
			}
			set
			{
				textBoxEntityName.Text = value;
			}
		}

		public UltraGridRow SelectedRow
		{
			get
			{
				if (base.DialogResult == DialogResult.Cancel)
				{
					return null;
				}
				if (dataGridItems.ActiveRow != null)
				{
					return dataGridItems.ActiveRow;
				}
				return null;
			}
		}

		public List<UltraGridRow> SelectedRows
		{
			get
			{
				if (base.DialogResult == DialogResult.Cancel)
				{
					return null;
				}
				List<UltraGridRow> list = new List<UltraGridRow>();
				if (IsMultiSelect)
				{
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						if (row.Cells["BillAmount"].Value.ToString() != "" && decimal.Parse(row.Cells["BillAmount"].Value.ToString()) > 0m)
						{
							list.Add(row);
						}
					}
					return list;
				}
				return list;
			}
		}

		public UltraGrid Grid => dataGridItems;

		public DataSet DataSource
		{
			get
			{
				return (DataSet)dataGridItems.DataSource;
			}
			set
			{
				if (value != null && value.Tables.Count > 0)
				{
					DataTable dataTable = value.Tables[0];
					dataTable.Columns.Add("SearchColumn");
					string text = "";
					foreach (DataRow row in dataTable.Rows)
					{
						text = "";
						foreach (DataColumn column in dataTable.Columns)
						{
							if (!(column.ColumnName == "SearchColumn"))
							{
								text = text + row[column].ToString() + " ";
							}
						}
						row["SearchColumn"] = text;
					}
				}
				dataGridItems.DataSource = value;
				dataGridItems.DisplayLayout.Bands[0].Columns["SearchColumn"].Hidden = true;
				dataGridItems.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
				if (IsMultiSelect && !dataGridItems.DisplayLayout.Bands[0].Columns.Exists("C"))
				{
					UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns.Insert(0, "C");
					ultraGridColumn.DataType = typeof(bool);
					ultraGridColumn.CellActivation = Activation.AllowEdit;
					ultraGridColumn.CellClickAction = CellClickAction.Edit;
					ultraGridColumn.CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
					ultraGridColumn.Width = 18;
					ultraGridColumn.MinWidth = 18;
					ultraGridColumn.MaxWidth = 18;
					ultraGridColumn.LockedWidth = true;
					ultraGridColumn.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
				}
				foreach (string hiddenColumn in hiddenColumns)
				{
					if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists(hiddenColumn))
					{
						dataGridItems.DisplayLayout.Bands[0].Columns[hiddenColumn].Hidden = true;
					}
				}
			}
		}

		public List<string> HiddenColumns => hiddenColumns;

		public event EventHandler ValidateSelection;

		public SelectFeesDialog()
		{
			InitializeComponent();
			base.Load += SelectFeesDialog_Load;
			base.Activated += SelectFeesDialog_Activated;
			base.FormClosing += SelectFeesDialog_FormClosing;
		}

		private void SelectFeesDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				Global.GlobalSettings.SaveFormProperties(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SelectFeesDialog_Activated(object sender, EventArgs e)
		{
		}

		private void SetupGrid()
		{
			try
			{
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("FeeID");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("FeeAmount", typeof(decimal));
				dataTable.Columns.Add("Billed", typeof(decimal));
				dataTable.Columns.Add("Due", typeof(decimal));
				dataTable.Columns.Add("BillAmount", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeID"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeID"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeID"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeID"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeID"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeID"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["BillAmount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["BillAmount"].Format = "#,0.00##";
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeAmount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeAmount"].Format = "#,0.00##";
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeAmount"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeAmount"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeAmount"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeAmount"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeAmount"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["Billed"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Billed"].Format = "#,0.00##";
				dataGridItems.DisplayLayout.Bands[0].Columns["Billed"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Billed"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Billed"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Billed"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Billed"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["Billed"].Header.Caption = "Billed to Date";
				dataGridItems.DisplayLayout.Bands[0].Columns["Due"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Due"].Format = "#,0.00##";
				dataGridItems.DisplayLayout.Bands[0].Columns["Due"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Due"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Due"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Due"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Due"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["FeeID"].Width = checked(18 * dataGridItems.Width) / 100;
				SummarySettings summarySettings = dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalFee", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["FeeAmount"], SummaryPosition.UseSummaryPositionColumn);
				summarySettings.Appearance.TextHAlign = HAlign.Right;
				summarySettings.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				summarySettings.DisplayFormat = "{0:n}";
				summarySettings.Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				SummarySettings summarySettings2 = dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalDue", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Due"], SummaryPosition.UseSummaryPositionColumn);
				summarySettings2.Appearance.TextHAlign = HAlign.Right;
				summarySettings2.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				summarySettings2.DisplayFormat = "{0:n}";
				summarySettings2.Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				SummarySettings summarySettings3 = dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalBilled", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Billed"], SummaryPosition.UseSummaryPositionColumn);
				summarySettings3.Appearance.TextHAlign = HAlign.Right;
				summarySettings3.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				summarySettings3.DisplayFormat = "{0:n}";
				summarySettings3.Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				SummarySettings summarySettings4 = dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalAmount", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["BillAmount"], SummaryPosition.UseSummaryPositionColumn);
				summarySettings4.Appearance.TextHAlign = HAlign.Right;
				summarySettings4.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				summarySettings4.DisplayFormat = "{0:n}";
				summarySettings4.Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Override.SummaryFooterAppearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.SetupUI();
				dataGridItems.AllowAddNew = false;
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void comboBoxPayeeTaxGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
		{
			checked
			{
				if (e.KeyCode == Keys.Down)
				{
					if (dataGridItems.ActiveRow != null)
					{
						int visibleIndex = dataGridItems.ActiveRow.VisibleIndex;
						if (visibleIndex < dataGridItems.Rows.VisibleRowCount)
						{
							dataGridItems.ActiveRow = dataGridItems.Rows.GetRowAtVisibleIndex(visibleIndex + 1);
						}
						e.SuppressKeyPress = true;
					}
				}
				else if (e.KeyCode == Keys.Up && dataGridItems.ActiveRow != null)
				{
					int visibleIndex2 = dataGridItems.ActiveRow.VisibleIndex;
					if (visibleIndex2 > 0)
					{
						dataGridItems.ActiveRow = dataGridItems.Rows.GetRowAtVisibleIndex(visibleIndex2 - 1);
					}
					e.SuppressKeyPress = true;
				}
			}
		}

		private void SelectFeesDialog_Load(object sender, EventArgs e)
		{
			try
			{
				Global.GlobalSettings.LoadFormProperties(this);
				SetupGrid();
				dataGridItems.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
				LoadData();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public void LoadData()
		{
			DataSet projectFeesByID = Factory.JobSystem.GetProjectFeesByID(ProjectID);
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			dataTable.Rows.Clear();
			foreach (DataRow row in projectFeesByID.Tables["Job_Fee_Detail"].Rows)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["FeeID"] = row["FeeID"];
				dataRow2["Description"] = row["Description"];
				dataRow2["FeeAmount"] = row["Amount"];
				dataRow2["Billed"] = row["Billed"];
				dataRow2["Due"] = row["Due"];
				dataRow2["BillAmount"] = 0;
				if (SelectedItemsTable != null && SelectedItemsTable.Rows.Count > 0)
				{
					DataRow[] array = SelectedItemsTable.Select("ItemType = 1 AND ItemCode = '" + row["FeeID"].ToString() + "'");
					if (array.Length != 0)
					{
						dataRow2["BillAmount"] = array[0]["Amount"].ToString();
					}
				}
				dataTable.Rows.Add(dataRow2);
			}
		}

		public string GetSelectedCode(string codeColumnName)
		{
			if (SelectedRow != null)
			{
				return SelectedRow.Cells[codeColumnName].Value.ToString();
			}
			return "";
		}

		private void textBoxSearch_TextChanged(object sender, EventArgs e)
		{
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			CanClose = true;
			if (CanClose)
			{
				Close();
			}
		}

		private void dataGridItems_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
		{
			if (!isMultiSelect || dataGridItems.Rows.Count <= 1)
			{
				SelectItem();
			}
		}

		private void SelectItem()
		{
			CanClose = true;
			if (dataGridItems.Rows.VisibleRowCount == 0 || dataGridItems.ActiveRow == null)
			{
				ErrorHelper.InformationMessage(UIMessages.SelectAnItemFirst);
				return;
			}
			if (this.ValidateSelection != null)
			{
				this.ValidateSelection(this, null);
			}
			if (CanClose && dataGridItems.ActiveRow != null)
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			dataGridItems.ExitEditModeOnLeave = true;
			dataGridItems.PerformAction(UltraGridAction.ExitEditMode);
			SelectItem();
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
			panelButtons = new System.Windows.Forms.Panel();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			panelDetails = new System.Windows.Forms.Panel();
			textBoxProjectCode = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			textBoxProjectName = new System.Windows.Forms.TextBox();
			textBoxEntityCode = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			textBoxEntityName = new System.Windows.Forms.TextBox();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			panelDetails.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 383);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(725, 40);
			panelButtons.TabIndex = 3;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(513, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 3;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(725, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(615, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 4;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
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
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(12, 72);
			dataGridItems.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(699, 304);
			dataGridItems.TabIndex = 6;
			dataGridItems.Text = "dataEntryGrid1";
			panelDetails.Controls.Add(textBoxProjectCode);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(textBoxProjectName);
			panelDetails.Controls.Add(textBoxEntityCode);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(textBoxEntityName);
			panelDetails.Location = new System.Drawing.Point(0, 1);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(711, 65);
			panelDetails.TabIndex = 7;
			textBoxProjectCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProjectCode.Location = new System.Drawing.Point(102, 33);
			textBoxProjectCode.MaxLength = 64;
			textBoxProjectCode.Name = "textBoxProjectCode";
			textBoxProjectCode.ReadOnly = true;
			textBoxProjectCode.Size = new System.Drawing.Size(105, 20);
			textBoxProjectCode.TabIndex = 148;
			textBoxProjectCode.TabStop = false;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(13, 36);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(43, 13);
			label2.TabIndex = 147;
			label2.Text = "Project:";
			textBoxProjectName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProjectName.Location = new System.Drawing.Point(213, 33);
			textBoxProjectName.MaxLength = 64;
			textBoxProjectName.Name = "textBoxProjectName";
			textBoxProjectName.ReadOnly = true;
			textBoxProjectName.Size = new System.Drawing.Size(349, 20);
			textBoxProjectName.TabIndex = 146;
			textBoxProjectName.TabStop = false;
			textBoxEntityCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEntityCode.Location = new System.Drawing.Point(103, 10);
			textBoxEntityCode.MaxLength = 64;
			textBoxEntityCode.Name = "textBoxEntityCode";
			textBoxEntityCode.ReadOnly = true;
			textBoxEntityCode.Size = new System.Drawing.Size(105, 20);
			textBoxEntityCode.TabIndex = 145;
			textBoxEntityCode.TabStop = false;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(13, 13);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(54, 13);
			label4.TabIndex = 140;
			label4.Text = "Customer:";
			textBoxEntityName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEntityName.Location = new System.Drawing.Point(214, 10);
			textBoxEntityName.MaxLength = 64;
			textBoxEntityName.Name = "textBoxEntityName";
			textBoxEntityName.ReadOnly = true;
			textBoxEntityName.Size = new System.Drawing.Size(349, 20);
			textBoxEntityName.TabIndex = 6;
			textBoxEntityName.TabStop = false;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(725, 423);
			base.Controls.Add(panelDetails);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(panelButtons);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(465, 215);
			base.Name = "SelectFeesDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Project Fees";
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			ResumeLayout(false);
		}
	}
}
