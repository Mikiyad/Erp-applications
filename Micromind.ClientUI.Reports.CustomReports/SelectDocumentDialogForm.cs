using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.CustomReports
{
	public class SelectDocumentDialogForm : Form
	{
		private Panel panelButtons;

		private XPButton buttonOK;

		private Line linePanelDown;

		private XPButton buttonCancel;

		private Label label1;

		private TextBox textBoxSearch;

		private DataGridList dataGridTransactionList;

		private List<string> hiddenColumns = new List<string>();

		public List<string> selectedDocuments = new List<string>();

		private bool isMultiSelect;

		public bool CanClose = true;

		private IContainer components;

		private string reportID = "";

		public bool IsMultiSelect
		{
			get
			{
				return isMultiSelect;
			}
			set
			{
				isMultiSelect = value;
				if (value && dataGridTransactionList.DataSource != null)
				{
					new DataSet();
					if (!(dataGridTransactionList.DataSource as DataSet).Tables[0].Columns.Contains("C"))
					{
						dataGridTransactionList.DisplayLayout.Bands[0].Columns.Insert(0, "C");
						dataGridTransactionList.DisplayLayout.Bands[0].Columns["C"].DataType = typeof(bool);
					}
					dataGridTransactionList.DisplayLayout.Bands[0].Columns["C"].CellActivation = Activation.AllowEdit;
					dataGridTransactionList.DisplayLayout.Bands[0].Columns["C"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
					dataGridTransactionList.DisplayLayout.Bands[0].Columns["C"].CellClickAction = CellClickAction.Edit;
					dataGridTransactionList.DisplayLayout.Bands[0].Columns["C"].Header.VisiblePosition = 0;
				}
			}
		}

		public DataSet DataSource
		{
			get
			{
				return (DataSet)dataGridTransactionList.DataSource;
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
				dataGridTransactionList.DataSource = value;
				dataGridTransactionList.DisplayLayout.Bands[0].Columns["SearchColumn"].Hidden = true;
				dataGridTransactionList.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
				if (IsMultiSelect && !dataGridTransactionList.DisplayLayout.Bands[0].Columns.Exists("C"))
				{
					UltraGridColumn ultraGridColumn = dataGridTransactionList.DisplayLayout.Bands[0].Columns.Insert(0, "C");
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
					if (dataGridTransactionList.DisplayLayout.Bands[0].Columns.Exists(hiddenColumn))
					{
						dataGridTransactionList.DisplayLayout.Bands[0].Columns[hiddenColumn].Hidden = true;
					}
				}
			}
		}

		public List<string> SelectedDocuments
		{
			get
			{
				List<string> list = new List<string>();
				foreach (UltraGridRow row in dataGridTransactionList.Rows)
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
					foreach (UltraGridRow row in dataGridTransactionList.Rows)
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
				foreach (UltraGridRow row in dataGridTransactionList.Rows)
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
					foreach (UltraGridRow row in dataGridTransactionList.Rows)
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

		public string ReportID
		{
			get
			{
				return reportID;
			}
			set
			{
				reportID = value;
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
				if (dataGridTransactionList.ActiveRow != null)
				{
					return dataGridTransactionList.ActiveRow;
				}
				return null;
			}
		}

		public event EventHandler ValidateSelection;

		public SelectDocumentDialogForm()
		{
			InitializeComponent();
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
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			label1 = new System.Windows.Forms.Label();
			textBoxSearch = new System.Windows.Forms.TextBox();
			dataGridTransactionList = new Micromind.UISupport.DataGridList(components);
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridTransactionList).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 339);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(565, 40);
			panelButtons.TabIndex = 7;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(353, 8);
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
			linePanelDown.Size = new System.Drawing.Size(565, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(455, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 2;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 9);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(31, 13);
			label1.TabIndex = 8;
			label1.Text = "Find:";
			textBoxSearch.Location = new System.Drawing.Point(48, 6);
			textBoxSearch.Name = "textBoxSearch";
			textBoxSearch.Size = new System.Drawing.Size(394, 21);
			textBoxSearch.TabIndex = 6;
			textBoxSearch.TextChanged += new System.EventHandler(textBoxSearch_TextChanged);
			dataGridTransactionList.AllowUnfittedView = false;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridTransactionList.DisplayLayout.Appearance = appearance;
			dataGridTransactionList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridTransactionList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridTransactionList.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridTransactionList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridTransactionList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridTransactionList.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridTransactionList.DisplayLayout.MaxColScrollRegions = 1;
			dataGridTransactionList.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridTransactionList.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridTransactionList.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridTransactionList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridTransactionList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridTransactionList.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridTransactionList.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridTransactionList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridTransactionList.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridTransactionList.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridTransactionList.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridTransactionList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridTransactionList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridTransactionList.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridTransactionList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridTransactionList.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridTransactionList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridTransactionList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridTransactionList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridTransactionList.LoadLayoutFailed = false;
			dataGridTransactionList.Location = new System.Drawing.Point(15, 33);
			dataGridTransactionList.Name = "dataGridTransactionList";
			dataGridTransactionList.ShowDeleteMenu = false;
			dataGridTransactionList.ShowMinusInRed = true;
			dataGridTransactionList.ShowNewMenu = false;
			dataGridTransactionList.Size = new System.Drawing.Size(538, 300);
			dataGridTransactionList.TabIndex = 9;
			dataGridTransactionList.Text = "dataGridList";
			AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			base.ClientSize = new System.Drawing.Size(565, 379);
			base.Controls.Add(dataGridTransactionList);
			base.Controls.Add(panelButtons);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxSearch);
			Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "SelectDocumentDialogForm";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Dialog Box";
			base.Load += new System.EventHandler(SelectDocumentDialogForm_Load);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridTransactionList).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		public void InitDialog()
		{
			Font = new Font("Tahoma", Font.Size, Font.Style);
			base.MinimizeBox = false;
			base.MaximizeBox = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			base.KeyPreview = true;
		}

		private void SelectDocumentDialogForm_Load(object sender, EventArgs e)
		{
			dataGridTransactionList.ApplyUIDesign();
			dataGridTransactionList.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
			textBoxSearch.Focus();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			CanClose = true;
			if (CanClose)
			{
				Close();
			}
		}

		private void dataGridTransactionList_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
		{
			if (isMultiSelect && dataGridTransactionList.Rows.Count > 1)
			{
				if (e.Row != null && e.Row.IsDataRow)
				{
					bool result = false;
					if (e.Row.Cells["C"].Value != null)
					{
						bool.TryParse(e.Row.Cells["C"].Value.ToString(), out result);
					}
					e.Row.Cells["C"].Value = !result;
				}
			}
			else
			{
				SelectItem();
			}
		}

		private void SelectItem()
		{
			CanClose = true;
			if (dataGridTransactionList.Rows.VisibleRowCount == 0 || dataGridTransactionList.ActiveRow == null)
			{
				ErrorHelper.InformationMessage("Please select an Item");
				return;
			}
			if (this.ValidateSelection != null)
			{
				this.ValidateSelection(this, null);
			}
			if (CanClose && dataGridTransactionList.ActiveRow != null)
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			SelectItem();
		}

		private void textBoxSearch_TextChanged(object sender, EventArgs e)
		{
			if (DataSource != null && dataGridTransactionList.DisplayLayout.Bands[0].Columns.Exists("SearchColumn"))
			{
				dataGridTransactionList.BeginUpdate();
				if (dataGridTransactionList.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions.Count == 0)
				{
					dataGridTransactionList.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions.Add(new FilterCondition());
				}
				dataGridTransactionList.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions[0].ComparisionOperator = FilterComparisionOperator.Contains;
				dataGridTransactionList.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions[0].CompareValue = textBoxSearch.Text;
				dataGridTransactionList.EndUpdate();
				if (dataGridTransactionList.Rows.VisibleRowCount > 0)
				{
					dataGridTransactionList.ActiveRow = dataGridTransactionList.Rows.GetRowAtVisibleIndex(0);
				}
			}
		}
	}
}
