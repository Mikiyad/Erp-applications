using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Projects
{
	public class JobFeeScheduledTermDialog : Form
	{
		private DataTable jobFeeTermTable;

		private JobData currentJobData;

		private bool isReturn;

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Line linePanelDown;

		private XPButton buttonCancel;

		private Panel panelDetails;

		private TextBox textBoxJobName;

		private Label label2;

		private TextBox textBoxJobCode;

		private Label label4;

		private TextBox textBoxFeeAmount;

		private Label labelFeeCode;

		private Label labelFeeAmount;

		private Label labelFeeName;

		private TextBox textBoxFeeName;

		private TextBox textBoxFeeCode;

		private DataEntryGrid dataGridItems;

		public DataTable JobFeeTermTable
		{
			get
			{
				return jobFeeTermTable;
			}
			set
			{
				jobFeeTermTable = value;
			}
		}

		public JobData CurrentJobData
		{
			get
			{
				return currentJobData;
			}
			set
			{
				currentJobData = value;
			}
		}

		public string JobID
		{
			get
			{
				return textBoxJobCode.Text;
			}
			set
			{
				textBoxJobCode.Text = value;
			}
		}

		public string JobName
		{
			get
			{
				return textBoxJobName.Text;
			}
			set
			{
				textBoxJobName.Text = value;
			}
		}

		public string FeeID
		{
			get
			{
				return textBoxFeeCode.Text;
			}
			set
			{
				textBoxFeeCode.Text = value;
			}
		}

		public string FeeName
		{
			get
			{
				return textBoxFeeName.Text;
			}
			set
			{
				textBoxFeeName.Text = value;
			}
		}

		public decimal FeeAmount
		{
			get
			{
				return decimal.Parse(textBoxFeeAmount.Text);
			}
			set
			{
				textBoxFeeAmount.Text = value.ToString();
			}
		}

		public string CustomerID
		{
			get;
			set;
		}

		public JobFeeScheduledTermDialog()
		{
			InitializeComponent();
			base.Activated += JobFeeScheduledTermDialog_Activated;
			base.StartPosition = FormStartPosition.CenterParent;
			base.Load += JobFeeScheduledTermDialog_Load;
			base.FormClosing += JobFeeScheduledTermDialog_FormClosing;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.CellDataError += dataGridItems_CellDataError;
			if (CompanyPreferences.UseProjectPhase)
			{
				labelFeeCode.Text = "Phase Code:";
				labelFeeName.Text = "Phase Name:";
				labelFeeAmount.Text = "Phase Amount:";
				Text = "Project Phase Payment Term";
			}
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "Amount" && e.Cell.IsActiveCell)
			{
				decimal num = decimal.Parse(textBoxFeeAmount.Text);
				decimal d = Convert.ToDecimal(e.Cell.Value.ToString());
				decimal num2 = default(decimal);
				if (num > 0m)
				{
					num2 = Math.Round(d / num, 2) * 100m;
				}
				e.Cell.Row.Cells["AmountPercent"].Value = num2;
			}
			else if (e.Cell.Column.Key == "AmountPercent" && e.Cell.IsActiveCell)
			{
				decimal num3 = decimal.Parse(textBoxFeeAmount.Text);
				decimal d2 = Convert.ToDecimal(e.Cell.Value.ToString());
				decimal num4 = default(decimal);
				if (num3 > 0m)
				{
					num4 = Math.Round(d2 * num3 / 100m, Global.CurDecimalPoints);
				}
				e.Cell.Row.Cells["Amount"].Value = num4;
			}
		}

		private void dataGridItems_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Amount")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Invalid amount. Please enter a non-negative numeric value.");
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "AmountPercent")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Invalid amount. Please enter a numeric value between 0 and 100.");
			}
		}

		private void JobFeeScheduledTermDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			Global.GlobalSettings.SaveFormProperties(this);
		}

		private void JobFeeScheduledTermDialog_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridItems.SetupUI();
				Global.GlobalSettings.LoadFormProperties(this);
				SetupGrid();
				FillData();
			}
			catch (Exception e2)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e2);
			}
		}

		private void FillData()
		{
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			if (JobFeeTermTable != null)
			{
				foreach (DataRow row in JobFeeTermTable.Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["DueDate"] = row["DueDate"];
					dataRow2["Amount"] = row["Amount"];
					dataRow2["AmountPercent"] = row["AmountPercent"];
					dataRow2["Description"] = row["Description"];
					dataTable.Rows.Add(dataRow2);
				}
				dataTable.AcceptChanges();
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("DueDate", typeof(DateTime));
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("AmountPercent", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Columns["DueDate"].Header.Caption = "Date";
				dataGridItems.DisplayLayout.Bands[0].Columns["AmountPercent"].Header.Caption = "Percent";
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["AmountPercent"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["AmountPercent"].Format = "n";
				dataGridItems.DisplayLayout.Bands[0].Columns["AmountPercent"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["AmountPercent"].MaxValue = 100;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Amount", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Amount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Percent", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["AmountPercent"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.SetupUI();
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void JobFeeScheduledTermDialog_Activated(object sender, EventArgs e)
		{
			dataGridItems.Focus();
		}

		private void Grid_AfterCellActivate(object sender, EventArgs e)
		{
		}

		private bool ValidateData()
		{
			decimal d = decimal.Parse(textBoxFeeAmount.Text);
			decimal d2 = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (row.Cells["Amount"].Value == null || row.Cells["Amount"].Value.ToString() == "")
				{
					ErrorHelper.WarningMessage("Please enter amount for all rows.");
					return false;
				}
				if (row.Cells["AmountPercent"].Value == null || row.Cells["Amount"].Value.ToString() == "")
				{
					ErrorHelper.WarningMessage("Please enter amount percent for all rows.");
					return false;
				}
				d2 += decimal.Parse(row.Cells["Amount"].Value.ToString());
			}
			if (d2 != 0m && d2 != d)
			{
				ErrorHelper.WarningMessage("Total scheduled payments must be equal to fee amount.");
				return false;
			}
			return true;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				dataGridItems.PerformAction(UltraGridAction.ExitEditMode);
				if (!ValidateData())
				{
					base.DialogResult = DialogResult.None;
				}
				else if (SaveRows())
				{
					base.DialogResult = DialogResult.OK;
					Close();
				}
				else
				{
					base.DialogResult = DialogResult.None;
				}
			}
			catch (Exception e2)
			{
				base.DialogResult = DialogResult.None;
				ErrorHelper.ProcessError(e2);
			}
		}

		private bool SaveRows()
		{
			try
			{
				DataTable jobFeePaymentTermTable = new JobData().JobFeePaymentTermTable;
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow = jobFeePaymentTermTable.NewRow();
					dataRow["JobID"] = textBoxJobCode.Text;
					dataRow["FeeID"] = FeeID;
					dataRow["DueDate"] = row.Cells["DueDate"].Value.ToString();
					dataRow["Amount"] = row.Cells["Amount"].Value.ToString();
					dataRow["AmountPercent"] = row.Cells["AmountPercent"].Value.ToString();
					dataRow["Description"] = row.Cells["Description"].Value.ToString();
					dataRow["RowIndex"] = row.Index;
					dataRow.EndEdit();
					jobFeePaymentTermTable.Rows.Add(dataRow);
				}
				JobFeeTermTable = jobFeePaymentTermTable.Copy();
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void labelCustomize_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void checkBoxUnitPriceOverWrite_CheckedChanged(object sender, EventArgs e)
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
			panelDetails = new System.Windows.Forms.Panel();
			labelFeeAmount = new System.Windows.Forms.Label();
			textBoxFeeAmount = new System.Windows.Forms.TextBox();
			labelFeeName = new System.Windows.Forms.Label();
			labelFeeCode = new System.Windows.Forms.Label();
			textBoxJobName = new System.Windows.Forms.TextBox();
			textBoxFeeName = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			textBoxFeeCode = new System.Windows.Forms.TextBox();
			textBoxJobCode = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 350);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(489, 40);
			panelButtons.TabIndex = 4;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(280, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 0;
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
			linePanelDown.Size = new System.Drawing.Size(489, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(381, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 1;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			panelDetails.Controls.Add(labelFeeAmount);
			panelDetails.Controls.Add(textBoxFeeAmount);
			panelDetails.Controls.Add(labelFeeName);
			panelDetails.Controls.Add(labelFeeCode);
			panelDetails.Controls.Add(textBoxJobName);
			panelDetails.Controls.Add(textBoxFeeName);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(textBoxFeeCode);
			panelDetails.Controls.Add(textBoxJobCode);
			panelDetails.Controls.Add(label4);
			panelDetails.Location = new System.Drawing.Point(0, 1);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(487, 136);
			panelDetails.TabIndex = 0;
			labelFeeAmount.AutoSize = true;
			labelFeeAmount.Location = new System.Drawing.Point(13, 99);
			labelFeeAmount.Name = "labelFeeAmount";
			labelFeeAmount.Size = new System.Drawing.Size(67, 13);
			labelFeeAmount.TabIndex = 152;
			labelFeeAmount.Text = "Fee Amount:";
			textBoxFeeAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFeeAmount.Location = new System.Drawing.Point(111, 96);
			textBoxFeeAmount.MaxLength = 64;
			textBoxFeeAmount.Name = "textBoxFeeAmount";
			textBoxFeeAmount.ReadOnly = true;
			textBoxFeeAmount.Size = new System.Drawing.Size(132, 20);
			textBoxFeeAmount.TabIndex = 4;
			textBoxFeeAmount.TabStop = false;
			textBoxFeeAmount.Text = "0.00";
			textBoxFeeAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			labelFeeName.AutoSize = true;
			labelFeeName.Location = new System.Drawing.Point(13, 77);
			labelFeeName.Name = "labelFeeName";
			labelFeeName.Size = new System.Drawing.Size(59, 13);
			labelFeeName.TabIndex = 151;
			labelFeeName.Text = "Fee Name:";
			labelFeeCode.AutoSize = true;
			labelFeeCode.Location = new System.Drawing.Point(12, 55);
			labelFeeCode.Name = "labelFeeCode";
			labelFeeCode.Size = new System.Drawing.Size(56, 13);
			labelFeeCode.TabIndex = 151;
			labelFeeCode.Text = "Fee Code:";
			textBoxJobName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxJobName.Location = new System.Drawing.Point(111, 31);
			textBoxJobName.MaxLength = 64;
			textBoxJobName.Name = "textBoxJobName";
			textBoxJobName.ReadOnly = true;
			textBoxJobName.Size = new System.Drawing.Size(364, 20);
			textBoxJobName.TabIndex = 2;
			textBoxJobName.TabStop = false;
			textBoxFeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFeeName.Location = new System.Drawing.Point(111, 74);
			textBoxFeeName.MaxLength = 64;
			textBoxFeeName.Name = "textBoxFeeName";
			textBoxFeeName.ReadOnly = true;
			textBoxFeeName.Size = new System.Drawing.Size(364, 20);
			textBoxFeeName.TabIndex = 0;
			textBoxFeeName.TabStop = false;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(13, 34);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(74, 13);
			label2.TabIndex = 147;
			label2.Text = "Project Name:";
			textBoxFeeCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFeeCode.Location = new System.Drawing.Point(111, 52);
			textBoxFeeCode.MaxLength = 64;
			textBoxFeeCode.Name = "textBoxFeeCode";
			textBoxFeeCode.ReadOnly = true;
			textBoxFeeCode.Size = new System.Drawing.Size(132, 20);
			textBoxFeeCode.TabIndex = 0;
			textBoxFeeCode.TabStop = false;
			textBoxJobCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxJobCode.Location = new System.Drawing.Point(111, 10);
			textBoxJobCode.MaxLength = 64;
			textBoxJobCode.Name = "textBoxJobCode";
			textBoxJobCode.ReadOnly = true;
			textBoxJobCode.Size = new System.Drawing.Size(132, 20);
			textBoxJobCode.TabIndex = 0;
			textBoxJobCode.TabStop = false;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(13, 13);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(71, 13);
			label4.TabIndex = 140;
			label4.Text = "Project Code:";
			dataGridItems.AllowAddNew = false;
			dataGridItems.AllowCustomizeHeaders = true;
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
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.Location = new System.Drawing.Point(12, 143);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(465, 201);
			dataGridItems.TabIndex = 13;
			dataGridItems.Text = "dataEntryGrid1";
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(489, 390);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(panelDetails);
			base.Controls.Add(panelButtons);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(465, 215);
			base.Name = "JobFeeScheduledTermDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Project Fee Payment Term";
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
		}
	}
}
