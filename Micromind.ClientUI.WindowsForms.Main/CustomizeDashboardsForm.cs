using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Properties;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.Main
{
	public class CustomizeDashboardsForm : Form
	{
		public bool IsDirty;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonOK;

		private MMSListGrid dataGridItems;

		private Button buttonAdd;

		private ImageList imageList1;

		public CustomizeDashboardsForm()
		{
			InitializeComponent();
			dataGridItems.ClickCellButton += dataGridItems_ClickCellButton;
		}

		private void dataGridItems_ClickCellButton(object sender, CellEventArgs e)
		{
			string text = e.Cell.Row.Cells["Code"].Value.ToString();
			checked
			{
				if (e.Cell.Column.Key == "Edit")
				{
					AddDashboardDialog addDashboardDialog = new AddDashboardDialog();
					addDashboardDialog.LoadData(text);
					if (addDashboardDialog.ShowDialog(this) == DialogResult.OK)
					{
						IsDirty = true;
						LoadData();
					}
				}
				else if (e.Cell.Column.Key == "Delete")
				{
					if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete this dashboard?") == DialogResult.Yes)
					{
						Factory.DashboardSystem.DeleteDashboard(text, Global.CurrentUser);
						IsDirty = true;
						LoadData();
					}
				}
				else if (e.Cell.Column.Key == "Up")
				{
					if (e.Cell.Row.Index != 0)
					{
						string idFieldValue = dataGridItems.Rows[e.Cell.Row.Index - 1].Cells["Code"].Value.ToString();
						int index = e.Cell.Row.Index;
						dataGridItems.Rows.Move(e.Cell.Row, e.Cell.Row.Index - 1);
						Factory.DatabaseSystem.UpdateFieldValue("Dashboard", "RowIndex", index - 1, typeof(int), "DashboardID", text);
						Factory.DatabaseSystem.UpdateFieldValue("Dashboard", "RowIndex", index, typeof(int), "DashboardID", idFieldValue);
						IsDirty = true;
					}
				}
				else if (e.Cell.Column.Key == "Down" && e.Cell.Row.Index != dataGridItems.Rows.Count - 1)
				{
					string idFieldValue2 = dataGridItems.Rows[e.Cell.Row.Index + 1].Cells["Code"].Value.ToString();
					int index2 = e.Cell.Row.Index;
					dataGridItems.Rows.Move(e.Cell.Row, e.Cell.Row.Index + 1);
					Factory.DatabaseSystem.UpdateFieldValue("Dashboard", "RowIndex", index2 + 1, typeof(int), "DashboardID", text);
					Factory.DatabaseSystem.UpdateFieldValue("Dashboard", "RowIndex", index2, typeof(int), "DashboardID", idFieldValue2);
					IsDirty = true;
				}
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataGridItems.ApplyUIDesign();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Code");
				dataTable.Columns.Add("Name");
				dataTable.Columns.Add("Edit");
				dataTable.Columns.Add("Delete");
				dataTable.Columns.Add("Up");
				dataTable.Columns.Add("Down");
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Columns["Delete"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
				dataGridItems.DisplayLayout.Bands[0].Columns["Edit"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
				dataGridItems.DisplayLayout.Bands[0].Columns["Up"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
				dataGridItems.DisplayLayout.Bands[0].Columns["Down"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
				dataGridItems.DisplayLayout.Bands[0].Columns["Code"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Delete"].CellButtonAppearance.Image = imageList1.Images[0];
				dataGridItems.DisplayLayout.Bands[0].Columns["Edit"].CellButtonAppearance.Image = imageList1.Images[1];
				dataGridItems.DisplayLayout.Bands[0].Columns["Up"].CellButtonAppearance.Image = imageList1.Images[2];
				dataGridItems.DisplayLayout.Bands[0].Columns["Down"].CellButtonAppearance.Image = imageList1.Images[3];
				Infragistics.Win.UltraWinGrid.ColumnHeader header = dataGridItems.DisplayLayout.Bands[0].Columns["Delete"].Header;
				Infragistics.Win.UltraWinGrid.ColumnHeader header2 = dataGridItems.DisplayLayout.Bands[0].Columns["Edit"].Header;
				Infragistics.Win.UltraWinGrid.ColumnHeader header3 = dataGridItems.DisplayLayout.Bands[0].Columns["Up"].Header;
				string text2 = dataGridItems.DisplayLayout.Bands[0].Columns["Down"].Header.Caption = "";
				string text4 = header3.Caption = text2;
				string text7 = header.Caption = (header2.Caption = text4);
				dataGridItems.DisplayLayout.Bands[0].Override.AllowColMoving = AllowColMoving.NotAllowed;
				dataGridItems.DisplayLayout.Bands[0].Override.HeaderClickAction = HeaderClickAction.Select;
				dataGridItems.DisplayLayout.Bands[0].Override.DefaultRowHeight = 20;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["Delete"];
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["Edit"];
				UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["Up"];
				int num2 = dataGridItems.DisplayLayout.Bands[0].Columns["Down"].Width = 22;
				int num4 = ultraGridColumn3.Width = num2;
				int num7 = ultraGridColumn.Width = (ultraGridColumn2.Width = num4);
				UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["Delete"];
				UltraGridColumn ultraGridColumn5 = dataGridItems.DisplayLayout.Bands[0].Columns["Edit"];
				UltraGridColumn ultraGridColumn6 = dataGridItems.DisplayLayout.Bands[0].Columns["Up"];
				bool flag2 = dataGridItems.DisplayLayout.Bands[0].Columns["Down"].LockedWidth = true;
				bool flag4 = ultraGridColumn6.LockedWidth = flag2;
				bool lockedWidth = ultraGridColumn5.LockedWidth = flag4;
				ultraGridColumn4.LockedWidth = lockedWidth;
				UltraGridColumn ultraGridColumn7 = dataGridItems.DisplayLayout.Bands[0].Columns["Delete"];
				UltraGridColumn ultraGridColumn8 = dataGridItems.DisplayLayout.Bands[0].Columns["Edit"];
				UltraGridColumn ultraGridColumn9 = dataGridItems.DisplayLayout.Bands[0].Columns["Up"];
				num2 = (dataGridItems.DisplayLayout.Bands[0].Columns["Down"].MaxWidth = 22);
				num4 = (ultraGridColumn9.MaxWidth = num2);
				num7 = (ultraGridColumn7.MaxWidth = (ultraGridColumn8.MaxWidth = num4));
				dataGridItems.DisplayLayout.Bands[0].Columns["Delete"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
				dataGridItems.DisplayLayout.Bands[0].Columns["Edit"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
				dataGridItems.DisplayLayout.Bands[0].Columns["up"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
				dataGridItems.DisplayLayout.Bands[0].Columns["Down"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void CustomizeDashboardForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetupGrid();
				LoadData();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadData()
		{
			DataSet dashboardsByUser = Factory.DashboardSystem.GetDashboardsByUser(Global.CurrentUser);
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			dataTable.Rows.Clear();
			foreach (DataRow row in dashboardsByUser.Tables[0].Rows)
			{
				if (!(row["DashboardID"].ToString() == "dashHome"))
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["Code"] = row["DashboardID"];
					dataRow2["Name"] = row["Name"];
					dataTable.Rows.Add(dataRow2);
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (new AddDashboardDialog().ShowDialog(this) == DialogResult.OK)
			{
				IsDirty = true;
				LoadData();
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (IsDirty)
			{
				base.DialogResult = DialogResult.OK;
			}
			else
			{
				base.DialogResult = DialogResult.Cancel;
			}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.Main.CustomizeDashboardsForm));
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
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonOK = new Micromind.UISupport.XPButton();
			buttonAdd = new System.Windows.Forms.Button();
			imageList1 = new System.Windows.Forms.ImageList(components);
			dataGridItems = new Micromind.DataControls.MMSListGrid();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 309);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(503, 40);
			panelButtons.TabIndex = 2;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(503, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(396, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 0;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonAdd.Image = Micromind.ClientUI.Properties.Resources.add;
			buttonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonAdd.Location = new System.Drawing.Point(12, 11);
			buttonAdd.Name = "buttonAdd";
			buttonAdd.Size = new System.Drawing.Size(84, 23);
			buttonAdd.TabIndex = 0;
			buttonAdd.Text = "Add";
			buttonAdd.UseVisualStyleBackColor = true;
			buttonAdd.Click += new System.EventHandler(button1_Click);
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "delete");
			imageList1.Images.SetKeyName(1, "edit.png");
			imageList1.Images.SetKeyName(2, "up");
			imageList1.Images.SetKeyName(3, "down");
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
			dataGridItems.Location = new System.Drawing.Point(12, 36);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowMinusInRed = true;
			dataGridItems.Size = new System.Drawing.Size(480, 265);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "mmsListGrid1";
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(503, 349);
			base.Controls.Add(buttonAdd);
			base.Controls.Add(panelButtons);
			base.Controls.Add(dataGridItems);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CustomizeDashboardsForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			Text = "Customize Dashboards";
			base.Load += new System.EventHandler(CustomizeDashboardForm_Load);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
		}
	}
}
