using Micromind.ClientLibraries;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class ChangeDueDateDialog : Form
	{
		private IContainer components;

		private Button buttonCancel;

		private Button buttonOK;

		private Label label2;

		private Line linePanelDown;

		private CustomerSelector customerSelector1;

		public ChangeDueDateDialog()
		{
			InitializeComponent();
			base.StartPosition = FormStartPosition.CenterParent;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo("This process will update the due dates for all open invoices of the selected customers. Are you sure you want to continue?") == DialogResult.No)
				{
					base.DialogResult = DialogResult.None;
				}
				else if (Factory.CustomerSystem.UpdateDueDates(customerSelector1.FromCustomer, customerSelector1.ToCustomer))
				{
					ErrorHelper.InformationMessage("Due dates updated successfully.");
					base.DialogResult = DialogResult.None;
				}
				else
				{
					ErrorHelper.ErrorMessage("An error occured during the transaction. Please try again.");
					base.DialogResult = DialogResult.None;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void ChangeDueDateDialog_Activated(object sender, EventArgs e)
		{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.ChangeDueDateDialog));
			buttonCancel = new System.Windows.Forms.Button();
			buttonOK = new System.Windows.Forms.Button();
			label2 = new System.Windows.Forms.Label();
			linePanelDown = new Micromind.UISupport.Line();
			customerSelector1 = new Micromind.DataControls.CustomerSelector();
			SuspendLayout();
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(355, 191);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(88, 25);
			buttonCancel.TabIndex = 0;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(262, 191);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(87, 25);
			buttonOK.TabIndex = 0;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(button2_Click);
			label2.Location = new System.Drawing.Point(12, 9);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(395, 36);
			label2.TabIndex = 3;
			label2.Text = "This utility will update the due dates of all unallocated invoices based on the current payment term of the customer.";
			linePanelDown.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(-41, 184);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(581, 1);
			linePanelDown.TabIndex = 15;
			linePanelDown.TabStop = false;
			customerSelector1.BackColor = System.Drawing.Color.Transparent;
			customerSelector1.CustomReportFieldName = "";
			customerSelector1.CustomReportKey = "";
			customerSelector1.CustomReportValueType = 1;
			customerSelector1.Location = new System.Drawing.Point(12, 48);
			customerSelector1.Name = "customerSelector1";
			customerSelector1.Size = new System.Drawing.Size(428, 47);
			customerSelector1.TabIndex = 16;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(451, 220);
			base.Controls.Add(customerSelector1);
			base.Controls.Add(linePanelDown);
			base.Controls.Add(label2);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ChangeDueDateDialog";
			Text = "Update Due Date";
			base.Activated += new System.EventHandler(ChangeDueDateDialog_Activated);
			ResumeLayout(false);
		}
	}
}
