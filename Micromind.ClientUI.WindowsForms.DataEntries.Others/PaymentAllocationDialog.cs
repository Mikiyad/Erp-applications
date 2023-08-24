using DevExpress.XtraEditors.Controls;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.WindowsForms.DataEntries.Customers;
using Micromind.ClientUI.WindowsForms.DataEntries.Vendors;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class PaymentAllocationDialog : Form
	{
		public bool CanClose = true;

		private bool isMultiSelect;

		private List<string> hiddenColumns = new List<string>();

		private decimal invoiceAmount;

		private decimal paid;

		private decimal balance;

		private EntityTypesEnum entityType = EntityTypesEnum.Customers;

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Line linePanelDown;

		private XPButton buttonCancel;

		private TextBox textBoxSearch;

		private Label label1;

		private MMSListGrid dataGridItems;

		private GadgetDateRangeComboBox comboBoxDateRange;

		private Panel panel1;

		private Label label7;

		private AmountTextBox textBoxPaid;

		private AmountTextBox textBoxBalance;

		private Label label5;

		private AmountTextBox textBoxInvoiceAmount;

		private Label label2;

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
				}
				if (list.Count == 0 && dataGridItems.ActiveRow != null)
				{
					list.Add(dataGridItems.ActiveRow);
				}
				return list;
			}
		}

		public UltraGrid Grid => dataGridItems;

		public decimal InvoiceAmount
		{
			get
			{
				return invoiceAmount;
			}
			set
			{
				invoiceAmount = value;
				textBoxInvoiceAmount.Text = invoiceAmount.ToString();
			}
		}

		public decimal Paid
		{
			get
			{
				return paid;
			}
			set
			{
				paid = value;
				textBoxPaid.Text = paid.ToString();
			}
		}

		public decimal Balance
		{
			get
			{
				return balance;
			}
			set
			{
				balance = value;
				textBoxBalance.Text = balance.ToString();
			}
		}

		public EntityTypesEnum EntityType
		{
			get
			{
				return entityType;
			}
			set
			{
				entityType = value;
			}
		}

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

		public event EventHandler<DateRangeStruct> RequireDataRefresh;

		public event EventHandler ValidateSelection;

		public PaymentAllocationDialog()
		{
			InitializeComponent();
			base.Load += SelectDialog_Load;
			textBoxSearch.KeyDown += textBoxSearch_KeyDown;
			base.Activated += SelectDialog_Activated;
			base.FormClosing += SelectDialog_FormClosing;
			comboBoxDateRange.LoadData();
			comboBoxDateRange.SelectedIndex = 4;
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
		}

		private void SelectDialog_FormClosing(object sender, FormClosingEventArgs e)
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

		private void SelectDialog_Activated(object sender, EventArgs e)
		{
			textBoxSearch.Focus();
			CreateContextMenu();
		}

		private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
		{
			checked
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
		}

		private void SelectDialog_Load(object sender, EventArgs e)
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
				ErrorHelper.InformationMessage(UIMessages.SelectAnItemFirst);
				return;
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

		private void CreateContextMenu()
		{
			ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
			if (EntityType == EntityTypesEnum.Vendors)
			{
				ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("Vendor Ledger");
				toolStripMenuItem.Click += menuItem_Click;
				toolStripMenuItem.Name = "Vendor Ledger";
				contextMenuStrip.Items.Add(toolStripMenuItem);
			}
			else if (EntityType == EntityTypesEnum.Customers)
			{
				ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem("Customer Ledger");
				toolStripMenuItem2.Click += menuItem_Click;
				toolStripMenuItem2.Name = "Customer Ledger";
				contextMenuStrip.Items.Add(toolStripMenuItem2);
			}
			ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem("Payment Voucher");
			toolStripMenuItem3.Click += menuItem_Click;
			toolStripMenuItem3.Name = "Payment Voucher";
			contextMenuStrip.Items.Add(toolStripMenuItem3);
			ContextMenuStrip = contextMenuStrip;
		}

		private void menuItem_Click(object sender, EventArgs e)
		{
			ToolStripItem toolStripItem = (ToolStripItem)sender;
			if (toolStripItem.Name == "Vendor Ledger")
			{
				VendorLedgerForm vendorLedgerForm = new VendorLedgerForm();
				vendorLedgerForm.SelectedID = dataGridItems.ActiveRow.Cells["VendorID"].Text.ToString();
				vendorLedgerForm.Show();
				vendorLedgerForm.BringToFront();
			}
			if (toolStripItem.Name == "Customer Ledger")
			{
				CustomerLedgerForm customerLedgerForm = new CustomerLedgerForm();
				customerLedgerForm.SelectedID = dataGridItems.ActiveRow.Cells["CustomerID"].Text.ToString();
				customerLedgerForm.Show();
				customerLedgerForm.BringToFront();
			}
			else if (toolStripItem.Name == "Payment Voucher" && dataGridItems.ActiveRow.IsDataRow)
			{
				string sysDocID = dataGridItems.ActiveRow.Cells["PaymentSysDocID"].Text.ToString();
				string text = dataGridItems.ActiveRow.Cells["PaymentVoucherID"].Text.ToString();
				if (text != null)
				{
					new FormHelper().EditTransaction(sysDocID, text);
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
			panelButtons = new System.Windows.Forms.Panel();
			comboBoxDateRange = new Micromind.DataControls.GadgetDateRangeComboBox(components);
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			textBoxSearch = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			dataGridItems = new Micromind.DataControls.MMSListGrid();
			panel1 = new System.Windows.Forms.Panel();
			label7 = new System.Windows.Forms.Label();
			textBoxPaid = new Micromind.UISupport.AmountTextBox();
			textBoxBalance = new Micromind.UISupport.AmountTextBox();
			label5 = new System.Windows.Forms.Label();
			textBoxInvoiceAmount = new Micromind.UISupport.AmountTextBox();
			label2 = new System.Windows.Forms.Label();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDateRange.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			panel1.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(comboBoxDateRange);
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 367);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(645, 40);
			panelButtons.TabIndex = 2;
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
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(433, 8);
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
			linePanelDown.Size = new System.Drawing.Size(645, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(535, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 2;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			textBoxSearch.Location = new System.Drawing.Point(48, 13);
			textBoxSearch.Name = "textBoxSearch";
			textBoxSearch.Size = new System.Drawing.Size(386, 20);
			textBoxSearch.TabIndex = 0;
			textBoxSearch.TextChanged += new System.EventHandler(textBoxSearch_TextChanged);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 16);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(30, 13);
			label1.TabIndex = 5;
			label1.Text = "Find:";
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
			dataGridItems.Location = new System.Drawing.Point(10, 45);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowMinusInRed = true;
			dataGridItems.Size = new System.Drawing.Size(620, 229);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "mmsListGrid1";
			dataGridItems.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(dataGridItems_DoubleClickRow);
			panel1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label7);
			panel1.Controls.Add(textBoxBalance);
			panel1.Controls.Add(textBoxPaid);
			panel1.Controls.Add(label5);
			panel1.Controls.Add(textBoxInvoiceAmount);
			panel1.Location = new System.Drawing.Point(321, 280);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(309, 81);
			panel1.TabIndex = 6;
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(4, 39);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(31, 13);
			label7.TabIndex = 147;
			label7.Text = "Paid:";
			textBoxPaid.AllowDecimal = true;
			textBoxPaid.CustomReportFieldName = "";
			textBoxPaid.CustomReportKey = "";
			textBoxPaid.CustomReportValueType = 1;
			textBoxPaid.IsComboTextBox = false;
			textBoxPaid.IsModified = false;
			textBoxPaid.Location = new System.Drawing.Point(168, 35);
			textBoxPaid.MaxLength = 15;
			textBoxPaid.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxPaid.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxPaid.Name = "textBoxPaid";
			textBoxPaid.NullText = "0";
			textBoxPaid.Size = new System.Drawing.Size(138, 20);
			textBoxPaid.TabIndex = 2;
			textBoxPaid.Text = "0.00";
			textBoxPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxPaid.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxBalance.AllowDecimal = true;
			textBoxBalance.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBalance.CustomReportFieldName = "";
			textBoxBalance.CustomReportKey = "";
			textBoxBalance.CustomReportValueType = 1;
			textBoxBalance.IsComboTextBox = false;
			textBoxBalance.IsModified = false;
			textBoxBalance.Location = new System.Drawing.Point(168, 59);
			textBoxBalance.MaxLength = 15;
			textBoxBalance.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxBalance.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxBalance.Name = "textBoxBalance";
			textBoxBalance.NullText = "0";
			textBoxBalance.ReadOnly = true;
			textBoxBalance.Size = new System.Drawing.Size(138, 20);
			textBoxBalance.TabIndex = 0;
			textBoxBalance.Text = "0.00";
			textBoxBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxBalance.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(4, 15);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(84, 13);
			label5.TabIndex = 133;
			label5.Text = "Invoice Amount:";
			textBoxInvoiceAmount.AllowDecimal = true;
			textBoxInvoiceAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxInvoiceAmount.CustomReportFieldName = "";
			textBoxInvoiceAmount.CustomReportKey = "";
			textBoxInvoiceAmount.CustomReportValueType = 1;
			textBoxInvoiceAmount.ForeColor = System.Drawing.Color.Black;
			textBoxInvoiceAmount.IsComboTextBox = false;
			textBoxInvoiceAmount.IsModified = false;
			textBoxInvoiceAmount.Location = new System.Drawing.Point(168, 11);
			textBoxInvoiceAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxInvoiceAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxInvoiceAmount.Name = "textBoxInvoiceAmount";
			textBoxInvoiceAmount.NullText = "0";
			textBoxInvoiceAmount.ReadOnly = true;
			textBoxInvoiceAmount.Size = new System.Drawing.Size(138, 20);
			textBoxInvoiceAmount.TabIndex = 0;
			textBoxInvoiceAmount.TabStop = false;
			textBoxInvoiceAmount.Text = "0.00";
			textBoxInvoiceAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxInvoiceAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(4, 63);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(49, 13);
			label2.TabIndex = 148;
			label2.Text = "Balance:";
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(645, 407);
			base.Controls.Add(panel1);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxSearch);
			base.Controls.Add(panelButtons);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(450, 215);
			base.Name = "PaymentAllocationDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Payment Allocation";
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxDateRange.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
