using Micromind.ClientLibraries;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Vendors
{
	public class UpdatePOStatusDialog : Form
	{
		public SysDocTypes SysDocumentType = SysDocTypes.None;

		private string sysDocID = "";

		private string voucherID = "";

		private IContainer components;

		private Button buttonCancel;

		private Button buttonOK;

		private Line linePanelDown;

		private Label label2;

		private Label label1;

		private POStatusComboBox comboBoxStatus;

		public PurchaseOrderStatus Status
		{
			get
			{
				return PurchaseOrderStatus.Open;
			}
			set
			{
				comboBoxStatus.SelectedStatus = value;
			}
		}

		public UpdatePOStatusDialog()
		{
			InitializeComponent();
			base.Load += ClosePeriodDialog_Load;
			base.StartPosition = FormStartPosition.CenterParent;
		}

		public void SetDocument(SysDocTypes DocumentType, string sysDocID, string voucherID)
		{
			this.sysDocID = sysDocID;
			this.voucherID = voucherID;
			SysDocumentType = DocumentType;
			object obj = null;
			switch (DocumentType)
			{
			case SysDocTypes.PurchaseOrder:
			case SysDocTypes.ImportPurchaseOrder:
				obj = Factory.DatabaseSystem.GetFieldValue("Purchase_Order", "Status", "SysDocID", sysDocID, "VoucherID", voucherID);
				break;
			case SysDocTypes.PackingList:
				obj = Factory.DatabaseSystem.GetFieldValue("PO_Shipment", "Status", "SysDocID", sysDocID, "VoucherID", voucherID);
				break;
			default:
				switch (DocumentType)
				{
				case SysDocTypes.PackingList:
					obj = Factory.DatabaseSystem.GetFieldValue("PO_Shipment", "Status", "SysDocID", sysDocID, "VoucherID", voucherID);
					break;
				case SysDocTypes.JobMaterialRequisition:
					obj = Factory.DatabaseSystem.GetFieldValue("Job_Material_Requisition", "Status", "SysDocID", sysDocID, "VoucherID", voucherID);
					break;
				case SysDocTypes.SalesOrder:
				case SysDocTypes.ExportSalesOrder:
					obj = Factory.DatabaseSystem.GetFieldValue("Sales_Order", "Status", "SysDocID", sysDocID, "VoucherID", voucherID);
					break;
				}
				break;
			}
			if (obj != null && DocumentType != SysDocTypes.JobMaterialRequisition)
			{
				comboBoxStatus.SelectedStatus = (PurchaseOrderStatus)int.Parse(obj.ToString());
				label2.Text = "Please select the new status for this purchase order:";
				comboBoxStatus.LoadData();
			}
			if (obj != null && DocumentType == SysDocTypes.SalesOrder)
			{
				comboBoxStatus.SelectedStatus = (PurchaseOrderStatus)int.Parse(obj.ToString());
				label2.Text = "Please select the new status for this Sales Order:";
				comboBoxStatus.LoadRequestStatus();
			}
			if (obj != null && DocumentType == SysDocTypes.JobMaterialRequisition)
			{
				comboBoxStatus.SelectedStatus = (PurchaseOrderStatus)int.Parse(obj.ToString());
				label2.Text = "Please select the new status for this Material Request:";
				comboBoxStatus.LoadRequestStatus();
			}
			if (obj != null && DocumentType == SysDocTypes.ExportSalesOrder)
			{
				comboBoxStatus.SelectedStatus = (PurchaseOrderStatus)int.Parse(obj.ToString());
				label2.Text = "Please select the new status for this Sales Order:";
				comboBoxStatus.LoadRequestStatus();
			}
		}

		private void ClosePeriodDialog_Load(object sender, EventArgs e)
		{
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				bool flag = false;
				if (SysDocumentType == SysDocTypes.ImportPurchaseOrder || SysDocumentType == SysDocTypes.PurchaseOrder)
				{
					flag = Factory.PurchaseOrderSystem.SetOrderStatus(sysDocID, voucherID, comboBoxStatus.SelectedStatus);
				}
				else if (SysDocumentType == SysDocTypes.PackingList)
				{
					flag = Factory.POShipmentSystem.SetOrderStatus(sysDocID, voucherID, comboBoxStatus.SelectedStatus);
				}
				else if (SysDocumentType == SysDocTypes.SalesOrder)
				{
					flag = Factory.SalesOrderSystem.SetOrderStatus(sysDocID, voucherID, (int)comboBoxStatus.SelectedStatus);
				}
				else if (SysDocumentType == SysDocTypes.ExportSalesOrder)
				{
					flag = Factory.SalesOrderSystem.SetOrderStatus(sysDocID, voucherID, (int)comboBoxStatus.SelectedStatus);
				}
				else if (SysDocumentType == SysDocTypes.JobMaterialRequisition)
				{
					flag = Factory.JobMaterialRequisitionSystem.SetOrderStatus(sysDocID, voucherID, (int)comboBoxStatus.SelectedStatus);
				}
				if (flag)
				{
					base.DialogResult = DialogResult.OK;
					Close();
				}
			}
			catch (CompanyException e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			catch (Exception e3)
			{
				ErrorHelper.ProcessError(e3);
			}
		}

		private void UpdatePOStatusDialog_Load(object sender, EventArgs e)
		{
		}

		private void ClosePeriodDialog_Activated(object sender, EventArgs e)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Vendors.UpdatePOStatusDialog));
			buttonCancel = new System.Windows.Forms.Button();
			buttonOK = new System.Windows.Forms.Button();
			linePanelDown = new Micromind.UISupport.Line();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			comboBoxStatus = new Micromind.DataControls.POStatusComboBox();
			SuspendLayout();
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(182, 110);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(88, 25);
			buttonCancel.TabIndex = 2;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(89, 110);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(87, 25);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(button2_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(-36, 100);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(527, 1);
			linePanelDown.TabIndex = 15;
			linePanelDown.TabStop = false;
			label2.Location = new System.Drawing.Point(4, 9);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(266, 21);
			label2.TabIndex = 23;
			label2.Text = "Please select the new status for this purchase order:";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(10, 46);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(40, 13);
			label1.TabIndex = 22;
			label1.Text = "Status:";
			comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStatus.FormattingEnabled = true;
			comboBoxStatus.Location = new System.Drawing.Point(55, 43);
			comboBoxStatus.Name = "comboBoxStatus";
			comboBoxStatus.SelectedStatus = Micromind.Common.Data.PurchaseOrderStatus.Open;
			comboBoxStatus.Size = new System.Drawing.Size(209, 21);
			comboBoxStatus.TabIndex = 0;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(282, 143);
			base.Controls.Add(comboBoxStatus);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(linePanelDown);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "UpdatePOStatusDialog";
			Text = "Change Status";
			base.Activated += new System.EventHandler(ClosePeriodDialog_Activated);
			base.Load += new System.EventHandler(UpdatePOStatusDialog_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
