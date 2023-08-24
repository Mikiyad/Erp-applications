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
	public class AccountTransactionsReportForm : Form, IForm
	{
		private bool allowposting = CompanyPreferences.FinancialTransactionPosting;

		private bool allowJobs = CompanyPreferences.UseJobCosting;

		private bool allowProperty = CompanyPreferences.UseProperty;

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

		private CostCenterComboBox comboBoxCostCenter;

		private Label label3;

		private UltraGroupBox ultraGroupBox2;

		private LocationSelector locationSelector;

		private GroupBox groupBoxJob;

		private JobSelector jobSelector;

		private GroupBox groupBoxCC;

		private CostCategorySelector costCategorySelector;

		private GroupBox groupBox2;

		private AnalysisGroupSelector analysisSelector;

		private GroupBox groupBox3;

		private PropertySelector propertySelector1;

		private GroupBox groupBox4;

		private PropertyUnitSelector propertyUnitSelector1;

		private UltraExpandableGroupBox jobModuleExpandableGroupBox1;

		private UltraExpandableGroupBoxPanel ultraExpandableGroupBoxPanel1;

		private UltraExpandableGroupBox PropertyModuleultraExpandableGroupBox2;

		private UltraExpandableGroupBoxPanel ultraExpandableGroupBoxPanel2;

		private Panel panelOtherFilter;

		private UltraGroupBox ultraGroupBox1;

		private DivisionSelector divisionSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsAccounts;

		public int ScreenID => 7002;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public AccountTransactionsReportForm()
		{
			InitializeComponent();
			base.Load += Form_Load;
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				SetUI();
				jobModuleExpandableGroupBox1.Expanded = false;
				PropertyModuleultraExpandableGroupBox2.Expanded = false;
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
			DataSet data = Factory.JournalSystem.GetAccountTransactionsReport(dateControl1.FromDate, dateControl1.ToDate, accountSelector.FromAccount, accountSelector.ToAccount, locationSelector.FromLocation, locationSelector.ToLocation, comboBoxAccountGroup.SelectedID, radioButtonAccountCurrency.Checked, comboBoxCostCenter.SelectedID, allowposting, jobSelector.FromJob, jobSelector.ToJob, costCategorySelector.FromCostCategory, costCategorySelector.ToCostCategory, propertySelector1.FromProperty, propertySelector1.ToProperty, propertyUnitSelector1.FromUnit, propertyUnitSelector1.ToUnit, analysisSelector.FromAnalysis, analysisSelector.ToAnalysis, divisionSelector.FromDivision, divisionSelector.ToDivision);
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
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			XtraReport report = reportHelper.GetReport("Account Transactions");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Account Transactions.repx", "Please make sure you have access to reports path and the files are not corrupted.");
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

		private void jobModuleExpandableGroupBox1_ExpandedStateChanged(object sender, EventArgs e)
		{
			if (jobModuleExpandableGroupBox1.Expanded)
			{
				PropertyModuleultraExpandableGroupBox2.Location = new Point(9, 461);
				if (PropertyModuleultraExpandableGroupBox2.Expanded)
				{
					base.Height = 884;
				}
				else
				{
					base.Height = 729;
				}
			}
			else
			{
				PropertyModuleultraExpandableGroupBox2.Location = new Point(9, 321);
				if (PropertyModuleultraExpandableGroupBox2.Expanded)
				{
					base.Height = 749;
				}
				else
				{
					base.Height = 578;
				}
			}
		}

		private void SetUI()
		{
			if (allowJobs)
			{
				jobModuleExpandableGroupBox1.Expanded = true;
				jobModuleExpandableGroupBox1.Visible = true;
			}
			else
			{
				jobModuleExpandableGroupBox1.Expanded = false;
				jobModuleExpandableGroupBox1.Visible = false;
			}
			if (allowProperty)
			{
				PropertyModuleultraExpandableGroupBox2.Expanded = true;
				PropertyModuleultraExpandableGroupBox2.Visible = true;
				if (!allowJobs)
				{
					PropertyModuleultraExpandableGroupBox2.Location = new Point(9, 299);
				}
			}
			else
			{
				PropertyModuleultraExpandableGroupBox2.Expanded = false;
				PropertyModuleultraExpandableGroupBox2.Visible = false;
			}
			if (!allowJobs && !allowProperty)
			{
				base.Height = 530;
			}
		}

		private void PropertyModuleultraExpandableGroupBox2_ExpandedStateChanged(object sender, EventArgs e)
		{
			if (PropertyModuleultraExpandableGroupBox2.Expanded)
			{
				if (jobModuleExpandableGroupBox1.Expanded)
				{
					base.Height = 884;
				}
				else
				{
					base.Height = 749;
				}
			}
			else if (jobModuleExpandableGroupBox1.Expanded)
			{
				base.Height = 729;
			}
			else
			{
				base.Height = 579;
			}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Accounts.AccountTransactionsReportForm));
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
			comboBoxCostCenter = new Micromind.DataControls.CostCenterComboBox();
			label3 = new System.Windows.Forms.Label();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			locationSelector = new Micromind.DataControls.LocationSelector();
			groupBoxJob = new System.Windows.Forms.GroupBox();
			jobSelector = new Micromind.DataControls.JobSelector();
			groupBoxCC = new System.Windows.Forms.GroupBox();
			costCategorySelector = new Micromind.DataControls.CostCategorySelector();
			groupBox2 = new System.Windows.Forms.GroupBox();
			analysisSelector = new Micromind.DataControls.AnalysisGroupSelector();
			groupBox3 = new System.Windows.Forms.GroupBox();
			propertySelector1 = new Micromind.DataControls.PropertySelector();
			groupBox4 = new System.Windows.Forms.GroupBox();
			propertyUnitSelector1 = new Micromind.DataControls.PropertyUnitSelector();
			jobModuleExpandableGroupBox1 = new Infragistics.Win.Misc.UltraExpandableGroupBox();
			ultraExpandableGroupBoxPanel1 = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
			PropertyModuleultraExpandableGroupBox2 = new Infragistics.Win.Misc.UltraExpandableGroupBox();
			ultraExpandableGroupBoxPanel2 = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
			panelOtherFilter = new System.Windows.Forms.Panel();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			divisionSelector = new Micromind.DataControls.DivisionSelector();
			panelButtons.SuspendLayout();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAccountGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCenter).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			groupBoxJob.SuspendLayout();
			groupBoxCC.SuspendLayout();
			groupBox2.SuspendLayout();
			groupBox3.SuspendLayout();
			groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)jobModuleExpandableGroupBox1).BeginInit();
			jobModuleExpandableGroupBox1.SuspendLayout();
			ultraExpandableGroupBoxPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)PropertyModuleultraExpandableGroupBox2).BeginInit();
			PropertyModuleultraExpandableGroupBox2.SuspendLayout();
			ultraExpandableGroupBoxPanel2.SuspendLayout();
			panelOtherFilter.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(263, 8);
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
			panelButtons.Location = new System.Drawing.Point(0, 709);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(473, 40);
			panelButtons.TabIndex = 2;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(473, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(363, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			groupBox1.Controls.Add(accountSelector);
			groupBox1.Location = new System.Drawing.Point(12, 45);
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
			radioButtonBase.Location = new System.Drawing.Point(7, 124);
			radioButtonBase.Name = "radioButtonBase";
			radioButtonBase.Size = new System.Drawing.Size(94, 17);
			radioButtonBase.TabIndex = 3;
			radioButtonBase.TabStop = true;
			radioButtonBase.Text = "Base Currency";
			radioButtonBase.UseVisualStyleBackColor = true;
			radioButtonAccountCurrency.AutoSize = true;
			radioButtonAccountCurrency.Location = new System.Drawing.Point(107, 124);
			radioButtonAccountCurrency.Name = "radioButtonAccountCurrency";
			radioButtonAccountCurrency.Size = new System.Drawing.Size(105, 17);
			radioButtonAccountCurrency.TabIndex = 3;
			radioButtonAccountCurrency.Text = "Foreign Currency";
			radioButtonAccountCurrency.UseVisualStyleBackColor = true;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(4, 104);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(87, 13);
			label1.TabIndex = 4;
			label1.Text = "Report Currency:";
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 8, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(3, 48);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(377, 55);
			dateControl1.TabIndex = 1;
			dateControl1.Tag = "Date";
			dateControl1.ToDate = new System.DateTime(2017, 8, 1, 23, 59, 59, 59);
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
			comboBoxAccountGroup.Location = new System.Drawing.Point(108, 19);
			comboBoxAccountGroup.MaxDropDownItems = 12;
			comboBoxAccountGroup.Name = "comboBoxAccountGroup";
			comboBoxAccountGroup.ShowInactiveItems = false;
			comboBoxAccountGroup.ShowQuickAdd = true;
			comboBoxAccountGroup.Size = new System.Drawing.Size(175, 20);
			comboBoxAccountGroup.TabIndex = 5;
			comboBoxAccountGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(17, 22);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(82, 13);
			label2.TabIndex = 6;
			label2.Text = "Account Group:";
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
			comboBoxCostCenter.Location = new System.Drawing.Point(94, 15);
			comboBoxCostCenter.MaxDropDownItems = 12;
			comboBoxCostCenter.Name = "comboBoxCostCenter";
			comboBoxCostCenter.ShowInactiveItems = false;
			comboBoxCostCenter.ShowQuickAdd = true;
			comboBoxCostCenter.Size = new System.Drawing.Size(175, 20);
			comboBoxCostCenter.TabIndex = 7;
			comboBoxCostCenter.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(4, 18);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(65, 13);
			label3.TabIndex = 8;
			label3.Text = "Cost Center:";
			ultraGroupBox2.Controls.Add(locationSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(10, 113);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(455, 81);
			ultraGroupBox2.TabIndex = 11;
			ultraGroupBox2.Text = "Location";
			ultraGroupBox2.Visible = false;
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(6, 18);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(412, 59);
			locationSelector.TabIndex = 7;
			groupBoxJob.Controls.Add(jobSelector);
			groupBoxJob.Location = new System.Drawing.Point(3, -2);
			groupBoxJob.Name = "groupBoxJob";
			groupBoxJob.Size = new System.Drawing.Size(443, 69);
			groupBoxJob.TabIndex = 12;
			groupBoxJob.TabStop = false;
			groupBoxJob.Text = "Jobs";
			jobSelector.BackColor = System.Drawing.Color.Transparent;
			jobSelector.CustomReportFieldName = "";
			jobSelector.CustomReportKey = "";
			jobSelector.CustomReportValueType = 1;
			jobSelector.Location = new System.Drawing.Point(6, 17);
			jobSelector.Name = "jobSelector";
			jobSelector.Size = new System.Drawing.Size(429, 46);
			jobSelector.TabIndex = 0;
			groupBoxCC.Controls.Add(costCategorySelector);
			groupBoxCC.Location = new System.Drawing.Point(3, 67);
			groupBoxCC.Name = "groupBoxCC";
			groupBoxCC.Size = new System.Drawing.Size(443, 69);
			groupBoxCC.TabIndex = 13;
			groupBoxCC.TabStop = false;
			groupBoxCC.Text = "Cost Categories";
			costCategorySelector.BackColor = System.Drawing.Color.Transparent;
			costCategorySelector.CustomReportFieldName = "";
			costCategorySelector.CustomReportKey = "";
			costCategorySelector.CustomReportValueType = 1;
			costCategorySelector.Location = new System.Drawing.Point(8, 19);
			costCategorySelector.Name = "costCategorySelector";
			costCategorySelector.Size = new System.Drawing.Size(432, 46);
			costCategorySelector.TabIndex = 0;
			groupBox2.Controls.Add(analysisSelector);
			groupBox2.Location = new System.Drawing.Point(10, 196);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(455, 99);
			groupBox2.TabIndex = 14;
			groupBox2.TabStop = false;
			groupBox2.Text = "Analysis";
			analysisSelector.BackColor = System.Drawing.Color.Transparent;
			analysisSelector.Location = new System.Drawing.Point(9, 14);
			analysisSelector.Name = "analysisSelector";
			analysisSelector.Size = new System.Drawing.Size(414, 77);
			analysisSelector.TabIndex = 0;
			groupBox3.Controls.Add(propertySelector1);
			groupBox3.Location = new System.Drawing.Point(2, 3);
			groupBox3.Name = "groupBox3";
			groupBox3.Size = new System.Drawing.Size(442, 90);
			groupBox3.TabIndex = 18;
			groupBox3.TabStop = false;
			groupBox3.Text = "Property";
			propertySelector1.BackColor = System.Drawing.Color.Transparent;
			propertySelector1.CustomReportFieldName = "";
			propertySelector1.CustomReportKey = "";
			propertySelector1.CustomReportValueType = 1;
			propertySelector1.Location = new System.Drawing.Point(3, 15);
			propertySelector1.Name = "propertySelector1";
			propertySelector1.Size = new System.Drawing.Size(428, 73);
			propertySelector1.TabIndex = 14;
			groupBox4.Controls.Add(propertyUnitSelector1);
			groupBox4.Location = new System.Drawing.Point(2, 94);
			groupBox4.Name = "groupBox4";
			groupBox4.Size = new System.Drawing.Size(442, 64);
			groupBox4.TabIndex = 19;
			groupBox4.TabStop = false;
			groupBox4.Text = "Property Unit";
			propertyUnitSelector1.BackColor = System.Drawing.Color.Transparent;
			propertyUnitSelector1.CustomReportFieldName = "";
			propertyUnitSelector1.CustomReportKey = "";
			propertyUnitSelector1.CustomReportValueType = 1;
			propertyUnitSelector1.Location = new System.Drawing.Point(6, 10);
			propertyUnitSelector1.Name = "propertyUnitSelector1";
			propertyUnitSelector1.Size = new System.Drawing.Size(428, 57);
			propertyUnitSelector1.TabIndex = 15;
			jobModuleExpandableGroupBox1.Controls.Add(ultraExpandableGroupBoxPanel1);
			jobModuleExpandableGroupBox1.Expanded = false;
			jobModuleExpandableGroupBox1.ExpandedSize = new System.Drawing.Size(455, 163);
			jobModuleExpandableGroupBox1.Location = new System.Drawing.Point(9, 299);
			jobModuleExpandableGroupBox1.Name = "jobModuleExpandableGroupBox1";
			jobModuleExpandableGroupBox1.Size = new System.Drawing.Size(455, 21);
			jobModuleExpandableGroupBox1.TabIndex = 20;
			jobModuleExpandableGroupBox1.Text = "Job";
			jobModuleExpandableGroupBox1.ExpandedStateChanged += new System.EventHandler(jobModuleExpandableGroupBox1_ExpandedStateChanged);
			ultraExpandableGroupBoxPanel1.Controls.Add(groupBoxCC);
			ultraExpandableGroupBoxPanel1.Controls.Add(groupBoxJob);
			ultraExpandableGroupBoxPanel1.Location = new System.Drawing.Point(-10000, -10000);
			ultraExpandableGroupBoxPanel1.Name = "ultraExpandableGroupBoxPanel1";
			ultraExpandableGroupBoxPanel1.Size = new System.Drawing.Size(449, 141);
			ultraExpandableGroupBoxPanel1.TabIndex = 0;
			ultraExpandableGroupBoxPanel1.Visible = false;
			PropertyModuleultraExpandableGroupBox2.Controls.Add(ultraExpandableGroupBoxPanel2);
			PropertyModuleultraExpandableGroupBox2.ExpandedSize = new System.Drawing.Size(454, 186);
			PropertyModuleultraExpandableGroupBox2.Location = new System.Drawing.Point(11, 462);
			PropertyModuleultraExpandableGroupBox2.Name = "PropertyModuleultraExpandableGroupBox2";
			PropertyModuleultraExpandableGroupBox2.Size = new System.Drawing.Size(454, 186);
			PropertyModuleultraExpandableGroupBox2.TabIndex = 21;
			PropertyModuleultraExpandableGroupBox2.Text = "Property";
			PropertyModuleultraExpandableGroupBox2.ExpandedStateChanged += new System.EventHandler(PropertyModuleultraExpandableGroupBox2_ExpandedStateChanged);
			ultraExpandableGroupBoxPanel2.Controls.Add(groupBox3);
			ultraExpandableGroupBoxPanel2.Controls.Add(groupBox4);
			ultraExpandableGroupBoxPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraExpandableGroupBoxPanel2.Location = new System.Drawing.Point(3, 19);
			ultraExpandableGroupBoxPanel2.Name = "ultraExpandableGroupBoxPanel2";
			ultraExpandableGroupBoxPanel2.Size = new System.Drawing.Size(448, 164);
			ultraExpandableGroupBoxPanel2.TabIndex = 0;
			panelOtherFilter.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			panelOtherFilter.Controls.Add(dateControl1);
			panelOtherFilter.Controls.Add(label3);
			panelOtherFilter.Controls.Add(comboBoxCostCenter);
			panelOtherFilter.Controls.Add(label1);
			panelOtherFilter.Controls.Add(radioButtonBase);
			panelOtherFilter.Controls.Add(radioButtonAccountCurrency);
			panelOtherFilter.Location = new System.Drawing.Point(7, 552);
			panelOtherFilter.Name = "panelOtherFilter";
			panelOtherFilter.Size = new System.Drawing.Size(458, 151);
			panelOtherFilter.TabIndex = 22;
			ultraGroupBox1.Controls.Add(divisionSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(9, 113);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(454, 81);
			ultraGroupBox1.TabIndex = 19;
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
			base.ClientSize = new System.Drawing.Size(473, 749);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(panelOtherFilter);
			base.Controls.Add(PropertyModuleultraExpandableGroupBox2);
			base.Controls.Add(jobModuleExpandableGroupBox1);
			base.Controls.Add(groupBox2);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(label2);
			base.Controls.Add(comboBoxAccountGroup);
			base.Controls.Add(groupBox1);
			base.Controls.Add(panelButtons);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "AccountTransactionsReportForm";
			Text = "Account Transactions";
			panelButtons.ResumeLayout(false);
			groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxAccountGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCenter).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			groupBoxJob.ResumeLayout(false);
			groupBoxCC.ResumeLayout(false);
			groupBox2.ResumeLayout(false);
			groupBox3.ResumeLayout(false);
			groupBox4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)jobModuleExpandableGroupBox1).EndInit();
			jobModuleExpandableGroupBox1.ResumeLayout(false);
			ultraExpandableGroupBoxPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)PropertyModuleultraExpandableGroupBox2).EndInit();
			PropertyModuleultraExpandableGroupBox2.ResumeLayout(false);
			ultraExpandableGroupBoxPanel2.ResumeLayout(false);
			panelOtherFilter.ResumeLayout(false);
			panelOtherFilter.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
