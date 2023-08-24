using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraPrinting.Control;
using DevExpress.XtraReports.UI;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.DataControls;
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
	public class TransactionLookupDialog : Form
	{
		public enum LookupTransactionTypes
		{
			Receipt
		}

		public bool CanClose = true;

		private bool isMultiSelect;

		public string SysDocID = "";

		private LookupTransactionTypes lookupType;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private TextBox textBoxSearch;

		private DefaultLookAndFeel defaultLookAndFeel1;

		private SimpleButton buttonOK;

		private SimpleButton buttonCancel;

		private Label label6;

		private POSGrid dataGridItems;

		private RadioButton radioButtonThisYear;

		private RadioButton radioButtonLastMonth;

		private RadioButton radioButtonThisMonth;

		private RadioButton radioButtonYesterday;

		private RadioButton radioButtonToday;

		private SplitContainer splitContainer1;

		private PrintControl printControl1;

		private CheckBox checkBoxShowPreview;

		private Line line1;

		private RadioButton radioButtonSelectedDate;

		private MMSDateTimePicker dateTimePickerDate;

		private Micromind.DataControls.DateControl dateControlDate;

		private BackgroundWorker backgroundWorker1;

		private SimpleButton buttonPrint;

		private SimpleButton buttonRefresh;

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

		public LookupTransactionTypes LookupType
		{
			get
			{
				return lookupType;
			}
			set
			{
				lookupType = value;
				if (lookupType == LookupTransactionTypes.Receipt)
				{
					Text = "Select Sales Receipt";
				}
				else
				{
					Text = "Select Transaction";
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

		public event EventHandler ValidateSelection;

		public TransactionLookupDialog()
		{
			InitializeComponent();
			backgroundWorker1.WorkerSupportsCancellation = true;
			base.Load += SelectDocumentDialog_Load;
			textBoxSearch.KeyDown += textBoxSearch_KeyDown;
			base.Activated += SelectDocumentDialog_Activated;
			dataGridItems.DataGrid.DoubleClickRow += dataGridItems_DoubleClickRow;
			base.AcceptButton = buttonOK;
			base.CancelButton = buttonCancel;
			dateControlDate.LoadData();
			dataGridItems.DataGrid.AfterRowActivate += DataGrid_AfterRowActivate;
			dateTimePickerDate.ValueChanged += DateTimePickerDate_ValueChanged;
			base.FormClosing += TransactionLookupDialog_FormClosing;
		}

		private void TransactionLookupDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				Global.GlobalSettings.SaveFormProperties(this);
				DatePeriods datePeriods = DatePeriods.Today;
				if (radioButtonToday.Checked)
				{
					datePeriods = DatePeriods.Today;
				}
				else if (radioButtonLastMonth.Checked)
				{
					datePeriods = DatePeriods.LastMonth;
				}
				else if (radioButtonThisMonth.Checked)
				{
					datePeriods = DatePeriods.ThisMonthToDate;
				}
				else if (radioButtonThisYear.Checked)
				{
					datePeriods = DatePeriods.ThisYearToDate;
				}
				else if (radioButtonYesterday.Checked)
				{
					datePeriods = DatePeriods.Yesterday;
				}
				UserPreferences.SaveCurrentUserSetting(base.Name + "Period", (int)datePeriods);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void DateTimePickerDate_ValueChanged(object sender, EventArgs e)
		{
			dateControlDate.SelectedPeriod = DatePeriods.Custom;
			dateControlDate.FromDate = dateTimePickerDate.ValueFrom;
			dateControlDate.ToDate = dateTimePickerDate.ValueTo;
			LoadData();
		}

		private void DataGrid_AfterRowActivate(object sender, EventArgs e)
		{
			try
			{
				if (checkBoxShowPreview.Checked)
				{
					if (backgroundWorker1.IsBusy)
					{
						backgroundWorker1.CancelAsync();
					}
					while (backgroundWorker1.IsBusy)
					{
						Application.DoEvents();
					}
					backgroundWorker1.RunWorkerAsync();
					printControl1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
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
				Global.GlobalSettings.LoadFormProperties(this);
				dateTimePickerDate.Value = DateTime.Today;
				dateControlDate.SelectedPeriod = DatePeriods.Today;
				switch (UserPreferences.GetCurrentUserSetting(base.Name + "Period", 0))
				{
				case 0:
					radioButtonToday.Checked = true;
					break;
				case 4:
					radioButtonLastMonth.Checked = true;
					break;
				case 2:
					radioButtonThisMonth.Checked = true;
					break;
				case 3:
					radioButtonThisYear.Checked = true;
					break;
				case 1:
					radioButtonYesterday.Checked = true;
					break;
				}
				dataGridItems.SetupUI();
				LoadData();
				dataGridItems.DataGrid.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
				checkBoxShowPreview.Checked = bool.Parse(Global.GlobalSettings.GetSetting(base.Name + "Prev", false).ToString());
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
			if (lookupType == LookupTransactionTypes.Receipt)
			{
				dataSet = Factory.SalesPOSSystem.GetSalesReceiptLookupList(SysDocID, dateControlDate.FromDate, dateControlDate.ToDate);
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
			if (lookupType == LookupTransactionTypes.Receipt)
			{
				dataGridItems.DataGrid.DisplayLayout.Bands[0].Columns["SysDocID"].Hidden = true;
				dataGridItems.DataGrid.DisplayLayout.Bands[0].Columns["SysDocID"].Header.Caption = "Number";
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

		private void radioButtonThisMonth_CheckedChanged(object sender, EventArgs e)
		{
			dateControlDate.SelectedPeriod = DatePeriods.ThisMonthToDate;
		}

		private void radioButtonLastMonth_CheckedChanged(object sender, EventArgs e)
		{
			dateControlDate.SelectedPeriod = DatePeriods.LastMonth;
		}

		private void radioButtonThisYear_CheckedChanged(object sender, EventArgs e)
		{
			dateControlDate.SelectedPeriod = DatePeriods.ThisYearToDate;
		}

		private void radioButtonAllDate_CheckedChanged(object sender, EventArgs e)
		{
			dateControlDate.SelectedPeriod = DatePeriods.AllDates;
		}

		private void radioButtonToday_CheckedChanged(object sender, EventArgs e)
		{
			dateControlDate.SelectedPeriod = DatePeriods.Today;
		}

		private void radioButtonYesterday_CheckedChanged(object sender, EventArgs e)
		{
			dateControlDate.SelectedPeriod = DatePeriods.Yesterday;
		}

		private void checkBoxShowPreview_CheckedChanged(object sender, EventArgs e)
		{
			splitContainer1.Panel2Collapsed = !checkBoxShowPreview.Checked;
			Global.GlobalSettings.SaveSetting(base.Name + "Prev", checkBoxShowPreview.Checked);
		}

		private void radioButtonSelectedDate_CheckedChanged(object sender, EventArgs e)
		{
			dateTimePickerDate.Enabled = radioButtonSelectedDate.Checked;
			DateTimePickerDate_ValueChanged(sender, e);
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				UltraGridRow activeRow = dataGridItems.DataGrid.ActiveRow;
				if (activeRow != null && activeRow.IsDataRow)
				{
					string sysDocID = "";
					if (dataGridItems.DataGrid.DisplayLayout.Bands[0].Columns.Exists("SysDocID"))
					{
						sysDocID = activeRow.Cells["SysDocID"].Value.ToString();
					}
					string voucherID = activeRow.Cells["VoucherID"].Value.ToString();
					XtraReport transactionPreviewDoc = new FormHelper().GetTransactionPreviewDoc(sysDocID, voucherID);
					if (transactionPreviewDoc != null)
					{
						transactionPreviewDoc.CreateDocument();
						transactionPreviewDoc.RollPaper = true;
						e.Result = transactionPreviewDoc;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (e.Result != null)
				{
					XtraReport xtraReport = e.Result as XtraReport;
					printControl1.PrintingSystem = xtraReport.PrintingSystem;
					decimal d = xtraReport.PageWidth;
					decimal value = Math.Round(Convert.ToDecimal((decimal)checked(printControl1.Width - 30) / d), 2);
					printControl1.Zoom = (float)value;
					printControl1.ScrollPageUp();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonPrint_Click(object sender, EventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.DataGrid.ActiveRow;
			if (activeRow != null && activeRow.IsDataRow)
			{
				string sysDocID = "";
				if (dataGridItems.DataGrid.DisplayLayout.Bands[0].Columns.Exists("SysDocID"))
				{
					sysDocID = activeRow.Cells["SysDocID"].Value.ToString();
				}
				string voucherID = activeRow.Cells["VoucherID"].Value.ToString();
				try
				{
					DataSet salesPOSToPrint = Factory.SalesPOSSystem.GetSalesPOSToPrint(sysDocID, voucherID);
					if (salesPOSToPrint == null || salesPOSToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(salesPOSToPrint, sysDocID, "Sales POS", SysDocTypes.SalesPOS, isPrint: true, showPrintDialog: true);
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			LoadData();
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
			buttonRefresh = new DevExpress.XtraEditors.SimpleButton();
			buttonPrint = new DevExpress.XtraEditors.SimpleButton();
			radioButtonSelectedDate = new System.Windows.Forms.RadioButton();
			dateTimePickerDate = new Micromind.UISupport.MMSDateTimePicker(components);
			checkBoxShowPreview = new System.Windows.Forms.CheckBox();
			line1 = new Micromind.UISupport.Line();
			radioButtonYesterday = new System.Windows.Forms.RadioButton();
			radioButtonToday = new System.Windows.Forms.RadioButton();
			radioButtonThisYear = new System.Windows.Forms.RadioButton();
			radioButtonLastMonth = new System.Windows.Forms.RadioButton();
			radioButtonThisMonth = new System.Windows.Forms.RadioButton();
			buttonCancel = new DevExpress.XtraEditors.SimpleButton();
			buttonOK = new DevExpress.XtraEditors.SimpleButton();
			linePanelDown = new Micromind.UISupport.Line();
			textBoxSearch = new System.Windows.Forms.TextBox();
			defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(components);
			label6 = new System.Windows.Forms.Label();
			splitContainer1 = new System.Windows.Forms.SplitContainer();
			dateControlDate = new Micromind.DataControls.DateControl();
			dataGridItems = new Micromind.DataControls.MMSDataGrid.POSGrid();
			printControl1 = new DevExpress.XtraPrinting.Control.PrintControl();
			backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(buttonRefresh);
			panelButtons.Controls.Add(buttonPrint);
			panelButtons.Controls.Add(radioButtonSelectedDate);
			panelButtons.Controls.Add(dateTimePickerDate);
			panelButtons.Controls.Add(checkBoxShowPreview);
			panelButtons.Controls.Add(line1);
			panelButtons.Controls.Add(radioButtonYesterday);
			panelButtons.Controls.Add(radioButtonToday);
			panelButtons.Controls.Add(radioButtonThisYear);
			panelButtons.Controls.Add(radioButtonLastMonth);
			panelButtons.Controls.Add(radioButtonThisMonth);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 386);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(778, 56);
			panelButtons.TabIndex = 3;
			buttonRefresh.Appearance.Font = new System.Drawing.Font("Tahoma", 8f);
			buttonRefresh.Appearance.Options.UseFont = true;
			buttonRefresh.Location = new System.Drawing.Point(256, 28);
			buttonRefresh.Name = "buttonRefresh";
			buttonRefresh.Size = new System.Drawing.Size(76, 24);
			buttonRefresh.TabIndex = 18;
			buttonRefresh.Text = "Refresh";
			buttonRefresh.Click += new System.EventHandler(buttonRefresh_Click);
			buttonPrint.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonPrint.Appearance.Options.UseFont = true;
			buttonPrint.Location = new System.Drawing.Point(464, 9);
			buttonPrint.Name = "buttonPrint";
			buttonPrint.Size = new System.Drawing.Size(96, 40);
			buttonPrint.TabIndex = 16;
			buttonPrint.Text = "&Print";
			buttonPrint.Click += new System.EventHandler(buttonPrint_Click);
			radioButtonSelectedDate.AutoSize = true;
			radioButtonSelectedDate.Location = new System.Drawing.Point(88, 32);
			radioButtonSelectedDate.Name = "radioButtonSelectedDate";
			radioButtonSelectedDate.Size = new System.Drawing.Size(48, 17);
			radioButtonSelectedDate.TabIndex = 5;
			radioButtonSelectedDate.Text = "Date";
			radioButtonSelectedDate.UseVisualStyleBackColor = true;
			radioButtonSelectedDate.CheckedChanged += new System.EventHandler(radioButtonSelectedDate_CheckedChanged);
			dateTimePickerDate.Enabled = false;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(142, 30);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(108, 20);
			dateTimePickerDate.TabIndex = 6;
			dateTimePickerDate.Value = new System.DateTime(2018, 3, 10, 23, 47, 2, 162);
			checkBoxShowPreview.AutoSize = true;
			checkBoxShowPreview.Location = new System.Drawing.Point(365, 25);
			checkBoxShowPreview.Name = "checkBoxShowPreview";
			checkBoxShowPreview.Size = new System.Drawing.Size(93, 17);
			checkBoxShowPreview.TabIndex = 7;
			checkBoxShowPreview.Text = "Show preview";
			checkBoxShowPreview.UseVisualStyleBackColor = true;
			checkBoxShowPreview.CheckedChanged += new System.EventHandler(checkBoxShowPreview_CheckedChanged);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = true;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(345, 7);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(1, 40);
			line1.TabIndex = 15;
			line1.TabStop = false;
			radioButtonYesterday.AutoSize = true;
			radioButtonYesterday.Location = new System.Drawing.Point(83, 7);
			radioButtonYesterday.Name = "radioButtonYesterday";
			radioButtonYesterday.Size = new System.Drawing.Size(72, 17);
			radioButtonYesterday.TabIndex = 1;
			radioButtonYesterday.Text = "Yesterday";
			radioButtonYesterday.UseVisualStyleBackColor = true;
			radioButtonYesterday.CheckedChanged += new System.EventHandler(radioButtonYesterday_CheckedChanged);
			radioButtonToday.AutoSize = true;
			radioButtonToday.Checked = true;
			radioButtonToday.Location = new System.Drawing.Point(12, 7);
			radioButtonToday.Name = "radioButtonToday";
			radioButtonToday.Size = new System.Drawing.Size(55, 17);
			radioButtonToday.TabIndex = 0;
			radioButtonToday.TabStop = true;
			radioButtonToday.Text = "Today";
			radioButtonToday.UseVisualStyleBackColor = true;
			radioButtonToday.CheckedChanged += new System.EventHandler(radioButtonToday_CheckedChanged);
			radioButtonThisYear.AutoSize = true;
			radioButtonThisYear.Location = new System.Drawing.Point(12, 30);
			radioButtonThisYear.Name = "radioButtonThisYear";
			radioButtonThisYear.Size = new System.Drawing.Size(70, 17);
			radioButtonThisYear.TabIndex = 4;
			radioButtonThisYear.Text = "This Year";
			radioButtonThisYear.UseVisualStyleBackColor = true;
			radioButtonThisYear.CheckedChanged += new System.EventHandler(radioButtonThisYear_CheckedChanged);
			radioButtonLastMonth.AutoSize = true;
			radioButtonLastMonth.Location = new System.Drawing.Point(261, 8);
			radioButtonLastMonth.Name = "radioButtonLastMonth";
			radioButtonLastMonth.Size = new System.Drawing.Size(78, 17);
			radioButtonLastMonth.TabIndex = 3;
			radioButtonLastMonth.Text = "Last Month";
			radioButtonLastMonth.UseVisualStyleBackColor = true;
			radioButtonLastMonth.CheckedChanged += new System.EventHandler(radioButtonLastMonth_CheckedChanged);
			radioButtonThisMonth.AutoSize = true;
			radioButtonThisMonth.Location = new System.Drawing.Point(172, 9);
			radioButtonThisMonth.Name = "radioButtonThisMonth";
			radioButtonThisMonth.Size = new System.Drawing.Size(78, 17);
			radioButtonThisMonth.TabIndex = 2;
			radioButtonThisMonth.Text = "This Month";
			radioButtonThisMonth.UseVisualStyleBackColor = true;
			radioButtonThisMonth.CheckedChanged += new System.EventHandler(radioButtonThisMonth_CheckedChanged);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonCancel.Appearance.Options.UseFont = true;
			buttonCancel.Location = new System.Drawing.Point(668, 9);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 40);
			buttonCancel.TabIndex = 9;
			buttonCancel.Text = "&Cancel";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonOK.Appearance.Options.UseFont = true;
			buttonOK.Location = new System.Drawing.Point(567, 9);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 40);
			buttonOK.TabIndex = 8;
			buttonOK.Text = "&OK";
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(778, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			textBoxSearch.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxSearch.Location = new System.Drawing.Point(63, 14);
			textBoxSearch.Name = "textBoxSearch";
			textBoxSearch.Size = new System.Drawing.Size(708, 35);
			textBoxSearch.TabIndex = 4;
			textBoxSearch.TextChanged += new System.EventHandler(textBoxSearch_TextChanged);
			defaultLookAndFeel1.LookAndFeel.SkinName = "Money Twins";
			label6.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label6.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			label6.Location = new System.Drawing.Point(13, 22);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(39, 19);
			label6.TabIndex = 21;
			label6.Text = "Find:";
			splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainer1.Location = new System.Drawing.Point(0, 0);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Panel1.Controls.Add(dateControlDate);
			splitContainer1.Panel1.Controls.Add(label6);
			splitContainer1.Panel1.Controls.Add(textBoxSearch);
			splitContainer1.Panel1.Controls.Add(dataGridItems);
			splitContainer1.Panel2.Controls.Add(printControl1);
			splitContainer1.Panel2Collapsed = true;
			splitContainer1.Size = new System.Drawing.Size(778, 386);
			splitContainer1.SplitterDistance = 442;
			splitContainer1.TabIndex = 24;
			dateControlDate.CustomReportFieldName = "";
			dateControlDate.CustomReportKey = "";
			dateControlDate.CustomReportValueType = 1;
			dateControlDate.FromDate = new System.DateTime(2018, 3, 10, 0, 0, 0, 0);
			dateControlDate.Location = new System.Drawing.Point(12, 452);
			dateControlDate.Name = "dateControlDate";
			dateControlDate.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControlDate.Size = new System.Drawing.Size(275, 50);
			dateControlDate.TabIndex = 23;
			dateControlDate.ToDate = new System.DateTime(2018, 3, 10, 23, 59, 59, 59);
			dateControlDate.Visible = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridItems.Location = new System.Drawing.Point(12, 51);
			dataGridItems.Margin = new System.Windows.Forms.Padding(6);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowRowButtons = false;
			dataGridItems.Size = new System.Drawing.Size(754, 326);
			dataGridItems.TabIndex = 22;
			printControl1.AutoZoom = true;
			printControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			printControl1.HorizontalScrollBarVisibility = DevExpress.XtraEditors.ViewInfo.ScrollBarVisibility.Hidden;
			printControl1.IsMetric = false;
			printControl1.Location = new System.Drawing.Point(0, 0);
			printControl1.Name = "printControl1";
			printControl1.Size = new System.Drawing.Size(96, 100);
			printControl1.TabIndex = 1;
			printControl1.VerticalScrollBarVisibility = DevExpress.XtraEditors.ViewInfo.ScrollBarVisibility.Hidden;
			backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker1_DoWork);
			backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.FromArgb(227, 241, 254);
			base.ClientSize = new System.Drawing.Size(778, 442);
			base.Controls.Add(splitContainer1);
			base.Controls.Add(panelButtons);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(465, 215);
			base.Name = "TransactionLookupDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Select Transaction";
			panelButtons.ResumeLayout(false);
			panelButtons.PerformLayout();
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel1.PerformLayout();
			splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
