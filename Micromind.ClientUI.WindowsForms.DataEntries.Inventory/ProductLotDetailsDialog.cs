using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class ProductLotDetailsDialog : Form, IForm
	{
		private DataSet currentData;

		private const string TABLENAME_CONST = "Product";

		private const string IDFIELD_CONST = "ProductID";

		private bool isLoading;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private FormManager formManager;

		private TextBox textBoxItemCode;

		private MMLabel mmLabel1;

		private DataGridList dataGrid;

		private MMLabel mmLabel2;

		private TextBox textBoxDescription;

		private MMLabel mmLabel3;

		private UltraButton buttonColumnChooser;

		public ScreenAreas ScreenArea => ScreenAreas.Products;

		public int ScreenID => 4014;

		public ScreenTypes ScreenType => ScreenTypes.Dialog;

		public string ProductID
		{
			get
			{
				return textBoxItemCode.Text;
			}
			set
			{
				textBoxItemCode.Text = value;
			}
		}

		public string Description
		{
			get
			{
				return textBoxDescription.Text;
			}
			set
			{
				textBoxDescription.Text = value;
			}
		}

		private bool IsDirty => formManager.GetDirtyStatus();

		public ProductLotDetailsDialog()
		{
			InitializeComponent();
			textBoxItemCode.TextChanged += textBoxItemCode_TextChanged;
			Global.GlobalSettings.LoadFormProperties(this);
			dataGrid.ClickCell += dataGrid_ClickCell;
			base.FormClosing += ProductLotDetailsDialog_FormClosing;
		}

		private void dataGrid_ClickCell(object sender, ClickCellEventArgs e)
		{
		}

		private void editor_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (dataGrid.ActiveRow.IsDataRow && dataGrid.ActiveCell.Column.Key == "LotNumber")
			{
				string lotID = dataGrid.ActiveRow.Cells["LotNumber"].Value.ToString();
				ProductLotDrillDialog productLotDrillDialog = new ProductLotDrillDialog();
				productLotDrillDialog.LoadData(lotID);
				productLotDrillDialog.ShowDialog(this);
			}
		}

		private void ProductLotDetailsDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				Global.GlobalSettings.SaveFormProperties(this);
				dataGrid.SaveLayout();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void textBoxItemCode_TextChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxUnit_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isLoading)
			{
				LoadData(textBoxItemCode.Text);
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
		}

		public void LoadData(string productID)
		{
			try
			{
				isLoading = true;
				if (CanClose())
				{
					textBoxItemCode.Text = productID;
					string text = Factory.CompanyInformationSystem.GetCompanyInformation().Tables[0].Rows[0]["Reference2"].ToString();
					if (text == "" || text == string.Empty)
					{
						text = "Reference2";
					}
					currentData = Factory.ProductSystem.GetProductLotWiseAvailability(productID);
					FillData();
					dataGrid.LoadLayout();
					dataGrid.ApplyQuantityColumnFormat(dataGrid.DisplayLayout.Bands[0].Columns["Quantity"], addSummary: true);
					dataGrid.ApplyQuantityColumnFormat(dataGrid.DisplayLayout.Bands[0].Columns["Received Qty"], addSummary: true);
					if (text != "Reference2")
					{
						dataGrid.DisplayLayout.Bands[0].Columns["Reference2"].Header.Caption = text;
					}
					else
					{
						dataGrid.DisplayLayout.Bands[0].Columns["Reference2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
						dataGrid.DisplayLayout.Bands[0].Columns["Reference2"].Hidden = true;
					}
					dataGrid.DisplayLayout.Bands[0].Columns["LotNumber"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
					(dataGrid.DisplayLayout.Bands[0].Columns["LotNumber"].Editor as FormattedLinkEditor).LinkClicked += editor_LinkClicked;
					formManager.ResetDirty();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			finally
			{
				isLoading = false;
			}
		}

		private void FillData()
		{
			dataGrid.DataSource = currentData;
		}

		private void ClearForm()
		{
			if (dataGrid.DataSource != null)
			{
				((DataTable)dataGrid.DataSource).Rows.Clear();
			}
			formManager.ResetDirty();
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		public void OnActivated()
		{
		}

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
		}

		private bool CanClose()
		{
			return true;
		}

		private void AnalysisGroupDetailsForm_Load(object sender, EventArgs e)
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

		public void SetupGrid()
		{
			dataGrid.ApplyUIDesign();
			dataGrid.ContextMenuStrip = null;
		}

		private void textBoxItemCode_ValueChanged(object sender, EventArgs e)
		{
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			LoadData(textBoxItemCode.Text);
		}

		private void buttonColumnChooser_Click(object sender, EventArgs e)
		{
			dataGrid.ShowColumnChooser();
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
			components = new System.ComponentModel.Container();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.ProductLotDetailsDialog));
			panelButtons = new System.Windows.Forms.Panel();
			buttonColumnChooser = new Infragistics.Win.Misc.UltraButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			textBoxItemCode = new System.Windows.Forms.TextBox();
			textBoxDescription = new System.Windows.Forms.TextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			dataGrid = new Micromind.UISupport.DataGridList(components);
			mmLabel1 = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGrid).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonColumnChooser);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 326);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(534, 40);
			panelButtons.TabIndex = 4;
			appearance.Image = Micromind.ClientUI.Properties.Resources.column;
			buttonColumnChooser.Appearance = appearance;
			buttonColumnChooser.Location = new System.Drawing.Point(15, 8);
			buttonColumnChooser.Name = "buttonColumnChooser";
			buttonColumnChooser.Size = new System.Drawing.Size(29, 24);
			buttonColumnChooser.TabIndex = 15;
			buttonColumnChooser.Click += new System.EventHandler(buttonColumnChooser_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(534, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(426, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 0;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			textBoxItemCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxItemCode.Location = new System.Drawing.Point(92, 9);
			textBoxItemCode.Name = "textBoxItemCode";
			textBoxItemCode.ReadOnly = true;
			textBoxItemCode.Size = new System.Drawing.Size(428, 20);
			textBoxItemCode.TabIndex = 0;
			textBoxDescription.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDescription.Location = new System.Drawing.Point(92, 31);
			textBoxDescription.Name = "textBoxDescription";
			textBoxDescription.ReadOnly = true;
			textBoxDescription.Size = new System.Drawing.Size(428, 20);
			textBoxDescription.TabIndex = 1;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(12, 32);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(63, 13);
			mmLabel3.TabIndex = 292;
			mmLabel3.Text = "Description:";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(12, 11);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(58, 13);
			mmLabel2.TabIndex = 292;
			mmLabel2.Text = "Item Code:";
			dataGrid.AllowUnfittedView = false;
			dataGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance2.BackColor = System.Drawing.SystemColors.Window;
			appearance2.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGrid.DisplayLayout.Appearance = appearance2;
			dataGrid.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGrid.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance3.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance3.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance3.BorderColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.GroupByBox.Appearance = appearance3;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGrid.DisplayLayout.GroupByBox.BandLabelAppearance = appearance4;
			dataGrid.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance5.BackColor2 = System.Drawing.SystemColors.Control;
			appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGrid.DisplayLayout.GroupByBox.PromptAppearance = appearance5;
			dataGrid.DisplayLayout.MaxColScrollRegions = 1;
			dataGrid.DisplayLayout.MaxRowScrollRegions = 1;
			appearance6.BackColor = System.Drawing.SystemColors.Window;
			appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGrid.DisplayLayout.Override.ActiveCellAppearance = appearance6;
			appearance7.BackColor = System.Drawing.SystemColors.Highlight;
			appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGrid.DisplayLayout.Override.ActiveRowAppearance = appearance7;
			dataGrid.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGrid.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance8.BackColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.Override.CardAreaAppearance = appearance8;
			appearance9.BorderColor = System.Drawing.Color.Silver;
			appearance9.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGrid.DisplayLayout.Override.CellAppearance = appearance9;
			dataGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGrid.DisplayLayout.Override.CellPadding = 0;
			appearance10.BackColor = System.Drawing.SystemColors.Control;
			appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance10.BorderColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.Override.GroupByRowAppearance = appearance10;
			appearance11.TextHAlignAsString = "Left";
			dataGrid.DisplayLayout.Override.HeaderAppearance = appearance11;
			dataGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGrid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance12.BackColor = System.Drawing.SystemColors.Window;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			dataGrid.DisplayLayout.Override.RowAppearance = appearance12;
			dataGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance13.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGrid.DisplayLayout.Override.TemplateAddRowAppearance = appearance13;
			dataGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGrid.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGrid.Location = new System.Drawing.Point(12, 79);
			dataGrid.Name = "dataGrid";
			dataGrid.ShowDeleteMenu = false;
			dataGrid.ShowMinusInRed = true;
			dataGrid.ShowNewMenu = false;
			dataGrid.Size = new System.Drawing.Size(510, 241);
			dataGrid.TabIndex = 3;
			dataGrid.Text = "dataGridList1";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(12, 63);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(76, 13);
			mmLabel1.TabIndex = 22;
			mmLabel1.Text = "Available Lots:";
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(534, 366);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(textBoxDescription);
			base.Controls.Add(textBoxItemCode);
			base.Controls.Add(dataGrid);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(542, 302);
			base.Name = "ProductLotDetailsDialog";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Product Lot Detail";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(AnalysisGroupDetailsForm_Load);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGrid).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
