using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class SelectAdvanceDialog : Form
	{
		public bool CanClose = true;

		private bool isMultiSelect;

		private List<string> hiddenColumns = new List<string>();

		public DataTable SelectedItemsTable;

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Line linePanelDown;

		private XPButton buttonCancel;

		private Panel panelDetails;

		private TextBox textBoxProjectCode;

		private Label label2;

		private TextBox textBoxProjectName;

		private TextBox textBoxEntityCode;

		private Label label4;

		private TextBox textBoxEntityName;

		private JobFeeComboBox comboBoxAdvance;

		private Label label1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private TextBox textBoxAdvanceName;

		private AmountTextBox textBoxAmount;

		private Label label3;

		private TextBox textBoxBilled;

		private TextBox textBoxProjectAdvance;

		private Label label5;

		private TextBox textBoxBillable;

		private Label label6;

		public bool IsMultiSelect
		{
			get
			{
				return isMultiSelect;
			}
			set
			{
				isMultiSelect = value;
			}
		}

		public string ProjectID
		{
			get
			{
				return textBoxProjectCode.Text;
			}
			set
			{
				textBoxProjectCode.Text = value;
			}
		}

		public string ProjectName
		{
			get
			{
				return textBoxProjectName.Text;
			}
			set
			{
				textBoxProjectName.Text = value;
			}
		}

		public string CustomerID
		{
			get
			{
				return textBoxEntityCode.Text;
			}
			set
			{
				textBoxEntityCode.Text = value;
			}
		}

		public string CustomerName
		{
			get
			{
				return textBoxEntityName.Text;
			}
			set
			{
				textBoxEntityName.Text = value;
			}
		}

		public string Description
		{
			get
			{
				return textBoxAdvanceName.Text;
			}
			set
			{
				textBoxAdvanceName.Text = value;
			}
		}

		public string Amount
		{
			get
			{
				return textBoxAmount.Text;
			}
			set
			{
				textBoxAmount.Text = value;
			}
		}

		public string AdvanceItemID
		{
			get
			{
				return comboBoxAdvance.SelectedID;
			}
			set
			{
				comboBoxAdvance.SelectedID = value;
			}
		}

		public DataSet DataSource
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public List<string> HiddenColumns => hiddenColumns;

		public event EventHandler ValidateSelection;

		public SelectAdvanceDialog()
		{
			InitializeComponent();
			base.Load += SelectAdvanceDialog_Load;
			base.Activated += SelectAdvanceDialog_Activated;
			base.FormClosing += SelectAdvanceDialog_FormClosing;
			base.StartPosition = FormStartPosition.CenterParent;
			comboBoxAdvance.ReadOnly = false;
			textBoxAdvanceName.ReadOnly = false;
		}

		private void SelectAdvanceDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				Global.GlobalSettings.SaveFormProperties(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SelectAdvanceDialog_Activated(object sender, EventArgs e)
		{
		}

		private void SelectAdvanceDialog_Load(object sender, EventArgs e)
		{
			try
			{
				Global.GlobalSettings.LoadFormProperties(this);
				LoadData();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public void LoadData()
		{
			try
			{
				DataSet jobByID = Factory.JobSystem.GetJobByID(ProjectID);
				decimal d = default(decimal);
				decimal d2 = default(decimal);
				if (jobByID.Tables[0].Rows[0]["AdvanceAmount"] != DBNull.Value)
				{
					d = decimal.Parse(jobByID.Tables[0].Rows[0]["AdvanceAmount"].ToString());
				}
				if (jobByID.Tables[0].Rows[0]["AdvanceBilled"] != DBNull.Value)
				{
					d2 = decimal.Parse(jobByID.Tables[0].Rows[0]["AdvanceBilled"].ToString());
				}
				textBoxProjectAdvance.Text = d.ToString(Format.TotalAmountFormat);
				textBoxBilled.Text = d2.ToString(Format.TotalAmountFormat);
				textBoxBillable.Text = (d - d2).ToString(Format.TotalAmountFormat);
				DataRow[] array = SelectedItemsTable.Select("ItemType = 4");
				if (array.Length != 0)
				{
					comboBoxAdvance.SelectedID = array[0]["ItemCode"].ToString();
					textBoxAmount.Text = array[0]["Amount"].ToString();
					textBoxAdvanceName.Text = array[0]["Description"].ToString();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public string GetSelectedCode(string codeColumnName)
		{
			return "";
		}

		private void textBoxSearch_TextChanged(object sender, EventArgs e)
		{
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			CanClose = true;
			if (CanClose)
			{
				Close();
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (comboBoxAdvance.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select an advance item.");
				base.DialogResult = DialogResult.None;
			}
			else
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditJobFee(comboBoxAdvance.SelectedID);
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
			panelButtons = new System.Windows.Forms.Panel();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			panelDetails = new System.Windows.Forms.Panel();
			textBoxProjectAdvance = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			textBoxBillable = new System.Windows.Forms.TextBox();
			label6 = new System.Windows.Forms.Label();
			textBoxBilled = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxProjectCode = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			textBoxProjectName = new System.Windows.Forms.TextBox();
			textBoxEntityCode = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			textBoxEntityName = new System.Windows.Forms.TextBox();
			comboBoxAdvance = new Micromind.DataControls.JobFeeComboBox();
			textBoxAdvanceName = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxAmount = new Micromind.UISupport.AmountTextBox();
			label3 = new System.Windows.Forms.Label();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAdvance).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 211);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(612, 40);
			panelButtons.TabIndex = 4;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(400, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 0;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(612, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(502, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 1;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			panelDetails.Controls.Add(textBoxProjectAdvance);
			panelDetails.Controls.Add(label5);
			panelDetails.Controls.Add(textBoxBillable);
			panelDetails.Controls.Add(label6);
			panelDetails.Controls.Add(textBoxBilled);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxProjectCode);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(textBoxProjectName);
			panelDetails.Controls.Add(textBoxEntityCode);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(textBoxEntityName);
			panelDetails.Location = new System.Drawing.Point(0, 1);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(594, 85);
			panelDetails.TabIndex = 0;
			textBoxProjectAdvance.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProjectAdvance.Location = new System.Drawing.Point(110, 56);
			textBoxProjectAdvance.MaxLength = 64;
			textBoxProjectAdvance.Name = "textBoxProjectAdvance";
			textBoxProjectAdvance.ReadOnly = true;
			textBoxProjectAdvance.Size = new System.Drawing.Size(92, 20);
			textBoxProjectAdvance.TabIndex = 4;
			textBoxProjectAdvance.TabStop = false;
			textBoxProjectAdvance.Text = "0.00";
			textBoxProjectAdvance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(12, 59);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(89, 13);
			label5.TabIndex = 151;
			label5.Text = "Project Advance:";
			textBoxBillable.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBillable.Location = new System.Drawing.Point(486, 55);
			textBoxBillable.MaxLength = 64;
			textBoxBillable.Name = "textBoxBillable";
			textBoxBillable.ReadOnly = true;
			textBoxBillable.Size = new System.Drawing.Size(101, 20);
			textBoxBillable.TabIndex = 6;
			textBoxBillable.TabStop = false;
			textBoxBillable.Text = "0.00";
			textBoxBillable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(396, 58);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(89, 13);
			label6.TabIndex = 149;
			label6.Text = "Advance Billable:";
			textBoxBilled.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBilled.Location = new System.Drawing.Point(291, 56);
			textBoxBilled.MaxLength = 64;
			textBoxBilled.Name = "textBoxBilled";
			textBoxBilled.ReadOnly = true;
			textBoxBilled.Size = new System.Drawing.Size(101, 20);
			textBoxBilled.TabIndex = 5;
			textBoxBilled.TabStop = false;
			textBoxBilled.Text = "0.00";
			textBoxBilled.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(208, 59);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(81, 13);
			label1.TabIndex = 149;
			label1.Text = "Advance Billed:";
			textBoxProjectCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProjectCode.Location = new System.Drawing.Point(110, 33);
			textBoxProjectCode.MaxLength = 64;
			textBoxProjectCode.Name = "textBoxProjectCode";
			textBoxProjectCode.ReadOnly = true;
			textBoxProjectCode.Size = new System.Drawing.Size(132, 20);
			textBoxProjectCode.TabIndex = 2;
			textBoxProjectCode.TabStop = false;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(13, 36);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(43, 13);
			label2.TabIndex = 147;
			label2.Text = "Project:";
			textBoxProjectName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProjectName.Location = new System.Drawing.Point(247, 33);
			textBoxProjectName.MaxLength = 64;
			textBoxProjectName.Name = "textBoxProjectName";
			textBoxProjectName.ReadOnly = true;
			textBoxProjectName.Size = new System.Drawing.Size(340, 20);
			textBoxProjectName.TabIndex = 3;
			textBoxProjectName.TabStop = false;
			textBoxEntityCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEntityCode.Location = new System.Drawing.Point(111, 10);
			textBoxEntityCode.MaxLength = 64;
			textBoxEntityCode.Name = "textBoxEntityCode";
			textBoxEntityCode.ReadOnly = true;
			textBoxEntityCode.Size = new System.Drawing.Size(132, 20);
			textBoxEntityCode.TabIndex = 0;
			textBoxEntityCode.TabStop = false;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(13, 13);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(54, 13);
			label4.TabIndex = 140;
			label4.Text = "Customer:";
			textBoxEntityName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEntityName.Location = new System.Drawing.Point(248, 10);
			textBoxEntityName.MaxLength = 64;
			textBoxEntityName.Name = "textBoxEntityName";
			textBoxEntityName.ReadOnly = true;
			textBoxEntityName.Size = new System.Drawing.Size(340, 20);
			textBoxEntityName.TabIndex = 1;
			textBoxEntityName.TabStop = false;
			comboBoxAdvance.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxAdvance.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAdvance.DescriptionTextBox = textBoxAdvanceName;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAdvance.DisplayLayout.Appearance = appearance;
			comboBoxAdvance.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAdvance.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAdvance.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAdvance.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxAdvance.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAdvance.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxAdvance.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAdvance.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAdvance.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAdvance.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxAdvance.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAdvance.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAdvance.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAdvance.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxAdvance.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAdvance.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAdvance.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxAdvance.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxAdvance.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAdvance.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxAdvance.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxAdvance.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAdvance.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxAdvance.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAdvance.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAdvance.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAdvance.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAdvance.Editable = true;
			comboBoxAdvance.FilteredType = Micromind.Common.Data.JobFeeTypes.Advance;
			comboBoxAdvance.FilterString = "";
			comboBoxAdvance.HasAllAccount = false;
			comboBoxAdvance.HasCustom = false;
			comboBoxAdvance.Location = new System.Drawing.Point(112, 104);
			comboBoxAdvance.MaxDropDownItems = 12;
			comboBoxAdvance.Name = "comboBoxAdvance";
			comboBoxAdvance.ReadOnly = true;
			comboBoxAdvance.ShowInactiveItems = false;
			comboBoxAdvance.ShowQuickAdd = true;
			comboBoxAdvance.Size = new System.Drawing.Size(131, 20);
			comboBoxAdvance.TabIndex = 1;
			comboBoxAdvance.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxAdvanceName.BackColor = System.Drawing.Color.White;
			textBoxAdvanceName.Location = new System.Drawing.Point(247, 104);
			textBoxAdvanceName.MaxLength = 64;
			textBoxAdvanceName.Name = "textBoxAdvanceName";
			textBoxAdvanceName.ReadOnly = true;
			textBoxAdvanceName.Size = new System.Drawing.Size(340, 20);
			textBoxAdvanceName.TabIndex = 2;
			appearance13.FontData.BoldAsString = "False";
			appearance13.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance13;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(16, 106);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(75, 15);
			ultraFormattedLinkLabel1.TabIndex = 135;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Advance Item:";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance14;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			textBoxAmount.BackColor = System.Drawing.Color.White;
			textBoxAmount.IsComboTextBox = false;
			textBoxAmount.Location = new System.Drawing.Point(112, 126);
			textBoxAmount.Name = "textBoxAmount";
			textBoxAmount.Size = new System.Drawing.Size(132, 20);
			textBoxAmount.TabIndex = 3;
			textBoxAmount.Text = "0.00";
			textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(14, 129);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(46, 13);
			label3.TabIndex = 151;
			label3.Text = "Amount:";
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(612, 251);
			base.Controls.Add(textBoxAmount);
			base.Controls.Add(label3);
			base.Controls.Add(textBoxAdvanceName);
			base.Controls.Add(ultraFormattedLinkLabel1);
			base.Controls.Add(comboBoxAdvance);
			base.Controls.Add(panelDetails);
			base.Controls.Add(panelButtons);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(465, 215);
			base.Name = "SelectAdvanceDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Project Advance";
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAdvance).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
