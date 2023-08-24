using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class EntityCategoryAssignDialog : Form, IForm
	{
		private bool isTreeView;

		private EntityTypesEnum entityType;

		private DataSet selectedData;

		private string selectedID = "";

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Line linePanelDown;

		private XPButton buttonCancel;

		private MMSListGrid dataGridItems;

		private Label label1;

		private Label label2;

		private TextBox textBoxCode;

		private TextBox textBoxName;

		private DataGridTreeList dataGridTreeList;

		public ScreenAreas ScreenArea => ScreenAreas.Accounts;

		public int ScreenID => 1030;

		public ScreenTypes ScreenType => ScreenTypes.System;

		public bool AllowEdit
		{
			get;
			set;
		}

		public string EntityID
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

		public string EntityName
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

		public EntityTypesEnum EntityType
		{
			get
			{
				return entityType;
			}
			set
			{
				entityType = value;
			}
		}

		public bool IsTreeView
		{
			get
			{
				return isTreeView;
			}
			set
			{
				isTreeView = value;
			}
		}

		public string SelectedID
		{
			get
			{
				if (base.DialogResult == DialogResult.Cancel)
				{
					return "";
				}
				return selectedID;
			}
			set
			{
				selectedID = value;
			}
		}

		public UltraGridRow SelectedRow
		{
			get
			{
				if (base.DialogResult == DialogResult.Cancel)
				{
					return null;
				}
				if (dataGridItems.ActiveRow != null)
				{
					return dataGridItems.ActiveRow;
				}
				return null;
			}
		}

		public UltraGrid Grid => dataGridItems;

		public DataSet DataSource
		{
			get
			{
				return (DataSet)dataGridItems.DataSource;
			}
			set
			{
				if (value != null && value.Tables.Count > 0)
				{
					DataTable dataTable = value.Tables[0];
					dataTable.Columns.Add("SearchColumn");
					string text = "";
					foreach (DataRow row in dataTable.Rows)
					{
						text = "";
						foreach (DataColumn column in dataTable.Columns)
						{
							if (!(column.ColumnName == "SearchColumn") && !(column.ColumnName == "ChequeID"))
							{
								text = text + row[column].ToString() + " ";
							}
						}
						row["SearchColumn"] = text;
					}
				}
				dataGridItems.DataSource = value;
				dataGridItems.DisplayLayout.Bands[0].Columns["SearchColumn"].Hidden = true;
			}
		}

		public EntityCategoryAssignDialog()
		{
			InitializeComponent();
			base.Load += SelectChequeDialog_Load;
			base.Activated += SelectChequeDialog_Activated;
			dataGridTreeList.CellChange += dataGridTreeList_CellChange;
		}

		private void SelectChequeDialog_Activated(object sender, EventArgs e)
		{
		}

		private void LoadData()
		{
			if (IsTreeView)
			{
				dataGridTreeList.ApplyUIDesign();
				selectedData = Factory.EntityCategorySystem.GetEntityAssignedCategorysTreeViewList(textBoxCode.Text, entityType);
				dataGridTreeList.DataSource = selectedData.Tables[0];
			}
			else
			{
				selectedData = Factory.EntityCategorySystem.GetEntityAssignedCategorysList(textBoxCode.Text, entityType);
				dataGridItems.DataSource = selectedData.Tables[0];
			}
		}

		private void SelectChequeDialog_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridItems.ApplyUIDesign();
				dataGridTreeList.ApplyUIDesign();
				EnableTreeViewCheckbox();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private bool SaveData()
		{
			try
			{
				EntityCategoryData entityCategoryData = new EntityCategoryData();
				if (!IsTreeView)
				{
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						if (row.Cells["C"].Value.ToString() == "True")
						{
							entityCategoryData.Tables["Entity_Category_Detail"].Rows.Add(textBoxCode.Text, row.Cells["Code"].Value.ToString(), EntityType);
						}
					}
				}
				else
				{
					foreach (UltraGridRow item in dataGridTreeList.Rows.GetRowEnumerator(GridRowType.DataRow, null, null))
					{
						if (item.Cells["C"].Value.ToString() == "True")
						{
							entityCategoryData.Tables["Entity_Category_Detail"].Rows.Add(textBoxCode.Text, item.Cells["Code"].Value.ToString(), EntityType);
						}
					}
				}
				return Factory.EntityCategorySystem.InsertEntityCategoryAssignment(entityCategoryData, textBoxCode.Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void dataGridItems_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
		{
		}

		private void SelectItem()
		{
			if (dataGridItems.Rows.VisibleRowCount == 0 || dataGridItems.ActiveRow == null)
			{
				ErrorHelper.InformationMessage(UIMessages.SelectAnItemFirst);
			}
			else if (dataGridItems.ActiveRow != null)
			{
				string text2 = SelectedID = dataGridItems.ActiveRow.Cells[0].Value.ToString();
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (SaveData())
			{
				Close();
			}
			else
			{
				base.DialogResult = DialogResult.None;
			}
		}

		private void EntityGroupAssignDialog_Load(object sender, EventArgs e)
		{
			try
			{
				LoadData();
				if (!IsTreeView)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].Width = 20;
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].MinWidth = 20;
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].MaxWidth = 20;
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].LockedWidth = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellClickAction = CellClickAction.Edit;
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellActivation = Activation.AllowEdit;
				}
				else
				{
					SetupTreeGrid();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetupTreeGrid()
		{
			foreach (UltraGridRow item in dataGridTreeList.Rows.GetRowEnumerator(GridRowType.DataRow, null, null))
			{
				if (bool.Parse(item.Cells["IsParent"].Value.ToString()))
				{
					item.Appearance.FontData.Bold = DefaultableBoolean.True;
				}
			}
			foreach (UltraGridBand band in dataGridTreeList.DisplayLayout.Bands)
			{
				band.Columns["Code"].Width = 25;
				band.Columns["C"].Width = 25;
				band.Columns["IsParent"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				band.Columns["IsParent"].Hidden = true;
				band.Columns["ParentID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				band.Columns["ParentID"].Hidden = true;
				band.Columns["C"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
				band.Columns["C"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
				band.Columns["C"].CellClickAction = CellClickAction.Edit;
				band.Columns["C"].CellActivation = Activation.AllowEdit;
				band.Columns["C"].FilterOperandStyle = FilterOperandStyle.None;
				band.Columns["Code"].CellClickAction = CellClickAction.RowSelect;
				band.Columns["Code"].CellActivation = Activation.NoEdit;
				band.Columns["Name"].CellClickAction = CellClickAction.RowSelect;
				band.Columns["Name"].CellActivation = Activation.NoEdit;
			}
			dataGridTreeList.Rows.ExpandAll(recursive: true);
			dataGridItems.Visible = false;
			dataGridTreeList.Visible = true;
			dataGridTreeList.BringToFront();
		}

		private void EnableTreeViewCheckbox()
		{
			dataGridTreeList.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
			dataGridTreeList.DisplayLayout.Override.CellClickAction = CellClickAction.Edit;
			dataGridTreeList.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
		}

		private void dataGridTreeList_CellChange(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "C" && e.Cell.Row.HasChild() && ErrorHelper.QuestionMessageYesNo("Apply to child items?") == DialogResult.Yes)
			{
				bool result = false;
				bool.TryParse(dataGridTreeList.ActiveRow.Cells["C"].Value.ToString(), out result);
				string a = dataGridTreeList.ActiveRow.Cells["Code"].Value.ToString();
				foreach (UltraGridRow item in dataGridTreeList.Rows.GetRowEnumerator(GridRowType.DataRow, null, null))
				{
					if (a == item.Cells["ParentID"].Value.ToString())
					{
						item.Cells["C"].Value = !result;
						if (IsParent(item.Cells["Code"].Value.ToString()))
						{
							ApplyChild(item.Cells["Code"].Value.ToString(), !result);
						}
					}
				}
			}
		}

		private void ApplyChild(string parentID, bool val)
		{
			foreach (UltraGridRow item in dataGridTreeList.Rows.GetRowEnumerator(GridRowType.DataRow, null, null))
			{
				if (parentID == item.Cells["ParentID"].Value.ToString())
				{
					item.Cells["C"].Value = val;
					if (IsParent(item.Cells["Code"].Value.ToString()))
					{
						ApplyChild(item.Cells["Code"].Value.ToString(), val);
					}
				}
			}
		}

		private bool IsParent(string code)
		{
			bool result = false;
			if (selectedData.Tables[1].AsEnumerable().FirstOrDefault((DataRow tt) => tt.Field<string>("ParentID") == code) != null)
			{
				result = true;
			}
			return result;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.EntityCategoryAssignDialog));
			panelButtons = new System.Windows.Forms.Panel();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			textBoxCode = new System.Windows.Forms.TextBox();
			textBoxName = new System.Windows.Forms.TextBox();
			dataGridTreeList = new Micromind.UISupport.DataGridTreeList(components);
			dataGridItems = new Micromind.DataControls.MMSListGrid();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridTreeList).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 326);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(449, 40);
			panelButtons.TabIndex = 3;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(237, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 3;
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
			linePanelDown.Size = new System.Drawing.Size(449, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(339, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 4;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 9);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(64, 13);
			label1.TabIndex = 7;
			label1.Text = "Entity Code:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 29);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(67, 13);
			label2.TabIndex = 8;
			label2.Text = "Entity Name:";
			textBoxCode.Location = new System.Drawing.Point(96, 7);
			textBoxCode.Name = "textBoxCode";
			textBoxCode.ReadOnly = true;
			textBoxCode.Size = new System.Drawing.Size(135, 20);
			textBoxCode.TabIndex = 9;
			textBoxName.Location = new System.Drawing.Point(96, 29);
			textBoxName.Name = "textBoxName";
			textBoxName.ReadOnly = true;
			textBoxName.Size = new System.Drawing.Size(338, 20);
			textBoxName.TabIndex = 10;
			dataGridTreeList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridTreeList.DisplayLayout.Appearance = appearance;
			dataGridTreeList.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
			dataGridTreeList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridTreeList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridTreeList.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridTreeList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridTreeList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridTreeList.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridTreeList.DisplayLayout.MaxColScrollRegions = 1;
			dataGridTreeList.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridTreeList.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridTreeList.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridTreeList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridTreeList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridTreeList.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridTreeList.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridTreeList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridTreeList.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridTreeList.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridTreeList.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridTreeList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridTreeList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridTreeList.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridTreeList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridTreeList.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridTreeList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridTreeList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridTreeList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridTreeList.Location = new System.Drawing.Point(10, 59);
			dataGridTreeList.Name = "dataGridTreeList";
			dataGridTreeList.ShowDeleteMenu = false;
			dataGridTreeList.ShowMinusInRed = true;
			dataGridTreeList.ShowNewMenu = false;
			dataGridTreeList.Size = new System.Drawing.Size(424, 257);
			dataGridTreeList.TabIndex = 295;
			dataGridTreeList.Text = "dataGridTreeList1";
			dataGridTreeList.Visible = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance13;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance20;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance22;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance23;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.Location = new System.Drawing.Point(10, 59);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowMinusInRed = true;
			dataGridItems.Size = new System.Drawing.Size(424, 257);
			dataGridItems.TabIndex = 6;
			dataGridItems.Text = "mmsListGrid1";
			dataGridItems.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(dataGridItems_DoubleClickRow);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(449, 366);
			base.Controls.Add(dataGridTreeList);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(465, 215);
			base.Name = "EntityCategoryAssignDialog";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Select Categories";
			base.Load += new System.EventHandler(EntityGroupAssignDialog_Load);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridTreeList).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
