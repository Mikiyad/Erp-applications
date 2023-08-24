using DevExpress.XtraReports.UI;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.Accounts
{
	public class DailyCashReportForm : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private DateControl dateControl1;

		private Button buttonOK;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private CheckBox checkBoxAll;

		private Label label1;

		private LocationSelector locationSelector1;

		private UltraGroupBox ultraGroupBox2;

		private AllAccountsComboBox comboBoxCashAccount;

		private UltraGroupBox ultraGroupBox1;

		private DivisionSelector divisionSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsAccounts;

		public int ScreenID => 7004;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public DailyCashReportForm()
		{
			InitializeComponent();
			base.Load += Form_Load;
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				locationSelector1.Visible = false;
				_ = base.IsDisposed;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
			}
		}

		public IEnumerable<Control> GetAllFormControls(Control control)
		{
			IEnumerable<Control> controls = control.Controls.Cast<Control>();
			return controls.SelectMany((Control ctrl) => GetAllFormControls(ctrl).Concat(controls));
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			object fieldValue = Factory.DatabaseSystem.GetFieldValue("Users", "DefaultTransactionRegisterID", "UserID", Global.CurrentUser);
			string registerID = "";
			if (fieldValue != null)
			{
				if (fieldValue.ToString() != "" && checkBoxAll.Checked)
				{
					ErrorHelper.ErrorMessage("You are not Allowed to view report");
					return;
				}
				object fieldValue2 = Factory.DatabaseSystem.GetFieldValue("Register", "CashAccountID", "RegisterID", fieldValue.ToString());
				if (fieldValue2 != null)
				{
					if (fieldValue2.ToString() == "" && !checkBoxAll.Checked)
					{
						ErrorHelper.ErrorMessage("You are not Allowed to view report");
						return;
					}
					if (!checkBoxAll.Checked)
					{
						registerID = comboBoxCashAccount.SelectedID;
						registerID = fieldValue2.ToString();
					}
					else
					{
						registerID = "";
					}
				}
			}
			DataSet data = Factory.JournalSystem.GetDailyCashReport(dateControl1.FromDate, dateControl1.ToDate, registerID);
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			XtraReport report = reportHelper.GetReport("Daily Cash");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "'Daily Cash.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
				return;
			}
			report.DataSource = data;
			reportHelper.ShowReport(report);
		}

		private string GetCashFlowSysDocTypeString(int sysDocType)
		{
			switch (sysDocType)
			{
			case 8:
				return "Operating Expenses";
			case 4:
				return "Payments";
			case 34:
				return "Purchases";
			case 35:
				return "Purchase Return";
			case 3:
				return "Cash Sale";
			case 28:
				return "Sales Return";
			case 7:
				return "Cheque Deposit";
			case 9:
				return "Expenses Paid by Cheque";
			case 5:
				return "Payment by Cheque";
			case 2:
				return "Received Cheque Payments";
			case 11:
				return "Credit Note";
			case 37:
				return "Credit Purchase Return";
			case 27:
				return "Credit Sales Return";
			case 10:
				return "Debit Note";
			case 41:
				return "Deposit";
			case 42:
				return "Expense";
			case 6:
				return "Transfer";
			case 1:
				return "Other Activities";
			case 39:
				return "Import Purchase Invoice";
			case 18:
				return "Inventory Adjustment";
			case 15:
				return "Issued Cheque Cancellation";
			case 14:
				return "Issued Cheque Clearance";
			case 16:
				return "Issued Cheque Return";
			case 17:
				return "Issued Security Chq";
			case 36:
				return "Packing List";
			case 33:
				return "Purchase Invoice";
			case 31:
				return "Purchase Order";
			case 30:
				return "Purchase Quote";
			case 32:
				return "Goods Received Note";
			case 13:
				return "Rec. Chq Cancellation";
			case 12:
				return "Returned Cheque";
			case 21:
				return "Stock Transfer Ret";
			case 25:
				return "Sales Invoice";
			case 23:
				return "Sales Order";
			case 22:
				return "Sales Quote";
			case 26:
				return "Cash Sale";
			case 20:
				return "Stock Transfer In";
			case 19:
				return "Stock Transfer Out";
			default:
				return sysDocType.ToString();
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Accounts.DailyCashReportForm));
			dateControl1 = new Micromind.DataControls.DateControl();
			buttonOK = new System.Windows.Forms.Button();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			checkBoxAll = new System.Windows.Forms.CheckBox();
			label1 = new System.Windows.Forms.Label();
			locationSelector1 = new Micromind.DataControls.LocationSelector();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			comboBoxCashAccount = new Micromind.DataControls.AllAccountsComboBox();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			divisionSelector = new Micromind.DataControls.DivisionSelector();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCashAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 12, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(12, 12);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(277, 60);
			dateControl1.TabIndex = 0;
			dateControl1.Tag = "Date";
			dateControl1.ToDate = new System.DateTime(2017, 12, 3, 23, 59, 59, 59);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(281, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 194);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(491, 40);
			panelButtons.TabIndex = 3;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(491, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(381, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			checkBoxAll.AutoSize = true;
			checkBoxAll.Location = new System.Drawing.Point(307, 35);
			checkBoxAll.Name = "checkBoxAll";
			checkBoxAll.Size = new System.Drawing.Size(86, 17);
			checkBoxAll.TabIndex = 20;
			checkBoxAll.Text = "All Locations";
			checkBoxAll.UseVisualStyleBackColor = true;
			checkBoxAll.Visible = false;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(136, 30);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(48, 13);
			label1.TabIndex = 19;
			label1.Text = "Location";
			label1.Visible = false;
			locationSelector1.BackColor = System.Drawing.Color.Transparent;
			locationSelector1.CustomReportFieldName = "";
			locationSelector1.CustomReportKey = "";
			locationSelector1.CustomReportValueType = 1;
			locationSelector1.Location = new System.Drawing.Point(16, 19);
			locationSelector1.Name = "locationSelector1";
			locationSelector1.Size = new System.Drawing.Size(412, 60);
			locationSelector1.TabIndex = 7;
			ultraGroupBox2.Controls.Add(locationSelector1);
			ultraGroupBox2.Controls.Add(label1);
			ultraGroupBox2.Location = new System.Drawing.Point(17, 66);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(453, 83);
			ultraGroupBox2.TabIndex = 21;
			ultraGroupBox2.Text = "Location";
			ultraGroupBox2.Visible = false;
			comboBoxCashAccount.Assigned = false;
			comboBoxCashAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCashAccount.CustomReportFieldName = "";
			comboBoxCashAccount.CustomReportKey = "";
			comboBoxCashAccount.CustomReportValueType = 1;
			comboBoxCashAccount.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCashAccount.DisplayLayout.Appearance = appearance;
			comboBoxCashAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCashAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCashAccount.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCashAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxCashAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCashAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxCashAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCashAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCashAccount.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCashAccount.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxCashAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCashAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCashAccount.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCashAccount.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxCashAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCashAccount.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCashAccount.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxCashAccount.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxCashAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCashAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxCashAccount.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxCashAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCashAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxCashAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCashAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCashAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCashAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCashAccount.Editable = true;
			comboBoxCashAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxCashAccount.FilterString = "";
			comboBoxCashAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.Cash;
			comboBoxCashAccount.FilterSysDocID = "";
			comboBoxCashAccount.HasAllAccount = false;
			comboBoxCashAccount.HasCustom = false;
			comboBoxCashAccount.IsDataLoaded = false;
			comboBoxCashAccount.Location = new System.Drawing.Point(212, 30);
			comboBoxCashAccount.MaxDropDownItems = 12;
			comboBoxCashAccount.Name = "comboBoxCashAccount";
			comboBoxCashAccount.ShowInactiveItems = false;
			comboBoxCashAccount.ShowQuickAdd = true;
			comboBoxCashAccount.Size = new System.Drawing.Size(137, 20);
			comboBoxCashAccount.TabIndex = 18;
			comboBoxCashAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCashAccount.Visible = false;
			ultraGroupBox1.Controls.Add(divisionSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 66);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(458, 81);
			ultraGroupBox1.TabIndex = 22;
			ultraGroupBox1.Text = "Division";
			divisionSelector.BackColor = System.Drawing.Color.Transparent;
			divisionSelector.CustomReportFieldName = "";
			divisionSelector.CustomReportKey = "";
			divisionSelector.CustomReportValueType = 1;
			divisionSelector.Location = new System.Drawing.Point(6, 19);
			divisionSelector.Name = "divisionSelector";
			divisionSelector.Size = new System.Drawing.Size(414, 54);
			divisionSelector.TabIndex = 0;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(491, 234);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(checkBoxAll);
			base.Controls.Add(panelButtons);
			base.Controls.Add(dateControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "DailyCashReportForm";
			Text = "Daily Cash Report";
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ultraGroupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCashAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
