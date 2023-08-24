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
	public class AccountLedgerReportForm : Form, IForm
	{
		private bool allowposting = CompanyPreferences.FinancialTransactionPosting;

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

		private AccountGroupComboBox comboBoxAccountGroup;

		private Label label2;

		private Label label3;

		private CostCenterComboBox comboBoxCostCenter;

		private UltraGroupBox ultraGroupBox2;

		private LocationSelector locationSelector;

		private RadioButton radioButtonSummary;

		private UltraGroupBox ultraGroupBox1;

		private DivisionSelector divisionSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsAccounts;

		public int ScreenID => 7002;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public AccountLedgerReportForm()
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

		public IEnumerable<Control> GetAllFormControls(Control control)
		{
			IEnumerable<Control> controls = control.Controls.Cast<Control>();
			return controls.SelectMany((Control ctrl) => GetAllFormControls(ctrl).Concat(controls));
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			DataSet data = new DataSet();
			if (radioButtonBase.Checked || radioButtonAccountCurrency.Checked)
			{
				data = Factory.JournalSystem.GetAccountLedgerReport(dateControl1.FromDate, dateControl1.ToDate, accountSelector.FromAccount, accountSelector.ToAccount, comboBoxAccountGroup.SelectedID, locationSelector.FromLocation, locationSelector.ToLocation, divisionSelector.FromDivision, divisionSelector.ToDivision, radioButtonAccountCurrency.Checked, comboBoxCostCenter.SelectedID, allowposting);
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
				}
			}
			else if (radioButtonSummary.Checked)
			{
				data = Factory.JournalSystem.GetAccountLedgerReportSummary(dateControl1.FromDate, dateControl1.ToDate, accountSelector.FromAccount, accountSelector.ToAccount, comboBoxAccountGroup.SelectedID, locationSelector.FromLocation, locationSelector.ToLocation, divisionSelector.FromDivision, divisionSelector.ToDivision, radioButtonAccountCurrency.Checked, comboBoxCostCenter.SelectedID, allowposting);
			}
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			XtraReport xtraReport = null;
			xtraReport = ((!radioButtonSummary.Checked) ? reportHelper.GetReport("Account Ledger") : reportHelper.GetReport("Account Ledger Summary"));
			if (xtraReport == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Account Ledger.repx", "Please make sure you have access to reports path and the files are not corrupted.");
				return;
			}
			xtraReport.DataSource = data;
			reportHelper.ShowReport(xtraReport);
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
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Accounts.AccountLedgerReportForm));
			buttonOK = new System.Windows.Forms.Button();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			groupBox1 = new System.Windows.Forms.GroupBox();
			accountSelector = new Micromind.DataControls.AccountSelector();
			radioButtonBase = new System.Windows.Forms.RadioButton();
			radioButtonAccountCurrency = new System.Windows.Forms.RadioButton();
			label1 = new System.Windows.Forms.Label();
			dateControl1 = new Micromind.DataControls.DateControl();
			comboBoxAccountGroup = new Micromind.DataControls.AccountGroupComboBox();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			comboBoxCostCenter = new Micromind.DataControls.CostCenterComboBox();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			locationSelector = new Micromind.DataControls.LocationSelector();
			radioButtonSummary = new System.Windows.Forms.RadioButton();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			divisionSelector = new Micromind.DataControls.DivisionSelector();
			panelButtons.SuspendLayout();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAccountGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCenter).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
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
			panelButtons.Location = new System.Drawing.Point(0, 370);
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
			groupBox1.Controls.Add(accountSelector);
			groupBox1.Location = new System.Drawing.Point(12, 52);
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
			radioButtonBase.Location = new System.Drawing.Point(23, 344);
			radioButtonBase.Name = "radioButtonBase";
			radioButtonBase.Size = new System.Drawing.Size(94, 17);
			radioButtonBase.TabIndex = 3;
			radioButtonBase.TabStop = true;
			radioButtonBase.Text = "Base Currency";
			radioButtonBase.UseVisualStyleBackColor = true;
			radioButtonAccountCurrency.AutoSize = true;
			radioButtonAccountCurrency.Location = new System.Drawing.Point(123, 344);
			radioButtonAccountCurrency.Name = "radioButtonAccountCurrency";
			radioButtonAccountCurrency.Size = new System.Drawing.Size(105, 17);
			radioButtonAccountCurrency.TabIndex = 3;
			radioButtonAccountCurrency.Text = "Foreign Currency";
			radioButtonAccountCurrency.UseVisualStyleBackColor = true;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 324);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(87, 13);
			label1.TabIndex = 4;
			label1.Text = "Report Currency:";
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 10, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(12, 260);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(377, 58);
			dateControl1.TabIndex = 1;
			dateControl1.ToDate = new System.DateTime(2017, 10, 19, 23, 59, 59, 59);
			comboBoxAccountGroup.Assigned = false;
			comboBoxAccountGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxAccountGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAccountGroup.CustomReportFieldName = "";
			comboBoxAccountGroup.CustomReportKey = "";
			comboBoxAccountGroup.CustomReportValueType = 1;
			comboBoxAccountGroup.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAccountGroup.DisplayLayout.Appearance = appearance;
			comboBoxAccountGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAccountGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccountGroup.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccountGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxAccountGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccountGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxAccountGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAccountGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAccountGroup.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAccountGroup.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxAccountGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAccountGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAccountGroup.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAccountGroup.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxAccountGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAccountGroup.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccountGroup.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxAccountGroup.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxAccountGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAccountGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxAccountGroup.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxAccountGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAccountGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxAccountGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAccountGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAccountGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAccountGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAccountGroup.Editable = true;
			comboBoxAccountGroup.FilterString = "";
			comboBoxAccountGroup.HasAllAccount = false;
			comboBoxAccountGroup.HasCustom = false;
			comboBoxAccountGroup.IsDataLoaded = false;
			comboBoxAccountGroup.Location = new System.Drawing.Point(109, 22);
			comboBoxAccountGroup.MaxDropDownItems = 12;
			comboBoxAccountGroup.Name = "comboBoxAccountGroup";
			comboBoxAccountGroup.ShowInactiveItems = false;
			comboBoxAccountGroup.ShowQuickAdd = true;
			comboBoxAccountGroup.Size = new System.Drawing.Size(175, 20);
			comboBoxAccountGroup.TabIndex = 5;
			comboBoxAccountGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(18, 26);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(82, 13);
			label2.TabIndex = 6;
			label2.Text = "Account Group:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(12, 225);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(65, 13);
			label3.TabIndex = 10;
			label3.Text = "Cost Center:";
			comboBoxCostCenter.Assigned = false;
			comboBoxCostCenter.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCostCenter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCostCenter.CustomReportFieldName = "";
			comboBoxCostCenter.CustomReportKey = "";
			comboBoxCostCenter.CustomReportValueType = 1;
			comboBoxCostCenter.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCostCenter.DisplayLayout.Appearance = appearance13;
			comboBoxCostCenter.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCostCenter.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCostCenter.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCostCenter.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxCostCenter.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCostCenter.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxCostCenter.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCostCenter.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCostCenter.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCostCenter.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxCostCenter.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCostCenter.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCostCenter.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCostCenter.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxCostCenter.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCostCenter.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCostCenter.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxCostCenter.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxCostCenter.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCostCenter.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxCostCenter.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxCostCenter.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCostCenter.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxCostCenter.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCostCenter.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCostCenter.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCostCenter.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCostCenter.Editable = true;
			comboBoxCostCenter.FilterString = "";
			comboBoxCostCenter.HasAllAccount = false;
			comboBoxCostCenter.HasCustom = false;
			comboBoxCostCenter.IsDataLoaded = false;
			comboBoxCostCenter.Location = new System.Drawing.Point(109, 221);
			comboBoxCostCenter.MaxDropDownItems = 12;
			comboBoxCostCenter.Name = "comboBoxCostCenter";
			comboBoxCostCenter.ShowInactiveItems = false;
			comboBoxCostCenter.ShowQuickAdd = true;
			comboBoxCostCenter.Size = new System.Drawing.Size(175, 20);
			comboBoxCostCenter.TabIndex = 9;
			comboBoxCostCenter.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraGroupBox2.Controls.Add(locationSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(12, 127);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(453, 81);
			ultraGroupBox2.TabIndex = 13;
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
			radioButtonSummary.AutoSize = true;
			radioButtonSummary.Location = new System.Drawing.Point(234, 344);
			radioButtonSummary.Name = "radioButtonSummary";
			radioButtonSummary.Size = new System.Drawing.Size(68, 17);
			radioButtonSummary.TabIndex = 14;
			radioButtonSummary.Text = "Summary";
			radioButtonSummary.UseVisualStyleBackColor = true;
			ultraGroupBox1.Controls.Add(divisionSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(10, 127);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(455, 81);
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
			base.ClientSize = new System.Drawing.Size(477, 410);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(radioButtonSummary);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(label3);
			base.Controls.Add(comboBoxCostCenter);
			base.Controls.Add(label2);
			base.Controls.Add(comboBoxAccountGroup);
			base.Controls.Add(label1);
			base.Controls.Add(radioButtonAccountCurrency);
			base.Controls.Add(radioButtonBase);
			base.Controls.Add(groupBox1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(dateControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "AccountLedgerReportForm";
			Text = "Account Ledger";
			panelButtons.ResumeLayout(false);
			groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxAccountGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCenter).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
