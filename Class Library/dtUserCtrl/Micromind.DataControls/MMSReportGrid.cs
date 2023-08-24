using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.DataControls.Properties;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class MMSReportGrid : UltraGrid
	{
		private bool showMinusInRed = true;

		private IContainer components;

		private PictureBox pictureBoxTotalLine;

		private PictureBox pictureBoxVoid;

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

		public MMSReportGrid()
		{
			InitializeComponent();
			InitializeComponent();
			base.SummaryValueChanged += MMSReportGrid_SummaryValueChanged;
		}

		private void MMSReportGrid_SummaryValueChanged(object sender, SummaryValueChangedEventArgs e)
		{
			if (base.DisplayLayout.Bands.Count != 0)
			{
				foreach (SummarySettings item in (IEnumerable)base.DisplayLayout.Bands[0].Summaries)
				{
					item.DisplayFormat = "{0:n}";
					item.Appearance.FontData.Bold = DefaultableBoolean.True;
				}
			}
		}

		public void VoidLine(UltraGridRow row)
		{
			row.Appearance.ForeColor = Color.Red;
		}

		public void DisableSort()
		{
			if (base.DisplayLayout.Bands.Count != 0)
			{
				foreach (UltraGridColumn column in base.DisplayLayout.Bands[0].Columns)
				{
					column.SortIndicator = SortIndicator.Disabled;
				}
				if (base.DisplayLayout.Bands.Count > 0)
				{
					foreach (UltraGridColumn column2 in base.DisplayLayout.Bands[1].Columns)
					{
						column2.SortIndicator = SortIndicator.Disabled;
					}
				}
			}
		}

		public void ApplyUIDesign()
		{
			base.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.Never;
			base.DisplayLayout.Override.RowAppearance.BorderColor = Color.White;
			base.DisplayLayout.RowConnectorStyle = RowConnectorStyle.None;
			base.DisplayLayout.Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
			base.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
			base.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.None;
			base.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Solid;
			base.DisplayLayout.TabNavigation = TabNavigation.NextControl;
			base.DisplayLayout.Override.HeaderStyle = HeaderStyle.Standard;
			base.DisplayLayout.Bands[0].Override.MaxSelectedRows = 1;
			base.DisplayLayout.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;
			base.DisplayLayout.Override.HeaderAppearance.BackColor = Color.White;
			base.DisplayLayout.Override.HeaderAppearance.TextHAlign = HAlign.Center;
			base.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
			base.DisplayLayout.Override.AllowRowSummaries = AllowRowSummaries.SingleSummaryBasedOnDataType;
			base.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
			base.DisplayLayout.Override.ColumnSizingArea = ColumnSizingArea.HeadersOnly;
			base.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Like;
			base.DisplayLayout.Override.SummaryFooterCaptionAppearance.BackColor = Color.LightYellow;
			base.DisplayLayout.Override.SummaryFooterCaptionAppearance.FontData.Bold = DefaultableBoolean.True;
			base.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Solid;
			base.DisplayLayout.GroupByBox.Style = GroupByBoxStyle.Compact;
			base.DisplayLayout.GroupByBox.Appearance.BackColor = Color.White;
			base.DisplayLayout.GroupByBox.Appearance.BackColor2 = Color.White;
			base.DisplayLayout.GroupByBox.Appearance.BorderColor = Color.Empty;
			base.DisplayLayout.GroupByBox.BorderStyle = UIElementBorderStyle.Solid;
			base.DisplayLayout.GroupByBox.PromptAppearance.BackColor = Color.White;
			base.DisplayLayout.GroupByBox.PromptAppearance.BackColor2 = Color.White;
			base.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.None;
			base.DisplayLayout.Override.RowSelectorHeaderStyle = RowSelectorHeaderStyle.ColumnChooserButton;
			base.DisplayLayout.Override.RowSelectorAppearance.BackColor = Color.White;
			base.DisplayLayout.Override.BorderStyleRowSelector = UIElementBorderStyle.None;
			base.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
			base.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
			base.DisplayLayout.Bands[0].Override.CellAppearance.FontData.Name = "Tahoma";
			base.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
			base.DisplayLayout.Bands[0].Override.RowSelectors = DefaultableBoolean.True;
			base.DisplayLayout.Bands[0].Override.RowSelectorStyle = HeaderStyle.Standard;
			base.DisplayLayout.Bands[0].Override.RowSelectorNumberStyle = RowSelectorNumberStyle.None;
			base.DisplayLayout.Bands[0].Override.RowSelectorAppearance.BorderColor = Color.White;
			base.DisplayLayout.Bands[0].Override.BorderStyleRowSelector = UIElementBorderStyle.None;
			base.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
			base.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
			base.DisplayLayout.Override.SummaryFooterAppearance.BorderColor = Color.White;
			base.DisplayLayout.Override.BorderStyleHeader = UIElementBorderStyle.Solid;
			base.DisplayLayout.Override.RowSizing = RowSizing.Fixed;
			base.DisplayLayout.Override.SummaryDisplayArea = SummaryDisplayAreas.Bottom;
			base.DisplayLayout.Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;
			base.DisplayLayout.Override.SummaryValueAppearance.FontData.Bold = DefaultableBoolean.True;
			base.DisplayLayout.Override.SummaryValueAppearance.BackColor = Color.White;
			base.DisplayLayout.Override.SummaryValueAppearance.BorderColor = Color.White;
			base.DisplayLayout.Override.RowAppearance.BorderColor = Color.White;
			base.DisplayLayout.Bands[0].Override.BorderStyleCell = UIElementBorderStyle.None;
			if (base.DisplayLayout.Bands.Count == 0)
			{
				base.DisplayLayout.Bands[0].Override.HotTrackRowAppearance.BorderColor = Color.Blue;
			}
			ApplyFormat();
			foreach (SummarySettings item in (IEnumerable)base.DisplayLayout.Bands[0].Summaries)
			{
				if (item.SummaryType != SummaryType.Formula)
				{
					item.Appearance.ImageBackground = pictureBoxTotalLine.Image;
					item.Appearance.ImageBackgroundStyle = ImageBackgroundStyle.Stretched;
				}
			}
		}

		public void SetSummaryLineImage(string sumKey, int bandNum)
		{
			SummarySettings summarySettings = base.DisplayLayout.Bands[bandNum].Summaries[sumKey];
			summarySettings.Appearance.ImageBackground = pictureBoxTotalLine.Image;
			summarySettings.Appearance.ImageBackgroundStyle = ImageBackgroundStyle.Stretched;
		}

		public void ApplySecondBandDesign()
		{
			if (base.DisplayLayout.Bands.Count >= 2)
			{
				base.DisplayLayout.Rows.ExpandAll(recursive: true);
				base.DisplayLayout.Bands[0].ColHeadersVisible = false;
				base.DisplayLayout.Bands[1].Override.HotTrackRowAppearance.BorderColor = Color.Blue;
				base.DisplayLayout.Override.RowSpacingAfter = 1;
				base.DisplayLayout.Bands[0].Override.CellAppearance.FontData.Bold = DefaultableBoolean.True;
				base.DisplayLayout.Bands[1].Override.AllowColMoving = AllowColMoving.WithinBand;
				base.DisplayLayout.Bands[1].Override.AllowRowSummaries = AllowRowSummaries.SingleSummaryBasedOnDataType;
				base.DisplayLayout.Bands[1].Override.CellAppearance.FontData.Name = "Tahoma";
				base.DisplayLayout.Bands[1].Override.ColumnAutoSizeMode = ColumnAutoSizeMode.AllRowsInBand;
				base.DisplayLayout.Bands[1].Override.ColumnSizingArea = ColumnSizingArea.HeadersOnly;
				base.DisplayLayout.Bands[1].Override.GroupByColumnsHidden = DefaultableBoolean.True;
				base.DisplayLayout.Bands[1].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				base.DisplayLayout.InterBandSpacing = 0;
				base.DisplayLayout.Bands[0].Indentation = 5;
				base.DisplayLayout.Bands[1].Indentation = 0;
				base.DisplayLayout.Bands[1].Override.MaxSelectedRows = 1;
				base.DisplayLayout.Bands[1].Override.RowSelectors = DefaultableBoolean.True;
				base.DisplayLayout.Bands[1].Override.RowSelectorStyle = HeaderStyle.Standard;
				base.DisplayLayout.Bands[1].Override.RowSelectorNumberStyle = RowSelectorNumberStyle.None;
				base.DisplayLayout.Bands[1].Override.RowSelectorAppearance.BorderColor = Color.White;
				base.DisplayLayout.Bands[1].Override.BorderStyleRowSelector = UIElementBorderStyle.None;
				base.DisplayLayout.Bands[0].Override.ActiveRowAppearance.BackColor = Color.White;
				base.DisplayLayout.Bands[0].Override.ActiveRowAppearance.ForeColor = Color.Black;
				base.DisplayLayout.Bands[0].Override.SelectedRowAppearance.BackColor = Color.White;
				base.DisplayLayout.Bands[0].Override.SelectedRowAppearance.ForeColor = Color.Black;
				base.DisplayLayout.Bands[1].Override.BorderStyleCell = UIElementBorderStyle.None;
				foreach (SummarySettings item in (IEnumerable)base.DisplayLayout.Bands[1].Summaries)
				{
					if (item.SummaryType != SummaryType.Formula)
					{
						item.Appearance.ImageBackground = pictureBoxTotalLine.Image;
						item.Appearance.ImageBackgroundStyle = ImageBackgroundStyle.Stretched;
					}
				}
				ApplyBand1Format();
			}
		}

		public void ApplyFormat()
		{
			if (base.DisplayLayout.Bands.Count != 0)
			{
				foreach (UltraGridColumn column in base.DisplayLayout.Bands[0].Columns)
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
						if (base.DisplayLayout.Bands.Count < 2)
						{
							column.CellAppearance.TextHAlign = HAlign.Right;
						}
					}
				}
				foreach (SummarySettings item in (IEnumerable)base.DisplayLayout.Bands[0].Summaries)
				{
					item.DisplayFormat = "{0:n}";
					item.Appearance.FontData.Bold = DefaultableBoolean.True;
					item.Appearance.TextHAlign = HAlign.Right;
				}
				ShowMinusInRed = showMinusInRed;
			}
		}

		public void ApplyBand1Format()
		{
			if (base.DisplayLayout.Bands.Count >= 2)
			{
				foreach (UltraGridColumn column in base.DisplayLayout.Bands[1].Columns)
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
						column.CellAppearance.TextHAlign = HAlign.Right;
					}
				}
				foreach (SummarySettings item in (IEnumerable)base.DisplayLayout.Bands[1].Summaries)
				{
					item.DisplayFormat = "{0:n}";
					item.Appearance.FontData.Bold = DefaultableBoolean.True;
					item.Appearance.TextHAlign = HAlign.Right;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.DataControls.MMSReportGrid));
			pictureBoxTotalLine = new System.Windows.Forms.PictureBox();
			pictureBoxVoid = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)pictureBoxTotalLine).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxVoid).BeginInit();
			((System.ComponentModel.ISupportInitialize)this).BeginInit();
			SuspendLayout();
			pictureBoxTotalLine.Image = Micromind.DataControls.Properties.Resources.GridCellDoubleLine;
			pictureBoxTotalLine.Location = new System.Drawing.Point(0, 0);
			pictureBoxTotalLine.Name = "pictureBoxTotalLine";
			pictureBoxTotalLine.Size = new System.Drawing.Size(100, 50);
			pictureBoxTotalLine.TabIndex = 0;
			pictureBoxTotalLine.TabStop = false;
			pictureBoxVoid.Image = (System.Drawing.Image)resources.GetObject("pictureBoxVoid.Image");
			pictureBoxVoid.Location = new System.Drawing.Point(0, 0);
			pictureBoxVoid.Name = "pictureBoxVoid";
			pictureBoxVoid.Size = new System.Drawing.Size(100, 50);
			pictureBoxVoid.TabIndex = 0;
			pictureBoxVoid.TabStop = false;
			((System.ComponentModel.ISupportInitialize)pictureBoxTotalLine).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxVoid).EndInit();
			((System.ComponentModel.ISupportInitialize)this).EndInit();
			ResumeLayout(false);
		}
	}
}
