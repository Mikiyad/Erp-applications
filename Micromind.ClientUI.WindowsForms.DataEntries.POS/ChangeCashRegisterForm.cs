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

namespace Micromind.ClientUI.WindowsForms.DataEntries.POS
{
	public class ChangeCashRegisterForm : Form, IForm
	{
		private string strPOSRegister = "";

		private string strPOSRegisterID = "";

		private IContainer components;

		private POSCashRegisterComboBox posCashRegisterComboBox1;

		private Label label1;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private Label label2;

		public ScreenAreas ScreenArea => ScreenAreas.POS;

		public int ScreenID => 1014;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		public ChangeCashRegisterForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
		}

		public void OnActivated()
		{
		}

		private void posCashRegisterComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				label2.Text = "";
				DataSet cashRegisterByComputerName = Factory.POSCashRegisterSystem.GetCashRegisterByComputerName(Environment.MachineName);
				if (cashRegisterByComputerName != null && cashRegisterByComputerName.Tables[0].Rows.Count > 0)
				{
					strPOSRegister = cashRegisterByComputerName.Tables[0].Rows[0]["CashRegisterName"].ToString();
					if (strPOSRegister != null)
					{
						label2.Text = "This Machine is already assigned with " + strPOSRegister + ".";
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			label2.Text = "";
			if (posCashRegisterComboBox1.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select a Cash Register.");
				base.DialogResult = DialogResult.None;
			}
			else
			{
				try
				{
					bool flag = Factory.POSCashRegisterSystem.IsCashRegisterFree(posCashRegisterComboBox1.SelectedID);
					if (!flag)
					{
						label2.Text = "This Cash Register is already assigned to another POS machine.";
					}
					if (flag)
					{
						DataSet cashRegisterByComputerName = Factory.POSCashRegisterSystem.GetCashRegisterByComputerName(Environment.MachineName);
						strPOSRegisterID = cashRegisterByComputerName.Tables[0].Rows[0]["CashRegisterID"].ToString();
						flag = Factory.POSCashRegisterSystem.ChangeCashRegister(strPOSRegisterID, Environment.MachineName);
					}
					if (flag)
					{
						flag = Factory.POSCashRegisterSystem.AssignCashRegister(posCashRegisterComboBox1.SelectedID, Environment.MachineName);
					}
					if (!flag)
					{
						ErrorHelper.ErrorMessage("Cannot assign the Cash Register. Please try again.");
						return;
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.POS.ChangeCashRegisterForm));
			label1 = new System.Windows.Forms.Label();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			label2 = new System.Windows.Forms.Label();
			posCashRegisterComboBox1 = new Micromind.DataControls.POSCashRegisterComboBox();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)posCashRegisterComboBox1).BeginInit();
			SuspendLayout();
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(38, 32);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(76, 13);
			label1.TabIndex = 2;
			label1.Text = "POS Registers";
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 122);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(537, 40);
			panelButtons.TabIndex = 15;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(537, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(427, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 3;
			xpButton1.Text = "&Cancel";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(327, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.ForeColor = System.Drawing.SystemColors.ControlText;
			label2.Location = new System.Drawing.Point(38, 91);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(0, 17);
			label2.TabIndex = 16;
			posCashRegisterComboBox1.AlwaysInEditMode = true;
			posCashRegisterComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			posCashRegisterComboBox1.CustomReportFieldName = "";
			posCashRegisterComboBox1.CustomReportKey = "";
			posCashRegisterComboBox1.CustomReportValueType = 1;
			posCashRegisterComboBox1.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			posCashRegisterComboBox1.DisplayLayout.Appearance = appearance;
			posCashRegisterComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			posCashRegisterComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			posCashRegisterComboBox1.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			posCashRegisterComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			posCashRegisterComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			posCashRegisterComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			posCashRegisterComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			posCashRegisterComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			posCashRegisterComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			posCashRegisterComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			posCashRegisterComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			posCashRegisterComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			posCashRegisterComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			posCashRegisterComboBox1.DisplayLayout.Override.CellAppearance = appearance8;
			posCashRegisterComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			posCashRegisterComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			posCashRegisterComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			posCashRegisterComboBox1.DisplayLayout.Override.HeaderAppearance = appearance10;
			posCashRegisterComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			posCashRegisterComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			posCashRegisterComboBox1.DisplayLayout.Override.RowAppearance = appearance11;
			posCashRegisterComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			posCashRegisterComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			posCashRegisterComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			posCashRegisterComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			posCashRegisterComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			posCashRegisterComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			posCashRegisterComboBox1.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			posCashRegisterComboBox1.Editable = true;
			posCashRegisterComboBox1.FilterString = "";
			posCashRegisterComboBox1.Font = new System.Drawing.Font("Tahoma", 14f);
			posCashRegisterComboBox1.HasAllAccount = false;
			posCashRegisterComboBox1.HasCustom = false;
			posCashRegisterComboBox1.IsDataLoaded = false;
			posCashRegisterComboBox1.Location = new System.Drawing.Point(142, 24);
			posCashRegisterComboBox1.MaxDropDownItems = 12;
			posCashRegisterComboBox1.Name = "posCashRegisterComboBox1";
			posCashRegisterComboBox1.ShowInactiveItems = false;
			posCashRegisterComboBox1.ShowQuickAdd = true;
			posCashRegisterComboBox1.Size = new System.Drawing.Size(220, 31);
			posCashRegisterComboBox1.TabIndex = 1;
			posCashRegisterComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			posCashRegisterComboBox1.SelectedIndexChanged += new System.EventHandler(posCashRegisterComboBox1_SelectedIndexChanged);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(537, 162);
			base.Controls.Add(label2);
			base.Controls.Add(panelButtons);
			base.Controls.Add(label1);
			base.Controls.Add(posCashRegisterComboBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(500, 200);
			base.Name = "ChangeCashRegisterForm";
			Text = "Change POS Register";
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)posCashRegisterComboBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
