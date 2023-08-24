using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataControls.Properties;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls.OtherControls
{
	public class ApprovalPanel : UserControl
	{
		private int approvalTaskID = -1;

		private IContainer components;

		private Micromind.UISupport.Line line1;

		private Button buttonApprove;

		private Button buttonReject;

		public string TableName
		{
			get;
			set;
		}

		public string IDColumnName
		{
			get;
			set;
		}

		public int ApprovalTaskID
		{
			get
			{
				return approvalTaskID;
			}
			set
			{
				approvalTaskID = value;
			}
		}

		public ApprovalPanel()
		{
			InitializeComponent();
		}

		private void buttonApprove_Click(object sender, EventArgs e)
		{
			try
			{
				Factory.ApprovalSystem.ApproveTask(approvalTaskID, TableName, IDColumnName);
				base.ParentForm?.Close();
				GlobalEvents.OnApprovalStatusChanged(ApprovalStatus.Approved, approvalTaskID);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonReject_Click(object sender, EventArgs e)
		{
			try
			{
				Factory.ApprovalSystem.RejectTask(approvalTaskID, TableName, IDColumnName);
				base.ParentForm?.Close();
				GlobalEvents.OnApprovalStatusChanged(ApprovalStatus.Rejected, approvalTaskID);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
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
			buttonReject = new System.Windows.Forms.Button();
			buttonApprove = new System.Windows.Forms.Button();
			line1 = new Micromind.UISupport.Line();
			SuspendLayout();
			buttonReject.Image = Micromind.DataControls.Properties.Resources.delete;
			buttonReject.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
			buttonReject.Location = new System.Drawing.Point(115, 4);
			buttonReject.Name = "buttonReject";
			buttonReject.Size = new System.Drawing.Size(97, 25);
			buttonReject.TabIndex = 1;
			buttonReject.Text = "Reject";
			buttonReject.UseVisualStyleBackColor = true;
			buttonReject.Click += new System.EventHandler(buttonReject_Click);
			buttonApprove.Image = Micromind.DataControls.Properties.Resources.check;
			buttonApprove.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
			buttonApprove.Location = new System.Drawing.Point(16, 4);
			buttonApprove.Name = "buttonApprove";
			buttonApprove.Size = new System.Drawing.Size(93, 25);
			buttonApprove.TabIndex = 1;
			buttonApprove.Text = "Approve";
			buttonApprove.UseVisualStyleBackColor = true;
			buttonApprove.Click += new System.EventHandler(buttonApprove_Click);
			line1.BackColor = System.Drawing.Color.White;
			line1.Dock = System.Windows.Forms.DockStyle.Top;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(0, 0);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(523, 1);
			line1.TabIndex = 0;
			line1.TabStop = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(buttonReject);
			base.Controls.Add(buttonApprove);
			base.Controls.Add(line1);
			base.Name = "ApprovalPanel";
			base.Size = new System.Drawing.Size(523, 32);
			ResumeLayout(false);
		}
	}
}
