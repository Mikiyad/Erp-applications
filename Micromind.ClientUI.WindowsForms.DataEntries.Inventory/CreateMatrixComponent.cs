using Infragistics.Win;
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
	public class CreateMatrixComponent : Form
	{
		private DataSet productData = new DataSet();

		private int dimensionCount = 3;

		public string Dimension1Name = "";

		public string Dimension2Name = "";

		public string Dimension3Name = "";

		public bool CreateAll = true;

		public DataSet SelectedData;

		public bool IsNewRecord = true;

		private UltraGrid dataGridDimension1;

		private UltraGrid dataGridDimension2;

		private UltraGrid dataGridDimension3;

		private IContainer components;

		private XPButton buttonOK;

		private XPButton buttonCancel;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonSelected;

		private DataEntryGrid dataGridAttributes;

		public UltraGrid ComponentsGrid => dataGridAttributes;

		public UltraGrid DimensionGrid1
		{
			get
			{
				return dataGridDimension1;
			}
			set
			{
				dataGridDimension1 = value;
			}
		}

		public UltraGrid DimensionGrid2
		{
			get
			{
				return dataGridDimension2;
			}
			set
			{
				dataGridDimension2 = value;
			}
		}

		public UltraGrid DimensionGrid3
		{
			get
			{
				return dataGridDimension3;
			}
			set
			{
				dataGridDimension3 = value;
			}
		}

		public CreateMatrixComponent()
		{
			InitializeComponent();
			radioButtonSelected.Checked = bool.Parse(Global.CompanySettings.GetSetting("MatrixCOM", false).ToString());
		}

		private void CreateMatrixComponent_Load(object sender, EventArgs e)
		{
			try
			{
				SetupGrid();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataGridAttributes.SetupUI(2);
				DataTable dataTable = productData.Tables.Add("Product");
				dataTable.Columns.Add("Attribute1");
				dataTable.Columns.Add("Attribute1Name");
				dataTable.Columns.Add("Attribute2");
				dataTable.Columns.Add("Attribute2Name");
				dataTable.Columns.Add("Attribute3");
				dataTable.Columns.Add("Attribute3Name");
				dataTable.Columns.Add("Check", typeof(bool)).DefaultValue = false;
				CreateComponents();
				dataGridAttributes.DataSource = dataTable;
				if (dataGridDimension3.Rows.Count == 0)
				{
					dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute3"].Hidden = true;
					dimensionCount = 2;
				}
				if (dataGridDimension2.Rows.Count == 0)
				{
					dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute2"].Hidden = true;
					dimensionCount = 1;
				}
				dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute1"].MergedCellStyle = MergedCellStyle.Always;
				dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute2"].MergedCellStyle = MergedCellStyle.Always;
				UltraGridColumn ultraGridColumn = dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute1"];
				UltraGridColumn ultraGridColumn2 = dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute2"];
				Activation activation2 = dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute3"].CellActivation = Activation.NoEdit;
				Activation activation5 = ultraGridColumn.CellActivation = (ultraGridColumn2.CellActivation = activation2);
				UltraGridColumn ultraGridColumn3 = dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute1"];
				UltraGridColumn ultraGridColumn4 = dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute2"];
				CellClickAction cellClickAction2 = dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute3"].CellClickAction = CellClickAction.RowSelect;
				CellClickAction cellClickAction5 = ultraGridColumn3.CellClickAction = (ultraGridColumn4.CellClickAction = cellClickAction2);
				AppearanceBase cellAppearance = dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute1"].CellAppearance;
				AppearanceBase cellAppearance2 = dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute2"].CellAppearance;
				Color color = dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute3"].CellAppearance.BackColor = Color.WhiteSmoke;
				Color color4 = cellAppearance.BackColor = (cellAppearance2.BackColor = color);
				dataGridAttributes.AllowAddNew = false;
				if (Dimension1Name != string.Empty)
				{
					dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute1"].Header.Caption = Dimension1Name;
				}
				if (Dimension2Name != string.Empty)
				{
					dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute2"].Header.Caption = Dimension2Name;
				}
				if (Dimension3Name != string.Empty)
				{
					dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute3"].Header.Caption = Dimension3Name;
				}
				UltraGridColumn ultraGridColumn5 = dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute1Name"];
				UltraGridColumn ultraGridColumn6 = dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute2Name"];
				bool flag2 = dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute3Name"].Hidden = true;
				bool hidden = ultraGridColumn6.Hidden = flag2;
				ultraGridColumn5.Hidden = hidden;
				CheckSelectedItems();
			}
			catch (Exception e)
			{
				dataGridAttributes.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void CheckSelectedItems()
		{
			if (SelectedData != null)
			{
				foreach (UltraGridRow row in dataGridAttributes.Rows)
				{
					string text = "Attribute1 = '" + row.Cells["Attribute1"].Value.ToString() + "'";
					if (dimensionCount > 1)
					{
						text = text + " AND Attribute2 = '" + row.Cells["Attribute2"].Value.ToString() + "'";
					}
					if (dimensionCount > 2)
					{
						text = text + " AND Attribute3 = '" + row.Cells["Attribute3"].Value.ToString() + "'";
					}
					if (SelectedData.Tables[0].Select(text).Length != 0)
					{
						row.Cells["Check"].Value = true;
						if (!IsNewRecord)
						{
							row.Cells["Check"].Activation = Activation.NoEdit;
							row.Cells["Check"].Appearance.BackColor = Color.WhiteSmoke;
						}
					}
				}
			}
		}

		private void CreateComponents()
		{
			foreach (UltraGridRow row in dataGridDimension1.Rows)
			{
				if (dataGridDimension2.Rows.Count > 0)
				{
					foreach (UltraGridRow row2 in dataGridDimension2.Rows)
					{
						if (dataGridDimension3.Rows.Count > 0)
						{
							foreach (UltraGridRow row3 in dataGridDimension3.Rows)
							{
								AddComponentRow(row, row2, row3);
							}
						}
						else
						{
							AddComponentRow(row, row2, null);
						}
					}
				}
				else
				{
					AddComponentRow(row, null, null);
				}
			}
		}

		private void AddComponentRow(UltraGridRow atr1, UltraGridRow atr2, UltraGridRow atr3)
		{
			DataRow dataRow = productData.Tables[0].NewRow();
			dataRow["Attribute1"] = atr1.Cells["AttributeID"].Value.ToString();
			dataRow["Attribute1Name"] = atr1.Cells["AttributeName"].Value.ToString();
			if (atr2 != null)
			{
				dataRow["Attribute2"] = atr2.Cells["AttributeID"].Value.ToString();
				dataRow["Attribute2Name"] = atr2.Cells["AttributeName"].Value.ToString();
			}
			if (atr3 != null)
			{
				dataRow["Attribute3"] = atr3.Cells["AttributeID"].Value.ToString();
				dataRow["Attribute3Name"] = atr3.Cells["AttributeName"].Value.ToString();
			}
			dataRow.EndEdit();
			productData.Tables[0].Rows.Add(dataRow);
		}

		private void radioButtonSelected_CheckedChanged(object sender, EventArgs e)
		{
			dataGridAttributes.Enabled = radioButtonSelected.Checked;
			Global.CompanySettings.SaveSetting("MatrixCOM", radioButtonSelected.Checked);
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (radioButtonAll.Checked)
			{
				CreateAll = true;
			}
			else
			{
				CreateAll = false;
			}
			base.DialogResult = DialogResult.OK;
			Close();
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
			radioButtonAll = new System.Windows.Forms.RadioButton();
			radioButtonSelected = new System.Windows.Forms.RadioButton();
			dataGridAttributes = new Micromind.DataControls.DataEntryGrid();
			((System.ComponentModel.ISupportInitialize)dataGridAttributes).BeginInit();
			SuspendLayout();
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(335, 308);
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
			buttonCancel.Location = new System.Drawing.Point(437, 308);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 6;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			radioButtonAll.AutoSize = true;
			radioButtonAll.Checked = true;
			radioButtonAll.Location = new System.Drawing.Point(12, 12);
			radioButtonAll.Name = "radioButtonAll";
			radioButtonAll.Size = new System.Drawing.Size(171, 17);
			radioButtonAll.TabIndex = 7;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "Create all available alternatives";
			radioButtonAll.UseVisualStyleBackColor = true;
			radioButtonSelected.AutoSize = true;
			radioButtonSelected.Location = new System.Drawing.Point(12, 35);
			radioButtonSelected.Name = "radioButtonSelected";
			radioButtonSelected.Size = new System.Drawing.Size(146, 17);
			radioButtonSelected.TabIndex = 7;
			radioButtonSelected.Text = "Create Selected Products";
			radioButtonSelected.UseVisualStyleBackColor = true;
			radioButtonSelected.CheckedChanged += new System.EventHandler(radioButtonSelected_CheckedChanged);
			dataGridAttributes.AllowAddNew = false;
			dataGridAttributes.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridAttributes.DisplayLayout.Appearance = appearance;
			dataGridAttributes.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridAttributes.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridAttributes.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridAttributes.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridAttributes.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridAttributes.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridAttributes.DisplayLayout.MaxColScrollRegions = 1;
			dataGridAttributes.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridAttributes.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridAttributes.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridAttributes.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridAttributes.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridAttributes.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridAttributes.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridAttributes.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridAttributes.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridAttributes.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridAttributes.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridAttributes.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridAttributes.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridAttributes.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridAttributes.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridAttributes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridAttributes.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridAttributes.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridAttributes.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridAttributes.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridAttributes.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridAttributes.Enabled = false;
			dataGridAttributes.ExitEditModeOnLeave = false;
			dataGridAttributes.Location = new System.Drawing.Point(12, 58);
			dataGridAttributes.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridAttributes.Name = "dataGridAttributes";
			dataGridAttributes.ShowDeleteMenu = true;
			dataGridAttributes.Size = new System.Drawing.Size(521, 244);
			dataGridAttributes.TabIndex = 19;
			dataGridAttributes.Text = "dataEntryGrid1";
			dataGridAttributes.Click += new System.EventHandler(dataGridAttributes_Click);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(555, 341);
			base.Controls.Add(dataGridAttributes);
			base.Controls.Add(radioButtonSelected);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CreateMatrixComponent";
			Text = "Create Matrix Components";
			base.Load += new System.EventHandler(CreateMatrixComponent_Load);
			((System.ComponentModel.ISupportInitialize)dataGridAttributes).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
