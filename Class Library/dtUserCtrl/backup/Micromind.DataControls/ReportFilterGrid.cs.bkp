using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ReportFilterGrid : UserControl
	{
		private UDFTypes udfType;

		private bool isSetupDone;

		private IContainer components;

		private DataEntryGrid dataEntryGrid1;

		private ImageList imageList1;

		public DataEntryGrid DataGrid => dataEntryGrid1;

		public CellsCollection Fields
		{
			get
			{
				if (dataEntryGrid1.Rows.Count == 0)
				{
					return null;
				}
				dataEntryGrid1.PerformAction(UltraGridAction.ExitEditMode);
				return dataEntryGrid1.Rows[0].Cells;
			}
		}

		public event EventHandler SetupUDF;

		public ReportFilterGrid()
		{
			InitializeComponent();
			dataEntryGrid1.AfterRowInsert += dataEntryGrid1_AfterRowInsert;
			dataEntryGrid1.SetupUI();
			SetupGrid();
		}

		private void dataEntryGrid1_AfterRowInsert(object sender, RowEventArgs e)
		{
			if (e.Row.Index == 0)
			{
				e.Row.Cells["AND"].Value = "";
			}
			else
			{
				e.Row.Cells["AND"].Value = "AND";
			}
			e.Row.Cells["ConditionType"].Value = "=";
		}

		public void LoadSetup(UDFTypes udfType)
		{
			try
			{
				this.udfType = udfType;
				dataEntryGrid1.SetupUI();
				dataEntryGrid1.DisplayLayout.Bands[0].CardView = true;
				dataEntryGrid1.DisplayLayout.Bands[0].CardSettings.ShowCaption = false;
				dataEntryGrid1.DisplayLayout.Bands[0].CardSettings.AutoFit = true;
				dataEntryGrid1.DisplayLayout.Bands[0].CardSettings.Style = CardStyle.StandardLabels;
				dataEntryGrid1.DisplayLayout.Bands[0].CardSettings.CardScrollbars = CardScrollbars.None;
				dataEntryGrid1.DisplayLayout.Bands[0].Override.HeaderAppearance.TextHAlign = HAlign.Left;
				dataEntryGrid1.DisplayLayout.Bands[0].CardSettings.MaxCardAreaRows = 1;
				dataEntryGrid1.DisplayLayout.Bands[0].CardSettings.MaxCardAreaCols = 1;
				dataEntryGrid1.DisplayLayout.Bands[0].Override.CellPadding = 2;
				dataEntryGrid1.DisplayLayout.Bands[0].Override.CellSpacing = 1;
				dataEntryGrid1.DisplayLayout.BorderStyle = UIElementBorderStyle.None;
				dataEntryGrid1.DisplayLayout.Appearance.BorderColor = Color.WhiteSmoke;
				dataEntryGrid1.DisplayLayout.Bands[0].Override.CardAreaAppearance.BackColor = Color.WhiteSmoke;
				dataEntryGrid1.DisplayLayout.Bands[0].Override.CardAreaAppearance.BackColor2 = Color.WhiteSmoke;
				dataEntryGrid1.DisplayLayout.Bands[0].Override.HeaderClickAction = HeaderClickAction.Select;
				Color color2 = dataEntryGrid1.DisplayLayout.Bands[0].Override.HeaderAppearance.BackColor = (dataEntryGrid1.DisplayLayout.Bands[0].Override.HeaderAppearance.BackColor2 = Color.WhiteSmoke);
				dataEntryGrid1.AllowAddNew = false;
				isSetupDone = true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public DataSet GetData()
		{
			DataSet dataSet = new DataSet();
			DataTable table = dataEntryGrid1.DataSource as DataTable;
			dataSet.Merge(table);
			return dataSet;
		}

		public void ClearData()
		{
			if (dataEntryGrid1.DataSource != null)
			{
				dataEntryGrid1.PerformAction(UltraGridAction.ExitEditMode);
				foreach (UltraGridColumn column in dataEntryGrid1.DisplayLayout.Bands[0].Columns)
				{
					dataEntryGrid1.Rows[0].Cells[column].Value = DBNull.Value;
				}
			}
		}

		private void SetupGrid()
		{
			dataEntryGrid1.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("AND");
			dataTable.Columns.Add("ColumnName");
			dataTable.Columns.Add("ConditionType");
			dataTable.Columns.Add("ConditionValue");
			dataEntryGrid1.DataSource = dataTable;
			ValueList valueList = new ValueList();
			valueList.ValueListItems.Add("=", "Equal").Appearance.Image = imageList1.Images["equal"];
			valueList.ValueListItems.Add("<>", "Not equal").Appearance.Image = imageList1.Images["notequal"];
			valueList.ValueListItems.Add(">", "Greater than").Appearance.Image = imageList1.Images["greater"];
			valueList.ValueListItems.Add(">=", "Greater than or equal to").Appearance.Image = imageList1.Images["greaterequal"];
			valueList.ValueListItems.Add("<", "Less than").Appearance.Image = imageList1.Images["less"];
			valueList.ValueListItems.Add("<=", "Less than or equal to").Appearance.Image = imageList1.Images["lessequal"];
			valueList.ValueListItems.Add("LIKE", "Like").Appearance.Image = imageList1.Images["like"];
			valueList.ValueListItems.Add("NOT LIKE", "Not like").Appearance.Image = imageList1.Images["notlike"];
			dataEntryGrid1.DisplayLayout.Bands[0].Override.CellAppearance.TextVAlign = VAlign.Middle;
			dataEntryGrid1.DisplayLayout.Bands[0].Columns["ConditionType"].ValueList = valueList;
			Color color2 = dataEntryGrid1.DisplayLayout.Bands[0].Columns["AND"].CellAppearance.BackColorDisabled = (dataEntryGrid1.DisplayLayout.Bands[0].Columns["AND"].CellAppearance.BackColor = Color.WhiteSmoke);
			UltraGridColumn ultraGridColumn = dataEntryGrid1.DisplayLayout.Bands[0].Columns["AND"];
			UltraGridColumn ultraGridColumn2 = dataEntryGrid1.DisplayLayout.Bands[0].Columns["AND"];
			int num2 = dataEntryGrid1.DisplayLayout.Bands[0].Columns["AND"].MaxWidth = 40;
			int num5 = ultraGridColumn.Width = (ultraGridColumn2.MinWidth = num2);
			dataEntryGrid1.DisplayLayout.Bands[0].Columns["AND"].CellActivation = Activation.NoEdit;
			UltraGridColumn ultraGridColumn3 = dataEntryGrid1.DisplayLayout.Bands[0].Columns["ConditionType"];
			UltraGridColumn ultraGridColumn4 = dataEntryGrid1.DisplayLayout.Bands[0].Columns["ConditionType"];
			num2 = (dataEntryGrid1.DisplayLayout.Bands[0].Columns["ConditionType"].MaxWidth = 80);
			num5 = (ultraGridColumn3.Width = (ultraGridColumn4.MinWidth = num2));
			dataEntryGrid1.DisplayLayout.Bands[0].Columns["ConditionType"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
			dataEntryGrid1.DisplayLayout.Bands[0].Columns["ColumnName"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
		}

		private void labelCustomize_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (this.SetupUDF != null)
			{
				this.SetupUDF(this, e);
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
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.DataControls.ReportFilterGrid));
			dataEntryGrid1 = new Micromind.DataControls.DataEntryGrid();
			imageList1 = new System.Windows.Forms.ImageList();
			((System.ComponentModel.ISupportInitialize)dataEntryGrid1).BeginInit();
			SuspendLayout();
			dataEntryGrid1.AllowAddNew = false;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataEntryGrid1.DisplayLayout.Appearance = appearance;
			ultraGridBand.ColHeadersVisible = false;
			ultraGridBand.Override.TemplateAddRowPrompt = "AND";
			dataEntryGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand);
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
			dataEntryGrid1.DisplayLayout.Override.TemplateAddRowPrompt = "AND";
			dataEntryGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataEntryGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataEntryGrid1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataEntryGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			dataEntryGrid1.Location = new System.Drawing.Point(0, 0);
			dataEntryGrid1.Name = "dataEntryGrid1";
			dataEntryGrid1.ShowDeleteMenu = true;
			dataEntryGrid1.ShowInsertMenu = true;
			dataEntryGrid1.ShowMoveRowsMenu = true;
			dataEntryGrid1.Size = new System.Drawing.Size(626, 333);
			dataEntryGrid1.TabIndex = 0;
			dataEntryGrid1.Text = "dataEntryGrid1";
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "equal");
			imageList1.Images.SetKeyName(1, "notequal");
			imageList1.Images.SetKeyName(2, "greater");
			imageList1.Images.SetKeyName(3, "greaterequal");
			imageList1.Images.SetKeyName(4, "less");
			imageList1.Images.SetKeyName(5, "lessequal");
			imageList1.Images.SetKeyName(6, "like");
			imageList1.Images.SetKeyName(7, "notlike");
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(dataEntryGrid1);
			base.Name = "ReportFilterGrid";
			base.Size = new System.Drawing.Size(626, 333);
			((System.ComponentModel.ISupportInitialize)dataEntryGrid1).EndInit();
			ResumeLayout(false);
		}
	}
}
