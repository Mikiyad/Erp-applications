using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class MatrixProductSelector : UserControl
	{
		private string matrixProductID = "";

		private DataSet matrixData;

		private int dimensionCount = 1;

		private Color disableColor = Color.FromArgb(214, 214, 214);

		private bool allowNegativeQuantity = true;

		private IContainer components;

		private DataEntryGrid dataGridItems;

		private Panel panelDown;

		private CheckBox checkBoxShowAll;

		public bool AllowNegativeQuantity
		{
			get
			{
				return allowNegativeQuantity;
			}
			set
			{
				allowNegativeQuantity = value;
			}
		}

		public DataSet MatrixData
		{
			get
			{
				return matrixData;
			}
			set
			{
				matrixData = value;
				dataGridItems.DataSource = matrixData;
			}
		}

		public UltraGrid Grid => dataGridItems;

		public int DimensionCount => dimensionCount;

		public MatrixProductSelector()
		{
			InitializeComponent();
			dataGridItems.SetupUI();
			dataGridItems.ExitEditModeOnLeave = true;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
		}

		private void dataGridItems_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
		{
			if (dataGridItems.ActiveCell != null && !(dataGridItems.ActiveCell.Text.Trim() == "") && !allowNegativeQuantity)
			{
				decimal result = default(decimal);
				decimal.TryParse(dataGridItems.ActiveCell.Value.ToString(), out result);
				if (result < 0m)
				{
					ErrorHelper.InformationMessage("Negative quantity is not allowed.", "Please enter a number greater than or equal to zero.");
					e.Cancel = true;
				}
			}
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dimensionCount > 1)
				{
					decimal num = default(decimal);
					decimal num2 = default(decimal);
					if (e.Cell.Column.Key != "Total")
					{
						foreach (UltraGridColumn column in dataGridItems.DisplayLayout.Bands[0].Columns)
						{
							if ((dimensionCount != 2 || column.Index != 0) && (dimensionCount != 3 || (column.Index != 0 && column.Index != 1)) && !(column.Key == "Total"))
							{
								num2 = default(decimal);
								decimal.TryParse(e.Cell.Row.Cells[column].Value.ToString(), out num2);
								num += num2;
							}
						}
						e.Cell.Row.Cells["Total"].Value = num;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public DataRow GetActiveCellInfo()
		{
			string text = "";
			bool flag = true;
			DataTable dataTable = matrixData.Tables["Product_Parent_Components"];
			if (dimensionCount == 2 || dimensionCount == 3)
			{
				UltraGridRow activeRow = dataGridItems.ActiveRow;
				UltraGridCell activeCell = dataGridItems.ActiveCell;
				if (activeCell.Activation == Activation.Disabled)
				{
					return null;
				}
				text = ((!flag) ? ("AtrName1 = '" + activeCell.Column.Key + "' ") : ("AtrName2 = '" + activeCell.Column.Key + "' "));
				if (dimensionCount == 2)
				{
					text = ((!flag) ? (text + " AND AtrName2 = '" + activeRow.Cells[0].Value.ToString() + "' ") : (text + " AND AtrName1 = '" + activeRow.Cells[0].Value.ToString() + "' "));
				}
				else if (dimensionCount == 3)
				{
					text = ((!flag) ? (text + " AND AtrName2 = '" + activeRow.Cells[1].Value.ToString() + "' AND AtrName3 = '" + activeRow.Cells[0].Value.ToString() + "'") : (text + " AND AtrName1 = '" + activeRow.Cells[1].Value.ToString() + "' AND AtrName3 = '" + activeRow.Cells[0].Value.ToString() + "'"));
				}
				DataRow[] array = dataTable.Select(text);
				if (array.Length != 0)
				{
					return array[0];
				}
				return null;
			}
			UltraGridRow activeRow2 = dataGridItems.ActiveRow;
			_ = dataGridItems.ActiveCell;
			text = " AtrName1 = '" + activeRow2.Cells[0].Value.ToString() + "' ";
			DataRow[] array2 = dataTable.Select(text);
			if (array2.Length != 0)
			{
				return array2[0];
			}
			return null;
		}

		public DataSet GetSelectedItems()
		{
			return GetSelectedItems("0");
		}

		public DataSet GetSelectedItems(string priceListID)
		{
			bool flag = true;
			if (dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.IsInEditMode)
			{
				dataGridItems.PerformAction(UltraGridAction.ExitEditMode);
			}
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("Product");
			dataTable.Columns.Add("ProductID");
			dataTable.Columns.Add("Description");
			dataTable.Columns.Add("Quantity", typeof(float));
			dataTable.Columns.Add("UnitPrice", typeof(decimal));
			dataTable.Columns.Add("LastCost", typeof(decimal));
			string text = "";
			DataTable dataTable2 = matrixData.Tables["Product_Parent_Components"];
			if (dimensionCount == 2 || dimensionCount == 3)
			{
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					foreach (UltraGridColumn column in dataGridItems.DisplayLayout.Bands[0].Columns)
					{
						UltraGridCell ultraGridCell = row.Cells[column.Index];
						if (ultraGridCell.Activation != Activation.Disabled && !(ultraGridCell.Value.ToString() == ""))
						{
							text = ((!flag) ? ("AtrName1= '" + column.Key + "' ") : ("AtrName2= '" + column.Key + "' "));
							if (dimensionCount == 2)
							{
								if (column.Index == 0)
								{
									continue;
								}
								text = ((!flag) ? (text + " AND AtrName2 = '" + row.Cells[0].Value.ToString() + "' ") : (text + " AND AtrName1 = '" + row.Cells[0].Value.ToString() + "' "));
							}
							else if (dimensionCount == 3)
							{
								if (column.Index == 0 || column.Index == 1)
								{
									continue;
								}
								text = text + " AND AtrName2 = '" + row.Cells[1].Value.ToString() + "' AND AtrName3 = '" + row.Cells[0].Value.ToString() + "'";
							}
							DataRow[] array = dataTable2.Select(text);
							if (array.Length != 0)
							{
								DataRow dataRow = dataTable.NewRow();
								dataRow["ProductID"] = array[0]["ProductID"];
								dataRow["Description"] = array[0]["Description"];
								dataRow["Quantity"] = ultraGridCell.Value.ToString();
								if (priceListID == "1")
								{
									dataRow["UnitPrice"] = array[0]["UnitPrice2"];
								}
								else if (priceListID == "2")
								{
									dataRow["UnitPrice"] = array[0]["UnitPrice3"];
								}
								else if (priceListID == "3")
								{
									dataRow["UnitPrice"] = array[0]["MinPrice"];
								}
								else
								{
									dataRow["UnitPrice"] = array[0]["UnitPrice1"];
								}
								dataRow["LastCost"] = array[0]["LastCost"];
								dataTable.Rows.Add(dataRow);
							}
						}
					}
				}
				return dataSet;
			}
			foreach (UltraGridColumn column2 in dataGridItems.DisplayLayout.Bands[0].Columns)
			{
				foreach (UltraGridRow row2 in dataGridItems.Rows)
				{
					if (column2.Index != 0)
					{
						UltraGridCell ultraGridCell2 = row2.Cells[column2.Index];
						if (ultraGridCell2.Value != null && !(ultraGridCell2.Value.ToString() == ""))
						{
							text = " AtrName1 = '" + row2.Cells[0].Value.ToString() + "' ";
							DataRow[] array2 = dataTable2.Select(text);
							if (array2.Length != 0)
							{
								DataRow dataRow2 = dataTable.NewRow();
								dataRow2["ProductID"] = array2[0]["ProductID"];
								dataRow2["Description"] = array2[0]["Description"];
								dataRow2["Quantity"] = ultraGridCell2.Value.ToString();
								dataRow2["UnitPrice"] = array2[0]["UnitPrice1"];
								dataRow2["LastCost"] = array2[0]["LastCost"];
								dataTable.Rows.Add(dataRow2);
							}
						}
					}
				}
			}
			return dataSet;
		}

		public void LoadMatrixData(string matrixProductID)
		{
			bool flag = true;
			this.matrixProductID = matrixProductID;
			matrixData = Factory.ProductParentSystem.GetMatrixTable(matrixProductID, checkBoxShowAll.Checked);
			dimensionCount = 0;
			new DataView(matrixData.Tables[0]);
			if (matrixData != null && matrixData.Tables.Count > 0)
			{
				if (matrixData.Tables[0].Columns.Contains("DimensionCount"))
				{
					dimensionCount = int.Parse(matrixData.Tables[0].Rows[0]["DimensionCount"].ToString());
					matrixData.Tables[0].Columns.Remove("DimensionCount");
				}
				if (dimensionCount > 1)
				{
					matrixData.Tables[0].Columns.Add("Total", typeof(float));
				}
				dataGridItems.DataSource = matrixData.Tables[0];
			}
			dataGridItems.DisplayLayout.Scrollbars = Scrollbars.Automatic;
			if (dimensionCount > 1)
			{
				if (dataGridItems.DisplayLayout.Bands[0].Columns["Total"].Header.VisiblePosition < dataGridItems.DisplayLayout.Bands[0].Columns.Count - 1)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Total"].Header.SetVisiblePosition(dataGridItems.DisplayLayout.Bands[0].Columns.Count - 1, raiseColPosChangedNotifications: false);
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].Header.FixOnRight = DefaultableBoolean.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].Header.Fixed = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].CellAppearance.FontData.Bold = DefaultableBoolean.True;
			}
			dataGridItems.AllowAddNew = false;
			dataGridItems.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellAppearance.BackColorDisabled = disableColor;
			dataGridItems.DisplayLayout.Override.CellAppearance.ForeColorDisabled = Color.Black;
			if (dataGridItems.DisplayLayout.Bands[0].Columns.Count > 0)
			{
				dataGridItems.DisplayLayout.Bands[0].Columns[0].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns[0].CellClickAction = CellClickAction.RowSelect;
			}
			if (dimensionCount == 3)
			{
				dataGridItems.DisplayLayout.Bands[0].Columns[1].CellClickAction = CellClickAction.RowSelect;
				dataGridItems.DisplayLayout.Bands[0].Columns[1].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns[0].MergedCellStyle = MergedCellStyle.Always;
			}
			DataTable dataTable = matrixData.Tables["Product_Parent_Components"];
			string text = "";
			if (dimensionCount == 2 || dimensionCount == 3)
			{
				dataGridItems.DisplayLayout.Bands[0].Columns[0].MinWidth = dataGridItems.DisplayLayout.Bands[0].Columns[0].CalculateAutoResizeWidth(PerformAutoSizeType.AllRowsInBand, AutoResizeColumnWidthOptions.All);
				if (dimensionCount == 3)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns[1].MinWidth = dataGridItems.DisplayLayout.Bands[0].Columns[1].CalculateAutoResizeWidth(PerformAutoSizeType.AllRowsInBand, AutoResizeColumnWidthOptions.All);
				}
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					foreach (UltraGridColumn column in dataGridItems.DisplayLayout.Bands[0].Columns)
					{
						text = ((!flag) ? ("AtrName1= '" + column.Key + "' ") : ("AtrName2= '" + column.Key + "' "));
						if (dimensionCount == 2)
						{
							if (column.Index == 0)
							{
								continue;
							}
							text = ((!flag) ? (text + " AND AtrName2 = '" + row.Cells[0].Value.ToString() + "' ") : (text + " AND AtrName1 = '" + row.Cells[0].Value.ToString() + "' "));
						}
						else if (dimensionCount == 3)
						{
							if (column.Index == 0 || column.Index == 1)
							{
								continue;
							}
							text = text + " AND AtrName2 = '" + row.Cells[1].Value.ToString() + "' AND AtrName3 = '" + row.Cells[0].Value.ToString() + "'";
						}
						if (dataTable.Select(text).Length == 0 && column.Key != "Total")
						{
							row.Cells[column.Index].Activation = Activation.NoEdit;
							row.Cells[column.Index].TabStop = DefaultableBoolean.False;
							row.Cells[column.Index].Appearance.BackColor = disableColor;
						}
					}
				}
				dataGridItems.DisplayLayout.UseFixedHeaders = true;
				dataGridItems.DisplayLayout.Override.FixedHeaderIndicator = FixedHeaderIndicator.None;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].CellAppearance.BackColor = Color.FromArgb(205, 225, 250);
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].CellClickAction = CellClickAction.RowSelect;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].MinWidth = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].TabStop = false;
				if (dimensionCount == 2)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns[0].CellAppearance.BackColor = Color.FromArgb(205, 225, 250);
					dataGridItems.DisplayLayout.Bands[0].Columns[0].Header.Fixed = true;
					dataGridItems.DisplayLayout.Bands[0].Columns[0].TabStop = false;
				}
				else if (dimensionCount == 3)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns[0].MergedCellStyle = MergedCellStyle.Always;
					dataGridItems.DisplayLayout.Bands[0].Columns[0].CellAppearance.BackColor = Color.FromArgb(205, 225, 250);
					dataGridItems.DisplayLayout.Bands[0].Columns[1].CellAppearance.BackColor = Color.FromArgb(205, 225, 250);
					dataGridItems.DisplayLayout.Bands[0].Columns[0].TabStop = false;
					dataGridItems.DisplayLayout.Bands[0].Columns[1].TabStop = false;
					dataGridItems.DisplayLayout.Bands[0].Columns[0].Header.Fixed = true;
					dataGridItems.DisplayLayout.Bands[0].Columns[1].Header.Fixed = true;
				}
				foreach (UltraGridRow row2 in dataGridItems.Rows)
				{
					row2.Cells["Total"].Value = 0;
				}
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns[0].TabStop = false;
				foreach (UltraGridRow row3 in dataGridItems.Rows)
				{
					foreach (UltraGridColumn column2 in dataGridItems.DisplayLayout.Bands[0].Columns)
					{
						text = " AtrName1 = '" + row3.Cells[0].Value.ToString() + "' ";
						if (dataTable.Select(text).Length == 0)
						{
							row3.Cells[column2.Index].Activation = Activation.NoEdit;
							row3.Cells[column2.Index].TabStop = DefaultableBoolean.False;
							row3.Cells[column2.Index].Appearance.BackColor = disableColor;
						}
					}
				}
			}
			dataGridItems.DisplayLayout.Bands[0].Summaries.Clear();
			foreach (UltraGridColumn column3 in dataGridItems.DisplayLayout.Bands[0].Columns)
			{
				if (!dataGridItems.DisplayLayout.Bands[0].Summaries.Exists(column3.Key))
				{
					if (column3.DataType == typeof(decimal) || column3.DataType == typeof(int) || column3.DataType == typeof(float))
					{
						column3.CellAppearance.TextHAlign = HAlign.Right;
						SummarySettings summarySettings = dataGridItems.DisplayLayout.Bands[0].Summaries.Add(column3.Key, SummaryType.Sum, column3);
						summarySettings.Appearance.TextHAlign = HAlign.Right;
						summarySettings.DisplayFormat = "{0:n}";
						summarySettings.Appearance.BackColor = Color.FromArgb(205, 225, 250);
						column3.AllowRowSummaries = AllowRowSummaries.False;
					}
					if (column3.CellActivation != Activation.NoEdit)
					{
						column3.MinWidth = 50;
					}
				}
			}
			dataGridItems.DisplayLayout.Bands[0].Override.SummaryFooterAppearance.BackColor = Color.FromArgb(205, 225, 250);
			foreach (UltraGridRow row4 in dataGridItems.Rows)
			{
				foreach (UltraGridColumn column4 in dataGridItems.DisplayLayout.Bands[0].Columns)
				{
					UltraGridCell ultraGridCell = row4.Cells[column4.Index];
					if (ultraGridCell.Activation != Activation.Disabled && ultraGridCell.Column.CellActivation != Activation.Disabled && column4.CellActivation != Activation.NoEdit)
					{
						ultraGridCell.Appearance.TextHAlign = HAlign.Right;
					}
				}
			}
		}

		private void ShowCardView(bool cardView, string captionColumn)
		{
			if (dataGridItems.DisplayLayout.Bands.Count != 0)
			{
				dataGridItems.DisplayLayout.Bands[0].CardView = true;
				dataGridItems.DisplayLayout.Bands[0].CardSettings.AutoFit = true;
				dataGridItems.DisplayLayout.Bands[0].CardSettings.LabelWidth = 50;
				dataGridItems.DisplayLayout.Bands[0].CardSettings.CaptionField = "";
			}
		}

		private void checkBoxShowAll_CheckedChanged(object sender, EventArgs e)
		{
			LoadMatrixData(matrixProductID);
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
			panelDown = new System.Windows.Forms.Panel();
			checkBoxShowAll = new System.Windows.Forms.CheckBox();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			panelDown.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			SuspendLayout();
			panelDown.Controls.Add(checkBoxShowAll);
			panelDown.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelDown.Location = new System.Drawing.Point(0, 271);
			panelDown.Name = "panelDown";
			panelDown.Size = new System.Drawing.Size(514, 31);
			panelDown.TabIndex = 3;
			checkBoxShowAll.AutoSize = true;
			checkBoxShowAll.Location = new System.Drawing.Point(11, 7);
			checkBoxShowAll.Name = "checkBoxShowAll";
			checkBoxShowAll.Size = new System.Drawing.Size(112, 17);
			checkBoxShowAll.TabIndex = 0;
			checkBoxShowAll.Text = "Show all attributes";
			checkBoxShowAll.UseVisualStyleBackColor = true;
			checkBoxShowAll.CheckedChanged += new System.EventHandler(checkBoxShowAll_CheckedChanged);
			dataGridItems.AllowAddNew = false;
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
			dataGridItems.Dock = System.Windows.Forms.DockStyle.Fill;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.Location = new System.Drawing.Point(0, 0);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.Size = new System.Drawing.Size(514, 271);
			dataGridItems.TabIndex = 2;
			dataGridItems.Text = "dataEntryGrid1";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(dataGridItems);
			base.Controls.Add(panelDown);
			base.Name = "MatrixProductSelector";
			base.Size = new System.Drawing.Size(514, 302);
			panelDown.ResumeLayout(false);
			panelDown.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
		}
	}
}
