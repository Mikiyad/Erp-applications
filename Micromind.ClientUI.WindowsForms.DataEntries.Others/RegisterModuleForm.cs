using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using LicenseManager;
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
	public class RegisterModuleForm : Form, IForm
	{
		private CompanyInformationData currentData;

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonSave;

		private FormManager formManager;

		private DataGridList dataGridList;

		private Label label1;

		private Button buttonAdd;

		private Label label2;

		private TextBox textBoxName;

		private Label label3;

		private MaskedTextBox textBoxProductKey;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem deleteModuleToolStripMenuItem;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6002;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

		private bool IsDirty => formManager.GetDirtyStatus();

		private bool IsNewRecord
		{
			get
			{
				return false;
			}
			set
			{
				isNewRecord = value;
			}
		}

		public RegisterModuleForm()
		{
			InitializeComponent();
			dataGridList.ContextMenuStrip = contextMenuStrip1;
		}

		private bool GetData()
		{
			try
			{
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			Close();
		}

		public void LoadData()
		{
			try
			{
				if (CanClose())
				{
					DataSet installedModules = Factory.CompanyInformationSystem.GetInstalledModules();
					DataTable dataTable = dataGridList.DataSource as DataTable;
					dataTable.Rows.Clear();
					LicenseManagerControl moduleLicenseManager = AxolonLicense.ModuleLicenseManager;
					foreach (DataRow row in installedModules.Tables[0].Rows)
					{
						bool flag = true;
						string key = row["ModuleKey"].ToString();
						flag = moduleLicenseManager.SetKey(key);
						int productID = AxolonLicense.ModuleLicenseManager.License.ProductID;
						if (productID < 11 || productID > 40)
						{
							flag = false;
						}
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["ModuleID"] = row["ModuleID"];
						dataRow2["Code"] = productID;
						dataRow2["Key"] = row["ModuleKey"];
						if (!flag)
						{
							dataRow2["Name"] = "Invalid Key";
						}
						else
						{
							switch (productID)
							{
							case 12:
								dataRow2["Name"] = "CRM";
								break;
							case 11:
								dataRow2["Name"] = "Advanced Manufacturing";
								break;
							case 18:
								dataRow2["Name"] = "OCR Scanning";
								break;
							case 13:
								dataRow2["Name"] = "Point of Sale";
								break;
							case 14:
								dataRow2["Name"] = "Project Accounting";
								break;
							case 16:
								dataRow2["Name"] = "Property Rental";
								break;
							case 15:
								dataRow2["Name"] = "Quality Control";
								break;
							case 17:
								dataRow2["Name"] = "3PL Warehousing";
								break;
							case 19:
								dataRow2["Name"] = "Garment Rental";
								break;
							case 20:
								dataRow2["Name"] = "Vehicle";
								break;
							case 21:
								dataRow2["Name"] = "Horse";
								break;
							case 23:
								dataRow2["Name"] = "HR";
								break;
							case 22:
								dataRow2["Name"] = "Consignment";
								break;
							case 24:
								dataRow2["Name"] = "Enterprise Asset";
								break;
							case 25:
								dataRow2["Name"] = "Legal";
								break;
							case 26:
								dataRow2["Name"] = "Medical";
								break;
							case 27:
								dataRow2["Name"] = "Budgeting";
								break;
							}
						}
						dataTable.Rows.Add(dataRow2);
					}
					IsNewRecord = false;
					formManager.ResetDirty();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
			if (currentData != null && currentData.Tables.Count != 0)
			{
				_ = currentData.Tables[0].Rows.Count;
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataGridList.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("ModuleID");
				dataTable.Columns.Add("Code");
				dataTable.Columns.Add("Key");
				dataTable.Columns.Add("Name");
				dataGridList.DataSource = dataTable;
				dataGridList.DisplayLayout.Bands[0].Columns["ModuleID"].Hidden = true;
			}
			catch (Exception e)
			{
				dataGridList.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private bool SaveData()
		{
			if (!IsDirty)
			{
				Close();
				return true;
			}
			if (!IsNewRecord)
			{
				switch (ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave))
				{
				case DialogResult.No:
					return true;
				case DialogResult.Cancel:
					return false;
				}
			}
			if (!ValidateData())
			{
				return false;
			}
			if (!GetData())
			{
				return false;
			}
			try
			{
				bool flag = true;
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					CompanyPreferences.LoadCompanyPreferences();
					ErrorHelper.InformationMessage("Some changes may need to restart the application in order to take effect.");
					ClearForm();
					Close();
				}
				return flag;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool ValidateData()
		{
			return true;
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			if (IsNewRecord)
			{
				ClearForm();
			}
			else if (SaveData())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		private void ClearForm()
		{
			formManager.ResetDirty();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		public void OnActivated()
		{
		}

		private void SetSecurity()
		{
		}

		private bool CanClose()
		{
			return true;
		}

		private void Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				if (!base.IsDisposed)
				{
					dataGridList.ApplyUIDesign();
					SetupGrid();
					IsNewRecord = true;
					ClearForm();
					LoadData();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			try
			{
				string text = textBoxProductKey.Text.Replace("-", "");
				bool flag = AxolonLicense.ModuleLicenseManager.SetKey(text);
				int productID = AxolonLicense.ModuleLicenseManager.License.ProductID;
				if (productID < 11 || productID > 40)
				{
					flag = false;
				}
				if (!flag)
				{
					ErrorHelper.ErrorMessage("Invalid module key.");
					base.DialogResult = DialogResult.None;
				}
				else if (Factory.CompanyInformationSystem.AddModule(text))
				{
					LoadData();
					textBoxProductKey.Clear();
					textBoxName.Clear();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			finally
			{
				base.DialogResult = DialogResult.None;
			}
		}

		private void deleteModuleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridList.ActiveRow != null && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Are you sure to delete selected module?") == DialogResult.Yes)
			{
				string moduleKey = dataGridList.ActiveRow.Cells["Key"].Value.ToString();
				if (Factory.CompanyInformationSystem.DeleteModule(moduleKey))
				{
					dataGridList.ActiveRow.Delete(displayPrompt: false);
				}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.RegisterModuleForm));
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonSave = new Micromind.UISupport.XPButton();
			formManager = new Micromind.DataControls.FormManager();
			dataGridList = new Micromind.UISupport.DataGridList(components);
			label1 = new System.Windows.Forms.Label();
			buttonAdd = new System.Windows.Forms.Button();
			label2 = new System.Windows.Forms.Label();
			textBoxName = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			textBoxProductKey = new System.Windows.Forms.MaskedTextBox();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			deleteModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			contextMenuStrip1.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 384);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(613, 40);
			panelButtons.TabIndex = 14;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(613, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(503, 7);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&Close";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 15;
			formManager.Text = "formManager1";
			formManager.Visible = false;
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
			dataGridList.Location = new System.Drawing.Point(12, 79);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(587, 299);
			dataGridList.TabIndex = 291;
			dataGridList.Text = "dataGridList1";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 11);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(66, 13);
			label1.TabIndex = 293;
			label1.Text = "Module Key:";
			buttonAdd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonAdd.Location = new System.Drawing.Point(500, 8);
			buttonAdd.Name = "buttonAdd";
			buttonAdd.Size = new System.Drawing.Size(99, 24);
			buttonAdd.TabIndex = 294;
			buttonAdd.Text = "Add";
			buttonAdd.UseVisualStyleBackColor = true;
			buttonAdd.Click += new System.EventHandler(buttonAdd_Click);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 63);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(96, 13);
			label2.TabIndex = 293;
			label2.Text = "Available Modules:";
			textBoxName.Location = new System.Drawing.Point(84, 31);
			textBoxName.Name = "textBoxName";
			textBoxName.ReadOnly = true;
			textBoxName.Size = new System.Drawing.Size(410, 20);
			textBoxName.TabIndex = 292;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(12, 34);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(38, 13);
			label3.TabIndex = 293;
			label3.Text = "Name:";
			textBoxProductKey.Location = new System.Drawing.Point(84, 8);
			textBoxProductKey.Mask = ">AAAAA-AAAAA-AAAAA-AAAAA-AAAAA-AAAAA-AAAAA";
			textBoxProductKey.Name = "textBoxProductKey";
			textBoxProductKey.Size = new System.Drawing.Size(410, 20);
			textBoxProductKey.TabIndex = 295;
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				deleteModuleToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
			deleteModuleToolStripMenuItem.Name = "deleteModuleToolStripMenuItem";
			deleteModuleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			deleteModuleToolStripMenuItem.Text = "Delete Module";
			deleteModuleToolStripMenuItem.Click += new System.EventHandler(deleteModuleToolStripMenuItem_Click);
			base.AcceptButton = buttonAdd;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonSave;
			base.ClientSize = new System.Drawing.Size(613, 424);
			base.Controls.Add(textBoxProductKey);
			base.Controls.Add(buttonAdd);
			base.Controls.Add(label2);
			base.Controls.Add(label3);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxName);
			base.Controls.Add(dataGridList);
			base.Controls.Add(panelButtons);
			base.Controls.Add(formManager);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "RegisterModuleForm";
			Text = "Register Module";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Form_FormClosing);
			base.Load += new System.EventHandler(Form_Load);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
