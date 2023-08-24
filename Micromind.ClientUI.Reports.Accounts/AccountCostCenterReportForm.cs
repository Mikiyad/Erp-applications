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
	public class AccountCostCenterReportForm : Form, IForm
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

		private Label label3;

		private CostCenterComboBox comboBoxCostCenter;

		private UltraGroupBox ultraGroupBox2;

		private UltraGroupBox ultraGroupBox1;

		private UltraGroupBox ultraGroupBox3;

		private DivisionSelector divisionSelector;

		private LocationSelector locationSelector;

		private LocationSelector locationSelector1;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsAccounts;

		public int ScreenID => 7002;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public AccountCostCenterReportForm()
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
			DataSet data = Factory.JournalSystem.GetAccountCostCenterReport(dateControl1.FromDate, dateControl1.ToDate, accountSelector.FromAccount, accountSelector.ToAccount, locationSelector.FromLocation, locationSelector.ToLocation, radioButtonAccountCurrency.Checked, comboBoxCostCenter.SelectedID, divisionSelector.FromDivision, divisionSelector.ToDivision);
			if (data.Tables.Count > 1)
			{
				string baseCurrencyID = Global.BaseCurrencyID;
				data.Tables[1].Columns.Add("CurrencyNote");
				foreach (DataRow row in data.Tables[1].Rows)
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
				data.Tables[1].Columns.Add("SysDocTypeName");
				foreach (DataRow row2 in data.Tables[1].Rows)
				{
					if (row2["SysDocType"] != DBNull.Value)
					{
						int sysDocType = int.Parse(row2["SysDocType"].ToString());
						row2["SysDocTypeName"] = PublicFunctions.GetSysDocTypeString(sysDocType);
					}
				}
				data.Tables[1].Columns.Add("Balance", typeof(decimal));
				foreach (DataRow row3 in data.Tables[1].Rows)
				{
					string str = row3["Account Code"].ToString();
					DataRow[] array = data.Tables[1].Select("[Account Code] ='" + str + "'");
					decimal result = default(decimal);
					decimal.TryParse(row3["Balance"].ToString(), out result);
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
			}
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			XtraReport report = reportHelper.GetReport("Account Cost Center");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "'Account Cost Center.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Accounts.AccountCostCenterReportForm));
			buttonOK = new System.Windows.Forms.Button();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			groupBox1 = new System.Windows.Forms.GroupBox();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			accountSelector = new Micromind.DataControls.AccountSelector();
			radioButtonBase = new System.Windows.Forms.RadioButton();
			radioButtonAccountCurrency = new System.Windows.Forms.RadioButton();
			label1 = new System.Windows.Forms.Label();
			dateControl1 = new Micromind.DataControls.DateControl();
			label3 = new System.Windows.Forms.Label();
			comboBoxCostCenter = new Micromind.DataControls.CostCenterComboBox();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
			divisionSelector = new Micromind.DataControls.DivisionSelector();
			locationSelector = new Micromind.DataControls.LocationSelector();
			locationSelector1 = new Micromind.DataControls.LocationSelector();
			panelButtons.SuspendLayout();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCenter).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).BeginInit();
			ultraGroupBox3.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(267, 8);
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
			panelButtons.Location = new System.Drawing.Point(0, 348);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(477, 40);
			panelButtons.TabIndex = 2;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(477, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(367, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			groupBox1.Controls.Add(ultraGroupBox2);
			groupBox1.Controls.Add(accountSelector);
			groupBox1.Location = new System.Drawing.Point(12, 52);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(453, 69);
			groupBox1.TabIndex = 0;
			groupBox1.TabStop = false;
			groupBox1.Text = "Accounts";
			ultraGroupBox2.Controls.Add(locationSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(6, 73);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(430, 81);
			ultraGroupBox2.TabIndex = 13;
			ultraGroupBox2.Text = "Location";
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
			radioButtonBase.Location = new System.Drawing.Point(23, 298);
			radioButtonBase.Name = "radioButtonBase";
			radioButtonBase.Size = new System.Drawing.Size(94, 17);
			radioButtonBase.TabIndex = 3;
			radioButtonBase.TabStop = true;
			radioButtonBase.Text = "Base Currency";
			radioButtonBase.UseVisualStyleBackColor = true;
			radioButtonBase.Visible = false;
			radioButtonAccountCurrency.AutoSize = true;
			radioButtonAccountCurrency.Location = new System.Drawing.Point(123, 298);
			radioButtonAccountCurrency.Name = "radioButtonAccountCurrency";
			radioButtonAccountCurrency.Size = new System.Drawing.Size(105, 17);
			radioButtonAccountCurrency.TabIndex = 3;
			radioButtonAccountCurrency.Text = "Foreign Currency";
			radioButtonAccountCurrency.UseVisualStyleBackColor = true;
			radioButtonAccountCurrency.Visible = false;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(20, 278);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(87, 13);
			label1.TabIndex = 4;
			label1.Text = "Report Currency:";
			label1.Visible = false;
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 10, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(25, 213);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(377, 58);
			dateControl1.TabIndex = 1;
			dateControl1.ToDate = new System.DateTime(2017, 10, 29, 23, 59, 59, 59);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(20, 22);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(65, 13);
			label3.TabIndex = 12;
			label3.Text = "Cost Center:";
			comboBoxCostCenter.Assigned = false;
			comboBoxCostCenter.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCostCenter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCostCenter.CustomReportFieldName = "";
			comboBoxCostCenter.CustomReportKey = "";
			comboBoxCostCenter.CustomReportValueType = 1;
			comboBoxCostCenter.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCostCenter.DisplayLayout.Appearance = appearance;
			comboBoxCostCenter.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCostCenter.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCostCenter.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCostCenter.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxCostCenter.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCostCenter.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxCostCenter.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCostCenter.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCostCenter.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCostCenter.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxCostCenter.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCostCenter.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCostCenter.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCostCenter.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxCostCenter.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCostCenter.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCostCenter.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxCostCenter.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxCostCenter.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCostCenter.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxCostCenter.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxCostCenter.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCostCenter.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxCostCenter.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCostCenter.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCostCenter.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCostCenter.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCostCenter.Editable = true;
			comboBoxCostCenter.FilterString = "";
			comboBoxCostCenter.HasAllAccount = false;
			comboBoxCostCenter.HasCustom = false;
			comboBoxCostCenter.IsDataLoaded = false;
			comboBoxCostCenter.Location = new System.Drawing.Point(109, 18);
			comboBoxCostCenter.MaxDropDownItems = 12;
			comboBoxCostCenter.Name = "comboBoxCostCenter";
			comboBoxCostCenter.ShowInactiveItems = false;
			comboBoxCostCenter.ShowQuickAdd = true;
			comboBoxCostCenter.Size = new System.Drawing.Size(175, 20);
			comboBoxCostCenter.TabIndex = 11;
			comboBoxCostCenter.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraGroupBox1.Controls.Add(locationSelector1);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 127);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(451, 81);
			ultraGroupBox1.TabIndex = 13;
			ultraGroupBox1.Text = "Location";
			ultraGroupBox1.Visible = false;
			ultraGroupBox3.Controls.Add(divisionSelector);
			ultraGroupBox3.Location = new System.Drawing.Point(17, 128);
			ultraGroupBox3.Name = "ultraGroupBox3";
			ultraGroupBox3.Size = new System.Drawing.Size(428, 81);
			ultraGroupBox3.TabIndex = 18;
			ultraGroupBox3.Text = "Division";
			divisionSelector.BackColor = System.Drawing.Color.Transparent;
			divisionSelector.CustomReportFieldName = "";
			divisionSelector.CustomReportKey = "";
			divisionSelector.CustomReportValueType = 1;
			divisionSelector.Location = new System.Drawing.Point(6, 19);
			divisionSelector.Name = "divisionSelector";
			divisionSelector.Size = new System.Drawing.Size(414, 54);
			divisionSelector.TabIndex = 0;
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(16, 19);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(412, 59);
			locationSelector.TabIndex = 7;
			locationSelector1.BackColor = System.Drawing.Color.Transparent;
			locationSelector1.CustomReportFieldName = "";
			locationSelector1.CustomReportKey = "";
			locationSelector1.CustomReportValueType = 1;
			locationSelector1.Location = new System.Drawing.Point(16, 19);
			locationSelector1.Name = "locationSelector1";
			locationSelector1.Size = new System.Drawing.Size(412, 59);
			locationSelector1.TabIndex = 7;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(477, 388);
			base.Controls.Add(ultraGroupBox3);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(label3);
			base.Controls.Add(comboBoxCostCenter);
			base.Controls.Add(label1);
			base.Controls.Add(radioButtonAccountCurrency);
			base.Controls.Add(radioButtonBase);
			base.Controls.Add(groupBox1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(dateControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "AccountCostCenterReportForm";
			Text = "Cost Center Report";
			panelButtons.ResumeLayout(false);
			groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxCostCenter).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).EndInit();
			ultraGroupBox3.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
