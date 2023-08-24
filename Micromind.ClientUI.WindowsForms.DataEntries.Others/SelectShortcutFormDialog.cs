using Infragistics.Win.UltraWinTree;
using Micromind.ClientUI.Libraries;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class SelectShortcutFormDialog : Form
	{
		public bool CanClose = true;

		private bool isMultiSelect;

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Line linePanelDown;

		private XPButton buttonCancel;

		private UltraTree dataGridItems;

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

		public UltraTreeNode SelectedNode
		{
			get
			{
				if (base.DialogResult == DialogResult.Cancel)
				{
					return null;
				}
				if (dataGridItems.SelectedNodes.Count > 0)
				{
					return dataGridItems.SelectedNodes[0];
				}
				return null;
			}
		}

		public string SelectedKey
		{
			get
			{
				if (base.DialogResult == DialogResult.Cancel)
				{
					return null;
				}
				if (dataGridItems.SelectedNodes.Count > 0)
				{
					if (dataGridItems.SelectedNodes[0].Nodes.Count == 0)
					{
						return dataGridItems.SelectedNodes[0].Key;
					}
					return "";
				}
				return "";
			}
		}

		public string SelectedText
		{
			get
			{
				if (base.DialogResult == DialogResult.Cancel)
				{
					return null;
				}
				if (dataGridItems.SelectedNodes.Count > 0)
				{
					if (dataGridItems.SelectedNodes[0].Nodes.Count == 0)
					{
						return dataGridItems.SelectedNodes[0].Text;
					}
					return "";
				}
				return "";
			}
		}

		public event EventHandler ValidateSelection;

		public SelectShortcutFormDialog()
		{
			InitializeComponent();
			base.Load += SelectShortcutFormDialog_Load;
			base.Activated += SelectShortcutFormDialog_Activated;
			dataGridItems.DoubleClick += dataGridItems_DoubleClick;
		}

		private void dataGridItems_DoubleClick(object sender, EventArgs e)
		{
			if (dataGridItems.SelectedNodes.Count != 0 && dataGridItems.SelectedNodes[0].Nodes.Count == 0)
			{
				SelectItem();
			}
		}

		private void SelectShortcutFormDialog_Activated(object sender, EventArgs e)
		{
		}

		private void SelectShortcutFormDialog_Load(object sender, EventArgs e)
		{
			checked
			{
				try
				{
					DataTable dataForms = new FormHelper().GetDataForms();
					DataRow[] array = dataForms.Select("FormCategory='Account'");
					for (int i = 0; i < array.Length; i++)
					{
						dataGridItems.Nodes["Account"].Nodes.Add(array[i]["FormName"].ToString(), array[i]["FormText"].ToString());
					}
					array = dataForms.Select("FormCategory='Customer'");
					for (int j = 0; j < array.Length; j++)
					{
						dataGridItems.Nodes["Customer"].Nodes.Add(array[j]["FormName"].ToString(), array[j]["FormText"].ToString());
					}
					array = dataForms.Select("FormCategory='Vendor'");
					for (int k = 0; k < array.Length; k++)
					{
						dataGridItems.Nodes["Vendor"].Nodes.Add(array[k]["FormName"].ToString(), array[k]["FormText"].ToString());
					}
					array = dataForms.Select("FormCategory='Inventory'");
					for (int l = 0; l < array.Length; l++)
					{
						dataGridItems.Nodes["Inventory"].Nodes.Add(array[l]["FormName"].ToString(), array[l]["FormText"].ToString());
					}
					array = dataForms.Select("FormCategory='HR'");
					for (int m = 0; m < array.Length; m++)
					{
						dataGridItems.Nodes["HR"].Nodes.Add(array[m]["FormName"].ToString(), array[m]["FormText"].ToString());
					}
					array = dataForms.Select("FormCategory='Report' AND FormSubCategory='Account'");
					dataGridItems.Nodes["Report"].Nodes.Add("RAccount", "Account");
					for (int n = 0; n < array.Length; n++)
					{
						dataGridItems.Nodes["Report"].Nodes["RAccount"].Nodes.Add(array[n]["FormName"].ToString(), array[n]["FormText"].ToString());
					}
					array = dataForms.Select("FormCategory='Report' AND FormSubCategory='Customer'");
					dataGridItems.Nodes["Report"].Nodes.Add("RCustomer", "Customer");
					for (int num = 0; num < array.Length; num++)
					{
						dataGridItems.Nodes["Report"].Nodes["RCustomer"].Nodes.Add(array[num]["FormName"].ToString(), array[num]["FormText"].ToString());
					}
					array = dataForms.Select("FormCategory='Report' AND FormSubCategory='Vendor'");
					dataGridItems.Nodes["Report"].Nodes.Add("RVendor", "Vendor");
					for (int num2 = 0; num2 < array.Length; num2++)
					{
						dataGridItems.Nodes["Report"].Nodes["RVendor"].Nodes.Add(array[num2]["FormName"].ToString(), array[num2]["FormText"].ToString());
					}
					array = dataForms.Select("FormCategory='Report' AND FormSubCategory='Inventory'");
					dataGridItems.Nodes["Report"].Nodes.Add("RInventory", "Inventory");
					for (int num3 = 0; num3 < array.Length; num3++)
					{
						dataGridItems.Nodes["Report"].Nodes["RInventory"].Nodes.Add(array[num3]["FormName"].ToString(), array[num3]["FormText"].ToString());
					}
					array = dataForms.Select("FormCategory='Report' AND FormSubCategory='HR'");
					dataGridItems.Nodes["Report"].Nodes.Add("RHR", "HR");
					for (int num4 = 0; num4 < array.Length; num4++)
					{
						dataGridItems.Nodes["Report"].Nodes["RHR"].Nodes.Add(array[num4]["FormName"].ToString(), array[num4]["FormText"].ToString());
					}
				}
				catch
				{
				}
			}
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
			CanClose = true;
			if (SelectedNode != null)
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			SelectItem();
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
			Infragistics.Win.UltraWinTree.UltraTreeNode ultraTreeNode = new Infragistics.Win.UltraWinTree.UltraTreeNode();
			Infragistics.Win.UltraWinTree.UltraTreeNode ultraTreeNode2 = new Infragistics.Win.UltraWinTree.UltraTreeNode();
			Infragistics.Win.UltraWinTree.UltraTreeNode ultraTreeNode3 = new Infragistics.Win.UltraWinTree.UltraTreeNode();
			Infragistics.Win.UltraWinTree.UltraTreeNode ultraTreeNode4 = new Infragistics.Win.UltraWinTree.UltraTreeNode();
			Infragistics.Win.UltraWinTree.UltraTreeNode ultraTreeNode5 = new Infragistics.Win.UltraWinTree.UltraTreeNode();
			Infragistics.Win.UltraWinTree.UltraTreeNode ultraTreeNode6 = new Infragistics.Win.UltraWinTree.UltraTreeNode();
			panelButtons = new System.Windows.Forms.Panel();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			dataGridItems = new Infragistics.Win.UltraWinTree.UltraTree();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 270);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(505, 40);
			panelButtons.TabIndex = 3;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(293, 8);
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
			linePanelDown.Size = new System.Drawing.Size(505, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(395, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 4;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			dataGridItems.ImageTransparentColor = System.Drawing.Color.Transparent;
			dataGridItems.Location = new System.Drawing.Point(12, 12);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.NodeConnectorColor = System.Drawing.SystemColors.ControlDark;
			ultraTreeNode.Key = "Account";
			ultraTreeNode.Text = "Account";
			ultraTreeNode2.Key = "Customer";
			ultraTreeNode2.Text = "Customer";
			ultraTreeNode3.Key = "Vendor";
			ultraTreeNode3.Text = "Vendor";
			ultraTreeNode4.Key = "Inventory";
			ultraTreeNode4.Text = "Inventory";
			ultraTreeNode5.Key = "HR";
			ultraTreeNode5.Text = "HR";
			ultraTreeNode6.Key = "Report";
			ultraTreeNode6.Text = "Report";
			dataGridItems.Nodes.AddRange(new Infragistics.Win.UltraWinTree.UltraTreeNode[6]
			{
				ultraTreeNode,
				ultraTreeNode2,
				ultraTreeNode3,
				ultraTreeNode4,
				ultraTreeNode5,
				ultraTreeNode6
			});
			dataGridItems.Size = new System.Drawing.Size(479, 252);
			dataGridItems.TabIndex = 15;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(505, 310);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(panelButtons);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(465, 215);
			base.Name = "SelectShortcutFormDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Select Shortcut";
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
		}
	}
}
