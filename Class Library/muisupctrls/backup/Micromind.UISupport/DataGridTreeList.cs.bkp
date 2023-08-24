using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.UISupport.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class DataGridTreeList : UltraGrid
	{
		private string key = "";

		private bool showMinusInRed = true;

		private IContainer components;

		private ToolStripMenuItem toolStripMenuItemOpen;

		private ToolStripMenuItem toolStripMenuItemNew;

		private ToolStripMenuItem toolStripMenuItemDelete;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem toolStripMenuItemAutoFit;

		private ToolStripMenuItem toolStripMenuItemColumnChooser;

		private PictureBox pictureBoxInactive;

		private PictureBox pictureBoxPhoto;

		public ContextMenuStrip contextMenuStrip1;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripMenuItem toolStripMenuItemExpandAll;

		private ToolStripMenuItem toolStripMenuItemCollapseAll;

		public ContextMenuStrip DropDownMenu
		{
			get
			{
				return contextMenuStrip1;
			}
			set
			{
				contextMenuStrip1 = value;
			}
		}

		public bool ShowMinusInRed
		{
			get
			{
				return showMinusInRed;
			}
			set
			{
				showMinusInRed = value;
				foreach (UltraGridRow row in base.Rows)
				{
					foreach (UltraGridCell cell in row.Cells)
					{
						if (cell.Column.DataType == typeof(int) || cell.Column.DataType == typeof(decimal))
						{
							try
							{
								if (decimal.Parse(cell.Text) < 0m && showMinusInRed)
								{
									cell.Appearance.ForeColor = Color.Red;
								}
								else
								{
									cell.Appearance.ForeColor = Color.Black;
								}
							}
							catch
							{
							}
						}
					}
				}
			}
		}

		public bool ShowNewMenu
		{
			get
			{
				return toolStripMenuItemNew.Visible;
			}
			set
			{
				toolStripMenuItemNew.Visible = value;
			}
		}

		public bool ShowDeleteMenu
		{
			get
			{
				return toolStripMenuItemDelete.Visible;
			}
			set
			{
				toolStripMenuItemDelete.Visible = value;
			}
		}

		public event EventHandler NewMenuClicked;

		public event EventHandler OpenMenuClicked;

		public event EventHandler DeleteMenuClicked;

		public DataGridTreeList()
		{
			InitializeComponent();
		}

		public DataGridTreeList(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
			if (ContextMenuStrip == null)
			{
				ContextMenuStrip = contextMenuStrip1;
			}
			base.SummaryValueChanged += FTGrid_SummaryValueChanged;
			base.MouseDown += DataGridTreeList_MouseDown;
			base.AfterColPosChanged += DataGridTreeList_AfterColPosChanged;
		}

		private void DataGridTreeList_AfterColPosChanged(object sender, AfterColPosChangedEventArgs e)
		{
			if (e.PosChanged == PosChanged.Moved)
			{
				GridEventManager gridEventManager = new GridEventManager(this);
				gridEventManager = base.EventManager;
				gridEventManager.SetEnabled(GridEventIds.AfterColPosChanged, enabled: false);
				foreach (UltraGridBand band in base.DisplayLayout.Bands)
				{
					if (band.Key != e.ColumnHeaders[0].Band.Key)
					{
						band.Columns[e.ColumnHeaders[0].Column.Key].Header.VisiblePosition = e.ColumnHeaders[0].VisiblePosition;
					}
				}
				gridEventManager.SetEnabled(GridEventIds.AfterColPosChanged, enabled: true);
			}
		}

		private void DataGridTreeList_MouseDown(object sender, MouseEventArgs e)
		{
			Point point = new Point(e.X, e.Y);
			UltraGridRow ultraGridRow = ((UltraGrid)sender).DisplayLayout.UIElement.ElementFromPoint(point).GetContext(typeof(UltraGridRow)) as UltraGridRow;
			if (ultraGridRow != null && ultraGridRow.GetType() != typeof(UltraGridFilterRow))
			{
				if (base.Selected.Rows.Count > 0 && base.Selected.Rows.Count < 2)
				{
					base.Selected.Rows[0].Selected = false;
				}
				if (base.ActiveRow != null)
				{
					base.ActiveRow = ultraGridRow;
				}
			}
		}

		public void SetInactiveColumn(string key)
		{
			if (base.DisplayLayout.Bands[0].Columns.Exists(key))
			{
				ValueList valueList = new ValueList();
				ValueListItem valueListItem = new ValueListItem();
				valueListItem.Appearance.ImageBackground = pictureBoxInactive.Image;
				valueListItem.DataValue = "True";
				valueListItem.DisplayText = " ";
				valueList.ValueListItems.Add(valueListItem);
				valueListItem = new ValueListItem();
				valueListItem.DataValue = "False";
				valueListItem.DisplayText = " ";
				valueList.ValueListItems.Add(valueListItem);
				base.DisplayLayout.Bands[0].Columns[key].ValueList = valueList;
			}
		}

		public void SetPhotoColumn(string key)
		{
			if (base.DisplayLayout.Bands[0].Columns.Exists(key))
			{
				ValueList valueList = new ValueList();
				ValueListItem valueListItem = new ValueListItem();
				valueListItem.Appearance.ImageBackground = pictureBoxPhoto.Image;
				valueListItem.DataValue = 1;
				valueListItem.DisplayText = " ";
				valueList.ValueListItems.Add(valueListItem);
				valueListItem = new ValueListItem();
				valueListItem.DataValue = 0;
				valueListItem.DisplayText = " ";
				valueList.ValueListItems.Add(valueListItem);
				base.DisplayLayout.Bands[0].Columns[key].ValueList = valueList;
			}
		}

		private void FTGrid_SummaryValueChanged(object sender, SummaryValueChangedEventArgs e)
		{
			if (base.DisplayLayout.Bands.Count != 0)
			{
				foreach (SummarySettings item in (IEnumerable)base.DisplayLayout.Bands[0].Summaries)
				{
					if (!(item.Key == "count"))
					{
						item.DisplayFormat = "{0:n}";
						item.Appearance.FontData.Bold = DefaultableBoolean.True;
					}
				}
			}
		}

		public void ApplyUIDesign()
		{
			base.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(245, 249, 254);
			base.DisplayLayout.Override.MaxSelectedRows = 100;
			base.DisplayLayout.Override.SelectTypeRow = SelectType.ExtendedAutoDrag;
			base.DisplayLayout.Override.AllowMultiCellOperations = AllowMultiCellOperation.All;
			base.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
			base.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
			base.DisplayLayout.Override.FilterUIType = FilterUIType.HeaderIcons;
			base.DisplayLayout.Override.CellAppearance.FontData.Name = "Tahoma";
			base.DisplayLayout.Override.BorderStyleHeader = UIElementBorderStyle.Solid;
			base.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Solid;
			base.DisplayLayout.Override.HeaderStyle = HeaderStyle.Standard;
			base.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromArgb(177, 207, 248);
			base.DisplayLayout.Override.HeaderAppearance.BackColor2 = Color.FromArgb(226, 236, 250);
			base.DisplayLayout.Override.HeaderAppearance.BackGradientStyle = GradientStyle.Vertical;
			base.DisplayLayout.Override.HeaderAppearance.FontData.Name = "Tahoma";
			base.DisplayLayout.GroupByBox.Hidden = true;
			base.DisplayLayout.Override.HeaderAppearance.BorderColor = Color.Silver;
			base.DisplayLayout.Override.HeaderAppearance.TextHAlign = HAlign.Center;
			base.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
			base.DisplayLayout.Override.AllowRowSummaries = AllowRowSummaries.True;
			base.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
			base.DisplayLayout.Override.ColumnSizingArea = ColumnSizingArea.HeadersOnly;
			base.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains;
			base.DisplayLayout.Override.SummaryFooterCaptionAppearance.BackColor = Color.LightYellow;
			base.DisplayLayout.Override.SummaryFooterCaptionAppearance.FontData.Bold = DefaultableBoolean.True;
			base.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Solid;
			base.DisplayLayout.GroupByBox.Style = GroupByBoxStyle.Compact;
			base.DisplayLayout.GroupByBox.Appearance.BackColor = Color.White;
			base.DisplayLayout.GroupByBox.Appearance.BackColor2 = Color.White;
			base.DisplayLayout.GroupByBox.PromptAppearance.BackColor = Color.White;
			base.DisplayLayout.GroupByBox.PromptAppearance.BackColor2 = Color.White;
			base.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
			base.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
			base.DisplayLayout.Override.SummaryFooterAppearance.BackColor = Color.FromArgb(205, 225, 250);
			base.DisplayLayout.Override.SummaryValueAppearance.BackColor = Color.FromArgb(205, 225, 250);
			base.DisplayLayout.Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;
			base.DisplayLayout.Override.BorderStyleSummaryValue = UIElementBorderStyle.None;
			base.DisplayLayout.Override.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
			base.DisplayLayout.Override.AllowRowSummaries = AllowRowSummaries.BasedOnDataType;
			base.DisplayLayout.Override.GroupByRowAppearance.BackColor = Color.White;
			base.DisplayLayout.Override.GroupByRowAppearance.BackColor2 = Color.White;
			base.DisplayLayout.Override.HeaderPlacement = HeaderPlacement.OncePerGroupedRowIsland;
			base.DisplayLayout.Override.GroupByRowAppearance.BackGradientStyle = GradientStyle.None;
			base.DisplayLayout.InterBandSpacing = 0;
			base.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;
			base.DisplayLayout.Override.AllowColSizing = AllowColSizing.Synchronized;
			base.DisplayLayout.MaxBandDepth = 7;
			foreach (UltraGridBand band in base.DisplayLayout.Bands)
			{
				if (band.Index == 0)
				{
					band.Indentation = 12;
				}
				else
				{
					band.ColHeadersVisible = false;
				}
			}
			ApplyFormat();
		}

		public void ApplyNumericColumnFormat(UltraGridColumn col, bool addSummary)
		{
			col.MergedCellStyle = MergedCellStyle.Never;
			col.NullText = "0.00";
			col.Format = "n";
			col.CellAppearance.TextHAlign = HAlign.Right;
			if (addSummary && !base.DisplayLayout.Bands[0].Summaries.Exists(col.Key))
			{
				SummarySettings summarySettings = base.DisplayLayout.Bands[0].Summaries.Add(col.Key, SummaryType.Sum, col, SummaryPosition.UseSummaryPositionColumn);
				summarySettings.SummaryType = SummaryType.Sum;
				summarySettings.DisplayFormat = "{0}";
				summarySettings.Appearance.TextHAlign = HAlign.Right;
			}
		}

		public void ApplyQuantityColumnFormat(UltraGridColumn col, bool addSummary)
		{
			col.MergedCellStyle = MergedCellStyle.Never;
			col.NullText = "0.00";
			col.CellAppearance.TextHAlign = HAlign.Right;
			if (addSummary && !base.DisplayLayout.Bands[0].Summaries.Exists(col.Key))
			{
				SummarySettings summarySettings = base.DisplayLayout.Bands[0].Summaries.Add(col.Key, SummaryType.Sum, col, SummaryPosition.UseSummaryPositionColumn);
				summarySettings.SummaryType = SummaryType.Sum;
				summarySettings.DisplayFormat = "{0}";
				summarySettings.Appearance.TextHAlign = HAlign.Right;
			}
		}

		public void AddColumnRowCount(UltraGridColumn col)
		{
			try
			{
				if (base.DisplayLayout.Bands.Count > 0 && !base.DisplayLayout.Bands[0].Summaries.Exists("count") && base.DisplayLayout.Bands[0].Columns.Count > 0)
				{
					base.DisplayLayout.Bands[0].Summaries.Add("count", SummaryType.Count, col, SummaryPosition.UseSummaryPositionColumn).DisplayFormat = "{0} Items";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void ApplyFormat()
		{
			if (base.DisplayLayout.Bands.Count != 0)
			{
				foreach (SummarySettings item in (IEnumerable)base.DisplayLayout.Bands[0].Summaries)
				{
					if (!(item.Key == "count"))
					{
						item.DisplayFormat = "{0:n}";
						item.Appearance.FontData.Bold = DefaultableBoolean.True;
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
				ShowMinusInRed = showMinusInRed;
			}
		}

		public void AutoFitColumns()
		{
			base.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
			Refresh();
		}

		private void toolStripMenuItemAutoFit_Click(object sender, EventArgs e)
		{
			AutoFitColumns();
		}

		private void toolStripMenuItemColumnChooser_Click(object sender, EventArgs e)
		{
			ShowColumnChooser();
		}

		private void toolStripMenuItemNew_Click(object sender, EventArgs e)
		{
			if (this.NewMenuClicked != null)
			{
				this.NewMenuClicked(sender, e);
			}
		}

		private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
		{
			if (this.OpenMenuClicked != null)
			{
				this.OpenMenuClicked(sender, e);
			}
		}

		private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
		{
			if (this.DeleteMenuClicked != null)
			{
				this.DeleteMenuClicked(sender, e);
			}
		}

		private void toolStripMenuItemCollapseAll_Click(object sender, EventArgs e)
		{
			if (base.Rows != null)
			{
				base.Rows.CollapseAll(recursive: true);
			}
		}

		private void toolStripMenuItemExpandAll_Click(object sender, EventArgs e)
		{
			if (base.Rows != null)
			{
				base.Rows.ExpandAll(recursive: true);
			}
		}

		public void SaveLayout()
		{
			try
			{
				BackgroundWorker backgroundWorker = new BackgroundWorker();
				backgroundWorker.DoWork += worker_DoWork;
				backgroundWorker.RunWorkerAsync();
			}
			catch
			{
				throw;
			}
		}

		private void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				key = "TList" + base.Parent.Name;
				MemoryStream memoryStream = new MemoryStream();
				base.DisplayLayout.Save(memoryStream, PropertyCategories.Bands);
				Factory.SettingSystem.SaveSettingStream(key, Global.CurrentUser, memoryStream.ToArray());
			}
			catch
			{
				throw;
			}
		}

		public bool LoadLayout()
		{
			try
			{
				key = "TList" + base.Parent.Name;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.UISupport.DataGridTreeList));
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
			toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemNew = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripMenuItemAutoFit = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemColumnChooser = new System.Windows.Forms.ToolStripMenuItem();
			pictureBoxInactive = new System.Windows.Forms.PictureBox();
			pictureBoxPhoto = new System.Windows.Forms.PictureBox();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripMenuItemExpandAll = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxInactive).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
			((System.ComponentModel.ISupportInitialize)this).BeginInit();
			SuspendLayout();
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[9]
			{
				toolStripMenuItemOpen,
				toolStripMenuItemNew,
				toolStripMenuItemDelete,
				toolStripSeparator1,
				toolStripMenuItemAutoFit,
				toolStripMenuItemColumnChooser,
				toolStripSeparator2,
				toolStripMenuItemExpandAll,
				toolStripMenuItemCollapseAll
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(174, 170);
			toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
			toolStripMenuItemOpen.Size = new System.Drawing.Size(173, 22);
			toolStripMenuItemOpen.Text = "Open";
			toolStripMenuItemOpen.Click += new System.EventHandler(toolStripMenuItemOpen_Click);
			toolStripMenuItemNew.Name = "toolStripMenuItemNew";
			toolStripMenuItemNew.Size = new System.Drawing.Size(173, 22);
			toolStripMenuItemNew.Text = "New...";
			toolStripMenuItemNew.Click += new System.EventHandler(toolStripMenuItemNew_Click);
			toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
			toolStripMenuItemDelete.Size = new System.Drawing.Size(173, 22);
			toolStripMenuItemDelete.Text = "Delete";
			toolStripMenuItemDelete.Click += new System.EventHandler(toolStripMenuItemDelete_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
			toolStripMenuItemAutoFit.Name = "toolStripMenuItemAutoFit";
			toolStripMenuItemAutoFit.Size = new System.Drawing.Size(173, 22);
			toolStripMenuItemAutoFit.Text = "Auto Fit Columns";
			toolStripMenuItemAutoFit.Click += new System.EventHandler(toolStripMenuItemAutoFit_Click);
			toolStripMenuItemColumnChooser.Name = "toolStripMenuItemColumnChooser";
			toolStripMenuItemColumnChooser.Size = new System.Drawing.Size(173, 22);
			toolStripMenuItemColumnChooser.Text = "Column Chooser...";
			toolStripMenuItemColumnChooser.Click += new System.EventHandler(toolStripMenuItemColumnChooser_Click);
			pictureBoxInactive.Image = (System.Drawing.Image)resources.GetObject("pictureBoxInactive.Image");
			pictureBoxInactive.Location = new System.Drawing.Point(0, 0);
			pictureBoxInactive.Name = "pictureBoxInactive";
			pictureBoxInactive.Size = new System.Drawing.Size(100, 50);
			pictureBoxInactive.TabIndex = 0;
			pictureBoxInactive.TabStop = false;
			pictureBoxPhoto.Image = (System.Drawing.Image)resources.GetObject("pictureBoxPhoto.Image");
			pictureBoxPhoto.Location = new System.Drawing.Point(0, 0);
			pictureBoxPhoto.Name = "pictureBoxPhoto";
			pictureBoxPhoto.Size = new System.Drawing.Size(100, 50);
			pictureBoxPhoto.TabIndex = 0;
			pictureBoxPhoto.TabStop = false;
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(170, 6);
			toolStripMenuItemExpandAll.Image = Micromind.UISupport.Properties.Resources._001_03;
			toolStripMenuItemExpandAll.Name = "toolStripMenuItemExpandAll";
			toolStripMenuItemExpandAll.Size = new System.Drawing.Size(173, 22);
			toolStripMenuItemExpandAll.Text = "Expand All";
			toolStripMenuItemExpandAll.Click += new System.EventHandler(toolStripMenuItemExpandAll_Click);
			toolStripMenuItemCollapseAll.Image = Micromind.UISupport.Properties.Resources._001_04;
			toolStripMenuItemCollapseAll.Name = "toolStripMenuItemCollapseAll";
			toolStripMenuItemCollapseAll.Size = new System.Drawing.Size(173, 22);
			toolStripMenuItemCollapseAll.Text = "Collapse All";
			toolStripMenuItemCollapseAll.Click += new System.EventHandler(toolStripMenuItemCollapseAll_Click);
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)pictureBoxInactive).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
			((System.ComponentModel.ISupportInitialize)this).EndInit();
			ResumeLayout(false);
		}
	}
}
