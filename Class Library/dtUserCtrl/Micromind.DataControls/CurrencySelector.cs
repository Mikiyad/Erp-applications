using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class CurrencySelector : UserControl
	{
		private decimal rate = 1m;

		private IContainer components;

		private CurrencyComboBox currencyComboBox1;

		private Label label1;

		public string SelectedID
		{
			get
			{
				if (!base.Visible)
				{
					return Global.BaseCurrencyID;
				}
				return currencyComboBox1.SelectedID;
			}
			set
			{
				if (!base.DesignMode)
				{
					if (!currencyComboBox1.isDataLoaded)
					{
						currencyComboBox1.LoadData();
					}
					if (value != "")
					{
						currencyComboBox1.SelectedID = value;
					}
					else
					{
						currencyComboBox1.SelectedID = Global.BaseCurrencyID;
					}
				}
			}
		}

		public bool IsBaseCurrency
		{
			get
			{
				if (!base.Visible || SelectedID == "")
				{
					return true;
				}
				return currencyComboBox1.SelectedID == Global.BaseCurrencyID;
			}
		}

		public string RateType => currencyComboBox1.SelectedRateType;

		public decimal Rate
		{
			get
			{
				if (base.Visible)
				{
					return rate;
				}
				return 1m;
			}
			set
			{
				rate = value;
				((EditorButton)currencyComboBox1.ButtonsRight[0]).Text = "R:" + value.ToString(Format.UnitPriceFormat);
			}
		}

		public event EventHandler CurrencyRateChanged;

		public event EventHandler SelectedIndexChanged;

		public CurrencySelector()
		{
			InitializeComponent();
			currencyComboBox1.SelectedIndexChanged += currencyComboBox1_SelectedIndexChanged;
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeCurrency))
			{
				currencyComboBox1.ReadOnly = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeCurrencyRate))
			{
				currencyComboBox1.ButtonsRight[0].Enabled = false;
			}
		}

		private void currencyComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				Rate = Factory.CurrencySystem.GetCurrencyRate(SelectedID);
				if (this.SelectedIndexChanged != null)
				{
					this.SelectedIndexChanged(this, e);
				}
			}
			catch
			{
				rate = 1m;
			}
		}

		private void CurrencySelector_Load(object sender, EventArgs e)
		{
		}

		public void LoadData()
		{
			currencyComboBox1.LoadData();
		}

		public void GetLastRate()
		{
			try
			{
				Rate = Factory.CurrencySystem.GetCurrencyRate(SelectedID);
			}
			catch
			{
			}
		}

		public decimal GetBaseEquivalant(decimal amount)
		{
			if (IsBaseCurrency)
			{
				return amount;
			}
			decimal d = Rate;
			decimal num = default(decimal);
			if (RateType == "M")
			{
				return Math.Round(amount * d, 4);
			}
			return Math.Round(amount / d, 4);
		}

		public decimal GetFCEquivalant(decimal amount)
		{
			if (IsBaseCurrency)
			{
				return amount;
			}
			decimal d = Rate;
			decimal num = default(decimal);
			if (RateType == "M")
			{
				return Math.Round(amount / d, 4);
			}
			return Math.Round(amount * d, 4);
		}

		private void currencyComboBox1_EditorButtonClick(object sender, EditorButtonEventArgs e)
		{
			SetCurrencyRateDialog setCurrencyRateDialog = new SetCurrencyRateDialog();
			setCurrencyRateDialog.Rate = rate;
			if (setCurrencyRateDialog.ShowDialog() == DialogResult.OK)
			{
				rate = setCurrencyRateDialog.Rate;
				((EditorButton)e.Button).Text = "R:" + rate.ToString();
				if (this.CurrencyRateChanged != null)
				{
					this.CurrencyRateChanged(this, null);
				}
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
			Infragistics.Win.UltraWinEditors.EditorButton editorButton = new Infragistics.Win.UltraWinEditors.EditorButton();
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
			currencyComboBox1 = new Micromind.DataControls.CurrencyComboBox();
			label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)currencyComboBox1).BeginInit();
			SuspendLayout();
			editorButton.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2003ToolbarButton;
			editorButton.Text = "R: 3.68";
			currencyComboBox1.ButtonsRight.Add(editorButton);
			currencyComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			currencyComboBox1.CustomReportFieldName = "";
			currencyComboBox1.CustomReportKey = "";
			currencyComboBox1.CustomReportValueType = 1;
			currencyComboBox1.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			currencyComboBox1.DisplayLayout.Appearance = appearance;
			currencyComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			currencyComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			currencyComboBox1.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			currencyComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			currencyComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			currencyComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			currencyComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			currencyComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			currencyComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			currencyComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			currencyComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			currencyComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			currencyComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			currencyComboBox1.DisplayLayout.Override.CellAppearance = appearance8;
			currencyComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			currencyComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			currencyComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			currencyComboBox1.DisplayLayout.Override.HeaderAppearance = appearance10;
			currencyComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			currencyComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			currencyComboBox1.DisplayLayout.Override.RowAppearance = appearance11;
			currencyComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			currencyComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			currencyComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			currencyComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			currencyComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			currencyComboBox1.Dock = System.Windows.Forms.DockStyle.Top;
			currencyComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			currencyComboBox1.Editable = true;
			currencyComboBox1.FilterString = "";
			currencyComboBox1.HasAllAccount = false;
			currencyComboBox1.HasCustom = false;
			currencyComboBox1.IsDataLoaded = false;
			currencyComboBox1.Location = new System.Drawing.Point(0, 0);
			currencyComboBox1.MaxDropDownItems = 12;
			currencyComboBox1.Name = "currencyComboBox1";
			currencyComboBox1.ShowInactiveItems = false;
			currencyComboBox1.ShowQuickAdd = true;
			currencyComboBox1.Size = new System.Drawing.Size(289, 20);
			currencyComboBox1.TabIndex = 0;
			currencyComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			currencyComboBox1.EditorButtonClick += new Infragistics.Win.UltraWinEditors.EditorButtonEventHandler(currencyComboBox1_EditorButtonClick);
			label1.Dock = System.Windows.Forms.DockStyle.Top;
			label1.Location = new System.Drawing.Point(0, 20);
			label1.MaximumSize = new System.Drawing.Size(999999, 2);
			label1.MinimumSize = new System.Drawing.Size(0, 2);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(289, 2);
			label1.TabIndex = 2;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.Controls.Add(label1);
			base.Controls.Add(currencyComboBox1);
			MaximumSize = new System.Drawing.Size(99999, 20);
			MinimumSize = new System.Drawing.Size(5, 20);
			base.Name = "CurrencySelector";
			base.Size = new System.Drawing.Size(289, 20);
			base.Load += new System.EventHandler(CurrencySelector_Load);
			((System.ComponentModel.ISupportInitialize)currencyComboBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
