using Infragistics.Win;
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
	public class ProductQuantityForm : Form, IForm
	{
		private DataSet currentData;

		private const string TABLENAME_CONST = "Product";

		private const string IDFIELD_CONST = "ProductID";

		private bool isLoading;

		private bool isFirstTime = true;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private TextBox textBoxItemCode;

		private MMLabel mmLabel1;

		private DataGridList dataGrid;

		private MMLabel mmLabel2;

		private TextBox textBoxDescription;

		private MMLabel mmLabel3;

		private ProductUnitComboBox comboBoxUnit;

		private MMLabel mmLabel4;

		private ToolStripButton toolStripButton1;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonInformation;

		private TextBox textBoxUnitDescription;

		private MMLabel labelFactor;

		private MMLabel mmLabelReserved;

		private MMLabel mmLabelReservedQty;

		public ScreenAreas ScreenArea => ScreenAreas.Products;

		public int ScreenID => 4014;

		public ScreenTypes ScreenType => ScreenTypes.Dialog;

		private bool IsDirty => formManager.GetDirtyStatus();

		public ProductQuantityForm()
		{
			InitializeComponent();
			comboBoxUnit.SelectedIndexChanged += comboBoxUnit_SelectedIndexChanged;
			textBoxItemCode.TextChanged += textBoxItemCode_TextChanged;
		}

		private void textBoxItemCode_TextChanged(object sender, EventArgs e)
		{
			object fieldValue = Factory.DatabaseSystem.GetFieldValue("Product", "Description", "ProductID", textBoxItemCode.Text);
			if (fieldValue != null)
			{
				textBoxDescription.Text = fieldValue.ToString();
			}
			else
			{
				textBoxDescription.Clear();
			}
			comboBoxUnit.LoadData();
			fieldValue = Factory.DatabaseSystem.GetFieldValue("Product", "UnitID", "ProductID", textBoxItemCode.Text);
			if (fieldValue != null)
			{
				comboBoxUnit.SelectedID = fieldValue.ToString();
			}
			else
			{
				comboBoxUnit.Clear();
			}
			if (string.IsNullOrEmpty(comboBoxUnit.SelectedID))
			{
				return;
			}
			string text = "";
			string text2 = "";
			fieldValue = Factory.DatabaseSystem.GetFieldValue("Product_Unit", "Factor", "ProductID", textBoxItemCode.Text);
			text = ((fieldValue == null) ? "" : fieldValue.ToString());
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			fieldValue = Factory.DatabaseSystem.GetFieldValue("Product_Unit", "UnitID", "ProductID", textBoxItemCode.Text);
			text2 = fieldValue.ToString();
			fieldValue = Factory.DatabaseSystem.GetFieldValue("Product_Unit", "FactorType", "ProductID", textBoxItemCode.Text);
			if (fieldValue != null)
			{
				fieldValue.ToString();
				if (fieldValue.ToString() == "M")
				{
					textBoxUnitDescription.Text = "1 " + comboBoxUnit.SelectedID + " = " + text + " " + text2;
				}
				else
				{
					textBoxUnitDescription.Text = "1 " + text2 + " = " + text + " " + comboBoxUnit.SelectedID;
				}
				if (!string.IsNullOrEmpty(textBoxUnitDescription.Text))
				{
					labelFactor.Visible = true;
					textBoxUnitDescription.Visible = true;
				}
			}
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
					comboBoxUnit.ApplyFilter(textBoxItemCode.Text);
					currentData = Factory.ProductSystem.GetProductAvailability(productID, "");
					FillData();
					if (currentData.Tables.Contains("ProductReserverd"))
					{
						mmLabelReservedQty.Text = currentData.Tables["ProductReserverd"].Rows[0]["TotalReserved"].ToString();
						mmLabelReserved.Visible = true;
						mmLabelReservedQty.Visible = true;
					}
					dataGrid.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
					if (isFirstTime)
					{
						isFirstTime = false;
						dataGrid.DisplayLayout.Bands[0].Columns["Location Code"].Width = 60;
					}
					if (!dataGrid.DisplayLayout.Bands[0].Summaries.Exists("Onhand"))
					{
						dataGrid.DisplayLayout.Bands[0].Summaries.Add("Onhand", SummaryType.Sum, dataGrid.DisplayLayout.Bands[0].Columns["Onhand"], SummaryPosition.UseSummaryPositionColumn).Appearance.TextHAlign = HAlign.Right;
					}
					dataGrid.ContextMenuStrip = null;
					dataGrid.DisplayLayout.Bands[0].Columns["Onhand"].AllowRowSummaries = AllowRowSummaries.False;
					dataGrid.DisplayLayout.Bands[0].Columns["Onhand"].CellAppearance.TextHAlign = HAlign.Right;
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

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			string nextID = DatabaseHelper.GetNextID("Product", "ProductID", textBoxItemCode.Text);
			if (!(nextID == ""))
			{
				textBoxItemCode.Text = nextID;
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Product", "ProductID", textBoxItemCode.Text);
			if (!(previousID == ""))
			{
				textBoxItemCode.Text = previousID;
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Product", "ProductID");
			if (!(lastID == ""))
			{
				textBoxItemCode.Text = lastID;
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Product", "ProductID");
			if (!(firstID == ""))
			{
				textBoxItemCode.Text = firstID;
				LoadData(firstID);
			}
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			Find();
		}

		private void Find()
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Product", "ProductID", toolStripTextBoxFind.Text.Trim()))
				{
					textBoxItemCode.Text = toolStripTextBoxFind.Text.Trim();
					LoadData(textBoxItemCode.Text);
				}
				else
				{
					ErrorHelper.InformationMessage("Item not found.");
					toolStripTextBoxFind.SelectAll();
					toolStripTextBoxFind.Focus();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
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

		private void toolStripTextBoxFind_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				Find();
			}
		}

		private void ProductQuantityForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.ProductQuantityForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			textBoxItemCode = new System.Windows.Forms.TextBox();
			textBoxDescription = new System.Windows.Forms.TextBox();
			comboBoxUnit = new Micromind.DataControls.ProductUnitComboBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			dataGrid = new Micromind.UISupport.DataGridList(components);
			mmLabel1 = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			textBoxUnitDescription = new System.Windows.Forms.TextBox();
			labelFactor = new Micromind.UISupport.MMLabel();
			mmLabelReserved = new Micromind.UISupport.MMLabel();
			mmLabelReservedQty = new Micromind.UISupport.MMLabel();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxUnit).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGrid).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButton1,
				toolStripSeparator3,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(534, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.Refresh;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(28, 28);
			toolStripButton1.Text = "Refresh";
			toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripTextBoxFind.KeyDown += new System.Windows.Forms.KeyEventHandler(toolStripTextBoxFind_KeyDown);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 365);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(534, 40);
			panelButtons.TabIndex = 4;
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
			textBoxItemCode.Location = new System.Drawing.Point(92, 30);
			textBoxItemCode.Name = "textBoxItemCode";
			textBoxItemCode.ReadOnly = true;
			textBoxItemCode.Size = new System.Drawing.Size(428, 20);
			textBoxItemCode.TabIndex = 0;
			textBoxDescription.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDescription.Location = new System.Drawing.Point(92, 52);
			textBoxDescription.Name = "textBoxDescription";
			textBoxDescription.ReadOnly = true;
			textBoxDescription.Size = new System.Drawing.Size(428, 20);
			textBoxDescription.TabIndex = 1;
			comboBoxUnit.Assigned = false;
			comboBoxUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxUnit.CustomReportFieldName = "";
			comboBoxUnit.CustomReportKey = "";
			comboBoxUnit.CustomReportValueType = 1;
			comboBoxUnit.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxUnit.DisplayLayout.Appearance = appearance;
			comboBoxUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxUnit.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxUnit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxUnit.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxUnit.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxUnit.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxUnit.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxUnit.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxUnit.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxUnit.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxUnit.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxUnit.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxUnit.Editable = true;
			comboBoxUnit.Enabled = false;
			comboBoxUnit.FilterString = "";
			comboBoxUnit.IsDataLoaded = false;
			comboBoxUnit.Location = new System.Drawing.Point(92, 74);
			comboBoxUnit.MaxDropDownItems = 12;
			comboBoxUnit.Name = "comboBoxUnit";
			comboBoxUnit.Size = new System.Drawing.Size(130, 20);
			comboBoxUnit.TabIndex = 2;
			comboBoxUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(12, 77);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(60, 13);
			mmLabel4.TabIndex = 292;
			mmLabel4.Text = "Stock Unit:";
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(12, 53);
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
			mmLabel2.Location = new System.Drawing.Point(12, 32);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(58, 13);
			mmLabel2.TabIndex = 292;
			mmLabel2.Text = "Item Code:";
			dataGrid.AllowUnfittedView = false;
			dataGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGrid.DisplayLayout.Appearance = appearance13;
			dataGrid.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGrid.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGrid.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			dataGrid.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGrid.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			dataGrid.DisplayLayout.MaxColScrollRegions = 1;
			dataGrid.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGrid.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGrid.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			dataGrid.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGrid.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGrid.DisplayLayout.Override.CellAppearance = appearance20;
			dataGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGrid.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			dataGrid.DisplayLayout.Override.HeaderAppearance = appearance22;
			dataGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGrid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			dataGrid.DisplayLayout.Override.RowAppearance = appearance23;
			dataGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGrid.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			dataGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGrid.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGrid.LoadLayoutFailed = false;
			dataGrid.Location = new System.Drawing.Point(12, 118);
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
			mmLabel1.Location = new System.Drawing.Point(12, 102);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(89, 13);
			mmLabel1.TabIndex = 22;
			mmLabel1.Text = "Stock availability:";
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 31);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			textBoxUnitDescription.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxUnitDescription.Location = new System.Drawing.Point(305, 75);
			textBoxUnitDescription.Name = "textBoxUnitDescription";
			textBoxUnitDescription.ReadOnly = true;
			textBoxUnitDescription.Size = new System.Drawing.Size(215, 20);
			textBoxUnitDescription.TabIndex = 305;
			textBoxUnitDescription.Visible = false;
			labelFactor.AutoSize = true;
			labelFactor.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelFactor.IsFieldHeader = false;
			labelFactor.IsRequired = false;
			labelFactor.Location = new System.Drawing.Point(239, 78);
			labelFactor.Name = "labelFactor";
			labelFactor.PenWidth = 1f;
			labelFactor.ShowBorder = false;
			labelFactor.Size = new System.Drawing.Size(40, 13);
			labelFactor.TabIndex = 306;
			labelFactor.Text = "Factor:";
			labelFactor.Visible = false;
			mmLabelReserved.AutoSize = true;
			mmLabelReserved.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabelReserved.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabelReserved.ForeColor = System.Drawing.Color.OrangeRed;
			mmLabelReserved.IsFieldHeader = false;
			mmLabelReserved.IsRequired = false;
			mmLabelReserved.Location = new System.Drawing.Point(367, 102);
			mmLabelReserved.Name = "mmLabelReserved";
			mmLabelReserved.PenWidth = 1f;
			mmLabelReserved.ShowBorder = false;
			mmLabelReserved.Size = new System.Drawing.Size(87, 13);
			mmLabelReserved.TabIndex = 307;
			mmLabelReserved.Text = "Stock Reserved:";
			mmLabelReserved.Visible = false;
			mmLabelReservedQty.AutoSize = true;
			mmLabelReservedQty.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabelReservedQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabelReservedQty.ForeColor = System.Drawing.Color.OrangeRed;
			mmLabelReservedQty.IsFieldHeader = false;
			mmLabelReservedQty.IsRequired = false;
			mmLabelReservedQty.Location = new System.Drawing.Point(460, 102);
			mmLabelReservedQty.Name = "mmLabelReservedQty";
			mmLabelReservedQty.PenWidth = 1f;
			mmLabelReservedQty.ShowBorder = false;
			mmLabelReservedQty.Size = new System.Drawing.Size(60, 13);
			mmLabelReservedQty.TabIndex = 308;
			mmLabelReservedQty.Text = "Stock Unit:";
			mmLabelReservedQty.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(534, 405);
			base.Controls.Add(mmLabelReservedQty);
			base.Controls.Add(mmLabelReserved);
			base.Controls.Add(labelFactor);
			base.Controls.Add(textBoxUnitDescription);
			base.Controls.Add(comboBoxUnit);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(textBoxDescription);
			base.Controls.Add(textBoxItemCode);
			base.Controls.Add(dataGrid);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(542, 302);
			base.Name = "ProductQuantityForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Stock Availability";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(AnalysisGroupDetailsForm_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(ProductQuantityForm_KeyDown);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxUnit).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGrid).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
