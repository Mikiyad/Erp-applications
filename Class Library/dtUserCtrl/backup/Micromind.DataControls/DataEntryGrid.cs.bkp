using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Authentication;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class DataEntryGrid : UltraGrid
	{
		private bool allowCustomizeHeaders;

		private bool includeLotItems;

		private bool loadLayoutFailed;

		private bool ignoreDropDown;

		private bool showDeleteMenu = true;

		private bool showInsertMenu = true;

		private bool showClearMenu = true;

		private bool showMoveMenu = true;

		private string key = "";

		private IContainer components;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem toolStripMenuItemDelete;

		private ContextMenuStrip contextMenuHeader;

		private ToolStripMenuItem menuItemCustomize;

		private ToolStripMenuItem toolStripMenuItemMoveUp;

		private ToolStripMenuItem toolStripMenuItemMoveDown;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem toolStripMenuItemInsert;

		private ToolStripMenuItem toolStripMenuItemClear;

		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool AllowCustomizeHeaders
		{
			get
			{
				return allowCustomizeHeaders;
			}
			set
			{
				allowCustomizeHeaders = value;
			}
		}

		public bool IncludeLotItems
		{
			get
			{
				return includeLotItems;
			}
			set
			{
				includeLotItems = value;
			}
		}

		public ContextMenuStrip DropDownMenu => ContextMenuStrip;

		public bool AllowAddNew
		{
			get
			{
				return base.DisplayLayout.Override.AllowAddNew == Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
			}
			set
			{
				if (value)
				{
					base.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
				}
				else
				{
					base.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
				}
			}
		}

		public bool ShowDeleteMenu
		{
			get
			{
				return showDeleteMenu;
			}
			set
			{
				toolStripMenuItemDelete.Enabled = (showDeleteMenu = value);
			}
		}

		public bool ShowInsertMenu
		{
			get
			{
				return showInsertMenu;
			}
			set
			{
				bool flag2 = toolStripMenuItemInsert.Enabled = value;
				showInsertMenu = flag2;
			}
		}

		public bool ShowClearMenu
		{
			get
			{
				return showClearMenu;
			}
			set
			{
				bool flag2 = toolStripMenuItemClear.Enabled = value;
				showClearMenu = flag2;
			}
		}

		public bool ShowMoveRowsMenu
		{
			get
			{
				return showMoveMenu;
			}
			set
			{
				ToolStripMenuItem toolStripMenuItem = toolStripMenuItemMoveDown;
				bool flag2 = toolStripMenuItemMoveUp.Enabled = value;
				bool flag4 = toolStripMenuItem.Enabled = flag2;
				showMoveMenu = flag4;
			}
		}

		public bool LoadLayoutFailed
		{
			get
			{
				return loadLayoutFailed;
			}
			set
			{
				loadLayoutFailed = value;
			}
		}

		public event EventHandler HeaderClicked;

		public DataEntryGrid()
		{
			InitializeComponent();
			base.Error += DataEntryGrid_Error;
			base.KeyDown += DataEntryGrid_KeyDown;
			toolStripMenuItemDelete.Click += toolStripMenuItemDelete_Click;
			toolStripMenuItemClear.Click += toolStripMenuItemClear_Click;
			base.BeforeRowDeactivate += DataEntryGrid_BeforeRowDeactivate;
			base.MouseClick += DataEntryGrid_MouseClick;
			base.AfterRowInsert += DataEntryGrid_AfterRowInsert;
			base.SummaryValueChanged += MMSReportGrid_SummaryValueChanged;
			base.BeforeCellListDropDown += DataEntryGrid_BeforeCellListDropDown;
			toolStripMenuItemInsert.Name = "Insert";
			ContextMenuStrip.Opening += ContextMenuStrip_Opening;
			menuItemCustomize.Click += menuItemCustomize_Click;
			toolStripMenuItemMoveDown.Click += toolStripMenuItemMoveDown_Click;
			toolStripMenuItemMoveUp.Click += toolStripMenuItemMoveUp_Click;
			toolStripMenuItemInsert.Click += toolStripMenuItemInsert_Click;
			base.AfterCellUpdate += DataEntryGrid_AfterCellUpdate;
			base.KeyActionMappings.Add(new GridKeyActionMapping(Keys.Return, UltraGridAction.NextCell, (UltraGridState)0, UltraGridState.Cell, SpecialKeys.Alt, (SpecialKeys)0));
			base.AfterCellActivate += DataEntryGrid_AfterCellActivate;
		}

		private void DataEntryGrid_AfterCellActivate(object sender, EventArgs e)
		{
			ignoreDropDown = false;
		}

		private void DataEntryGrid_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.CellMultiLine == DefaultableBoolean.True)
			{
				e.Cell.Row.PerformAutoSize();
			}
		}

		public DataSet ImportFromExcel(bool autoFill)
		{
			try
			{
				GridImportFromExcelForm gridImportFromExcelForm = new GridImportFromExcelForm();
				gridImportFromExcelForm.Grid = this;
				if (gridImportFromExcelForm.ShowDialog() == DialogResult.OK)
				{
					if (autoFill)
					{
						DataSet importData = gridImportFromExcelForm.ImportData;
						DataTable dataTable = base.DataSource as DataTable;
						bool result = false;
						bool.TryParse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.ShowSlNo.ToString(), false).ToString(), out result);
						if (!dataTable.Columns.Contains("LotNumber") && result)
						{
							dataTable.Columns.Add("LotNumber");
							dataTable.Columns.Add("BinID");
							dataTable.Columns.Add("RackID");
							dataTable.Columns.Add("Reference");
							dataTable.Columns.Add("Reference2");
							if (!dataTable.Columns.Contains("SourceLotNumber"))
							{
								dataTable.Columns.Add("SourceLotNumber");
							}
							if (!dataTable.Columns.Contains("SoldQty"))
							{
								dataTable.Columns.Add("SoldQty");
							}
							if (!dataTable.Columns.Contains("Location"))
							{
								dataTable.Columns.Add("Location");
							}
							if (!dataTable.Columns.Contains("Cost"))
							{
								dataTable.Columns.Add("Cost");
							}
							dataTable.Columns.Add("ProductionDate");
							dataTable.Columns.Add("ExpiryDate");
						}
						if (dataTable != null)
						{
							dataTable.Rows.Clear();
							foreach (DataRow row in importData.Tables[0].Rows)
							{
								DataRow dataRow2 = dataTable.NewRow();
								foreach (DataColumn column in importData.Tables[0].Columns)
								{
									dataRow2[column.ColumnName] = row[column.ColumnName];
								}
								dataTable.Rows.Add(dataRow2);
							}
						}
					}
					return gridImportFromExcelForm.ImportData;
				}
				return null;
			}
			catch (Exception ex)
			{
				ErrorHelper.WarningMessage("An error occured during import.", "The error message is:", ex.Message);
				return null;
			}
		}

		private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			if (base.ActiveRow == null || base.Rows.Count == 0)
			{
				toolStripMenuItemDelete.Enabled = false;
				toolStripMenuItemInsert.Enabled = false;
				toolStripMenuItemMoveDown.Enabled = false;
				toolStripMenuItemMoveUp.Enabled = false;
			}
			else
			{
				if (base.Rows.Count == 1)
				{
					toolStripMenuItemMoveDown.Enabled = false;
					toolStripMenuItemMoveUp.Enabled = false;
				}
				else
				{
					toolStripMenuItemMoveDown.Enabled = true;
					toolStripMenuItemMoveUp.Enabled = true;
				}
				if (ShowInsertMenu)
				{
					toolStripMenuItemInsert.Enabled = true;
				}
				if (ShowDeleteMenu)
				{
					toolStripMenuItemDelete.Enabled = true;
				}
			}
			if (base.Rows.Count == 0)
			{
				toolStripMenuItemClear.Enabled = false;
			}
			else
			{
				toolStripMenuItemClear.Enabled = true;
			}
		}

		private void toolStripMenuItemInsert_Click(object sender, EventArgs e)
		{
			try
			{
				if (base.ActiveRow != null && base.DisplayLayout != null && !base.ActiveRow.IsGroupByRow && AllowAddNew)
				{
					int index = base.ActiveRow.Index;
					UltraGridRow row = base.DisplayLayout.Bands[0].AddNew();
					base.Rows.Move(row, index);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripMenuItemMoveUp_Click(object sender, EventArgs e)
		{
			if (base.ActiveRow != null && base.ActiveRow.Index != 0 && !base.ActiveRow.IsGroupByRow)
			{
				base.Rows.Move(base.ActiveRow, base.ActiveRow.Index - 1);
			}
		}

		private void toolStripMenuItemMoveDown_Click(object sender, EventArgs e)
		{
			if (base.ActiveRow != null && base.ActiveRow.Index != base.Rows.Count - 1 && !base.ActiveRow.IsGroupByRow)
			{
				base.Rows.Move(base.ActiveRow, base.ActiveRow.Index + 1);
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			try
			{
				if (e.Button == MouseButtons.Right && allowCustomizeHeaders && base.DisplayLayout.UIElement.LastElementEntered.GetContext(typeof(Infragistics.Win.UltraWinGrid.ColumnHeader)) is Infragistics.Win.UltraWinGrid.ColumnHeader)
				{
					contextMenuHeader.Show(this, e.Location, ToolStripDropDownDirection.BelowRight);
					return;
				}
			}
			catch
			{
			}
			base.OnMouseUp(e);
		}

		private void menuItemCustomize_Click(object sender, EventArgs e)
		{
			ShowColumnChooser("Choose Columns");
		}

		private void DataEntryGrid_BeforeCellListDropDown(object sender, CancelableCellEventArgs e)
		{
			if (ignoreDropDown)
			{
				ignoreDropDown = false;
				e.Cancel = true;
			}
			else if (e.Cell.Column.ValueList != null && e.Cell.Column.ValueList.GetType().BaseType == typeof(MultiColumnComboBox))
			{
				(e.Cell.Column.ValueList as MultiColumnComboBox).FireBeforeDropDownEvent(sender);
			}
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			try
			{
				if (base.ActiveCell != null && base.ActiveCell.IsInEditMode && base.ActiveCell.Column.ValueList != null && base.ActiveCell.Column.Style != Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList && base.ActiveCell.Column.ValueList.GetType().BaseType == typeof(MultiColumnComboBox))
				{
					MultiColumnComboBox multiColumnComboBox = base.ActiveCell.Column.ValueList as MultiColumnComboBox;
					if (multiColumnComboBox.DropDownStyle != UltraComboStyle.DropDownList)
					{
						string comboText = base.ActiveCell.Text.Substring(0, base.ActiveCell.EditorResolved.SelectionStart);
						multiColumnComboBox.FireOnKeyUpEvent(e, comboText);
					}
				}
			}
			catch (Exception)
			{
			}
			base.OnKeyUp(e);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Return && e.Alt && base.ActiveCell.Column.CellMultiLine == DefaultableBoolean.True)
				{
					try
					{
						e.Handled = true;
						int selStart = base.ActiveCell.SelStart;
						base.ActiveCell.Value = base.ActiveCell.Text.ToString().Insert(selStart, Environment.NewLine);
						base.ActiveCell.SelStart = selStart + 2;
						base.ActiveCell.SelLength = 0;
					}
					catch
					{
					}
				}
				if (base.ActiveCell != null && base.ActiveCell.Column.ValueList != null && base.ActiveCell.Column.ValueList.GetType().BaseType == typeof(MultiColumnComboBox))
				{
					MultiColumnComboBox multiColumnComboBox = base.ActiveCell.Column.ValueList as MultiColumnComboBox;
					multiColumnComboBox.FireOnKeyDownEvent(null, e);
					if (e.KeyCode == Keys.F2)
					{
						base.ActiveCell.Value = multiColumnComboBox.SelectedID;
					}
				}
			}
			catch (Exception)
			{
			}
			base.OnKeyDown(e);
		}

		private void MMSReportGrid_SummaryValueChanged(object sender, SummaryValueChangedEventArgs e)
		{
			if (base.DisplayLayout.Bands.Count != 0)
			{
				foreach (SummarySettings item in (IEnumerable)base.DisplayLayout.Bands[0].Summaries)
				{
					item.Appearance.BorderColor = Color.FromArgb(169, 169, 169);
					if (item.SummaryType != SummaryType.Count)
					{
						item.DisplayFormat = "{0:n}";
						item.Appearance.TextHAlign = HAlign.Right;
						item.Appearance.FontData.Bold = DefaultableBoolean.True;
					}
				}
				if (base.DisplayLayout.Bands.Count > 1)
				{
					foreach (SummarySettings item2 in (IEnumerable)base.DisplayLayout.Bands[1].Summaries)
					{
						item2.DisplayFormat = "{0:n}";
						item2.Appearance.TextHAlign = HAlign.Right;
						item2.Appearance.FontData.Bold = DefaultableBoolean.True;
						item2.Appearance.BorderColor = Color.FromArgb(169, 169, 169);
					}
				}
			}
		}

		public void EnterEditMode()
		{
			if (base.ActiveCell != null)
			{
				EnterEditMode(base.ActiveCell);
			}
		}

		public void EnterEditMode(UltraGridCell activeCell)
		{
			activeCell.Activated = true;
			SendKeys.Send("0");
		}

		private void DataEntryGrid_AfterRowInsert(object sender, RowEventArgs e)
		{
		}

		private void DataEntryGrid_MouseClick(object sender, MouseEventArgs e)
		{
			UIElement uIElement = base.DisplayLayout.UIElement.ElementFromPoint(e.Location);
			if (uIElement != null)
			{
				Infragistics.Win.UltraWinGrid.ColumnHeader columnHeader = uIElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.ColumnHeader)) as Infragistics.Win.UltraWinGrid.ColumnHeader;
				if (columnHeader != null && this.HeaderClicked != null)
				{
					this.HeaderClicked(columnHeader.Column, null);
				}
			}
		}

		public void ApplyFormat()
		{
			if (base.DisplayLayout.Bands.Count != 0)
			{
				foreach (UltraGridBand band in base.DisplayLayout.Bands)
				{
					foreach (SummarySettings item in (IEnumerable)band.Summaries)
					{
						if (!(item.Key == "count"))
						{
							item.DisplayFormat = "{0:n}";
							item.Appearance.FontData.Bold = DefaultableBoolean.True;
						}
					}
				}
				foreach (UltraGridColumn column in base.DisplayLayout.Bands[0].Columns)
				{
					if (column.DataType == typeof(int) || column.DataType == typeof(decimal) || column.DataType == typeof(float))
					{
						if (column.ValueList != null)
						{
							continue;
						}
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
						if (base.DisplayLayout.Bands.Count < 2)
						{
							column.CellAppearance.TextHAlign = HAlign.Right;
						}
					}
					if (column.DataType == typeof(DateTime))
					{
						column.FormatInfo = Global.CurrentCulture;
						column.Format = Global.CurrentCulture.DateTimeFormat.ShortDatePattern;
					}
				}
				try
				{
					if (base.DisplayLayout.Bands.Count > 0 && !base.DisplayLayout.Bands[0].Summaries.Exists("count"))
					{
						_ = base.DisplayLayout.Bands[0].Columns.Count;
						_ = 0;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void DataEntryGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			if (base.ActiveRow != null && !base.ActiveRow.IsGroupByRow && !HasRowAnyValue(base.ActiveRow))
			{
				base.ActiveRow.Delete(displayPrompt: false);
			}
		}

		private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
		{
			if (base.ActiveRow != null && !base.ActiveRow.IsGroupByRow)
			{
				base.ActiveRow.Delete();
			}
		}

		private void toolStripMenuItemClear_Click(object sender, EventArgs e)
		{
			Clear();
		}

		private void DataEntryGrid_KeyDown(object sender, KeyEventArgs e)
		{
			if (base.ActiveRow == null || (base.ActiveRow.IsAddRow && base.ActiveCell == null))
			{
				try
				{
					base.DisplayLayout.Bands[0].AddNew();
				}
				catch (Exception)
				{
				}
				return;
			}
			if (e.KeyCode == Keys.Tab || e.Control)
			{
				ignoreDropDown = true;
			}
			if (base.ActiveCell == null || base.ActiveCell.Column.Style == Infragistics.Win.UltraWinGrid.ColumnStyle.Date || base.ActiveCell.Column.Style == Infragistics.Win.UltraWinGrid.ColumnStyle.DateTime)
			{
				return;
			}
			if (!base.ActiveCell.DroppedDown && e.KeyCode != Keys.ShiftKey && e.KeyCode != Keys.ControlKey && e.KeyCode != Keys.Return && e.KeyCode != Keys.Escape && e.KeyCode != Keys.F2)
			{
				base.ActiveCell.DroppedDown = true;
			}
			if (base.ActiveCell.Column.ValueList == null)
			{
				if (e.KeyCode == Keys.Up)
				{
					PerformAction(UltraGridAction.AboveCell);
					PerformAction(UltraGridAction.EnterEditMode);
					e.SuppressKeyPress = true;
				}
				else if (e.KeyCode == Keys.Down)
				{
					PerformAction(UltraGridAction.BelowCell);
					PerformAction(UltraGridAction.EnterEditMode);
					e.SuppressKeyPress = true;
				}
			}
			if (e.KeyCode == Keys.Right && base.ActiveCell.Text == "")
			{
				PerformAction(UltraGridAction.NextCellByTab);
			}
			else if (e.KeyCode == Keys.Left && base.ActiveCell.Text == "")
			{
				PerformAction(UltraGridAction.PrevCellByTab);
			}
			else if (e.KeyCode == Keys.Return)
			{
				if (base.ActiveCell != null && base.ActiveCell.Column.CellMultiLine != DefaultableBoolean.True)
				{
					PerformAction(UltraGridAction.NextCellByTab);
					e.SuppressKeyPress = true;
				}
				else if (base.ActiveCell != null)
				{
					base.ActiveRow.PerformAutoSize();
				}
			}
			else if (e.Control)
			{
				if (e.KeyCode == Keys.C)
				{
					Clipboard.SetText(base.ActiveCell.Text);
				}
				else if (e.KeyCode == Keys.V && base.ActiveCell != null)
				{
					PerformAction(UltraGridAction.EnterEditMode);
				}
			}
		}

		public void SetupUI()
		{
			SetupUI(1);
		}

		public void SetupUI(int gridType)
		{
			base.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
			base.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
			base.DisplayLayout.GroupByBox.Hidden = true;
			base.DisplayLayout.Scrollbars = Scrollbars.Vertical;
			base.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.Select;
			base.DisplayLayout.Override.HeaderStyle = HeaderStyle.WindowsVista;
			base.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
			base.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromArgb(148, 190, 241);
			base.DisplayLayout.Override.HeaderAppearance.BackColor2 = Color.FromArgb(225, 238, 253);
			base.DisplayLayout.Override.HeaderAppearance.BorderColor = Color.FromArgb(148, 190, 241);
			base.DisplayLayout.Override.HeaderAppearance.BackGradientStyle = GradientStyle.Default;
			base.DisplayLayout.Override.AddRowAppearance.BackColor = Color.White;
			base.DisplayLayout.Override.TemplateAddRowPrompt = "* Click Here to Add a New Row";
			base.DisplayLayout.Override.TemplateAddRowAppearance.BackColor = Color.White;
			base.DisplayLayout.Override.HeaderAppearance.TextHAlign = HAlign.Center;
			base.DisplayLayout.Override.RowAppearance.BorderColor = Color.DarkGray;
			base.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Solid;
			base.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Solid;
			base.DisplayLayout.Override.ActiveRowAppearance.BackColor = Color.Empty;
			base.DisplayLayout.Override.ActiveRowAppearance.ForeColor = Color.Empty;
			base.DisplayLayout.Override.ActiveCellAppearance.BackColor = Color.Empty;
			base.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
			base.DisplayLayout.Override.AllowMultiCellOperations = AllowMultiCellOperation.All;
			base.DisplayLayout.Appearance.BackColor = Color.White;
			if (gridType == 1)
			{
				base.DisplayLayout.Override.SelectedRowAppearance.BackColor = Color.White;
				base.DisplayLayout.Override.SelectedRowAppearance.ForeColor = Color.Black;
			}
			base.DisplayLayout.Appearance.BorderColor = SystemColors.InactiveCaption;
			base.DisplayLayout.Override.SelectTypeCol = SelectType.None;
			base.DisplayLayout.Override.SelectTypeRow = SelectType.None;
			base.DisplayLayout.TabNavigation = TabNavigation.NextControlOnLastCell;
			base.DisplayLayout.Override.SummaryFooterAppearance.BorderColor = Color.Gray;
			base.DisplayLayout.Override.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
			base.DisplayLayout.Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;
			base.DisplayLayout.Override.SummaryValueAppearance.FontData.Bold = DefaultableBoolean.True;
			base.DisplayLayout.Override.SummaryValueAppearance.BackColor = Color.White;
			base.DisplayLayout.Override.SummaryValueAppearance.BorderColor = Color.White;
			base.DisplayLayout.Override.CellAppearance.BackColorDisabled = Color.White;
			base.DisplayLayout.Override.CellAppearance.ForeColorDisabled = Color.DarkGray;
			base.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
			base.DisplayLayout.Override.RowSizingArea = RowSizingArea.RowSelectorsOnly;
			base.DisplayLayout.Override.BorderStyleRowSelector = UIElementBorderStyle.Rounded1;
			bool result = false;
			bool result2 = false;
			if (Factory.IsConnected)
			{
				bool.TryParse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.ShowSlNo.ToString(), false).ToString(), out result);
				if (result)
				{
					base.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
					base.DisplayLayout.Override.RowSelectorWidth = 25;
					base.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.VisibleIndex;
					base.DisplayLayout.Override.RowSelectorAppearance.ForeColor = Color.Black;
					base.DisplayLayout.Override.RowSelectorAppearance.FontData.Bold = DefaultableBoolean.False;
				}
				bool.TryParse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.ShowFilter.ToString(), false).ToString(), out result2);
				if (result2)
				{
					base.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
					base.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
					base.DisplayLayout.Override.RowFilterAction = RowFilterAction.AppearancesOnly;
					base.DisplayLayout.Override.FilteredInRowAppearance.BackColor = Color.Red;
					base.DisplayLayout.Override.FilteredInCellAppearance.ForeColor = Color.Black;
					base.DisplayLayout.Override.FilteredOutCellAppearance.BackColor = Color.White;
					base.DisplayLayout.Override.FilteredOutCellAppearance.ForeColor = Color.White;
					base.DisplayLayout.Override.FilteredOutRowAppearance.BackColor = Color.White;
					base.DisplayLayout.Override.FilteredOutCellAppearance.ForeColor = Color.Black;
				}
				else
				{
					base.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.False;
				}
			}
			base.ExitEditModeOnLeave = true;
		}

		private void DataEntryGrid_Error(object sender, Infragistics.Win.UltraWinGrid.ErrorEventArgs e)
		{
			if (e.DataErrorInfo == null || e.DataErrorInfo.Cell == null)
			{
				return;
			}
			if (e.ErrorText == "Unable to update the data value: Value in the editor is not valid.")
			{
				if (e.DataErrorInfo.Cell.Column.DataType == typeof(decimal))
				{
					e.ErrorText = "Invalid data. Please enter a numeric value.";
				}
			}
			else if (e.DataErrorInfo.Cell.Column.DataType == typeof(DateTime))
			{
				e.ErrorText = "Please enter a valid date.";
			}
			else if (e.DataErrorInfo.Cell.Column.Key == "Chq Date")
			{
				e.ErrorText = "Please enter a valid date.";
			}
		}

		public void Clear()
		{
			(base.DataSource as DataTable)?.Rows.Clear();
		}

		public bool HasRowAnyValue(UltraGridRow row)
		{
			if (row == null || row.IsGroupByRow)
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

		public int ExistCellValue(string columnKey, string value)
		{
			if (base.DisplayLayout.Bands.Count == 0)
			{
				return -1;
			}
			if (!base.DisplayLayout.Bands[0].Columns.Exists(columnKey))
			{
				return -1;
			}
			foreach (UltraGridRow row in base.Rows)
			{
				if (row.GetCellValue(columnKey).ToString().ToLower() == value.ToLower())
				{
					return row.Index;
				}
			}
			return -1;
		}

		public UltraGridRow SearchRowByValue(string columnKey, string value)
		{
			if (base.DisplayLayout.Bands.Count == 0)
			{
				return null;
			}
			if (!base.DisplayLayout.Bands[0].Columns.Exists(columnKey))
			{
				return null;
			}
			foreach (UltraGridRow row in base.Rows)
			{
				if (row.GetCellValue(columnKey).ToString().ToLower() == value.ToLower())
				{
					return row;
				}
			}
			return null;
		}

		public void SaveLayout()
		{
			try
			{
				if (!loadLayoutFailed)
				{
					BackgroundWorker backgroundWorker = new BackgroundWorker();
					backgroundWorker.DoWork += worker_DoWork;
					MemoryStream memoryStream = new MemoryStream();
					base.DisplayLayout.Save(memoryStream, PropertyCategories.Bands);
					KeyValuePair<string, MemoryStream> keyValuePair = default(KeyValuePair<string, MemoryStream>);
					keyValuePair = new KeyValuePair<string, MemoryStream>("EnList" + GetParentName(), memoryStream);
					backgroundWorker.RunWorkerAsync(keyValuePair);
				}
			}
			catch (Exception ex)
			{
				if (ex == null || !(ex.GetType() == typeof(AuthenticationException)))
				{
					throw;
				}
			}
		}

		public void SaveLayout(string key)
		{
			try
			{
				if (!loadLayoutFailed)
				{
					BackgroundWorker backgroundWorker = new BackgroundWorker();
					backgroundWorker.DoWork += worker_DoWork;
					MemoryStream memoryStream = new MemoryStream();
					base.DisplayLayout.Save(memoryStream, PropertyCategories.Bands);
					KeyValuePair<string, MemoryStream> keyValuePair = default(KeyValuePair<string, MemoryStream>);
					keyValuePair = new KeyValuePair<string, MemoryStream>(key + GetParentName(), memoryStream);
					backgroundWorker.RunWorkerAsync(keyValuePair);
				}
			}
			catch (Exception ex)
			{
				if (ex == null || !(ex.GetType() == typeof(AuthenticationException)))
				{
					throw;
				}
			}
		}

		private void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				KeyValuePair<string, MemoryStream> keyValuePair = (KeyValuePair<string, MemoryStream>)e.Argument;
				Factory.SettingSystemAsync.SaveSettingStream(keyValuePair.Key, Global.CurrentUser, keyValuePair.Value.ToArray());
			}
			catch
			{
			}
		}

		private string GetParentName()
		{
			string text = "";
			if (base.Parent.GetType().BaseType == typeof(Form))
			{
				return base.Parent.Name;
			}
			if (base.Parent.Parent.GetType().BaseType == typeof(Form))
			{
				return base.Parent.Parent.Name;
			}
			return base.Parent.Parent.Parent.Name;
		}

		public bool LoadLayout()
		{
			try
			{
				loadLayoutFailed = false;
				string parentName = GetParentName();
				key = "EnList" + parentName;
				byte[] binaryData = Factory.SettingSystemAsync.GetBinaryData(Global.CurrentUser, key);
				if (binaryData == null)
				{
					return false;
				}
				if (binaryData.Length == 0)
				{
					return false;
				}
				MemoryStream stream = new MemoryStream(binaryData, 0, binaryData.Length);
				base.DisplayLayout.Load(stream);
				return true;
			}
			catch
			{
				loadLayoutFailed = true;
				throw;
			}
		}

		public bool LoadLayout(string keyParm)
		{
			try
			{
				loadLayoutFailed = false;
				string parentName = GetParentName();
				key = keyParm + parentName;
				byte[] binaryData = Factory.SettingSystemAsync.GetBinaryData(Global.CurrentUser, key);
				if (binaryData == null)
				{
					return false;
				}
				if (binaryData.Length == 0)
				{
					return false;
				}
				MemoryStream stream = new MemoryStream(binaryData, 0, binaryData.Length);
				base.DisplayLayout.Load(stream);
				return true;
			}
			catch
			{
				loadLayoutFailed = true;
				throw;
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
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
			toolStripMenuItemMoveUp = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemMoveDown = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemInsert = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemClear = new System.Windows.Forms.ToolStripMenuItem();
			contextMenuHeader = new System.Windows.Forms.ContextMenuStrip();
			menuItemCustomize = new System.Windows.Forms.ToolStripMenuItem();
			contextMenuStrip1.SuspendLayout();
			contextMenuHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this).BeginInit();
			SuspendLayout();
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[6]
			{
				toolStripMenuItemMoveUp,
				toolStripMenuItemMoveDown,
				toolStripMenuItemInsert,
				toolStripSeparator1,
				toolStripMenuItemDelete,
				toolStripMenuItemClear
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(139, 120);
			toolStripMenuItemMoveUp.Name = "toolStripMenuItemMoveUp";
			toolStripMenuItemMoveUp.Size = new System.Drawing.Size(138, 22);
			toolStripMenuItemMoveUp.Text = "Move Up";
			toolStripMenuItemMoveDown.Name = "toolStripMenuItemMoveDown";
			toolStripMenuItemMoveDown.Size = new System.Drawing.Size(138, 22);
			toolStripMenuItemMoveDown.Text = "Move Down";
			toolStripMenuItemInsert.Name = "toolStripMenuItemInsert";
			toolStripMenuItemInsert.Size = new System.Drawing.Size(138, 22);
			toolStripMenuItemInsert.Text = "Insert Row";
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(135, 6);
			toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
			toolStripMenuItemDelete.Size = new System.Drawing.Size(138, 22);
			toolStripMenuItemDelete.Text = "Delete Row";
			toolStripMenuItemClear.Name = "toolStripMenuItemClear";
			toolStripMenuItemClear.Size = new System.Drawing.Size(138, 22);
			toolStripMenuItemClear.Text = "Clear Grid";
			toolStripMenuItemClear.Visible = false;
			contextMenuHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				menuItemCustomize
			});
			contextMenuHeader.Name = "contextMenuHeader";
			contextMenuHeader.Size = new System.Drawing.Size(175, 26);
			menuItemCustomize.Name = "menuItemCustomize";
			menuItemCustomize.Size = new System.Drawing.Size(174, 22);
			menuItemCustomize.Text = "Choose Columns...";
			ContextMenuStrip = contextMenuStrip1;
			contextMenuStrip1.ResumeLayout(false);
			contextMenuHeader.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this).EndInit();
			ResumeLayout(false);
		}
	}
}
