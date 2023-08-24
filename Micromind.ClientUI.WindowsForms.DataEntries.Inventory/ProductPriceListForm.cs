using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class ProductPriceListForm : Form, IForm
	{
		private DataSet currentData;

		private const string TABLENAME_CONST = "Product";

		private const string IDFIELD_CONST = "ProductID";

		private bool isLoading;

		private bool isFirstTime = true;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private DataGridList dataGrid;

		private XPButton xpButton2;

		private MMLabel mmLabel7;

		private MMTextBox textBoxUOM;

		public ScreenAreas ScreenArea => ScreenAreas.Products;

		public int ScreenID => 4013;

		public ScreenTypes ScreenType => ScreenTypes.Dialog;

		public decimal SelectedPrice
		{
			get
			{
				if (dataGrid.ActiveRow == null)
				{
					return 0m;
				}
				return decimal.Parse(dataGrid.ActiveRow.Cells["Amount"].Value.ToString());
			}
		}

		public ProductPriceListForm()
		{
			InitializeComponent();
			dataGrid.DoubleClickRow += dataGrid_DoubleClickRow;
		}

		private void dataGrid_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
		{
			if (dataGrid.ActiveRow != null)
			{
				base.DialogResult = DialogResult.OK;
			}
			else
			{
				base.DialogResult = DialogResult.Cancel;
			}
		}

		public void LoadData(string productID, string customerID, string LocationID = "", string UnitID = "")
		{
			try
			{
				isLoading = true;
				if (!(productID == ""))
				{
					currentData = Factory.ProductSystem.GetProductPriceList(productID, customerID, LocationID, UnitID);
					FillData();
					dataGrid.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
					if (isFirstTime)
					{
						isFirstTime = false;
						dataGrid.DisplayLayout.Bands[0].Columns[0].Width = checked(dataGrid.Width * 70) / 100;
					}
					dataGrid.ContextMenuStrip = null;
					dataGrid.DisplayLayout.Bands[0].Columns["Amount"].AllowRowSummaries = AllowRowSummaries.False;
					dataGrid.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
					dataGrid.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
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
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice1))
			{
				foreach (DataRow row in currentData.Tables[0].Rows)
				{
					if (row["PriceID"].ToString().ToLower() == "unitprice1")
					{
						row.Delete();
						break;
					}
				}
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice2))
			{
				foreach (DataRow row2 in currentData.Tables[0].Rows)
				{
					if (row2["PriceID"].ToString().ToLower() == "unitprice2")
					{
						row2.Delete();
						break;
					}
				}
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice3))
			{
				foreach (DataRow row3 in currentData.Tables[0].Rows)
				{
					if (row3["PriceID"].ToString().ToLower() == "unitprice3")
					{
						row3.Delete();
						break;
					}
				}
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemMinPrice))
			{
				foreach (DataRow row4 in currentData.Tables[0].Rows)
				{
					if (row4["PriceID"].ToString().ToLower() == "minprice")
					{
						row4.Delete();
						break;
					}
				}
			}
			if (currentData.Tables.Count > 0 && currentData.Tables[0].Rows.Count > 0)
			{
				textBoxUOM.Text = currentData.Tables[0].Rows[0]["UnitID"].ToString();
			}
			currentData.Tables[0].Columns.Remove("PriceID");
			currentData.Tables[0].Columns.Remove("UnitID");
			dataGrid.DataSource = currentData;
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		public void OnActivated()
		{
		}

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
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
		}

		private void xpButton2_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.ProductPriceListForm));
			panelButtons = new System.Windows.Forms.Panel();
			xpButton2 = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			dataGrid = new Micromind.UISupport.DataGridList(components);
			mmLabel7 = new Micromind.UISupport.MMLabel();
			textBoxUOM = new Micromind.UISupport.MMTextBox();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGrid).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(xpButton2);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 235);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(417, 40);
			panelButtons.TabIndex = 4;
			xpButton2.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton2.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton2.BackColor = System.Drawing.Color.DarkGray;
			xpButton2.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton2.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton2.Location = new System.Drawing.Point(211, 8);
			xpButton2.Name = "xpButton2";
			xpButton2.Size = new System.Drawing.Size(96, 24);
			xpButton2.TabIndex = 15;
			xpButton2.Text = "Select";
			xpButton2.UseVisualStyleBackColor = false;
			xpButton2.Click += new System.EventHandler(xpButton2_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(417, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(309, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 0;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			dataGrid.AllowUnfittedView = false;
			dataGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGrid.DisplayLayout.Appearance = appearance;
			dataGrid.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGrid.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGrid.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGrid.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGrid.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGrid.DisplayLayout.MaxColScrollRegions = 1;
			dataGrid.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGrid.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGrid.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGrid.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGrid.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGrid.DisplayLayout.Override.CellAppearance = appearance8;
			dataGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGrid.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGrid.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGrid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGrid.DisplayLayout.Override.RowAppearance = appearance11;
			dataGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGrid.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGrid.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGrid.LoadLayoutFailed = false;
			dataGrid.Location = new System.Drawing.Point(12, 43);
			dataGrid.Name = "dataGrid";
			dataGrid.ShowDeleteMenu = false;
			dataGrid.ShowMinusInRed = true;
			dataGrid.ShowNewMenu = false;
			dataGrid.Size = new System.Drawing.Size(393, 186);
			dataGrid.TabIndex = 3;
			dataGrid.Text = "dataGridList1";
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(12, 16);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(35, 13);
			mmLabel7.TabIndex = 57;
			mmLabel7.Text = "UOM:";
			textBoxUOM.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxUOM.CustomReportFieldName = "";
			textBoxUOM.CustomReportKey = "";
			textBoxUOM.CustomReportValueType = 1;
			textBoxUOM.IsComboTextBox = false;
			textBoxUOM.IsModified = false;
			textBoxUOM.Location = new System.Drawing.Point(53, 12);
			textBoxUOM.MaxLength = 60;
			textBoxUOM.Name = "textBoxUOM";
			textBoxUOM.ReadOnly = true;
			textBoxUOM.Size = new System.Drawing.Size(154, 20);
			textBoxUOM.TabIndex = 58;
			base.AcceptButton = xpButton2;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = xpButton1;
			base.ClientSize = new System.Drawing.Size(417, 275);
			base.Controls.Add(textBoxUOM);
			base.Controls.Add(mmLabel7);
			base.Controls.Add(dataGrid);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(342, 302);
			base.Name = "ProductPriceListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Price List";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(AnalysisGroupDetailsForm_Load);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGrid).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
