using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.DataControls.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls.MMSDataGrid
{
	public class POSGrid : UserControl
	{
		public class NoFocusRect : IUIElementDrawFilter
		{
			public bool DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams)
			{
				return true;
			}

			public DrawPhase GetPhasesToFilter(ref UIElementDrawParams drawParams)
			{
				return DrawPhase.BeforeDrawFocus;
			}
		}

		private IContainer components;

		private POSGridButtons posGridButtons1;

		private UltraGrid posEntryGrid1;

		public bool ShowRowButtons
		{
			get;
			set;
		}

		public UltraGrid DataGrid => posEntryGrid1;

		public ContextMenuStrip DropDownMenu => ContextMenuStrip;

		public event EventHandler RowButtonClicked;

		public POSGrid()
		{
			InitializeComponent();
			posEntryGrid1.AfterRowActivate += posEntryGrid1_AfterRowActivate;
			posEntryGrid1.AfterRowActivate += POSEntryGrid_AfterRowActivate;
			base.KeyDown += DataEntryGrid_KeyDown;
			posEntryGrid1.BeforeRowDeactivate += DataEntryGrid_BeforeRowDeactivate;
			base.MouseClick += DataEntryGrid_MouseClick;
			posEntryGrid1.AfterRowInsert += DataEntryGrid_AfterRowInsert;
			posEntryGrid1.SummaryValueChanged += MMSReportGrid_SummaryValueChanged;
			posEntryGrid1.AfterRowActivate += POSEntryGrid_AfterRowActivate;
			posEntryGrid1.BeforeRowDeactivate += POSEntryGrid_BeforeRowDeactivate;
			posEntryGrid1.AfterRowLayoutItemResized += posEntryGrid1_AfterRowLayoutItemResized;
			posEntryGrid1.BeforePerformAction += posEntryGrid1_BeforePerformAction;
			posEntryGrid1.AfterRowActivate += DataGrid_AfterRowActivate;
			posEntryGrid1.BeforeRowDeactivate += DataGrid_BeforeRowDeactivate;
			posEntryGrid1.DrawFilter = new NoFocusRect();
		}

		private void DataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			if (!ShowRowButtons)
			{
				return;
			}
			posEntryGrid1.ActiveRow.Height = 24;
			foreach (UltraGridColumn column in posEntryGrid1.DisplayLayout.Bands[0].Columns)
			{
				column.CellAppearance.TextVAlign = VAlign.Middle;
			}
			if (posEntryGrid1.ActiveRow != null)
			{
				if (posEntryGrid1.DisplayLayout.Bands[0].Columns.Exists("Quantity"))
				{
					posEntryGrid1.DisplayLayout.Bands[0].Columns["Quantity"].CellClickAction = CellClickAction.RowSelect;
					posEntryGrid1.DisplayLayout.Bands[0].Columns["Quantity"].CellActivation = Activation.NoEdit;
					posEntryGrid1.ActiveRow.Cells["Quantity"].Editor = null;
				}
				if (posEntryGrid1.DisplayLayout.Bands[0].Columns.Exists("Price"))
				{
					posEntryGrid1.DisplayLayout.Bands[0].Columns["Price"].CellClickAction = CellClickAction.RowSelect;
					posEntryGrid1.DisplayLayout.Bands[0].Columns["Price"].CellActivation = Activation.NoEdit;
					posEntryGrid1.ActiveRow.Cells["Price"].Editor = null;
				}
				if (posEntryGrid1.DisplayLayout.Bands[0].Columns.Exists("Discount"))
				{
					posEntryGrid1.DisplayLayout.Bands[0].Columns["Discount"].CellClickAction = CellClickAction.RowSelect;
					posEntryGrid1.DisplayLayout.Bands[0].Columns["Discount"].CellActivation = Activation.NoEdit;
					posEntryGrid1.ActiveRow.Cells["Discount"].Editor = null;
				}
				if (posEntryGrid1.DisplayLayout.Bands[0].Columns.Exists("Amount"))
				{
					posEntryGrid1.DisplayLayout.Bands[0].Columns["Amount"].CellClickAction = CellClickAction.RowSelect;
					posEntryGrid1.DisplayLayout.Bands[0].Columns["Amount"].CellActivation = Activation.NoEdit;
					posEntryGrid1.ActiveRow.Cells["Amount"].Editor = null;
				}
				if (posEntryGrid1.DisplayLayout.Bands[0].Columns.Exists("Description"))
				{
					posEntryGrid1.DisplayLayout.Bands[0].Columns["Description"].CellClickAction = CellClickAction.RowSelect;
					posEntryGrid1.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
					posEntryGrid1.ActiveRow.Cells["Description"].Editor = null;
				}
			}
		}

		private void DataGrid_AfterRowActivate(object sender, EventArgs e)
		{
			if (ShowRowButtons)
			{
				posEntryGrid1.ActiveRow.Height = 80;
				foreach (UltraGridColumn column in posEntryGrid1.DisplayLayout.Bands[0].Columns)
				{
					column.CellAppearance.TextVAlign = VAlign.Top;
				}
				posEntryGrid1.DrawFilter = new NoFocusRect();
				CellButtonEditor cellButtonEditor = new CellButtonEditor("Qty", "Edit Qty", HAlign.Right, typeof(decimal), Format.QuantityFormat);
				cellButtonEditor.ButtonClicked += Editor_ButtonClicked;
				if (posEntryGrid1.DisplayLayout.Bands[0].Columns.Exists("Quantity"))
				{
					posEntryGrid1.DisplayLayout.Bands[0].Columns["Quantity"].CellClickAction = CellClickAction.Edit;
					posEntryGrid1.DisplayLayout.Bands[0].Columns["Quantity"].CellActivation = Activation.AllowEdit;
					posEntryGrid1.ActiveRow.Cells["Quantity"].Editor = cellButtonEditor;
				}
				cellButtonEditor = new CellButtonEditor("Price", "Price", HAlign.Right, typeof(decimal), Format.QuantityFormat);
				cellButtonEditor.ButtonClicked += Editor_ButtonClicked;
				if (posEntryGrid1.DisplayLayout.Bands[0].Columns.Exists("Price"))
				{
					posEntryGrid1.DisplayLayout.Bands[0].Columns["Price"].CellClickAction = CellClickAction.Edit;
					posEntryGrid1.DisplayLayout.Bands[0].Columns["Price"].CellActivation = Activation.AllowEdit;
					posEntryGrid1.ActiveRow.Cells["Price"].Editor = cellButtonEditor;
				}
				cellButtonEditor = new CellButtonEditor("Discount", "Discount", HAlign.Right, typeof(decimal), Format.UnitPriceFormat);
				cellButtonEditor.ButtonClicked += Editor_ButtonClicked;
				if (posEntryGrid1.DisplayLayout.Bands[0].Columns.Exists("Discount"))
				{
					posEntryGrid1.DisplayLayout.Bands[0].Columns["Discount"].CellClickAction = CellClickAction.Edit;
					posEntryGrid1.DisplayLayout.Bands[0].Columns["Discount"].CellActivation = Activation.AllowEdit;
					posEntryGrid1.ActiveRow.Cells["Discount"].Editor = cellButtonEditor;
					posEntryGrid1.ActiveRow.Cells["Discount"].Activation = Activation.AllowEdit;
				}
				cellButtonEditor = new CellButtonEditor("Amount", "Amount", HAlign.Right, typeof(decimal), Format.UnitPriceFormat);
				cellButtonEditor.ButtonClicked += Editor_ButtonClicked;
				if (posEntryGrid1.DisplayLayout.Bands[0].Columns.Exists("Amount"))
				{
					posEntryGrid1.DisplayLayout.Bands[0].Columns["Amount"].CellClickAction = CellClickAction.Edit;
					posEntryGrid1.DisplayLayout.Bands[0].Columns["Amount"].CellActivation = Activation.AllowEdit;
					posEntryGrid1.ActiveRow.Cells["Amount"].Editor = cellButtonEditor;
					posEntryGrid1.ActiveRow.Cells["Amount"].Activation = Activation.AllowEdit;
				}
				cellButtonEditor = new CellButtonEditor("Delete", "Delete", "View", "View", HAlign.Left, typeof(string), "");
				cellButtonEditor.ButtonClicked += Editor_ButtonClicked;
				if (posEntryGrid1.DisplayLayout.Bands[0].Columns.Exists("Description"))
				{
					posEntryGrid1.DisplayLayout.Bands[0].Columns["Description"].CellClickAction = CellClickAction.Edit;
					posEntryGrid1.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.AllowEdit;
					posEntryGrid1.ActiveRow.Cells["Description"].Editor = cellButtonEditor;
					posEntryGrid1.ActiveRow.Cells["Description"].Activation = Activation.AllowEdit;
				}
			}
		}

		private void Editor_ButtonClicked(object sender, EventArgs e)
		{
			DataGrid.PerformAction(UltraGridAction.ExitEditMode);
			if (this.RowButtonClicked != null)
			{
				this.RowButtonClicked(sender, e);
			}
			posEntryGrid1.ActiveCell = null;
		}

		private void posEntryGrid1_BeforePerformAction(object sender, BeforeUltraGridPerformActionEventArgs e)
		{
		}

		private void posEntryGrid1_AfterRowLayoutItemResized(object sender, AfterRowLayoutItemResizedEventArgs e)
		{
		}

		private void POSEntryGrid_AfterRowActivate(object sender, EventArgs e)
		{
		}

		private void posEntryGrid1_AfterRowActivate(object sender, EventArgs e)
		{
		}

		private void POSEntryGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			_ = posEntryGrid1.ActiveRow;
		}

		public void ApplyFormat()
		{
			if (DataGrid.DisplayLayout.Bands.Count != 0)
			{
				foreach (UltraGridColumn column in DataGrid.DisplayLayout.Bands[0].Columns)
				{
					if (column.DataType == typeof(int) || column.DataType == typeof(decimal) || column.DataType == typeof(float))
					{
						column.MergedCellStyle = MergedCellStyle.Never;
						column.NullText = "";
						if (column.DataType == typeof(int))
						{
							column.Format = "#,###";
						}
						else
						{
							column.Format = "#,##0.00#";
						}
						if (DataGrid.DisplayLayout.Bands.Count < 2)
						{
							column.CellAppearance.TextHAlign = HAlign.Right;
						}
					}
				}
				foreach (SummarySettings item in (IEnumerable)DataGrid.DisplayLayout.Bands[0].Summaries)
				{
					item.DisplayFormat = "{0:n}";
					item.Appearance.FontData.Bold = DefaultableBoolean.True;
					item.Appearance.TextHAlign = HAlign.Right;
				}
			}
		}

		private void MMSReportGrid_SummaryValueChanged(object sender, SummaryValueChangedEventArgs e)
		{
			if (posEntryGrid1.DisplayLayout.Bands.Count != 0)
			{
				foreach (SummarySettings item in (IEnumerable)posEntryGrid1.DisplayLayout.Bands[0].Summaries)
				{
					item.DisplayFormat = "{0:n}";
					item.Appearance.TextHAlign = HAlign.Right;
					item.Appearance.FontData.Bold = DefaultableBoolean.True;
					item.Appearance.BorderColor = Color.FromArgb(169, 169, 169);
				}
				if (posEntryGrid1.DisplayLayout.Bands.Count > 1)
				{
					foreach (SummarySettings item2 in (IEnumerable)posEntryGrid1.DisplayLayout.Bands[1].Summaries)
					{
						item2.DisplayFormat = "{0:n}";
						item2.Appearance.TextHAlign = HAlign.Right;
						item2.Appearance.FontData.Bold = DefaultableBoolean.True;
						item2.Appearance.BorderColor = Color.FromArgb(169, 169, 169);
					}
				}
			}
		}

		private void DataEntryGrid_AfterRowInsert(object sender, RowEventArgs e)
		{
		}

		private void DataEntryGrid_MouseClick(object sender, MouseEventArgs e)
		{
			UIElement uIElement = posEntryGrid1.DisplayLayout.UIElement.ElementFromPoint(e.Location);
			if (uIElement != null)
			{
				_ = (uIElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.ColumnHeader)) is Infragistics.Win.UltraWinGrid.ColumnHeader);
			}
		}

		private void DataEntryGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			if (!HasRowAnyValue(posEntryGrid1.ActiveRow))
			{
				posEntryGrid1.ActiveRow.Delete(displayPrompt: false);
			}
		}

		private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
		{
			if (posEntryGrid1.ActiveRow != null)
			{
				posEntryGrid1.ActiveRow.Delete();
			}
		}

		private void DataEntryGrid_KeyDown(object sender, KeyEventArgs e)
		{
			if (posEntryGrid1.ActiveRow == null || (posEntryGrid1.ActiveRow.IsAddRow && posEntryGrid1.ActiveCell == null))
			{
				try
				{
					posEntryGrid1.DisplayLayout.Bands[0].AddNew();
				}
				catch (Exception)
				{
				}
			}
			else
			{
				if (posEntryGrid1.ActiveCell == null || posEntryGrid1.ActiveCell.Column.Style == Infragistics.Win.UltraWinGrid.ColumnStyle.Date || posEntryGrid1.ActiveCell.Column.Style == Infragistics.Win.UltraWinGrid.ColumnStyle.DateTime)
				{
					return;
				}
				if (!posEntryGrid1.ActiveCell.DroppedDown && e.KeyCode != Keys.ShiftKey && e.KeyCode != Keys.ControlKey && e.KeyCode != Keys.Return && e.KeyCode != Keys.Escape)
				{
					posEntryGrid1.ActiveCell.DroppedDown = true;
				}
				if (posEntryGrid1.ActiveCell.Column.ValueList == null)
				{
					if (e.KeyCode == Keys.Up)
					{
						posEntryGrid1.PerformAction(UltraGridAction.AboveCell);
						posEntryGrid1.PerformAction(UltraGridAction.EnterEditMode);
						e.SuppressKeyPress = true;
					}
					else if (e.KeyCode == Keys.Down)
					{
						posEntryGrid1.PerformAction(UltraGridAction.BelowCell);
						posEntryGrid1.PerformAction(UltraGridAction.EnterEditMode);
						e.SuppressKeyPress = true;
					}
				}
				if (e.KeyCode == Keys.Right && posEntryGrid1.ActiveCell.Text == "")
				{
					posEntryGrid1.PerformAction(UltraGridAction.NextCellByTab);
				}
				else if (e.KeyCode == Keys.Left && posEntryGrid1.ActiveCell.Text == "")
				{
					posEntryGrid1.PerformAction(UltraGridAction.PrevCellByTab);
				}
				else if (e.KeyCode == Keys.Return)
				{
					posEntryGrid1.PerformAction(UltraGridAction.NextCellByTab);
					e.SuppressKeyPress = true;
				}
			}
		}

		public void SetupUI()
		{
			posEntryGrid1.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
			posEntryGrid1.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
			posEntryGrid1.DisplayLayout.GroupByBox.Hidden = true;
			posEntryGrid1.DisplayLayout.Scrollbars = Scrollbars.Vertical;
			posEntryGrid1.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.Select;
			posEntryGrid1.DisplayLayout.Override.HeaderStyle = HeaderStyle.Standard;
			posEntryGrid1.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
			posEntryGrid1.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromArgb(194, 216, 252);
			posEntryGrid1.DisplayLayout.Override.HeaderAppearance.BackColor2 = Color.FromArgb(225, 238, 253);
			posEntryGrid1.DisplayLayout.Override.HeaderAppearance.BackGradientStyle = GradientStyle.Vertical;
			posEntryGrid1.DisplayLayout.Override.AddRowAppearance.BackColor = Color.White;
			posEntryGrid1.DisplayLayout.Override.TemplateAddRowPrompt = "* Click Here to Add a New Row";
			posEntryGrid1.DisplayLayout.Override.TemplateAddRowAppearance.BackColor = Color.White;
			posEntryGrid1.DisplayLayout.Override.HeaderAppearance.TextHAlign = HAlign.Center;
			posEntryGrid1.DisplayLayout.Override.RowAppearance.BorderColor = Color.DarkGray;
			posEntryGrid1.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Solid;
			posEntryGrid1.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Solid;
			posEntryGrid1.DisplayLayout.Override.ActiveRowAppearance.BackColor = Color.FromArgb(54, 118, 216);
			posEntryGrid1.DisplayLayout.Override.ActiveRowAppearance.ForeColor = Color.White;
			posEntryGrid1.DisplayLayout.Override.ActiveCellAppearance.BackColor = Color.Empty;
			posEntryGrid1.DisplayLayout.Override.SelectedRowAppearance.BackColor = Color.White;
			posEntryGrid1.DisplayLayout.Override.SelectedRowAppearance.ForeColor = Color.Black;
			posEntryGrid1.DisplayLayout.Override.ActiveCellAppearance.ForeColor = Color.White;
			posEntryGrid1.DisplayLayout.Override.SelectTypeCol = SelectType.None;
			posEntryGrid1.DisplayLayout.Override.SelectTypeRow = SelectType.None;
			posEntryGrid1.DisplayLayout.TabNavigation = TabNavigation.NextControlOnLastCell;
			posEntryGrid1.DisplayLayout.Override.SummaryFooterAppearance.BorderColor = Color.Gray;
			posEntryGrid1.DisplayLayout.Override.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
			posEntryGrid1.DisplayLayout.Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;
			posEntryGrid1.DisplayLayout.Override.SummaryValueAppearance.FontData.Bold = DefaultableBoolean.True;
			posEntryGrid1.DisplayLayout.Override.SummaryValueAppearance.BackColor = Color.White;
			posEntryGrid1.DisplayLayout.Override.SummaryValueAppearance.BorderColor = Color.White;
			posEntryGrid1.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
			posEntryGrid1.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.RowSelect;
			posEntryGrid1.DisplayLayout.Bands[0].Override.DefaultRowHeight = 24;
			posEntryGrid1.DisplayLayout.Bands[0].Override.CellAppearance.TextVAlign = VAlign.Middle;
			posEntryGrid1.DisplayLayout.Bands[0].Override.RowSizing = RowSizing.Free;
			posEntryGrid1.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(239, 247, 253);
			posEntryGrid1.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.None;
			posEntryGrid1.DisplayLayout.Override.HeaderAppearance.FontData.SizeInPoints = 11f;
			posEntryGrid1.ExitEditModeOnLeave = true;
			LoadDataSet();
			posEntryGrid1.DisplayLayout.Bands[0].Columns["ItemType"].Hidden = true;
		}

		private void DataEntryGrid_Error(object sender, ErrorEventArgs e)
		{
			if (e.ErrorText == "Unable to update the data value: Value in the editor is not valid." && e.DataErrorInfo.Cell.Column.DataType == typeof(decimal))
			{
				e.ErrorText = "Invalid data. Please enter a numeric value.";
			}
		}

		public void Clear()
		{
			(posEntryGrid1.DataSource as DataTable)?.Rows.Clear();
		}

		public bool HasRowAnyValue(UltraGridRow row)
		{
			if (row == null)
			{
				return false;
			}
			foreach (UltraGridCell cell in row.Cells)
			{
				if (cell.Value != null && cell.Value.ToString() != "")
				{
					return true;
				}
			}
			return false;
		}

		private void LoadDataSet()
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("table");
			dataTable.Columns.Add("ProductID", typeof(string));
			dataTable.Columns.Add("UPC", typeof(string));
			dataTable.Columns.Add("Description", typeof(string));
			dataTable.Columns.Add("Quantity", typeof(decimal));
			dataTable.Columns.Add("ItemType", typeof(int));
			dataTable.Columns.Add("Price", typeof(decimal));
			dataTable.Columns.Add("Discount", typeof(decimal));
			dataTable.Columns.Add("TaxGroupID", typeof(string));
			dataTable.Columns.Add("TaxOption", typeof(byte));
			dataTable.Columns.Add("Tax", typeof(decimal));
			dataTable.Columns.Add("Amount", typeof(decimal));
			dataTable.Columns.Add("TaxTotal", typeof(decimal));
			dataTable.Columns.Add("Barcode", typeof(string));
			posEntryGrid1.DataSource = dataSet.Tables[0];
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
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
			posEntryGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
			posGridButtons1 = new Micromind.DataControls.MMSDataGrid.POSGridButtons();
			((System.ComponentModel.ISupportInitialize)posEntryGrid1).BeginInit();
			SuspendLayout();
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			posEntryGrid1.DisplayLayout.Appearance = appearance;
			appearance2.BackColor = System.Drawing.Color.FromArgb(155, 194, 233);
			appearance2.ImageBackground = Micromind.DataControls.Properties.Resources.listheader;
			appearance2.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Tiled;
			ultraGridBand.Header.Appearance = appearance2;
			appearance3.BackColor = System.Drawing.Color.FromArgb(155, 194, 233);
			appearance3.BackColor2 = System.Drawing.Color.FromArgb(242, 249, 253);
			appearance3.ForeColor = System.Drawing.Color.FromArgb(24, 63, 120);
			appearance3.ImageBackground = Micromind.DataControls.Properties.Resources.listheader;
			appearance3.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Tiled;
			ultraGridBand.Override.HeaderAppearance = appearance3;
			posEntryGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand);
			posEntryGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			posEntryGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance4.BorderColor = System.Drawing.SystemColors.Window;
			posEntryGrid1.DisplayLayout.GroupByBox.Appearance = appearance4;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			posEntryGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
			posEntryGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance6.BackColor2 = System.Drawing.SystemColors.Control;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			posEntryGrid1.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
			posEntryGrid1.DisplayLayout.MaxColScrollRegions = 1;
			posEntryGrid1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			posEntryGrid1.DisplayLayout.Override.ActiveCellAppearance = appearance7;
			appearance8.BackColor = System.Drawing.SystemColors.Highlight;
			appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
			posEntryGrid1.DisplayLayout.Override.ActiveRowAppearance = appearance8;
			posEntryGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			posEntryGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			posEntryGrid1.DisplayLayout.Override.CardAreaAppearance = appearance9;
			appearance10.BorderColor = System.Drawing.Color.Silver;
			appearance10.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			posEntryGrid1.DisplayLayout.Override.CellAppearance = appearance10;
			posEntryGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			posEntryGrid1.DisplayLayout.Override.CellPadding = 0;
			appearance11.BackColor = System.Drawing.SystemColors.Control;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			posEntryGrid1.DisplayLayout.Override.GroupByRowAppearance = appearance11;
			appearance12.TextHAlignAsString = "Left";
			posEntryGrid1.DisplayLayout.Override.HeaderAppearance = appearance12;
			posEntryGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			posEntryGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.Color.Silver;
			posEntryGrid1.DisplayLayout.Override.RowAppearance = appearance13;
			posEntryGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
			posEntryGrid1.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
			posEntryGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			posEntryGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			posEntryGrid1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			posEntryGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			posEntryGrid1.Location = new System.Drawing.Point(0, 0);
			posEntryGrid1.Name = "posEntryGrid1";
			posEntryGrid1.Size = new System.Drawing.Size(932, 621);
			posEntryGrid1.TabIndex = 2;
			posEntryGrid1.Text = "posEntryGrid1";
			posGridButtons1.BackColor = System.Drawing.Color.FromArgb(156, 189, 226);
			posGridButtons1.Location = new System.Drawing.Point(192, 261);
			posGridButtons1.Name = "posGridButtons1";
			posGridButtons1.Size = new System.Drawing.Size(549, 47);
			posGridButtons1.TabIndex = 1;
			posGridButtons1.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(posGridButtons1);
			base.Controls.Add(posEntryGrid1);
			base.Name = "POSGrid";
			base.Size = new System.Drawing.Size(932, 621);
			((System.ComponentModel.ISupportInitialize)posEntryGrid1).EndInit();
			ResumeLayout(false);
		}
	}
}
