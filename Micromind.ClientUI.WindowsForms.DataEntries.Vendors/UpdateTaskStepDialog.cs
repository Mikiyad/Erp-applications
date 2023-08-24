using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Vendors
{
	public class UpdateTaskStepDialog : Form
	{
		public SysDocTypes SysDocumentType = SysDocTypes.None;

		private string sysDocID = "";

		private string voucherID = "";

		private IContainer components;

		private Button buttonCancel;

		private Button buttonOK;

		private Line linePanelDown;

		private Label label2;

		private Label label1;

		private TaskStepsComboBox comboBoxStep;

		public string Step
		{
			get
			{
				return comboBoxStep.SelectedID;
			}
			set
			{
				comboBoxStep.SelectedID = value;
			}
		}

		public UpdateTaskStepDialog()
		{
			InitializeComponent();
			base.Load += ClosePeriodDialog_Load;
			base.StartPosition = FormStartPosition.CenterParent;
		}

		public void SetDocument(SysDocTypes DocumentType, string sysDocID, string voucherID)
		{
			this.sysDocID = sysDocID;
			this.voucherID = voucherID;
			SysDocumentType = DocumentType;
			object obj = null;
			if (DocumentType == SysDocTypes.TaskTransaction)
			{
				obj = Factory.DatabaseSystem.GetFieldValue("Task_Transaction", "TaskStepID", "SysDocID", sysDocID, "VoucherID", voucherID);
			}
			if (obj != null)
			{
				comboBoxStep.SelectedID = obj.ToString();
				label2.Text = "Please select the new Step :";
				comboBoxStep.LoadData();
			}
		}

		private void ClosePeriodDialog_Load(object sender, EventArgs e)
		{
			try
			{
				label2.Text = "Please select the Step:";
				comboBoxStep.LoadData();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				if (Factory.TaskTransactionSystem.SetTaskStep(sysDocID, voucherID, comboBoxStep.SelectedID))
				{
					base.DialogResult = DialogResult.OK;
					Close();
				}
			}
			catch (CompanyException e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			catch (Exception e3)
			{
				ErrorHelper.ProcessError(e3);
			}
		}

		private void UpdatePOStatusDialog_Load(object sender, EventArgs e)
		{
		}

		private void ClosePeriodDialog_Activated(object sender, EventArgs e)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Vendors.UpdateTaskStepDialog));
			buttonCancel = new System.Windows.Forms.Button();
			buttonOK = new System.Windows.Forms.Button();
			linePanelDown = new Micromind.UISupport.Line();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			comboBoxStep = new Micromind.DataControls.TaskStepsComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxStep).BeginInit();
			SuspendLayout();
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(182, 110);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(88, 25);
			buttonCancel.TabIndex = 2;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(89, 110);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(87, 25);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(button2_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(-36, 100);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(527, 1);
			linePanelDown.TabIndex = 15;
			linePanelDown.TabStop = false;
			label2.Location = new System.Drawing.Point(4, 9);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(266, 21);
			label2.TabIndex = 23;
			label2.Text = "Please select the new Step :";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(10, 46);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(32, 13);
			label1.TabIndex = 22;
			label1.Text = "Step:";
			comboBoxStep.Assigned = false;
			comboBoxStep.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxStep.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxStep.CustomReportFieldName = "";
			comboBoxStep.CustomReportKey = "";
			comboBoxStep.CustomReportValueType = 1;
			comboBoxStep.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxStep.DisplayLayout.Appearance = appearance;
			comboBoxStep.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxStep.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStep.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStep.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxStep.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStep.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxStep.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxStep.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxStep.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxStep.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxStep.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxStep.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxStep.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxStep.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxStep.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxStep.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStep.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxStep.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxStep.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxStep.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxStep.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxStep.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxStep.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxStep.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxStep.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxStep.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxStep.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxStep.Editable = true;
			comboBoxStep.FilterString = "";
			comboBoxStep.HasAllAccount = false;
			comboBoxStep.HasCustom = false;
			comboBoxStep.IsDataLoaded = false;
			comboBoxStep.Location = new System.Drawing.Point(48, 46);
			comboBoxStep.MaxDropDownItems = 12;
			comboBoxStep.Name = "comboBoxStep";
			comboBoxStep.ShowInactiveItems = false;
			comboBoxStep.ShowQuickAdd = true;
			comboBoxStep.Size = new System.Drawing.Size(156, 20);
			comboBoxStep.TabIndex = 24;
			comboBoxStep.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(282, 143);
			base.Controls.Add(comboBoxStep);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(linePanelDown);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "UpdateTaskStepDialog";
			Text = "Change Step";
			base.Activated += new System.EventHandler(ClosePeriodDialog_Activated);
			base.Load += new System.EventHandler(UpdatePOStatusDialog_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxStep).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
