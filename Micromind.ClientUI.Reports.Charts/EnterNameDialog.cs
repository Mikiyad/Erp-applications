using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.Charts
{
	public class EnterNameDialog : Form
	{
		private IContainer components;

		private Button buttonCancel;

		private TextBox textBoxCode;

		private Label label1;

		private Button buttonOK;

		private Label label2;

		private Line linePanelDown;

		private Label label3;

		private TextBox textBoxName;

		private PivotGroupComboBox comboBoxPivotGroup;

		private Label label4;

		public string EnteredCode
		{
			get
			{
				return textBoxCode.Text;
			}
			set
			{
				textBoxCode.Text = value;
			}
		}

		public string EnteredName
		{
			get
			{
				return textBoxName.Text;
			}
			set
			{
				textBoxName.Text = value;
			}
		}

		public string SelectedGroup
		{
			get
			{
				return comboBoxPivotGroup.SelectedID;
			}
			set
			{
				comboBoxPivotGroup.SelectedID = value;
			}
		}

		public EnterNameDialog()
		{
			InitializeComponent();
			base.StartPosition = FormStartPosition.CenterParent;
			textBoxCode.Validating += textBoxCode_Validating;
		}

		private void textBoxCode_Validating(object sender, CancelEventArgs e)
		{
			textBoxCode.Text = textBoxCode.Text.Trim().Replace(" ", "");
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (textBoxCode.Text == "" || textBoxName.Text == "" || comboBoxPivotGroup.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				base.DialogResult = DialogResult.None;
			}
			else if (Factory.DatabaseSystem.ExistFieldValue("Pivot_Report", "PivotID", textBoxCode.Text.Trim()))
			{
				ErrorHelper.InformationMessage("This code is already exist.", "Please enter another code.");
				base.DialogResult = DialogResult.None;
			}
			else
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void EnterNameDialog_Activated(object sender, EventArgs e)
		{
			textBoxCode.Focus();
		}

		private void label1_Click(object sender, EventArgs e)
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
			buttonCancel = new System.Windows.Forms.Button();
			textBoxCode = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			buttonOK = new System.Windows.Forms.Button();
			label2 = new System.Windows.Forms.Label();
			linePanelDown = new Micromind.UISupport.Line();
			label3 = new System.Windows.Forms.Label();
			textBoxName = new System.Windows.Forms.TextBox();
			comboBoxPivotGroup = new Micromind.DataControls.PivotGroupComboBox();
			label4 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)comboBoxPivotGroup).BeginInit();
			SuspendLayout();
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(336, 156);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(88, 25);
			buttonCancel.TabIndex = 4;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.Location = new System.Drawing.Point(66, 48);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(222, 20);
			textBoxCode.TabIndex = 0;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(5, 51);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(40, 13);
			label1.TabIndex = 2;
			label1.Text = "Code:";
			label1.Click += new System.EventHandler(label1_Click);
			buttonOK.Location = new System.Drawing.Point(243, 156);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(87, 25);
			buttonOK.TabIndex = 3;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(button2_Click);
			label2.Location = new System.Drawing.Point(12, 9);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(351, 23);
			label2.TabIndex = 3;
			label2.Text = "Please enter name and display text for the report:";
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(-8, 149);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(505, 1);
			linePanelDown.TabIndex = 15;
			linePanelDown.TabStop = false;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(5, 74);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(43, 13);
			label3.TabIndex = 17;
			label3.Text = "Name:";
			textBoxName.Location = new System.Drawing.Point(66, 71);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(357, 20);
			textBoxName.TabIndex = 1;
			comboBoxPivotGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPivotGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPivotGroup.CustomReportFieldName = "";
			comboBoxPivotGroup.CustomReportKey = "";
			comboBoxPivotGroup.CustomReportValueType = 1;
			comboBoxPivotGroup.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPivotGroup.DisplayLayout.Appearance = appearance;
			comboBoxPivotGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPivotGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPivotGroup.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPivotGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxPivotGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPivotGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxPivotGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPivotGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPivotGroup.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPivotGroup.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxPivotGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPivotGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPivotGroup.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPivotGroup.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxPivotGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPivotGroup.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPivotGroup.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxPivotGroup.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxPivotGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPivotGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxPivotGroup.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxPivotGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPivotGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxPivotGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPivotGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPivotGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPivotGroup.DisplayMember = "Name";
			comboBoxPivotGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPivotGroup.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxPivotGroup.Editable = true;
			comboBoxPivotGroup.FilterString = "";
			comboBoxPivotGroup.HasAllAccount = false;
			comboBoxPivotGroup.HasCustom = false;
			comboBoxPivotGroup.IsDataLoaded = false;
			comboBoxPivotGroup.Location = new System.Drawing.Point(66, 95);
			comboBoxPivotGroup.MaxDropDownItems = 12;
			comboBoxPivotGroup.Name = "comboBoxPivotGroup";
			comboBoxPivotGroup.ShowInactiveItems = false;
			comboBoxPivotGroup.ShowQuickAdd = true;
			comboBoxPivotGroup.Size = new System.Drawing.Size(177, 20);
			comboBoxPivotGroup.TabIndex = 2;
			comboBoxPivotGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(6, 97);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(45, 13);
			label4.TabIndex = 19;
			label4.Text = "Group:";
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(429, 187);
			base.Controls.Add(label4);
			base.Controls.Add(comboBoxPivotGroup);
			base.Controls.Add(label3);
			base.Controls.Add(textBoxName);
			base.Controls.Add(linePanelDown);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "EnterNameDialog";
			Text = "Save as";
			base.Activated += new System.EventHandler(EnterNameDialog_Activated);
			((System.ComponentModel.ISupportInitialize)comboBoxPivotGroup).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
