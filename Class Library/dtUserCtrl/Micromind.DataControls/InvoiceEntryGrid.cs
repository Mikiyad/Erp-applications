using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class InvoiceEntryGrid : DataEntryGrid
	{
		private DataTable table;

		private IContainer components;

		private AnalysisGroupComboBox itemCombo1;

		private InvoiceRowTypeCombo invoiceRowTypeCombo1;

		private AnalysisGroupComboBox accountsCombo1;

		public InvoiceEntryGrid()
		{
			InitializeComponent();
			base.BeforeCellActivate += InvoiceEntryGrid_BeforeCellActivate;
		}

		private void InvoiceEntryGrid_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
			if (e.Cell.Column.Key == "Type" && e.Cell.Text == "")
			{
				e.Cell.Value = InvoiceLineTypes.Item;
			}
		}

		public void BindData()
		{
			table = new DataTable();
			table.Columns.Add("Type", typeof(byte));
			table.Columns.Add("ItemID", typeof(string));
			table.Columns.Add("Description", typeof(string));
			table.Columns.Add("Quantity", typeof(decimal));
			table.Columns.Add("UnitID", typeof(string));
			table.Columns.Add("Price", typeof(decimal));
			table.Columns.Add("Total", typeof(decimal));
			base.DataSource = table;
			SetupUI();
			base.CellChange += dataEntryGrid1_CellChange;
			base.BeforeCellDeactivate += InvoiceEntryGrid_BeforeCellDeactivate;
		}

		private void CalculateTotal(UltraGridRow row, bool fromTotal)
		{
			try
			{
				decimal num = default(decimal);
				decimal num2 = default(decimal);
				decimal d = default(decimal);
				if (base.Rows[row.Index].Cells["Quantity"].Text.Trim() != "")
				{
					num2 = decimal.Parse(base.Rows[row.Index].Cells["Quantity"].Text);
				}
				if (base.Rows[row.Index].Cells["Price"].Text.Trim() != "")
				{
					num = decimal.Parse(base.Rows[row.Index].Cells["Price"].Text);
				}
				if (base.Rows[row.Index].Cells["Total"].Text.Trim() != "")
				{
					d = decimal.Parse(base.Rows[row.Index].Cells["Total"].Text);
				}
				if (!fromTotal)
				{
					d = num2 * num;
					d = Math.Round(d, 2);
					base.Rows[row.Index].Cells["Total"].Value = d.ToString();
				}
				else
				{
					if (num2 != 0m)
					{
						num = d / num2;
					}
					num = Math.Round(num, 2);
					base.Rows[row.Index].Cells["Price"].Value = num.ToString();
				}
			}
			catch
			{
			}
		}

		private void InvoiceEntryGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridCell activeCell = base.ActiveCell;
			if (activeCell != null && (activeCell.Column.Key == "Quantity" || activeCell.Column.Key == "Price" || activeCell.Column.Key == "Total"))
			{
				if (activeCell.Text.Trim() == "")
				{
					activeCell.Value = 0;
				}
				activeCell.Value = Math.Round(decimal.Parse(activeCell.Value.ToString()), 2);
			}
		}

		public void SetupGrid()
		{
			SetupUI();
			BindData();
			itemCombo1.LoadData();
			invoiceRowTypeCombo1.LoadData();
			accountsCombo1.LoadData();
			base.DisplayLayout.Bands[0].Columns["Type"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
			base.DisplayLayout.Bands[0].Columns["Type"].ValueList = invoiceRowTypeCombo1;
			base.DisplayLayout.Bands[0].Columns["ItemID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			base.DisplayLayout.Bands[0].Columns["ItemID"].ValueList = itemCombo1;
			base.DisplayLayout.Bands[0].Columns["Quantity"].Format = "#,##0.##";
			base.DisplayLayout.Bands[0].Columns["Quantity"].MaxValue = "999999.99";
			base.DisplayLayout.Bands[0].Columns["Quantity"].MinValue = "0";
			base.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
			base.DisplayLayout.Bands[0].Columns["Price"].Format = "#,##0.00";
			base.DisplayLayout.Bands[0].Columns["Price"].MaxValue = "9999999999.99";
			base.DisplayLayout.Bands[0].Columns["Price"].MinValue = "0";
			base.DisplayLayout.Bands[0].Columns["Price"].CellAppearance.TextHAlign = HAlign.Right;
			base.DisplayLayout.Bands[0].Columns["Total"].Format = "#,##0.00";
			base.DisplayLayout.Bands[0].Columns["Total"].MaxValue = "999999999999.99";
			base.DisplayLayout.Bands[0].Columns["Total"].MinValue = "0";
			base.DisplayLayout.Bands[0].Columns["Total"].CellAppearance.TextHAlign = HAlign.Right;
			BeginUpdate();
			EndUpdate();
		}

		private void dataEntryGrid1_CellChange(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "Type")
			{
				byte result = 1;
				byte.TryParse(e.Cell.Value.ToString(), out result);
				for (int i = 0; i < base.DisplayLayout.Bands[0].Columns.Count; i++)
				{
					e.Cell.Row.Cells[i].Hidden = false;
				}
				switch (result)
				{
				case 1:
					base.DisplayLayout.Bands[0].Columns["ItemID"].ValueList = itemCombo1;
					break;
				case 2:
					base.DisplayLayout.Bands[0].Columns["ItemID"].ValueList = accountsCombo1;
					break;
				case 3:
					e.Cell.Row.Cells["ItemID"].Activation = Activation.Disabled;
					e.Cell.Row.Cells["ItemID"].Value = "";
					e.Cell.Row.Cells["Quantity"].Activation = Activation.Disabled;
					e.Cell.Row.Cells["Quantity"].Value = DBNull.Value;
					e.Cell.Row.Cells["Price"].Activation = Activation.Disabled;
					e.Cell.Row.Cells["Price"].Value = DBNull.Value;
					e.Cell.Row.Cells["UnitID"].Activation = Activation.Disabled;
					e.Cell.Row.Cells["UnitID"].Value = "";
					e.Cell.Row.Cells["Total"].Activation = Activation.Disabled;
					e.Cell.Row.Cells["Total"].Value = DBNull.Value;
					break;
				case 4:
					e.Cell.Row.Cells["Quantity"].Activation = Activation.Disabled;
					e.Cell.Row.Cells["Quantity"].Value = DBNull.Value;
					e.Cell.Row.Cells["Price"].Activation = Activation.Disabled;
					e.Cell.Row.Cells["Price"].Value = DBNull.Value;
					e.Cell.Row.Cells["UnitID"].Activation = Activation.Disabled;
					e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
					e.Cell.Row.Cells["Total"].Activation = Activation.Disabled;
					e.Cell.Row.Cells["Total"].Value = DBNull.Value;
					break;
				}
				e.Cell.Row.Cells["ItemID"].Value = "";
			}
			if (e.Cell.Column.Key == "ItemID")
			{
				_ = (e.Cell.Row.Cells["Type"].Value.ToString() == ((byte)1).ToString());
			}
			if (e.Cell.Column.Key == "Total")
			{
				CalculateTotal(e.Cell.Row, fromTotal: true);
			}
			else
			{
				CalculateTotal(e.Cell.Row, fromTotal: false);
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
			itemCombo1 = new Micromind.DataControls.AnalysisGroupComboBox();
			invoiceRowTypeCombo1 = new Micromind.DataControls.InvoiceRowTypeCombo();
			accountsCombo1 = new Micromind.DataControls.AnalysisGroupComboBox();
			((System.ComponentModel.ISupportInitialize)itemCombo1).BeginInit();
			((System.ComponentModel.ISupportInitialize)invoiceRowTypeCombo1).BeginInit();
			((System.ComponentModel.ISupportInitialize)accountsCombo1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this).BeginInit();
			SuspendLayout();
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			itemCombo1.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			itemCombo1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			itemCombo1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			itemCombo1.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			itemCombo1.DisplayLayout.MaxColScrollRegions = 1;
			itemCombo1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			itemCombo1.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			itemCombo1.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			itemCombo1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			itemCombo1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			itemCombo1.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			itemCombo1.DisplayLayout.Override.CellAppearance = appearance8;
			itemCombo1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			itemCombo1.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			itemCombo1.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			itemCombo1.DisplayLayout.Override.HeaderAppearance = appearance10;
			itemCombo1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			itemCombo1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			itemCombo1.DisplayLayout.Override.RowAppearance = appearance11;
			itemCombo1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			itemCombo1.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			itemCombo1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			itemCombo1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			itemCombo1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			itemCombo1.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
			itemCombo1.Location = new System.Drawing.Point(453, 472);
			itemCombo1.Name = "itemCombo1";
			itemCombo1.Size = new System.Drawing.Size(108, 22);
			itemCombo1.TabIndex = 15;
			itemCombo1.Text = "itemCombo1";
			itemCombo1.Visible = false;
			invoiceRowTypeCombo1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			invoiceRowTypeCombo1.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
			invoiceRowTypeCombo1.Location = new System.Drawing.Point(0, 0);
			invoiceRowTypeCombo1.Name = "invoiceRowTypeCombo1";
			invoiceRowTypeCombo1.Size = new System.Drawing.Size(121, 22);
			invoiceRowTypeCombo1.TabIndex = 0;
			invoiceRowTypeCombo1.Visible = false;
			accountsCombo1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			accountsCombo1.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
			accountsCombo1.Location = new System.Drawing.Point(0, 0);
			accountsCombo1.Name = "accountsCombo1";
			accountsCombo1.Size = new System.Drawing.Size(100, 21);
			accountsCombo1.TabIndex = 0;
			accountsCombo1.Text = "accountsCombo1";
			accountsCombo1.Visible = false;
			((System.ComponentModel.ISupportInitialize)itemCombo1).EndInit();
			((System.ComponentModel.ISupportInitialize)invoiceRowTypeCombo1).EndInit();
			((System.ComponentModel.ISupportInitialize)accountsCombo1).EndInit();
			((System.ComponentModel.ISupportInitialize)this).EndInit();
			ResumeLayout(false);
		}
	}
}
