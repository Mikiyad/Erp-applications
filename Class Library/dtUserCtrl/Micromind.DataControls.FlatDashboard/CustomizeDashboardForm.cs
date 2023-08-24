using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.WinExplorer;
using DevExpress.XtraLayout;
using Micromind.ClientLibraries;
using Micromind.ClientUI.WindowsForms.DataEntries;
using Micromind.DataControls.Properties;
using Micromind.UISupport;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.DataControls.FlatDashboard
{
	public class CustomizeDashboardForm : Form
	{
		private string key = "";

		private IContainer components;

		private DefaultLookAndFeel defaultLookAndFeel1;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Micromind.UISupport.Line linePanelDown;

		private XPButton buttonCancel;

		private OpenFileDialog openFileDialog1;

		private SaveFileDialog saveFileDialog1;

		private LayoutControl layoutControl1;

		private LayoutControlGroup layoutControlGroup1;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonAdd;

		private ToolStripButton toolStripButtonRemove;

		private ToolStripButton toolStripButton1;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButton2;

		private ToolStripButton toolStripButtonSaveTemplate;

		private ToolStripButton toolStripButton3;

		public string DashboardKey
		{
			get
			{
				return key;
			}
			set
			{
				key = value;
			}
		}

		public CustomizeDashboardForm()
		{
			InitializeComponent();
			if (key == "")
			{
				string str = base.Name;
				if (base.Name.Length > 11)
				{
					str = base.Name.Substring(0, 10);
				}
				key = "DSHB" + str;
			}
			layoutControl1.HideCustomization += LayoutControl1_HideCustomization;
			base.Load += CustomizeDashboardForm_Load;
		}

		private void CustomizeDashboardForm_Load(object sender, EventArgs e)
		{
			((ILayoutControl)layoutControl1).EnableCustomizationForm = false;
			((ILayoutControl)layoutControl1).EnableCustomizationMode = true;
			SetupLayout();
			LoadLayout();
		}

		public bool LoadLayout()
		{
			try
			{
				bool result = true;
				DataSet dashboardByID = Factory.DashboardSystem.GetDashboardByID(key, Global.CurrentUser);
				if (dashboardByID.Tables.Count == 0 || dashboardByID.Tables[0].Rows.Count == 0)
				{
					return false;
				}
				if (dashboardByID.Tables[0].Rows[0]["Layout"].IsDBNullOrEmpty())
				{
					return false;
				}
				DashboardLayout dashboardLayout = (DashboardLayout)Global.DeserializeFromStream((byte[])dashboardByID.Tables[0].Rows[0]["Layout"]);
				foreach (GroupLayout gadgets in dashboardLayout.GadgetsList)
				{
					if (gadgets != null)
					{
						GadgetContainer gadgetContainer = new GadgetContainer();
						gadgetContainer.Name = gadgets.Code;
						gadgetContainer.Title = gadgets.Title;
						LayoutControlItem layoutControlItem = layoutControl1.Root.AddItem();
						layoutControlItem.Name = gadgets.Code;
						layoutControlItem.Control = gadgetContainer;
						layoutControlItem.Text = gadgetContainer.Title;
						layoutControlItem.Tag = gadgets;
						layoutControlItem.TextVisible = false;
					}
				}
				dashboardLayout.Layout.Seek(0L, SeekOrigin.Begin);
				layoutControl1.RestoreLayoutFromStream(dashboardLayout.Layout);
				return result;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool FillCustomLayout(byte[] layoutData)
		{
			try
			{
				return true;
			}
			catch
			{
				throw;
			}
		}

		private byte[] GetData()
		{
			return GadgetsHelper.SerializeToStream(new DashboardLayout()).ToArray();
		}

		public bool SaveLayout()
		{
			try
			{
				bool result = true;
				MemoryStream memoryStream = new MemoryStream();
				layoutControl1.SaveLayoutToStream(memoryStream);
				DashboardLayout dashboardLayout = new DashboardLayout();
				dashboardLayout.Layout = memoryStream;
				foreach (LayoutControlItem item2 in layoutControl1.Root.Items)
				{
					GroupLayout item = item2.Tag as GroupLayout;
					dashboardLayout.GadgetsList.Add(item);
				}
				MemoryStream memoryStream2 = Global.SerializeToStream(dashboardLayout);
				Factory.DashboardSystem.SaveLayout(DashboardKey, memoryStream2.ToArray());
				return result;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				if (SaveLayout())
				{
					base.DialogResult = DialogResult.OK;
					Close();
				}
				else
				{
					ErrorHelper.ErrorMessage("Cannot save the layout.", "Please try again.");
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonExport_Click(object sender, EventArgs e)
		{
			try
			{
				byte[] data = GetData();
				saveFileDialog1.AddExtension = true;
				saveFileDialog1.Filter = "Axolon Dashboard |*.axd";
				saveFileDialog1.FileName = "Dashboard";
				if (saveFileDialog1.ShowDialog() == DialogResult.OK && data != null)
				{
					new PublicFunctions().ByteArrayToFile(saveFileDialog1.FileName, data);
					ErrorHelper.InformationMessage("Custom report exported successfully.");
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{
		}

		private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
		{
		}

		private void checkBoxThreeColumn_CheckedChanged(object sender, EventArgs e)
		{
			SetupLayout();
		}

		private void SetupLayout()
		{
			layoutControl1.BackColor = Color.WhiteSmoke;
		}

		private void checkBoxCrossColumn_CheckedChanged(object sender, EventArgs e)
		{
			SetupLayout();
		}

		private void CustomizeDashboardForm_Load_1(object sender, EventArgs e)
		{
		}

		private void barButtonItemNewGroup_ItemClick(object sender, ItemClickEventArgs e)
		{
		}

		private void Form_GadgetSelected(object sender, EventArgs e)
		{
			try
			{
				WinExplorerView winExplorerView = sender as WinExplorerView;
				GridRow row = (e as WinExplorerViewItemDoubleClickEventArgs).ItemInfo.Row;
				GadgetContainer gadgetContainer = new GadgetContainer();
				gadgetContainer.Name = winExplorerView.GetRowCellValue(row.RowHandle, "Code").ToString();
				gadgetContainer.ShowRefreshButton = false;
				gadgetContainer.Title = winExplorerView.GetRowCellValue(row.RowHandle, "Title").ToString();
				gadgetContainer.GadgetLayout = (GroupLayout)Global.DeserializeFromStream((byte[])winExplorerView.GetRowCellValue(row.RowHandle, "Layout"));
				LayoutControlItem layoutControlItem = layoutControl1.Root.AddItem();
				layoutControlItem.Name = gadgetContainer.Name;
				layoutControlItem.Control = gadgetContainer;
				layoutControlItem.Text = gadgetContainer.Title;
				layoutControlItem.Tag = gadgetContainer.GadgetLayout;
				layoutControlItem.TextVisible = false;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void barButtonItemDelete_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				if (layoutControl1.Root.SelectedItems.Count > 0)
				{
					LayoutControlItem obj = (LayoutControlItem)layoutControl1.Root.SelectedItems[0];
					obj.Control.Parent = null;
					obj.Control.Dispose();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			try
			{
				((ILayoutControl)layoutControl1).EnableCustomizationForm = false;
				((ILayoutControl)layoutControl1).EnableCustomizationMode = true;
				ArrayList arrayList = new ArrayList();
				foreach (LayoutControlItem item in layoutControl1.Root.Items)
				{
					arrayList.Add(item.Name);
				}
				AddDashboardGadgetForm addDashboardGadgetForm = new AddDashboardGadgetForm();
				addDashboardGadgetForm.UsedGadgets = arrayList;
				addDashboardGadgetForm.GadgetSelected += Form_GadgetSelected;
				addDashboardGadgetForm.ShowDialog();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButtonRemove_Click(object sender, EventArgs e)
		{
			try
			{
				if (layoutControl1.Root.SelectedItems.Count != 0)
				{
					LayoutControlItem obj = (LayoutControlItem)layoutControl1.Root.SelectedItems[0];
					obj.Control.Dispose();
					obj.Dispose();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButton1_Click_1(object sender, EventArgs e)
		{
			((ILayoutControl)layoutControl1).EnableCustomizationForm = true;
			((ILayoutControl)layoutControl1).EnableCustomizationMode = false;
			layoutControl1.OptionsCustomizationForm.ShowSaveButton = false;
			layoutControl1.OptionsCustomizationForm.ShowLoadButton = false;
			layoutControl1.ShowCustomizationForm();
			layoutControl1.PopupMenuShowing += LayoutControl1_PopupMenuShowing;
		}

		private void LayoutControl1_HideCustomization(object sender, EventArgs e)
		{
			((ILayoutControl)layoutControl1).EnableCustomizationForm = false;
			((ILayoutControl)layoutControl1).EnableCustomizationMode = true;
		}

		private void LayoutControl1_PopupMenuShowing(object sender, DevExpress.XtraLayout.PopupMenuShowingEventArgs e)
		{
			for (int i = 0; i < e.Menu.Items.Count; i++)
			{
				switch (e.Menu.Items[i].Caption)
				{
				case "Size Constraints":
				case "Group":
				case "Create Tabbed Group":
				case "Add Empty Space Item":
				case "Show Text":
				case "Convert to Table Layout":
					e.Menu.Items[i].Visible = false;
					break;
				}
			}
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			layoutControl1.BestFit();
		}

		private void toolStripButtonSaveTemplate_Click(object sender, EventArgs e)
		{
			EnterNameDialog enterNameDialog = new EnterNameDialog();
			if (enterNameDialog.ShowDialog() == DialogResult.OK)
			{
				string enteredName = enterNameDialog.EnteredName;
				try
				{
					MemoryStream memoryStream = new MemoryStream();
					layoutControl1.SaveLayoutToStream(memoryStream);
					DashboardLayout dashboardLayout = new DashboardLayout();
					dashboardLayout.Layout = memoryStream;
					foreach (LayoutControlItem item2 in layoutControl1.Root.Items)
					{
						GroupLayout item = item2.Tag as GroupLayout;
						dashboardLayout.GadgetsList.Add(item);
					}
					MemoryStream memoryStream2 = Global.SerializeToStream(dashboardLayout);
					Factory.DashboardSystem.SaveLayoutTemplate(enteredName, memoryStream2.ToArray(), enteredName);
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			string id = "";
			SelectDashboardTemplateDialog selectDashboardTemplateDialog = new SelectDashboardTemplateDialog();
			selectDashboardTemplateDialog.IsLoad = true;
			selectDashboardTemplateDialog.ShowDialog();
			if (selectDashboardTemplateDialog.DialogResult == DialogResult.OK)
			{
				id = selectDashboardTemplateDialog.TemplateName;
			}
			((ILayoutControl)layoutControl1).EnableCustomizationForm = false;
			((ILayoutControl)layoutControl1).EnableCustomizationMode = true;
			SetupLayout();
			DataSet dashboardByID = Factory.DashboardSystem.GetDashboardByID(id, "SYS_TMPLT");
			if (dashboardByID.Tables.Count != 0 && dashboardByID.Tables[0].Rows.Count != 0 && !dashboardByID.Tables[0].Rows[0]["Layout"].IsDBNullOrEmpty())
			{
				DashboardLayout dashboardLayout = (DashboardLayout)Global.DeserializeFromStream((byte[])dashboardByID.Tables[0].Rows[0]["Layout"]);
				foreach (GroupLayout gadgets in dashboardLayout.GadgetsList)
				{
					if (gadgets != null)
					{
						GadgetContainer gadgetContainer = new GadgetContainer();
						gadgetContainer.Name = gadgets.Code;
						gadgetContainer.Title = gadgets.Title;
						LayoutControlItem layoutControlItem = layoutControl1.Root.AddItem();
						layoutControlItem.Name = gadgets.Code;
						layoutControlItem.Control = gadgetContainer;
						layoutControlItem.Text = gadgetContainer.Title;
						layoutControlItem.Tag = gadgets;
						layoutControlItem.TextVisible = false;
					}
				}
				dashboardLayout.Layout.Seek(0L, SeekOrigin.Begin);
				layoutControl1.RestoreLayoutFromStream(dashboardLayout.Layout);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.DataControls.FlatDashboard.CustomizeDashboardForm));
			defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel();
			layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			panelButtons = new System.Windows.Forms.Panel();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
			toolStripButtonRemove = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			toolStripButtonSaveTemplate = new System.Windows.Forms.ToolStripButton();
			toolStripButton3 = new System.Windows.Forms.ToolStripButton();
			((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			SuspendLayout();
			defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2007 Blue";
			layoutControl1.BackColor = System.Drawing.Color.Gainsboro;
			layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			layoutControl1.Location = new System.Drawing.Point(0, 31);
			layoutControl1.Name = "layoutControl1";
			layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
			layoutControl1.Root = layoutControlGroup1;
			layoutControl1.Size = new System.Drawing.Size(1035, 459);
			layoutControl1.TabIndex = 9;
			layoutControl1.Text = "layoutControl1";
			layoutControlGroup1.AppearanceGroup.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
			layoutControlGroup1.AppearanceGroup.Options.UseBackColor = true;
			layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			layoutControlGroup1.GroupBordersVisible = false;
			layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			layoutControlGroup1.Name = "layoutControlGroup1";
			layoutControlGroup1.Size = new System.Drawing.Size(1035, 459);
			layoutControlGroup1.TextVisible = false;
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 490);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(1035, 40);
			panelButtons.TabIndex = 9;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(823, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 0;
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
			linePanelDown.Size = new System.Drawing.Size(1035, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(925, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 1;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			openFileDialog1.FileName = "openFileDialog1";
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[8]
			{
				toolStripButtonPrint,
				toolStripButtonAdd,
				toolStripButtonRemove,
				toolStripSeparator1,
				toolStripButton1,
				toolStripButton2,
				toolStripButtonSaveTemplate,
				toolStripButton3
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(1035, 31);
			toolStrip1.TabIndex = 10;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(36, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
			toolStripButtonAdd.Image = Micromind.DataControls.Properties.Resources.add;
			toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAdd.Name = "toolStripButtonAdd";
			toolStripButtonAdd.Size = new System.Drawing.Size(98, 28);
			toolStripButtonAdd.Text = "Add Gadget";
			toolStripButtonAdd.Click += new System.EventHandler(toolStripButton1_Click);
			toolStripButtonRemove.Image = Micromind.DataControls.Properties.Resources.delete;
			toolStripButtonRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonRemove.Name = "toolStripButtonRemove";
			toolStripButtonRemove.Size = new System.Drawing.Size(119, 28);
			toolStripButtonRemove.Text = "Remove Gadget";
			toolStripButtonRemove.Click += new System.EventHandler(toolStripButtonRemove_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripButton1.Image = Micromind.DataControls.Properties.Resources.option;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(91, 28);
			toolStripButton1.Text = "Customize";
			toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click_1);
			toolStripButton2.Image = Micromind.DataControls.Properties.Resources.fullscreen2;
			toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton2.Name = "toolStripButton2";
			toolStripButton2.Size = new System.Drawing.Size(73, 28);
			toolStripButton2.Text = "Best Fit";
			toolStripButton2.Click += new System.EventHandler(toolStripButton2_Click);
			toolStripButtonSaveTemplate.Image = Micromind.DataControls.Properties.Resources.saveasicon;
			toolStripButtonSaveTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonSaveTemplate.Name = "toolStripButtonSaveTemplate";
			toolStripButtonSaveTemplate.Size = new System.Drawing.Size(127, 28);
			toolStripButtonSaveTemplate.Text = "Save As Template";
			toolStripButtonSaveTemplate.Click += new System.EventHandler(toolStripButtonSaveTemplate_Click);
			toolStripButton3.Image = Micromind.DataControls.Properties.Resources.saveasicon;
			toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton3.Name = "toolStripButton3";
			toolStripButton3.Size = new System.Drawing.Size(113, 28);
			toolStripButton3.Text = "Load Template";
			toolStripButton3.Click += new System.EventHandler(toolStripButton3_Click);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(1035, 530);
			base.Controls.Add(layoutControl1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CustomizeDashboardForm";
			Text = "Customize Dashboard";
			base.Load += new System.EventHandler(CustomizeDashboardForm_Load_1);
			((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
