using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.UISupport.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class DataGridList : UltraGrid
	{
		private string key = "";

		private bool allowUnfittedView;

		private bool loadLayoutFailed;

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

		private ToolStripMenuItem toolStripMenuItemCopy;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripMenuItem toolStripMenuItemAutoSize;

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

		public bool AllowUnfittedView
		{
			get
			{
				return allowUnfittedView;
			}
			set
			{
				allowUnfittedView = value;
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

		public event EventHandler NewMenuClicked;

		public event EventHandler OpenMenuClicked;

		public event EventHandler DeleteMenuClicked;

		public DataGridList()
		{
			InitializeComponent();
			Init();
		}

		private void toolStripMenuItemAutoSize_Click(object sender, EventArgs e)
		{
			AutoSizeColumns();
		}

		private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
		{
			PerformAction(UltraGridAction.Copy, shift: true, control: false);
		}

		public DataGridList(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
			Init();
		}

		private void Init()
		{
			if (ContextMenuStrip == null)
			{
				ContextMenuStrip = contextMenuStrip1;
			}
			base.SummaryValueChanged += FTGrid_SummaryValueChanged;
			base.MouseDown += DataGridList_MouseDown;
			toolStripMenuItemCopy.Click += toolStripMenuItemCopy_Click;
			toolStripMenuItemAutoSize.Click += toolStripMenuItemAutoSize_Click;
		}

		private void DataGridList_MouseDown(object sender, MouseEventArgs e)
		{
			Point point = new Point(e.X, e.Y);
			UIElement uIElement = ((UltraGrid)sender).DisplayLayout.UIElement.ElementFromPoint(point);
			if (uIElement == null)
			{
				return;
			}
			UltraGridRow ultraGridRow = uIElement.GetContext(typeof(UltraGridRow)) as UltraGridRow;
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
			base.DisplayLayout.Override.AllowMultiCellOperations = (AllowMultiCellOperation.Copy | AllowMultiCellOperation.CopyWithHeaders);
			base.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
			base.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
			base.DisplayLayout.Override.FilterUIType = FilterUIType.FilterRow;
			base.DisplayLayout.Override.CellAppearance.FontData.Name = "Tahoma";
			base.DisplayLayout.Override.BorderStyleHeader = UIElementBorderStyle.Solid;
			base.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Solid;
			base.DisplayLayout.Appearance.BorderColor = SystemColors.InactiveCaption;
			base.DisplayLayout.Override.HeaderStyle = HeaderStyle.WindowsVista;
			base.DisplayLayout.Override.HeaderAppearance.BorderColor = Color.FromArgb(148, 190, 241);
			base.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromArgb(148, 190, 241);
			base.DisplayLayout.Override.HeaderAppearance.BackColor2 = Color.FromArgb(226, 236, 250);
			base.DisplayLayout.Override.HeaderAppearance.BackGradientStyle = GradientStyle.Default;
			base.DisplayLayout.Override.HeaderAppearance.FontData.Name = "Tahoma";
			base.DisplayLayout.Appearance.BackColor = Color.White;
			base.DisplayLayout.GroupByBox.Hidden = true;
			base.DisplayLayout.MaxRowScrollRegions = 1;
			base.DisplayLayout.Override.AllowGroupBy = DefaultableBoolean.True;
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
			base.DisplayLayout.CaptionVisible = DefaultableBoolean.False;
			Color color3 = base.DisplayLayout.Override.SelectedRowAppearance.BackColor = (base.DisplayLayout.Override.ActiveRowAppearance.BackColor = Color.FromKnownColor(KnownColor.Highlight));
			color3 = (base.DisplayLayout.Override.SelectedRowAppearance.ForeColor = (base.DisplayLayout.Override.ActiveRowAppearance.ForeColor = Color.FromKnownColor(KnownColor.HighlightText)));
			if (UserPreferences.ResizeColumnsToFit || !allowUnfittedView)
			{
				base.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
			}
			else
			{
				base.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
			}
			if (base.DisplayLayout.Bands.Count > 0)
			{
				base.DisplayLayout.Bands[0].CardView = false;
			}
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
			base.DisplayLayout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
			base.DisplayLayout.Override.GroupByRowAppearance.BackGradientStyle = GradientStyle.None;
			base.DisplayLayout.Override.GroupByRowDescriptionMask = "[Value] : [Count] Items";
			base.DisplayLayout.Override.GroupByRowAppearance.BackColor = Color.Gainsboro;
			base.DisplayLayout.Override.GroupByRowAppearance.ForeColor = Color.DarkBlue;
			base.DisplayLayout.Override.GroupByRowAppearance.FontData.Bold = DefaultableBoolean.True;
			base.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;
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
			col.NullText = "";
			col.Format = "#,##0.##";
			col.CellAppearance.TextHAlign = HAlign.Right;
			if (addSummary && !base.DisplayLayout.Bands[0].Summaries.Exists(col.Key))
			{
				SummarySettings summarySettings = base.DisplayLayout.Bands[0].Summaries.Add(col.Key, SummaryType.Sum, col, SummaryPosition.UseSummaryPositionColumn);
				summarySettings.SummaryType = SummaryType.Sum;
				summarySettings.DisplayFormat = "{0:#,##0.##}";
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

		public void AddNumericColumnsSummary(string[] excludedColumns, SummaryType summaryType)
		{
			List<string> list = new List<string>();
			if (excludedColumns != null)
			{
				for (int i = 0; i < excludedColumns.Length; i++)
				{
					list.Add(excludedColumns[i]);
				}
			}
			base.DisplayLayout.Bands[0].Summaries.Clear();
			foreach (UltraGridColumn column in base.DisplayLayout.Bands[0].Columns)
			{
				if (!list.Contains(column.Key) && (column.DataType == typeof(int) || column.DataType == typeof(decimal) || column.DataType == typeof(float)) && !base.DisplayLayout.Bands[0].Summaries.Exists(column.Key))
				{
					SummarySettings summarySettings = base.DisplayLayout.Bands[0].Summaries.Add(column.Key, summaryType, column, SummaryPosition.UseSummaryPositionColumn);
					summarySettings.SummaryType = SummaryType.Sum;
					summarySettings.DisplayFormat = "{0}";
					summarySettings.Appearance.TextHAlign = HAlign.Right;
				}
			}
		}

		public void FormatAllNumericColumns(string[] excludedColumns)
		{
			List<string> list = new List<string>();
			if (excludedColumns != null)
			{
				for (int i = 0; i < excludedColumns.Length; i++)
				{
					list.Add(excludedColumns[i]);
				}
			}
			foreach (UltraGridBand band in base.DisplayLayout.Bands)
			{
				foreach (UltraGridColumn column in band.Columns)
				{
					if (!list.Contains(column.Key) && (column.DataType == typeof(int) || column.DataType == typeof(decimal) || column.DataType == typeof(float) || column.Key == "Quantity" || column.Key == "Amount" || column.Key == "Onhand" || column.Key == "AverageCost"))
					{
						column.MergedCellStyle = MergedCellStyle.Never;
						column.NullText = "0.00";
						column.Format = Format.TotalAmountFormat;
						column.CellAppearance.TextHAlign = HAlign.Right;
						if (base.DisplayLayout.Bands[0].Summaries.Count == 0)
						{
							base.DisplayLayout.Bands[0].Summaries.Add(SummaryType.Sum, column, SummaryPosition.UseSummaryPositionColumn).SummaryType = SummaryType.Sum;
						}
					}
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
							item.Appearance.TextHAlign = HAlign.Right;
							item.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
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

		public void AutoFitColumns()
		{
			_ = base.DisplayLayout.AutoFitStyle;
			base.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
			Refresh();
		}

		public void AutoSizeColumns()
		{
			if (base.DisplayLayout.Bands.Count != 0)
			{
				base.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
				foreach (UltraGridColumn column in base.DisplayLayout.Bands[0].Columns)
				{
					if (!column.LockedWidth)
					{
						column.PerformAutoResize(PerformAutoSizeType.VisibleRows, includeHeader: true);
					}
				}
			}
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

		public void SaveLayout()
		{
			SaveLayout("", saveFullSetting: false);
		}

		public void SaveLayout(string keySuffix, bool saveFullSetting)
		{
			try
			{
				if (!loadLayoutFailed && Global.ConStatus == ConnectionStatus.Connected)
				{
					BackgroundWorker backgroundWorker = new BackgroundWorker();
					backgroundWorker.DoWork += worker_DoWork;
					MemoryStream memoryStream = new MemoryStream();
					if (saveFullSetting)
					{
						base.DisplayLayout.Save(memoryStream, (PropertyCategories)6444);
					}
					else
					{
						base.DisplayLayout.Save(memoryStream, PropertyCategories.Bands);
					}
					KeyValuePair<string, MemoryStream> keyValuePair = default(KeyValuePair<string, MemoryStream>);
					keyValuePair = new KeyValuePair<string, MemoryStream>("GList" + base.Parent.Name + keySuffix, memoryStream);
					backgroundWorker.RunWorkerAsync(keyValuePair);
				}
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
				KeyValuePair<string, MemoryStream> keyValuePair = (KeyValuePair<string, MemoryStream>)e.Argument;
				Factory.SettingSystemAsync.SaveSettingStream(keyValuePair.Key, Global.CurrentUser, keyValuePair.Value.ToArray());
			}
			catch
			{
				throw;
			}
		}

		public bool LoadLayout()
		{
			return LoadLayout("");
		}

		public bool LoadLayout(string keySuffix)
		{
			try
			{
				loadLayoutFailed = false;
				if (base.Parent == null)
				{
					return false;
				}
				key = "GList" + base.Parent.Name + keySuffix;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.UISupport.DataGridList));
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
			toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemNew = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripMenuItemAutoFit = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemAutoSize = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemColumnChooser = new System.Windows.Forms.ToolStripMenuItem();
			pictureBoxInactive = new System.Windows.Forms.PictureBox();
			pictureBoxPhoto = new System.Windows.Forms.PictureBox();
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
				toolStripMenuItemCopy,
				toolStripSeparator2,
				toolStripMenuItemAutoFit,
				toolStripMenuItemAutoSize,
				toolStripMenuItemColumnChooser
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(191, 170);
			toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
			toolStripMenuItemOpen.Size = new System.Drawing.Size(190, 22);
			toolStripMenuItemOpen.Text = "Open";
			toolStripMenuItemOpen.Click += new System.EventHandler(toolStripMenuItemOpen_Click);
			toolStripMenuItemNew.Name = "toolStripMenuItemNew";
			toolStripMenuItemNew.Size = new System.Drawing.Size(190, 22);
			toolStripMenuItemNew.Text = "New...";
			toolStripMenuItemNew.Click += new System.EventHandler(toolStripMenuItemNew_Click);
			toolStripMenuItemDelete.Image = Micromind.UISupport.Properties.Resources.delete_2__1_;
			toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
			toolStripMenuItemDelete.Size = new System.Drawing.Size(190, 22);
			toolStripMenuItemDelete.Text = "Delete";
			toolStripMenuItemDelete.Click += new System.EventHandler(toolStripMenuItemDelete_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(187, 6);
			toolStripMenuItemCopy.Image = Micromind.UISupport.Properties.Resources.Copy;
			toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
			toolStripMenuItemCopy.Size = new System.Drawing.Size(190, 22);
			toolStripMenuItemCopy.Text = "Copy to Clipboard";
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(187, 6);
			toolStripMenuItemAutoFit.Image = Micromind.UISupport.Properties.Resources.autofit;
			toolStripMenuItemAutoFit.Name = "toolStripMenuItemAutoFit";
			toolStripMenuItemAutoFit.Size = new System.Drawing.Size(190, 22);
			toolStripMenuItemAutoFit.Text = "Fit Columns to Screen";
			toolStripMenuItemAutoFit.Click += new System.EventHandler(toolStripMenuItemAutoFit_Click);
			toolStripMenuItemAutoSize.Image = Micromind.UISupport.Properties.Resources.colbestsize;
			toolStripMenuItemAutoSize.Name = "toolStripMenuItemAutoSize";
			toolStripMenuItemAutoSize.Size = new System.Drawing.Size(190, 22);
			toolStripMenuItemAutoSize.Text = "Auto Resize Columns";
			toolStripMenuItemColumnChooser.Image = Micromind.UISupport.Properties.Resources.column;
			toolStripMenuItemColumnChooser.Name = "toolStripMenuItemColumnChooser";
			toolStripMenuItemColumnChooser.Size = new System.Drawing.Size(190, 22);
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
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)pictureBoxInactive).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
			((System.ComponentModel.ISupportInitialize)this).EndInit();
			ResumeLayout(false);
		}
	}
}
