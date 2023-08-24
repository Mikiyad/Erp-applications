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
	public class SelectRetentionDialog : Form
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

		private JobFeeComboBox comboBoxRetention;

		private Label label1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private TextBox textBoxRetentionName;

		private AmountTextBox textBoxAmount;

		private Label label3;

		private TextBox textBoxBalance;

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
				return textBoxRetentionName.Text;
			}
			set
			{
				textBoxRetentionName.Text = value;
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

		public string RetentionItemID
		{
			get
			{
				return comboBoxRetention.SelectedID;
			}
			set
			{
				comboBoxRetention.SelectedID = value;
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

		public SelectRetentionDialog()
		{
			InitializeComponent();
			base.Load += SelectRetentionDialog_Load;
			base.Activated += SelectRetentionDialog_Activated;
			base.FormClosing += SelectRetentionDialog_FormClosing;
			base.StartPosition = FormStartPosition.CenterParent;
		}

		private void SelectRetentionDialog_FormClosing(object sender, FormClosingEventArgs e)
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

		private void SelectRetentionDialog_Activated(object sender, EventArgs e)
		{
		}

		private void SelectRetentionDialog_Load(object sender, EventArgs e)
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
			DataSet jobByID = Factory.JobSystem.GetJobByID(ProjectID);
			decimal d = default(decimal);
			decimal d2 = default(decimal);
			if (jobByID.Tables[0].Rows[0]["RetentionAmount"] != DBNull.Value)
			{
				d = decimal.Parse(jobByID.Tables[0].Rows[0]["RetentionAmount"].ToString());
			}
			if (jobByID.Tables[0].Rows[0]["RetentionPaid"] != DBNull.Value)
			{
				d2 = decimal.Parse(jobByID.Tables[0].Rows[0]["RetentionPaid"].ToString());
			}
			textBoxBalance.Text = (d - d2).ToString(Format.TotalAmountFormat);
			DataRow[] array = SelectedItemsTable.Select("ItemType = 3");
			if (array.Length != 0)
			{
				comboBoxRetention.SelectedID = array[0]["ItemCode"].ToString();
				textBoxAmount.Text = array[0]["Amount"].ToString();
				textBoxRetentionName.Text = array[0]["Description"].ToString();
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
			if (comboBoxRetention.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select a retention item.");
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
			new FormHelper().EditJobFee(comboBoxRetention.SelectedID);
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
			textBoxBalance = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxProjectCode = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			textBoxProjectName = new System.Windows.Forms.TextBox();
			textBoxEntityCode = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			textBoxEntityName = new System.Windows.Forms.TextBox();
			comboBoxRetention = new Micromind.DataControls.JobFeeComboBox();
			textBoxRetentionName = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxAmount = new Micromind.UISupport.AmountTextBox();
			label3 = new System.Windows.Forms.Label();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxRetention).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 211);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(594, 40);
			panelButtons.TabIndex = 4;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(382, 8);
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
			linePanelDown.Size = new System.Drawing.Size(594, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(484, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 1;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			panelDetails.Controls.Add(textBoxBalance);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxProjectCode);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(textBoxProjectName);
			panelDetails.Controls.Add(textBoxEntityCode);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(textBoxEntityName);
			panelDetails.Location = new System.Drawing.Point(0, 1);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(576, 85);
			panelDetails.TabIndex = 0;
			textBoxBalance.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBalance.Location = new System.Drawing.Point(103, 55);
			textBoxBalance.MaxLength = 64;
			textBoxBalance.Name = "textBoxBalance";
			textBoxBalance.ReadOnly = true;
			textBoxBalance.Size = new System.Drawing.Size(132, 20);
			textBoxBalance.TabIndex = 4;
			textBoxBalance.TabStop = false;
			textBoxBalance.Text = "0.00";
			textBoxBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(13, 62);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(72, 13);
			label1.TabIndex = 149;
			label1.Text = "Ret. Balance:";
			textBoxProjectCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProjectCode.Location = new System.Drawing.Point(102, 33);
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
			textBoxProjectName.Location = new System.Drawing.Point(241, 33);
			textBoxProjectName.MaxLength = 64;
			textBoxProjectName.Name = "textBoxProjectName";
			textBoxProjectName.ReadOnly = true;
			textBoxProjectName.Size = new System.Drawing.Size(321, 20);
			textBoxProjectName.TabIndex = 3;
			textBoxProjectName.TabStop = false;
			textBoxEntityCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEntityCode.Location = new System.Drawing.Point(103, 10);
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
			textBoxEntityName.Location = new System.Drawing.Point(242, 10);
			textBoxEntityName.MaxLength = 64;
			textBoxEntityName.Name = "textBoxEntityName";
			textBoxEntityName.ReadOnly = true;
			textBoxEntityName.Size = new System.Drawing.Size(321, 20);
			textBoxEntityName.TabIndex = 1;
			textBoxEntityName.TabStop = false;
			comboBoxRetention.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxRetention.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxRetention.DescriptionTextBox = textBoxRetentionName;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxRetention.DisplayLayout.Appearance = appearance;
			comboBoxRetention.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxRetention.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRetention.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRetention.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxRetention.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRetention.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxRetention.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxRetention.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxRetention.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxRetention.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxRetention.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxRetention.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxRetention.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxRetention.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxRetention.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxRetention.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRetention.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxRetention.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxRetention.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxRetention.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxRetention.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxRetention.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxRetention.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxRetention.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxRetention.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxRetention.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxRetention.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxRetention.Editable = true;
			comboBoxRetention.FilteredType = Micromind.Common.Data.JobFeeTypes.Retention;
			comboBoxRetention.FilterString = "";
			comboBoxRetention.HasAllAccount = false;
			comboBoxRetention.HasCustom = false;
			comboBoxRetention.Location = new System.Drawing.Point(103, 104);
			comboBoxRetention.MaxDropDownItems = 12;
			comboBoxRetention.Name = "comboBoxRetention";
			comboBoxRetention.ShowInactiveItems = false;
			comboBoxRetention.ShowQuickAdd = true;
			comboBoxRetention.Size = new System.Drawing.Size(131, 20);
			comboBoxRetention.TabIndex = 1;
			comboBoxRetention.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxRetentionName.BackColor = System.Drawing.Color.White;
			textBoxRetentionName.Location = new System.Drawing.Point(242, 104);
			textBoxRetentionName.MaxLength = 64;
			textBoxRetentionName.Name = "textBoxRetentionName";
			textBoxRetentionName.Size = new System.Drawing.Size(321, 20);
			textBoxRetentionName.TabIndex = 2;
			appearance13.FontData.BoldAsString = "False";
			appearance13.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance13;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(16, 106);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(80, 15);
			ultraFormattedLinkLabel1.TabIndex = 135;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Retention Item:";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance14;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			textBoxAmount.BackColor = System.Drawing.Color.White;
			textBoxAmount.IsComboTextBox = false;
			textBoxAmount.Location = new System.Drawing.Point(103, 130);
			textBoxAmount.Name = "textBoxAmount";
			textBoxAmount.Size = new System.Drawing.Size(132, 20);
			textBoxAmount.TabIndex = 3;
			textBoxAmount.Text = "0.00";
			textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(14, 133);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(46, 13);
			label3.TabIndex = 151;
			label3.Text = "Amount:";
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(594, 251);
			base.Controls.Add(textBoxAmount);
			base.Controls.Add(label3);
			base.Controls.Add(textBoxRetentionName);
			base.Controls.Add(ultraFormattedLinkLabel1);
			base.Controls.Add(comboBoxRetention);
			base.Controls.Add(panelDetails);
			base.Controls.Add(panelButtons);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(465, 215);
			base.Name = "SelectRetentionDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Project Retention";
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxRetention).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
