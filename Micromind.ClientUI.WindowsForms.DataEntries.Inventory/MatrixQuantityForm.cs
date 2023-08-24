using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class MatrixQuantityForm : Form
	{
		private DataSet matrixData;

		private string matrixProductID = "";

		private DataSet productData;

		private int matrixCount;

		private int dimensionCount = 1;

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Line linePanelDown;

		private Label labelMatrixItem;

		private Label label3;

		private DataGridList dataGridList;

		private Panel panel1;

		private Label labelUnitPrice4;

		private Label labelPrice4;

		private Label labelUnitPrice3;

		private Label labelPrice3;

		private Label labelUnitPrice2;

		private Label labelPrice2;

		private Label labelUnitPrice1;

		private Label labelPrice1;

		private Label labelDescription;

		private Label label4;

		private Label labelItemCode;

		private Label label1;

		private UltraGridPrintDocument ultraGridPrintDocument1;

		private Button buttonPrint;

		public DataSet SelectedItems => productData;

		public MatrixQuantityForm()
		{
			InitializeComponent();
			base.FormClosing += MatrixQuantityForm_FormClosing;
			base.Activated += MatrixQuantityForm_Activated;
			dataGridList.DoubleClickCell += dataGridList_DoubleClickCell;
			dataGridList.ContextMenuStrip = null;
			base.StartPosition = FormStartPosition.CenterParent;
			base.Load += MatrixQuantityForm_Load;
			dataGridList.AfterCellActivate += dataGridList_AfterCellActivate;
			labelPrice1.Text = CompanyPreferences.UnitPrice1Title + ":";
			labelPrice2.Text = CompanyPreferences.UnitPrice2Title + ":";
			labelPrice3.Text = CompanyPreferences.UnitPrice3Title + ":";
			labelUnitPrice1.Visible = Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice1);
			labelUnitPrice2.Visible = Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice2);
			labelUnitPrice3.Visible = Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice3);
			labelUnitPrice4.Visible = Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemMinPrice);
		}

		private void dataGridList_AfterCellActivate(object sender, EventArgs e)
		{
			DataRow activeCellInfo = GetActiveCellInfo();
			if (activeCellInfo == null)
			{
				labelDescription.Text = "";
				labelItemCode.Text = "";
				labelUnitPrice1.Text = 0.ToString(Format.UnitPriceFormat);
				labelUnitPrice2.Text = 0.ToString(Format.UnitPriceFormat);
				labelUnitPrice3.Text = 0.ToString(Format.UnitPriceFormat);
				labelUnitPrice4.Text = 0.ToString(Format.UnitPriceFormat);
			}
			else
			{
				labelItemCode.Text = activeCellInfo["ProductID"].ToString();
				labelDescription.Text = activeCellInfo["Description"].ToString();
				labelUnitPrice1.Text = decimal.Parse(activeCellInfo["UnitPrice1"].ToString()).ToString(Format.UnitPriceFormat);
				labelUnitPrice1.Text = decimal.Parse(activeCellInfo["UnitPrice1"].ToString()).ToString(Format.UnitPriceFormat);
				labelUnitPrice2.Text = decimal.Parse(activeCellInfo["UnitPrice2"].ToString()).ToString(Format.UnitPriceFormat);
				labelUnitPrice3.Text = decimal.Parse(activeCellInfo["UnitPrice3"].ToString()).ToString(Format.UnitPriceFormat);
				labelUnitPrice4.Text = decimal.Parse(activeCellInfo["MinPrice"].ToString()).ToString(Format.UnitPriceFormat);
			}
		}

		private void MatrixQuantityForm_Load(object sender, EventArgs e)
		{
			Global.GlobalSettings.LoadFormProperties(this);
		}

		private void MatrixQuantityForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Global.GlobalSettings.SaveFormProperties(this);
		}

		private void dataGridList_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
		{
			if (e.Cell.Column.CellClickAction == CellClickAction.CellSelect)
			{
				DataRow activeCellInfo = GetActiveCellInfo();
				if (activeCellInfo != null && activeCellInfo["ProductID"] != DBNull.Value)
				{
					string productID = activeCellInfo["ProductID"].ToString();
					ProductQuantityForm productQuantityForm = new ProductQuantityForm();
					productQuantityForm.LoadData(productID);
					productQuantityForm.ShowDialog();
				}
			}
		}

		private void MatrixQuantityForm_Activated(object sender, EventArgs e)
		{
		}

		private void Grid_AfterCellActivate(object sender, EventArgs e)
		{
		}

		public DataRow GetActiveCellInfo()
		{
			bool flag = true;
			string text = "";
			DataTable dataTable = matrixData.Tables["Product_Parent_Components"];
			if (dimensionCount == 2 || dimensionCount == 3)
			{
				UltraGridRow activeRow = dataGridList.ActiveRow;
				UltraGridCell activeCell = dataGridList.ActiveCell;
				if (activeRow == null || activeRow.Cells[0].Value == null)
				{
					return null;
				}
				if (activeCell.Activation == Activation.Disabled)
				{
					return null;
				}
				text = ((!flag) ? ("AtrName1= '" + activeCell.Column.Key + "' ") : ("AtrName2= '" + activeCell.Column.Key + "' "));
				if (dimensionCount == 2)
				{
					text = ((!flag) ? (text + " AND AtrName2 = '" + activeRow.Cells[0].Value.ToString() + "' ") : (text + " AND AtrName1 = '" + activeRow.Cells[0].Value.ToString() + "' "));
				}
				else if (dimensionCount == 3)
				{
					text = text + " AND AtrName2 = '" + activeRow.Cells[1].Value.ToString() + "' AND AtrName3 = '" + activeRow.Cells[0].Value.ToString() + "'";
				}
				DataRow[] array = dataTable.Select(text);
				if (array.Length != 0)
				{
					return array[0];
				}
				return null;
			}
			UltraGridRow activeRow2 = dataGridList.ActiveRow;
			_ = dataGridList.ActiveCell;
			text = " AtrName1 = '" + activeRow2.Cells[0].Value.ToString() + "' ";
			DataRow[] array2 = dataTable.Select(text);
			if (array2.Length != 0)
			{
				return array2[0];
			}
			return null;
		}

		public void LoadData(string matrixProductID, string matrixDescription)
		{
			dataGridList.ApplyUIDesign();
			this.matrixProductID = matrixProductID;
			matrixData = Factory.ProductParentSystem.GetMatrixQuantityTable(matrixProductID, showAllComponents: false);
			if (matrixData != null && matrixData.Tables.Count != 0 && matrixData.Tables[0].Rows.Count != 0)
			{
				dataGridList.DataSource = matrixData.Tables[0];
				labelMatrixItem.Text = matrixProductID + " | " + matrixDescription;
				dataGridList.DisplayLayout.Bands[0].Columns[0].MergedCellStyle = MergedCellStyle.Always;
				dataGridList.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.Select;
				if (matrixData != null && matrixData.Tables.Count > 0 && matrixData.Tables[0].Columns.Contains("DimensionCount"))
				{
					dimensionCount = int.Parse(matrixData.Tables[0].Rows[0]["DimensionCount"].ToString());
					matrixData.Tables[0].Columns.Remove("DimensionCount");
				}
				UltraGridColumn ultraGridColumn = dataGridList.DisplayLayout.Bands[0].Columns.Add("Total");
				decimal num = default(decimal);
				ultraGridColumn.CellAppearance.TextHAlign = HAlign.Right;
				SummarySettings summarySettings = dataGridList.DisplayLayout.Bands[0].Summaries.Add(ultraGridColumn.Key, SummaryType.Sum, ultraGridColumn);
				summarySettings.Appearance.TextHAlign = HAlign.Right;
				summarySettings.DisplayFormat = "{0:n}";
				ultraGridColumn.CellAppearance.BackColor = Color.FromArgb(205, 225, 250);
				ultraGridColumn.CellAppearance.FontData.Bold = DefaultableBoolean.True;
				ultraGridColumn.CellClickAction = CellClickAction.RowSelect;
				foreach (UltraGridColumn column in dataGridList.DisplayLayout.Bands[0].Columns)
				{
					if (column.DataType == typeof(decimal) || column.DataType == typeof(int) || column.DataType == typeof(float))
					{
						column.CellAppearance.TextHAlign = HAlign.Right;
						SummarySettings summarySettings2 = dataGridList.DisplayLayout.Bands[0].Summaries.Add(column.Key, SummaryType.Sum, column);
						summarySettings2.Appearance.TextHAlign = HAlign.Right;
						summarySettings2.DisplayFormat = "{0:n}";
						column.CellClickAction = CellClickAction.CellSelect;
						column.AllowRowSummaries = AllowRowSummaries.False;
					}
					column.MinWidth = 50;
				}
				foreach (UltraGridRow row in dataGridList.Rows)
				{
					num = default(decimal);
					foreach (UltraGridColumn column2 in dataGridList.DisplayLayout.Bands[0].Columns)
					{
						if (column2.DataType == typeof(decimal) || column2.DataType == typeof(int) || column2.DataType == typeof(float))
						{
							decimal result = default(decimal);
							decimal.TryParse(row.Cells[column2.Key].Value.ToString(), out result);
							num += result;
						}
					}
					row.Cells["Total"].Value = num;
				}
				dataGridList.DisplayLayout.UseFixedHeaders = true;
				dataGridList.DisplayLayout.Override.FixedHeaderIndicator = FixedHeaderIndicator.None;
				if (dimensionCount == 2)
				{
					dataGridList.DisplayLayout.Bands[0].Columns[0].CellAppearance.BackColor = Color.FromArgb(205, 225, 250);
					dataGridList.DisplayLayout.Bands[0].Columns[0].Header.Fixed = true;
				}
				else if (dimensionCount == 3)
				{
					dataGridList.DisplayLayout.Bands[0].Columns[0].MergedCellStyle = MergedCellStyle.Always;
					dataGridList.DisplayLayout.Bands[0].Columns[0].CellAppearance.BackColor = Color.FromArgb(205, 225, 250);
					dataGridList.DisplayLayout.Bands[0].Columns[1].CellAppearance.BackColor = Color.FromArgb(205, 225, 250);
					dataGridList.DisplayLayout.Bands[0].Columns[0].Header.Fixed = true;
					dataGridList.DisplayLayout.Bands[0].Columns[1].Header.Fixed = true;
				}
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonPrint_Click(object sender, EventArgs e)
		{
			Print();
		}

		private string GetDocumentTitle()
		{
			return "Item List";
		}

		private void Print()
		{
			try
			{
				PrintHelper.PreviewDocument(ultraGridPrintDocument1, Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.MatrixQuantityForm));
			panelButtons = new System.Windows.Forms.Panel();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			labelMatrixItem = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			dataGridList = new Micromind.UISupport.DataGridList(components);
			panel1 = new System.Windows.Forms.Panel();
			labelUnitPrice4 = new System.Windows.Forms.Label();
			labelPrice4 = new System.Windows.Forms.Label();
			labelUnitPrice3 = new System.Windows.Forms.Label();
			labelPrice3 = new System.Windows.Forms.Label();
			labelUnitPrice2 = new System.Windows.Forms.Label();
			labelPrice2 = new System.Windows.Forms.Label();
			labelUnitPrice1 = new System.Windows.Forms.Label();
			labelPrice1 = new System.Windows.Forms.Label();
			labelDescription = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			labelItemCode = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			buttonPrint = new System.Windows.Forms.Button();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			panel1.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(buttonPrint);
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 359);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(628, 40);
			panelButtons.TabIndex = 4;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(518, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 3;
			buttonOK.Text = "&Close";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(628, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			labelMatrixItem.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelMatrixItem.Location = new System.Drawing.Point(98, 8);
			labelMatrixItem.Name = "labelMatrixItem";
			labelMatrixItem.Size = new System.Drawing.Size(419, 19);
			labelMatrixItem.TabIndex = 11;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(12, 9);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(81, 13);
			label3.TabIndex = 10;
			label3.Text = "Matrix Item: ";
			dataGridList.AllowUnfittedView = false;
			dataGridList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridList.DisplayLayout.Appearance = appearance;
			dataGridList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridList.DisplayLayout.MaxColScrollRegions = 1;
			dataGridList.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridList.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridList.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridList.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridList.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridList.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridList.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridList.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridList.Location = new System.Drawing.Point(12, 30);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(430, 323);
			dataGridList.TabIndex = 291;
			dataGridList.Text = "dataGridList1";
			panel1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			panel1.Controls.Add(labelUnitPrice4);
			panel1.Controls.Add(labelPrice4);
			panel1.Controls.Add(labelUnitPrice3);
			panel1.Controls.Add(labelPrice3);
			panel1.Controls.Add(labelUnitPrice2);
			panel1.Controls.Add(labelPrice2);
			panel1.Controls.Add(labelUnitPrice1);
			panel1.Controls.Add(labelPrice1);
			panel1.Controls.Add(labelDescription);
			panel1.Controls.Add(label4);
			panel1.Controls.Add(labelItemCode);
			panel1.Controls.Add(label1);
			panel1.Location = new System.Drawing.Point(451, 31);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(176, 279);
			panel1.TabIndex = 292;
			labelUnitPrice4.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			labelUnitPrice4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelUnitPrice4.Location = new System.Drawing.Point(105, 176);
			labelUnitPrice4.Name = "labelUnitPrice4";
			labelUnitPrice4.Size = new System.Drawing.Size(61, 16);
			labelUnitPrice4.TabIndex = 24;
			labelUnitPrice4.Text = "0";
			labelUnitPrice4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			labelPrice4.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			labelPrice4.AutoSize = true;
			labelPrice4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelPrice4.Location = new System.Drawing.Point(1, 175);
			labelPrice4.Name = "labelPrice4";
			labelPrice4.Size = new System.Drawing.Size(61, 13);
			labelPrice4.TabIndex = 23;
			labelPrice4.Text = "Min Price:";
			labelUnitPrice3.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			labelUnitPrice3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelUnitPrice3.Location = new System.Drawing.Point(105, 156);
			labelUnitPrice3.Name = "labelUnitPrice3";
			labelUnitPrice3.Size = new System.Drawing.Size(61, 16);
			labelUnitPrice3.TabIndex = 24;
			labelUnitPrice3.Text = "0";
			labelUnitPrice3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			labelPrice3.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			labelPrice3.AutoSize = true;
			labelPrice3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelPrice3.Location = new System.Drawing.Point(1, 155);
			labelPrice3.Name = "labelPrice3";
			labelPrice3.Size = new System.Drawing.Size(64, 13);
			labelPrice3.TabIndex = 23;
			labelPrice3.Text = "Unit Price:";
			labelUnitPrice2.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			labelUnitPrice2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelUnitPrice2.Location = new System.Drawing.Point(105, 138);
			labelUnitPrice2.Name = "labelUnitPrice2";
			labelUnitPrice2.Size = new System.Drawing.Size(61, 16);
			labelUnitPrice2.TabIndex = 24;
			labelUnitPrice2.Text = "0";
			labelUnitPrice2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			labelPrice2.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			labelPrice2.AutoSize = true;
			labelPrice2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelPrice2.Location = new System.Drawing.Point(1, 137);
			labelPrice2.Name = "labelPrice2";
			labelPrice2.Size = new System.Drawing.Size(64, 13);
			labelPrice2.TabIndex = 23;
			labelPrice2.Text = "Unit Price:";
			labelUnitPrice1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			labelUnitPrice1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelUnitPrice1.Location = new System.Drawing.Point(102, 120);
			labelUnitPrice1.Name = "labelUnitPrice1";
			labelUnitPrice1.Size = new System.Drawing.Size(64, 16);
			labelUnitPrice1.TabIndex = 22;
			labelUnitPrice1.Text = "0";
			labelUnitPrice1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			labelPrice1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			labelPrice1.AutoSize = true;
			labelPrice1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelPrice1.Location = new System.Drawing.Point(1, 119);
			labelPrice1.Name = "labelPrice1";
			labelPrice1.Size = new System.Drawing.Size(64, 13);
			labelPrice1.TabIndex = 20;
			labelPrice1.Text = "Unit Price:";
			labelDescription.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			labelDescription.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelDescription.Location = new System.Drawing.Point(1, 75);
			labelDescription.Name = "labelDescription";
			labelDescription.Size = new System.Drawing.Size(158, 35);
			labelDescription.TabIndex = 17;
			label4.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(1, 60);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(74, 13);
			label4.TabIndex = 18;
			label4.Text = "Description:";
			labelItemCode.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			labelItemCode.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelItemCode.Location = new System.Drawing.Point(3, 25);
			labelItemCode.Name = "labelItemCode";
			labelItemCode.Size = new System.Drawing.Size(158, 35);
			labelItemCode.TabIndex = 15;
			label1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(3, 10);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(69, 13);
			label1.TabIndex = 16;
			label1.Text = "Item Code:";
			ultraGridPrintDocument1.Grid = dataGridList;
			buttonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			buttonPrint.Location = new System.Drawing.Point(12, 4);
			buttonPrint.Name = "buttonPrint";
			buttonPrint.Size = new System.Drawing.Size(38, 29);
			buttonPrint.TabIndex = 16;
			buttonPrint.UseVisualStyleBackColor = true;
			buttonPrint.Click += new System.EventHandler(buttonPrint_Click);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(628, 399);
			base.Controls.Add(panel1);
			base.Controls.Add(dataGridList);
			base.Controls.Add(labelMatrixItem);
			base.Controls.Add(label3);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MatrixQuantityForm";
			Text = "Matrix Quantity";
			base.Load += new System.EventHandler(MatrixQuantityForm_Load);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
