using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class JVEntryGrid : DataEntryGrid
	{
		private DataTable table;

		private IContainer components;

		private JVRowTypeCombo jvRowTypeCombo1;

		private AllAccountsComboBox accountsCombo1;

		private customersFlatComboBox customersCombo1;

		private vendorsFlatComboBox vendorsCombo1;

		private AnalysisComboBox analysisCombo1;

		private EmployeeComboBox employeesFlatComboBox1;

		public JVEntryGrid()
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
			table.Columns.Add("Account Code", typeof(string));
			table.Columns.Add("Account Name", typeof(string));
			table.Columns.Add("Analysis", typeof(string));
			table.Columns.Add("Description", typeof(string));
			table.Columns.Add("Debit", typeof(decimal));
			table.Columns.Add("Credit", typeof(decimal));
			base.DataSource = table;
			SetupUI();
			base.CellChange += dataEntryGrid1_CellChange;
			base.AfterCellUpdate += JVEntryGrid_AfterCellUpdate;
			base.BeforeCellDeactivate += InvoiceEntryGrid_BeforeCellDeactivate;
		}

		private void JVEntryGrid_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "Type")
			{
				byte result = 1;
				byte.TryParse(e.Cell.Value.ToString(), out result);
				switch (result)
				{
				case 1:
					base.DisplayLayout.Bands[0].Columns["Account Code"].ValueList = accountsCombo1;
					break;
				case 2:
					base.DisplayLayout.Bands[0].Columns["Account Code"].ValueList = customersCombo1;
					break;
				case 3:
					base.DisplayLayout.Bands[0].Columns["Account Code"].ValueList = vendorsCombo1;
					break;
				case 4:
					base.DisplayLayout.Bands[0].Columns["Account Code"].ValueList = employeesFlatComboBox1;
					break;
				}
				e.Cell.Row.Cells["Account Code"].Value = "";
			}
			if (e.Cell.Column.Key == "Account Code")
			{
				if (e.Cell.Row.Cells["Type"].Value.ToString() == ((byte)1).ToString())
				{
					e.Cell.Row.Cells["Description"].Value = accountsCombo1.GetSelectedItemName();
				}
				if (e.Cell.Row.Cells["Type"].Value.ToString() == ((byte)2).ToString())
				{
					e.Cell.Row.Cells["Description"].Value = customersCombo1.GetSelectedItemName();
				}
				if (e.Cell.Row.Cells["Type"].Value.ToString() == ((byte)3).ToString())
				{
					e.Cell.Row.Cells["Description"].Value = vendorsCombo1.GetSelectedItemName();
				}
				if (e.Cell.Row.Cells["Type"].Value.ToString() == ((byte)4).ToString())
				{
					e.Cell.Row.Cells["Description"].Value = employeesFlatComboBox1.GetSelectedItemName();
				}
			}
			if (e.Cell.Column.Key == "Debit" && e.Cell.Row.Cells["Debit"].Text != "")
			{
				e.Cell.Row.Cells["Credit"].Value = DBNull.Value;
			}
			if (e.Cell.Column.Key == "Credit" && e.Cell.Row.Cells["Credit"].Text != "")
			{
				e.Cell.Row.Cells["Debit"].Value = DBNull.Value;
			}
		}

		private void CalculateTotal(UltraGridRow row, bool fromTotal)
		{
		}

		private void InvoiceEntryGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridCell activeCell = base.ActiveCell;
			if (activeCell != null && (activeCell.Column.Key == "Debit" || activeCell.Column.Key == "Credit") && activeCell.Value != DBNull.Value)
			{
				activeCell.Value = Math.Round(decimal.Parse(activeCell.Value.ToString()), 2);
			}
		}

		public void SetupGrid()
		{
			SetupUI();
			BindData();
			jvRowTypeCombo1.LoadData();
			jvRowTypeCombo1.LoadData();
			base.DisplayLayout.Bands[0].Columns["Type"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
			base.DisplayLayout.Bands[0].Columns["Type"].ValueList = jvRowTypeCombo1;
			base.DisplayLayout.Bands[0].Columns["Account Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			base.DisplayLayout.Bands[0].Columns["Account Code"].ValueList = accountsCombo1;
			base.DisplayLayout.Bands[0].Columns["Analysis"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			base.DisplayLayout.Bands[0].Columns["Analysis"].ValueList = analysisCombo1;
			base.DisplayLayout.Bands[0].Columns["Debit"].Format = "#,##0.00";
			base.DisplayLayout.Bands[0].Columns["Debit"].MaxValue = "9999999999.99";
			base.DisplayLayout.Bands[0].Columns["Debit"].MinValue = "0";
			base.DisplayLayout.Bands[0].Columns["Debit"].CellAppearance.TextHAlign = HAlign.Right;
			base.DisplayLayout.Bands[0].Columns["Credit"].Format = "#,##0.00";
			base.DisplayLayout.Bands[0].Columns["Credit"].MaxValue = "9999999999.99";
			base.DisplayLayout.Bands[0].Columns["Credit"].MinValue = "0";
			base.DisplayLayout.Bands[0].Columns["Credit"].CellAppearance.TextHAlign = HAlign.Right;
			BeginUpdate();
			EndUpdate();
		}

		private void dataEntryGrid1_CellChange(object sender, CellEventArgs e)
		{
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
			jvRowTypeCombo1 = new Micromind.DataControls.JVRowTypeCombo();
			accountsCombo1 = new Micromind.DataControls.AllAccountsComboBox();
			customersCombo1 = new Micromind.DataControls.customersFlatComboBox();
			vendorsCombo1 = new Micromind.DataControls.vendorsFlatComboBox();
			analysisCombo1 = new Micromind.DataControls.AnalysisComboBox();
			employeesFlatComboBox1 = new Micromind.DataControls.EmployeeComboBox();
			((System.ComponentModel.ISupportInitialize)jvRowTypeCombo1).BeginInit();
			((System.ComponentModel.ISupportInitialize)accountsCombo1).BeginInit();
			((System.ComponentModel.ISupportInitialize)customersCombo1).BeginInit();
			((System.ComponentModel.ISupportInitialize)vendorsCombo1).BeginInit();
			((System.ComponentModel.ISupportInitialize)analysisCombo1).BeginInit();
			((System.ComponentModel.ISupportInitialize)employeesFlatComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this).BeginInit();
			SuspendLayout();
			jvRowTypeCombo1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			jvRowTypeCombo1.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
			jvRowTypeCombo1.Location = new System.Drawing.Point(0, 0);
			jvRowTypeCombo1.Name = "jvRowTypeCombo1";
			jvRowTypeCombo1.Size = new System.Drawing.Size(121, 22);
			jvRowTypeCombo1.TabIndex = 0;
			jvRowTypeCombo1.Visible = false;
			accountsCombo1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			accountsCombo1.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
			accountsCombo1.Editable = true;
			accountsCombo1.HasAllAccount = false;
			accountsCombo1.HasCustom = false;
			accountsCombo1.Location = new System.Drawing.Point(0, 0);
			accountsCombo1.Name = "accountsCombo1";
			accountsCombo1.SelectedIndex = -1;
			accountsCombo1.ShowInactiveItems = false;
			accountsCombo1.ShowQuickAdd = true;
			accountsCombo1.Size = new System.Drawing.Size(100, 21);
			accountsCombo1.TabIndex = 0;
			accountsCombo1.Text = "accountsCombo1";
			accountsCombo1.Visible = false;
			customersCombo1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			customersCombo1.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
			customersCombo1.Editable = true;
			customersCombo1.HasCustom = false;
			customersCombo1.Location = new System.Drawing.Point(0, 0);
			customersCombo1.Name = "customersCombo1";
			customersCombo1.SelectedIndex = -1;
			customersCombo1.ShowQuickAdd = true;
			customersCombo1.Size = new System.Drawing.Size(100, 21);
			customersCombo1.TabIndex = 0;
			customersCombo1.Text = "customersCombo1";
			customersCombo1.Visible = false;
			vendorsCombo1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			vendorsCombo1.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
			vendorsCombo1.Editable = true;
			vendorsCombo1.HasCustom = false;
			vendorsCombo1.Location = new System.Drawing.Point(0, 0);
			vendorsCombo1.Name = "vendorsCombo1";
			vendorsCombo1.SelectedIndex = -1;
			vendorsCombo1.ShowQuickAdd = true;
			vendorsCombo1.Size = new System.Drawing.Size(100, 21);
			vendorsCombo1.TabIndex = 0;
			vendorsCombo1.Text = "vendorsCombo1";
			vendorsCombo1.Visible = false;
			analysisCombo1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			analysisCombo1.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
			analysisCombo1.Editable = true;
			analysisCombo1.HasAllAccount = false;
			analysisCombo1.HasCustom = false;
			analysisCombo1.Location = new System.Drawing.Point(0, 0);
			analysisCombo1.Name = "analysisCombo1";
			analysisCombo1.SelectedIndex = -1;
			analysisCombo1.ShowInactiveItems = false;
			analysisCombo1.ShowQuickAdd = true;
			analysisCombo1.Size = new System.Drawing.Size(100, 21);
			analysisCombo1.TabIndex = 0;
			analysisCombo1.Text = "analysisCombo1";
			employeesFlatComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			employeesFlatComboBox1.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
			employeesFlatComboBox1.Editable = true;
			employeesFlatComboBox1.HasCustom = false;
			employeesFlatComboBox1.Location = new System.Drawing.Point(0, 0);
			employeesFlatComboBox1.Name = "employeesFlatComboBox1";
			employeesFlatComboBox1.SelectedIndex = -1;
			employeesFlatComboBox1.ShowQuickAdd = true;
			employeesFlatComboBox1.Size = new System.Drawing.Size(288, 22);
			employeesFlatComboBox1.TabIndex = 0;
			employeesFlatComboBox1.Text = "employeesFlatComboBox1";
			employeesFlatComboBox1.Visible = false;
			((System.ComponentModel.ISupportInitialize)jvRowTypeCombo1).EndInit();
			((System.ComponentModel.ISupportInitialize)accountsCombo1).EndInit();
			((System.ComponentModel.ISupportInitialize)customersCombo1).EndInit();
			((System.ComponentModel.ISupportInitialize)vendorsCombo1).EndInit();
			((System.ComponentModel.ISupportInitialize)analysisCombo1).EndInit();
			((System.ComponentModel.ISupportInitialize)employeesFlatComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)this).EndInit();
			ResumeLayout(false);
		}
	}
}
