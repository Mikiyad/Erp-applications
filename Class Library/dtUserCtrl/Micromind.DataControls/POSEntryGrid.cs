using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.DataControls.MMSDataGrid;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class POSEntryGrid : UltraGrid
	{
		private IContainer components;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem toolStripMenuItemDelete;

		private POSGridButtons posGridButtons1;

		public ContextMenuStrip DropDownMenu => ContextMenuStrip;

		public bool ShowDeleteMenu
		{
			get
			{
				return toolStripMenuItemDelete.Enabled;
			}
			set
			{
				toolStripMenuItemDelete.Enabled = value;
			}
		}

		public event EventHandler HeaderClicked;

		public POSEntryGrid()
		{
			InitializeComponent();
			base.Error += DataEntryGrid_Error;
			base.KeyDown += DataEntryGrid_KeyDown;
			toolStripMenuItemDelete.Click += toolStripMenuItemDelete_Click;
			base.BeforeRowDeactivate += DataEntryGrid_BeforeRowDeactivate;
			base.MouseClick += DataEntryGrid_MouseClick;
			base.AfterRowInsert += DataEntryGrid_AfterRowInsert;
			base.SummaryValueChanged += MMSReportGrid_SummaryValueChanged;
			base.AfterRowActivate += POSEntryGrid_AfterRowActivate;
			base.BeforeRowDeactivate += POSEntryGrid_BeforeRowDeactivate;
		}

		private void POSEntryGrid_AfterRowActivate(object sender, EventArgs e)
		{
			base.ActiveRow.Height = 50;
			base.ActiveRow.CellAppearance.TextVAlign = VAlign.Top;
		}

		private void POSEntryGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
		}

		private void MMSReportGrid_SummaryValueChanged(object sender, SummaryValueChangedEventArgs e)
		{
			if (base.DisplayLayout.Bands.Count != 0)
			{
				foreach (SummarySettings item in (IEnumerable)base.DisplayLayout.Bands[0].Summaries)
				{
					item.DisplayFormat = "{0:n}";
					item.Appearance.TextHAlign = HAlign.Right;
					item.Appearance.FontData.Bold = DefaultableBoolean.True;
					item.Appearance.BorderColor = Color.FromArgb(169, 169, 169);
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

		private void DataEntryGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			if (!HasRowAnyValue(base.ActiveRow))
			{
				base.ActiveRow.Delete(displayPrompt: false);
			}
		}

		private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
		{
			if (base.ActiveRow != null)
			{
				base.ActiveRow.Delete();
			}
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
			}
			else
			{
				if (base.ActiveCell == null || base.ActiveCell.Column.Style == Infragistics.Win.UltraWinGrid.ColumnStyle.Date || base.ActiveCell.Column.Style == Infragistics.Win.UltraWinGrid.ColumnStyle.DateTime)
				{
					return;
				}
				if (!base.ActiveCell.DroppedDown && e.KeyCode != Keys.ShiftKey && e.KeyCode != Keys.ControlKey && e.KeyCode != Keys.Return && e.KeyCode != Keys.Escape)
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
					PerformAction(UltraGridAction.NextCellByTab);
					e.SuppressKeyPress = true;
				}
			}
		}

		public void SetupUI()
		{
			base.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
			base.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
			base.DisplayLayout.GroupByBox.Hidden = true;
			base.DisplayLayout.Scrollbars = Scrollbars.Vertical;
			base.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.Select;
			base.DisplayLayout.Override.HeaderStyle = HeaderStyle.Standard;
			base.DisplayLayout.Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
			base.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromArgb(194, 216, 252);
			base.DisplayLayout.Override.HeaderAppearance.BackColor2 = Color.FromArgb(225, 238, 253);
			base.DisplayLayout.Override.HeaderAppearance.BackGradientStyle = GradientStyle.Vertical;
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
			base.DisplayLayout.Override.SelectedRowAppearance.BackColor = Color.White;
			base.DisplayLayout.Override.SelectedRowAppearance.ForeColor = Color.Black;
			base.DisplayLayout.Override.SelectTypeCol = SelectType.None;
			base.DisplayLayout.Override.SelectTypeRow = SelectType.None;
			base.DisplayLayout.TabNavigation = TabNavigation.NextControlOnLastCell;
			base.DisplayLayout.Override.SummaryFooterAppearance.BorderColor = Color.Gray;
			base.DisplayLayout.Override.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
			base.DisplayLayout.Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;
			base.DisplayLayout.Override.SummaryValueAppearance.FontData.Bold = DefaultableBoolean.True;
			base.DisplayLayout.Override.SummaryValueAppearance.BackColor = Color.White;
			base.DisplayLayout.Override.SummaryValueAppearance.BorderColor = Color.White;
			base.DisplayLayout.Bands[0].Override.DefaultRowHeight = 24;
			base.DisplayLayout.Bands[0].Override.CellAppearance.TextVAlign = VAlign.Middle;
			base.DisplayLayout.Bands[0].Override.RowSizing = RowSizing.Free;
			base.ExitEditModeOnLeave = true;
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
			(base.DataSource as DataTable)?.Rows.Clear();
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
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			posGridButtons1 = new Micromind.DataControls.MMSDataGrid.POSGridButtons();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this).BeginInit();
			SuspendLayout();
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				toolStripMenuItemDelete
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(134, 26);
			toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
			toolStripMenuItemDelete.Size = new System.Drawing.Size(133, 22);
			toolStripMenuItemDelete.Text = "Delete Row";
			posGridButtons1.BackColor = System.Drawing.Color.LavenderBlush;
			posGridButtons1.Location = new System.Drawing.Point(192, 287);
			posGridButtons1.Name = "posGridButtons1";
			posGridButtons1.Size = new System.Drawing.Size(549, 47);
			posGridButtons1.TabIndex = 1;
			ContextMenuStrip = contextMenuStrip1;
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this).EndInit();
			ResumeLayout(false);
		}
	}
}
