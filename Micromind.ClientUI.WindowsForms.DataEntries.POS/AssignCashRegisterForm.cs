using DevExpress.XtraEditors;
using Infragistics.Win;
using Infragistics.Win.UltraWinDataSource;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.DataControls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.POS
{
	public class AssignCashRegisterForm : XtraForm
	{
		private IContainer components;

		private UltraDataSource ultraDataSource1;

		private SimpleButton buttonSave;

		private SimpleButton buttonCancel;

		private Label labelVoucherNumber;

		private POSCashRegisterComboBox posCashRegisterComboBox1;

		public AssignCashRegisterForm()
		{
			InitializeComponent();
		}

		private void XtraForm1_Load(object sender, EventArgs e)
		{
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (posCashRegisterComboBox1.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select a Cash Register.");
				base.DialogResult = DialogResult.None;
			}
			else
			{
				try
				{
					if (!Factory.POSCashRegisterSystem.IsCashRegisterFree(posCashRegisterComboBox1.SelectedID) && ErrorHelper.QuestionMessageYesNo("This Cash Register is already assigned to another POS machine. Are you sure you want to assign it to this machine?") == DialogResult.No)
					{
						return;
					}
					if (!Factory.POSCashRegisterSystem.AssignCashRegister(posCashRegisterComboBox1.SelectedID, Environment.MachineName))
					{
						ErrorHelper.ErrorMessage("Cannot assign the Cash Register. Please try again.");
						return;
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn1 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Column 0");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn2 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Column 1");
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
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
            this.ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
            this.buttonSave = new DevExpress.XtraEditors.SimpleButton();
            this.buttonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelVoucherNumber = new System.Windows.Forms.Label();
            this.posCashRegisterComboBox1 = new Micromind.DataControls.POSCashRegisterComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.posCashRegisterComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraDataSource1
            // 
            this.ultraDataSource1.Band.Columns.AddRange(new object[] {
            ultraDataColumn1,
            ultraDataColumn2});
            // 
            // buttonSave
            // 
            this.buttonSave.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.buttonSave.Appearance.Options.UseFont = true;
            this.buttonSave.Location = new System.Drawing.Point(174, 193);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(102, 40);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.buttonCancel.Appearance.Options.UseFont = true;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(282, 193);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(97, 40);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelVoucherNumber
            // 
            this.labelVoucherNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelVoucherNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVoucherNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(37)))), ((int)(((byte)(127)))));
            this.labelVoucherNumber.Location = new System.Drawing.Point(12, 13);
            this.labelVoucherNumber.Name = "labelVoucherNumber";
            this.labelVoucherNumber.Size = new System.Drawing.Size(367, 57);
            this.labelVoucherNumber.TabIndex = 31;
            this.labelVoucherNumber.Text = "There is no Cash Register assigned for this POS system. Please assign a Cash Regi" +
    "ster:";
            // 
            // posCashRegisterComboBox1
            // 
            this.posCashRegisterComboBox1.AlwaysInEditMode = true;
            this.posCashRegisterComboBox1.Assigned = false;
            this.posCashRegisterComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.posCashRegisterComboBox1.CustomReportFieldName = "";
            this.posCashRegisterComboBox1.CustomReportKey = "";
            this.posCashRegisterComboBox1.CustomReportValueType = ((byte)(1));
            this.posCashRegisterComboBox1.DescriptionTextBox = null;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.posCashRegisterComboBox1.DisplayLayout.Appearance = appearance1;
            this.posCashRegisterComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.posCashRegisterComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.posCashRegisterComboBox1.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.posCashRegisterComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.posCashRegisterComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.posCashRegisterComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.posCashRegisterComboBox1.DisplayLayout.MaxColScrollRegions = 1;
            this.posCashRegisterComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.posCashRegisterComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.posCashRegisterComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.posCashRegisterComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.posCashRegisterComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.posCashRegisterComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.posCashRegisterComboBox1.DisplayLayout.Override.CellAppearance = appearance8;
            this.posCashRegisterComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.posCashRegisterComboBox1.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.posCashRegisterComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.posCashRegisterComboBox1.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.posCashRegisterComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.posCashRegisterComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.posCashRegisterComboBox1.DisplayLayout.Override.RowAppearance = appearance11;
            this.posCashRegisterComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.posCashRegisterComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.posCashRegisterComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.posCashRegisterComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.posCashRegisterComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.posCashRegisterComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.posCashRegisterComboBox1.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
            this.posCashRegisterComboBox1.Editable = true;
            this.posCashRegisterComboBox1.FilterString = "";
            this.posCashRegisterComboBox1.Font = new System.Drawing.Font("Tahoma", 14F);
            this.posCashRegisterComboBox1.HasAllAccount = false;
            this.posCashRegisterComboBox1.HasCustom = false;
            this.posCashRegisterComboBox1.IsDataLoaded = false;
            this.posCashRegisterComboBox1.Location = new System.Drawing.Point(16, 69);
            this.posCashRegisterComboBox1.MaxDropDownItems = 12;
            this.posCashRegisterComboBox1.Name = "posCashRegisterComboBox1";
            this.posCashRegisterComboBox1.ShowInactiveItems = false;
            this.posCashRegisterComboBox1.ShowQuickAdd = true;
            this.posCashRegisterComboBox1.Size = new System.Drawing.Size(277, 31);
            this.posCashRegisterComboBox1.TabIndex = 0;
            this.posCashRegisterComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // AssignCashRegisterForm
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(393, 248);
            this.Controls.Add(this.posCashRegisterComboBox1);
            this.Controls.Add(this.labelVoucherNumber);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AssignCashRegisterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Assign Cash Register";
            this.Load += new System.EventHandler(this.XtraForm1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.posCashRegisterComboBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
