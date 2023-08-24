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
	public class PropertyAccountTransactionsReportForm : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private DateControl dateControl1;

		private Button buttonOK;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private AccountSelector accountSelector;

		private GroupBox groupBox1;

		private RadioButton radioButtonBase;

		private RadioButton radioButtonAccountCurrency;

		private Label label1;

		private JobSelector jobCostSelector;

		private UltraGroupBox ultraGroupBox2;

		private LocationSelector locationSelector;

		private GroupBox groupBox2;

		private GroupBox groupBox3;

		private PropertySelector propertySelector1;

		private PropertyUnitSelector propertyUnitSelector1;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsAccounts;

		public int ScreenID => 7002;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public PropertyAccountTransactionsReportForm()
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
				DataSet dataSet = null;
				dataSet = Factory.JournalSystem.GetPropertyAccountTransactionsReport(dateControl1.FromDate, dateControl1.ToDate, accountSelector.FromAccount, accountSelector.ToAccount, locationSelector.FromLocation, locationSelector.ToLocation, propertySelector1.FromProperty, propertySelector1.ToProperty, propertySelector1.FromClass, propertySelector1.ToClass, propertyUnitSelector1.FromUnit, propertyUnitSelector1.ToUnit, radioButtonAccountCurrency.Checked);
				if (dataSet.Tables.Count > 0)
				{
					string baseCurrencyID = Global.BaseCurrencyID;
					dataSet.Tables[0].Columns.Add("CurrencyNote");
					foreach (DataRow row in dataSet.Tables[0].Rows)
					{
						if (radioButtonBase.Checked && row["CurrencyID"] != DBNull.Value && row["CurrencyID"].ToString() != baseCurrencyID)
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
					dataSet.Tables[0].Columns.Add("SysDocTypeName");
					foreach (DataRow row2 in dataSet.Tables[0].Rows)
					{
						if (row2["SysDocType"] != DBNull.Value)
						{
							int sysDocType = int.Parse(row2["SysDocType"].ToString());
							row2["SysDocTypeName"] = PublicFunctions.GetSysDocTypeString(sysDocType);
						}
					}
					dataSet.Tables[0].Columns.Add("Balance", typeof(decimal));
				}
				ReportHelper reportHelper = new ReportHelper();
				string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
				reportHelper.AddGeneralReportData(ref dataSet, reportFilter);
				reportHelper.AddFilterData(ref dataSet, GetAllFormControls(this));
				
				XtraReport report = reportHelper.GetReport("Account Transaction Property");
				if (report == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "'Account Transaction Property.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
				}
				else
				{
					report.DataSource = dataSet;
					reportHelper.ShowReport(report);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
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

		private List<Control> GetAllControls(Control container, List<Control> list)
		{
			foreach (Control control in container.Controls)
			{
				if (control is TextBox)
				{
					list.Add(control);
				}
				if (control.Controls.Count > 0)
				{
					list = GetAllControls(control, list);
				}
			}
			return list;
		}

		private List<Control> GetAllControls(Control container)
		{
			return GetAllControls(container, new List<Control>());
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Accounts.PropertyAccountTransactionsReportForm));
			buttonOK = new System.Windows.Forms.Button();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			groupBox1 = new System.Windows.Forms.GroupBox();
			accountSelector = new Micromind.DataControls.AccountSelector();
			radioButtonBase = new System.Windows.Forms.RadioButton();
			radioButtonAccountCurrency = new System.Windows.Forms.RadioButton();
			label1 = new System.Windows.Forms.Label();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			locationSelector = new Micromind.DataControls.LocationSelector();
			dateControl1 = new Micromind.DataControls.DateControl();
			groupBox2 = new System.Windows.Forms.GroupBox();
			groupBox3 = new System.Windows.Forms.GroupBox();
			propertySelector1 = new Micromind.DataControls.PropertySelector();
			propertyUnitSelector1 = new Micromind.DataControls.PropertyUnitSelector();
			panelButtons.SuspendLayout();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			groupBox2.SuspendLayout();
			groupBox3.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(269, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 0;
			buttonOK.Text = "Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 498);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(479, 40);
			panelButtons.TabIndex = 2;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(479, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(369, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			groupBox1.Controls.Add(accountSelector);
			groupBox1.Location = new System.Drawing.Point(12, 12);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(453, 69);
			groupBox1.TabIndex = 0;
			groupBox1.TabStop = false;
			groupBox1.Text = "Accounts";
			accountSelector.BackColor = System.Drawing.Color.Transparent;
			accountSelector.CustomReportFieldName = "";
			accountSelector.CustomReportKey = "";
			accountSelector.CustomReportValueType = 1;
			accountSelector.Location = new System.Drawing.Point(6, 19);
			accountSelector.Name = "accountSelector";
			accountSelector.Size = new System.Drawing.Size(432, 48);
			accountSelector.TabIndex = 4;
			radioButtonBase.AutoSize = true;
			radioButtonBase.Checked = true;
			radioButtonBase.Location = new System.Drawing.Point(18, 466);
			radioButtonBase.Name = "radioButtonBase";
			radioButtonBase.Size = new System.Drawing.Size(94, 17);
			radioButtonBase.TabIndex = 3;
			radioButtonBase.TabStop = true;
			radioButtonBase.Text = "Base Currency";
			radioButtonBase.UseVisualStyleBackColor = true;
			radioButtonAccountCurrency.AutoSize = true;
			radioButtonAccountCurrency.Location = new System.Drawing.Point(118, 466);
			radioButtonAccountCurrency.Name = "radioButtonAccountCurrency";
			radioButtonAccountCurrency.Size = new System.Drawing.Size(105, 17);
			radioButtonAccountCurrency.TabIndex = 3;
			radioButtonAccountCurrency.Text = "Foreign Currency";
			radioButtonAccountCurrency.UseVisualStyleBackColor = true;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(15, 439);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(87, 13);
			label1.TabIndex = 4;
			label1.Text = "Report Currency:";
			ultraGroupBox2.Controls.Add(locationSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(12, 262);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(453, 81);
			ultraGroupBox2.TabIndex = 13;
			ultraGroupBox2.Text = "Location";
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(6, 18);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(412, 59);
			locationSelector.TabIndex = 7;
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 10, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(12, 352);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(447, 64);
			dateControl1.TabIndex = 1;
			dateControl1.ToDate = new System.DateTime(2017, 10, 29, 23, 59, 59, 59);
			groupBox2.Controls.Add(propertySelector1);
			groupBox2.Location = new System.Drawing.Point(12, 83);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(453, 90);
			groupBox2.TabIndex = 17;
			groupBox2.TabStop = false;
			groupBox2.Text = "Property";
			groupBox3.Controls.Add(propertyUnitSelector1);
			groupBox3.Location = new System.Drawing.Point(12, 179);
			groupBox3.Name = "groupBox3";
			groupBox3.Size = new System.Drawing.Size(453, 75);
			groupBox3.TabIndex = 18;
			groupBox3.TabStop = false;
			groupBox3.Text = "Property Unit";
			propertySelector1.BackColor = System.Drawing.Color.Transparent;
			propertySelector1.CustomReportFieldName = "";
			propertySelector1.CustomReportKey = "";
			propertySelector1.CustomReportValueType = 1;
			propertySelector1.Location = new System.Drawing.Point(10, 11);
			propertySelector1.Name = "propertySelector1";
			propertySelector1.Size = new System.Drawing.Size(428, 73);
			propertySelector1.TabIndex = 15;
			propertyUnitSelector1.BackColor = System.Drawing.Color.Transparent;
			propertyUnitSelector1.CustomReportFieldName = "";
			propertyUnitSelector1.CustomReportKey = "";
			propertyUnitSelector1.CustomReportValueType = 1;
			propertyUnitSelector1.Location = new System.Drawing.Point(11, 14);
			propertyUnitSelector1.Name = "propertyUnitSelector1";
			propertyUnitSelector1.Size = new System.Drawing.Size(428, 57);
			propertyUnitSelector1.TabIndex = 16;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(479, 538);
			base.Controls.Add(groupBox3);
			base.Controls.Add(groupBox2);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(label1);
			base.Controls.Add(radioButtonAccountCurrency);
			base.Controls.Add(radioButtonBase);
			base.Controls.Add(groupBox1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(dateControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "PropertyAccountTransactionsReportForm";
			Text = "Property Account Transactions";
			panelButtons.ResumeLayout(false);
			groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			groupBox2.ResumeLayout(false);
			groupBox3.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
