using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.DataControls.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class MMSListGrid : UltraGrid
	{
		private bool showMinusInRed;

		private string key = "";

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

		public MMSListGrid()
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
			base.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
			base.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
			base.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.Never;
			base.DisplayLayout.Override.RowAppearance.BorderColor = Color.White;
			base.DisplayLayout.RowConnectorStyle = RowConnectorStyle.None;
			base.DisplayLayout.Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
			base.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
			base.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.None;
			base.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Solid;
			base.DisplayLayout.TabNavigation = TabNavigation.NextControl;
			base.DisplayLayout.Override.HeaderStyle = HeaderStyle.WindowsVista;
			base.DisplayLayout.Bands[0].Override.MaxSelectedRows = 1;
			base.DisplayLayout.Override.HeaderAppearance.BorderColor = Color.FromArgb(148, 190, 241);
			base.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromArgb(148, 190, 241);
			base.DisplayLayout.Override.HeaderAppearance.BackColor2 = Color.FromArgb(225, 238, 253);
			base.DisplayLayout.Override.HeaderAppearance.BackGradientStyle = GradientStyle.Default;
			base.DisplayLayout.Override.HeaderAppearance.TextHAlign = HAlign.Center;
			base.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
			base.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
			base.DisplayLayout.Override.ColumnSizingArea = ColumnSizingArea.HeadersOnly;
			base.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Like;
			base.DisplayLayout.GroupByBox.Hidden = true;
			base.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.None;
			base.DisplayLayout.Override.RowSelectorHeaderStyle = RowSelectorHeaderStyle.ColumnChooserButton;
			base.DisplayLayout.Override.RowSelectorAppearance.BackColor = Color.White;
			base.DisplayLayout.Override.BorderStyleRowSelector = UIElementBorderStyle.None;
			base.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
			base.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
			base.DisplayLayout.Bands[0].Override.CellAppearance.FontData.Name = "Tahoma";
			base.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortSingle;
			base.DisplayLayout.Bands[0].Override.RowSelectorStyle = HeaderStyle.Standard;
			base.DisplayLayout.Bands[0].Override.RowSelectorNumberStyle = RowSelectorNumberStyle.None;
			base.DisplayLayout.Bands[0].Override.RowSelectorAppearance.BorderColor = Color.White;
			base.DisplayLayout.Bands[0].Override.BorderStyleRowSelector = UIElementBorderStyle.None;
			base.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
			base.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.False;
			base.DisplayLayout.Override.BorderStyleHeader = UIElementBorderStyle.Solid;
			base.DisplayLayout.Override.RowSizing = RowSizing.Fixed;
			base.DisplayLayout.Override.SummaryFooterAppearance.BorderColor = Color.White;
			base.DisplayLayout.Override.SummaryDisplayArea = SummaryDisplayAreas.Bottom;
			base.DisplayLayout.Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;
			base.DisplayLayout.Override.SummaryValueAppearance.FontData.Bold = DefaultableBoolean.True;
			base.DisplayLayout.Override.SummaryValueAppearance.BackColor = Color.White;
			base.DisplayLayout.Override.SummaryValueAppearance.BorderColor = Color.White;
			base.DisplayLayout.Override.RowAppearance.BorderColor = Color.LightGray;
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
			_ = base.DisplayLayout.Bands.Count;
			_ = 2;
		}

		public void ApplyFormat()
		{
			if (base.DisplayLayout.Bands.Count != 0)
			{
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
				foreach (SummarySettings item in (IEnumerable)base.DisplayLayout.Bands[0].Summaries)
				{
					item.DisplayFormat = "{0:n}";
					item.Appearance.FontData.Bold = DefaultableBoolean.True;
					item.Appearance.TextHAlign = HAlign.Right;
				}
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
