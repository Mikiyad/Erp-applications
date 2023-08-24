using DevExpress.XtraReports.UI;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.Items
{
	public class ProductPriceListReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private ProductSelector productSelector;

		private UltraGroupBox ultraGroupBox1;

		private CheckBox checkBoxShowInactive;

		private Button buttonClose;

		private PriceLevelComboBox comboBoxPriceLevel;

		private RadioButton radioButtonAllPrice;

		private RadioButton radioButtonSinglePrice;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsInventory;

		public int ScreenID => 7025;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public ProductPriceListReport()
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
			DataSet data;
			ReportHelper reportHelper;
			XtraReport report;
			if (radioButtonAllPrice.Checked || comboBoxPriceLevel.SelectedID == "")
			{
				data = Factory.ProductSystem.GetProductPriceListReport(productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, checkBoxShowInactive.Checked, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin);
				reportHelper = new ReportHelper();
				string reportFilter = "";
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				report = reportHelper.GetReport("Item Price List");
			}
			else
			{
				data = Factory.ProductSystem.GetProductSinglePriceListReport(productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, comboBoxPriceLevel.SelectedID, checkBoxShowInactive.Checked, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin);
				reportHelper = new ReportHelper();
				string reportFilter2 = "";
				reportHelper.AddGeneralReportData(ref data, reportFilter2);
				report = reportHelper.GetReport("Item Single Price List");
			}
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "'Item Price List.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
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

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxPriceLevel.Enabled = radioButtonSinglePrice.Checked;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Items.ProductPriceListReport));
			buttonOK = new System.Windows.Forms.Button();
			productSelector = new Micromind.DataControls.ProductSelector();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			checkBoxShowInactive = new System.Windows.Forms.CheckBox();
			buttonClose = new System.Windows.Forms.Button();
			comboBoxPriceLevel = new Micromind.DataControls.PriceLevelComboBox();
			radioButtonAllPrice = new System.Windows.Forms.RadioButton();
			radioButtonSinglePrice = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPriceLevel).BeginInit();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(286, 295);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 5;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			productSelector.BackColor = System.Drawing.Color.Transparent;
			productSelector.CustomReportFieldName = "";
			productSelector.CustomReportKey = "";
			productSelector.CustomReportValueType = 1;
			productSelector.Location = new System.Drawing.Point(6, 19);
			productSelector.Name = "productSelector";
			productSelector.ShowOnlyAssemlbyItems = false;
			productSelector.Size = new System.Drawing.Size(403, 189);
			productSelector.TabIndex = 0;
			ultraGroupBox1.Controls.Add(productSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(476, 210);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Products";
			checkBoxShowInactive.AutoSize = true;
			checkBoxShowInactive.Location = new System.Drawing.Point(24, 256);
			checkBoxShowInactive.Name = "checkBoxShowInactive";
			checkBoxShowInactive.Size = new System.Drawing.Size(137, 17);
			checkBoxShowInactive.TabIndex = 4;
			checkBoxShowInactive.Text = "Show inactive products";
			checkBoxShowInactive.UseVisualStyleBackColor = true;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(390, 295);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 6;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			comboBoxPriceLevel.Assigned = false;
			comboBoxPriceLevel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPriceLevel.CustomReportFieldName = "";
			comboBoxPriceLevel.CustomReportKey = "";
			comboBoxPriceLevel.CustomReportValueType = 1;
			comboBoxPriceLevel.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPriceLevel.DisplayLayout.Appearance = appearance;
			comboBoxPriceLevel.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPriceLevel.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPriceLevel.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPriceLevel.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxPriceLevel.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPriceLevel.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxPriceLevel.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPriceLevel.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPriceLevel.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPriceLevel.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxPriceLevel.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPriceLevel.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPriceLevel.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPriceLevel.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxPriceLevel.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPriceLevel.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPriceLevel.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxPriceLevel.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxPriceLevel.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPriceLevel.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxPriceLevel.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxPriceLevel.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPriceLevel.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxPriceLevel.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPriceLevel.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPriceLevel.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPriceLevel.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPriceLevel.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxPriceLevel.Editable = true;
			comboBoxPriceLevel.Enabled = false;
			comboBoxPriceLevel.FilterString = "";
			comboBoxPriceLevel.HasAllAccount = false;
			comboBoxPriceLevel.HasCustom = false;
			comboBoxPriceLevel.IsDataLoaded = false;
			comboBoxPriceLevel.Location = new System.Drawing.Point(200, 228);
			comboBoxPriceLevel.MaxDropDownItems = 12;
			comboBoxPriceLevel.Name = "comboBoxPriceLevel";
			comboBoxPriceLevel.ShowInactiveItems = false;
			comboBoxPriceLevel.ShowQuickAdd = true;
			comboBoxPriceLevel.Size = new System.Drawing.Size(129, 20);
			comboBoxPriceLevel.TabIndex = 3;
			comboBoxPriceLevel.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			radioButtonAllPrice.AutoSize = true;
			radioButtonAllPrice.Checked = true;
			radioButtonAllPrice.Location = new System.Drawing.Point(24, 229);
			radioButtonAllPrice.Name = "radioButtonAllPrice";
			radioButtonAllPrice.Size = new System.Drawing.Size(68, 17);
			radioButtonAllPrice.TabIndex = 1;
			radioButtonAllPrice.TabStop = true;
			radioButtonAllPrice.Text = "All Prices";
			radioButtonAllPrice.UseVisualStyleBackColor = true;
			radioButtonSinglePrice.AutoSize = true;
			radioButtonSinglePrice.Location = new System.Drawing.Point(115, 229);
			radioButtonSinglePrice.Name = "radioButtonSinglePrice";
			radioButtonSinglePrice.Size = new System.Drawing.Size(84, 17);
			radioButtonSinglePrice.TabIndex = 2;
			radioButtonSinglePrice.Text = "Single Price:";
			radioButtonSinglePrice.UseVisualStyleBackColor = true;
			radioButtonSinglePrice.CheckedChanged += new System.EventHandler(radioButton2_CheckedChanged);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(497, 327);
			base.Controls.Add(radioButtonSinglePrice);
			base.Controls.Add(radioButtonAllPrice);
			base.Controls.Add(comboBoxPriceLevel);
			base.Controls.Add(buttonClose);
			base.Controls.Add(checkBoxShowInactive);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "ProductPriceListReport";
			Text = "Item Price List Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxPriceLevel).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
