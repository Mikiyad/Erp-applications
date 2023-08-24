using DevExpress.XtraReports.UI;
using Infragistics.Win.Misc;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.Sales
{
	public class SalesReport : Form, IForm
	{
		private string selectionString = "";

		private string groupByString = "";

		private string joinQuery = "";

		private string group3 = "";

		private string nameFields = "";

		private string fields = "";

		private string filter2 = "";

		private int comboCount;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private Button buttonClose;

		private Button button1;

		private UltraGroupBox ultraGroupBox2;

		private CustomerSelector customerSelector;

		private UltraGroupBox ultraGroupBox3;

		private ProductSelector productSelector;

		private UltraGroupBox ultraGroupBox4;

		private LocationSelector locationSelector;

		private UltraGroupBox ultraGroupBox5;

		private SalespersonSelector salespersonSelector;

		private DateControl dateControl1;

		private UltraGroupBox ultraGroupBoxGroupBy;

		private ComboBox comboBox1;

		private ComboBox comboBox5;

		private ComboBox comboBox2;

		private ComboBox comboBox4;

		private ComboBox comboBox3;

		private MMLabel mmLabel4;

		private MMLabel mmLabel3;

		private MMLabel mmLabel2;

		private MMLabel mmLabel1;

		private MMLabel mmLabel17;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsSales;

		public int ScreenID => 7035;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public SalesReport()
		{
			InitializeComponent();
			base.Load += Form_Load;
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				InitializeControls();
				_ = base.IsDisposed;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			checked
			{
				try
				{
					ReportHelper reportHelper = new ReportHelper();
					XtraReport xtraReport = null;
					fields = " [Doc ID], [Doc Number],ASD.ProductID, ASD.Description, SUM(COGS) AS COGS, ASD.AverageCost, ASD.CustomerID, Date, ASD.SalespersonID, ASD.ReportTo, SUM(Profit) AS Profit, IsExport, Reference,ASD.CurrencyID, CurrencyRate, SUM(ASD.Quantity) AS Quantity, SUM(UnitPrice) AS UnitPrice, SUM(Amount) AS Amount, SUM(AmountFC) AS AmountFC, Type ";
					selectionString = "";
					groupByString = "";
					joinQuery = "";
					filter2 = "";
					if (!string.IsNullOrEmpty(comboBox1.Text))
					{
						if (comboBox1.Text == "BrandID" || comboBox1.Text == "CategoryID" || comboBox1.Text == "ClassID")
						{
							selectionString = " P. " + comboBox1.Text + " AS Group1,'" + comboBox1.Text + "' AS Group1Type ";
							groupByString = " P. " + comboBox1.Text;
						}
						else if (comboBox1.Text == "VoucherID")
						{
							selectionString = " ASD.[Doc Number]  AS Group1, '" + comboBox1.Text + "' AS Group1Type";
							groupByString = " ASD.[Doc Number] ";
						}
						else if (comboBox1.Text == "GroupID")
						{
							selectionString = " CG. " + comboBox1.Text + " AS Group1,'" + comboBox1.Text + "' AS Group1Type ";
							groupByString = " CG. " + comboBox1.Text;
						}
						else if (comboBox1.Text == "CustomerClassID")
						{
							selectionString = " Cl.ClassID AS Group1,'" + comboBox1.Text + "' AS Group1Type ";
							groupByString = " Cl.ClassID";
						}
						else if (comboBox1.Text == "AreaID")
						{
							selectionString = " C3.AreaID AS Group1,'" + comboBox1.Text + "' AS Group1Type ";
							groupByString = " C3.AreaID";
						}
						else if (comboBox1.Text == "ShortName")
						{
							selectionString = " C4.CustomerID AS Group1,'" + comboBox1.Text + "' AS Group1Type ";
							groupByString = " C4.CustomerID";
						}
						else
						{
							selectionString = " ASD. " + comboBox1.Text + " AS Group1, '" + comboBox1.Text + "' AS Group1Type";
							groupByString = " ASD. " + comboBox1.Text;
						}
						GenerateQuery(comboBox1.Text, "Group1");
						filter2 += comboBox1.Text.Substring(0, comboBox1.Text.Length - 2);
					}
					else
					{
						selectionString += "0 AS Group1";
						selectionString = selectionString + ",  '" + 0 + "'  AS Group1Type";
					}
					if (!string.IsNullOrEmpty(comboBox2.Text))
					{
						if (!string.IsNullOrEmpty(selectionString) && !string.IsNullOrEmpty(groupByString))
						{
							if (comboBox2.Text == "BrandID" || comboBox2.Text == "CategoryID" || comboBox2.Text == "ClassID")
							{
								selectionString = selectionString + ",  P. " + comboBox2.Text + " AS Group2,'" + comboBox2.Text + "' AS Group2Type";
								groupByString = groupByString + ", P.  " + comboBox2.Text;
							}
							else if (comboBox2.Text == "VoucherID")
							{
								selectionString = selectionString + " ,ASD.[Doc Number]  AS Group2, '" + comboBox2.Text + "' AS Group2Type";
								groupByString += " ,ASD. [Doc Number] ";
							}
							else if (comboBox2.Text == "GroupID")
							{
								selectionString = selectionString + " ,CG. " + comboBox2.Text + " AS Group2,'" + comboBox2.Text + "' AS Group2Type ";
								groupByString = groupByString + " ,CG. " + comboBox2.Text;
							}
							else if (comboBox2.Text == "CustomerClassID")
							{
								selectionString = selectionString + " ,Cl. ClassID AS Group2,'" + comboBox2.Text + "' AS Group2Type ";
								groupByString += " ,Cl.ClassID";
							}
							else if (comboBox2.Text == "AreaID")
							{
								selectionString = selectionString + " ,C3.AreaID AS Group2,'" + comboBox2.Text + "' AS Group2Type ";
								groupByString += " ,C3.AreaID";
							}
							else if (comboBox2.Text == "ShortName")
							{
								selectionString = selectionString + " ,C4.CustomerID AS Group2,'" + comboBox2.Text + "' AS Group2Type ";
								groupByString += " ,C4.CustomerID";
							}
							else
							{
								selectionString = selectionString + ",  ASD. " + comboBox2.Text + " AS Group2,'" + comboBox2.Text + "' AS Group2Type";
								groupByString = groupByString + ", ASD.  " + comboBox2.Text;
							}
						}
						else
						{
							selectionString = selectionString + comboBox2.Text + " AS Group2,'" + comboBox2.Text + "' AS Group2Type";
							groupByString += comboBox2.Text;
						}
						GenerateQuery(comboBox2.Text, "Group2");
						filter2 = filter2 + "/" + comboBox2.Text.Substring(0, comboBox2.Text.Length - 2);
					}
					else
					{
						selectionString += ", 0 AS Group2";
						selectionString = selectionString + ",  '" + 0 + "'  AS Group2Type";
					}
					if (!string.IsNullOrEmpty(comboBox3.Text))
					{
						if (!string.IsNullOrEmpty(selectionString) && !string.IsNullOrEmpty(groupByString))
						{
							if (comboBox3.Text == "BrandID" || comboBox3.Text == "CategoryID" || comboBox3.Text == "ClassID")
							{
								selectionString = selectionString + ", P. " + comboBox3.Text + " AS Group3,'" + comboBox3.Text + "' AS Group3Type";
								groupByString = groupByString + ", P.  " + comboBox3.Text;
							}
							else if (comboBox3.Text == "VoucherID")
							{
								selectionString = selectionString + " ,ASD.[Doc Number]  AS Group3, '" + comboBox3.Text + "' AS Group3Type";
								groupByString += " ,ASD. [Doc Number] ";
							}
							else if (comboBox3.Text == "GroupID")
							{
								selectionString = selectionString + " ,CG. " + comboBox3.Text + " AS Group3,'" + comboBox3.Text + "' AS Group3Type ";
								groupByString = groupByString + ", CG. " + comboBox3.Text;
							}
							else if (comboBox3.Text == "CustomerClassID")
							{
								selectionString = selectionString + ", Cl. ClassID AS Group3,'" + comboBox3.Text + "' AS Group3Type ";
								groupByString += " ,Cl. ClassID";
							}
							else if (comboBox3.Text == "AreaID")
							{
								selectionString = selectionString + " ,C3.AreaID AS Group3,'" + comboBox3.Text + "' AS Group3Type ";
								groupByString += " ,C3.AreaID";
							}
							else if (comboBox3.Text == "ShortName")
							{
								selectionString = selectionString + ", C4.CustomerID AS Group3,'" + comboBox3.Text + "' AS Group3Type ";
								groupByString += " ,C4.CustomerID";
							}
							else
							{
								selectionString = selectionString + ", ASD. " + comboBox3.Text + " AS Group3,'" + comboBox3.Text + "' AS Group3Type";
								groupByString = groupByString + ", ASD.  " + comboBox3.Text;
							}
						}
						else
						{
							selectionString = selectionString + comboBox3.Text + " AS Group3,'" + comboBox3.Text + "' AS Group3Type";
							groupByString += comboBox3.Text;
						}
						GenerateQuery(comboBox3.Text, "Group3");
						filter2 = filter2 + "/" + comboBox3.Text.Substring(0, comboBox3.Text.Length - 2);
					}
					else
					{
						selectionString += ",  0 AS Group3";
						selectionString = selectionString + ",  '" + 0 + "'  AS Group3Type";
					}
					if (!string.IsNullOrEmpty(comboBox4.Text))
					{
						if (!string.IsNullOrEmpty(selectionString) && !string.IsNullOrEmpty(groupByString))
						{
							if (comboBox4.Text == "BrandID" || comboBox4.Text == "CategoryID" || comboBox4.Text == "ClassID")
							{
								selectionString = selectionString + ", P. " + comboBox4.Text + " AS Group4,'" + comboBox4.Text + "' AS Group4Type";
								groupByString = groupByString + ", P.  " + comboBox4.Text;
							}
							else if (comboBox4.Text == "VoucherID")
							{
								selectionString = selectionString + " ,ASD.[Doc Number]  AS Group4, '" + comboBox4.Text + "' AS Group4Type";
								groupByString += " ,ASD.[Doc Number] ";
							}
							else if (comboBox4.Text == "GroupID")
							{
								selectionString = selectionString + ", CG. " + comboBox4.Text + " AS Group4,'" + comboBox4.Text + "' AS Group4Type ";
								groupByString = groupByString + " ,CG. " + comboBox4.Text;
							}
							else if (comboBox4.Text == "CustomerClassID")
							{
								selectionString = selectionString + " ,Cl. ClassID AS Group4,'" + comboBox4.Text + "' AS Group4Type ";
								groupByString += " ,Cl. ClassID";
							}
							else if (comboBox4.Text == "AreaID")
							{
								selectionString = selectionString + ", C3.AreaID AS Group4,'" + comboBox4.Text + "' AS Group4Type ";
								groupByString += " ,C3.AreaID";
							}
							else if (comboBox4.Text == "ShortName")
							{
								selectionString = selectionString + " ,C4.CustomerID AS Group4,'" + comboBox4.Text + "' AS Group4Type ";
								groupByString += " ,C4.CustomerID";
							}
							else
							{
								selectionString = selectionString + ", ASD. " + comboBox4.Text + " AS Group4,'" + comboBox4.Text + "' AS Group4Type";
								groupByString = groupByString + ", ASD.  " + comboBox4.Text;
							}
						}
						else
						{
							selectionString = selectionString + comboBox4.Text + " AS Group4,'" + comboBox4.Text + "' AS Group4Type";
							groupByString += comboBox4.Text;
						}
						GenerateQuery(comboBox4.Text, "Group4");
						filter2 = filter2 + "/" + comboBox4.Text.Substring(0, comboBox4.Text.Length - 2);
					}
					else
					{
						selectionString += ", 0 AS Group4";
						selectionString = selectionString + ",  '" + 0 + "'  AS Group4Type";
					}
					if (!string.IsNullOrEmpty(comboBox5.Text))
					{
						if (!string.IsNullOrEmpty(selectionString) && !string.IsNullOrEmpty(groupByString))
						{
							if (comboBox5.Text == "BrandID" || comboBox5.Text == "CategoryID" || comboBox5.Text == "ClassID")
							{
								selectionString = selectionString + ", P. " + comboBox5.Text + " AS Group5,'" + comboBox5.Text + "' AS Group5Type";
								groupByString = groupByString + ", P.  " + comboBox5.Text;
							}
							else if (comboBox5.Text == "VoucherID")
							{
								selectionString = " ,ASD.[Doc Number]  AS Group5, '" + comboBox5.Text + "' AS Group5Type";
								groupByString = " ,ASD. [Doc Number] ";
							}
							else if (comboBox5.Text == "GroupID")
							{
								selectionString = selectionString + " ,CG. " + comboBox5.Text + " AS Group5,'" + comboBox5.Text + "' AS Group5Type ";
								groupByString = groupByString + " ,CG. " + comboBox5.Text;
							}
							else if (comboBox5.Text == "CustomerClassID")
							{
								selectionString = selectionString + " ,Cl. ClassID AS Group5,'" + comboBox5.Text + "' AS Group5Type ";
								groupByString += " ,Cl. ClassID";
							}
							else if (comboBox5.Text == "AreaID")
							{
								selectionString = selectionString + " ,C3.AreaID AS Group5,'" + comboBox5.Text + "' AS Group5Type ";
								groupByString += " ,C3.AreaID";
							}
							else if (comboBox5.Text == "ShortName")
							{
								selectionString = selectionString + ", C4.CustomerID AS Group5,'" + comboBox5.Text + "' AS Group5Type ";
								groupByString += " ,C4.CustomerID";
							}
							else
							{
								selectionString = selectionString + ", ASD. " + comboBox5.Text + " AS Group5,'" + comboBox5.Text + "' AS Group5Type";
								groupByString = groupByString + ", ASD.  " + comboBox5.Text;
							}
						}
						else
						{
							selectionString = selectionString + comboBox5.Text + " AS Group5,'" + comboBox5.Text + "' AS Group5Type";
							groupByString += comboBox5.Text;
						}
						GenerateQuery(comboBox5.Text, "Group5");
						filter2 = filter2 + "/" + comboBox5.Text.Substring(0, comboBox5.Text.Length - 2);
					}
					else
					{
						selectionString += ", 0 AS Group5";
						selectionString = selectionString + ",  '" + 0 + "'  AS Group5Type";
					}
					DataSet data = Factory.ProductSystem.GetSalesByItemCustomerSalespersonReport(selectionString, groupByString, fields, joinQuery, dateControl1.FromDate, dateControl1.ToDate, productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, productSelector.FromBrand, productSelector.ToBrand, customerSelector.FromCustomer, customerSelector.ToCustomer, customerSelector.FromClass, customerSelector.ToClass, customerSelector.FromGroup, customerSelector.ToGroup, customerSelector.FromArea, customerSelector.ToArea, customerSelector.FromCountry, customerSelector.ToCountry, salespersonSelector.FromSalesperson, salespersonSelector.ToSalesperson, salespersonSelector.FromDivision, salespersonSelector.ToDivision, salespersonSelector.FromGroup, salespersonSelector.ToGroup, salespersonSelector.FromArea, salespersonSelector.ToArea, salespersonSelector.FromCountry, salespersonSelector.ToCountry, locationSelector.FromLocation, locationSelector.ToLocation, customerSelector.MultipleCustomers);
					xtraReport = reportHelper.GetReport("Sales GroupBy");
					string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
					reportHelper.AddGeneralReportData(ref data, reportFilter, filter2);
					reportHelper.AddFilterData(ref data, GetAllFormControls(this));
					if (xtraReport == null)
					{
						ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Sales GroupBy.repx'");
					}
					else
					{
						xtraReport.DataSource = data;
						reportHelper.ShowReport(xtraReport);
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void comboBoxGroupBy_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxGroupBy_SelectedValueChanged(object sender, EventArgs e)
		{
		}

		private void radioButtonSummary_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void radioButtonDetail_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void radioButtonReportTo_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void GenerateQuery(string comboText, string groupName)
		{
			if (comboText == "SalespersonID")
			{
				selectionString = selectionString + " , FullName  AS [ " + groupName + " Name]";
				groupByString += " , FullName";
				joinQuery += " LEFT OUTER JOIN Salesperson SP ON ASD.SalespersonID=SP.SalespersonID";
			}
			else if (comboText == "CustomerID")
			{
				selectionString = selectionString + " , C.CustomerName  AS [ " + groupName + " Name],(CASE WHEN ISNULL(C.ShortName,'')='' THEN '' ELSE ' [' + C.ShortName + ']' END) AS CustomerShortName";
				groupByString += " , C.CustomerName, C.ShortName";
				joinQuery += " LEFT OUTER JOIN Customer C ON ASD.CustomerID=C.CustomerID";
			}
			else if (comboText == "BrandID")
			{
				selectionString = selectionString + " , BrandName   AS [ " + groupName + " Name]";
				groupByString += " , BrandName";
				joinQuery += " LEFT OUTER JOIN Product_Brand PB ON P.BrandID=PB.BrandID";
			}
			else if (comboText == "CategoryID")
			{
				selectionString = selectionString + " , CategoryName  AS [ " + groupName + " Name]";
				groupByString += " , CategoryName";
				joinQuery += " LEFT OUTER JOIN Product_Category PC ON P.CategoryID=PC.CategoryID";
			}
			else if (comboText == "ClassID")
			{
				selectionString = selectionString + " , ClassName   AS [ " + groupName + " Name]";
				groupByString += " , ClassName";
				joinQuery += " LEFT OUTER JOIN Product_Class PS ON P.ClassID=PS.ClassID";
			}
			else if (comboText == "ProductID")
			{
				selectionString = selectionString + " , P.Description   AS [ " + groupName + " Name], P.UnitID ";
				groupByString += " , P.Description, P.UnitID";
			}
			else if (comboText == "GroupID")
			{
				selectionString = selectionString + " , CG.GroupName  AS [ " + groupName + " Name]";
				groupByString += " , CG.GroupName";
				joinQuery += " LEFT OUTER JOIN Customer C1 ON ASD.CustomerID=C1.CustomerID LEFT OUTER JOIN Customer_Group CG ON CG.GroupID=C1.CustomerGroupID";
			}
			else if (comboText == "CustomerClassID")
			{
				selectionString = selectionString + " , Cl.ClassName  AS [ " + groupName + " Name]";
				groupByString += " , Cl.ClassName";
				joinQuery += " LEFT OUTER JOIN Customer C2 ON ASD.CustomerID=C2.CustomerID LEFT OUTER JOIN Customer_Class Cl ON Cl.ClassID=C2.CustomerClassID";
			}
			else if (comboText == "AreaID")
			{
				selectionString = selectionString + " , AR.AreaName  AS [ " + groupName + " Name]";
				groupByString += " , AR.AreaName";
				joinQuery += " LEFT OUTER JOIN Customer C3 ON ASD.CustomerID=C3.CustomerID LEFT OUTER JOIN Area AR ON AR.AreaID=C3.AreaID";
			}
			else if (comboText == "ShortName")
			{
				selectionString = selectionString + " , CASE WHEN C4.ShortName = '' THEN  C4.CustomerName ELSE C4.CustomerName + ' [' + C4.ShortName + ']' END   AS [ " + groupName + " Name]";
				groupByString += " , C4.ShortName,C4.CustomerName";
				joinQuery += " LEFT OUTER JOIN Customer C4 ON ASD.CustomerID=C4.CustomerID";
			}
		}

		private void InitializeControls()
		{
			checked
			{
				foreach (Control control in ultraGroupBoxGroupBy.Controls)
				{
					if (control.GetType() == typeof(ComboBox))
					{
						ComboBox obj = control as ComboBox;
						comboCount++;
						obj.SelectedIndexChanged += comboBox_SelectedIndexChanged;
					}
				}
			}
		}

		private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;
			int num = int.Parse(comboBox.Name.Substring(8, 1));
			string text = comboBox.Text;
			foreach (Control control in ultraGroupBoxGroupBy.Controls)
			{
				if (control.GetType() == typeof(ComboBox))
				{
					ComboBox comboBox2 = control as ComboBox;
					int num2 = int.Parse(comboBox2.Name.Substring(8, 1));
					if (!(comboBox.Name == comboBox2.Name))
					{
						if (num < num2 && comboBox2.Text != "")
						{
							comboBox2.Text = string.Empty;
						}
						if (num < num2 && (text == comboBox2.Text || text == ""))
						{
							comboBox2.Text = string.Empty;
						}
						if (num > num2 && text == comboBox2.Text && comboBox2.Text != "")
						{
							ErrorHelper.InformationMessage("Group is already selected");
							comboBox2.Text = string.Empty;
						}
						if (num > num2 && text != "" && comboBox2.Text == "")
						{
							ErrorHelper.InformationMessage("Please Select Value In  Group " + num2);
							comboBox.Text = string.Empty;
						}
					}
				}
			}
		}

		public IEnumerable<Control> GetAllFormControls(Control control)
		{
			IEnumerable<Control> controls = control.Controls.Cast<Control>();
			return controls.SelectMany((Control ctrl) => GetAllFormControls(ctrl).Concat(controls));
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(comboBox1.Text))
			{
				selectionString = comboBox1.Text + " AS Group1";
				groupByString = comboBox1.Text;
			}
			if (!string.IsNullOrEmpty(comboBox2.Text))
			{
				if (!string.IsNullOrEmpty(selectionString) && !string.IsNullOrEmpty(groupByString))
				{
					selectionString = selectionString + ", " + comboBox2.Text + " AS Group2";
					groupByString = groupByString + ", " + comboBox2.Text;
				}
				else
				{
					selectionString = selectionString + comboBox2.Text + " AS Group2";
					groupByString += comboBox2.Text;
				}
			}
			if (!string.IsNullOrEmpty(comboBox3.Text))
			{
				if (!string.IsNullOrEmpty(selectionString) && !string.IsNullOrEmpty(groupByString))
				{
					selectionString = selectionString + ", " + comboBox3.Text + " AS Group3";
					groupByString = groupByString + ", " + comboBox3.Text;
				}
				else
				{
					selectionString = selectionString + comboBox3.Text + " AS Group3";
					groupByString += comboBox3.Text;
				}
			}
			if (!string.IsNullOrEmpty(comboBox4.Text))
			{
				if (!string.IsNullOrEmpty(selectionString) && !string.IsNullOrEmpty(groupByString))
				{
					selectionString = selectionString + ", " + comboBox4.Text + " AS Group4";
					groupByString = groupByString + ", " + comboBox4.Text;
				}
				else
				{
					selectionString = selectionString + comboBox4.Text + " AS Group4";
					groupByString += comboBox4.Text;
				}
			}
			if (!string.IsNullOrEmpty(comboBox5.Text))
			{
				if (!string.IsNullOrEmpty(selectionString) && !string.IsNullOrEmpty(groupByString))
				{
					selectionString = selectionString + ", " + comboBox5.Text + " AS Group5";
					groupByString = groupByString + ", " + comboBox5.Text;
				}
				else
				{
					selectionString = selectionString + comboBox5.Text + " AS Group5";
					groupByString += comboBox5.Text;
				}
			}
		}

		private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void radioButtonCategory_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void radioButtonClass_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void SalesByItemCustomerSalespersonGrpByReport_Load(object sender, EventArgs e)
		{
		}

		private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Sales.SalesReport));
			buttonOK = new System.Windows.Forms.Button();
			buttonClose = new System.Windows.Forms.Button();
			button1 = new System.Windows.Forms.Button();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraGroupBox4 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraGroupBox5 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraGroupBoxGroupBy = new Infragistics.Win.Misc.UltraGroupBox();
			comboBox1 = new System.Windows.Forms.ComboBox();
			comboBox5 = new System.Windows.Forms.ComboBox();
			comboBox2 = new System.Windows.Forms.ComboBox();
			comboBox4 = new System.Windows.Forms.ComboBox();
			comboBox3 = new System.Windows.Forms.ComboBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel17 = new Micromind.UISupport.MMLabel();
			dateControl1 = new Micromind.DataControls.DateControl();
			locationSelector = new Micromind.DataControls.LocationSelector();
			salespersonSelector = new Micromind.DataControls.SalespersonSelector();
			customerSelector = new Micromind.DataControls.CustomerSelector();
			productSelector = new Micromind.DataControls.ProductSelector();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).BeginInit();
			ultraGroupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).BeginInit();
			ultraGroupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox5).BeginInit();
			ultraGroupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBoxGroupBy).BeginInit();
			ultraGroupBoxGroupBy.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(759, 475);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(863, 475);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			button1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			button1.Location = new System.Drawing.Point(655, 475);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(98, 24);
			button1.TabIndex = 13;
			button1.Text = "&Query";
			button1.UseVisualStyleBackColor = true;
			button1.Visible = false;
			button1.Click += new System.EventHandler(button1_Click);
			ultraGroupBox2.Controls.Add(customerSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(14, 236);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(476, 185);
			ultraGroupBox2.TabIndex = 18;
			ultraGroupBox2.Text = "Customers";
			ultraGroupBox3.Controls.Add(productSelector);
			ultraGroupBox3.Location = new System.Drawing.Point(14, 12);
			ultraGroupBox3.Name = "ultraGroupBox3";
			ultraGroupBox3.Size = new System.Drawing.Size(476, 219);
			ultraGroupBox3.TabIndex = 17;
			ultraGroupBox3.Text = "Items";
			ultraGroupBox4.Controls.Add(locationSelector);
			ultraGroupBox4.Location = new System.Drawing.Point(14, 423);
			ultraGroupBox4.Name = "ultraGroupBox4";
			ultraGroupBox4.Size = new System.Drawing.Size(476, 76);
			ultraGroupBox4.TabIndex = 20;
			ultraGroupBox4.Text = "Locations";
			ultraGroupBox5.Controls.Add(salespersonSelector);
			ultraGroupBox5.Location = new System.Drawing.Point(503, 12);
			ultraGroupBox5.Name = "ultraGroupBox5";
			ultraGroupBox5.Size = new System.Drawing.Size(457, 162);
			ultraGroupBox5.TabIndex = 19;
			ultraGroupBox5.Text = "Salespersons";
			ultraGroupBoxGroupBy.Controls.Add(mmLabel4);
			ultraGroupBoxGroupBy.Controls.Add(mmLabel3);
			ultraGroupBoxGroupBy.Controls.Add(mmLabel2);
			ultraGroupBoxGroupBy.Controls.Add(mmLabel1);
			ultraGroupBoxGroupBy.Controls.Add(mmLabel17);
			ultraGroupBoxGroupBy.Controls.Add(comboBox1);
			ultraGroupBoxGroupBy.Controls.Add(comboBox5);
			ultraGroupBoxGroupBy.Controls.Add(comboBox2);
			ultraGroupBoxGroupBy.Controls.Add(comboBox4);
			ultraGroupBoxGroupBy.Controls.Add(comboBox3);
			ultraGroupBoxGroupBy.Location = new System.Drawing.Point(503, 236);
			ultraGroupBoxGroupBy.Name = "ultraGroupBoxGroupBy";
			ultraGroupBoxGroupBy.Size = new System.Drawing.Size(457, 184);
			ultraGroupBoxGroupBy.TabIndex = 22;
			ultraGroupBoxGroupBy.Text = "Group By";
			comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBox1.FormattingEnabled = true;
			comboBox1.Items.AddRange(new object[12]
			{
				"",
				"AreaID",
				"BrandID",
				"CategoryID",
				"ClassID",
				"CustomerClassID",
				"CustomerID",
				"GroupID",
				"ProductID",
				"SalespersonID",
				"ShortName",
				"VoucherID"
			});
			comboBox1.Location = new System.Drawing.Point(111, 19);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new System.Drawing.Size(121, 21);
			comboBox1.TabIndex = 10;
			comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBox5.FormattingEnabled = true;
			comboBox5.Items.AddRange(new object[12]
			{
				"",
				"AreaID",
				"BrandID",
				"CategoryID",
				"ClassID",
				"CustomerClassID",
				"CustomerID",
				"GroupID",
				"ProductID",
				"SalespersonID",
				"ShortName",
				"VoucherID"
			});
			comboBox5.Location = new System.Drawing.Point(111, 127);
			comboBox5.Name = "comboBox5";
			comboBox5.Size = new System.Drawing.Size(121, 21);
			comboBox5.TabIndex = 15;
			comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBox2.FormattingEnabled = true;
			comboBox2.Items.AddRange(new object[12]
			{
				"",
				"AreaID",
				"BrandID",
				"CategoryID",
				"ClassID",
				"CustomerClassID",
				"CustomerID",
				"GroupID",
				"ProductID",
				"SalespersonID",
				"ShortName",
				"VoucherID"
			});
			comboBox2.Location = new System.Drawing.Point(111, 46);
			comboBox2.Name = "comboBox2";
			comboBox2.Size = new System.Drawing.Size(121, 21);
			comboBox2.TabIndex = 11;
			comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBox4.FormattingEnabled = true;
			comboBox4.Items.AddRange(new object[12]
			{
				"",
				"AreaID",
				"BrandID",
				"CategoryID",
				"ClassID",
				"CustomerClassID",
				"CustomerID",
				"GroupID",
				"ProductID",
				"SalespersonID",
				"ShortName",
				"VoucherID"
			});
			comboBox4.Location = new System.Drawing.Point(111, 100);
			comboBox4.Name = "comboBox4";
			comboBox4.Size = new System.Drawing.Size(121, 21);
			comboBox4.TabIndex = 14;
			comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBox3.FormattingEnabled = true;
			comboBox3.Items.AddRange(new object[12]
			{
				"",
				"AreaID",
				"BrandID",
				"CategoryID",
				"ClassID",
				"CustomerClassID",
				"CustomerID",
				"GroupID",
				"ProductID",
				"SalespersonID",
				"ShortName",
				"VoucherID"
			});
			comboBox3.Location = new System.Drawing.Point(111, 73);
			comboBox3.Name = "comboBox3";
			comboBox3.Size = new System.Drawing.Size(121, 21);
			comboBox3.TabIndex = 12;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(19, 104);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(49, 13);
			mmLabel4.TabIndex = 31;
			mmLabel4.Text = "Group 4:";
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(19, 77);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(49, 13);
			mmLabel3.TabIndex = 30;
			mmLabel3.Text = "Group 3:";
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(19, 50);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(49, 13);
			mmLabel2.TabIndex = 29;
			mmLabel2.Text = "Group 2:";
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(19, 23);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(49, 13);
			mmLabel1.TabIndex = 28;
			mmLabel1.Text = "Group 1:";
			mmLabel17.AutoSize = true;
			mmLabel17.BackColor = System.Drawing.Color.Transparent;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel17.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(19, 131);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(49, 13);
			mmLabel17.TabIndex = 27;
			mmLabel17.Text = "Group 5:";
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 10, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(503, 177);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(457, 54);
			dateControl1.TabIndex = 21;
			dateControl1.ToDate = new System.DateTime(2017, 10, 18, 23, 59, 59, 59);
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(6, 19);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(436, 55);
			locationSelector.TabIndex = 0;
			salespersonSelector.BackColor = System.Drawing.Color.Transparent;
			salespersonSelector.Location = new System.Drawing.Point(6, 19);
			salespersonSelector.Name = "salespersonSelector";
			salespersonSelector.Size = new System.Drawing.Size(436, 137);
			salespersonSelector.TabIndex = 0;
			customerSelector.BackColor = System.Drawing.Color.Transparent;
			customerSelector.CustomReportFieldName = "";
			customerSelector.CustomReportKey = "";
			customerSelector.CustomReportValueType = 1;
			customerSelector.Location = new System.Drawing.Point(6, 19);
			customerSelector.Name = "customerSelector";
			customerSelector.Size = new System.Drawing.Size(436, 165);
			customerSelector.TabIndex = 0;
			productSelector.BackColor = System.Drawing.Color.Transparent;
			productSelector.CustomReportFieldName = "";
			productSelector.CustomReportKey = "";
			productSelector.CustomReportValueType = 1;
			productSelector.Location = new System.Drawing.Point(5, 18);
			productSelector.Name = "productSelector";
			productSelector.ShowOnlyAssemlbyItems = false;
			productSelector.Size = new System.Drawing.Size(446, 186);
			productSelector.TabIndex = 0;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(978, 511);
			base.Controls.Add(ultraGroupBoxGroupBy);
			base.Controls.Add(dateControl1);
			base.Controls.Add(ultraGroupBox4);
			base.Controls.Add(ultraGroupBox5);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(ultraGroupBox3);
			base.Controls.Add(button1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "SalesReport";
			Text = "Sales  Report";
			base.Load += new System.EventHandler(SalesByItemCustomerSalespersonGrpByReport_Load);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).EndInit();
			ultraGroupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).EndInit();
			ultraGroupBox4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox5).EndInit();
			ultraGroupBox5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBoxGroupBy).EndInit();
			ultraGroupBoxGroupBy.ResumeLayout(false);
			ultraGroupBoxGroupBy.PerformLayout();
			ResumeLayout(false);
		}
	}
}
