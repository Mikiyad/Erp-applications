using DevExpress.XtraEditors;
using Infragistics.Win;
using Infragistics.Win.UltraWinDataSource;
using Infragistics.Win.UltraWinGrid;
using Micromind.DataControls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.POS
{
	public class ChangeSalespersonForm : XtraForm
	{
		private decimal subtotal;

		private bool isDiscountPercent;

		private IContainer components;

		private UltraDataSource ultraDataSource1;

		private SimpleButton buttonSave;

		private SimpleButton buttonCancel;

		private Label label1;

		private SalespersonComboBox salespersonComboBox1;

		public decimal Subtotal
		{
			set
			{
				subtotal = value;
			}
		}

		public string SalespersonID
		{
			get
			{
				return salespersonComboBox1.SelectedID;
			}
			set
			{
				salespersonComboBox1.SelectedID = value;
			}
		}

		public string SalespersonName => salespersonComboBox1.SelectedName;

		public bool IsPercent
		{
			get
			{
				return isDiscountPercent;
			}
			set
			{
				isDiscountPercent = value;
			}
		}

		public ChangeSalespersonForm()
		{
			InitializeComponent();
		}

		private void XtraForm1_Load(object sender, EventArgs e)
		{
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void textBoxDiscountAmount_Validating(object sender, CancelEventArgs e)
		{
		}

		private void textBoxDiscountPercent_TextChanged(object sender, EventArgs e)
		{
		}

		private void textBoxDiscountAmount_TextChanged(object sender, EventArgs e)
		{
		}

		private void CalculateTotal()
		{
		}

		private void DiscountForm_Activated(object sender, EventArgs e)
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
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			buttonSave = new DevExpress.XtraEditors.SimpleButton();
			buttonCancel = new DevExpress.XtraEditors.SimpleButton();
			label1 = new System.Windows.Forms.Label();
			salespersonComboBox1 = new Micromind.DataControls.SalespersonComboBox();
			((System.ComponentModel.ISupportInitialize)salespersonComboBox1).BeginInit();
			SuspendLayout();
			buttonSave.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonSave.Appearance.Options.UseFont = true;
			buttonSave.Location = new System.Drawing.Point(197, 116);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(102, 40);
			buttonSave.TabIndex = 2;
			buttonSave.Text = "OK";
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			buttonCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonCancel.Appearance.Options.UseFont = true;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(305, 116);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(97, 40);
			buttonCancel.TabIndex = 3;
			buttonCancel.Text = "Cancel";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			label1.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			label1.Location = new System.Drawing.Point(9, 28);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(181, 22);
			label1.TabIndex = 37;
			label1.Text = "Select Salesperson:";
			appearance.FontData.SizeInPoints = 14f;
			salespersonComboBox1.Appearance = appearance;
			salespersonComboBox1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			salespersonComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			salespersonComboBox1.CustomReportFieldName = "";
			salespersonComboBox1.CustomReportKey = "";
			salespersonComboBox1.CustomReportValueType = 1;
			salespersonComboBox1.DescriptionTextBox = null;
			appearance2.BackColor = System.Drawing.SystemColors.Window;
			appearance2.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			appearance2.FontData.BoldAsString = "False";
			appearance2.FontData.SizeInPoints = 14f;
			salespersonComboBox1.DisplayLayout.Appearance = appearance2;
			salespersonComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			salespersonComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance3.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance3.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance3.BorderColor = System.Drawing.SystemColors.Window;
			salespersonComboBox1.DisplayLayout.GroupByBox.Appearance = appearance3;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			salespersonComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance4;
			salespersonComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance5.BackColor2 = System.Drawing.SystemColors.Control;
			appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			salespersonComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance5;
			salespersonComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			salespersonComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance6.BackColor = System.Drawing.SystemColors.Window;
			appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
			salespersonComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance6;
			appearance7.BackColor = System.Drawing.SystemColors.Highlight;
			appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
			salespersonComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance7;
			salespersonComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			salespersonComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance8.BackColor = System.Drawing.SystemColors.Window;
			salespersonComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance8;
			appearance9.BorderColor = System.Drawing.Color.Silver;
			appearance9.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			salespersonComboBox1.DisplayLayout.Override.CellAppearance = appearance9;
			salespersonComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			salespersonComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance10.BackColor = System.Drawing.SystemColors.Control;
			appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance10.BorderColor = System.Drawing.SystemColors.Window;
			salespersonComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance10;
			appearance11.TextHAlignAsString = "Left";
			salespersonComboBox1.DisplayLayout.Override.HeaderAppearance = appearance11;
			salespersonComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			salespersonComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance12.BackColor = System.Drawing.SystemColors.Window;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			salespersonComboBox1.DisplayLayout.Override.RowAppearance = appearance12;
			salespersonComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance13.BackColor = System.Drawing.SystemColors.ControlLight;
			salespersonComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance13;
			salespersonComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			salespersonComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			salespersonComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			salespersonComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			salespersonComboBox1.Editable = true;
			salespersonComboBox1.FilterString = "";
			salespersonComboBox1.HasAllAccount = false;
			salespersonComboBox1.HasCustom = false;
			salespersonComboBox1.IsDataLoaded = false;
			salespersonComboBox1.Location = new System.Drawing.Point(11, 53);
			salespersonComboBox1.MaxDropDownItems = 12;
			salespersonComboBox1.Name = "salespersonComboBox1";
			salespersonComboBox1.ShowInactiveItems = false;
			salespersonComboBox1.ShowQuickAdd = true;
			salespersonComboBox1.Size = new System.Drawing.Size(393, 31);
			salespersonComboBox1.TabIndex = 38;
			salespersonComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(415, 167);
			base.Controls.Add(salespersonComboBox1);
			base.Controls.Add(label1);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(buttonSave);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ChangeSalespersonForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Change Salesperson";
			base.Activated += new System.EventHandler(DiscountForm_Activated);
			base.Load += new System.EventHandler(XtraForm1_Load);
			((System.ComponentModel.ISupportInitialize)salespersonComboBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
