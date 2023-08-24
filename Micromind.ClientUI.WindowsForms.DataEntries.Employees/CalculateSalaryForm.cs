using Infragistics.Win.Misc;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class CalculateSalaryForm : Form, IForm
	{
		public bool isrecalculate;

		private IContainer components;

		private Button buttonOK;

		private UltraGroupBox ultraGroupBox1;

		private Button buttonClose;

		private EmployeeSelector2 employeeSelector1;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5028;

		public ScreenTypes ScreenType => ScreenTypes.Dialog;

		public string FromEmployee => employeeSelector1.FromEmployee;

		public string ToEmployee => employeeSelector1.ToEmployee;

		public string FromDepartment => employeeSelector1.FromDepartment;

		public string ToDepartment => employeeSelector1.ToDepartment;

		public string FromLocation => employeeSelector1.FromLocation;

		public string ToLocation => employeeSelector1.ToLocation;

		public string FromType => employeeSelector1.FromType;

		public string ToType => employeeSelector1.ToType;

		public string FromDivision => employeeSelector1.FromDivision;

		public string ToDivision => employeeSelector1.ToDivision;

		public string FromSponsor => employeeSelector1.FromSponsor;

		public string ToSponsor => employeeSelector1.ToSponsor;

		public string FromGroup => employeeSelector1.FromGroup;

		public string ToGroup => employeeSelector1.ToGroup;

		public string FromGrade => employeeSelector1.FromGrade;

		public string ToGrade => employeeSelector1.ToGrade;

		public string FromPosition => employeeSelector1.FromPosition;

		public string ToPosition => employeeSelector1.ToPosition;

		public string FromBank => employeeSelector1.FromBank;

		public string ToBank => employeeSelector1.ToBank;

		public string FromAccount => employeeSelector1.FromAccount;

		public string ToAccount => employeeSelector1.ToAccount;

		public string MultipleEmployees => employeeSelector1.MultipleEmployees;

		public bool IsRecalulate
		{
			get
			{
				return isrecalculate;
			}
			set
			{
				isrecalculate = value;
			}
		}

		public CalculateSalaryForm()
		{
			InitializeComponent();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void CalculateSalaryForm_Load(object sender, EventArgs e)
		{
			if (IsRecalulate)
			{
				employeeSelector1.SetonReCalculate = IsRecalulate;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.CalculateSalaryForm));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			employeeSelector1 = new Micromind.DataControls.EmployeeSelector2();
			buttonClose = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(263, 347);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(employeeSelector1);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(450, 330);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Employees";
			employeeSelector1.BackColor = System.Drawing.Color.Transparent;
			employeeSelector1.CustomReportFieldName = "";
			employeeSelector1.CustomReportKey = "";
			employeeSelector1.CustomReportValueType = 1;
			employeeSelector1.Location = new System.Drawing.Point(17, 19);
			employeeSelector1.Name = "employeeSelector1";
			employeeSelector1.Size = new System.Drawing.Size(405, 316);
			employeeSelector1.TabIndex = 1;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(367, 347);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(474, 378);
			base.Controls.Add(buttonClose);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CalculateSalaryForm";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Select Employee";
			base.Load += new System.EventHandler(CalculateSalaryForm_Load);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
