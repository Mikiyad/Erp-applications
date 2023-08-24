using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class MatrixSelectionForm : Form
	{
		private string matrixProductID = "";

		private DataSet productData;

		private int matrixCount;

		private IContainer components;

		private MatrixProductSelector matrixProductSelector1;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Line linePanelDown;

		private XPButton buttonCancel;

		private Label label1;

		private Label labelItemCode;

		private Label labelDescription;

		private Label label4;

		private Label labelQuantity;

		private Label label6;

		private Label label2;

		private Label label3;

		private Label labelMatrixItem;

		private UltraFormattedLinkLabel labelCustomize;

		private UnitPriceTextBox textBoxUnitPrice;

		private Label labelUnitPrice;

		private CheckBox checkBoxUnitPriceOverWrite;

		private PriceLevelComboBox comboBoxPriceLevel;

		private Label label5;

		public string PriceLevel
		{
			get
			{
				return comboBoxPriceLevel.SelectedID;
			}
			set
			{
				comboBoxPriceLevel.SelectedID = value;
			}
		}

		public bool AllowNegativeQuantity
		{
			get
			{
				return matrixProductSelector1.AllowNegativeQuantity;
			}
			set
			{
				matrixProductSelector1.AllowNegativeQuantity = value;
			}
		}

		public DataSet SelectedItems => productData;

		public MatrixSelectionForm()
		{
			InitializeComponent();
			matrixProductSelector1.Grid.AfterCellActivate += Grid_AfterCellActivate;
			base.Activated += MatrixSelectionForm_Activated;
			base.StartPosition = FormStartPosition.CenterParent;
			textBoxUnitPrice.Visible = true;
			base.Load += MatrixSelectionForm_Load;
			base.FormClosing += MatrixSelectionForm_FormClosing;
		}

		private void MatrixSelectionForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Global.GlobalSettings.SaveFormProperties(this);
		}

		private void MatrixSelectionForm_Load(object sender, EventArgs e)
		{
			Global.GlobalSettings.LoadFormProperties(this);
			comboBoxPriceLevel.LoadData();
			if (comboBoxPriceLevel.SelectedID == "")
			{
				comboBoxPriceLevel.SelectedID = "1";
			}
		}

		private void MatrixSelectionForm_Activated(object sender, EventArgs e)
		{
			int dimensionCount = matrixProductSelector1.DimensionCount;
			try
			{
				switch (dimensionCount)
				{
				case 3:
					if (matrixProductSelector1.Grid.Rows.Count > 0)
					{
						matrixProductSelector1.Grid.Rows[0].Cells[2].Activate();
						matrixProductSelector1.Grid.PerformAction(UltraGridAction.EnterEditMode, shift: false, control: false);
					}
					break;
				case 2:
					if (matrixProductSelector1.Grid.Rows.Count > 0)
					{
						matrixProductSelector1.Grid.Rows[0].Cells[1].Activate();
						matrixProductSelector1.Grid.PerformAction(UltraGridAction.EnterEditMode, shift: false, control: false);
					}
					break;
				case 1:
					if (matrixProductSelector1.Grid.Rows.Count > 0)
					{
						matrixProductSelector1.Grid.Rows[0].Cells[0].Activate();
						matrixProductSelector1.Grid.PerformAction(UltraGridAction.EnterEditMode, shift: false, control: false);
					}
					break;
				}
			}
			catch (Exception)
			{
			}
		}

		private void Grid_AfterCellActivate(object sender, EventArgs e)
		{
			DataRow activeCellInfo = matrixProductSelector1.GetActiveCellInfo();
			if (activeCellInfo == null)
			{
				labelDescription.Text = "";
				labelItemCode.Text = "";
				labelQuantity.Text = "0";
				labelUnitPrice.Text = 0.ToString(Format.UnitPriceFormat);
				return;
			}
			labelItemCode.Text = activeCellInfo["ProductID"].ToString();
			labelDescription.Text = activeCellInfo["Description"].ToString();
			labelQuantity.Text = activeCellInfo["Quantity"].ToString();
			if (comboBoxPriceLevel.SelectedID == "1")
			{
				labelUnitPrice.Text = decimal.Parse(activeCellInfo["UnitPrice2"].ToString()).ToString(Format.UnitPriceFormat);
			}
			else if (comboBoxPriceLevel.SelectedID == "2")
			{
				labelUnitPrice.Text = decimal.Parse(activeCellInfo["UnitPrice3"].ToString()).ToString(Format.UnitPriceFormat);
			}
			else if (comboBoxPriceLevel.SelectedID == "3")
			{
				labelUnitPrice.Text = decimal.Parse(activeCellInfo["MinPrice"].ToString()).ToString(Format.UnitPriceFormat);
			}
			else
			{
				labelUnitPrice.Text = decimal.Parse(activeCellInfo["UnitPrice1"].ToString()).ToString(Format.UnitPriceFormat);
			}
		}

		public void LoadMatrixData(string matrixProductID, string matrixDescription)
		{
			this.matrixProductID = matrixProductID;
			matrixProductSelector1.LoadMatrixData(matrixProductID);
			labelMatrixItem.Text = matrixProductID + " | " + matrixDescription;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			productData = matrixProductSelector1.GetSelectedItems(comboBoxPriceLevel.SelectedID);
			if (checkBoxUnitPriceOverWrite.Checked)
			{
				foreach (DataRow row in productData.Tables[0].Rows)
				{
					row["UnitPrice"] = textBoxUnitPrice.Text;
				}
			}
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void labelCustomize_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			MatrixQuantityForm matrixQuantityForm = new MatrixQuantityForm();
			matrixQuantityForm.LoadData(matrixProductID, matrixProductSelector1.ProductName);
			matrixQuantityForm.ShowDialog(this);
		}

		private void checkBoxUnitPriceOverWrite_CheckedChanged(object sender, EventArgs e)
		{
			textBoxUnitPrice.Enabled = checkBoxUnitPriceOverWrite.Checked;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.MatrixSelectionForm));
			panelButtons = new System.Windows.Forms.Panel();
			labelCustomize = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			label1 = new System.Windows.Forms.Label();
			labelItemCode = new System.Windows.Forms.Label();
			labelDescription = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			labelQuantity = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			labelMatrixItem = new System.Windows.Forms.Label();
			textBoxUnitPrice = new Micromind.UISupport.UnitPriceTextBox();
			labelUnitPrice = new System.Windows.Forms.Label();
			checkBoxUnitPriceOverWrite = new System.Windows.Forms.CheckBox();
			label5 = new System.Windows.Forms.Label();
			comboBoxPriceLevel = new Micromind.DataControls.PriceLevelComboBox();
			matrixProductSelector1 = new Micromind.DataControls.MatrixProductSelector();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPriceLevel).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(labelCustomize);
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 328);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(587, 40);
			panelButtons.TabIndex = 3;
			appearance.ForeColor = System.Drawing.Color.FromArgb(102, 165, 221);
			labelCustomize.Appearance = appearance;
			labelCustomize.AutoSize = true;
			appearance2.ForeColor = System.Drawing.Color.FromArgb(26, 88, 134);
			labelCustomize.LinkAppearance = appearance2;
			labelCustomize.Location = new System.Drawing.Point(13, 14);
			labelCustomize.Name = "labelCustomize";
			labelCustomize.Size = new System.Drawing.Size(120, 14);
			labelCustomize.TabIndex = 2;
			labelCustomize.TabStop = true;
			labelCustomize.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelCustomize.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelCustomize.Value = "View Available Quantity";
			appearance3.ForeColor = System.Drawing.Color.Blue;
			labelCustomize.VisitedLinkAppearance = appearance3;
			labelCustomize.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelCustomize_LinkClicked);
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(375, 8);
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
			linePanelDown.Size = new System.Drawing.Size(587, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(477, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 1;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			label1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(429, 16);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(69, 13);
			label1.TabIndex = 5;
			label1.Text = "Item Code:";
			labelItemCode.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			labelItemCode.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelItemCode.Location = new System.Drawing.Point(429, 31);
			labelItemCode.Name = "labelItemCode";
			labelItemCode.Size = new System.Drawing.Size(158, 35);
			labelItemCode.TabIndex = 5;
			labelDescription.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			labelDescription.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelDescription.Location = new System.Drawing.Point(427, 81);
			labelDescription.Name = "labelDescription";
			labelDescription.Size = new System.Drawing.Size(158, 35);
			labelDescription.TabIndex = 6;
			label4.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(427, 66);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(74, 13);
			label4.TabIndex = 7;
			label4.Text = "Description:";
			labelQuantity.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			labelQuantity.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelQuantity.Location = new System.Drawing.Point(506, 124);
			labelQuantity.Name = "labelQuantity";
			labelQuantity.Size = new System.Drawing.Size(71, 16);
			labelQuantity.TabIndex = 8;
			labelQuantity.Text = "0";
			labelQuantity.TextAlign = System.Drawing.ContentAlignment.TopRight;
			label6.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(427, 124);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(78, 13);
			label6.TabIndex = 9;
			label6.Text = "QTY Onhand:";
			label2.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(430, 202);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(64, 13);
			label2.TabIndex = 9;
			label2.Text = "Unit Price:";
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(12, 9);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(81, 13);
			label3.TabIndex = 10;
			label3.Text = "Matrix Item: ";
			labelMatrixItem.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelMatrixItem.Location = new System.Drawing.Point(90, 8);
			labelMatrixItem.Name = "labelMatrixItem";
			labelMatrixItem.Size = new System.Drawing.Size(369, 19);
			labelMatrixItem.TabIndex = 11;
			textBoxUnitPrice.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			textBoxUnitPrice.Enabled = false;
			textBoxUnitPrice.IsComboTextBox = false;
			textBoxUnitPrice.Location = new System.Drawing.Point(447, 261);
			textBoxUnitPrice.Name = "textBoxUnitPrice";
			textBoxUnitPrice.Size = new System.Drawing.Size(131, 20);
			textBoxUnitPrice.TabIndex = 2;
			textBoxUnitPrice.Text = "0";
			textBoxUnitPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxUnitPrice.Visible = false;
			labelUnitPrice.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			labelUnitPrice.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelUnitPrice.Location = new System.Drawing.Point(509, 203);
			labelUnitPrice.Name = "labelUnitPrice";
			labelUnitPrice.Size = new System.Drawing.Size(71, 16);
			labelUnitPrice.TabIndex = 12;
			labelUnitPrice.Text = "0";
			labelUnitPrice.TextAlign = System.Drawing.ContentAlignment.TopRight;
			checkBoxUnitPriceOverWrite.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			checkBoxUnitPriceOverWrite.AutoSize = true;
			checkBoxUnitPriceOverWrite.Location = new System.Drawing.Point(432, 241);
			checkBoxUnitPriceOverWrite.Name = "checkBoxUnitPriceOverWrite";
			checkBoxUnitPriceOverWrite.Size = new System.Drawing.Size(123, 17);
			checkBoxUnitPriceOverWrite.TabIndex = 1;
			checkBoxUnitPriceOverWrite.Text = "Overwrite Unit Price:";
			checkBoxUnitPriceOverWrite.UseVisualStyleBackColor = true;
			checkBoxUnitPriceOverWrite.CheckedChanged += new System.EventHandler(checkBoxUnitPriceOverWrite_CheckedChanged);
			label5.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(430, 162);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(61, 13);
			label5.TabIndex = 14;
			label5.Text = "Price List:";
			comboBoxPriceLevel.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			comboBoxPriceLevel.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPriceLevel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPriceLevel.DescriptionTextBox = null;
			appearance4.BackColor = System.Drawing.SystemColors.Window;
			appearance4.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPriceLevel.DisplayLayout.Appearance = appearance4;
			comboBoxPriceLevel.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPriceLevel.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance5.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance5.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance5.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPriceLevel.DisplayLayout.GroupByBox.Appearance = appearance5;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPriceLevel.DisplayLayout.GroupByBox.BandLabelAppearance = appearance6;
			comboBoxPriceLevel.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance7.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance7.BackColor2 = System.Drawing.SystemColors.Control;
			appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPriceLevel.DisplayLayout.GroupByBox.PromptAppearance = appearance7;
			comboBoxPriceLevel.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPriceLevel.DisplayLayout.MaxRowScrollRegions = 1;
			appearance8.BackColor = System.Drawing.SystemColors.Window;
			appearance8.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPriceLevel.DisplayLayout.Override.ActiveCellAppearance = appearance8;
			appearance9.BackColor = System.Drawing.SystemColors.Highlight;
			appearance9.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPriceLevel.DisplayLayout.Override.ActiveRowAppearance = appearance9;
			comboBoxPriceLevel.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPriceLevel.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance10.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPriceLevel.DisplayLayout.Override.CardAreaAppearance = appearance10;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			appearance11.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPriceLevel.DisplayLayout.Override.CellAppearance = appearance11;
			comboBoxPriceLevel.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPriceLevel.DisplayLayout.Override.CellPadding = 0;
			appearance12.BackColor = System.Drawing.SystemColors.Control;
			appearance12.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance12.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance12.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPriceLevel.DisplayLayout.Override.GroupByRowAppearance = appearance12;
			appearance13.TextHAlignAsString = "Left";
			comboBoxPriceLevel.DisplayLayout.Override.HeaderAppearance = appearance13;
			comboBoxPriceLevel.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPriceLevel.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance14.BackColor = System.Drawing.SystemColors.Window;
			appearance14.BorderColor = System.Drawing.Color.Silver;
			comboBoxPriceLevel.DisplayLayout.Override.RowAppearance = appearance14;
			comboBoxPriceLevel.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance15.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPriceLevel.DisplayLayout.Override.TemplateAddRowAppearance = appearance15;
			comboBoxPriceLevel.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPriceLevel.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPriceLevel.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPriceLevel.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPriceLevel.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxPriceLevel.Editable = true;
			comboBoxPriceLevel.FilterString = "";
			comboBoxPriceLevel.HasAllAccount = false;
			comboBoxPriceLevel.HasCustom = false;
			comboBoxPriceLevel.Location = new System.Drawing.Point(430, 178);
			comboBoxPriceLevel.MaxDropDownItems = 12;
			comboBoxPriceLevel.Name = "comboBoxPriceLevel";
			comboBoxPriceLevel.ShowInactiveItems = false;
			comboBoxPriceLevel.ShowQuickAdd = true;
			comboBoxPriceLevel.Size = new System.Drawing.Size(151, 20);
			comboBoxPriceLevel.TabIndex = 13;
			comboBoxPriceLevel.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			matrixProductSelector1.AllowNegativeQuantity = true;
			matrixProductSelector1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			matrixProductSelector1.Location = new System.Drawing.Point(12, 31);
			matrixProductSelector1.MatrixData = null;
			matrixProductSelector1.Name = "matrixProductSelector1";
			matrixProductSelector1.Size = new System.Drawing.Size(409, 291);
			matrixProductSelector1.TabIndex = 0;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(587, 368);
			base.Controls.Add(label5);
			base.Controls.Add(comboBoxPriceLevel);
			base.Controls.Add(panelButtons);
			base.Controls.Add(checkBoxUnitPriceOverWrite);
			base.Controls.Add(labelUnitPrice);
			base.Controls.Add(textBoxUnitPrice);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(labelQuantity);
			base.Controls.Add(label6);
			base.Controls.Add(labelDescription);
			base.Controls.Add(label4);
			base.Controls.Add(labelItemCode);
			base.Controls.Add(label1);
			base.Controls.Add(matrixProductSelector1);
			base.Controls.Add(labelMatrixItem);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MatrixSelectionForm";
			Text = "Matrix Detail";
			panelButtons.ResumeLayout(false);
			panelButtons.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPriceLevel).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
