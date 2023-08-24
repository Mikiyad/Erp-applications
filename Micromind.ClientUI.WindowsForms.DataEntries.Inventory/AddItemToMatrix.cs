using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class AddItemToMatrix : Form
	{
		private ValueList attribute1List;

		private ValueList attribute2List;

		private ValueList attribute3List;

		private IContainer components;

		private XPButton buttonOK;

		private XPButton buttonCancel;

		private ProductComboBox comboBoxProduct;

		private TextBox textBoxProductName;

		private Label label1;

		private ComboBox comboBoxAttribute1;

		private Label labelAttribute1;

		private Label labelAttribute2;

		private ComboBox comboBoxAttribute2;

		private ComboBox comboBoxAttribute3;

		private Label labelAttribute3;

		private Line line1;

		public ValueList Attribute1List
		{
			set
			{
				if (value != null)
				{
					attribute1List = value;
					comboBoxAttribute1.Items.Clear();
					comboBoxAttribute1.Items.AddRange(value.ValueListItems.All);
				}
			}
		}

		public byte DimentionCount
		{
			get;
			set;
		}

		public UltraGrid DataGrid
		{
			get;
			set;
		}

		public string Attribute1 => comboBoxAttribute1.Text;

		public string Attribute2 => comboBoxAttribute2.Text;

		public string Attribute3 => comboBoxAttribute3.Text;

		public string ProductID => comboBoxProduct.SelectedID;

		public ValueList Attribute2List
		{
			set
			{
				if (value == null)
				{
					comboBoxAttribute2.Enabled = false;
					return;
				}
				attribute2List = value;
				comboBoxAttribute2.Items.Clear();
				comboBoxAttribute2.Items.AddRange(value.ValueListItems.All);
			}
		}

		public ValueList Attribute3List
		{
			set
			{
				if (value == null)
				{
					comboBoxAttribute3.Enabled = false;
					return;
				}
				attribute3List = value;
				comboBoxAttribute3.Items.Clear();
				comboBoxAttribute3.Items.AddRange(value.ValueListItems.All);
			}
		}

		public AddItemToMatrix()
		{
			InitializeComponent();
			comboBoxProduct.ShowOnlyMatrixAddableItems = true;
			labelAttribute1.Text = CompanyPreferences.Attribute1Name + ":";
			labelAttribute2.Text = CompanyPreferences.Attribute2Name + ":";
			labelAttribute3.Text = CompanyPreferences.Attribute3Name + ":";
		}

		private void AddItemToMatrix_Load(object sender, EventArgs e)
		{
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private bool ValidateData()
		{
			if (comboBoxAttribute1.Text == "" || (comboBoxAttribute2.Text == "" && DimentionCount >= 2) || (comboBoxAttribute3.Text == "" && DimentionCount >= 3))
			{
				ErrorHelper.InformationMessage("Please select all required attributes.");
				return false;
			}
			foreach (UltraGridRow row in DataGrid.Rows)
			{
				if (row.Cells["Attribute1"].Value.ToString() == comboBoxAttribute1.Text && row.Cells["Attribute2"].Value.ToString() == comboBoxAttribute2.Text && row.Cells["Attribute3"].Value.ToString() == comboBoxAttribute3.Text)
				{
					ErrorHelper.InformationMessage("There is already a row with the same attributes. Please enter different attributes.");
					return false;
				}
			}
			return true;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (ValidateData())
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void dataGridAttributes_Click(object sender, EventArgs e)
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
			buttonOK = new Micromind.UISupport.XPButton();
			buttonCancel = new Micromind.UISupport.XPButton();
			comboBoxProduct = new Micromind.DataControls.ProductComboBox();
			textBoxProductName = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			comboBoxAttribute1 = new System.Windows.Forms.ComboBox();
			labelAttribute1 = new System.Windows.Forms.Label();
			labelAttribute2 = new System.Windows.Forms.Label();
			comboBoxAttribute2 = new System.Windows.Forms.ComboBox();
			comboBoxAttribute3 = new System.Windows.Forms.ComboBox();
			labelAttribute3 = new System.Windows.Forms.Label();
			line1 = new Micromind.UISupport.Line();
			((System.ComponentModel.ISupportInitialize)comboBoxProduct).BeginInit();
			SuspendLayout();
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(335, 183);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 5;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(437, 183);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 6;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			comboBoxProduct.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxProduct.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxProduct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxProduct.CustomReportFieldName = "";
			comboBoxProduct.CustomReportKey = "";
			comboBoxProduct.CustomReportValueType = 1;
			comboBoxProduct.DescriptionTextBox = textBoxProductName;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxProduct.DisplayLayout.Appearance = appearance;
			comboBoxProduct.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxProduct.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProduct.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProduct.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxProduct.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProduct.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxProduct.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxProduct.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxProduct.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxProduct.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxProduct.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxProduct.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxProduct.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxProduct.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxProduct.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxProduct.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProduct.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxProduct.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxProduct.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxProduct.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxProduct.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxProduct.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxProduct.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxProduct.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxProduct.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxProduct.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxProduct.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxProduct.Editable = true;
			comboBoxProduct.FilterString = "";
			comboBoxProduct.HasAllAccount = false;
			comboBoxProduct.HasCustom = false;
			comboBoxProduct.IsDataLoaded = false;
			comboBoxProduct.Location = new System.Drawing.Point(91, 29);
			comboBoxProduct.MaxDropDownItems = 12;
			comboBoxProduct.Name = "comboBoxProduct";
			comboBoxProduct.ShowInactiveItems = false;
			comboBoxProduct.ShowQuickAdd = true;
			comboBoxProduct.Size = new System.Drawing.Size(272, 20);
			comboBoxProduct.TabIndex = 0;
			comboBoxProduct.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxProductName.Location = new System.Drawing.Point(91, 50);
			textBoxProductName.Name = "textBoxProductName";
			textBoxProductName.ReadOnly = true;
			textBoxProductName.Size = new System.Drawing.Size(442, 20);
			textBoxProductName.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 33);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(30, 13);
			label1.TabIndex = 9;
			label1.Text = "Item:";
			comboBoxAttribute1.FormattingEnabled = true;
			comboBoxAttribute1.Location = new System.Drawing.Point(91, 86);
			comboBoxAttribute1.Name = "comboBoxAttribute1";
			comboBoxAttribute1.Size = new System.Drawing.Size(121, 21);
			comboBoxAttribute1.TabIndex = 2;
			labelAttribute1.AutoSize = true;
			labelAttribute1.Location = new System.Drawing.Point(12, 89);
			labelAttribute1.Name = "labelAttribute1";
			labelAttribute1.Size = new System.Drawing.Size(58, 13);
			labelAttribute1.TabIndex = 11;
			labelAttribute1.Text = "Attribute 1:";
			labelAttribute2.AutoSize = true;
			labelAttribute2.Location = new System.Drawing.Point(12, 115);
			labelAttribute2.Name = "labelAttribute2";
			labelAttribute2.Size = new System.Drawing.Size(58, 13);
			labelAttribute2.TabIndex = 13;
			labelAttribute2.Text = "Attribute 2:";
			comboBoxAttribute2.FormattingEnabled = true;
			comboBoxAttribute2.Location = new System.Drawing.Point(91, 112);
			comboBoxAttribute2.Name = "comboBoxAttribute2";
			comboBoxAttribute2.Size = new System.Drawing.Size(121, 21);
			comboBoxAttribute2.TabIndex = 3;
			comboBoxAttribute3.FormattingEnabled = true;
			comboBoxAttribute3.Location = new System.Drawing.Point(91, 137);
			comboBoxAttribute3.Name = "comboBoxAttribute3";
			comboBoxAttribute3.Size = new System.Drawing.Size(121, 21);
			comboBoxAttribute3.TabIndex = 4;
			labelAttribute3.AutoSize = true;
			labelAttribute3.Location = new System.Drawing.Point(12, 140);
			labelAttribute3.Name = "labelAttribute3";
			labelAttribute3.Size = new System.Drawing.Size(58, 13);
			labelAttribute3.TabIndex = 13;
			labelAttribute3.Text = "Attribute 3:";
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(-5, 174);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(603, 1);
			line1.TabIndex = 14;
			line1.TabStop = false;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(548, 215);
			base.Controls.Add(line1);
			base.Controls.Add(labelAttribute3);
			base.Controls.Add(comboBoxAttribute3);
			base.Controls.Add(labelAttribute2);
			base.Controls.Add(comboBoxAttribute2);
			base.Controls.Add(labelAttribute1);
			base.Controls.Add(comboBoxAttribute1);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxProductName);
			base.Controls.Add(comboBoxProduct);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AddItemToMatrix";
			Text = "Add Item to  Matrix";
			base.Load += new System.EventHandler(AddItemToMatrix_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxProduct).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
