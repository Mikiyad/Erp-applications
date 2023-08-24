using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.DataControls.MMSDataGrid;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.POS
{
	public class ItemLookupDialog : Form
	{
		public enum LookupItemTypes
		{
			Customer,
			Product,
			HoldReceipt
		}

		public bool CanClose = true;

		private bool isMultiSelect;

		private string location = string.Empty;

		private LookupItemTypes lookupType;

		private string selectedItem = "";

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private TextBox textBoxSearch;

		private DefaultLookAndFeel defaultLookAndFeel1;

		private SimpleButton buttonOK;

		private SimpleButton buttonCancel;

		private Label label6;

		private POSGrid dataGridItems;

		public bool IsMultiSelect
		{
			get
			{
				return isMultiSelect;
			}
			set
			{
				isMultiSelect = value;
			}
		}

		public LookupItemTypes LookupType
		{
			get
			{
				return lookupType;
			}
			set
			{
				lookupType = value;
				if (lookupType == LookupItemTypes.Customer)
				{
					Text = "Select Customer";
				}
				else if (lookupType == LookupItemTypes.Product)
				{
					Text = "Select Item";
				}
				else if (lookupType == LookupItemTypes.HoldReceipt)
				{
					Text = "Select Receipt";
				}
			}
		}

		public UltraGridRow SelectedRow
		{
			get
			{
				if (base.DialogResult == DialogResult.Cancel)
				{
					return null;
				}
				if (dataGridItems.DataGrid.ActiveRow != null)
				{
					return dataGridItems.DataGrid.ActiveRow;
				}
				return null;
			}
		}

		public List<UltraGridRow> SelectedRows
		{
			get
			{
				if (base.DialogResult == DialogResult.Cancel)
				{
					return null;
				}
				List<UltraGridRow> list = new List<UltraGridRow>();
				if (IsMultiSelect)
				{
					foreach (UltraGridRow row in dataGridItems.DataGrid.Rows)
					{
						if (row.Cells["C"].Value.ToString() == "True")
						{
							list.Add(row);
						}
					}
				}
				if (list.Count == 0 && dataGridItems.DataGrid.ActiveRow != null)
				{
					list.Add(dataGridItems.DataGrid.ActiveRow);
				}
				return list;
			}
		}

		public UltraGrid Grid => dataGridItems.DataGrid;

		public string SelectedItem
		{
			get
			{
				return selectedItem;
			}
			set
			{
				selectedItem = value;
			}
		}

		public string SysLocation
		{
			get
			{
				return location;
			}
			set
			{
				location = value;
			}
		}

		public event EventHandler ValidateSelection;

		public ItemLookupDialog()
		{
			InitializeComponent();
			base.Load += SelectDocumentDialog_Load;
			textBoxSearch.KeyDown += textBoxSearch_KeyDown;
			base.Activated += SelectDocumentDialog_Activated;
			dataGridItems.DataGrid.DoubleClickRow += dataGridItems_DoubleClickRow;
			base.AcceptButton = buttonOK;
			base.CancelButton = buttonCancel;
			base.FormClosing += Form_FormClosing;
		}

		private void SelectDocumentDialog_Activated(object sender, EventArgs e)
		{
			textBoxSearch.Focus();
		}

		private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
		{
			checked
			{
				if (e.KeyCode == Keys.Down)
				{
					if (dataGridItems.DataGrid.ActiveRow != null)
					{
						int visibleIndex = dataGridItems.DataGrid.ActiveRow.VisibleIndex;
						if (visibleIndex < dataGridItems.DataGrid.Rows.VisibleRowCount)
						{
							dataGridItems.DataGrid.ActiveRow = dataGridItems.DataGrid.Rows.GetRowAtVisibleIndex(visibleIndex + 1);
						}
						e.SuppressKeyPress = true;
					}
				}
				else if (e.KeyCode == Keys.Up && dataGridItems.DataGrid.ActiveRow != null)
				{
					int visibleIndex2 = dataGridItems.DataGrid.ActiveRow.VisibleIndex;
					if (visibleIndex2 > 0)
					{
						dataGridItems.DataGrid.ActiveRow = dataGridItems.DataGrid.Rows.GetRowAtVisibleIndex(visibleIndex2 - 1);
					}
					e.SuppressKeyPress = true;
				}
			}
		}

		private void SelectDocumentDialog_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridItems.SetupUI();
				LoadData();
				dataGridItems.DataGrid.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
				dataGridItems.DataGrid.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
				if (SelectedItem != "")
				{
					textBoxSearch.Text = SelectedItem.Trim();
				}
				textBoxSearch.Focus();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public string GetSelectedCode(string codeColumnName)
		{
			if (SelectedRow != null)
			{
				return SelectedRow.Cells[codeColumnName].Value.ToString();
			}
			return "";
		}

		private void textBoxSearch_TextChanged(object sender, EventArgs e)
		{
			if (dataGridItems.DataGrid.DataSource != null && dataGridItems.DataGrid.DisplayLayout.Bands[0].Columns.Exists("SearchColumn"))
			{
				dataGridItems.DataGrid.BeginUpdate();
				if (dataGridItems.DataGrid.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions.Count == 0)
				{
					dataGridItems.DataGrid.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions.Add(new FilterCondition());
				}
				dataGridItems.DataGrid.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions[0].ComparisionOperator = FilterComparisionOperator.Contains;
				dataGridItems.DataGrid.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions[0].CompareValue = textBoxSearch.Text;
				dataGridItems.DataGrid.EndUpdate();
				if (dataGridItems.DataGrid.Rows.VisibleRowCount > 0)
				{
					dataGridItems.DataGrid.ActiveRow = dataGridItems.DataGrid.Rows.GetRowAtVisibleIndex(0);
				}
			}
		}

		public void LoadData()
		{
			DataSet dataSet = null;
			if (lookupType == LookupItemTypes.Product)
			{
				dataSet = Factory.ProductSystem.GetProductListPOS(SysLocation);
			}
			else if (lookupType == LookupItemTypes.Customer)
			{
				dataSet = Factory.CustomerSystem.GetPOSCustomerList();
			}
			else if (lookupType == LookupItemTypes.HoldReceipt)
			{
				dataSet = Factory.POSHoldSystem.GetHoldDocumentList(Global.CurrentCashRegisterID);
			}
			dataGridItems.DataGrid.DataSource = dataSet;
			if (!dataSet.Tables[0].Columns.Contains("SearchColumn") && dataSet != null && dataSet.Tables.Count > 0)
			{
				DataTable dataTable = dataSet.Tables[0];
				dataTable.Columns.Add("SearchColumn");
				string text = "";
				foreach (DataRow row in dataTable.Rows)
				{
					text = "";
					foreach (DataColumn column in dataTable.Columns)
					{
						if (!(column.ColumnName == "SearchColumn"))
						{
							text = text + row[column].ToString() + " ";
						}
					}
					row["SearchColumn"] = text;
				}
			}
			dataGridItems.SuspendLayout();
			dataGridItems.DataGrid.DataSource = dataSet.Tables[0];
			dataGridItems.DataGrid.DisplayLayout.Bands[0].Columns["SearchColumn"].Hidden = true;
			if (dataGridItems.DataGrid.DisplayLayout.Bands[0].Columns.Exists("Description"))
			{
				dataGridItems.DataGrid.DisplayLayout.Bands[0].Columns["Description"].MinWidth = checked(50 * dataGridItems.Width) / 100;
			}
			dataGridItems.DataGrid.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
			dataGridItems.ResumeLayout();
			if (IsMultiSelect && !dataGridItems.DataGrid.DisplayLayout.Bands[0].Columns.Exists("C"))
			{
				UltraGridColumn ultraGridColumn = dataGridItems.DataGrid.DisplayLayout.Bands[0].Columns.Insert(0, "C");
				ultraGridColumn.DataType = typeof(bool);
				ultraGridColumn.CellActivation = Activation.AllowEdit;
				ultraGridColumn.CellClickAction = CellClickAction.Edit;
				ultraGridColumn.CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
				ultraGridColumn.Width = 18;
				ultraGridColumn.MinWidth = 18;
				ultraGridColumn.MaxWidth = 18;
				ultraGridColumn.LockedWidth = true;
				ultraGridColumn.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
			}
			if (lookupType == LookupItemTypes.Product)
			{
				dataGridItems.DataGrid.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;
				dataGridItems.DataGrid.DisplayLayout.Bands[0].Columns["TaxGroupID"].Hidden = true;
				dataGridItems.DataGrid.DisplayLayout.Bands[0].Columns["TaxOption"].Hidden = true;
			}
			else if (lookupType == LookupItemTypes.Customer)
			{
				dataGridItems.DataGrid.DisplayLayout.Bands[0].Columns["TaxGroupID"].Hidden = true;
				dataGridItems.DataGrid.DisplayLayout.Bands[0].Columns["TaxOption"].Hidden = true;
			}
			dataGridItems.ApplyFormat();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			CanClose = true;
			if (this.ValidateSelection != null)
			{
				this.ValidateSelection(this, null);
			}
			if (CanClose)
			{
				Close();
			}
		}

		private void Form_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void dataGridItems_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
		{
			SelectItem();
		}

		private void SelectItem()
		{
			CanClose = true;
			if (dataGridItems.DataGrid.Rows.VisibleRowCount == 0 || dataGridItems.DataGrid.ActiveRow == null)
			{
				ErrorHelper.InformationMessage(UIMessages.SelectAnItemFirst);
				return;
			}
			if (this.ValidateSelection != null)
			{
				this.ValidateSelection(this, null);
			}
			if (CanClose && dataGridItems.DataGrid.ActiveRow != null)
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			SelectItem();
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
			panelButtons = new System.Windows.Forms.Panel();
			buttonCancel = new DevExpress.XtraEditors.SimpleButton();
			buttonOK = new DevExpress.XtraEditors.SimpleButton();
			linePanelDown = new Micromind.UISupport.Line();
			textBoxSearch = new System.Windows.Forms.TextBox();
			defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(components);
			label6 = new System.Windows.Forms.Label();
			dataGridItems = new Micromind.DataControls.MMSDataGrid.POSGrid();
			panelButtons.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 366);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(601, 56);
			panelButtons.TabIndex = 3;
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonCancel.Appearance.Options.UseFont = true;
			buttonCancel.Location = new System.Drawing.Point(490, 9);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 40);
			buttonCancel.TabIndex = 29;
			buttonCancel.Text = "&Cancel";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonOK.Appearance.Options.UseFont = true;
			buttonOK.Location = new System.Drawing.Point(389, 9);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 40);
			buttonOK.TabIndex = 29;
			buttonOK.Text = "&OK";
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(601, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxSearch.Location = new System.Drawing.Point(57, 13);
			textBoxSearch.Name = "textBoxSearch";
			textBoxSearch.Size = new System.Drawing.Size(448, 35);
			textBoxSearch.TabIndex = 4;
			textBoxSearch.TextChanged += new System.EventHandler(textBoxSearch_TextChanged);
			defaultLookAndFeel1.LookAndFeel.SkinName = "Money Twins";
			label6.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label6.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			label6.Location = new System.Drawing.Point(12, 21);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(39, 19);
			label6.TabIndex = 21;
			label6.Text = "Find:";
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridItems.Location = new System.Drawing.Point(15, 57);
			dataGridItems.Margin = new System.Windows.Forms.Padding(6);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowRowButtons = false;
			dataGridItems.Size = new System.Drawing.Size(571, 300);
			dataGridItems.TabIndex = 22;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.FromArgb(227, 241, 254);
			base.ClientSize = new System.Drawing.Size(601, 422);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(label6);
			base.Controls.Add(textBoxSearch);
			base.Controls.Add(panelButtons);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(465, 215);
			base.Name = "ItemLookupDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Select Customer";
			panelButtons.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
