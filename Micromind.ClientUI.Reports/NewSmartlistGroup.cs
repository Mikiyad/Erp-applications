using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports
{
	public class NewSmartlistGroup : Form
	{
		private int index = -1;

		private string setreport = "";

		private IContainer components;

		private Button buttonCancel;

		private TextBox textBoxName;

		private Label label1;

		private Button buttonOK;

		private SmartListCategoryComboBox comboBoxCategory;

		private MMLabel mmLabel2;

		private Line line1;

		private ExternalReportCategoryComboBox comboBoxExternalCategory;

		public bool IsNewRecord
		{
			get;
			set;
		}

		public string CategoryName
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

		public string CategoryID
		{
			get;
			set;
		}

		public int RowIndex
		{
			set
			{
				index = value;
			}
		}

		public string ParentID
		{
			get
			{
				if (setreport == "")
				{
					return comboBoxCategory.SelectedID;
				}
				return comboBoxExternalCategory.SelectedID;
			}
			set
			{
				if (setreport == "")
				{
					comboBoxExternalCategory.Visible = false;
					comboBoxCategory.SelectedID = value;
				}
				else
				{
					comboBoxCategory.Visible = false;
					comboBoxExternalCategory.SelectedID = value;
				}
			}
		}

		public string SetReport
		{
			get
			{
				return setreport;
			}
			set
			{
				setreport = value;
			}
		}

		public NewSmartlistGroup()
		{
			InitializeComponent();
			comboBoxCategory.LoadData(isReferesh: true);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (CategoryID != "" && comboBoxCategory.SelectedID == CategoryID)
			{
				ErrorHelper.WarningMessage("Parent category cannot be same as the category. Please select a different parent.");
				base.DialogResult = DialogResult.None;
			}
			else
			{
				base.DialogResult = DialogResult.OK;
				Close();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.NewSmartlistGroup));
			buttonCancel = new System.Windows.Forms.Button();
			textBoxName = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			buttonOK = new System.Windows.Forms.Button();
			line1 = new Micromind.UISupport.Line();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			comboBoxExternalCategory = new Micromind.DataControls.ExternalReportCategoryComboBox();
			comboBoxCategory = new Micromind.DataControls.SmartListCategoryComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxExternalCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCategory).BeginInit();
			SuspendLayout();
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(359, 110);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(75, 23);
			buttonCancel.TabIndex = 3;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			textBoxName.Location = new System.Drawing.Point(63, 17);
			textBoxName.MaxLength = 30;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(371, 20);
			textBoxName.TabIndex = 0;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(11, 21);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(38, 13);
			label1.TabIndex = 2;
			label1.Text = "Name:";
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(278, 110);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(75, 23);
			buttonOK.TabIndex = 2;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(button2_Click);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(-11, 101);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(503, 1);
			line1.TabIndex = 6;
			line1.TabStop = false;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(11, 44);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(41, 13);
			mmLabel2.TabIndex = 5;
			mmLabel2.Text = "Parent:";
			comboBoxExternalCategory.Assigned = false;
			comboBoxExternalCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxExternalCategory.CustomReportFieldName = "";
			comboBoxExternalCategory.CustomReportKey = "";
			comboBoxExternalCategory.CustomReportValueType = 1;
			comboBoxExternalCategory.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxExternalCategory.DisplayLayout.Appearance = appearance;
			comboBoxExternalCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxExternalCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxExternalCategory.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxExternalCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxExternalCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxExternalCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxExternalCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxExternalCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxExternalCategory.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxExternalCategory.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxExternalCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxExternalCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxExternalCategory.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxExternalCategory.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxExternalCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxExternalCategory.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxExternalCategory.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxExternalCategory.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxExternalCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxExternalCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxExternalCategory.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxExternalCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxExternalCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxExternalCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxExternalCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxExternalCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxExternalCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxExternalCategory.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxExternalCategory.Editable = true;
			comboBoxExternalCategory.FilterString = "";
			comboBoxExternalCategory.HasAllAccount = false;
			comboBoxExternalCategory.HasCustom = false;
			comboBoxExternalCategory.IsDataLoaded = false;
			comboBoxExternalCategory.Location = new System.Drawing.Point(63, 40);
			comboBoxExternalCategory.MaxDropDownItems = 12;
			comboBoxExternalCategory.Name = "comboBoxExternalCategory";
			comboBoxExternalCategory.ShowInactiveItems = false;
			comboBoxExternalCategory.ShowQuickAdd = true;
			comboBoxExternalCategory.Size = new System.Drawing.Size(371, 20);
			comboBoxExternalCategory.TabIndex = 7;
			comboBoxExternalCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCategory.Assigned = false;
			comboBoxCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCategory.CustomReportFieldName = "";
			comboBoxCategory.CustomReportKey = "";
			comboBoxCategory.CustomReportValueType = 1;
			comboBoxCategory.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCategory.DisplayLayout.Appearance = appearance13;
			comboBoxCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCategory.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCategory.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCategory.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCategory.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxCategory.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxCategory.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCategory.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxCategory.Editable = true;
			comboBoxCategory.FilterString = "";
			comboBoxCategory.HasAllAccount = false;
			comboBoxCategory.HasCustom = false;
			comboBoxCategory.IsDataLoaded = false;
			comboBoxCategory.Location = new System.Drawing.Point(63, 40);
			comboBoxCategory.MaxDropDownItems = 12;
			comboBoxCategory.Name = "comboBoxCategory";
			comboBoxCategory.ShowInactiveItems = false;
			comboBoxCategory.ShowQuickAdd = true;
			comboBoxCategory.Size = new System.Drawing.Size(371, 20);
			comboBoxCategory.TabIndex = 1;
			comboBoxCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(446, 138);
			base.Controls.Add(comboBoxExternalCategory);
			base.Controls.Add(line1);
			base.Controls.Add(comboBoxCategory);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxName);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "NewSmartlistGroup";
			Text = "Smart List Category";
			((System.ComponentModel.ISupportInitialize)comboBoxExternalCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCategory).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
