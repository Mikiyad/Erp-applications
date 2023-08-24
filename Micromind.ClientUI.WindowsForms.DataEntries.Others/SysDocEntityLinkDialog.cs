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
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class SysDocEntityLinkDialog : Form, IForm
	{
		public SysDocEntityTypes EntityType = SysDocEntityTypes.CustomerClass;

		private string selectedID = "";

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Line linePanelDown;

		private XPButton buttonCancel;

		private MMSListGrid dataGridItems;

		private Label label1;

		private TextBox textBoxCode;

		private TextBox textBoxReportID;

		public ScreenAreas ScreenArea => ScreenAreas.Accounts;

		public int ScreenID => 1030;

		public ScreenTypes ScreenType => ScreenTypes.System;

		public string SysDocID
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

		public string ReportID
		{
			get
			{
				return textBoxReportID.Text;
			}
			set
			{
				textBoxReportID.Text = value;
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

		public SysDocEntityLinkDialog()
		{
			InitializeComponent();
			base.Load += Form_Load;
			base.Activated += SelectChequeDialog_Activated;
		}

		private void SelectChequeDialog_Activated(object sender, EventArgs e)
		{
		}

		private void LoadData()
		{
			DataSet dataSet = null;
			if (EntityType == SysDocEntityTypes.CustomerClass)
			{
				dataSet = Factory.CustomerClassSystem.GetCustomerClassComboList();
			}
			else if (EntityType == SysDocEntityTypes.SupplierClass)
			{
				dataSet = Factory.VendorClassSystem.GetVendorClassComboList();
			}
			else if (EntityType == SysDocEntityTypes.ProductClass)
			{
				dataSet = Factory.ProductClassSystem.GetProductClassComboList();
			}
			else if (EntityType == SysDocEntityTypes.AccountGroup)
			{
				dataSet = Factory.AccountGroupsSystem.GetAccountGroupsComboList();
			}
			else
			{
				if (EntityType != SysDocEntityTypes.User)
				{
					throw new Exception("SysDocEntityType not implimented");
				}
				dataSet = Factory.UserSystem.GetUserComboList();
			}
			dataGridItems.DataSource = dataSet;
			dataGridItems.DisplayLayout.Bands[0].Columns.Insert(0, "C");
			dataGridItems.DisplayLayout.Bands[0].Columns["C"].DefaultCellValue = false;
			if (textBoxCode.Text != "")
			{
				dataSet = Factory.SystemDocumentSystem.GetEntityLinks(textBoxCode.Text, EntityType);
			}
			else if (textBoxReportID.Text != "")
			{
				dataSet = Factory.SystemDocumentSystem.GetExternalReportUserLinks(textBoxReportID.Text, EntityType);
			}
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				row.Cells["C"].Value = false;
			}
			string b = "";
			foreach (DataRow row2 in dataSet.Tables[0].Rows)
			{
				if (SysDocID != "")
				{
					b = row2["EntityID"].ToString();
				}
				else if (ReportID != "")
				{
					b = row2["UserID"].ToString();
				}
				foreach (UltraGridRow row3 in dataGridItems.Rows)
				{
					if (row3.Cells["Code"].Value != null && row3.Cells["Code"].Value.ToString() == b)
					{
						row3.Cells["C"].Value = true;
					}
				}
			}
			if (EntityType == SysDocEntityTypes.AccountGroup)
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Type"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["TypeID"].Header.Caption = "Type";
				ValueList valueList = new ValueList();
				valueList.ValueListItems.Add(1, "Asset");
				valueList.ValueListItems.Add(2, "Liability");
				valueList.ValueListItems.Add(3, "Income");
				valueList.ValueListItems.Add(4, "Expense");
				valueList.ValueListItems.Add(5, "Equity");
				dataGridItems.DisplayLayout.Bands[0].Columns["TypeID"].ValueList = valueList;
			}
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridItems.ApplyUIDesign();
				LoadData();
				dataGridItems.DisplayLayout.Bands[0].Columns["C"].Width = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["C"].MinWidth = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["C"].MaxWidth = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["C"].LockedWidth = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["C"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
				dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
				dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellClickAction = CellClickAction.Edit;
				dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellActivation = Activation.AllowEdit;
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
				SystemDocumentData systemDocumentData = new SystemDocumentData();
				bool result = false;
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (row.Cells["C"].Value.ToString() == "True")
					{
						if (textBoxCode.Text != "")
						{
							systemDocumentData.Tables["System_Doc_Entity_Link"].Rows.Add(textBoxCode.Text, row.Cells["Code"].Value.ToString(), EntityType);
						}
						else
						{
							systemDocumentData.Tables["External_Report_User_Link"].Rows.Add(textBoxReportID.Text, row.Cells["Code"].Value.ToString(), EntityType);
						}
					}
				}
				if (textBoxCode.Text != "")
				{
					result = Factory.SystemDocumentSystem.InsertEntityLinks(systemDocumentData, textBoxCode.Text, EntityType);
					if (systemDocumentData.Tables["System_Doc_Entity_Link"].Rows.Count == 0)
					{
						result = Factory.SystemDocumentSystem.DeleteEntityLinks(textBoxCode.Text, EntityType);
					}
				}
				else if (textBoxReportID.Text != "")
				{
					result = Factory.SystemDocumentSystem.InsertExternalReportLinks(systemDocumentData, textBoxReportID.Text, EntityType);
				}
				return result;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.SysDocEntityLinkDialog));
			panelButtons = new System.Windows.Forms.Panel();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			dataGridItems = new Micromind.DataControls.MMSListGrid();
			label1 = new System.Windows.Forms.Label();
			textBoxCode = new System.Windows.Forms.TextBox();
			textBoxReportID = new System.Windows.Forms.TextBox();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 284);
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
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.Location = new System.Drawing.Point(10, 33);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowMinusInRed = true;
			dataGridItems.Size = new System.Drawing.Size(424, 241);
			dataGridItems.TabIndex = 6;
			dataGridItems.Text = "mmsListGrid1";
			dataGridItems.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(dataGridItems_DoubleClickRow);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 9);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(81, 13);
			label1.TabIndex = 7;
			label1.Text = "System Doc ID:";
			textBoxCode.Location = new System.Drawing.Point(96, 7);
			textBoxCode.Name = "textBoxCode";
			textBoxCode.ReadOnly = true;
			textBoxCode.Size = new System.Drawing.Size(135, 20);
			textBoxCode.TabIndex = 9;
			textBoxReportID.Location = new System.Drawing.Point(299, 7);
			textBoxReportID.Name = "textBoxReportID";
			textBoxReportID.ReadOnly = true;
			textBoxReportID.Size = new System.Drawing.Size(135, 20);
			textBoxReportID.TabIndex = 10;
			textBoxReportID.Visible = false;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(449, 324);
			base.Controls.Add(textBoxReportID);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(label1);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(465, 215);
			base.Name = "SysDocEntityLinkDialog";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Select Items";
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
