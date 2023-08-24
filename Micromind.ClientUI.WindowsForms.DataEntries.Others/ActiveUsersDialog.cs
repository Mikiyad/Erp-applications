using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class ActiveUsersDialog : Form
	{
		public bool CanClose = true;

		private bool isMultiSelect;

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Line linePanelDown;

		private XPButton buttonCancel;

		private DataGridList dataGridList;

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

		public event EventHandler ValidateSelection;

		public ActiveUsersDialog()
		{
			InitializeComponent();
			base.Activated += SelectShortcutFormDialog_Activated;
			dataGridList.DoubleClick += dataGridItems_DoubleClick;
		}

		private void dataGridItems_DoubleClick(object sender, EventArgs e)
		{
		}

		private void SelectShortcutFormDialog_Activated(object sender, EventArgs e)
		{
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			CanClose = true;
			if (this.ValidateSelection != null)
			{
				this.ValidateSelection(this, null);
			}
			if (CanClose)
			{
				Close();
			}
		}

		private void SelectItem()
		{
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (dataGridList.ActiveRow != null && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "This users's connection with the server will be lost. Are you sure to remove this user?") == DialogResult.Yes)
			{
				try
				{
					Factory.RemoveUser(dataGridList.ActiveRow.Cells["UserID"].Value.ToString(), dataGridList.ActiveRow.Cells["MachineID"].Value.ToString(), dataGridList.ActiveRow.Cells["DBName"].Value.ToString());
					LoadData();
				}
				catch (Exception ex)
				{
					ErrorHelper.ErrorMessage(ex.Message);
				}
			}
		}

		private void panelButtons_Paint(object sender, PaintEventArgs e)
		{
		}

		private void ActiveUsersDialog_Load(object sender, EventArgs e)
		{
			try
			{
				LoadData();
				dataGridList.ApplyUIDesign();
				dataGridList.ContextMenuStrip = null;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadData()
		{
			dataGridList.DataSource = Factory.GetActiveUsers();
			dataGridList.DisplayLayout.Bands[0].Columns["ActivityID"].Hidden = true;
			dataGridList.DisplayLayout.Bands[0].Columns["ServerID"].Hidden = true;
			dataGridList.DisplayLayout.Bands[0].Columns["AppName"].Hidden = true;
			dataGridList.DisplayLayout.Bands[0].Columns["SystemID"].Hidden = true;
			dataGridList.DisplayLayout.Bands[0].Columns["ProductKey"].Hidden = true;
			dataGridList.DisplayLayout.Bands[0].Columns["LastLogDate"].Hidden = true;
			dataGridList.DisplayLayout.Bands[0].Columns["MachineID"].Header.Caption = "Computer Name";
			dataGridList.DisplayLayout.Bands[0].Columns["UserID"].Header.Caption = "User ID";
			dataGridList.DisplayLayout.Bands[0].Columns["LoginDate"].Header.Caption = "Login Date";
			dataGridList.DisplayLayout.Bands[0].Columns["DBName"].Header.Caption = "Database";
			dataGridList.AutoFitColumns();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.ActiveUsersDialog));
			panelButtons = new System.Windows.Forms.Panel();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			dataGridList = new Micromind.UISupport.DataGridList();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 269);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(502, 40);
			panelButtons.TabIndex = 3;
			panelButtons.Paint += new System.Windows.Forms.PaintEventHandler(panelButtons_Paint);
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(12, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 3;
			buttonOK.Text = "&Remove";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(502, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(392, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 4;
			buttonCancel.Text = "&Close";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			dataGridList.AllowUnfittedView = false;
			dataGridList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridList.DisplayLayout.Appearance = appearance;
			dataGridList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridList.DisplayLayout.MaxColScrollRegions = 1;
			dataGridList.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridList.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridList.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridList.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridList.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridList.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridList.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridList.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridList.Location = new System.Drawing.Point(12, 12);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(478, 251);
			dataGridList.TabIndex = 4;
			dataGridList.Text = "dataGridList1";
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(502, 309);
			base.Controls.Add(dataGridList);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(465, 215);
			base.Name = "ActiveUsersDialog";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Active Users";
			base.Load += new System.EventHandler(ActiveUsersDialog_Load);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			ResumeLayout(false);
		}
	}
}
