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
	public class GLReportForm : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private DateControl dateControl1;

		private Button buttonOK;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private UltraGroupBox ultraGroupBox1;

		private LocationSelector locationSelector;

		private UltraGroupBox ultraGroupBox2;

		private DivisionSelector divisionSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsAccounts;

		public int ScreenID => 7005;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public GLReportForm()
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
			DataSet data = Factory.JournalSystem.GetGLReport(dateControl1.FromDate, dateControl1.ToDate, locationSelector.FromLocation, locationSelector.ToLocation, divisionSelector.FromDivision, divisionSelector.ToDivision);
			if (data.Tables.Count > 1)
			{
				string baseCurrencyID = Global.BaseCurrencyID;
				data.Tables[1].Columns.Add("CurrencyNote");
				foreach (DataRow row in data.Tables[1].Rows)
				{
					if (row["CurrencyID"] != DBNull.Value && row["CurrencyID"].ToString() != baseCurrencyID)
					{
						row["CurrencyNote"] = "Cur: " + row["CurrencyID"].ToString() + "  -  Rate: " + decimal.Parse(row["CurrencyRate"].ToString()).ToString(Format.QuantityFormat);
						if (row["DebitFC"] != DBNull.Value && row["DebitFC"].ToString() != "" && decimal.Parse(row["DebitFC"].ToString()) != 0m)
						{
							row["CurrencyNote"] = row["CurrencyNote"].ToString() + "  -  Debit: " + decimal.Parse(row["DebitFC"].ToString()).ToString(Format.TotalAmountFormat);
						}
						else if (row["CreditFC"] != DBNull.Value && row["CreditFC"].ToString() != "" && decimal.Parse(row["CreditFC"].ToString()) != 0m)
						{
							row["CurrencyNote"] = row["CurrencyNote"].ToString() + "  -  Credit: " + decimal.Parse(row["CreditFC"].ToString()).ToString(Format.TotalAmountFormat);
						}
					}
				}
			}
			if (data.Tables.Count > 1)
			{
				data.Tables[1].Columns.Add("SysDocTypeName");
				foreach (DataRow row2 in data.Tables[1].Rows)
				{
					if (row2["SysDocType"] != DBNull.Value)
					{
						int sysDocType = int.Parse(row2["SysDocType"].ToString());
						row2["SysDocTypeName"] = PublicFunctions.GetSysDocTypeString(sysDocType);
					}
				}
			}
			data.Tables[1].Columns.Add("Balance", typeof(decimal));
			foreach (DataRow row3 in data.Tables[0].Rows)
			{
				string str = row3["Account Code"].ToString();
				DataRow[] array = data.Tables[1].Select("[Account Code] ='" + str + "'");
				decimal result = default(decimal);
				decimal.TryParse(row3["Opening Balance"].ToString(), out result);
				for (int i = 0; i < array.Length; i = checked(i + 1))
				{
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					DataRow dataRow3 = array[i];
					if (dataRow3["Debit"] != DBNull.Value)
					{
						decimal.TryParse(dataRow3["Debit"].ToString(), out result2);
					}
					if (dataRow3["Credit"] != DBNull.Value)
					{
						decimal.TryParse(dataRow3["Credit"].ToString(), out result3);
					}
					result = result + result2 - result3;
					array[i]["Balance"] = result;
				}
			}
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			XtraReport report = reportHelper.GetReport("GL");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "'GL.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
				return;
			}
			report.DataSource = data;
			reportHelper.ShowReport(report);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Accounts.GLReportForm));
			buttonOK = new System.Windows.Forms.Button();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			dateControl1 = new Micromind.DataControls.DateControl();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			locationSelector = new Micromind.DataControls.LocationSelector();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			divisionSelector = new Micromind.DataControls.DivisionSelector();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(262, 8);
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
			panelButtons.Location = new System.Drawing.Point(0, 166);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(472, 40);
			panelButtons.TabIndex = 3;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(472, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(362, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 11, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(11, 12);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(375, 49);
			dateControl1.TabIndex = 0;
			dateControl1.Tag = "Date";
			dateControl1.ToDate = new System.DateTime(2017, 11, 16, 23, 59, 59, 59);
			ultraGroupBox1.Controls.Add(locationSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 67);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(451, 81);
			ultraGroupBox1.TabIndex = 15;
			ultraGroupBox1.Text = "Location";
			ultraGroupBox1.Visible = false;
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(16, 19);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(412, 59);
			locationSelector.TabIndex = 7;
			ultraGroupBox2.Controls.Add(divisionSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(13, 61);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(444, 81);
			ultraGroupBox2.TabIndex = 18;
			ultraGroupBox2.Text = "Division";
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
			base.ClientSize = new System.Drawing.Size(472, 206);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(dateControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "GLReportForm";
			Text = "General Ledger Report";
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
