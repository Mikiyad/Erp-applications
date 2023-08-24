using DevExpress.XtraEditors.Controls;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class SelectDocumentDialogByRange : Form
	{
		public bool CanClose = true;

		private bool isMultiSelect;

		private List<string> hiddenColumns = new List<string>();

		public List<string> selectedDocuments = new List<string>();

		private DataComboType selectedCombo;

		private string fromRange = "";

		private string toRange = "";

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Micromind.UISupport.Line linePanelDown;

		private XPButton buttonCancel;

		private TextBox textBoxSearch;

		private Label label1;

		private MMSListGrid dataGridItems;

		private GadgetDateRangeComboBox comboBoxDateRange;

		private Label label3;

		private Label labelTo;

		private Panel panelCustomer;

		private Panel panelCustomerClass;

		private Label label4;

		private Label label2;

		private Label label5;

		private Label label6;

		private Panel panelCustomerArea;

		private Label label7;

		private Label label8;

		private Panel panelCountry;

		private Label label9;

		private Label label10;

		private Panel panel1;

		private Label label11;

		private CustomerClassComboBox comboBoxFromClass;

		private CustomerClassComboBox comboBoxToClass;

		private customersFlatComboBox comboBoxToCustomer;

		private customersFlatComboBox comboBoxFromCustomer;

		private CustomerGroupComboBox comboBoxToGroup;

		private CustomerGroupComboBox comboBoxFromGroup;

		private AreaComboBox comboBoxToArea;

		private AreaComboBox comboBoxFromArea;

		private CountryComboBox comboBoxToCountry;

		private CountryComboBox comboBoxFromCountry;

		private Panel panelGroup;

		private Button buttonClear;

		private string SelectedCustomerID
		{
			get;
			set;
		}

		public bool IsMultiSelect
		{
			get
			{
				return isMultiSelect;
			}
			set
			{
				isMultiSelect = value;
				if (value && dataGridItems.DataSource != null)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns.Insert(0, "C");
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].DataType = typeof(bool);
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellActivation = Activation.AllowEdit;
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellClickAction = CellClickAction.Edit;
				}
			}
		}

		public bool AllowDateFilter
		{
			get
			{
				return comboBoxDateRange.Visible;
			}
			set
			{
				comboBoxDateRange.Visible = value;
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
				if (dataGridItems.ActiveRow != null)
				{
					return dataGridItems.ActiveRow;
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
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						if (row.Cells["C"].Value.ToString() == "True")
						{
							list.Add(row);
						}
					}
					return list;
				}
				if (list.Count == 0 && !IsMultiSelect && dataGridItems.ActiveRow != null)
				{
					list.Add(dataGridItems.ActiveRow);
				}
				return list;
			}
		}

		public UltraGrid Grid => dataGridItems;

		public DataSet DataSource
		{
			get
			{
				return (DataSet)dataGridItems.DataSource;
			}
			set
			{
				if (value != null && value.Tables.Count > 0)
				{
					DataTable dataTable = value.Tables[0];
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
				dataGridItems.DataSource = value;
				dataGridItems.DisplayLayout.Bands[0].Columns["SearchColumn"].Hidden = true;
				dataGridItems.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
				if (IsMultiSelect && !dataGridItems.DisplayLayout.Bands[0].Columns.Exists("C"))
				{
					UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns.Insert(0, "C");
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
				foreach (string hiddenColumn in hiddenColumns)
				{
					if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists(hiddenColumn))
					{
						dataGridItems.DisplayLayout.Bands[0].Columns[hiddenColumn].Hidden = true;
					}
				}
			}
		}

		public List<string> HiddenColumns => hiddenColumns;

		public List<string> SelectedDocuments
		{
			get
			{
				List<string> list = new List<string>();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (row.Cells["C"] != null && bool.Parse(row.Cells["C"].Value.ToString()))
					{
						string item = row.Cells["Code"].Value.ToString() + row.Cells["Name"].Value.ToString();
						if (!list.Contains(item))
						{
							list.Add(item);
						}
					}
				}
				return list;
			}
			set
			{
				selectedDocuments = value;
				foreach (string selectedDocument in selectedDocuments)
				{
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						if (row.Cells["Code"].Value.ToString() == selectedDocument)
						{
							row.Cells["C"].Value = true;
							break;
						}
					}
				}
			}
		}

		public DataComboType SelectedCombo
		{
			get
			{
				return selectedCombo;
			}
			set
			{
				selectedCombo = value;
				switch (selectedCombo)
				{
				case DataComboType.Vendor:
				case DataComboType.Item:
				case DataComboType.Accounts:
				case DataComboType.VendorGroup:
					break;
				case DataComboType.Customer:
					panelCustomer.Visible = true;
					break;
				case DataComboType.CustomerClass:
					panelCustomerClass.Visible = true;
					break;
				case DataComboType.CustomerGroup:
					panelGroup.Visible = true;
					break;
				case DataComboType.Area:
					panelCustomerArea.Visible = true;
					break;
				case DataComboType.Country:
					panelCountry.Visible = true;
					break;
				}
			}
		}

		public string RangeFrom
		{
			get
			{
				switch (SelectedCombo)
				{
				case DataComboType.Customer:
					fromRange = comboBoxFromCustomer.SelectedID;
					break;
				case DataComboType.CustomerClass:
					fromRange = comboBoxFromClass.SelectedID;
					break;
				case DataComboType.CustomerGroup:
					fromRange = comboBoxFromGroup.SelectedID;
					break;
				case DataComboType.Area:
					fromRange = comboBoxFromArea.SelectedID;
					break;
				case DataComboType.Country:
					fromRange = comboBoxFromCountry.SelectedID;
					break;
				}
				return fromRange;
			}
			set
			{
				switch (SelectedCombo)
				{
				case DataComboType.Vendor:
				case DataComboType.Item:
				case DataComboType.Accounts:
				case DataComboType.VendorGroup:
					break;
				case DataComboType.Customer:
					comboBoxFromCustomer.SelectedID = value;
					break;
				case DataComboType.CustomerClass:
					comboBoxFromClass.SelectedID = value;
					break;
				case DataComboType.CustomerGroup:
					comboBoxFromGroup.SelectedID = value;
					break;
				case DataComboType.Area:
					comboBoxFromArea.SelectedID = value;
					break;
				case DataComboType.Country:
					comboBoxFromCountry.SelectedID = value;
					break;
				}
			}
		}

		public string RangeTo
		{
			get
			{
				switch (SelectedCombo)
				{
				case DataComboType.Customer:
					toRange = comboBoxToCustomer.SelectedID;
					break;
				case DataComboType.CustomerClass:
					toRange = comboBoxToClass.SelectedID;
					break;
				case DataComboType.CustomerGroup:
					toRange = comboBoxToGroup.SelectedID;
					break;
				case DataComboType.Area:
					toRange = comboBoxToArea.SelectedID;
					break;
				case DataComboType.Country:
					toRange = comboBoxToCountry.SelectedID;
					break;
				}
				return toRange;
			}
			set
			{
				switch (SelectedCombo)
				{
				case DataComboType.Vendor:
				case DataComboType.Item:
				case DataComboType.Accounts:
				case DataComboType.VendorGroup:
					break;
				case DataComboType.Customer:
					comboBoxToCustomer.SelectedID = value;
					break;
				case DataComboType.CustomerClass:
					comboBoxToClass.SelectedID = value;
					break;
				case DataComboType.CustomerGroup:
					comboBoxToGroup.SelectedID = value;
					break;
				case DataComboType.Area:
					comboBoxToArea.SelectedID = value;
					break;
				case DataComboType.Country:
					comboBoxToCountry.SelectedID = value;
					break;
				}
			}
		}

		public event EventHandler<DateRangeStruct> RequireDataRefresh;

		public event EventHandler ValidateSelection;

		public SelectDocumentDialogByRange()
		{
			InitializeComponent();
			base.Load += SelectTransactionDialog_Load;
			textBoxSearch.KeyDown += textBoxSearch_KeyDown;
			base.Activated += SelectTransactionDialog_Activated;
			base.FormClosing += SelectTransactionDialog_FormClosing;
			comboBoxDateRange.LoadData();
			comboBoxDateRange.SelectedIndex = UserPreferences.GetCurrentUserSetting(base.Name + "Range", 6);
			comboBoxDateRange.SelectedIndexChanged += gadgetDateRangeComboBox1_SelectedIndexChanged;
			dataGridItems.BeforeCellUpdate += dataGridItems_BeforeCellUpdate;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			_ = dataGridItems.ActiveRow;
		}

		private void dataGridItems_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			_ = dataGridItems.ActiveRow;
		}

		private void gadgetDateRangeComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.RequireDataRefresh != null)
			{
				this.RequireDataRefresh(this, comboBoxDateRange.GetRange());
			}
			UserPreferences.SaveCurrentUserSetting(base.Name + "Range", comboBoxDateRange.SelectedIndex);
		}

		private void SelectTransactionDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				Global.GlobalSettings.SaveFormProperties(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SelectTransactionDialog_Activated(object sender, EventArgs e)
		{
			textBoxSearch.Focus();
		}

		private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Down)
			{
				if (dataGridItems.ActiveRow != null)
				{
					int visibleIndex = dataGridItems.ActiveRow.VisibleIndex;
					if (visibleIndex < dataGridItems.Rows.VisibleRowCount)
					{
						dataGridItems.ActiveRow = dataGridItems.Rows.GetRowAtVisibleIndex(visibleIndex + 1);
					}
					e.SuppressKeyPress = true;
				}
			}
			else if (e.KeyCode == Keys.Up && dataGridItems.ActiveRow != null)
			{
				int visibleIndex2 = dataGridItems.ActiveRow.VisibleIndex;
				if (visibleIndex2 > 0)
				{
					dataGridItems.ActiveRow = dataGridItems.Rows.GetRowAtVisibleIndex(visibleIndex2 - 1);
				}
				e.SuppressKeyPress = true;
			}
		}

		private void SelectTransactionDialog_Load(object sender, EventArgs e)
		{
			try
			{
				Global.GlobalSettings.LoadFormProperties(this);
				if (this.RequireDataRefresh != null)
				{
					this.RequireDataRefresh(this, comboBoxDateRange.GetRange());
				}
				dataGridItems.ApplyUIDesign();
				dataGridItems.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
				textBoxSearch.Focus();
			}
			catch
			{
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
			if (DataSource != null && dataGridItems.DisplayLayout.Bands[0].Columns.Exists("SearchColumn"))
			{
				dataGridItems.BeginUpdate();
				if (dataGridItems.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions.Count == 0)
				{
					dataGridItems.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions.Add(new FilterCondition());
				}
				dataGridItems.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions[0].ComparisionOperator = FilterComparisionOperator.Contains;
				dataGridItems.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions[0].CompareValue = textBoxSearch.Text;
				dataGridItems.EndUpdate();
				if (dataGridItems.Rows.VisibleRowCount > 0)
				{
					dataGridItems.ActiveRow = dataGridItems.Rows.GetRowAtVisibleIndex(0);
				}
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			CanClose = true;
			if (CanClose)
			{
				Close();
			}
		}

		private void dataGridItems_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
		{
			if (!isMultiSelect || dataGridItems.Rows.Count <= 1)
			{
				SelectItem();
			}
		}

		private void SelectItem()
		{
			CanClose = true;
			if (dataGridItems.Rows.VisibleRowCount == 0 || dataGridItems.ActiveRow == null)
			{
				ErrorHelper.InformationMessage("Please select an item first.");
				return;
			}
			if (RangeFrom != "" && RangeTo == "")
			{
				RangeTo = RangeFrom;
			}
			else if (RangeFrom == "" && RangeTo != "")
			{
				RangeFrom = RangeTo;
			}
			if (this.ValidateSelection != null)
			{
				this.ValidateSelection(this, null);
			}
			if (CanClose && dataGridItems.ActiveRow != null)
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			SelectItem();
		}

		private void radioButtonRange_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			textBoxSearch.Clear();
		}

		private void comboBoxFromCustomer_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (((Control)sender).GetType() == typeof(customersFlatComboBox))
			{
				bool flag = false;
				foreach (customersFlatComboBox item in GetAll(this, typeof(customersFlatComboBox)))
				{
					if (item.SelectedID != "")
					{
						flag = true;
					}
				}
				dataGridItems.Enabled = !flag;
			}
		}

		public IEnumerable<Control> GetAll(Control control, Type type)
		{
			IEnumerable<Control> enumerable = control.Controls.Cast<Control>();
			return from c in enumerable.SelectMany((Control ctrl) => GetAll(ctrl, type)).Concat(enumerable)
				where c.GetType() == type
				select c;
		}

		private void comboBoxFromArea_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (((Control)sender).GetType() == typeof(AreaComboBox))
			{
				bool flag = false;
				foreach (AreaComboBox item in GetAll(this, typeof(AreaComboBox)))
				{
					if (item.SelectedID != "")
					{
						flag = true;
					}
				}
				dataGridItems.Enabled = !flag;
			}
		}

		private void comboBoxFromClass_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (((Control)sender).GetType() == typeof(CustomerClassComboBox))
			{
				bool flag = false;
				foreach (CustomerClassComboBox item in GetAll(this, typeof(CustomerClassComboBox)))
				{
					if (item.SelectedID != "")
					{
						flag = true;
					}
				}
				dataGridItems.Enabled = !flag;
			}
		}

		private void comboBoxFromCountry_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (((Control)sender).GetType() == typeof(CountryComboBox))
			{
				bool flag = false;
				foreach (CountryComboBox item in GetAll(this, typeof(CountryComboBox)))
				{
					if (item.SelectedID != "")
					{
						flag = true;
					}
				}
				dataGridItems.Enabled = !flag;
			}
		}

		private void comboBoxFromGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (((Control)sender).GetType() == typeof(CustomerGroupComboBox))
			{
				bool flag = false;
				foreach (CustomerGroupComboBox item in GetAll(this, typeof(CustomerGroupComboBox)))
				{
					if (item.SelectedID != "")
					{
						flag = true;
					}
				}
				dataGridItems.Enabled = !flag;
			}
		}

		private void buttonClear_Click(object sender, EventArgs e)
		{
			clear();
		}

		private void clear()
		{
			if (selectedCombo == DataComboType.Customer)
			{
				foreach (customersFlatComboBox item in GetAll(this, typeof(customersFlatComboBox)))
				{
					item.SelectedID = "";
				}
			}
			else if (selectedCombo == DataComboType.CustomerGroup)
			{
				foreach (CustomerGroupComboBox item2 in GetAll(this, typeof(CustomerGroupComboBox)))
				{
					item2.SelectedID = "";
				}
			}
			else if (selectedCombo == DataComboType.CustomerClass)
			{
				foreach (CustomerClassComboBox item3 in GetAll(this, typeof(CustomerClassComboBox)))
				{
					item3.SelectedID = "";
				}
			}
			else if (selectedCombo == DataComboType.CustomerClass)
			{
				foreach (CustomerClassComboBox item4 in GetAll(this, typeof(CustomerClassComboBox)))
				{
					item4.SelectedID = "";
				}
			}
			else if (selectedCombo == DataComboType.Area)
			{
				foreach (AreaComboBox item5 in GetAll(this, typeof(AreaComboBox)))
				{
					item5.SelectedID = "";
				}
			}
			else if (selectedCombo == DataComboType.Country)
			{
				foreach (CountryComboBox item6 in GetAll(this, typeof(CountryComboBox)))
				{
					item6.SelectedID = "";
				}
			}
			foreach (UltraGridRow item7 in dataGridItems.Rows.Where((UltraGridRow x) => Convert.ToBoolean(x.Cells["C"].Value)))
			{
				item7.Cells["C"].Value = false;
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
			panelButtons = new System.Windows.Forms.Panel();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			textBoxSearch = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			labelTo = new System.Windows.Forms.Label();
			panelCustomer = new System.Windows.Forms.Panel();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			panelCustomerClass = new System.Windows.Forms.Panel();
			label4 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			panelCustomerArea = new System.Windows.Forms.Panel();
			label7 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			panelCountry = new System.Windows.Forms.Panel();
			label9 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			panelGroup = new System.Windows.Forms.Panel();
			label11 = new System.Windows.Forms.Label();
			buttonClear = new System.Windows.Forms.Button();
			comboBoxToClass = new Micromind.DataControls.CustomerClassComboBox();
			comboBoxFromClass = new Micromind.DataControls.CustomerClassComboBox();
			comboBoxToGroup = new Micromind.DataControls.CustomerGroupComboBox();
			comboBoxFromGroup = new Micromind.DataControls.CustomerGroupComboBox();
			comboBoxToCountry = new Micromind.DataControls.CountryComboBox();
			comboBoxFromCountry = new Micromind.DataControls.CountryComboBox();
			comboBoxToArea = new Micromind.DataControls.AreaComboBox();
			comboBoxFromArea = new Micromind.DataControls.AreaComboBox();
			comboBoxToCustomer = new Micromind.DataControls.customersFlatComboBox();
			comboBoxFromCustomer = new Micromind.DataControls.customersFlatComboBox();
			dataGridItems = new Micromind.DataControls.MMSListGrid();
			comboBoxDateRange = new Micromind.DataControls.GadgetDateRangeComboBox();
			panelButtons.SuspendLayout();
			panelCustomer.SuspendLayout();
			panelCustomerClass.SuspendLayout();
			panelCustomerArea.SuspendLayout();
			panelCountry.SuspendLayout();
			panel1.SuspendLayout();
			panelGroup.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxToClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToCountry).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromCountry).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToArea).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromArea).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDateRange.Properties).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(comboBoxDateRange);
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 342);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(517, 40);
			panelButtons.TabIndex = 2;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(305, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(517, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(407, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 2;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			textBoxSearch.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxSearch.Location = new System.Drawing.Point(70, 13);
			textBoxSearch.Name = "textBoxSearch";
			textBoxSearch.Size = new System.Drawing.Size(437, 20);
			textBoxSearch.TabIndex = 0;
			textBoxSearch.TextChanged += new System.EventHandler(textBoxSearch_TextChanged);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 16);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(30, 13);
			label1.TabIndex = 5;
			label1.Text = "Find:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(6, 8);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 10;
			label3.Text = "From:";
			labelTo.AutoSize = true;
			labelTo.Location = new System.Drawing.Point(162, 8);
			labelTo.Name = "labelTo";
			labelTo.Size = new System.Drawing.Size(23, 13);
			labelTo.TabIndex = 11;
			labelTo.Text = "To:";
			panelCustomer.Controls.Add(comboBoxToCustomer);
			panelCustomer.Controls.Add(comboBoxFromCustomer);
			panelCustomer.Controls.Add(label3);
			panelCustomer.Controls.Add(labelTo);
			panelCustomer.Location = new System.Drawing.Point(3, 3);
			panelCustomer.Name = "panelCustomer";
			panelCustomer.Size = new System.Drawing.Size(300, 27);
			panelCustomer.TabIndex = 12;
			panelCustomer.Visible = false;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(7, 7);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(33, 13);
			label5.TabIndex = 14;
			label5.Text = "From:";
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(162, 7);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(23, 13);
			label6.TabIndex = 15;
			label6.Text = "To:";
			panelCustomerClass.Controls.Add(comboBoxToClass);
			panelCustomerClass.Controls.Add(comboBoxFromClass);
			panelCustomerClass.Controls.Add(label4);
			panelCustomerClass.Controls.Add(label2);
			panelCustomerClass.Location = new System.Drawing.Point(3, 3);
			panelCustomerClass.Name = "panelCustomerClass";
			panelCustomerClass.Size = new System.Drawing.Size(300, 27);
			panelCustomerClass.TabIndex = 13;
			panelCustomerClass.Visible = false;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(5, 7);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(33, 13);
			label4.TabIndex = 9;
			label4.Text = "From:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(161, 7);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(23, 13);
			label2.TabIndex = 10;
			label2.Text = "To:";
			panelCustomerArea.Controls.Add(comboBoxToArea);
			panelCustomerArea.Controls.Add(comboBoxFromArea);
			panelCustomerArea.Controls.Add(label7);
			panelCustomerArea.Controls.Add(label8);
			panelCustomerArea.Location = new System.Drawing.Point(3, 3);
			panelCustomerArea.Name = "panelCustomerArea";
			panelCustomerArea.Size = new System.Drawing.Size(300, 27);
			panelCustomerArea.TabIndex = 15;
			panelCustomerArea.Visible = false;
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(5, 7);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(33, 13);
			label7.TabIndex = 14;
			label7.Text = "From:";
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(161, 8);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(23, 13);
			label8.TabIndex = 15;
			label8.Text = "To:";
			panelCountry.Controls.Add(comboBoxToCountry);
			panelCountry.Controls.Add(comboBoxFromCountry);
			panelCountry.Controls.Add(label9);
			panelCountry.Controls.Add(label10);
			panelCountry.Location = new System.Drawing.Point(3, 3);
			panelCountry.Name = "panelCountry";
			panelCountry.Size = new System.Drawing.Size(300, 27);
			panelCountry.TabIndex = 16;
			panelCountry.Visible = false;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(11, 8);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(33, 13);
			label9.TabIndex = 14;
			label9.Text = "From:";
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(162, 8);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(23, 13);
			label10.TabIndex = 15;
			label10.Text = "To:";
			panel1.Controls.Add(panelCustomerClass);
			panel1.Controls.Add(panelGroup);
			panel1.Controls.Add(panelCountry);
			panel1.Controls.Add(panelCustomerArea);
			panel1.Controls.Add(panelCustomer);
			panel1.Location = new System.Drawing.Point(58, 35);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(306, 33);
			panel1.TabIndex = 17;
			panelGroup.Controls.Add(comboBoxToGroup);
			panelGroup.Controls.Add(label5);
			panelGroup.Controls.Add(comboBoxFromGroup);
			panelGroup.Controls.Add(label6);
			panelGroup.Location = new System.Drawing.Point(3, 3);
			panelGroup.Name = "panelGroup";
			panelGroup.Size = new System.Drawing.Size(300, 27);
			panelGroup.TabIndex = 19;
			panelGroup.Visible = false;
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(12, 45);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(39, 13);
			label11.TabIndex = 18;
			label11.Text = "Range";
			buttonClear.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			buttonClear.Location = new System.Drawing.Point(426, 42);
			buttonClear.Name = "buttonClear";
			buttonClear.Size = new System.Drawing.Size(81, 21);
			buttonClear.TabIndex = 19;
			buttonClear.Text = "Clear";
			buttonClear.UseVisualStyleBackColor = true;
			buttonClear.Click += new System.EventHandler(buttonClear_Click);
			comboBoxToClass.Assigned = false;
			comboBoxToClass.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToClass.CustomReportFieldName = "";
			comboBoxToClass.CustomReportKey = "";
			comboBoxToClass.CustomReportValueType = 1;
			comboBoxToClass.DescriptionTextBox = null;
			comboBoxToClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToClass.Editable = true;
			comboBoxToClass.FilterString = "";
			comboBoxToClass.HasAllAccount = false;
			comboBoxToClass.HasCustom = false;
			comboBoxToClass.IsDataLoaded = false;
			comboBoxToClass.Location = new System.Drawing.Point(187, 3);
			comboBoxToClass.MaxDropDownItems = 12;
			comboBoxToClass.Name = "comboBoxToClass";
			comboBoxToClass.ShowInactiveItems = false;
			comboBoxToClass.ShowQuickAdd = true;
			comboBoxToClass.Size = new System.Drawing.Size(110, 20);
			comboBoxToClass.TabIndex = 20;
			comboBoxToClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToClass.SelectedIndexChanged += new System.EventHandler(comboBoxFromClass_SelectedIndexChanged);
			comboBoxFromClass.Assigned = false;
			comboBoxFromClass.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromClass.CustomReportFieldName = "";
			comboBoxFromClass.CustomReportKey = "";
			comboBoxFromClass.CustomReportValueType = 1;
			comboBoxFromClass.DescriptionTextBox = null;
			comboBoxFromClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromClass.Editable = true;
			comboBoxFromClass.FilterString = "";
			comboBoxFromClass.HasAllAccount = false;
			comboBoxFromClass.HasCustom = false;
			comboBoxFromClass.IsDataLoaded = false;
			comboBoxFromClass.Location = new System.Drawing.Point(40, 3);
			comboBoxFromClass.MaxDropDownItems = 12;
			comboBoxFromClass.Name = "comboBoxFromClass";
			comboBoxFromClass.ShowInactiveItems = false;
			comboBoxFromClass.ShowQuickAdd = true;
			comboBoxFromClass.Size = new System.Drawing.Size(108, 20);
			comboBoxFromClass.TabIndex = 19;
			comboBoxFromClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromClass.SelectedIndexChanged += new System.EventHandler(comboBoxFromClass_SelectedIndexChanged);
			comboBoxToGroup.Assigned = false;
			comboBoxToGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToGroup.CustomReportFieldName = "";
			comboBoxToGroup.CustomReportKey = "";
			comboBoxToGroup.CustomReportValueType = 1;
			comboBoxToGroup.DescriptionTextBox = null;
			comboBoxToGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToGroup.Editable = true;
			comboBoxToGroup.FilterString = "";
			comboBoxToGroup.HasAllAccount = false;
			comboBoxToGroup.HasCustom = false;
			comboBoxToGroup.IsDataLoaded = false;
			comboBoxToGroup.Location = new System.Drawing.Point(182, 3);
			comboBoxToGroup.MaxDropDownItems = 12;
			comboBoxToGroup.Name = "comboBoxToGroup";
			comboBoxToGroup.ShowInactiveItems = false;
			comboBoxToGroup.ShowQuickAdd = true;
			comboBoxToGroup.Size = new System.Drawing.Size(114, 20);
			comboBoxToGroup.TabIndex = 20;
			comboBoxToGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToGroup.SelectedIndexChanged += new System.EventHandler(comboBoxFromGroup_SelectedIndexChanged);
			comboBoxFromGroup.Assigned = false;
			comboBoxFromGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromGroup.CustomReportFieldName = "";
			comboBoxFromGroup.CustomReportKey = "";
			comboBoxFromGroup.CustomReportValueType = 1;
			comboBoxFromGroup.DescriptionTextBox = null;
			comboBoxFromGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromGroup.Editable = true;
			comboBoxFromGroup.FilterString = "";
			comboBoxFromGroup.HasAllAccount = false;
			comboBoxFromGroup.HasCustom = false;
			comboBoxFromGroup.IsDataLoaded = false;
			comboBoxFromGroup.Location = new System.Drawing.Point(44, 3);
			comboBoxFromGroup.MaxDropDownItems = 12;
			comboBoxFromGroup.Name = "comboBoxFromGroup";
			comboBoxFromGroup.ShowInactiveItems = false;
			comboBoxFromGroup.ShowQuickAdd = true;
			comboBoxFromGroup.Size = new System.Drawing.Size(110, 20);
			comboBoxFromGroup.TabIndex = 19;
			comboBoxFromGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromGroup.SelectedIndexChanged += new System.EventHandler(comboBoxFromGroup_SelectedIndexChanged);
			comboBoxToCountry.Assigned = false;
			comboBoxToCountry.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToCountry.CustomReportFieldName = "";
			comboBoxToCountry.CustomReportKey = "";
			comboBoxToCountry.CustomReportValueType = 1;
			comboBoxToCountry.DescriptionTextBox = null;
			comboBoxToCountry.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToCountry.Editable = true;
			comboBoxToCountry.FilterString = "";
			comboBoxToCountry.HasAllAccount = false;
			comboBoxToCountry.HasCustom = false;
			comboBoxToCountry.IsDataLoaded = false;
			comboBoxToCountry.Location = new System.Drawing.Point(182, 3);
			comboBoxToCountry.MaxDropDownItems = 12;
			comboBoxToCountry.Name = "comboBoxToCountry";
			comboBoxToCountry.ShowInactiveItems = false;
			comboBoxToCountry.ShowQuickAdd = true;
			comboBoxToCountry.Size = new System.Drawing.Size(112, 20);
			comboBoxToCountry.TabIndex = 20;
			comboBoxToCountry.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToCountry.SelectedIndexChanged += new System.EventHandler(comboBoxFromCountry_SelectedIndexChanged);
			comboBoxFromCountry.Assigned = false;
			comboBoxFromCountry.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromCountry.CustomReportFieldName = "";
			comboBoxFromCountry.CustomReportKey = "";
			comboBoxFromCountry.CustomReportValueType = 1;
			comboBoxFromCountry.DescriptionTextBox = null;
			comboBoxFromCountry.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromCountry.Editable = true;
			comboBoxFromCountry.FilterString = "";
			comboBoxFromCountry.HasAllAccount = false;
			comboBoxFromCountry.HasCustom = false;
			comboBoxFromCountry.IsDataLoaded = false;
			comboBoxFromCountry.Location = new System.Drawing.Point(44, 3);
			comboBoxFromCountry.MaxDropDownItems = 12;
			comboBoxFromCountry.Name = "comboBoxFromCountry";
			comboBoxFromCountry.ShowInactiveItems = false;
			comboBoxFromCountry.ShowQuickAdd = true;
			comboBoxFromCountry.Size = new System.Drawing.Size(112, 20);
			comboBoxFromCountry.TabIndex = 19;
			comboBoxFromCountry.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromCountry.SelectedIndexChanged += new System.EventHandler(comboBoxFromCountry_SelectedIndexChanged);
			comboBoxToArea.Assigned = false;
			comboBoxToArea.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToArea.CustomReportFieldName = "";
			comboBoxToArea.CustomReportKey = "";
			comboBoxToArea.CustomReportValueType = 1;
			comboBoxToArea.DescriptionTextBox = null;
			comboBoxToArea.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToArea.Editable = true;
			comboBoxToArea.FilterString = "";
			comboBoxToArea.HasAllAccount = false;
			comboBoxToArea.HasCustom = false;
			comboBoxToArea.IsDataLoaded = false;
			comboBoxToArea.Location = new System.Drawing.Point(186, 3);
			comboBoxToArea.MaxDropDownItems = 12;
			comboBoxToArea.Name = "comboBoxToArea";
			comboBoxToArea.ShowInactiveItems = false;
			comboBoxToArea.ShowQuickAdd = true;
			comboBoxToArea.Size = new System.Drawing.Size(111, 20);
			comboBoxToArea.TabIndex = 20;
			comboBoxToArea.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToArea.SelectedIndexChanged += new System.EventHandler(comboBoxFromArea_SelectedIndexChanged);
			comboBoxFromArea.Assigned = false;
			comboBoxFromArea.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromArea.CustomReportFieldName = "";
			comboBoxFromArea.CustomReportKey = "";
			comboBoxFromArea.CustomReportValueType = 1;
			comboBoxFromArea.DescriptionTextBox = null;
			comboBoxFromArea.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromArea.Editable = true;
			comboBoxFromArea.FilterString = "";
			comboBoxFromArea.HasAllAccount = false;
			comboBoxFromArea.HasCustom = false;
			comboBoxFromArea.IsDataLoaded = false;
			comboBoxFromArea.Location = new System.Drawing.Point(37, 3);
			comboBoxFromArea.MaxDropDownItems = 12;
			comboBoxFromArea.Name = "comboBoxFromArea";
			comboBoxFromArea.ShowInactiveItems = false;
			comboBoxFromArea.ShowQuickAdd = true;
			comboBoxFromArea.Size = new System.Drawing.Size(111, 20);
			comboBoxFromArea.TabIndex = 19;
			comboBoxFromArea.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromArea.SelectedIndexChanged += new System.EventHandler(comboBoxFromArea_SelectedIndexChanged);
			comboBoxToCustomer.Assigned = false;
			comboBoxToCustomer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToCustomer.CustomReportFieldName = "";
			comboBoxToCustomer.CustomReportKey = "";
			comboBoxToCustomer.CustomReportValueType = 1;
			comboBoxToCustomer.DescriptionTextBox = null;
			comboBoxToCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToCustomer.Editable = true;
			comboBoxToCustomer.FilterString = "";
			comboBoxToCustomer.FilterSysDocID = "";
			comboBoxToCustomer.HasAll = false;
			comboBoxToCustomer.HasCustom = false;
			comboBoxToCustomer.IsDataLoaded = false;
			comboBoxToCustomer.Location = new System.Drawing.Point(183, 3);
			comboBoxToCustomer.MaxDropDownItems = 12;
			comboBoxToCustomer.Name = "comboBoxToCustomer";
			comboBoxToCustomer.ShowConsignmentOnly = false;
			comboBoxToCustomer.ShowInactive = false;
			comboBoxToCustomer.ShowLPOCustomersOnly = false;
			comboBoxToCustomer.ShowPROCustomersOnly = false;
			comboBoxToCustomer.ShowQuickAdd = true;
			comboBoxToCustomer.Size = new System.Drawing.Size(114, 20);
			comboBoxToCustomer.TabIndex = 20;
			comboBoxToCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToCustomer.SelectedIndexChanged += new System.EventHandler(comboBoxFromCustomer_SelectedIndexChanged);
			comboBoxFromCustomer.Assigned = false;
			comboBoxFromCustomer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromCustomer.CustomReportFieldName = "";
			comboBoxFromCustomer.CustomReportKey = "";
			comboBoxFromCustomer.CustomReportValueType = 1;
			comboBoxFromCustomer.DescriptionTextBox = null;
			comboBoxFromCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromCustomer.Editable = true;
			comboBoxFromCustomer.FilterString = "";
			comboBoxFromCustomer.FilterSysDocID = "";
			comboBoxFromCustomer.HasAll = false;
			comboBoxFromCustomer.HasCustom = false;
			comboBoxFromCustomer.IsDataLoaded = false;
			comboBoxFromCustomer.Location = new System.Drawing.Point(42, 3);
			comboBoxFromCustomer.MaxDropDownItems = 12;
			comboBoxFromCustomer.Name = "comboBoxFromCustomer";
			comboBoxFromCustomer.ShowConsignmentOnly = false;
			comboBoxFromCustomer.ShowInactive = false;
			comboBoxFromCustomer.ShowLPOCustomersOnly = false;
			comboBoxFromCustomer.ShowPROCustomersOnly = false;
			comboBoxFromCustomer.ShowQuickAdd = true;
			comboBoxFromCustomer.Size = new System.Drawing.Size(114, 20);
			comboBoxFromCustomer.TabIndex = 19;
			comboBoxFromCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromCustomer.SelectedIndexChanged += new System.EventHandler(comboBoxFromCustomer_SelectedIndexChanged);
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
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
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.Location = new System.Drawing.Point(15, 74);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowMinusInRed = true;
			dataGridItems.Size = new System.Drawing.Size(492, 262);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "mmsListGrid1";
			dataGridItems.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(dataGridItems_DoubleClickRow);
			comboBoxDateRange.Location = new System.Drawing.Point(10, 8);
			comboBoxDateRange.Name = "comboBoxDateRange";
			comboBoxDateRange.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			comboBoxDateRange.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			comboBoxDateRange.Size = new System.Drawing.Size(160, 20);
			comboBoxDateRange.TabIndex = 0;
			comboBoxDateRange.Visible = false;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(517, 382);
			base.Controls.Add(buttonClear);
			base.Controls.Add(label11);
			base.Controls.Add(panel1);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxSearch);
			base.Controls.Add(panelButtons);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(465, 215);
			base.Name = "SelectDocumentDialogByRange";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Select Cheque";
			panelButtons.ResumeLayout(false);
			panelCustomer.ResumeLayout(false);
			panelCustomer.PerformLayout();
			panelCustomerClass.ResumeLayout(false);
			panelCustomerClass.PerformLayout();
			panelCustomerArea.ResumeLayout(false);
			panelCustomerArea.PerformLayout();
			panelCountry.ResumeLayout(false);
			panelCountry.PerformLayout();
			panel1.ResumeLayout(false);
			panelGroup.ResumeLayout(false);
			panelGroup.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxToClass).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromClass).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToCountry).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromCountry).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToArea).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromArea).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDateRange.Properties).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
