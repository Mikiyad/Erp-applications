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

namespace Micromind.ClientUI.Reports.Legal
{
	public class PendingCaseReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private Button buttonClose;

		private DateControl dateControl1;

		private GenericListComboBox ComboBoxLegalStatus;

		private MMLabel mmLabel11;

		private UltraGroupBox ultraGroupBox1;

		private CaseClientSelector caseClientSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsLegal;

		public int ScreenID => 7022;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public PendingCaseReport()
		{
			InitializeComponent();
			base.Load += Form_Load;
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
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

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				DataSet data = Factory.LegalActivitySystem.GetPendingCasesReport(dateControl1.FromDate, dateControl1.ToDate, caseClientSelector.FromCustomer, caseClientSelector.ToCustomer, ComboBoxLegalStatus.SelectedID);
				ReportHelper reportHelper = new ReportHelper();
				string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				XtraReport report = reportHelper.GetReport("PendingCasesReport");
				if (report == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'PendingCases Report.repx'");
				}
				else
				{
					report.DataSource = data;
					reportHelper.ShowReport(report);
				}
			}
			catch (Exception ex)
			{
				ErrorHelper.ErrorMessage(ex.Message);
			}
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		public IEnumerable<Control> GetAllFormControls(Control control)
		{
			IEnumerable<Control> controls = control.Controls.Cast<Control>();
			return controls.SelectMany((Control ctrl) => GetAllFormControls(ctrl).Concat(controls));
		}

		private void dateControl1_Load(object sender, EventArgs e)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Legal.PendingCaseReport));
			buttonOK = new System.Windows.Forms.Button();
			buttonClose = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			caseClientSelector = new Micromind.DataControls.CaseClientSelector();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			ComboBoxLegalStatus = new Micromind.DataControls.GenericListComboBox();
			dateControl1 = new Micromind.DataControls.DateControl();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ComboBoxLegalStatus).BeginInit();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(249, 229);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(102, 24);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(353, 229);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(102, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			ultraGroupBox1.Controls.Add(caseClientSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(8, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(445, 107);
			ultraGroupBox1.TabIndex = 11;
			ultraGroupBox1.Text = "Case Clients";
			caseClientSelector.BackColor = System.Drawing.Color.Transparent;
			caseClientSelector.CustomReportFieldName = "";
			caseClientSelector.CustomReportKey = "";
			caseClientSelector.CustomReportValueType = 1;
			caseClientSelector.Location = new System.Drawing.Point(11, 21);
			caseClientSelector.Name = "caseClientSelector";
			caseClientSelector.Size = new System.Drawing.Size(428, 80);
			caseClientSelector.TabIndex = 0;
			mmLabel11.AutoSize = true;
			mmLabel11.BackColor = System.Drawing.Color.Transparent;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel11.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(8, 224);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(70, 13);
			mmLabel11.TabIndex = 13;
			mmLabel11.Text = "Legal Status:";
			ComboBoxLegalStatus.Assigned = false;
			ComboBoxLegalStatus.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxLegalStatus.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxLegalStatus.CustomReportFieldName = "";
			ComboBoxLegalStatus.CustomReportKey = "";
			ComboBoxLegalStatus.CustomReportValueType = 1;
			ComboBoxLegalStatus.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxLegalStatus.DisplayLayout.Appearance = appearance;
			ComboBoxLegalStatus.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxLegalStatus.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxLegalStatus.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxLegalStatus.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			ComboBoxLegalStatus.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxLegalStatus.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			ComboBoxLegalStatus.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxLegalStatus.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxLegalStatus.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxLegalStatus.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			ComboBoxLegalStatus.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxLegalStatus.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxLegalStatus.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxLegalStatus.DisplayLayout.Override.CellAppearance = appearance8;
			ComboBoxLegalStatus.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxLegalStatus.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxLegalStatus.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			ComboBoxLegalStatus.DisplayLayout.Override.HeaderAppearance = appearance10;
			ComboBoxLegalStatus.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxLegalStatus.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			ComboBoxLegalStatus.DisplayLayout.Override.RowAppearance = appearance11;
			ComboBoxLegalStatus.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxLegalStatus.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			ComboBoxLegalStatus.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxLegalStatus.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxLegalStatus.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxLegalStatus.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxLegalStatus.Editable = true;
			ComboBoxLegalStatus.FilterString = "";
			ComboBoxLegalStatus.GenericListType = Micromind.Common.Data.GenericListTypes.ActionStatus;
			ComboBoxLegalStatus.HasAllAccount = false;
			ComboBoxLegalStatus.HasCustom = false;
			ComboBoxLegalStatus.IsDataLoaded = false;
			ComboBoxLegalStatus.IsSingleColumn = false;
			ComboBoxLegalStatus.Location = new System.Drawing.Point(84, 220);
			ComboBoxLegalStatus.MaxDropDownItems = 12;
			ComboBoxLegalStatus.Name = "ComboBoxLegalStatus";
			ComboBoxLegalStatus.ShowInactiveItems = false;
			ComboBoxLegalStatus.ShowQuickAdd = true;
			ComboBoxLegalStatus.Size = new System.Drawing.Size(100, 20);
			ComboBoxLegalStatus.TabIndex = 12;
			ComboBoxLegalStatus.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 11, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(14, 125);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(441, 58);
			dateControl1.TabIndex = 6;
			dateControl1.ToDate = new System.DateTime(2017, 11, 28, 23, 59, 59, 59);
			dateControl1.Load += new System.EventHandler(dateControl1_Load);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(465, 259);
			base.Controls.Add(mmLabel11);
			base.Controls.Add(ComboBoxLegalStatus);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(dateControl1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "PendingCaseReport";
			Text = "Legal Activity Status Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ComboBoxLegalStatus).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
