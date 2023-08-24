using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class EmployeeSelector2 : UserControl, ICustomReportControl
	{
		private string crFieldName = "";

		private string crKey = "";

		private byte crValueType = 1;

		private List<string> Employeelist = new List<string>();

		private IContainer components;

		private EmployeeComboBox comboBoxFromEmployee;

		private EmployeeComboBox comboBoxToEmployee;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonRange;

		private RadioButton radioButtonDepartment;

		private RadioButton radioButtonLocation;

		private Label label1;

		private Label label2;

		private Label label3;

		private Label label4;

		private Label label5;

		private RadioButton radioButtonSingle;

		private EmployeeComboBox comboBoxSingleEmployee;

		private DepartmentComboBox comboBoxDepartmentFrom;

		private DepartmentComboBox comboBoxDepartmentTo;

		private WorkLocationComboBox comboBoxFromLocation;

		private WorkLocationComboBox comboBoxToLocation;

		private RadioButton radioButtonType;

		private EmployeeTypeComboBox comboBoxFromType;

		private EmployeeTypeComboBox comboBoxToType;

		private Label label6;

		private Label label7;

		private RadioButton radioButtonDivision;

		private Label label8;

		private Label label9;

		private RadioButton radioButtonSponsor;

		private RadioButton radioButtonGroup;

		private RadioButton radioButtonGrade;

		private RadioButton radioButtonPosition;

		private RadioButton radioButtonBank;

		private RadioButton radioButtonAccount;

		private Label label10;

		private Label label11;

		private Label label12;

		private Label label13;

		private Label label14;

		private Label label15;

		private Label label16;

		private Label label17;

		private Label label18;

		private Label label19;

		private Label label20;

		private Label label21;

		private DivisionComboBox comboBoxFromDivision;

		private DivisionComboBox comboBoxToDivision;

		private SponsorComboBox comboBoxFromSponsor;

		private SponsorComboBox comboBoxToSponsor;

		private EmployeeGroupComboBox comboBoxFromGroup;

		private EmployeeGroupComboBox comboBoxToGroup;

		private GradeComboBox comboBoxFromGrade;

		private GradeComboBox comboBoxToGrade;

		private PositionComboBox comboBoxFromPosition;

		private PositionComboBox comboBoxToPosition;

		private BankComboBox comboBoxFromBank;

		private BankComboBox comboBoxToBank;

		private AllAccountsComboBox comboBoxFromAccount;

		private AllAccountsComboBox comboBoxToAccount;

		private Button buttonMultiple;

		private RadioButton radioButtonMultipleEmployee;

		private TextBox textBoxMultipleEmployees;

		public string CustomReportFieldName
		{
			get
			{
				return crFieldName;
			}
			set
			{
				crFieldName = value;
			}
		}

		public string CustomReportKey
		{
			get
			{
				return crKey;
			}
			set
			{
				crKey = value;
			}
		}

		public byte CustomReportValueType
		{
			get
			{
				return crValueType;
			}
			set
			{
				crValueType = value;
			}
		}

		public string FromEmployee
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleEmployee.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromEmployee.SelectedID;
				}
				return "";
			}
		}

		public string ToEmployee
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleEmployee.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToEmployee.SelectedID;
				}
				return "";
			}
		}

		public string FromLocation
		{
			get
			{
				if (radioButtonLocation.Checked)
				{
					return comboBoxFromLocation.SelectedID;
				}
				return "";
			}
		}

		public string ToLocation
		{
			get
			{
				if (radioButtonLocation.Checked)
				{
					return comboBoxToLocation.SelectedID;
				}
				return "";
			}
		}

		public string FromDepartment
		{
			get
			{
				if (radioButtonDepartment.Checked)
				{
					return comboBoxDepartmentFrom.SelectedID;
				}
				return "";
			}
		}

		public string ToDepartment
		{
			get
			{
				if (radioButtonDepartment.Checked)
				{
					return comboBoxDepartmentTo.SelectedID;
				}
				return "";
			}
		}

		public string FromType
		{
			get
			{
				if (radioButtonType.Checked)
				{
					return comboBoxFromType.SelectedID;
				}
				return "";
			}
		}

		public string ToType
		{
			get
			{
				if (radioButtonType.Checked)
				{
					return comboBoxToType.SelectedID;
				}
				return "";
			}
		}

		public string FromDivision
		{
			get
			{
				if (radioButtonDivision.Checked)
				{
					return comboBoxFromDivision.SelectedID;
				}
				return "";
			}
		}

		public string ToDivision
		{
			get
			{
				if (radioButtonDivision.Checked)
				{
					return comboBoxToDivision.SelectedID;
				}
				return "";
			}
		}

		public string FromAccount
		{
			get
			{
				if (radioButtonAccount.Checked)
				{
					return comboBoxFromAccount.SelectedID;
				}
				return "";
			}
		}

		public string ToAccount
		{
			get
			{
				if (radioButtonAccount.Checked)
				{
					return comboBoxToAccount.SelectedID;
				}
				return "";
			}
		}

		public string FromSponsor
		{
			get
			{
				if (radioButtonSponsor.Checked)
				{
					return comboBoxFromSponsor.SelectedID;
				}
				return "";
			}
		}

		public string ToSponsor
		{
			get
			{
				if (radioButtonSponsor.Checked)
				{
					return comboBoxToSponsor.SelectedID;
				}
				return "";
			}
		}

		public string FromGroup
		{
			get
			{
				if (radioButtonGroup.Checked)
				{
					return comboBoxFromGroup.SelectedID;
				}
				return "";
			}
		}

		public string ToGroup
		{
			get
			{
				if (radioButtonGroup.Checked)
				{
					return comboBoxToGroup.SelectedID;
				}
				return "";
			}
		}

		public string FromGrade
		{
			get
			{
				if (radioButtonGrade.Checked)
				{
					return comboBoxFromGrade.SelectedID;
				}
				return "";
			}
		}

		public string ToGrade
		{
			get
			{
				if (radioButtonGrade.Checked)
				{
					return comboBoxToGrade.SelectedID;
				}
				return "";
			}
		}

		public string FromPosition
		{
			get
			{
				if (radioButtonPosition.Checked)
				{
					return comboBoxFromPosition.SelectedID;
				}
				return "";
			}
		}

		public string ToPosition
		{
			get
			{
				if (radioButtonPosition.Checked)
				{
					return comboBoxToPosition.SelectedID;
				}
				return "";
			}
		}

		public string FromBank
		{
			get
			{
				if (radioButtonBank.Checked)
				{
					return comboBoxFromBank.SelectedID;
				}
				return "";
			}
		}

		public string ToBank
		{
			get
			{
				if (radioButtonBank.Checked)
				{
					return comboBoxToBank.SelectedID;
				}
				return "";
			}
		}

		public string MultipleEmployees
		{
			get
			{
				if (radioButtonMultipleEmployee.Checked)
				{
					return "'" + string.Join("','", Employeelist) + "'";
				}
				return "";
			}
		}

		public bool SetonReCalculate
		{
			set
			{
				radioButtonAccount.Enabled = false;
				radioButtonSingle.Enabled = false;
				radioButtonRange.Enabled = false;
				radioButtonDepartment.Enabled = false;
				radioButtonLocation.Enabled = false;
				radioButtonType.Enabled = false;
				radioButtonDivision.Enabled = false;
				radioButtonSponsor.Enabled = false;
				radioButtonGroup.Enabled = false;
				radioButtonGrade.Enabled = false;
				radioButtonPosition.Enabled = false;
				radioButtonBank.Enabled = false;
				radioButtonAccount.Enabled = false;
				radioButtonAll.Enabled = false;
				radioButtonMultipleEmployee.Enabled = true;
				radioButtonMultipleEmployee.Checked = true;
			}
		}

		public EmployeeSelector2()
		{
			InitializeComponent();
		}

		private void EmployeeSelector_Load(object sender, EventArgs e)
		{
			buttonMultiple.Enabled = false;
		}

		public string GetParameterValue()
		{
			if (crValueType == 1)
			{
				if (radioButtonAll.Checked)
				{
					return "ANY(SELECT EmployeeID FROM Employee)";
				}
				if (radioButtonSingle.Checked)
				{
					return "'" + comboBoxSingleEmployee.SelectedID + "'";
				}
				if (radioButtonRange.Checked)
				{
					return "ANY(SELECT EmployeeID FROM Employee WHERE EmployeeID Between '" + comboBoxFromEmployee.SelectedID + "' AND '" + comboBoxToEmployee.SelectedID + "')";
				}
				if (radioButtonDepartment.Checked)
				{
					return "ANY(SELECT EmployeeID FROM Employee WHERE DepartmentID Between '" + comboBoxDepartmentFrom.SelectedID + "' AND '" + comboBoxDepartmentTo.SelectedID + "')";
				}
				if (radioButtonLocation.Checked)
				{
					return "ANY(SELECT EmployeeID FROM Employee WHERE LocationID Between '" + comboBoxFromLocation.SelectedID + "' AND '" + comboBoxToLocation.SelectedID + "')";
				}
				if (radioButtonDivision.Checked)
				{
					return "ANY(SELECT EmployeeID FROM Employee WHERE DivisionID Between '" + comboBoxFromDivision.SelectedID + "' AND '" + comboBoxToDivision.SelectedID + "')";
				}
				if (radioButtonSponsor.Checked)
				{
					return "ANY(SELECT EmployeeID FROM Employee WHERE SponsorID Between '" + comboBoxFromSponsor.SelectedID + "' AND '" + comboBoxToSponsor.SelectedID + "')";
				}
				if (radioButtonGroup.Checked)
				{
					return "ANY(SELECT EmployeeID FROM Employee WHERE GroupID Between '" + comboBoxFromGroup.SelectedID + "' AND '" + comboBoxToGroup.SelectedID + "')";
				}
				if (radioButtonGrade.Checked)
				{
					return "ANY(SELECT EmployeeID FROM Employee WHERE GradeID Between '" + comboBoxFromGrade.SelectedID + "' AND '" + comboBoxToGrade.SelectedID + "')";
				}
				if (radioButtonPosition.Checked)
				{
					return "ANY(SELECT EmployeeID FROM Employee WHERE PositionID Between '" + comboBoxFromPosition.SelectedID + "' AND '" + comboBoxToPosition.SelectedID + "')";
				}
				if (radioButtonBank.Checked)
				{
					return "ANY(SELECT EmployeeID FROM Employee WHERE BankID Between '" + comboBoxFromBank.SelectedID + "' AND '" + comboBoxToBank.SelectedID + "')";
				}
				if (radioButtonAccount.Checked)
				{
					return "ANY(SELECT EmployeeID FROM Employee WHERE AccountID Between '" + comboBoxFromAccount.SelectedID + "' AND '" + comboBoxToAccount.SelectedID + "')";
				}
				return "1=1";
			}
			if (crFieldName == "")
			{
				return "''=''";
			}
			if (radioButtonAll.Checked)
			{
				return "''=''";
			}
			if (radioButtonSingle.Checked)
			{
				return crFieldName + " = '" + comboBoxSingleEmployee.SelectedID + "'";
			}
			if (radioButtonRange.Checked)
			{
				return crFieldName + " Between '" + comboBoxFromEmployee.SelectedID + "' AND '" + comboBoxToEmployee.SelectedID + "')";
			}
			if (radioButtonDepartment.Checked)
			{
				return crFieldName + " = ANY(SELECT EmployeeID FROM Employee WHERE DepartmentID Between '" + comboBoxDepartmentFrom.SelectedID + "' AND '" + comboBoxDepartmentTo.SelectedID + "')";
			}
			if (radioButtonLocation.Checked)
			{
				return crFieldName + " = ANY(SELECT EmployeeID FROM Employee WHERE LocationID Between '" + comboBoxFromLocation.SelectedID + "' AND '" + comboBoxToLocation.SelectedID + "')";
			}
			if (radioButtonDivision.Checked)
			{
				return crFieldName + " = ANY(SELECT EmployeeID FROM Employee WHERE DivisionID Between '" + comboBoxFromDivision.SelectedID + "' AND '" + comboBoxToDivision.SelectedID + "')";
			}
			if (radioButtonSponsor.Checked)
			{
				return crFieldName + " = ANY(SELECT EmployeeID FROM Employee WHERE SponsorID Between '" + comboBoxFromSponsor.SelectedID + "' AND '" + comboBoxToSponsor.SelectedID + "')";
			}
			if (radioButtonGroup.Checked)
			{
				return crFieldName + " = ANY(SELECT EmployeeID FROM Employee WHERE GroupID Between '" + comboBoxFromGroup.SelectedID + "' AND '" + comboBoxToGroup.SelectedID + "')";
			}
			if (radioButtonGrade.Checked)
			{
				return crFieldName + " = ANY(SELECT EmployeeID FROM Employee WHERE GradeID Between '" + comboBoxFromGrade.SelectedID + "' AND '" + comboBoxToGrade.SelectedID + "')";
			}
			if (radioButtonPosition.Checked)
			{
				return crFieldName + " = ANY(SELECT EmployeeID FROM Employee WHERE PositionID Between '" + comboBoxFromPosition.SelectedID + "' AND '" + comboBoxToPosition.SelectedID + "')";
			}
			if (radioButtonBank.Checked)
			{
				return crFieldName + " = ANY(SELECT EmployeeID FROM Employee WHERE BankID Between '" + comboBoxFromBank.SelectedID + "' AND '" + comboBoxToBank.SelectedID + "')";
			}
			if (radioButtonAccount.Checked)
			{
				return crFieldName + " = ANY(SELECT EmployeeID FROM Employee WHERE AccountID Between '" + comboBoxFromAccount.SelectedID + "' AND '" + comboBoxToAccount.SelectedID + "')";
			}
			return "''=''";
		}

		private void EnableDisableControls()
		{
			comboBoxSingleEmployee.Enabled = radioButtonSingle.Checked;
			EmployeeComboBox employeeComboBox = comboBoxFromEmployee;
			bool enabled = comboBoxToEmployee.Enabled = radioButtonRange.Checked;
			employeeComboBox.Enabled = enabled;
			DepartmentComboBox departmentComboBox = comboBoxDepartmentFrom;
			enabled = (comboBoxDepartmentTo.Enabled = radioButtonDepartment.Checked);
			departmentComboBox.Enabled = enabled;
			WorkLocationComboBox workLocationComboBox = comboBoxFromLocation;
			enabled = (comboBoxToLocation.Enabled = radioButtonLocation.Checked);
			workLocationComboBox.Enabled = enabled;
			EmployeeTypeComboBox employeeTypeComboBox = comboBoxFromType;
			enabled = (comboBoxToType.Enabled = radioButtonType.Checked);
			employeeTypeComboBox.Enabled = enabled;
			DivisionComboBox divisionComboBox = comboBoxFromDivision;
			enabled = (comboBoxToDivision.Enabled = radioButtonDivision.Checked);
			divisionComboBox.Enabled = enabled;
			SponsorComboBox sponsorComboBox = comboBoxFromSponsor;
			enabled = (comboBoxToSponsor.Enabled = radioButtonSponsor.Checked);
			sponsorComboBox.Enabled = enabled;
			EmployeeGroupComboBox employeeGroupComboBox = comboBoxFromGroup;
			enabled = (comboBoxToGroup.Enabled = radioButtonGroup.Checked);
			employeeGroupComboBox.Enabled = enabled;
			GradeComboBox gradeComboBox = comboBoxFromGrade;
			enabled = (comboBoxToGrade.Enabled = radioButtonGrade.Checked);
			gradeComboBox.Enabled = enabled;
			PositionComboBox positionComboBox = comboBoxFromPosition;
			enabled = (comboBoxToPosition.Enabled = radioButtonPosition.Checked);
			positionComboBox.Enabled = enabled;
			BankComboBox bankComboBox = comboBoxFromBank;
			enabled = (comboBoxToBank.Enabled = radioButtonBank.Checked);
			bankComboBox.Enabled = enabled;
			AllAccountsComboBox allAccountsComboBox = comboBoxFromAccount;
			enabled = (comboBoxToAccount.Enabled = radioButtonAccount.Checked);
			allAccountsComboBox.Enabled = enabled;
			buttonMultiple.Enabled = radioButtonMultipleEmployee.Checked;
		}

		private void radioButtons_CheckedChanged(object sender, EventArgs e)
		{
			EnableDisableControls();
		}

		private void buttonMultiple_Click(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			List<string> selectedDocuments = new List<string>();
			dataSet = Factory.EmployeeSystem.GetActiveEmployeeList();
			SelectTransactionDialog selectTransactionDialog = new SelectTransactionDialog();
			selectTransactionDialog.DataSource = dataSet;
			selectTransactionDialog.IsMultiSelect = true;
			selectTransactionDialog.SelectedDocuments = selectedDocuments;
			selectTransactionDialog.Text = "Select Employee";
			if (selectTransactionDialog.ShowDialog(this) == DialogResult.OK)
			{
				selectedDocuments = selectTransactionDialog.SelectedDocuments;
				foreach (UltraGridRow selectedRow in selectTransactionDialog.SelectedRows)
				{
					string item = selectedRow.Cells["Code"].Value.ToString();
					selectedRow.Cells["Name"].Value.ToString();
					Employeelist.Add(item);
				}
				textBoxMultipleEmployees.Text = string.Join(",", Employeelist);
			}
		}

		private void radioButtonMultipleEmployee_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonMultipleEmployee.Checked)
			{
				buttonMultiple.Enabled = true;
				textBoxMultipleEmployees.Clear();
				Employeelist.Clear();
			}
			else
			{
				buttonMultiple.Enabled = false;
				textBoxMultipleEmployees.Clear();
				Employeelist.Clear();
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
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance74 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance75 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance76 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance77 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance78 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance79 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance80 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance81 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance82 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance83 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance84 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance85 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance86 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance87 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance88 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance89 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance90 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance91 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance92 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance93 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance94 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance95 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance96 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance97 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance98 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance99 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance100 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance101 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance102 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance103 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance104 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance105 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance106 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance107 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance108 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance109 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance110 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance111 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance112 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance113 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance114 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance115 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance116 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance117 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance118 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance119 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance120 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance121 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance122 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance123 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance124 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance125 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance126 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance127 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance128 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance129 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance130 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance131 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance132 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance133 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance134 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance135 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance136 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance137 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance138 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance139 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance140 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance141 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance142 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance143 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance144 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance145 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance146 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance147 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance148 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance149 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance150 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance151 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance152 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance153 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance154 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance155 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance156 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance157 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance158 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance159 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance160 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance161 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance162 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance163 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance164 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance165 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance166 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance167 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance168 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance169 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance170 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance171 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance172 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance173 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance174 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance175 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance176 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance177 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance178 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance179 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance180 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance181 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance182 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance183 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance184 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance185 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance186 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance187 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance188 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance189 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance190 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance191 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance192 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance193 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance194 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance195 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance196 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance197 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance198 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance199 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance200 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance201 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance202 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance203 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance204 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance205 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance206 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance207 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance208 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance209 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance210 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance211 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance212 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance213 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance214 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance215 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance216 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance217 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance218 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance219 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance220 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance221 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance222 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance223 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance224 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance225 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance226 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance227 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance228 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance229 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance230 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance231 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance232 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance233 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance234 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance235 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance236 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance237 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance238 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance239 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance240 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance241 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance242 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance243 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance244 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance245 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance246 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance247 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance248 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance249 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance250 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance251 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance252 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance253 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance254 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance255 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance256 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance257 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance258 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance259 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance260 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance261 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance262 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance263 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance264 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance265 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance266 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance267 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance268 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance269 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance270 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance271 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance272 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance273 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance274 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance275 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance276 = new Infragistics.Win.Appearance();
			labelTo = new System.Windows.Forms.Label();
			radioButtonAll = new System.Windows.Forms.RadioButton();
			radioButtonRange = new System.Windows.Forms.RadioButton();
			radioButtonDepartment = new System.Windows.Forms.RadioButton();
			radioButtonLocation = new System.Windows.Forms.RadioButton();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			radioButtonType = new System.Windows.Forms.RadioButton();
			label6 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			radioButtonDivision = new System.Windows.Forms.RadioButton();
			label8 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			radioButtonSponsor = new System.Windows.Forms.RadioButton();
			radioButtonGroup = new System.Windows.Forms.RadioButton();
			radioButtonGrade = new System.Windows.Forms.RadioButton();
			radioButtonPosition = new System.Windows.Forms.RadioButton();
			radioButtonBank = new System.Windows.Forms.RadioButton();
			radioButtonAccount = new System.Windows.Forms.RadioButton();
			label10 = new System.Windows.Forms.Label();
			label11 = new System.Windows.Forms.Label();
			label12 = new System.Windows.Forms.Label();
			label13 = new System.Windows.Forms.Label();
			label14 = new System.Windows.Forms.Label();
			label15 = new System.Windows.Forms.Label();
			label16 = new System.Windows.Forms.Label();
			label17 = new System.Windows.Forms.Label();
			label18 = new System.Windows.Forms.Label();
			label19 = new System.Windows.Forms.Label();
			label20 = new System.Windows.Forms.Label();
			label21 = new System.Windows.Forms.Label();
			buttonMultiple = new System.Windows.Forms.Button();
			radioButtonMultipleEmployee = new System.Windows.Forms.RadioButton();
			textBoxMultipleEmployees = new System.Windows.Forms.TextBox();
			comboBoxToAccount = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxFromAccount = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxToBank = new Micromind.DataControls.BankComboBox();
			comboBoxFromBank = new Micromind.DataControls.BankComboBox();
			comboBoxToPosition = new Micromind.DataControls.PositionComboBox();
			comboBoxFromPosition = new Micromind.DataControls.PositionComboBox();
			comboBoxToGrade = new Micromind.DataControls.GradeComboBox();
			comboBoxFromGrade = new Micromind.DataControls.GradeComboBox();
			comboBoxToGroup = new Micromind.DataControls.EmployeeGroupComboBox();
			comboBoxFromGroup = new Micromind.DataControls.EmployeeGroupComboBox();
			comboBoxToSponsor = new Micromind.DataControls.SponsorComboBox();
			comboBoxFromSponsor = new Micromind.DataControls.SponsorComboBox();
			comboBoxToDivision = new Micromind.DataControls.DivisionComboBox();
			comboBoxFromDivision = new Micromind.DataControls.DivisionComboBox();
			comboBoxToType = new Micromind.DataControls.EmployeeTypeComboBox();
			comboBoxFromType = new Micromind.DataControls.EmployeeTypeComboBox();
			comboBoxToLocation = new Micromind.DataControls.WorkLocationComboBox();
			comboBoxFromLocation = new Micromind.DataControls.WorkLocationComboBox();
			comboBoxDepartmentTo = new Micromind.DataControls.DepartmentComboBox();
			comboBoxDepartmentFrom = new Micromind.DataControls.DepartmentComboBox();
			comboBoxToEmployee = new Micromind.DataControls.EmployeeComboBox();
			comboBoxSingleEmployee = new Micromind.DataControls.EmployeeComboBox();
			comboBoxFromEmployee = new Micromind.DataControls.EmployeeComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxToAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToBank).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromBank).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToPosition).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromPosition).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToGrade).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromGrade).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToSponsor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromSponsor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToDivision).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromDivision).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDepartmentTo).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDepartmentFrom).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromEmployee).BeginInit();
			SuspendLayout();
			labelTo.AutoSize = true;
			labelTo.Location = new System.Drawing.Point(265, 52);
			labelTo.Name = "labelTo";
			labelTo.Size = new System.Drawing.Size(23, 13);
			labelTo.TabIndex = 6;
			labelTo.Text = "To:";
			radioButtonAll.AutoSize = true;
			radioButtonAll.Checked = true;
			radioButtonAll.Location = new System.Drawing.Point(6, 3);
			radioButtonAll.Name = "radioButtonAll";
			radioButtonAll.Size = new System.Drawing.Size(90, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Employees";
			radioButtonAll.UseVisualStyleBackColor = true;
			radioButtonAll.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonRange.AutoSize = true;
			radioButtonRange.Location = new System.Drawing.Point(6, 51);
			radioButtonRange.Name = "radioButtonRange";
			radioButtonRange.Size = new System.Drawing.Size(60, 17);
			radioButtonRange.TabIndex = 3;
			radioButtonRange.Text = "Range:";
			radioButtonRange.UseVisualStyleBackColor = true;
			radioButtonRange.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonDepartment.AutoSize = true;
			radioButtonDepartment.Location = new System.Drawing.Point(6, 73);
			radioButtonDepartment.Name = "radioButtonDepartment";
			radioButtonDepartment.Size = new System.Drawing.Size(83, 17);
			radioButtonDepartment.TabIndex = 7;
			radioButtonDepartment.Text = "Department:";
			radioButtonDepartment.UseVisualStyleBackColor = true;
			radioButtonDepartment.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonLocation.AutoSize = true;
			radioButtonLocation.Location = new System.Drawing.Point(6, 97);
			radioButtonLocation.Name = "radioButtonLocation";
			radioButtonLocation.Size = new System.Drawing.Size(69, 17);
			radioButtonLocation.TabIndex = 10;
			radioButtonLocation.Text = "Location:";
			radioButtonLocation.UseVisualStyleBackColor = true;
			radioButtonLocation.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(265, 98);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(23, 13);
			label1.TabIndex = 11;
			label1.Text = "To:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(265, 75);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(23, 13);
			label2.TabIndex = 6;
			label2.Text = "To:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(116, 52);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 4;
			label3.Text = "From:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(116, 75);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(33, 13);
			label4.TabIndex = 6;
			label4.Text = "From:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(116, 98);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(33, 13);
			label5.TabIndex = 11;
			label5.Text = "From:";
			radioButtonSingle.AutoSize = true;
			radioButtonSingle.Location = new System.Drawing.Point(100, 3);
			radioButtonSingle.Name = "radioButtonSingle";
			radioButtonSingle.Size = new System.Drawing.Size(57, 17);
			radioButtonSingle.TabIndex = 1;
			radioButtonSingle.Text = "Single:";
			radioButtonSingle.UseVisualStyleBackColor = true;
			radioButtonSingle.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonType.AutoSize = true;
			radioButtonType.Location = new System.Drawing.Point(6, 120);
			radioButtonType.Name = "radioButtonType";
			radioButtonType.Size = new System.Drawing.Size(52, 17);
			radioButtonType.TabIndex = 13;
			radioButtonType.Text = "Type:";
			radioButtonType.UseVisualStyleBackColor = true;
			radioButtonType.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(116, 122);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(33, 13);
			label6.TabIndex = 16;
			label6.Text = "From:";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(265, 122);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(23, 13);
			label7.TabIndex = 17;
			label7.Text = "To:";
			radioButtonDivision.AutoSize = true;
			radioButtonDivision.Location = new System.Drawing.Point(6, 143);
			radioButtonDivision.Name = "radioButtonDivision";
			radioButtonDivision.Size = new System.Drawing.Size(65, 17);
			radioButtonDivision.TabIndex = 16;
			radioButtonDivision.Text = "Division:";
			radioButtonDivision.UseVisualStyleBackColor = true;
			radioButtonDivision.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(116, 145);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(33, 13);
			label8.TabIndex = 19;
			label8.Text = "From:";
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(265, 145);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(23, 13);
			label9.TabIndex = 22;
			label9.Text = "To:";
			radioButtonSponsor.AutoSize = true;
			radioButtonSponsor.Location = new System.Drawing.Point(6, 166);
			radioButtonSponsor.Name = "radioButtonSponsor";
			radioButtonSponsor.Size = new System.Drawing.Size(67, 17);
			radioButtonSponsor.TabIndex = 19;
			radioButtonSponsor.Text = "Sponsor:";
			radioButtonSponsor.UseVisualStyleBackColor = true;
			radioButtonSponsor.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonGroup.AutoSize = true;
			radioButtonGroup.Location = new System.Drawing.Point(6, 189);
			radioButtonGroup.Name = "radioButtonGroup";
			radioButtonGroup.Size = new System.Drawing.Size(57, 17);
			radioButtonGroup.TabIndex = 22;
			radioButtonGroup.Text = "Group:";
			radioButtonGroup.UseVisualStyleBackColor = true;
			radioButtonGroup.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonGrade.AutoSize = true;
			radioButtonGrade.Location = new System.Drawing.Point(6, 212);
			radioButtonGrade.Name = "radioButtonGrade";
			radioButtonGrade.Size = new System.Drawing.Size(57, 17);
			radioButtonGrade.TabIndex = 25;
			radioButtonGrade.Text = "Grade:";
			radioButtonGrade.UseVisualStyleBackColor = true;
			radioButtonGrade.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonPosition.AutoSize = true;
			radioButtonPosition.Location = new System.Drawing.Point(6, 235);
			radioButtonPosition.Name = "radioButtonPosition";
			radioButtonPosition.Size = new System.Drawing.Size(65, 17);
			radioButtonPosition.TabIndex = 28;
			radioButtonPosition.Text = "Position:";
			radioButtonPosition.UseVisualStyleBackColor = true;
			radioButtonPosition.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonBank.AutoSize = true;
			radioButtonBank.Location = new System.Drawing.Point(6, 259);
			radioButtonBank.Name = "radioButtonBank";
			radioButtonBank.Size = new System.Drawing.Size(53, 17);
			radioButtonBank.TabIndex = 31;
			radioButtonBank.Text = "Bank:";
			radioButtonBank.UseVisualStyleBackColor = true;
			radioButtonBank.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonAccount.AutoSize = true;
			radioButtonAccount.Location = new System.Drawing.Point(6, 281);
			radioButtonAccount.Name = "radioButtonAccount";
			radioButtonAccount.Size = new System.Drawing.Size(68, 17);
			radioButtonAccount.TabIndex = 34;
			radioButtonAccount.Text = "Account:";
			radioButtonAccount.UseVisualStyleBackColor = true;
			radioButtonAccount.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(116, 167);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(33, 13);
			label10.TabIndex = 29;
			label10.Text = "From:";
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(116, 188);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(33, 13);
			label11.TabIndex = 30;
			label11.Text = "From:";
			label12.AutoSize = true;
			label12.Location = new System.Drawing.Point(116, 212);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(33, 13);
			label12.TabIndex = 31;
			label12.Text = "From:";
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(116, 235);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(33, 13);
			label13.TabIndex = 32;
			label13.Text = "From:";
			label14.AutoSize = true;
			label14.Location = new System.Drawing.Point(116, 259);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(33, 13);
			label14.TabIndex = 33;
			label14.Text = "From:";
			label15.AutoSize = true;
			label15.Location = new System.Drawing.Point(116, 282);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(33, 13);
			label15.TabIndex = 34;
			label15.Text = "From:";
			label16.AutoSize = true;
			label16.Location = new System.Drawing.Point(265, 168);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(23, 13);
			label16.TabIndex = 41;
			label16.Text = "To:";
			label17.AutoSize = true;
			label17.Location = new System.Drawing.Point(265, 189);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(23, 13);
			label17.TabIndex = 42;
			label17.Text = "To:";
			label18.AutoSize = true;
			label18.Location = new System.Drawing.Point(265, 211);
			label18.Name = "label18";
			label18.Size = new System.Drawing.Size(23, 13);
			label18.TabIndex = 43;
			label18.Text = "To:";
			label19.AutoSize = true;
			label19.Location = new System.Drawing.Point(266, 234);
			label19.Name = "label19";
			label19.Size = new System.Drawing.Size(23, 13);
			label19.TabIndex = 44;
			label19.Text = "To:";
			label20.AutoSize = true;
			label20.Location = new System.Drawing.Point(266, 254);
			label20.Name = "label20";
			label20.Size = new System.Drawing.Size(23, 13);
			label20.TabIndex = 45;
			label20.Text = "To:";
			label21.AutoSize = true;
			label21.Location = new System.Drawing.Point(266, 277);
			label21.Name = "label21";
			label21.Size = new System.Drawing.Size(23, 13);
			label21.TabIndex = 46;
			label21.Text = "To:";
			buttonMultiple.Location = new System.Drawing.Point(372, 23);
			buttonMultiple.Name = "buttonMultiple";
			buttonMultiple.Size = new System.Drawing.Size(26, 23);
			buttonMultiple.TabIndex = 55;
			buttonMultiple.Text = "...";
			buttonMultiple.UseVisualStyleBackColor = true;
			buttonMultiple.Click += new System.EventHandler(buttonMultiple_Click);
			radioButtonMultipleEmployee.AutoSize = true;
			radioButtonMultipleEmployee.Location = new System.Drawing.Point(6, 27);
			radioButtonMultipleEmployee.Name = "radioButtonMultipleEmployee";
			radioButtonMultipleEmployee.Size = new System.Drawing.Size(118, 17);
			radioButtonMultipleEmployee.TabIndex = 54;
			radioButtonMultipleEmployee.Text = "Multiple Employees ";
			radioButtonMultipleEmployee.UseVisualStyleBackColor = true;
			radioButtonMultipleEmployee.CheckedChanged += new System.EventHandler(radioButtonMultipleEmployee_CheckedChanged);
			textBoxMultipleEmployees.Location = new System.Drawing.Point(161, 24);
			textBoxMultipleEmployees.Name = "textBoxMultipleEmployees";
			textBoxMultipleEmployees.ReadOnly = true;
			textBoxMultipleEmployees.Size = new System.Drawing.Size(210, 20);
			textBoxMultipleEmployees.TabIndex = 53;
			comboBoxToAccount.Assigned = false;
			comboBoxToAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToAccount.CustomReportFieldName = "";
			comboBoxToAccount.CustomReportKey = "";
			comboBoxToAccount.CustomReportValueType = 1;
			comboBoxToAccount.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToAccount.DisplayLayout.Appearance = appearance;
			comboBoxToAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToAccount.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxToAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxToAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToAccount.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToAccount.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxToAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToAccount.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToAccount.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxToAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToAccount.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToAccount.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxToAccount.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxToAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxToAccount.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxToAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxToAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToAccount.Editable = true;
			comboBoxToAccount.Enabled = false;
			comboBoxToAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxToAccount.FilterString = "";
			comboBoxToAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxToAccount.FilterSysDocID = "";
			comboBoxToAccount.HasAllAccount = false;
			comboBoxToAccount.HasCustom = false;
			comboBoxToAccount.IsDataLoaded = false;
			comboBoxToAccount.Location = new System.Drawing.Point(294, 277);
			comboBoxToAccount.MaxDropDownItems = 12;
			comboBoxToAccount.Name = "comboBoxToAccount";
			comboBoxToAccount.ShowInactiveItems = false;
			comboBoxToAccount.ShowQuickAdd = true;
			comboBoxToAccount.Size = new System.Drawing.Size(103, 20);
			comboBoxToAccount.TabIndex = 36;
			comboBoxToAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromAccount.Assigned = false;
			comboBoxFromAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromAccount.CustomReportFieldName = "";
			comboBoxFromAccount.CustomReportKey = "";
			comboBoxFromAccount.CustomReportValueType = 1;
			comboBoxFromAccount.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromAccount.DisplayLayout.Appearance = appearance13;
			comboBoxFromAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromAccount.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxFromAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxFromAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromAccount.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromAccount.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxFromAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromAccount.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromAccount.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxFromAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromAccount.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromAccount.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxFromAccount.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxFromAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromAccount.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxFromAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxFromAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromAccount.Editable = true;
			comboBoxFromAccount.Enabled = false;
			comboBoxFromAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxFromAccount.FilterString = "";
			comboBoxFromAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxFromAccount.FilterSysDocID = "";
			comboBoxFromAccount.HasAllAccount = false;
			comboBoxFromAccount.HasCustom = false;
			comboBoxFromAccount.IsDataLoaded = false;
			comboBoxFromAccount.Location = new System.Drawing.Point(161, 277);
			comboBoxFromAccount.MaxDropDownItems = 12;
			comboBoxFromAccount.Name = "comboBoxFromAccount";
			comboBoxFromAccount.ShowInactiveItems = false;
			comboBoxFromAccount.ShowQuickAdd = true;
			comboBoxFromAccount.Size = new System.Drawing.Size(104, 20);
			comboBoxFromAccount.TabIndex = 35;
			comboBoxFromAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToBank.Assigned = false;
			comboBoxToBank.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToBank.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToBank.CustomReportFieldName = "";
			comboBoxToBank.CustomReportKey = "";
			comboBoxToBank.CustomReportValueType = 1;
			comboBoxToBank.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToBank.DisplayLayout.Appearance = appearance25;
			comboBoxToBank.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToBank.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToBank.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToBank.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxToBank.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToBank.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxToBank.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToBank.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToBank.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToBank.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxToBank.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToBank.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToBank.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToBank.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxToBank.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToBank.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToBank.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxToBank.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxToBank.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToBank.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxToBank.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxToBank.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToBank.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxToBank.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToBank.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToBank.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToBank.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToBank.Editable = true;
			comboBoxToBank.Enabled = false;
			comboBoxToBank.FilterString = "";
			comboBoxToBank.HasAllAccount = false;
			comboBoxToBank.HasCustom = false;
			comboBoxToBank.IsDataLoaded = false;
			comboBoxToBank.Location = new System.Drawing.Point(294, 255);
			comboBoxToBank.MaxDropDownItems = 12;
			comboBoxToBank.Name = "comboBoxToBank";
			comboBoxToBank.ShowInactiveItems = false;
			comboBoxToBank.ShowQuickAdd = true;
			comboBoxToBank.Size = new System.Drawing.Size(103, 20);
			comboBoxToBank.TabIndex = 33;
			comboBoxToBank.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromBank.Assigned = false;
			comboBoxFromBank.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromBank.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromBank.CustomReportFieldName = "";
			comboBoxFromBank.CustomReportKey = "";
			comboBoxFromBank.CustomReportValueType = 1;
			comboBoxFromBank.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromBank.DisplayLayout.Appearance = appearance37;
			comboBoxFromBank.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromBank.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromBank.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromBank.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxFromBank.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromBank.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxFromBank.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromBank.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromBank.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromBank.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxFromBank.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromBank.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromBank.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromBank.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxFromBank.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromBank.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromBank.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxFromBank.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxFromBank.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromBank.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromBank.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxFromBank.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromBank.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			comboBoxFromBank.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromBank.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromBank.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromBank.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromBank.Editable = true;
			comboBoxFromBank.Enabled = false;
			comboBoxFromBank.FilterString = "";
			comboBoxFromBank.HasAllAccount = false;
			comboBoxFromBank.HasCustom = false;
			comboBoxFromBank.IsDataLoaded = false;
			comboBoxFromBank.Location = new System.Drawing.Point(161, 254);
			comboBoxFromBank.MaxDropDownItems = 12;
			comboBoxFromBank.Name = "comboBoxFromBank";
			comboBoxFromBank.ShowInactiveItems = false;
			comboBoxFromBank.ShowQuickAdd = true;
			comboBoxFromBank.Size = new System.Drawing.Size(104, 20);
			comboBoxFromBank.TabIndex = 32;
			comboBoxFromBank.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToPosition.Assigned = false;
			comboBoxToPosition.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToPosition.CustomReportFieldName = "";
			comboBoxToPosition.CustomReportKey = "";
			comboBoxToPosition.CustomReportValueType = 1;
			comboBoxToPosition.DescriptionTextBox = null;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToPosition.DisplayLayout.Appearance = appearance49;
			comboBoxToPosition.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToPosition.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToPosition.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToPosition.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			comboBoxToPosition.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToPosition.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			comboBoxToPosition.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToPosition.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToPosition.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToPosition.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			comboBoxToPosition.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToPosition.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToPosition.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToPosition.DisplayLayout.Override.CellAppearance = appearance56;
			comboBoxToPosition.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToPosition.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToPosition.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			comboBoxToPosition.DisplayLayout.Override.HeaderAppearance = appearance58;
			comboBoxToPosition.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToPosition.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			comboBoxToPosition.DisplayLayout.Override.RowAppearance = appearance59;
			comboBoxToPosition.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToPosition.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			comboBoxToPosition.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToPosition.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToPosition.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToPosition.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToPosition.Editable = true;
			comboBoxToPosition.Enabled = false;
			comboBoxToPosition.FilterString = "";
			comboBoxToPosition.HasAllAccount = false;
			comboBoxToPosition.HasCustom = false;
			comboBoxToPosition.IsDataLoaded = false;
			comboBoxToPosition.Location = new System.Drawing.Point(294, 231);
			comboBoxToPosition.MaxDropDownItems = 12;
			comboBoxToPosition.Name = "comboBoxToPosition";
			comboBoxToPosition.ShowInactiveItems = false;
			comboBoxToPosition.ShowQuickAdd = true;
			comboBoxToPosition.Size = new System.Drawing.Size(103, 20);
			comboBoxToPosition.TabIndex = 30;
			comboBoxToPosition.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromPosition.Assigned = false;
			comboBoxFromPosition.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromPosition.CustomReportFieldName = "";
			comboBoxFromPosition.CustomReportKey = "";
			comboBoxFromPosition.CustomReportValueType = 1;
			comboBoxFromPosition.DescriptionTextBox = null;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromPosition.DisplayLayout.Appearance = appearance61;
			comboBoxFromPosition.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromPosition.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromPosition.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromPosition.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			comboBoxFromPosition.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromPosition.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			comboBoxFromPosition.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromPosition.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromPosition.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromPosition.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			comboBoxFromPosition.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromPosition.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromPosition.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromPosition.DisplayLayout.Override.CellAppearance = appearance68;
			comboBoxFromPosition.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromPosition.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromPosition.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			comboBoxFromPosition.DisplayLayout.Override.HeaderAppearance = appearance70;
			comboBoxFromPosition.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromPosition.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromPosition.DisplayLayout.Override.RowAppearance = appearance71;
			comboBoxFromPosition.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromPosition.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
			comboBoxFromPosition.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromPosition.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromPosition.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromPosition.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromPosition.Editable = true;
			comboBoxFromPosition.Enabled = false;
			comboBoxFromPosition.FilterString = "";
			comboBoxFromPosition.HasAllAccount = false;
			comboBoxFromPosition.HasCustom = false;
			comboBoxFromPosition.IsDataLoaded = false;
			comboBoxFromPosition.Location = new System.Drawing.Point(161, 231);
			comboBoxFromPosition.MaxDropDownItems = 12;
			comboBoxFromPosition.Name = "comboBoxFromPosition";
			comboBoxFromPosition.ShowInactiveItems = false;
			comboBoxFromPosition.ShowQuickAdd = true;
			comboBoxFromPosition.Size = new System.Drawing.Size(103, 20);
			comboBoxFromPosition.TabIndex = 29;
			comboBoxFromPosition.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToGrade.Assigned = false;
			comboBoxToGrade.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToGrade.CustomReportFieldName = "";
			comboBoxToGrade.CustomReportKey = "";
			comboBoxToGrade.CustomReportValueType = 1;
			comboBoxToGrade.DescriptionTextBox = null;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToGrade.DisplayLayout.Appearance = appearance73;
			comboBoxToGrade.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToGrade.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToGrade.DisplayLayout.GroupByBox.Appearance = appearance74;
			appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToGrade.DisplayLayout.GroupByBox.BandLabelAppearance = appearance75;
			comboBoxToGrade.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance76.BackColor2 = System.Drawing.SystemColors.Control;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToGrade.DisplayLayout.GroupByBox.PromptAppearance = appearance76;
			comboBoxToGrade.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToGrade.DisplayLayout.MaxRowScrollRegions = 1;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToGrade.DisplayLayout.Override.ActiveCellAppearance = appearance77;
			appearance78.BackColor = System.Drawing.SystemColors.Highlight;
			appearance78.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToGrade.DisplayLayout.Override.ActiveRowAppearance = appearance78;
			comboBoxToGrade.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToGrade.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToGrade.DisplayLayout.Override.CardAreaAppearance = appearance79;
			appearance80.BorderColor = System.Drawing.Color.Silver;
			appearance80.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToGrade.DisplayLayout.Override.CellAppearance = appearance80;
			comboBoxToGrade.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToGrade.DisplayLayout.Override.CellPadding = 0;
			appearance81.BackColor = System.Drawing.SystemColors.Control;
			appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance81.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToGrade.DisplayLayout.Override.GroupByRowAppearance = appearance81;
			appearance82.TextHAlignAsString = "Left";
			comboBoxToGrade.DisplayLayout.Override.HeaderAppearance = appearance82;
			comboBoxToGrade.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToGrade.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.Color.Silver;
			comboBoxToGrade.DisplayLayout.Override.RowAppearance = appearance83;
			comboBoxToGrade.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToGrade.DisplayLayout.Override.TemplateAddRowAppearance = appearance84;
			comboBoxToGrade.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToGrade.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToGrade.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToGrade.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToGrade.Editable = true;
			comboBoxToGrade.Enabled = false;
			comboBoxToGrade.FilterString = "";
			comboBoxToGrade.HasAllAccount = false;
			comboBoxToGrade.HasCustom = false;
			comboBoxToGrade.IsDataLoaded = false;
			comboBoxToGrade.Location = new System.Drawing.Point(294, 209);
			comboBoxToGrade.MaxDropDownItems = 12;
			comboBoxToGrade.Name = "comboBoxToGrade";
			comboBoxToGrade.ShowInactiveItems = false;
			comboBoxToGrade.ShowQuickAdd = true;
			comboBoxToGrade.Size = new System.Drawing.Size(103, 20);
			comboBoxToGrade.TabIndex = 27;
			comboBoxToGrade.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromGrade.Assigned = false;
			comboBoxFromGrade.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromGrade.CustomReportFieldName = "";
			comboBoxFromGrade.CustomReportKey = "";
			comboBoxFromGrade.CustomReportValueType = 1;
			comboBoxFromGrade.DescriptionTextBox = null;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromGrade.DisplayLayout.Appearance = appearance85;
			comboBoxFromGrade.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromGrade.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance86.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance86.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance86.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance86.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromGrade.DisplayLayout.GroupByBox.Appearance = appearance86;
			appearance87.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromGrade.DisplayLayout.GroupByBox.BandLabelAppearance = appearance87;
			comboBoxFromGrade.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance88.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance88.BackColor2 = System.Drawing.SystemColors.Control;
			appearance88.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance88.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromGrade.DisplayLayout.GroupByBox.PromptAppearance = appearance88;
			comboBoxFromGrade.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromGrade.DisplayLayout.MaxRowScrollRegions = 1;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromGrade.DisplayLayout.Override.ActiveCellAppearance = appearance89;
			appearance90.BackColor = System.Drawing.SystemColors.Highlight;
			appearance90.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromGrade.DisplayLayout.Override.ActiveRowAppearance = appearance90;
			comboBoxFromGrade.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromGrade.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromGrade.DisplayLayout.Override.CardAreaAppearance = appearance91;
			appearance92.BorderColor = System.Drawing.Color.Silver;
			appearance92.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromGrade.DisplayLayout.Override.CellAppearance = appearance92;
			comboBoxFromGrade.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromGrade.DisplayLayout.Override.CellPadding = 0;
			appearance93.BackColor = System.Drawing.SystemColors.Control;
			appearance93.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance93.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance93.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance93.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromGrade.DisplayLayout.Override.GroupByRowAppearance = appearance93;
			appearance94.TextHAlignAsString = "Left";
			comboBoxFromGrade.DisplayLayout.Override.HeaderAppearance = appearance94;
			comboBoxFromGrade.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromGrade.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			appearance95.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromGrade.DisplayLayout.Override.RowAppearance = appearance95;
			comboBoxFromGrade.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance96.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromGrade.DisplayLayout.Override.TemplateAddRowAppearance = appearance96;
			comboBoxFromGrade.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromGrade.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromGrade.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromGrade.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromGrade.Editable = true;
			comboBoxFromGrade.Enabled = false;
			comboBoxFromGrade.FilterString = "";
			comboBoxFromGrade.HasAllAccount = false;
			comboBoxFromGrade.HasCustom = false;
			comboBoxFromGrade.IsDataLoaded = false;
			comboBoxFromGrade.Location = new System.Drawing.Point(161, 209);
			comboBoxFromGrade.MaxDropDownItems = 12;
			comboBoxFromGrade.Name = "comboBoxFromGrade";
			comboBoxFromGrade.ShowInactiveItems = false;
			comboBoxFromGrade.ShowQuickAdd = true;
			comboBoxFromGrade.Size = new System.Drawing.Size(104, 20);
			comboBoxFromGrade.TabIndex = 26;
			comboBoxFromGrade.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToGroup.Assigned = false;
			comboBoxToGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToGroup.CustomReportFieldName = "";
			comboBoxToGroup.CustomReportKey = "";
			comboBoxToGroup.CustomReportValueType = 1;
			comboBoxToGroup.DescriptionTextBox = null;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToGroup.DisplayLayout.Appearance = appearance97;
			comboBoxToGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance98.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance98.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance98.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance98.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToGroup.DisplayLayout.GroupByBox.Appearance = appearance98;
			appearance99.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance99;
			comboBoxToGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance100.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance100.BackColor2 = System.Drawing.SystemColors.Control;
			appearance100.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance100.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance100;
			comboBoxToGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			appearance101.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToGroup.DisplayLayout.Override.ActiveCellAppearance = appearance101;
			appearance102.BackColor = System.Drawing.SystemColors.Highlight;
			appearance102.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToGroup.DisplayLayout.Override.ActiveRowAppearance = appearance102;
			comboBoxToGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToGroup.DisplayLayout.Override.CardAreaAppearance = appearance103;
			appearance104.BorderColor = System.Drawing.Color.Silver;
			appearance104.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToGroup.DisplayLayout.Override.CellAppearance = appearance104;
			comboBoxToGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToGroup.DisplayLayout.Override.CellPadding = 0;
			appearance105.BackColor = System.Drawing.SystemColors.Control;
			appearance105.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance105.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance105.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance105.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToGroup.DisplayLayout.Override.GroupByRowAppearance = appearance105;
			appearance106.TextHAlignAsString = "Left";
			comboBoxToGroup.DisplayLayout.Override.HeaderAppearance = appearance106;
			comboBoxToGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			appearance107.BorderColor = System.Drawing.Color.Silver;
			comboBoxToGroup.DisplayLayout.Override.RowAppearance = appearance107;
			comboBoxToGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance108.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance108;
			comboBoxToGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToGroup.Editable = true;
			comboBoxToGroup.Enabled = false;
			comboBoxToGroup.FilterString = "";
			comboBoxToGroup.HasAllAccount = false;
			comboBoxToGroup.HasCustom = false;
			comboBoxToGroup.IsDataLoaded = false;
			comboBoxToGroup.Location = new System.Drawing.Point(294, 186);
			comboBoxToGroup.MaxDropDownItems = 12;
			comboBoxToGroup.Name = "comboBoxToGroup";
			comboBoxToGroup.ShowInactiveItems = false;
			comboBoxToGroup.ShowQuickAdd = true;
			comboBoxToGroup.Size = new System.Drawing.Size(103, 20);
			comboBoxToGroup.TabIndex = 24;
			comboBoxToGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromGroup.Assigned = false;
			comboBoxFromGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromGroup.CustomReportFieldName = "";
			comboBoxFromGroup.CustomReportKey = "";
			comboBoxFromGroup.CustomReportValueType = 1;
			comboBoxFromGroup.DescriptionTextBox = null;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			appearance109.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromGroup.DisplayLayout.Appearance = appearance109;
			comboBoxFromGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance110.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance110.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance110.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance110.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromGroup.DisplayLayout.GroupByBox.Appearance = appearance110;
			appearance111.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance111;
			comboBoxFromGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance112.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance112.BackColor2 = System.Drawing.SystemColors.Control;
			appearance112.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance112.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance112;
			comboBoxFromGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			appearance113.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromGroup.DisplayLayout.Override.ActiveCellAppearance = appearance113;
			appearance114.BackColor = System.Drawing.SystemColors.Highlight;
			appearance114.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromGroup.DisplayLayout.Override.ActiveRowAppearance = appearance114;
			comboBoxFromGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromGroup.DisplayLayout.Override.CardAreaAppearance = appearance115;
			appearance116.BorderColor = System.Drawing.Color.Silver;
			appearance116.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromGroup.DisplayLayout.Override.CellAppearance = appearance116;
			comboBoxFromGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromGroup.DisplayLayout.Override.CellPadding = 0;
			appearance117.BackColor = System.Drawing.SystemColors.Control;
			appearance117.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance117.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance117.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance117.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromGroup.DisplayLayout.Override.GroupByRowAppearance = appearance117;
			appearance118.TextHAlignAsString = "Left";
			comboBoxFromGroup.DisplayLayout.Override.HeaderAppearance = appearance118;
			comboBoxFromGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance119.BackColor = System.Drawing.SystemColors.Window;
			appearance119.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromGroup.DisplayLayout.Override.RowAppearance = appearance119;
			comboBoxFromGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance120.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance120;
			comboBoxFromGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromGroup.Editable = true;
			comboBoxFromGroup.Enabled = false;
			comboBoxFromGroup.FilterString = "";
			comboBoxFromGroup.HasAllAccount = false;
			comboBoxFromGroup.HasCustom = false;
			comboBoxFromGroup.IsDataLoaded = false;
			comboBoxFromGroup.Location = new System.Drawing.Point(161, 186);
			comboBoxFromGroup.MaxDropDownItems = 12;
			comboBoxFromGroup.Name = "comboBoxFromGroup";
			comboBoxFromGroup.ShowInactiveItems = false;
			comboBoxFromGroup.ShowQuickAdd = true;
			comboBoxFromGroup.Size = new System.Drawing.Size(104, 20);
			comboBoxFromGroup.TabIndex = 23;
			comboBoxFromGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToSponsor.Assigned = false;
			comboBoxToSponsor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToSponsor.CustomReportFieldName = "";
			comboBoxToSponsor.CustomReportKey = "";
			comboBoxToSponsor.CustomReportValueType = 1;
			comboBoxToSponsor.DescriptionTextBox = null;
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			appearance121.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToSponsor.DisplayLayout.Appearance = appearance121;
			comboBoxToSponsor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToSponsor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance122.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance122.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance122.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance122.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToSponsor.DisplayLayout.GroupByBox.Appearance = appearance122;
			appearance123.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToSponsor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance123;
			comboBoxToSponsor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance124.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance124.BackColor2 = System.Drawing.SystemColors.Control;
			appearance124.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance124.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToSponsor.DisplayLayout.GroupByBox.PromptAppearance = appearance124;
			comboBoxToSponsor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToSponsor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance125.BackColor = System.Drawing.SystemColors.Window;
			appearance125.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToSponsor.DisplayLayout.Override.ActiveCellAppearance = appearance125;
			appearance126.BackColor = System.Drawing.SystemColors.Highlight;
			appearance126.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToSponsor.DisplayLayout.Override.ActiveRowAppearance = appearance126;
			comboBoxToSponsor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToSponsor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance127.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToSponsor.DisplayLayout.Override.CardAreaAppearance = appearance127;
			appearance128.BorderColor = System.Drawing.Color.Silver;
			appearance128.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToSponsor.DisplayLayout.Override.CellAppearance = appearance128;
			comboBoxToSponsor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToSponsor.DisplayLayout.Override.CellPadding = 0;
			appearance129.BackColor = System.Drawing.SystemColors.Control;
			appearance129.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance129.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance129.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance129.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToSponsor.DisplayLayout.Override.GroupByRowAppearance = appearance129;
			appearance130.TextHAlignAsString = "Left";
			comboBoxToSponsor.DisplayLayout.Override.HeaderAppearance = appearance130;
			comboBoxToSponsor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToSponsor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance131.BackColor = System.Drawing.SystemColors.Window;
			appearance131.BorderColor = System.Drawing.Color.Silver;
			comboBoxToSponsor.DisplayLayout.Override.RowAppearance = appearance131;
			comboBoxToSponsor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance132.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToSponsor.DisplayLayout.Override.TemplateAddRowAppearance = appearance132;
			comboBoxToSponsor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToSponsor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToSponsor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToSponsor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToSponsor.Editable = true;
			comboBoxToSponsor.Enabled = false;
			comboBoxToSponsor.FilterString = "";
			comboBoxToSponsor.HasAllAccount = false;
			comboBoxToSponsor.HasCustom = false;
			comboBoxToSponsor.IsDataLoaded = false;
			comboBoxToSponsor.Location = new System.Drawing.Point(294, 163);
			comboBoxToSponsor.MaxDropDownItems = 12;
			comboBoxToSponsor.Name = "comboBoxToSponsor";
			comboBoxToSponsor.ShowInactiveItems = false;
			comboBoxToSponsor.ShowQuickAdd = true;
			comboBoxToSponsor.Size = new System.Drawing.Size(103, 20);
			comboBoxToSponsor.TabIndex = 21;
			comboBoxToSponsor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromSponsor.Assigned = false;
			comboBoxFromSponsor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromSponsor.CustomReportFieldName = "";
			comboBoxFromSponsor.CustomReportKey = "";
			comboBoxFromSponsor.CustomReportValueType = 1;
			comboBoxFromSponsor.DescriptionTextBox = null;
			appearance133.BackColor = System.Drawing.SystemColors.Window;
			appearance133.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromSponsor.DisplayLayout.Appearance = appearance133;
			comboBoxFromSponsor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromSponsor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance134.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance134.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance134.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance134.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromSponsor.DisplayLayout.GroupByBox.Appearance = appearance134;
			appearance135.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromSponsor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance135;
			comboBoxFromSponsor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance136.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance136.BackColor2 = System.Drawing.SystemColors.Control;
			appearance136.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance136.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromSponsor.DisplayLayout.GroupByBox.PromptAppearance = appearance136;
			comboBoxFromSponsor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromSponsor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance137.BackColor = System.Drawing.SystemColors.Window;
			appearance137.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromSponsor.DisplayLayout.Override.ActiveCellAppearance = appearance137;
			appearance138.BackColor = System.Drawing.SystemColors.Highlight;
			appearance138.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromSponsor.DisplayLayout.Override.ActiveRowAppearance = appearance138;
			comboBoxFromSponsor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromSponsor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance139.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromSponsor.DisplayLayout.Override.CardAreaAppearance = appearance139;
			appearance140.BorderColor = System.Drawing.Color.Silver;
			appearance140.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromSponsor.DisplayLayout.Override.CellAppearance = appearance140;
			comboBoxFromSponsor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromSponsor.DisplayLayout.Override.CellPadding = 0;
			appearance141.BackColor = System.Drawing.SystemColors.Control;
			appearance141.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance141.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance141.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance141.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromSponsor.DisplayLayout.Override.GroupByRowAppearance = appearance141;
			appearance142.TextHAlignAsString = "Left";
			comboBoxFromSponsor.DisplayLayout.Override.HeaderAppearance = appearance142;
			comboBoxFromSponsor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromSponsor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance143.BackColor = System.Drawing.SystemColors.Window;
			appearance143.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromSponsor.DisplayLayout.Override.RowAppearance = appearance143;
			comboBoxFromSponsor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance144.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromSponsor.DisplayLayout.Override.TemplateAddRowAppearance = appearance144;
			comboBoxFromSponsor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromSponsor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromSponsor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromSponsor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromSponsor.Editable = true;
			comboBoxFromSponsor.Enabled = false;
			comboBoxFromSponsor.FilterString = "";
			comboBoxFromSponsor.HasAllAccount = false;
			comboBoxFromSponsor.HasCustom = false;
			comboBoxFromSponsor.IsDataLoaded = false;
			comboBoxFromSponsor.Location = new System.Drawing.Point(161, 163);
			comboBoxFromSponsor.MaxDropDownItems = 12;
			comboBoxFromSponsor.Name = "comboBoxFromSponsor";
			comboBoxFromSponsor.ShowInactiveItems = false;
			comboBoxFromSponsor.ShowQuickAdd = true;
			comboBoxFromSponsor.Size = new System.Drawing.Size(103, 20);
			comboBoxFromSponsor.TabIndex = 20;
			comboBoxFromSponsor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToDivision.Assigned = false;
			comboBoxToDivision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToDivision.CustomReportFieldName = "";
			comboBoxToDivision.CustomReportKey = "";
			comboBoxToDivision.CustomReportValueType = 1;
			comboBoxToDivision.DescriptionTextBox = null;
			appearance145.BackColor = System.Drawing.SystemColors.Window;
			appearance145.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToDivision.DisplayLayout.Appearance = appearance145;
			comboBoxToDivision.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToDivision.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance146.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance146.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance146.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance146.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToDivision.DisplayLayout.GroupByBox.Appearance = appearance146;
			appearance147.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToDivision.DisplayLayout.GroupByBox.BandLabelAppearance = appearance147;
			comboBoxToDivision.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance148.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance148.BackColor2 = System.Drawing.SystemColors.Control;
			appearance148.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance148.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToDivision.DisplayLayout.GroupByBox.PromptAppearance = appearance148;
			comboBoxToDivision.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToDivision.DisplayLayout.MaxRowScrollRegions = 1;
			appearance149.BackColor = System.Drawing.SystemColors.Window;
			appearance149.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToDivision.DisplayLayout.Override.ActiveCellAppearance = appearance149;
			appearance150.BackColor = System.Drawing.SystemColors.Highlight;
			appearance150.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToDivision.DisplayLayout.Override.ActiveRowAppearance = appearance150;
			comboBoxToDivision.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToDivision.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance151.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToDivision.DisplayLayout.Override.CardAreaAppearance = appearance151;
			appearance152.BorderColor = System.Drawing.Color.Silver;
			appearance152.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToDivision.DisplayLayout.Override.CellAppearance = appearance152;
			comboBoxToDivision.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToDivision.DisplayLayout.Override.CellPadding = 0;
			appearance153.BackColor = System.Drawing.SystemColors.Control;
			appearance153.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance153.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance153.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance153.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToDivision.DisplayLayout.Override.GroupByRowAppearance = appearance153;
			appearance154.TextHAlignAsString = "Left";
			comboBoxToDivision.DisplayLayout.Override.HeaderAppearance = appearance154;
			comboBoxToDivision.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToDivision.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance155.BackColor = System.Drawing.SystemColors.Window;
			appearance155.BorderColor = System.Drawing.Color.Silver;
			comboBoxToDivision.DisplayLayout.Override.RowAppearance = appearance155;
			comboBoxToDivision.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance156.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToDivision.DisplayLayout.Override.TemplateAddRowAppearance = appearance156;
			comboBoxToDivision.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToDivision.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToDivision.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToDivision.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToDivision.Editable = true;
			comboBoxToDivision.Enabled = false;
			comboBoxToDivision.FilterString = "";
			comboBoxToDivision.HasAllAccount = false;
			comboBoxToDivision.HasCustom = false;
			comboBoxToDivision.IsDataLoaded = false;
			comboBoxToDivision.Location = new System.Drawing.Point(294, 140);
			comboBoxToDivision.MaxDropDownItems = 12;
			comboBoxToDivision.Name = "comboBoxToDivision";
			comboBoxToDivision.ShowInactiveItems = false;
			comboBoxToDivision.ShowQuickAdd = true;
			comboBoxToDivision.Size = new System.Drawing.Size(103, 20);
			comboBoxToDivision.TabIndex = 18;
			comboBoxToDivision.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromDivision.Assigned = false;
			comboBoxFromDivision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromDivision.CustomReportFieldName = "";
			comboBoxFromDivision.CustomReportKey = "";
			comboBoxFromDivision.CustomReportValueType = 1;
			comboBoxFromDivision.DescriptionTextBox = null;
			appearance157.BackColor = System.Drawing.SystemColors.Window;
			appearance157.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromDivision.DisplayLayout.Appearance = appearance157;
			comboBoxFromDivision.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromDivision.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance158.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance158.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance158.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance158.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromDivision.DisplayLayout.GroupByBox.Appearance = appearance158;
			appearance159.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromDivision.DisplayLayout.GroupByBox.BandLabelAppearance = appearance159;
			comboBoxFromDivision.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance160.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance160.BackColor2 = System.Drawing.SystemColors.Control;
			appearance160.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance160.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromDivision.DisplayLayout.GroupByBox.PromptAppearance = appearance160;
			comboBoxFromDivision.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromDivision.DisplayLayout.MaxRowScrollRegions = 1;
			appearance161.BackColor = System.Drawing.SystemColors.Window;
			appearance161.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromDivision.DisplayLayout.Override.ActiveCellAppearance = appearance161;
			appearance162.BackColor = System.Drawing.SystemColors.Highlight;
			appearance162.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromDivision.DisplayLayout.Override.ActiveRowAppearance = appearance162;
			comboBoxFromDivision.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromDivision.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance163.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromDivision.DisplayLayout.Override.CardAreaAppearance = appearance163;
			appearance164.BorderColor = System.Drawing.Color.Silver;
			appearance164.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromDivision.DisplayLayout.Override.CellAppearance = appearance164;
			comboBoxFromDivision.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromDivision.DisplayLayout.Override.CellPadding = 0;
			appearance165.BackColor = System.Drawing.SystemColors.Control;
			appearance165.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance165.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance165.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance165.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromDivision.DisplayLayout.Override.GroupByRowAppearance = appearance165;
			appearance166.TextHAlignAsString = "Left";
			comboBoxFromDivision.DisplayLayout.Override.HeaderAppearance = appearance166;
			comboBoxFromDivision.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromDivision.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance167.BackColor = System.Drawing.SystemColors.Window;
			appearance167.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromDivision.DisplayLayout.Override.RowAppearance = appearance167;
			comboBoxFromDivision.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance168.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromDivision.DisplayLayout.Override.TemplateAddRowAppearance = appearance168;
			comboBoxFromDivision.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromDivision.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromDivision.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromDivision.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromDivision.Editable = true;
			comboBoxFromDivision.Enabled = false;
			comboBoxFromDivision.FilterString = "";
			comboBoxFromDivision.HasAllAccount = false;
			comboBoxFromDivision.HasCustom = false;
			comboBoxFromDivision.IsDataLoaded = false;
			comboBoxFromDivision.Location = new System.Drawing.Point(161, 140);
			comboBoxFromDivision.MaxDropDownItems = 12;
			comboBoxFromDivision.Name = "comboBoxFromDivision";
			comboBoxFromDivision.ShowInactiveItems = false;
			comboBoxFromDivision.ShowQuickAdd = true;
			comboBoxFromDivision.Size = new System.Drawing.Size(103, 20);
			comboBoxFromDivision.TabIndex = 17;
			comboBoxFromDivision.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToType.Assigned = false;
			comboBoxToType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToType.CustomReportFieldName = "";
			comboBoxToType.CustomReportKey = "";
			comboBoxToType.CustomReportValueType = 1;
			comboBoxToType.DescriptionTextBox = null;
			appearance169.BackColor = System.Drawing.SystemColors.Window;
			appearance169.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToType.DisplayLayout.Appearance = appearance169;
			comboBoxToType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance170.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance170.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance170.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance170.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToType.DisplayLayout.GroupByBox.Appearance = appearance170;
			appearance171.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance171;
			comboBoxToType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance172.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance172.BackColor2 = System.Drawing.SystemColors.Control;
			appearance172.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance172.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToType.DisplayLayout.GroupByBox.PromptAppearance = appearance172;
			comboBoxToType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance173.BackColor = System.Drawing.SystemColors.Window;
			appearance173.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToType.DisplayLayout.Override.ActiveCellAppearance = appearance173;
			appearance174.BackColor = System.Drawing.SystemColors.Highlight;
			appearance174.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToType.DisplayLayout.Override.ActiveRowAppearance = appearance174;
			comboBoxToType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance175.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToType.DisplayLayout.Override.CardAreaAppearance = appearance175;
			appearance176.BorderColor = System.Drawing.Color.Silver;
			appearance176.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToType.DisplayLayout.Override.CellAppearance = appearance176;
			comboBoxToType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToType.DisplayLayout.Override.CellPadding = 0;
			appearance177.BackColor = System.Drawing.SystemColors.Control;
			appearance177.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance177.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance177.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance177.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToType.DisplayLayout.Override.GroupByRowAppearance = appearance177;
			appearance178.TextHAlignAsString = "Left";
			comboBoxToType.DisplayLayout.Override.HeaderAppearance = appearance178;
			comboBoxToType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance179.BackColor = System.Drawing.SystemColors.Window;
			appearance179.BorderColor = System.Drawing.Color.Silver;
			comboBoxToType.DisplayLayout.Override.RowAppearance = appearance179;
			comboBoxToType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance180.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToType.DisplayLayout.Override.TemplateAddRowAppearance = appearance180;
			comboBoxToType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToType.Editable = true;
			comboBoxToType.Enabled = false;
			comboBoxToType.FilterString = "";
			comboBoxToType.HasAllAccount = false;
			comboBoxToType.HasCustom = false;
			comboBoxToType.IsDataLoaded = false;
			comboBoxToType.Location = new System.Drawing.Point(294, 118);
			comboBoxToType.MaxDropDownItems = 12;
			comboBoxToType.Name = "comboBoxToType";
			comboBoxToType.ShowInactiveItems = false;
			comboBoxToType.ShowQuickAdd = true;
			comboBoxToType.Size = new System.Drawing.Size(103, 20);
			comboBoxToType.TabIndex = 15;
			comboBoxToType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromType.Assigned = false;
			comboBoxFromType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromType.CustomReportFieldName = "";
			comboBoxFromType.CustomReportKey = "";
			comboBoxFromType.CustomReportValueType = 1;
			comboBoxFromType.DescriptionTextBox = null;
			appearance181.BackColor = System.Drawing.SystemColors.Window;
			appearance181.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromType.DisplayLayout.Appearance = appearance181;
			comboBoxFromType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance182.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance182.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance182.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance182.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromType.DisplayLayout.GroupByBox.Appearance = appearance182;
			appearance183.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance183;
			comboBoxFromType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance184.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance184.BackColor2 = System.Drawing.SystemColors.Control;
			appearance184.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance184.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromType.DisplayLayout.GroupByBox.PromptAppearance = appearance184;
			comboBoxFromType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance185.BackColor = System.Drawing.SystemColors.Window;
			appearance185.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromType.DisplayLayout.Override.ActiveCellAppearance = appearance185;
			appearance186.BackColor = System.Drawing.SystemColors.Highlight;
			appearance186.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromType.DisplayLayout.Override.ActiveRowAppearance = appearance186;
			comboBoxFromType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance187.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromType.DisplayLayout.Override.CardAreaAppearance = appearance187;
			appearance188.BorderColor = System.Drawing.Color.Silver;
			appearance188.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromType.DisplayLayout.Override.CellAppearance = appearance188;
			comboBoxFromType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromType.DisplayLayout.Override.CellPadding = 0;
			appearance189.BackColor = System.Drawing.SystemColors.Control;
			appearance189.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance189.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance189.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance189.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromType.DisplayLayout.Override.GroupByRowAppearance = appearance189;
			appearance190.TextHAlignAsString = "Left";
			comboBoxFromType.DisplayLayout.Override.HeaderAppearance = appearance190;
			comboBoxFromType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance191.BackColor = System.Drawing.SystemColors.Window;
			appearance191.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromType.DisplayLayout.Override.RowAppearance = appearance191;
			comboBoxFromType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance192.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromType.DisplayLayout.Override.TemplateAddRowAppearance = appearance192;
			comboBoxFromType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromType.Editable = true;
			comboBoxFromType.Enabled = false;
			comboBoxFromType.FilterString = "";
			comboBoxFromType.HasAllAccount = false;
			comboBoxFromType.HasCustom = false;
			comboBoxFromType.IsDataLoaded = false;
			comboBoxFromType.Location = new System.Drawing.Point(161, 118);
			comboBoxFromType.MaxDropDownItems = 12;
			comboBoxFromType.Name = "comboBoxFromType";
			comboBoxFromType.ShowInactiveItems = false;
			comboBoxFromType.ShowQuickAdd = true;
			comboBoxFromType.Size = new System.Drawing.Size(103, 20);
			comboBoxFromType.TabIndex = 14;
			comboBoxFromType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToLocation.Assigned = false;
			comboBoxToLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToLocation.CustomReportFieldName = "";
			comboBoxToLocation.CustomReportKey = "";
			comboBoxToLocation.CustomReportValueType = 1;
			comboBoxToLocation.DescriptionTextBox = null;
			appearance193.BackColor = System.Drawing.SystemColors.Window;
			appearance193.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToLocation.DisplayLayout.Appearance = appearance193;
			comboBoxToLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance194.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance194.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance194.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance194.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToLocation.DisplayLayout.GroupByBox.Appearance = appearance194;
			appearance195.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance195;
			comboBoxToLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance196.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance196.BackColor2 = System.Drawing.SystemColors.Control;
			appearance196.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance196.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance196;
			comboBoxToLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance197.BackColor = System.Drawing.SystemColors.Window;
			appearance197.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToLocation.DisplayLayout.Override.ActiveCellAppearance = appearance197;
			appearance198.BackColor = System.Drawing.SystemColors.Highlight;
			appearance198.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToLocation.DisplayLayout.Override.ActiveRowAppearance = appearance198;
			comboBoxToLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance199.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToLocation.DisplayLayout.Override.CardAreaAppearance = appearance199;
			appearance200.BorderColor = System.Drawing.Color.Silver;
			appearance200.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToLocation.DisplayLayout.Override.CellAppearance = appearance200;
			comboBoxToLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToLocation.DisplayLayout.Override.CellPadding = 0;
			appearance201.BackColor = System.Drawing.SystemColors.Control;
			appearance201.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance201.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance201.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance201.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToLocation.DisplayLayout.Override.GroupByRowAppearance = appearance201;
			appearance202.TextHAlignAsString = "Left";
			comboBoxToLocation.DisplayLayout.Override.HeaderAppearance = appearance202;
			comboBoxToLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance203.BackColor = System.Drawing.SystemColors.Window;
			appearance203.BorderColor = System.Drawing.Color.Silver;
			comboBoxToLocation.DisplayLayout.Override.RowAppearance = appearance203;
			comboBoxToLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance204.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance204;
			comboBoxToLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToLocation.Editable = true;
			comboBoxToLocation.Enabled = false;
			comboBoxToLocation.FilterString = "";
			comboBoxToLocation.HasAllAccount = false;
			comboBoxToLocation.HasCustom = false;
			comboBoxToLocation.IsDataLoaded = false;
			comboBoxToLocation.Location = new System.Drawing.Point(294, 95);
			comboBoxToLocation.MaxDropDownItems = 12;
			comboBoxToLocation.Name = "comboBoxToLocation";
			comboBoxToLocation.ShowAll = false;
			comboBoxToLocation.ShowConsignIn = false;
			comboBoxToLocation.ShowConsignOut = false;
			comboBoxToLocation.ShowInactiveItems = false;
			comboBoxToLocation.ShowNormalLocations = true;
			comboBoxToLocation.ShowPOSOnly = false;
			comboBoxToLocation.ShowQuickAdd = true;
			comboBoxToLocation.ShowWarehouseOnly = false;
			comboBoxToLocation.Size = new System.Drawing.Size(103, 20);
			comboBoxToLocation.TabIndex = 12;
			comboBoxToLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromLocation.Assigned = false;
			comboBoxFromLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromLocation.CustomReportFieldName = "";
			comboBoxFromLocation.CustomReportKey = "";
			comboBoxFromLocation.CustomReportValueType = 1;
			comboBoxFromLocation.DescriptionTextBox = null;
			appearance205.BackColor = System.Drawing.SystemColors.Window;
			appearance205.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromLocation.DisplayLayout.Appearance = appearance205;
			comboBoxFromLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance206.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance206.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance206.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance206.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromLocation.DisplayLayout.GroupByBox.Appearance = appearance206;
			appearance207.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance207;
			comboBoxFromLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance208.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance208.BackColor2 = System.Drawing.SystemColors.Control;
			appearance208.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance208.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance208;
			comboBoxFromLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance209.BackColor = System.Drawing.SystemColors.Window;
			appearance209.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromLocation.DisplayLayout.Override.ActiveCellAppearance = appearance209;
			appearance210.BackColor = System.Drawing.SystemColors.Highlight;
			appearance210.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromLocation.DisplayLayout.Override.ActiveRowAppearance = appearance210;
			comboBoxFromLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance211.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromLocation.DisplayLayout.Override.CardAreaAppearance = appearance211;
			appearance212.BorderColor = System.Drawing.Color.Silver;
			appearance212.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromLocation.DisplayLayout.Override.CellAppearance = appearance212;
			comboBoxFromLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromLocation.DisplayLayout.Override.CellPadding = 0;
			appearance213.BackColor = System.Drawing.SystemColors.Control;
			appearance213.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance213.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance213.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance213.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromLocation.DisplayLayout.Override.GroupByRowAppearance = appearance213;
			appearance214.TextHAlignAsString = "Left";
			comboBoxFromLocation.DisplayLayout.Override.HeaderAppearance = appearance214;
			comboBoxFromLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance215.BackColor = System.Drawing.SystemColors.Window;
			appearance215.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromLocation.DisplayLayout.Override.RowAppearance = appearance215;
			comboBoxFromLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance216.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance216;
			comboBoxFromLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromLocation.Editable = true;
			comboBoxFromLocation.Enabled = false;
			comboBoxFromLocation.FilterString = "";
			comboBoxFromLocation.HasAllAccount = false;
			comboBoxFromLocation.HasCustom = false;
			comboBoxFromLocation.IsDataLoaded = false;
			comboBoxFromLocation.Location = new System.Drawing.Point(161, 95);
			comboBoxFromLocation.MaxDropDownItems = 12;
			comboBoxFromLocation.Name = "comboBoxFromLocation";
			comboBoxFromLocation.ShowAll = false;
			comboBoxFromLocation.ShowConsignIn = false;
			comboBoxFromLocation.ShowConsignOut = false;
			comboBoxFromLocation.ShowInactiveItems = false;
			comboBoxFromLocation.ShowNormalLocations = true;
			comboBoxFromLocation.ShowPOSOnly = false;
			comboBoxFromLocation.ShowQuickAdd = true;
			comboBoxFromLocation.ShowWarehouseOnly = false;
			comboBoxFromLocation.Size = new System.Drawing.Size(103, 20);
			comboBoxFromLocation.TabIndex = 11;
			comboBoxFromLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDepartmentTo.Assigned = false;
			comboBoxDepartmentTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDepartmentTo.CustomReportFieldName = "";
			comboBoxDepartmentTo.CustomReportKey = "";
			comboBoxDepartmentTo.CustomReportValueType = 1;
			comboBoxDepartmentTo.DescriptionTextBox = null;
			appearance217.BackColor = System.Drawing.SystemColors.Window;
			appearance217.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDepartmentTo.DisplayLayout.Appearance = appearance217;
			comboBoxDepartmentTo.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDepartmentTo.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance218.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance218.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance218.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance218.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDepartmentTo.DisplayLayout.GroupByBox.Appearance = appearance218;
			appearance219.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDepartmentTo.DisplayLayout.GroupByBox.BandLabelAppearance = appearance219;
			comboBoxDepartmentTo.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance220.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance220.BackColor2 = System.Drawing.SystemColors.Control;
			appearance220.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance220.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDepartmentTo.DisplayLayout.GroupByBox.PromptAppearance = appearance220;
			comboBoxDepartmentTo.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDepartmentTo.DisplayLayout.MaxRowScrollRegions = 1;
			appearance221.BackColor = System.Drawing.SystemColors.Window;
			appearance221.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDepartmentTo.DisplayLayout.Override.ActiveCellAppearance = appearance221;
			appearance222.BackColor = System.Drawing.SystemColors.Highlight;
			appearance222.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDepartmentTo.DisplayLayout.Override.ActiveRowAppearance = appearance222;
			comboBoxDepartmentTo.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDepartmentTo.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance223.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDepartmentTo.DisplayLayout.Override.CardAreaAppearance = appearance223;
			appearance224.BorderColor = System.Drawing.Color.Silver;
			appearance224.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDepartmentTo.DisplayLayout.Override.CellAppearance = appearance224;
			comboBoxDepartmentTo.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDepartmentTo.DisplayLayout.Override.CellPadding = 0;
			appearance225.BackColor = System.Drawing.SystemColors.Control;
			appearance225.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance225.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance225.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance225.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDepartmentTo.DisplayLayout.Override.GroupByRowAppearance = appearance225;
			appearance226.TextHAlignAsString = "Left";
			comboBoxDepartmentTo.DisplayLayout.Override.HeaderAppearance = appearance226;
			comboBoxDepartmentTo.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDepartmentTo.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance227.BackColor = System.Drawing.SystemColors.Window;
			appearance227.BorderColor = System.Drawing.Color.Silver;
			comboBoxDepartmentTo.DisplayLayout.Override.RowAppearance = appearance227;
			comboBoxDepartmentTo.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance228.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDepartmentTo.DisplayLayout.Override.TemplateAddRowAppearance = appearance228;
			comboBoxDepartmentTo.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDepartmentTo.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDepartmentTo.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDepartmentTo.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDepartmentTo.Editable = true;
			comboBoxDepartmentTo.Enabled = false;
			comboBoxDepartmentTo.FilterString = "";
			comboBoxDepartmentTo.HasAllAccount = false;
			comboBoxDepartmentTo.HasCustom = false;
			comboBoxDepartmentTo.IsDataLoaded = false;
			comboBoxDepartmentTo.Location = new System.Drawing.Point(294, 72);
			comboBoxDepartmentTo.MaxDropDownItems = 12;
			comboBoxDepartmentTo.Name = "comboBoxDepartmentTo";
			comboBoxDepartmentTo.ShowInactiveItems = false;
			comboBoxDepartmentTo.ShowQuickAdd = true;
			comboBoxDepartmentTo.Size = new System.Drawing.Size(103, 20);
			comboBoxDepartmentTo.TabIndex = 9;
			comboBoxDepartmentTo.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDepartmentFrom.Assigned = false;
			comboBoxDepartmentFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDepartmentFrom.CustomReportFieldName = "";
			comboBoxDepartmentFrom.CustomReportKey = "";
			comboBoxDepartmentFrom.CustomReportValueType = 1;
			comboBoxDepartmentFrom.DescriptionTextBox = null;
			appearance229.BackColor = System.Drawing.SystemColors.Window;
			appearance229.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDepartmentFrom.DisplayLayout.Appearance = appearance229;
			comboBoxDepartmentFrom.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDepartmentFrom.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance230.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance230.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance230.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance230.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDepartmentFrom.DisplayLayout.GroupByBox.Appearance = appearance230;
			appearance231.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDepartmentFrom.DisplayLayout.GroupByBox.BandLabelAppearance = appearance231;
			comboBoxDepartmentFrom.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance232.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance232.BackColor2 = System.Drawing.SystemColors.Control;
			appearance232.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance232.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDepartmentFrom.DisplayLayout.GroupByBox.PromptAppearance = appearance232;
			comboBoxDepartmentFrom.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDepartmentFrom.DisplayLayout.MaxRowScrollRegions = 1;
			appearance233.BackColor = System.Drawing.SystemColors.Window;
			appearance233.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDepartmentFrom.DisplayLayout.Override.ActiveCellAppearance = appearance233;
			appearance234.BackColor = System.Drawing.SystemColors.Highlight;
			appearance234.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDepartmentFrom.DisplayLayout.Override.ActiveRowAppearance = appearance234;
			comboBoxDepartmentFrom.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDepartmentFrom.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance235.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDepartmentFrom.DisplayLayout.Override.CardAreaAppearance = appearance235;
			appearance236.BorderColor = System.Drawing.Color.Silver;
			appearance236.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDepartmentFrom.DisplayLayout.Override.CellAppearance = appearance236;
			comboBoxDepartmentFrom.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDepartmentFrom.DisplayLayout.Override.CellPadding = 0;
			appearance237.BackColor = System.Drawing.SystemColors.Control;
			appearance237.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance237.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance237.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance237.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDepartmentFrom.DisplayLayout.Override.GroupByRowAppearance = appearance237;
			appearance238.TextHAlignAsString = "Left";
			comboBoxDepartmentFrom.DisplayLayout.Override.HeaderAppearance = appearance238;
			comboBoxDepartmentFrom.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDepartmentFrom.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance239.BackColor = System.Drawing.SystemColors.Window;
			appearance239.BorderColor = System.Drawing.Color.Silver;
			comboBoxDepartmentFrom.DisplayLayout.Override.RowAppearance = appearance239;
			comboBoxDepartmentFrom.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance240.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDepartmentFrom.DisplayLayout.Override.TemplateAddRowAppearance = appearance240;
			comboBoxDepartmentFrom.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDepartmentFrom.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDepartmentFrom.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDepartmentFrom.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDepartmentFrom.Editable = true;
			comboBoxDepartmentFrom.Enabled = false;
			comboBoxDepartmentFrom.FilterString = "";
			comboBoxDepartmentFrom.HasAllAccount = false;
			comboBoxDepartmentFrom.HasCustom = false;
			comboBoxDepartmentFrom.IsDataLoaded = false;
			comboBoxDepartmentFrom.Location = new System.Drawing.Point(161, 72);
			comboBoxDepartmentFrom.MaxDropDownItems = 12;
			comboBoxDepartmentFrom.Name = "comboBoxDepartmentFrom";
			comboBoxDepartmentFrom.ShowInactiveItems = false;
			comboBoxDepartmentFrom.ShowQuickAdd = true;
			comboBoxDepartmentFrom.Size = new System.Drawing.Size(102, 20);
			comboBoxDepartmentFrom.TabIndex = 8;
			comboBoxDepartmentFrom.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToEmployee.Assigned = false;
			comboBoxToEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToEmployee.CustomReportFieldName = "";
			comboBoxToEmployee.CustomReportKey = "";
			comboBoxToEmployee.CustomReportValueType = 1;
			comboBoxToEmployee.DescriptionTextBox = null;
			appearance241.BackColor = System.Drawing.SystemColors.Window;
			appearance241.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToEmployee.DisplayLayout.Appearance = appearance241;
			comboBoxToEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance242.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance242.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance242.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance242.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToEmployee.DisplayLayout.GroupByBox.Appearance = appearance242;
			appearance243.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance243;
			comboBoxToEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance244.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance244.BackColor2 = System.Drawing.SystemColors.Control;
			appearance244.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance244.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance244;
			comboBoxToEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance245.BackColor = System.Drawing.SystemColors.Window;
			appearance245.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance245;
			appearance246.BackColor = System.Drawing.SystemColors.Highlight;
			appearance246.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance246;
			comboBoxToEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance247.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToEmployee.DisplayLayout.Override.CardAreaAppearance = appearance247;
			appearance248.BorderColor = System.Drawing.Color.Silver;
			appearance248.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToEmployee.DisplayLayout.Override.CellAppearance = appearance248;
			comboBoxToEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance249.BackColor = System.Drawing.SystemColors.Control;
			appearance249.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance249.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance249.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance249.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance249;
			appearance250.TextHAlignAsString = "Left";
			comboBoxToEmployee.DisplayLayout.Override.HeaderAppearance = appearance250;
			comboBoxToEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance251.BackColor = System.Drawing.SystemColors.Window;
			appearance251.BorderColor = System.Drawing.Color.Silver;
			comboBoxToEmployee.DisplayLayout.Override.RowAppearance = appearance251;
			comboBoxToEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance252.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance252;
			comboBoxToEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToEmployee.Editable = true;
			comboBoxToEmployee.Enabled = false;
			comboBoxToEmployee.FilterString = "";
			comboBoxToEmployee.HasAllAccount = false;
			comboBoxToEmployee.HasCustom = false;
			comboBoxToEmployee.IsDataLoaded = false;
			comboBoxToEmployee.Location = new System.Drawing.Point(294, 49);
			comboBoxToEmployee.MaxDropDownItems = 12;
			comboBoxToEmployee.Name = "comboBoxToEmployee";
			comboBoxToEmployee.ShowInactiveItems = false;
			comboBoxToEmployee.ShowQuickAdd = true;
			comboBoxToEmployee.ShowTerminatedEmployees = true;
			comboBoxToEmployee.Size = new System.Drawing.Size(103, 20);
			comboBoxToEmployee.TabIndex = 6;
			comboBoxToEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSingleEmployee.Assigned = false;
			comboBoxSingleEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSingleEmployee.CustomReportFieldName = "";
			comboBoxSingleEmployee.CustomReportKey = "";
			comboBoxSingleEmployee.CustomReportValueType = 1;
			comboBoxSingleEmployee.DescriptionTextBox = null;
			appearance253.BackColor = System.Drawing.SystemColors.Window;
			appearance253.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleEmployee.DisplayLayout.Appearance = appearance253;
			comboBoxSingleEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance254.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance254.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance254.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance254.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleEmployee.DisplayLayout.GroupByBox.Appearance = appearance254;
			appearance255.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance255;
			comboBoxSingleEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance256.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance256.BackColor2 = System.Drawing.SystemColors.Control;
			appearance256.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance256.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance256;
			comboBoxSingleEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance257.BackColor = System.Drawing.SystemColors.Window;
			appearance257.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance257;
			appearance258.BackColor = System.Drawing.SystemColors.Highlight;
			appearance258.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance258;
			comboBoxSingleEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance259.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleEmployee.DisplayLayout.Override.CardAreaAppearance = appearance259;
			appearance260.BorderColor = System.Drawing.Color.Silver;
			appearance260.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleEmployee.DisplayLayout.Override.CellAppearance = appearance260;
			comboBoxSingleEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance261.BackColor = System.Drawing.SystemColors.Control;
			appearance261.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance261.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance261.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance261.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance261;
			appearance262.TextHAlignAsString = "Left";
			comboBoxSingleEmployee.DisplayLayout.Override.HeaderAppearance = appearance262;
			comboBoxSingleEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance263.BackColor = System.Drawing.SystemColors.Window;
			appearance263.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleEmployee.DisplayLayout.Override.RowAppearance = appearance263;
			comboBoxSingleEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance264.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance264;
			comboBoxSingleEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleEmployee.Editable = true;
			comboBoxSingleEmployee.Enabled = false;
			comboBoxSingleEmployee.FilterString = "";
			comboBoxSingleEmployee.HasAllAccount = false;
			comboBoxSingleEmployee.HasCustom = false;
			comboBoxSingleEmployee.IsDataLoaded = false;
			comboBoxSingleEmployee.Location = new System.Drawing.Point(161, 1);
			comboBoxSingleEmployee.MaxDropDownItems = 12;
			comboBoxSingleEmployee.Name = "comboBoxSingleEmployee";
			comboBoxSingleEmployee.ShowInactiveItems = false;
			comboBoxSingleEmployee.ShowQuickAdd = true;
			comboBoxSingleEmployee.ShowTerminatedEmployees = true;
			comboBoxSingleEmployee.Size = new System.Drawing.Size(236, 20);
			comboBoxSingleEmployee.TabIndex = 2;
			comboBoxSingleEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromEmployee.Assigned = false;
			comboBoxFromEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromEmployee.CustomReportFieldName = "";
			comboBoxFromEmployee.CustomReportKey = "";
			comboBoxFromEmployee.CustomReportValueType = 1;
			comboBoxFromEmployee.DescriptionTextBox = null;
			appearance265.BackColor = System.Drawing.SystemColors.Window;
			appearance265.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromEmployee.DisplayLayout.Appearance = appearance265;
			comboBoxFromEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance266.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance266.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance266.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance266.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromEmployee.DisplayLayout.GroupByBox.Appearance = appearance266;
			appearance267.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance267;
			comboBoxFromEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance268.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance268.BackColor2 = System.Drawing.SystemColors.Control;
			appearance268.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance268.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance268;
			comboBoxFromEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance269.BackColor = System.Drawing.SystemColors.Window;
			appearance269.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance269;
			appearance270.BackColor = System.Drawing.SystemColors.Highlight;
			appearance270.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance270;
			comboBoxFromEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance271.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromEmployee.DisplayLayout.Override.CardAreaAppearance = appearance271;
			appearance272.BorderColor = System.Drawing.Color.Silver;
			appearance272.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromEmployee.DisplayLayout.Override.CellAppearance = appearance272;
			comboBoxFromEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance273.BackColor = System.Drawing.SystemColors.Control;
			appearance273.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance273.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance273.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance273.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance273;
			appearance274.TextHAlignAsString = "Left";
			comboBoxFromEmployee.DisplayLayout.Override.HeaderAppearance = appearance274;
			comboBoxFromEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance275.BackColor = System.Drawing.SystemColors.Window;
			appearance275.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromEmployee.DisplayLayout.Override.RowAppearance = appearance275;
			comboBoxFromEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance276.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance276;
			comboBoxFromEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromEmployee.Editable = true;
			comboBoxFromEmployee.Enabled = false;
			comboBoxFromEmployee.FilterString = "";
			comboBoxFromEmployee.HasAllAccount = false;
			comboBoxFromEmployee.HasCustom = false;
			comboBoxFromEmployee.IsDataLoaded = false;
			comboBoxFromEmployee.Location = new System.Drawing.Point(161, 49);
			comboBoxFromEmployee.MaxDropDownItems = 12;
			comboBoxFromEmployee.Name = "comboBoxFromEmployee";
			comboBoxFromEmployee.ShowInactiveItems = false;
			comboBoxFromEmployee.ShowQuickAdd = true;
			comboBoxFromEmployee.ShowTerminatedEmployees = true;
			comboBoxFromEmployee.Size = new System.Drawing.Size(103, 20);
			comboBoxFromEmployee.TabIndex = 5;
			comboBoxFromEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(buttonMultiple);
			base.Controls.Add(radioButtonMultipleEmployee);
			base.Controls.Add(textBoxMultipleEmployees);
			base.Controls.Add(comboBoxToAccount);
			base.Controls.Add(comboBoxFromAccount);
			base.Controls.Add(comboBoxToBank);
			base.Controls.Add(comboBoxFromBank);
			base.Controls.Add(comboBoxToPosition);
			base.Controls.Add(comboBoxFromPosition);
			base.Controls.Add(comboBoxToGrade);
			base.Controls.Add(comboBoxFromGrade);
			base.Controls.Add(comboBoxToGroup);
			base.Controls.Add(comboBoxFromGroup);
			base.Controls.Add(comboBoxToSponsor);
			base.Controls.Add(comboBoxFromSponsor);
			base.Controls.Add(comboBoxToDivision);
			base.Controls.Add(comboBoxFromDivision);
			base.Controls.Add(label21);
			base.Controls.Add(label20);
			base.Controls.Add(label19);
			base.Controls.Add(label18);
			base.Controls.Add(label17);
			base.Controls.Add(label16);
			base.Controls.Add(label15);
			base.Controls.Add(label14);
			base.Controls.Add(label13);
			base.Controls.Add(label12);
			base.Controls.Add(label11);
			base.Controls.Add(label10);
			base.Controls.Add(radioButtonAccount);
			base.Controls.Add(radioButtonBank);
			base.Controls.Add(radioButtonPosition);
			base.Controls.Add(radioButtonGrade);
			base.Controls.Add(radioButtonGroup);
			base.Controls.Add(radioButtonSponsor);
			base.Controls.Add(label9);
			base.Controls.Add(label8);
			base.Controls.Add(radioButtonDivision);
			base.Controls.Add(label7);
			base.Controls.Add(label6);
			base.Controls.Add(comboBoxToType);
			base.Controls.Add(comboBoxFromType);
			base.Controls.Add(radioButtonType);
			base.Controls.Add(comboBoxToLocation);
			base.Controls.Add(comboBoxFromLocation);
			base.Controls.Add(comboBoxDepartmentTo);
			base.Controls.Add(comboBoxDepartmentFrom);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(label5);
			base.Controls.Add(label1);
			base.Controls.Add(radioButtonLocation);
			base.Controls.Add(radioButtonDepartment);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(label4);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(labelTo);
			base.Controls.Add(comboBoxToEmployee);
			base.Controls.Add(comboBoxSingleEmployee);
			base.Controls.Add(comboBoxFromEmployee);
			base.Name = "EmployeeSelector2";
			base.Size = new System.Drawing.Size(419, 304);
			base.Load += new System.EventHandler(EmployeeSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxToAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToBank).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromBank).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToPosition).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromPosition).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToGrade).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromGrade).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToSponsor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromSponsor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToDivision).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromDivision).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDepartmentTo).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDepartmentFrom).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromEmployee).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
