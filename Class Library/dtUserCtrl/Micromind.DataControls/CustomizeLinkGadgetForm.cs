using DevExpress.LookAndFeel;
using Micromind.ClientLibraries;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class CustomizeLinkGadgetForm : Form
	{
		private List<Link> SelectedLinksColl = new List<Link>();

		private List<Link> FilterdResult = new List<Link>();

		private List<Link> LinksColl = new List<Link>();

		private IContainer components;

		private DefaultLookAndFeel defaultLookAndFeel1;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Micromind.UISupport.Line linePanelDown;

		private XPButton buttonCancel;

		private Label label2;

		private CheckedListBox listBoxItems;

		private TextBox textBoxFilter;

		private Label label1;

		public LinksCollection SelectedLinks
		{
			get
			{
				LinksCollection linksCollection = new LinksCollection();
				foreach (Link item in SelectedLinksColl)
				{
					linksCollection.Add(item);
				}
				return linksCollection;
			}
		}

		public CustomizeLinkGadgetForm()
		{
			InitializeComponent();
			base.Load += CustomizeDashboardForm_Load;
		}

		private void CustomizeDashboardForm_Load(object sender, EventArgs e)
		{
			textBoxFilter.Focus();
		}

		private void AddToSelectedColl(int index)
		{
			try
			{
				Link link = LinksColl[index];
				if (FilterdResult.Count() > 0)
				{
					link = FilterdResult[index];
				}
				else
				{
					link = LinksColl[index];
				}
				if (SelectedLinksColl.Where((Link x) => x.TargetScreen == link.TargetScreen).Count() <= 0)
				{
					SelectedLinksColl.Add(new Link
					{
						TargetScreen = link.TargetScreen,
						DisplayName = link.DisplayName,
						Group = link.Group,
						SubGroup = link.SubGroup,
						Index = link.Index
					});
				}
			}
			catch (Exception)
			{
			}
		}

		private void DeleteFromSelectedColl(int index)
		{
			string name = "";
			if (FilterdResult.Count() > 0)
			{
				name = FilterdResult[index].TargetScreen;
			}
			else
			{
				name = LinksColl[index].TargetScreen;
			}
			if (SelectedLinksColl.Where((Link x) => x.TargetScreen == name).Count() > 0)
			{
				SelectedLinksColl.Remove(SelectedLinksColl.First((Link x) => x.TargetScreen == name));
			}
		}

		private void SetCheked()
		{
			foreach (Link item in SelectedLinksColl)
			{
				string targetScreen = item.TargetScreen;
				for (int i = 0; i < listBoxItems.Items.Count; i++)
				{
					if ((listBoxItems.Items[i] as Link).TargetScreen == targetScreen)
					{
						listBoxItems.SetItemChecked(i, value: true);
					}
				}
			}
		}

		public void LoadData(LinksCollection availableLinks, LinksCollection selectedLinks)
		{
			LinksColl = availableLinks;
			LoadAvailableLinks(availableLinks);
			foreach (Link selectedLink in selectedLinks)
			{
				string targetScreen = selectedLink.TargetScreen;
				for (int i = 0; i < listBoxItems.Items.Count; i++)
				{
					if ((listBoxItems.Items[i] as Link).TargetScreen == targetScreen)
					{
						listBoxItems.SetItemChecked(i, value: true);
					}
				}
			}
		}

		private void LoadAvailableLinks(LinksCollection availableLinks)
		{
			foreach (Link availableLink in availableLinks)
			{
				listBoxItems.Items.Add(availableLink);
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void textBoxFilter_TextChanged(object sender, EventArgs e)
		{
			listBoxItems.Items.Clear();
			FilterdResult = LinksColl.Where((Link x) => x.DisplayName.ToLower().Contains(textBoxFilter.Text.ToLower())).ToList();
			foreach (Link item in FilterdResult)
			{
				listBoxItems.Items.Add(item);
			}
			SetCheked();
		}

		private void textBoxFilter_Leave(object sender, EventArgs e)
		{
		}

		private void listBoxItems_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (e.NewValue == CheckState.Checked)
			{
				AddToSelectedColl(e.Index);
			}
			else
			{
				DeleteFromSelectedColl(e.Index);
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
			defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(components);
			panelButtons = new System.Windows.Forms.Panel();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			label2 = new System.Windows.Forms.Label();
			listBoxItems = new System.Windows.Forms.CheckedListBox();
			textBoxFilter = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			panelButtons.SuspendLayout();
			SuspendLayout();
			defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2007 Blue";
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 417);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(272, 40);
			panelButtons.TabIndex = 9;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(60, 8);
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
			linePanelDown.Size = new System.Drawing.Size(272, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(162, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 4;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 61);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(204, 13);
			label2.TabIndex = 10;
			label2.Text = "Select the shortcuts that you want to add:";
			listBoxItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			listBoxItems.FormattingEnabled = true;
			listBoxItems.Location = new System.Drawing.Point(12, 77);
			listBoxItems.Name = "listBoxItems";
			listBoxItems.Size = new System.Drawing.Size(244, 334);
			listBoxItems.TabIndex = 11;
			listBoxItems.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(listBoxItems_ItemCheck);
			textBoxFilter.Location = new System.Drawing.Point(12, 38);
			textBoxFilter.Name = "textBoxFilter";
			textBoxFilter.Size = new System.Drawing.Size(244, 20);
			textBoxFilter.TabIndex = 12;
			textBoxFilter.TextChanged += new System.EventHandler(textBoxFilter_TextChanged);
			textBoxFilter.Leave += new System.EventHandler(textBoxFilter_Leave);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 19);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(32, 13);
			label1.TabIndex = 13;
			label1.Text = "Filter:";
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(272, 457);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxFilter);
			base.Controls.Add(listBoxItems);
			base.Controls.Add(label2);
			base.Controls.Add(panelButtons);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CustomizeLinkGadgetForm";
			base.ShowIcon = false;
			Text = "Customize Shortcuts";
			panelButtons.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
