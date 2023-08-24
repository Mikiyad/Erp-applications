using Micromind.ClientLibraries;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class ListSelectionForm : Form
	{
		private enum Transactions
		{
			SalesQt = 1,
			SalesOrder,
			DeliveryNote,
			SalesInvoice
		}

		private enum Tables
		{
			SALES_QUOTE = 1,
			SALES_ORDER,
			DELIVERY_NOTE,
			SALES_INVOICE
		}

		public int CurrentIndex;

		public string SysDocId = "";

		public string VoucherNo = "";

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Line linePanelDown;

		private XPButton buttonCancel;

		private Panel panel1;

		private Panel panel2;

		private Label label2;

		private TextBox txtVoucherNumber;

		private Label label1;

		private TreeView treeViewList;

		private TextBox txtSysDocID;

		public ListSelectionForm(int index)
		{
			InitializeComponent();
			base.Load += SelectDocumentDialog_Load;
			CurrentIndex = index;
			SysDocId = txtSysDocID.Text;
			VoucherNo = txtVoucherNumber.Text;
		}

		private void SelectDocumentDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				Global.GlobalSettings.SaveFormProperties(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SelectDocumentDialog_Activated(object sender, EventArgs e)
		{
		}

		private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
		{
		}

		private void SelectDocumentDialog_Load(object sender, EventArgs e)
		{
			try
			{
				Global.GlobalSettings.LoadFormProperties(this);
				AddTransaction();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void textBoxSearch_TextChanged(object sender, EventArgs e)
		{
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
		}

		private void createTable()
		{
			new DataTable
			{
				Columns = 
				{
					"Transaction",
					"TableName",
					"Column",
					"SOSysDocID",
					"SOVoucherID",
					{
						"SORowIndex",
						typeof(int)
					},
					"Description"
				}
			};
		}

		private void AddTransaction()
		{
			for (int num = CurrentIndex; num > 0; num = checked(num - 1))
			{
				string name = Enum.GetName(typeof(Transactions), num);
				TreeNode treeNode = treeViewList.Nodes.Add(name.ToString(), name.ToString(), 0, 0);
				DataSet dataSet = new DataSet();
				string text = "";
				switch (name.ToString())
				{
				case "SalesInvoice":
					dataSet = new DataSet();
					text = Enum.GetName(typeof(Tables), num);
					dataSet = Factory.JournalSystem.GetDetails(text, SysDocId, VoucherNo);
					treeNode.Nodes.Add("SYSDOCID:" + dataSet.Tables[0].Rows[0]["SYSDOCID"]);
					treeNode.Nodes.Add("DATE:" + dataSet.Tables[0].Rows[0]["TRANSACTIONDATE"]);
					treeNode.Nodes.Add("AMOUNT:" + dataSet.Tables[0].Rows[0]["SYSDOCID"]);
					break;
				case "DeliveryNote":
					AddNode(treeNode, num, VoucherNo);
					VoucherNo = getReference("Reference", SysDocId, VoucherNo, num);
					break;
				case "Sales Order":
					dataSet = new DataSet();
					text = Enum.GetName(typeof(Tables), num);
					dataSet = Factory.JournalSystem.GetDetails(text, SysDocId, VoucherNo);
					treeNode.Nodes.Add("SYSDOCID:" + dataSet.Tables[0].Rows[0]["SYSDOCID"]);
					treeNode.Nodes.Add("DATE:" + dataSet.Tables[0].Rows[0]["TRANSACTIONDATE"]);
					treeNode.Nodes.Add("AMOUNT:" + dataSet.Tables[0].Rows[0]["SYSDOCID"]);
					break;
				}
			}
		}

		private string getReference(string ColumnName, string SysdocId, string VoucherNo, int TableIndex)
		{
			string name = Enum.GetName(typeof(Tables), TableIndex);
			DataSet dataSet = new DataSet();
			dataSet = Factory.WorkOrderSystem.GetReference(name, ColumnName, SysDocId, VoucherNo);
			string result = "";
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				result = dataSet.Tables[0].Rows[0][ColumnName].ToString();
			}
			return result;
		}

		private void AddNode(TreeNode parent, int TableIndex, string Reference)
		{
			DataSet dataSet = new DataSet();
			string name = Enum.GetName(typeof(Tables), TableIndex);
			dataSet = Factory.WorkOrderSystem.GetDetails(name, SysDocId, VoucherNo);
			parent.Nodes.Add("SYSDOCID:" + dataSet.Tables[0].Rows[0]["SYSDOCID"]);
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
			panelButtons = new System.Windows.Forms.Panel();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			panel1 = new System.Windows.Forms.Panel();
			treeViewList = new System.Windows.Forms.TreeView();
			panel2 = new System.Windows.Forms.Panel();
			txtSysDocID = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			txtVoucherNumber = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			panelButtons.SuspendLayout();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 389);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(582, 40);
			panelButtons.TabIndex = 2;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(370, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 1;
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
			linePanelDown.Size = new System.Drawing.Size(582, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(472, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 2;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			panel1.Controls.Add(treeViewList);
			panel1.Controls.Add(panel2);
			panel1.Dock = System.Windows.Forms.DockStyle.Left;
			panel1.Location = new System.Drawing.Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(228, 389);
			panel1.TabIndex = 3;
			treeViewList.AllowDrop = true;
			treeViewList.Dock = System.Windows.Forms.DockStyle.Fill;
			treeViewList.FullRowSelect = true;
			treeViewList.HideSelection = false;
			treeViewList.Location = new System.Drawing.Point(0, 66);
			treeViewList.Name = "treeViewList";
			treeViewList.Size = new System.Drawing.Size(228, 323);
			treeViewList.TabIndex = 292;
			panel2.Controls.Add(txtSysDocID);
			panel2.Controls.Add(label2);
			panel2.Controls.Add(txtVoucherNumber);
			panel2.Controls.Add(label1);
			panel2.Dock = System.Windows.Forms.DockStyle.Top;
			panel2.Location = new System.Drawing.Point(0, 0);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(228, 66);
			panel2.TabIndex = 0;
			txtSysDocID.Location = new System.Drawing.Point(90, 12);
			txtSysDocID.Name = "txtSysDocID";
			txtSysDocID.Size = new System.Drawing.Size(136, 20);
			txtSysDocID.TabIndex = 4;
			txtSysDocID.Text = "DNA";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(3, 39);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(87, 13);
			label2.TabIndex = 3;
			label2.Text = "VoucherNumber:";
			txtVoucherNumber.Location = new System.Drawing.Point(90, 36);
			txtVoucherNumber.Name = "txtVoucherNumber";
			txtVoucherNumber.Size = new System.Drawing.Size(136, 20);
			txtVoucherNumber.TabIndex = 2;
			txtVoucherNumber.Text = "AW000267";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(3, 15);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(58, 13);
			label1.TabIndex = 0;
			label1.Text = "SysDocID:";
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(582, 429);
			base.Controls.Add(panel1);
			base.Controls.Add(panelButtons);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(465, 215);
			base.Name = "ListSelectionForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "List Selection";
			panelButtons.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			ResumeLayout(false);
		}
	}
}
