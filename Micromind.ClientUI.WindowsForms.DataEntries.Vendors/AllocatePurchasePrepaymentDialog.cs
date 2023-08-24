using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Vendors
{
	public class AllocatePurchasePrepaymentDialog : Form
	{
		public bool CanClose = true;

		private bool isMultiSelect = true;

		private bool isAllowtoedit;

		private bool isAllowVendorMultiple;

		private List<string> hiddenColumns = new List<string>();

		public List<string> selectedDocuments = new List<string>();

		public string InvoiceSysDocID = "";

		public string invoiceVoucherID = "";

		public DateTime InvoiceDate = DateTime.Now;

		public string VendorID = "";

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Line linePanelDown;

		private XPButton buttonCancel;

		private MMSListGrid dataGridItems;

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
					new DataSet();
					if (!(dataGridItems.DataSource as DataSet).Tables[0].Columns.Contains("C"))
					{
						dataGridItems.DisplayLayout.Bands[0].Columns.Insert(0, "C");
						dataGridItems.DisplayLayout.Bands[0].Columns["C"].DataType = typeof(bool);
					}
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellActivation = Activation.AllowEdit;
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellClickAction = CellClickAction.Edit;
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].Header.VisiblePosition = 0;
				}
			}
		}

		public bool IsAllowtoedit
		{
			get
			{
				return isAllowtoedit;
			}
			set
			{
				isAllowtoedit = value;
				if (value)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Cost to Allocate"].CellActivation = Activation.AllowEdit;
					dataGridItems.DisplayLayout.Bands[0].Columns["Cost to Allocate"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
					dataGridItems.DisplayLayout.Bands[0].Columns["Cost to Allocate"].CellClickAction = CellClickAction.Edit;
					dataGridItems.DisplayLayout.Bands[0].Columns["Cost to Allocate"].CellAppearance.TextHAlign = HAlign.Right;
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].DefaultCellValue = true;
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
				if (list.Count == 0 && dataGridItems.ActiveRow != null)
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

		public List<string> SelectedDocuments
		{
			get
			{
				List<string> list = new List<string>();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (row.Cells["C"] != null && bool.Parse(row.Cells["C"].Value.ToString()))
					{
						string item = row.Cells["Doc ID"].Value.ToString() + row.Cells["Number"].Value.ToString();
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
						if (row.Cells["Doc ID"].Value.ToString() + row.Cells["Number"].Value.ToString() == selectedDocument)
						{
							row.Cells["C"].Value = true;
							break;
						}
					}
				}
			}
		}

		public List<string> SelectedCodes
		{
			get
			{
				List<string> list = new List<string>();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (row.Cells["C"] != null && bool.Parse(row.Cells["C"].Value.ToString()))
					{
						string item = row.Cells["Code"].Value.ToString();
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

		public List<string> HiddenColumns => hiddenColumns;

		public event EventHandler<DateRangeStruct> RequireDataRefresh;

		public event EventHandler ValidateSelection;

		public AllocatePurchasePrepaymentDialog()
		{
			InitializeComponent();
			base.Load += SelectDocumentDialog_Load;
			base.Activated += SelectDocumentDialog_Activated;
			base.FormClosing += SelectDocumentDialog_FormClosing;
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

		private void SelectDocumentDialog_FormClosing(object sender, FormClosingEventArgs e)
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

		private void SelectDocumentDialog_Activated(object sender, EventArgs e)
		{
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

		private void SelectDocumentDialog_Load(object sender, EventArgs e)
		{
			try
			{
				Global.GlobalSettings.LoadFormProperties(this);
				dataGridItems.ApplyUIDesign();
				dataGridItems.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
				LoadData();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadData()
		{
			DataSet invoicePrepayments = Factory.PurchasePrepaymentInvoiceSystem.GetInvoicePrepayments(InvoiceSysDocID, invoiceVoucherID);
			DataTable dataTable = new DataTable
			{
				Columns = 
				{
					{
						"C",
						typeof(bool)
					},
					"SysDocID",
					"VoucherID",
					"PONumber",
					"Date",
					"CurrencyID",
					"CurrencyRate",
					"Invoiced",
					"Paid"
				}
			};
			foreach (DataRow row in invoicePrepayments.Tables[0].Rows)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["C"] = false;
				dataRow2["SysDocID"] = row["SysDocID"];
				dataRow2["VoucherID"] = row["VoucherID"];
				dataRow2["PONumber"] = row["SourceVoucherID"];
				dataRow2["Date"] = row["TransactionDate"];
				dataRow2["CurrencyID"] = row["CurrencyID"];
				dataRow2["CurrencyRate"] = row["CurrencyRate"];
				dataRow2["Invoiced"] = row["Invoiced"];
				dataRow2["Paid"] = row["PaidAmount"];
				dataTable.Rows.Add(dataRow2);
			}
			dataGridItems.DataSource = dataTable;
			dataGridItems.DisplayLayout.Bands[0].Columns["C"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
			dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
			dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellClickAction = CellClickAction.Edit;
			dataGridItems.DisplayLayout.Bands[0].Columns["PONumber"].Header.Caption = "PO Number";
			dataGridItems.DisplayLayout.Bands[0].Columns["VoucherID"].Header.Caption = "Doc Number";
			dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].Header.Caption = "Doc ID";
			dataGridItems.DisplayLayout.Bands[0].Columns["Invoiced"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["Paid"].CellAppearance.TextHAlign = HAlign.Right;
		}

		public string GetSelectedCode(string codeColumnName)
		{
			if (SelectedRow != null)
			{
				return SelectedRow.Cells[codeColumnName].Value.ToString();
			}
			return "";
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
			if (isMultiSelect && dataGridItems.Rows.Count > 1 && e.Row != null && e.Row.IsDataRow)
			{
				bool result = false;
				if (e.Row.Cells["C"].Value != null)
				{
					bool.TryParse(e.Row.Cells["C"].Value.ToString(), out result);
				}
				e.Row.Cells["C"].Value = !result;
			}
		}

		private bool GetData(ref PurchasePrepaymentInvoiceData data)
		{
			try
			{
				data = new PurchasePrepaymentInvoiceData();
				foreach (UltraGridRow selectedRow in SelectedRows)
				{
					DataRow dataRow = data.PurchasePrepaymentInvoiceTable.NewRow();
					dataRow.BeginEdit();
					dataRow["TransactionDate"] = InvoiceDate;
					dataRow["VendorID"] = VendorID;
					dataRow["InvoiceSysDocID"] = InvoiceSysDocID;
					dataRow["InvoiceVoucherID"] = invoiceVoucherID;
					dataRow["SysDocID"] = selectedRow.Cells["SysDocID"].Value.ToString();
					dataRow["VoucherID"] = selectedRow.Cells["VoucherID"].Value.ToString();
					if (!selectedRow.Cells["CurrencyID"].Value.IsNullOrEmpty())
					{
						dataRow["CurrencyID"] = selectedRow.Cells["CurrencyID"].Value.ToString();
					}
					else
					{
						dataRow["CurrencyID"] = DBNull.Value;
					}
					if (!selectedRow.Cells["CurrencyRate"].Value.IsNullOrEmpty())
					{
						dataRow["CurrencyRate"] = selectedRow.Cells["CurrencyRate"].Value.ToString();
					}
					else
					{
						dataRow["CurrencyRate"] = 1;
					}
					dataRow["Amount"] = selectedRow.Cells["Invoiced"].Value.ToString();
					dataRow.EndEdit();
					data.Tables[0].Rows.Add(dataRow);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ApplyItems()
		{
			try
			{
				CanClose = true;
				if (dataGridItems.Rows.VisibleRowCount == 0 || dataGridItems.ActiveRow == null)
				{
					ErrorHelper.InformationMessage(UIMessages.SelectAnItemFirst);
				}
				else
				{
					PurchasePrepaymentInvoiceData data = new PurchasePrepaymentInvoiceData();
					GetData(ref data);
					if (Factory.PurchasePrepaymentInvoiceSystem.ClosePrepaymentInvoice(data))
					{
						LoadData();
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			ApplyItems();
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
			dataGridItems = new Micromind.DataControls.MMSListGrid();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 333);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(607, 40);
			panelButtons.TabIndex = 2;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(395, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "&Apply";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(607, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(497, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 2;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
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
			dataGridItems.Location = new System.Drawing.Point(10, 12);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowMinusInRed = true;
			dataGridItems.Size = new System.Drawing.Size(582, 311);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "mmsListGrid1";
			dataGridItems.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(dataGridItems_DoubleClickRow);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(607, 373);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(panelButtons);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(465, 215);
			base.Name = "AllocatePurchasePrepaymentDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Apply Prepayments";
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
		}
	}
}
