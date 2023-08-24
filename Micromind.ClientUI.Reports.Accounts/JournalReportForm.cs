using DevExpress.XtraReports.UI;
using Infragistics.Win.Misc;
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
	public class JournalReportForm : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private DateControl dateControl1;

		private Button buttonOK;

		private CheckBox checkBoxShowForeignCurrency;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private CheckBox checkBoxShowVoid;

		private UltraGroupBox ultraGroupBox2;

		private LocationSelector locationSelector;

		private UltraGroupBox ultraGroupBox1;

		private DivisionSelector divisionSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsAccounts;

		public int ScreenID => 7006;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public JournalReportForm()
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
			DataSet data = Factory.JournalSystem.GetJournalReport(dateControl1.FromDate, dateControl1.ToDate, locationSelector.FromLocation, locationSelector.ToLocation, divisionSelector.FromDivision, divisionSelector.ToDivision, checkBoxShowVoid.Checked);
			if (data.Tables.Count > 1)
			{
				string baseCurrencyID = Global.BaseCurrencyID;
				data.Tables[0].Columns.Add("CurrencyNote");
				foreach (DataRow row in data.Tables[0].Rows)
				{
					if (row["CurrencyID"] != DBNull.Value && row["CurrencyID"].ToString() != baseCurrencyID)
					{
						row["CurrencyNote"] = "Cur: " + row["CurrencyID"].ToString() + "  -  Rate: " + decimal.Parse(row["CurrencyRate"].ToString()).ToString(Format.QuantityFormat);
					}
				}
			}
			if (data.Tables.Count > 0)
			{
				data.Tables[0].Columns.Add("SysDocTypeName");
				foreach (DataRow row2 in data.Tables[0].Rows)
				{
					if (row2["SysDocType"] != DBNull.Value)
					{
						int sysDocType = int.Parse(row2["SysDocType"].ToString());
						row2["SysDocTypeName"] = PublicFunctions.GetSysDocTypeString(sysDocType);
					}
				}
			}
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			XtraReport xtraReport = (!checkBoxShowForeignCurrency.Checked) ? reportHelper.GetReport("Journal") : reportHelper.GetReport("Journal Detail");
			if (xtraReport == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.");
				return;
			}
			xtraReport.DataSource = data;
			reportHelper.ShowReport(xtraReport);
		}

		public IEnumerable<Control> GetAllFormControls(Control control)
		{
			IEnumerable<Control> controls = control.Controls.Cast<Control>();
			return controls.SelectMany((Control ctrl) => GetAllFormControls(ctrl).Concat(controls));
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Accounts.JournalReportForm));
			dateControl1 = new Micromind.DataControls.DateControl();
			buttonOK = new System.Windows.Forms.Button();
			checkBoxShowForeignCurrency = new System.Windows.Forms.CheckBox();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			checkBoxShowVoid = new System.Windows.Forms.CheckBox();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			locationSelector = new Micromind.DataControls.LocationSelector();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			divisionSelector = new Micromind.DataControls.DivisionSelector();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 11, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(12, 12);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(378, 53);
			dateControl1.TabIndex = 0;
			dateControl1.Tag = "Date";
			dateControl1.ToDate = new System.DateTime(2017, 11, 16, 23, 59, 59, 59);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(256, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			checkBoxShowForeignCurrency.AutoSize = true;
			checkBoxShowForeignCurrency.Location = new System.Drawing.Point(19, 168);
			checkBoxShowForeignCurrency.Name = "checkBoxShowForeignCurrency";
			checkBoxShowForeignCurrency.Size = new System.Drawing.Size(316, 17);
			checkBoxShowForeignCurrency.TabIndex = 2;
			checkBoxShowForeignCurrency.Text = "Show original currency details in foreign currency transactions";
			checkBoxShowForeignCurrency.UseVisualStyleBackColor = true;
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 223);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(463, 40);
			panelButtons.TabIndex = 3;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(463, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(355, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			checkBoxShowVoid.AutoSize = true;
			checkBoxShowVoid.Location = new System.Drawing.Point(19, 191);
			checkBoxShowVoid.Name = "checkBoxShowVoid";
			checkBoxShowVoid.Size = new System.Drawing.Size(148, 17);
			checkBoxShowVoid.TabIndex = 2;
			checkBoxShowVoid.Text = "Show voided transactions";
			checkBoxShowVoid.UseVisualStyleBackColor = true;
			ultraGroupBox2.Controls.Add(locationSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(12, 71);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(428, 81);
			ultraGroupBox2.TabIndex = 14;
			ultraGroupBox2.Text = "Location";
			ultraGroupBox2.Visible = false;
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(16, 19);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(412, 59);
			locationSelector.TabIndex = 7;
			ultraGroupBox1.Controls.Add(divisionSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(23, 71);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(428, 81);
			ultraGroupBox1.TabIndex = 18;
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
			base.ClientSize = new System.Drawing.Size(463, 263);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(panelButtons);
			base.Controls.Add(checkBoxShowVoid);
			base.Controls.Add(checkBoxShowForeignCurrency);
			base.Controls.Add(dateControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "JournalReportForm";
			Text = "Journal Report";
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
