using DevExpress.XtraReports.UI;
using Micromind.ClientLibraries;
using Micromind.ClientUI.WindowsForms.DataEntries.Printing;
using Micromind.Common.Data;
using Micromind.DataControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.Libraries
{
	public class ReportHelper
	{
		public XtraReport GetReport(string fileName)
		{
			try
			{
				CompanyInformationData companyInformation = Global.CompanyInformation;
				string text = "\\Print Templates";
				if (companyInformation.CompanyInformationTable.Rows[0]["TemplatePathFolder"] != DBNull.Value)
				{
					text = companyInformation.CompanyInformationTable.Rows[0]["TemplatePathFolder"].ToString();
				}
				XtraReport xtraReport = new XtraReport();
				if (text != "")
				{
					string text2 = Path.GetDirectoryName(Application.ExecutablePath) + text + "\\Reports\\" + fileName + ".repx";
					xtraReport.LoadLayout(text2);
					xtraReport.Tag = text2;
				}
				else
				{
					string text3 = Path.GetDirectoryName(Application.ExecutablePath) + "\\Print Templates\\Reports\\" + fileName + ".repx";
					xtraReport.LoadLayout(text3);
					xtraReport.Tag = text3;
				}
				return xtraReport;
			}
			catch
			{
				return null;
			}
		}

		public void AddGeneralReportData(ref DataSet data, string reportFilter)
		{
			if (data != null)
			{
				data.Tables.Add("ReportInfo");
				if (data.Tables.Count > 0)
				{
					data.Tables["ReportInfo"].Columns.Add("CompanyName");
					data.Tables["ReportInfo"].Columns.Add("UserID");
					data.Tables["ReportInfo"].Columns.Add("UserName");
					data.Tables["ReportInfo"].Columns.Add("ReportFilter");
					data.Tables["ReportInfo"].Columns.Add("IsPrintOnLetterHead");
					object fieldValue = Factory.DatabaseSystem.GetFieldValue("Users", "UserName", "UserID", Global.CurrentUser);
					data.Tables["ReportInfo"].Rows.Add(Global.CompanyName, Global.CurrentUser, fieldValue.ToString(), reportFilter, UserPreferences.PrintReportOnLetterHead);
				}
			}
		}

		public void AddGeneralReportData(ref DataSet data, string reportFilter, string reportFilter2)
		{
			if (data != null)
			{
				data.Tables.Add("ReportInfo");
				if (data.Tables.Count > 0)
				{
					data.Tables["ReportInfo"].Columns.Add("CompanyName");
					data.Tables["ReportInfo"].Columns.Add("UserID");
					data.Tables["ReportInfo"].Columns.Add("UserName");
					data.Tables["ReportInfo"].Columns.Add("ReportFilter");
					data.Tables["ReportInfo"].Columns.Add("ReportGroupBy");
					data.Tables["ReportInfo"].Columns.Add("IsPrintOnLetterHead");
					object fieldValue = Factory.DatabaseSystem.GetFieldValue("Users", "UserName", "UserID", Global.CurrentUser);
					data.Tables["ReportInfo"].Rows.Add(Global.CompanyName, Global.CurrentUser, fieldValue.ToString(), reportFilter, reportFilter2, UserPreferences.PrintReportOnLetterHead);
				}
			}
		}

		private string AddColumns(ref DataTable filterTable, string columName, Type type)
		{
			int num = 0;
			string text = columName;
			while (filterTable.Columns.Contains(text))
			{
				num = checked(num + 1);
				text += num;
			}
			columName = ((columName == text) ? columName : text);
			filterTable.Columns.Add(columName, type);
			return columName;
		}

		public void AddFilterData(ref DataSet data, IEnumerable<Control> allControls)
		{
			if (data != null)
			{
				
				DataTable filterTable = new DataTable();
				filterTable.TableName = "FilterData";
				filterTable.Columns.Add("Id");
				filterTable.Rows.Add("1");
				List<Control> list = allControls.Distinct().ToList();
				List<Control> list2 = new List<Control>();
				string text = "";
				filterTable.Columns.Add("AllFilter");
				foreach (Control item in list)
				{
					_ = item.Parent;
					if (item is DateControl && !list2.Contains(item))
					{
						try
						{
							DateControl dateControl = (DateControl)item;
							item.Tag = ((!(item.Tag is string) || item.Tag as string== "") ? "Date" : item.Tag);
							string text2 = AddColumns(ref filterTable, item.Tag + "From", typeof(DateTime));
							string text3 = AddColumns(ref filterTable, item.Tag + "To", typeof(DateTime));
							filterTable.Rows[0][text2] = dateControl.FromDate;
							filterTable.Rows[0][text3] = dateControl.ToDate;
							text = text + text2 + ":" + dateControl.FromDate + " " + text3 + ":" + dateControl.ToDate + "; ";
							list2.Add(dateControl);
						}
						catch (Exception ex)
						{
							ErrorHelper.ErrorMessage(ex.Message);
						}
					}
					else if (item is DateBetween && !list2.Contains(item))
					{
						try
						{
							DateBetween dateBetween = (DateBetween)item;
							item.Tag = ((!(item.Tag is string) || item.Tag as string== "") ? "Date" : item.Tag);
							string text4 = AddColumns(ref filterTable, item.Tag + "From", typeof(DateTime));
							string text5 = AddColumns(ref filterTable, item.Tag + "To", typeof(DateTime));
							filterTable.Rows[0][text4] = dateBetween.FromDate;
							filterTable.Rows[0][text5] = dateBetween.ToDate;
							text = text + text4 + ":" + dateBetween.FromDate + " " + text5 + ":" + dateBetween.ToDate + "; ";
							list2.Add(dateBetween);
						}
						catch (Exception ex2)
						{
							ErrorHelper.ErrorMessage(ex2.Message);
						}
					}
					else if (item is LocationSelector && !list2.Contains(item))
					{
						try
						{
							LocationSelector locationSelector = (LocationSelector)item;
							string text6 = AddColumns(ref filterTable, item.Tag + "Location From", typeof(string));
							string columnName = AddColumns(ref filterTable, item.Tag + "Location From Name", typeof(string));
							string text7 = AddColumns(ref filterTable, item.Tag + "Location To", typeof(string));
							string columnName2 = AddColumns(ref filterTable, item.Tag + "Location To Name", typeof(string));
							filterTable.Rows[0][text6] = locationSelector.FromLocation;
							filterTable.Rows[0][columnName] = locationSelector.FromLocationName;
							filterTable.Rows[0][text7] = locationSelector.ToLocation;
							filterTable.Rows[0][columnName2] = locationSelector.ToLocationName;
							if (locationSelector.FromLocation != "" && locationSelector.FromLocation != locationSelector.ToLocation)
							{
								text = text + text6 + ":" + locationSelector.FromLocationName + " " + text7 + ":" + locationSelector.ToLocationName + "; ";
							}
							else if (locationSelector.FromLocation != "" && locationSelector.FromLocation != locationSelector.ToLocation)
							{
								text = text + "Location :" + locationSelector.FromLocationName + "; ";
							}
							else if (locationSelector.FromLocation != "")
							{
								text = text + "Location :" + locationSelector.FromLocationName + "; ";
							}
							list2.Add(locationSelector);
						}
						catch (Exception ex3)
						{
							ErrorHelper.ErrorMessage(ex3.Message);
						}
					}
					else if (item is DivisionSelector && !list2.Contains(item))
					{
						try
						{
							DivisionSelector divisionSelector = (DivisionSelector)item;
							string text8 = AddColumns(ref filterTable, item.Tag + "Division From", typeof(string));
							string text9 = AddColumns(ref filterTable, item.Tag + "Division To", typeof(string));
							filterTable.Rows[0][text8] = divisionSelector.FromDivision;
							filterTable.Rows[0][text9] = divisionSelector.ToDivision;
							if (divisionSelector.FromDivision != "" && divisionSelector.FromDivision != divisionSelector.ToDivision)
							{
								text = text + text8 + ":" + divisionSelector.FromDivision + " " + text9 + ":" + divisionSelector.ToDivision + "; ";
							}
							else if (divisionSelector.FromDivision != "" && divisionSelector.FromDivision != divisionSelector.ToDivision)
							{
								text = text + "Division :" + divisionSelector.FromDivision + "; ";
							}
							else if (divisionSelector.FromDivision != "")
							{
								text = text + "Division :" + divisionSelector.FromDivision + "; ";
							}
							list2.Add(divisionSelector);
						}
						catch (Exception ex4)
						{
							ErrorHelper.ErrorMessage(ex4.Message);
						}
					}
					else if (item is AccountSelector && !list2.Contains(item))
					{
						try
						{
							AccountSelector accountSelector = (AccountSelector)item;
							string text10 = AddColumns(ref filterTable, item.Tag + "Account From", typeof(string));
							string columnName3 = AddColumns(ref filterTable, item.Tag + "Account From Name", typeof(string));
							string text11 = AddColumns(ref filterTable, item.Tag + "Account To", typeof(string));
							string columnName4 = AddColumns(ref filterTable, item.Tag + "Account To Name", typeof(string));
							filterTable.Rows[0][text10] = accountSelector.FromAccount;
							filterTable.Rows[0][text11] = accountSelector.ToAccount;
							filterTable.Rows[0][columnName3] = accountSelector.FromAccountName;
							filterTable.Rows[0][columnName4] = accountSelector.ToAccountName;
							if (accountSelector.FromAccount != "" && accountSelector.FromAccount != accountSelector.ToAccount)
							{
								text = text + text10 + ":" + accountSelector.FromAccountName + " " + text11 + ":" + accountSelector.ToAccountName + "; ";
							}
							else if (accountSelector.FromAccount != "" && accountSelector.FromAccount != accountSelector.ToAccount)
							{
								text = text + "Account :" + accountSelector.FromAccountName + "; ";
							}
							list2.Add(accountSelector);
						}
						catch (Exception ex5)
						{
							ErrorHelper.ErrorMessage(ex5.Message);
						}
					}
					else if (item is AnalysisGroupSelector && !list2.Contains(item))
					{
						try
						{
							AnalysisGroupSelector analysisGroupSelector = (AnalysisGroupSelector)item;
							string text12 = AddColumns(ref filterTable, item.Tag + "Analysis From", typeof(string));
							string text13 = AddColumns(ref filterTable, item.Tag + "Analysis To", typeof(string));
							string text14 = AddColumns(ref filterTable, item.Tag + "Analysis Group From", typeof(string));
							string text15 = AddColumns(ref filterTable, item.Tag + "Analysis Group To", typeof(string));
							filterTable.Rows[0][text12] = analysisGroupSelector.FromAnalysis;
							filterTable.Rows[0][text13] = analysisGroupSelector.ToAnalysis;
							filterTable.Rows[0][text14] = analysisGroupSelector.FromGroup;
							filterTable.Rows[0][text15] = analysisGroupSelector.ToGroup;
							if (analysisGroupSelector.FromAnalysis != "")
							{
								text = text + text12 + ":" + analysisGroupSelector.FromAnalysis + " " + text13 + ":" + analysisGroupSelector.ToAnalysis + "; ";
							}
							if (analysisGroupSelector.FromGroup != "")
							{
								text = text + text14 + ":" + analysisGroupSelector.FromGroup + " " + text15 + ":" + analysisGroupSelector.ToGroup + "; ";
							}
							list2.Add(analysisGroupSelector);
						}
						catch (Exception ex6)
						{
							ErrorHelper.ErrorMessage(ex6.Message);
						}
					}
					else if (item is JobSelector && !list2.Contains(item))
					{
						try
						{
							JobSelector jobSelector = (JobSelector)item;
							string text16 = AddColumns(ref filterTable, item.Tag + "Job From", typeof(string));
							string text17 = AddColumns(ref filterTable, item.Tag + "Job To", typeof(string));
							filterTable.Rows[0][text16] = jobSelector.FromJob;
							filterTable.Rows[0][text17] = jobSelector.ToJob;
							if (jobSelector.FromJob != "")
							{
								text = text + text16 + ":" + jobSelector.FromJob + " " + text17 + ":" + jobSelector.ToJob + "; ";
							}
							list2.Add(jobSelector);
						}
						catch (Exception ex7)
						{
							ErrorHelper.ErrorMessage(ex7.Message);
						}
					}
					else if (item is CustomerGroupSelector && !list2.Contains(item))
					{
						try
						{
							CustomerGroupSelector customerGroupSelector = (CustomerGroupSelector)item;
							string text18 = AddColumns(ref filterTable, item.Tag + "Customer Group From", typeof(string));
							string text19 = AddColumns(ref filterTable, item.Tag + "Customer Group To", typeof(string));
							filterTable.Rows[0][text18] = customerGroupSelector.FromCustomerGroup;
							filterTable.Rows[0][text19] = customerGroupSelector.ToCustomerGroup;
							if (customerGroupSelector.FromCustomerGroup != "")
							{
								text = text + text18 + ":" + customerGroupSelector.FromCustomerGroup + " " + text19 + ":" + customerGroupSelector.ToCustomerGroup + "; ";
							}
							list2.Add(customerGroupSelector);
						}
						catch (Exception ex8)
						{
							ErrorHelper.ErrorMessage(ex8.Message);
						}
					}
					else if (item is ProductCategorySelector && !list2.Contains(item))
					{
						try
						{
							ProductCategorySelector productCategorySelector = (ProductCategorySelector)item;
							string text20 = AddColumns(ref filterTable, item.Tag + "Product Category From", typeof(string));
							string text21 = AddColumns(ref filterTable, item.Tag + "Product Category  To", typeof(string));
							filterTable.Rows[0][text20] = productCategorySelector.FromProductCategory;
							filterTable.Rows[0][text21] = productCategorySelector.ToProductCategory;
							if (productCategorySelector.FromProductCategory != "")
							{
								text = text + text20 + ":" + productCategorySelector.FromProductCategory + " " + text21 + ":" + productCategorySelector.ToProductCategory + "; ";
							}
							list2.Add(productCategorySelector);
						}
						catch (Exception ex9)
						{
							ErrorHelper.ErrorMessage(ex9.Message);
						}
					}
					else if (item is FixedAssetSelector && !list2.Contains(item))
					{
						try
						{
							FixedAssetSelector fixedAssetSelector = (FixedAssetSelector)item;
							string text22 = AddColumns(ref filterTable, item.Tag + "FixedAsset From", typeof(string));
							string text23 = AddColumns(ref filterTable, item.Tag + "FixedAsset To", typeof(string));
							string text24 = AddColumns(ref filterTable, item.Tag + "FixedAsset Class From", typeof(string));
							string text25 = AddColumns(ref filterTable, item.Tag + "FixedAsset Class To", typeof(string));
							string text26 = AddColumns(ref filterTable, item.Tag + "FixedAsset Group From", typeof(string));
							string text27 = AddColumns(ref filterTable, item.Tag + "FixedAsset Group To", typeof(string));
							string text28 = AddColumns(ref filterTable, item.Tag + "FixedAsset Location From", typeof(string));
							string text29 = AddColumns(ref filterTable, item.Tag + "FixedAsset Location To", typeof(string));
							filterTable.Rows[0][text22] = fixedAssetSelector.FromFixedAsset;
							filterTable.Rows[0][text23] = fixedAssetSelector.ToFixedAsset;
							filterTable.Rows[0][text24] = fixedAssetSelector.FromClass;
							filterTable.Rows[0][text25] = fixedAssetSelector.ToClass;
							filterTable.Rows[0][text26] = fixedAssetSelector.FromGroup;
							filterTable.Rows[0][text27] = fixedAssetSelector.ToGroup;
							filterTable.Rows[0][text28] = fixedAssetSelector.FromLocation;
							filterTable.Rows[0][text29] = fixedAssetSelector.ToLocation;
							if (fixedAssetSelector.FromFixedAsset != "")
							{
								text = text + text22 + ":" + fixedAssetSelector.FromFixedAsset + " " + text23 + ":" + fixedAssetSelector.ToFixedAsset + "; ";
							}
							if (fixedAssetSelector.FromClass != "")
							{
								text = text + text24 + ":" + fixedAssetSelector.FromClass + " " + text25 + ":" + fixedAssetSelector.ToClass + "; ";
							}
							if (fixedAssetSelector.FromGroup != "")
							{
								text = text + text26 + ":" + fixedAssetSelector.FromGroup + " " + text27 + ":" + fixedAssetSelector.ToGroup + "; ";
							}
							if (fixedAssetSelector.FromLocation != "")
							{
								text = text + text28 + ":" + fixedAssetSelector.FromLocation + " " + text29 + ":" + fixedAssetSelector.ToLocation + "; ";
							}
							list2.Add(fixedAssetSelector);
						}
						catch (Exception ex10)
						{
							ErrorHelper.ErrorMessage(ex10.Message);
						}
					}
					else if (item is TenantSelector && !list2.Contains(item))
					{
						try
						{
							TenantSelector tenantSelector = (TenantSelector)item;
							string text30 = AddColumns(ref filterTable, item.Tag + "Tenant From", typeof(string));
							string text31 = AddColumns(ref filterTable, item.Tag + "Tenant To", typeof(string));
							string text32 = AddColumns(ref filterTable, item.Tag + "FixedAsset Class From", typeof(string));
							string text33 = AddColumns(ref filterTable, item.Tag + "FixedAsset Class To", typeof(string));
							string text34 = AddColumns(ref filterTable, item.Tag + "FixedAsset Group From", typeof(string));
							string text35 = AddColumns(ref filterTable, item.Tag + "FixedAsset Group To", typeof(string));
							filterTable.Rows[0][text30] = tenantSelector.FromCustomer;
							filterTable.Rows[0][text31] = tenantSelector.ToCustomer;
							filterTable.Rows[0][text32] = tenantSelector.FromClass;
							filterTable.Rows[0][text33] = tenantSelector.ToClass;
							filterTable.Rows[0][text34] = tenantSelector.FromGroup;
							filterTable.Rows[0][text35] = tenantSelector.ToGroup;
							if (tenantSelector.FromCustomer != "")
							{
								text = text + text30 + ":" + tenantSelector.FromCustomer + " " + text31 + ":" + tenantSelector.ToCustomer + "; ";
							}
							if (tenantSelector.FromClass != "")
							{
								text = text + text32 + ":" + tenantSelector.FromClass + " " + text33 + ":" + tenantSelector.ToClass + "; ";
							}
							if (tenantSelector.FromGroup != "")
							{
								text = text + text34 + ":" + tenantSelector.FromGroup + " " + text35 + ":" + tenantSelector.ToGroup + "; ";
							}
							list2.Add(tenantSelector);
						}
						catch (Exception ex11)
						{
							ErrorHelper.ErrorMessage(ex11.Message);
						}
					}
					else if (item is EquipmentSelector && !list2.Contains(item))
					{
						try
						{
							EquipmentSelector equipmentSelector = (EquipmentSelector)item;
							string text36 = AddColumns(ref filterTable, item.Tag + "Equipment From", typeof(string));
							string text37 = AddColumns(ref filterTable, item.Tag + "Equipment To", typeof(string));
							string text38 = AddColumns(ref filterTable, item.Tag + "Equipment Category From", typeof(string));
							string text39 = AddColumns(ref filterTable, item.Tag + "Equipment Category To", typeof(string));
							string text40 = AddColumns(ref filterTable, item.Tag + "Equipment Type From", typeof(string));
							string text41 = AddColumns(ref filterTable, item.Tag + "Equipment Type To", typeof(string));
							filterTable.Rows[0][text36] = equipmentSelector.FromEquipment;
							filterTable.Rows[0][text37] = equipmentSelector.ToEquipment;
							filterTable.Rows[0][text38] = equipmentSelector.FromCategory;
							filterTable.Rows[0][text39] = equipmentSelector.ToCategory;
							filterTable.Rows[0][text40] = equipmentSelector.FromType;
							filterTable.Rows[0][text41] = equipmentSelector.ToType;
							if (equipmentSelector.FromEquipment != "")
							{
								text = text + text36 + ":" + equipmentSelector.FromEquipment + " " + text37 + ":" + equipmentSelector.ToEquipment + "; ";
							}
							if (equipmentSelector.FromCategory != "")
							{
								text = text + text38 + ":" + equipmentSelector.FromCategory + " " + text39 + ":" + equipmentSelector.ToCategory + "; ";
							}
							if (equipmentSelector.FromType != "")
							{
								text = text + text40 + ":" + equipmentSelector.ToType + " " + text41 + ":" + equipmentSelector.ToType + "; ";
							}
							list2.Add(equipmentSelector);
						}
						catch (Exception ex12)
						{
							ErrorHelper.ErrorMessage(ex12.Message);
						}
					}
					else if (item is ProductNISelector && !list2.Contains(item))
					{
						try
						{
							ProductNISelector productNISelector = (ProductNISelector)item;
							string text42 = AddColumns(ref filterTable, item.Tag + "ProductNI From", typeof(string));
							string text43 = AddColumns(ref filterTable, item.Tag + "ProductNI  To", typeof(string));
							string text44 = AddColumns(ref filterTable, item.Tag + "ProductNI Class From", typeof(string));
							string text45 = AddColumns(ref filterTable, item.Tag + "ProductNI Class To", typeof(string));
							string text46 = AddColumns(ref filterTable, item.Tag + "ProductNI Category From", typeof(string));
							string text47 = AddColumns(ref filterTable, item.Tag + "ProductNI Category To", typeof(string));
							filterTable.Rows[0][text42] = productNISelector.FromItem;
							filterTable.Rows[0][text43] = productNISelector.ToItem;
							filterTable.Rows[0][text44] = productNISelector.FromClass;
							filterTable.Rows[0][text45] = productNISelector.ToClass;
							filterTable.Rows[0][text46] = productNISelector.FromCategory;
							filterTable.Rows[0][text47] = productNISelector.ToCategory;
							if (productNISelector.FromItem != "")
							{
								text = text + text42 + ":" + productNISelector.FromItem + " " + text43 + ":" + productNISelector.ToItem + "; ";
							}
							if (productNISelector.FromClass != "")
							{
								text = text + text44 + ":" + productNISelector.FromClass + " " + text45 + ":" + productNISelector.ToClass + "; ";
							}
							if (productNISelector.FromCategory != "")
							{
								text = text + text46 + ":" + productNISelector.FromCategory + " " + text47 + ":" + productNISelector.ToCategory + "; ";
							}
							list2.Add(productNISelector);
						}
						catch (Exception ex13)
						{
							ErrorHelper.ErrorMessage(ex13.Message);
						}
					}
					else if (item is HorseSelector && !list2.Contains(item))
					{
						try
						{
							HorseSelector horseSelector = (HorseSelector)item;
							string text48 = AddColumns(ref filterTable, item.Tag + "Horse From", typeof(string));
							string text49 = AddColumns(ref filterTable, item.Tag + "Horse  To", typeof(string));
							string text50 = AddColumns(ref filterTable, item.Tag + "Horse Class From", typeof(string));
							string text51 = AddColumns(ref filterTable, item.Tag + "Horse Class To", typeof(string));
							string text52 = AddColumns(ref filterTable, item.Tag + "Horse Category From", typeof(string));
							string text53 = AddColumns(ref filterTable, item.Tag + "Horse Category To", typeof(string));
							filterTable.Rows[0][text48] = horseSelector.FromHorse;
							filterTable.Rows[0][text49] = horseSelector.ToHorse;
							filterTable.Rows[0][text50] = horseSelector.FromLocation;
							filterTable.Rows[0][text51] = horseSelector.ToLocation;
							filterTable.Rows[0][text52] = horseSelector.FromTrainer;
							filterTable.Rows[0][text53] = horseSelector.ToTrainer;
							if (horseSelector.FromHorse != "")
							{
								text = text + text48 + ":" + horseSelector.FromHorse + " " + text49 + ":" + horseSelector.ToHorse + "; ";
							}
							if (horseSelector.FromLocation != "")
							{
								text = text + text50 + ":" + horseSelector.FromLocation + " " + text51 + ":" + horseSelector.ToLocation + "; ";
							}
							if (horseSelector.FromTrainer != "")
							{
								text = text + text52 + ":" + horseSelector.FromTrainer + " " + text53 + ":" + horseSelector.ToTrainer + "; ";
							}
							list2.Add(horseSelector);
						}
						catch (Exception ex14)
						{
							ErrorHelper.ErrorMessage(ex14.Message);
						}
					}
					else if (item is W3PLProductSelector && !list2.Contains(item))
					{
						try
						{
							W3PLProductSelector w3PLProductSelector = (W3PLProductSelector)item;
							string text54 = AddColumns(ref filterTable, item.Tag + "W3PLProduct From", typeof(string));
							string text55 = AddColumns(ref filterTable, item.Tag + "W3PLProduct  To", typeof(string));
							string text56 = AddColumns(ref filterTable, item.Tag + "W3PLProduct Class From", typeof(string));
							string text57 = AddColumns(ref filterTable, item.Tag + "W3PLProduct Class To", typeof(string));
							string text58 = AddColumns(ref filterTable, item.Tag + "W3PLProduct Category From", typeof(string));
							string text59 = AddColumns(ref filterTable, item.Tag + "W3PLProduct Category To", typeof(string));
							filterTable.Rows[0][text54] = w3PLProductSelector.FromItem;
							filterTable.Rows[0][text55] = w3PLProductSelector.ToItem;
							filterTable.Rows[0][text56] = w3PLProductSelector.FromClass;
							filterTable.Rows[0][text57] = w3PLProductSelector.ToClass;
							filterTable.Rows[0][text58] = w3PLProductSelector.FromCategory;
							filterTable.Rows[0][text59] = w3PLProductSelector.ToCategory;
							if (w3PLProductSelector.FromItem != "")
							{
								text = text + text54 + ":" + w3PLProductSelector.FromItem + " " + text55 + ":" + w3PLProductSelector.ToItem + "; ";
							}
							if (w3PLProductSelector.FromClass != "")
							{
								text = text + text56 + ":" + w3PLProductSelector.FromClass + " " + text57 + ":" + w3PLProductSelector.ToClass + "; ";
							}
							if (w3PLProductSelector.FromCategory != "")
							{
								text = text + text58 + ":" + w3PLProductSelector.FromCategory + " " + text59 + ":" + w3PLProductSelector.ToCategory + "; ";
							}
							list2.Add(w3PLProductSelector);
						}
						catch (Exception ex15)
						{
							ErrorHelper.ErrorMessage(ex15.Message);
						}
					}
					else if (item is ProductClassSelector && !list2.Contains(item))
					{
						try
						{
							ProductClassSelector productClassSelector = (ProductClassSelector)item;
							string text60 = AddColumns(ref filterTable, item.Tag + "Product Class From", typeof(string));
							string text61 = AddColumns(ref filterTable, item.Tag + "Product Class  To", typeof(string));
							filterTable.Rows[0][text60] = productClassSelector.FromProductClass;
							filterTable.Rows[0][text61] = productClassSelector.ToProductClass;
							if (productClassSelector.FromProductClass != "")
							{
								text = text + text60 + ":" + productClassSelector.FromProductClass + " " + text61 + ":" + productClassSelector.ToProductClass + "; ";
							}
							list2.Add(productClassSelector);
						}
						catch (Exception ex16)
						{
							ErrorHelper.ErrorMessage(ex16.Message);
						}
					}
					else if (item is PropertyAgentSelector && !list2.Contains(item))
					{
						try
						{
							PropertyAgentSelector propertyAgentSelector = (PropertyAgentSelector)item;
							string text62 = AddColumns(ref filterTable, item.Tag + "PropertyAgent From", typeof(string));
							string text63 = AddColumns(ref filterTable, item.Tag + "PropertyAgent To", typeof(string));
							filterTable.Rows[0][text62] = propertyAgentSelector.FromAgent;
							filterTable.Rows[0][text63] = propertyAgentSelector.ToAgent;
							if (propertyAgentSelector.FromAgent != "")
							{
								text = text + text62 + ":" + propertyAgentSelector.FromAgent + " " + text63 + ":" + propertyAgentSelector.ToAgent + "; ";
							}
							list2.Add(propertyAgentSelector);
						}
						catch (Exception ex17)
						{
							ErrorHelper.ErrorMessage(ex17.Message);
						}
					}
					else if (item is WorkLocationSelector && !list2.Contains(item))
					{
						try
						{
							WorkLocationSelector workLocationSelector = (WorkLocationSelector)item;
							string text64 = AddColumns(ref filterTable, item.Tag + "WorkLocation From", typeof(string));
							string text65 = AddColumns(ref filterTable, item.Tag + "WorkLocation To", typeof(string));
							filterTable.Rows[0][text64] = workLocationSelector.FromLocation;
							filterTable.Rows[0][text65] = workLocationSelector.ToLocation;
							if (workLocationSelector.FromLocation != "")
							{
								text = text + text64 + ":" + workLocationSelector.FromLocation + " " + text65 + ":" + workLocationSelector.ToLocation + "; ";
							}
							list2.Add(workLocationSelector);
						}
						catch (Exception ex18)
						{
							ErrorHelper.ErrorMessage(ex18.Message);
						}
					}
					else if (item is CaseClientSelector && !list2.Contains(item))
					{
						try
						{
							CaseClientSelector caseClientSelector = (CaseClientSelector)item;
							string text66 = AddColumns(ref filterTable, item.Tag + "CaseClient From", typeof(string));
							string text67 = AddColumns(ref filterTable, item.Tag + "CaseClient To", typeof(string));
							filterTable.Rows[0][text66] = caseClientSelector.FromCustomer;
							filterTable.Rows[0][text67] = caseClientSelector.ToCustomer;
							if (caseClientSelector.FromCustomer != "")
							{
								text = text + text66 + ":" + caseClientSelector.FromCustomer + " " + text67 + ":" + caseClientSelector.ToCustomer + "; ";
							}
							list2.Add(caseClientSelector);
						}
						catch (Exception ex19)
						{
							ErrorHelper.ErrorMessage(ex19.Message);
						}
					}
					else if (item is LawyerSelector && !list2.Contains(item))
					{
						try
						{
							LawyerSelector lawyerSelector = (LawyerSelector)item;
							string text68 = AddColumns(ref filterTable, item.Tag + "Lawyer From", typeof(string));
							string text69 = AddColumns(ref filterTable, item.Tag + "Lawyer To", typeof(string));
							filterTable.Rows[0][text68] = lawyerSelector.FromLawyer;
							filterTable.Rows[0][text69] = lawyerSelector.ToLawyer;
							if (lawyerSelector.FromLawyer != "")
							{
								text = text + text68 + ":" + lawyerSelector.FromLawyer + " " + text69 + ":" + lawyerSelector.ToLawyer + "; ";
							}
							list2.Add(lawyerSelector);
						}
						catch (Exception ex20)
						{
							ErrorHelper.ErrorMessage(ex20.Message);
						}
					}
					else if (item is ProductParentSelector && !list2.Contains(item))
					{
						try
						{
							ProductParentSelector productParentSelector = (ProductParentSelector)item;
							string text70 = AddColumns(ref filterTable, item.Tag + "MatrixProduct  From", typeof(string));
							string text71 = AddColumns(ref filterTable, item.Tag + "MatrixProduct   To", typeof(string));
							string text72 = AddColumns(ref filterTable, item.Tag + "MatrixProduct Category  From", typeof(string));
							string text73 = AddColumns(ref filterTable, item.Tag + "MatrixProduct Category  To", typeof(string));
							filterTable.Rows[0][text70] = productParentSelector.FromItem;
							filterTable.Rows[0][text71] = productParentSelector.ToItem;
							filterTable.Rows[0][text72] = productParentSelector.FromCategory;
							filterTable.Rows[0][text73] = productParentSelector.ToCategory;
							if (productParentSelector.FromItem != "")
							{
								text = text + text70 + ":" + productParentSelector.FromItem + " " + text71 + ":" + productParentSelector.ToItem + "; ";
							}
							if (productParentSelector.FromCategory != "")
							{
								text = text + text72 + ":" + productParentSelector.FromCategory + " " + text73 + ":" + productParentSelector.ToCategory + "; ";
							}
							list2.Add(productParentSelector);
						}
						catch (Exception ex21)
						{
							ErrorHelper.ErrorMessage(ex21.Message);
						}
					}
					else if (item is BuyerSelector && !list2.Contains(item))
					{
						try
						{
							BuyerSelector buyerSelector = (BuyerSelector)item;
							string text74 = AddColumns(ref filterTable, item.Tag + "Buyer From", typeof(string));
							string text75 = AddColumns(ref filterTable, item.Tag + "Buyer To", typeof(string));
							filterTable.Rows[0][text74] = buyerSelector.FromBuyer;
							filterTable.Rows[0][text75] = buyerSelector.ToBuyer;
							if (buyerSelector.FromBuyer != "")
							{
								text = text + text74 + ":" + buyerSelector.FromBuyer + " " + text75 + ":" + buyerSelector.ToBuyer + "; ";
							}
							list2.Add(buyerSelector);
						}
						catch (Exception ex22)
						{
							ErrorHelper.ErrorMessage(ex22.Message);
						}
					}
					else if (item is LocationSelector && !list2.Contains(item))
					{
						try
						{
							LocationSelector locationSelector2 = (LocationSelector)item;
							string text76 = AddColumns(ref filterTable, item.Tag + "Location From", typeof(string));
							string text77 = AddColumns(ref filterTable, item.Tag + "Location  To", typeof(string));
							filterTable.Rows[0][text76] = locationSelector2.FromLocation;
							filterTable.Rows[0][text77] = locationSelector2.ToLocation;
							if (locationSelector2.FromLocation != "")
							{
								text = text + text76 + ":" + locationSelector2.FromLocation + " " + text77 + ":" + locationSelector2.ToLocation + "; ";
							}
							list2.Add(locationSelector2);
						}
						catch (Exception ex23)
						{
							ErrorHelper.ErrorMessage(ex23.Message);
						}
					}
					else if (item is ItemBrandSelector && !list2.Contains(item))
					{
						try
						{
							ItemBrandSelector itemBrandSelector = (ItemBrandSelector)item;
							string text78 = AddColumns(ref filterTable, item.Tag + "Product Brand From", typeof(string));
							string text79 = AddColumns(ref filterTable, item.Tag + "Product Brand  To", typeof(string));
							filterTable.Rows[0][text78] = itemBrandSelector.FromProductBrand;
							filterTable.Rows[0][text79] = itemBrandSelector.ToProductBrand;
							if (itemBrandSelector.FromProductBrand != "")
							{
								text = text + text78 + ":" + itemBrandSelector.FromProductBrand + " " + text79 + ":" + itemBrandSelector.ToProductBrand + "; ";
							}
							list2.Add(itemBrandSelector);
						}
						catch (Exception ex24)
						{
							ErrorHelper.ErrorMessage(ex24.Message);
						}
					}
					else if (item is CostCategorySelector && !list2.Contains(item))
					{
						try
						{
							CostCategorySelector costCategorySelector = (CostCategorySelector)item;
							string text80 = AddColumns(ref filterTable, item.Tag + "CostCategory From", typeof(string));
							string text81 = AddColumns(ref filterTable, item.Tag + "CostCategory To", typeof(string));
							filterTable.Rows[0][text80] = costCategorySelector.FromCostCategory;
							filterTable.Rows[0][text81] = costCategorySelector.ToCostCategory;
							if (costCategorySelector.FromCostCategory != "")
							{
								text = text + text80 + ":" + costCategorySelector.FromCostCategory + " " + text81 + ":" + costCategorySelector.ToCostCategory + "; ";
							}
							list2.Add(costCategorySelector);
						}
						catch (Exception ex25)
						{
							ErrorHelper.ErrorMessage(ex25.Message);
						}
					}
					else if (item is SalespersonSelector && !list2.Contains(item))
					{
						try
						{
							SalespersonSelector salespersonSelector = (SalespersonSelector)item;
							string text82 = AddColumns(ref filterTable, item.Tag + "Salesperson From", typeof(string));
							string columnName5 = AddColumns(ref filterTable, item.Tag + "Salesperson From Name", typeof(string));
							string text83 = AddColumns(ref filterTable, item.Tag + "Salesperson To", typeof(string));
							string columnName6 = AddColumns(ref filterTable, item.Tag + "Salesperson To Name", typeof(string));
							string text84 = AddColumns(ref filterTable, item.Tag + "SalespersonDivision From", typeof(string));
							string columnName7 = AddColumns(ref filterTable, item.Tag + "SalespersonDivision From Name", typeof(string));
							string text85 = AddColumns(ref filterTable, item.Tag + "SalespersonDivision To", typeof(string));
							string columnName8 = AddColumns(ref filterTable, item.Tag + "SalespersonDivision To Name", typeof(string));
							string text86 = AddColumns(ref filterTable, item.Tag + "SalespersonGroup From", typeof(string));
							string columnName9 = AddColumns(ref filterTable, item.Tag + "SalespersonGroup From Name", typeof(string));
							string text87 = AddColumns(ref filterTable, item.Tag + "SalespersonGroup To", typeof(string));
							string columnName10 = AddColumns(ref filterTable, item.Tag + "SalespersonGroup To Name", typeof(string));
							string text88 = AddColumns(ref filterTable, item.Tag + "SalespersonArea From", typeof(string));
							string columnName11 = AddColumns(ref filterTable, item.Tag + "SalespersonArea From Name", typeof(string));
							string text89 = AddColumns(ref filterTable, item.Tag + "SalespersonArea To", typeof(string));
							string columnName12 = AddColumns(ref filterTable, item.Tag + "SalespersonArea To Name", typeof(string));
							string text90 = AddColumns(ref filterTable, item.Tag + "SalespersonCountry From", typeof(string));
							string columnName13 = AddColumns(ref filterTable, item.Tag + "SalespersonCountry From Name", typeof(string));
							string text91 = AddColumns(ref filterTable, item.Tag + "SalespersonCountry To", typeof(string));
							string columnName14 = AddColumns(ref filterTable, item.Tag + "SalespersonCountry To Name", typeof(string));
							filterTable.Rows[0][text82] = salespersonSelector.FromSalesperson;
							filterTable.Rows[0][columnName5] = salespersonSelector.FromSalespersonName;
							filterTable.Rows[0][text83] = salespersonSelector.ToSalesperson;
							filterTable.Rows[0][columnName6] = salespersonSelector.ToSalespersonName;
							filterTable.Rows[0][text84] = salespersonSelector.FromDivision;
							filterTable.Rows[0][columnName7] = salespersonSelector.FromDivisionName;
							filterTable.Rows[0][text85] = salespersonSelector.ToDivision;
							filterTable.Rows[0][columnName8] = salespersonSelector.ToDivisionName;
							filterTable.Rows[0][text86] = salespersonSelector.FromGroup;
							filterTable.Rows[0][columnName9] = salespersonSelector.FromGroupName;
							filterTable.Rows[0][text87] = salespersonSelector.ToGroup;
							filterTable.Rows[0][columnName10] = salespersonSelector.ToGroupName;
							filterTable.Rows[0][text88] = salespersonSelector.FromArea;
							filterTable.Rows[0][columnName11] = salespersonSelector.FromAreaName;
							filterTable.Rows[0][text89] = salespersonSelector.ToArea;
							filterTable.Rows[0][columnName12] = salespersonSelector.ToAreaName;
							filterTable.Rows[0][text90] = salespersonSelector.FromArea;
							filterTable.Rows[0][columnName13] = salespersonSelector.FromAreaName;
							filterTable.Rows[0][text91] = salespersonSelector.ToArea;
							filterTable.Rows[0][columnName14] = salespersonSelector.ToAreaName;
							if (salespersonSelector.FromSalesperson != "")
							{
								text = text + text82 + ":" + salespersonSelector.FromSalespersonName + " " + text83 + ":" + salespersonSelector.ToSalespersonName + "; ";
							}
							if (salespersonSelector.FromDivision != "")
							{
								text = text + text84 + ":" + salespersonSelector.FromDivisionName + " " + text85 + ":" + salespersonSelector.ToDivisionName + "; ";
							}
							if (salespersonSelector.FromGroup != "")
							{
								text = text + text86 + ":" + salespersonSelector.FromGroupName + " " + text87 + ":" + salespersonSelector.ToGroupName + "; ";
							}
							if (salespersonSelector.FromArea != "")
							{
								text = text + text88 + ":" + salespersonSelector.FromAreaName + " " + text89 + ":" + salespersonSelector.ToAreaName + "; ";
							}
							if (salespersonSelector.FromCountry != "")
							{
								text = text + text90 + ":" + salespersonSelector.FromCountryName + " " + text91 + ":" + salespersonSelector.ToCountryName + "; ";
							}
							list2.Add(salespersonSelector);
						}
						catch (Exception ex26)
						{
							ErrorHelper.ErrorMessage(ex26.Message);
						}
					}
					else if (item is PropertySelector && !list2.Contains(item))
					{
						try
						{
							PropertySelector propertySelector = (PropertySelector)item;
							string text92 = AddColumns(ref filterTable, item.Tag + "Property From", typeof(string));
							string text93 = AddColumns(ref filterTable, item.Tag + "Property From Name", typeof(string));
							string text94 = AddColumns(ref filterTable, item.Tag + "Property To", typeof(string));
							string text95 = AddColumns(ref filterTable, item.Tag + "Property To Name", typeof(string));
							string text96 = AddColumns(ref filterTable, item.Tag + "Property Class From", typeof(string));
							string text97 = AddColumns(ref filterTable, item.Tag + "Property Class From Name", typeof(string));
							string text98 = AddColumns(ref filterTable, item.Tag + "Property Class To", typeof(string));
							string text99 = AddColumns(ref filterTable, item.Tag + "Property Class To Name", typeof(string));
							filterTable.Rows[0][text92] = propertySelector.FromProperty;
							filterTable.Rows[0][text94] = propertySelector.ToProperty;
							filterTable.Rows[0][text96] = propertySelector.FromClass;
							filterTable.Rows[0][text98] = propertySelector.ToClass;
							filterTable.Rows[0][text93] = propertySelector.FromPropertyName;
							filterTable.Rows[0][text95] = propertySelector.ToPropertyName;
							filterTable.Rows[0][text97] = propertySelector.FromClassName;
							filterTable.Rows[0][text99] = propertySelector.ToClassName;
							if (propertySelector.FromProperty != "")
							{
								text = text + text92 + ":" + propertySelector.FromProperty + " " + text94 + ":" + propertySelector.ToProperty + " " + text93 + ":" + propertySelector.FromPropertyName + " " + text95 + ":" + propertySelector.ToPropertyName + ";";
							}
							if (propertySelector.FromClass != "")
							{
								text = text + text96 + ":" + propertySelector.FromClass + " " + text98 + ":" + propertySelector.ToClass + " " + text97 + ":" + propertySelector.FromClassName + " " + text99 + ":" + propertySelector.ToClassName + "; ";
							}
							list2.Add(propertySelector);
						}
						catch (Exception ex27)
						{
							ErrorHelper.ErrorMessage(ex27.Message);
						}
					}
					else if (item is PropertyUnitSelector && !list2.Contains(item))
					{
						try
						{
							PropertyUnitSelector propertyUnitSelector = (PropertyUnitSelector)item;
							string text100 = AddColumns(ref filterTable, item.Tag + "PropertyUnit From", typeof(string));
							string text101 = AddColumns(ref filterTable, item.Tag + "PropertyUnit To", typeof(string));
							filterTable.Rows[0][text100] = propertyUnitSelector.FromUnitName;
							filterTable.Rows[0][text101] = propertyUnitSelector.ToUnitName;
							if (propertyUnitSelector.FromUnit != "")
							{
								text = text + text100 + ":" + propertyUnitSelector.FromUnitName + " " + text101 + ":" + propertyUnitSelector.ToUnitName + "; ";
							}
							list2.Add(propertyUnitSelector);
						}
						catch (Exception ex28)
						{
							ErrorHelper.ErrorMessage(ex28.Message);
						}
					}
					else if (item is ProductSelector && !list2.Contains(item))
					{
						try
						{
							ProductSelector productSelector = (ProductSelector)item;
							string text102 = AddColumns(ref filterTable, item.Tag + "Product From", typeof(string));
							string columnName15 = AddColumns(ref filterTable, item.Tag + "Product From Name", typeof(string));
							string text103 = AddColumns(ref filterTable, item.Tag + "Product To", typeof(string));
							string columnName16 = AddColumns(ref filterTable, item.Tag + "Product To Name", typeof(string));
							string text104 = AddColumns(ref filterTable, item.Tag + "ItemClass From", typeof(string));
							string columnName17 = AddColumns(ref filterTable, item.Tag + "ItemClass From Name", typeof(string));
							string text105 = AddColumns(ref filterTable, item.Tag + "ItemClass To", typeof(string));
							string columnName18 = AddColumns(ref filterTable, item.Tag + "ItemClass To Name", typeof(string));
							string text106 = AddColumns(ref filterTable, item.Tag + "ItemCategory From", typeof(string));
							string columnName19 = AddColumns(ref filterTable, item.Tag + "ItemCategory From Name", typeof(string));
							string text107 = AddColumns(ref filterTable, item.Tag + "ItemCategory To", typeof(string));
							string columnName20 = AddColumns(ref filterTable, item.Tag + "ItemCategory To Name", typeof(string));
							string text108 = AddColumns(ref filterTable, item.Tag + "Brand From", typeof(string));
							string columnName21 = AddColumns(ref filterTable, item.Tag + "Brand From Name", typeof(string));
							string text109 = AddColumns(ref filterTable, item.Tag + "Brand To", typeof(string));
							string columnName22 = AddColumns(ref filterTable, item.Tag + "Brand To Name", typeof(string));
							string text110 = AddColumns(ref filterTable, item.Tag + "Manufacturer From", typeof(string));
							string columnName23 = AddColumns(ref filterTable, item.Tag + "Manufacturer From Name", typeof(string));
							string text111 = AddColumns(ref filterTable, item.Tag + "Manufacturer To", typeof(string));
							string columnName24 = AddColumns(ref filterTable, item.Tag + "Manufacturer To Name", typeof(string));
							string text112 = AddColumns(ref filterTable, item.Tag + "Origin From", typeof(string));
							string columnName25 = AddColumns(ref filterTable, item.Tag + "Origin From Name", typeof(string));
							string text113 = AddColumns(ref filterTable, item.Tag + "Origin To", typeof(string));
							string columnName26 = AddColumns(ref filterTable, item.Tag + "Origin To Name", typeof(string));
							string text114 = AddColumns(ref filterTable, item.Tag + "Style From", typeof(string));
							string columnName27 = AddColumns(ref filterTable, item.Tag + "Style From Name", typeof(string));
							string text115 = AddColumns(ref filterTable, item.Tag + "Style To", typeof(string));
							string columnName28 = AddColumns(ref filterTable, item.Tag + "Style To Name", typeof(string));
							filterTable.Rows[0][text102] = productSelector.FromItem;
							filterTable.Rows[0][text103] = productSelector.ToItem;
							filterTable.Rows[0][columnName15] = productSelector.FromItemName;
							filterTable.Rows[0][columnName16] = productSelector.ToItemName;
							filterTable.Rows[0][text104] = productSelector.FromClass;
							filterTable.Rows[0][text105] = productSelector.ToClass;
							filterTable.Rows[0][columnName17] = productSelector.FromClassName;
							filterTable.Rows[0][columnName18] = productSelector.ToClassName;
							filterTable.Rows[0][text106] = productSelector.FromCategory;
							filterTable.Rows[0][text107] = productSelector.ToCategory;
							filterTable.Rows[0][columnName19] = productSelector.FromCategoryName;
							filterTable.Rows[0][columnName20] = productSelector.ToCategoryName;
							filterTable.Rows[0][text108] = productSelector.FromBrand;
							filterTable.Rows[0][text109] = productSelector.ToBrand;
							filterTable.Rows[0][columnName21] = productSelector.FromBrandName;
							filterTable.Rows[0][columnName22] = productSelector.ToBrandName;
							filterTable.Rows[0][text110] = productSelector.FromManufacturer;
							filterTable.Rows[0][text111] = productSelector.ToManufacturer;
							filterTable.Rows[0][columnName23] = productSelector.FromManufacturerName;
							filterTable.Rows[0][columnName24] = productSelector.ToManufacturerName;
							filterTable.Rows[0][text112] = productSelector.FromOrigin;
							filterTable.Rows[0][text113] = productSelector.ToOrigin;
							filterTable.Rows[0][columnName25] = productSelector.FromOriginName;
							filterTable.Rows[0][columnName26] = productSelector.ToOriginName;
							filterTable.Rows[0][text114] = productSelector.FromStyle;
							filterTable.Rows[0][text115] = productSelector.ToStyle;
							filterTable.Rows[0][columnName27] = productSelector.FromStyleName;
							filterTable.Rows[0][columnName28] = productSelector.ToStyleName;
							if (productSelector.FromItem != "")
							{
								text = text + text102 + ":" + productSelector.FromItemName + " " + text103 + ":" + productSelector.ToItemName + "; ";
							}
							if (productSelector.FromClass != "")
							{
								text = text + text104 + ":" + productSelector.FromClassName + " " + text105 + ":" + productSelector.ToClassName + "; ";
							}
							if (productSelector.FromCategory != "")
							{
								text = text + text106 + ":" + productSelector.FromCategoryName + " " + text107 + ":" + productSelector.ToCategoryName + "; ";
							}
							if (productSelector.FromBrand != "")
							{
								text = text + text108 + ":" + productSelector.FromBrandName + " " + text109 + ":" + productSelector.ToBrandName + "; ";
							}
							if (productSelector.FromManufacturer != "")
							{
								text = text + text110 + ":" + productSelector.FromManufacturerName + " " + text111 + ":" + productSelector.ToManufacturerName + "; ";
							}
							if (productSelector.FromOrigin != "")
							{
								text = text + text112 + ":" + productSelector.FromOriginName + " " + text113 + ":" + productSelector.ToOriginName + "; ";
							}
							if (productSelector.FromStyle != "")
							{
								text = text + text114 + ":" + productSelector.FromStyleName + " " + text115 + ":" + productSelector.ToStyleName + "; ";
							}
							list2.Add(productSelector);
						}
						catch (Exception ex29)
						{
							ErrorHelper.ErrorMessage(ex29.Message);
						}
					}
					else if (item is CustomerSelector && !list2.Contains(item))
					{
						try
						{
							CustomerSelector customerSelector = (CustomerSelector)item;
							string text116 = AddColumns(ref filterTable, item.Tag + "customer From", typeof(string));
							string text117 = AddColumns(ref filterTable, item.Tag + "customer From Name", typeof(string));
							string text118 = AddColumns(ref filterTable, item.Tag + "customer To", typeof(string));
							string text119 = AddColumns(ref filterTable, item.Tag + "customer To Name", typeof(string));
							string text120 = AddColumns(ref filterTable, item.Tag + "customerClass From", typeof(string));
							string text121 = AddColumns(ref filterTable, item.Tag + "customerClass From Name", typeof(string));
							string text122 = AddColumns(ref filterTable, item.Tag + "customerClass To", typeof(string));
							string text123 = AddColumns(ref filterTable, item.Tag + "customerClass To Name", typeof(string));
							string text124 = AddColumns(ref filterTable, item.Tag + "customerGroup From", typeof(string));
							string text125 = AddColumns(ref filterTable, item.Tag + "customerGroup From Name", typeof(string));
							string text126 = AddColumns(ref filterTable, item.Tag + "customerGroup To", typeof(string));
							string text127 = AddColumns(ref filterTable, item.Tag + "customerGroup To Name", typeof(string));
							string text128 = AddColumns(ref filterTable, item.Tag + "customerArea From", typeof(string));
							string text129 = AddColumns(ref filterTable, item.Tag + "customerArea From Name", typeof(string));
							string text130 = AddColumns(ref filterTable, item.Tag + "customerArea To", typeof(string));
							string text131 = AddColumns(ref filterTable, item.Tag + "customerArea To Name", typeof(string));
							string text132 = AddColumns(ref filterTable, item.Tag + "customerCountry From", typeof(string));
							string text133 = AddColumns(ref filterTable, item.Tag + "customerCountry From Name", typeof(string));
							string text134 = AddColumns(ref filterTable, item.Tag + "customerCountry To", typeof(string));
							string text135 = AddColumns(ref filterTable, item.Tag + "customerCountry To Name", typeof(string));
							filterTable.Rows[0][text116] = customerSelector.FromCustomer;
							filterTable.Rows[0][text118] = customerSelector.ToCustomer;
							filterTable.Rows[0][text117] = customerSelector.FromCustomerName;
							filterTable.Rows[0][text119] = customerSelector.ToCustomerName;
							filterTable.Rows[0][text120] = customerSelector.FromClass;
							filterTable.Rows[0][text122] = customerSelector.ToClass;
							filterTable.Rows[0][text121] = customerSelector.FromClassName;
							filterTable.Rows[0][text123] = customerSelector.ToClassName;
							filterTable.Rows[0][text124] = customerSelector.FromGroup;
							filterTable.Rows[0][text126] = customerSelector.ToGroup;
							filterTable.Rows[0][text125] = customerSelector.FromGroupName;
							filterTable.Rows[0][text127] = customerSelector.ToGroupName;
							filterTable.Rows[0][text128] = customerSelector.FromArea;
							filterTable.Rows[0][text130] = customerSelector.ToArea;
							filterTable.Rows[0][text129] = customerSelector.FromAreaName;
							filterTable.Rows[0][text131] = customerSelector.ToAreaName;
							filterTable.Rows[0][text132] = customerSelector.FromCountry;
							filterTable.Rows[0][text134] = customerSelector.ToCountry;
							filterTable.Rows[0][text133] = customerSelector.FromCountryName;
							filterTable.Rows[0][text135] = customerSelector.ToCountryName;
							if (customerSelector.FromCustomer != "")
							{
								text = text + text116 + ":" + customerSelector.FromCustomer + " " + text118 + ":" + customerSelector.ToCustomer + " " + text117 + ":" + customerSelector.FromCustomerName + " " + text119 + ":" + customerSelector.ToCustomerName + "; ";
							}
							if (customerSelector.FromClass != "")
							{
								text = text + text120 + ":" + customerSelector.FromClass + " " + text122 + ":" + customerSelector.ToClass + " " + text121 + ":" + customerSelector.FromClassName + " " + text123 + ":" + customerSelector.ToClassName + "; ";
							}
							if (customerSelector.FromGroup != "")
							{
								text = text + text124 + ":" + customerSelector.FromGroup + " " + text126 + ":" + customerSelector.ToGroup + " " + text125 + ":" + customerSelector.FromGroupName + " " + text127 + ":" + customerSelector.ToGroupName + "; ";
							}
							if (customerSelector.FromArea != "")
							{
								text = text + text128 + ":" + customerSelector.FromArea + " " + text130 + ":" + customerSelector.ToArea + " " + text129 + ":" + customerSelector.FromAreaName + " " + text131 + ":" + customerSelector.ToAreaName + "; ";
							}
							if (customerSelector.FromCountry != "")
							{
								text = text + text132 + ":" + customerSelector.FromCountry + " " + text134 + ":" + customerSelector.ToCountry + " " + text133 + ":" + customerSelector.FromCountryName + " " + text135 + ":" + customerSelector.ToCountryName + "; ";
							}
							list2.Add(customerSelector);
						}
						catch (Exception ex30)
						{
							ErrorHelper.ErrorMessage(ex30.Message);
						}
					}
					else if (item is EmployeeSelector && !list2.Contains(item))
					{
						try
						{
							EmployeeSelector employeeSelector = (EmployeeSelector)item;
							string text136 = AddColumns(ref filterTable, item.Tag + "Employee From", typeof(string));
							string text137 = AddColumns(ref filterTable, item.Tag + "Employee To", typeof(string));
							string text138 = AddColumns(ref filterTable, item.Tag + "EmployeeDepartment From", typeof(string));
							string text139 = AddColumns(ref filterTable, item.Tag + "EmployeeDepartment To", typeof(string));
							string text140 = AddColumns(ref filterTable, item.Tag + "EmployeeLocation From", typeof(string));
							string text141 = AddColumns(ref filterTable, item.Tag + "EmployeeLocation To", typeof(string));
							filterTable.Rows[0][text136] = employeeSelector.FromEmployee;
							filterTable.Rows[0][text137] = employeeSelector.ToEmployee;
							filterTable.Rows[0][text138] = employeeSelector.FromDepartment;
							filterTable.Rows[0][text139] = employeeSelector.ToDepartment;
							filterTable.Rows[0][text140] = employeeSelector.FromLocation;
							filterTable.Rows[0][text141] = employeeSelector.ToLocation;
							if (employeeSelector.FromEmployee != "")
							{
								text = text + text136 + ":" + employeeSelector.FromEmployee + " " + text137 + ":" + employeeSelector.ToEmployee + "; ";
							}
							if (employeeSelector.FromDepartment != "")
							{
								text = text + text138 + ":" + employeeSelector.FromDepartment + " " + text139 + ":" + employeeSelector.ToDepartment + "; ";
							}
							if (employeeSelector.FromLocation != "")
							{
								text = text + text140 + ":" + employeeSelector.FromLocation + " " + text141 + ":" + employeeSelector.ToLocation + "; ";
							}
							list2.Add(employeeSelector);
						}
						catch (Exception ex31)
						{
							ErrorHelper.ErrorMessage(ex31.Message);
						}
					}
					else if (item is EmployeeSelector2 && !list2.Contains(item))
					{
						try
						{
							EmployeeSelector2 employeeSelector2 = (EmployeeSelector2)item;
							string text142 = AddColumns(ref filterTable, item.Tag + "Employee From", typeof(string));
							string text143 = AddColumns(ref filterTable, item.Tag + "Employee To", typeof(string));
							string text144 = AddColumns(ref filterTable, item.Tag + "EmployeeDepartment From", typeof(string));
							string text145 = AddColumns(ref filterTable, item.Tag + "EmployeeDepartment To", typeof(string));
							string text146 = AddColumns(ref filterTable, item.Tag + "EmployeeLocation From", typeof(string));
							string text147 = AddColumns(ref filterTable, item.Tag + "EmployeeLocation To", typeof(string));
							string text148 = AddColumns(ref filterTable, item.Tag + "EmployeeType From", typeof(string));
							string text149 = AddColumns(ref filterTable, item.Tag + "EmployeeType To", typeof(string));
							string text150 = AddColumns(ref filterTable, item.Tag + "EmployeeDivision From", typeof(string));
							string text151 = AddColumns(ref filterTable, item.Tag + "EmployeeDivision To", typeof(string));
							string text152 = AddColumns(ref filterTable, item.Tag + "EmployeeSponsor From", typeof(string));
							string text153 = AddColumns(ref filterTable, item.Tag + "EmployeeSponsor To", typeof(string));
							string text154 = AddColumns(ref filterTable, item.Tag + "EmployeeGroup From", typeof(string));
							string text155 = AddColumns(ref filterTable, item.Tag + "EmployeeGroup To", typeof(string));
							string text156 = AddColumns(ref filterTable, item.Tag + "EmployeeGrade From", typeof(string));
							string text157 = AddColumns(ref filterTable, item.Tag + "EmployeeGrade To", typeof(string));
							string text158 = AddColumns(ref filterTable, item.Tag + "EmployeePosition From", typeof(string));
							string columnName29 = AddColumns(ref filterTable, item.Tag + "EmployeePosition To", typeof(string));
							string text159 = AddColumns(ref filterTable, item.Tag + "EmployeeBank From", typeof(string));
							string text160 = AddColumns(ref filterTable, item.Tag + "EmployeeBank To", typeof(string));
							string text161 = AddColumns(ref filterTable, item.Tag + "EmployeeAccount From", typeof(string));
							string text162 = AddColumns(ref filterTable, item.Tag + "EmployeeAccount To", typeof(string));
							filterTable.Rows[0][text142] = employeeSelector2.FromEmployee;
							filterTable.Rows[0][text143] = employeeSelector2.ToEmployee;
							filterTable.Rows[0][text144] = employeeSelector2.FromDepartment;
							filterTable.Rows[0][text145] = employeeSelector2.ToDepartment;
							filterTable.Rows[0][text146] = employeeSelector2.FromLocation;
							filterTable.Rows[0][text147] = employeeSelector2.ToLocation;
							filterTable.Rows[0][text148] = employeeSelector2.FromType;
							filterTable.Rows[0][text149] = employeeSelector2.ToType;
							filterTable.Rows[0][text150] = employeeSelector2.FromDivision;
							filterTable.Rows[0][text151] = employeeSelector2.ToDivision;
							filterTable.Rows[0][text152] = employeeSelector2.FromSponsor;
							filterTable.Rows[0][text153] = employeeSelector2.ToSponsor;
							filterTable.Rows[0][text154] = employeeSelector2.FromGroup;
							filterTable.Rows[0][text155] = employeeSelector2.ToGroup;
							filterTable.Rows[0][text156] = employeeSelector2.FromGrade;
							filterTable.Rows[0][text157] = employeeSelector2.ToGrade;
							filterTable.Rows[0][text158] = employeeSelector2.FromPosition;
							filterTable.Rows[0][columnName29] = employeeSelector2.ToPosition;
							filterTable.Rows[0][text159] = employeeSelector2.FromBank;
							filterTable.Rows[0][text160] = employeeSelector2.ToBank;
							filterTable.Rows[0][text161] = employeeSelector2.FromBank;
							filterTable.Rows[0][text162] = employeeSelector2.ToBank;
							if (employeeSelector2.FromEmployee != "")
							{
								text = text + text142 + ":" + employeeSelector2.FromEmployee + " " + text143 + ":" + employeeSelector2.ToEmployee + "; ";
							}
							if (employeeSelector2.FromDepartment != "")
							{
								text = text + text144 + ":" + employeeSelector2.FromDepartment + " " + text145 + ":" + employeeSelector2.ToDepartment + "; ";
							}
							if (employeeSelector2.FromLocation != "")
							{
								text = text + text146 + ":" + employeeSelector2.FromLocation + " " + text147 + ":" + employeeSelector2.ToLocation + "; ";
							}
							if (employeeSelector2.FromType != "")
							{
								text = text + text148 + ":" + employeeSelector2.FromType + " " + text149 + ":" + employeeSelector2.ToType + "; ";
							}
							if (employeeSelector2.FromDivision != "")
							{
								text = text + text150 + ":" + employeeSelector2.FromDivision + " " + text151 + ":" + employeeSelector2.ToDivision + "; ";
							}
							if (employeeSelector2.FromSponsor != "")
							{
								text = text + text152 + ":" + employeeSelector2.FromSponsor + " " + text153 + ":" + employeeSelector2.ToSponsor + "; ";
							}
							if (employeeSelector2.FromGroup != "")
							{
								text = text + text154 + ":" + employeeSelector2.FromGroup + " " + text155 + ":" + employeeSelector2.ToGroup + "; ";
							}
							if (employeeSelector2.FromGrade != "")
							{
								text = text + text156 + ":" + employeeSelector2.FromGrade + " " + text157 + ":" + employeeSelector2.ToGrade + "; ";
							}
							if (employeeSelector2.FromPosition != "")
							{
								text = text + text158 + ":" + employeeSelector2.FromPosition + " " + text158 + ":" + employeeSelector2.ToPosition + "; ";
							}
							if (employeeSelector2.FromBank != "")
							{
								text = text + text159 + ":" + employeeSelector2.FromBank + " " + text160 + ":" + employeeSelector2.ToBank + "; ";
							}
							if (employeeSelector2.FromBank != "")
							{
								text = text + text161 + ":" + employeeSelector2.FromAccount + " " + text162 + ":" + employeeSelector2.ToAccount + "; ";
							}
							list2.Add(employeeSelector2);
						}
						catch (Exception ex32)
						{
							ErrorHelper.ErrorMessage(ex32.Message);
						}
					}
					else if (item is VendorSelector && !list2.Contains(item))
					{
						try
						{
							VendorSelector vendorSelector = (VendorSelector)item;
							string text163 = AddColumns(ref filterTable, item.Tag + "Vendor From", typeof(string));
							string text164 = AddColumns(ref filterTable, item.Tag + "Vendor To", typeof(string));
							string text165 = AddColumns(ref filterTable, item.Tag + "VendorClass From", typeof(string));
							string text166 = AddColumns(ref filterTable, item.Tag + "VendorClass To", typeof(string));
							string text167 = AddColumns(ref filterTable, item.Tag + "VendorGroup From", typeof(string));
							string text168 = AddColumns(ref filterTable, item.Tag + "VendorGroup To", typeof(string));
							AddColumns(ref filterTable, item.Tag + "VendorArea From", typeof(string));
							AddColumns(ref filterTable, item.Tag + "VendorArea To", typeof(string));
							AddColumns(ref filterTable, item.Tag + "VendorCountry From", typeof(string));
							AddColumns(ref filterTable, item.Tag + "VendorCountry To", typeof(string));
							filterTable.Rows[0][text163] = vendorSelector.FromVendor;
							filterTable.Rows[0][text164] = vendorSelector.ToVendor;
							filterTable.Rows[0][text165] = vendorSelector.FromClass;
							filterTable.Rows[0][text166] = vendorSelector.ToClass;
							filterTable.Rows[0][text167] = vendorSelector.FromGroup;
							filterTable.Rows[0][text168] = vendorSelector.ToGroup;
							if (vendorSelector.FromVendor != "")
							{
								text = text + text163 + ":" + vendorSelector.FromVendor + " " + text164 + ":" + vendorSelector.ToVendor + "; ";
							}
							if (vendorSelector.FromClass != "")
							{
								text = text + text165 + ":" + vendorSelector.FromClass + " " + text166 + ":" + vendorSelector.ToClass + "; ";
							}
							if (vendorSelector.FromGroup != "")
							{
								text = text + text167 + ":" + vendorSelector.FromGroup + " " + text168 + ":" + vendorSelector.ToGroup + "; ";
							}
							list2.Add(vendorSelector);
						}
						catch (Exception ex33)
						{
							ErrorHelper.ErrorMessage(ex33.Message);
						}
					}
					else if (item is VendorGroupSelector && !list2.Contains(item))
					{
						try
						{
							VendorGroupSelector vendorGroupSelector = (VendorGroupSelector)item;
							string text169 = AddColumns(ref filterTable, item.Tag + "Vendor  Group From", typeof(string));
							string text170 = AddColumns(ref filterTable, item.Tag + "Vendor Group  To", typeof(string));
							filterTable.Rows[0][text169] = vendorGroupSelector.FromVendorGroup;
							filterTable.Rows[0][text170] = vendorGroupSelector.ToVendorGroup;
							if (vendorGroupSelector.FromVendorGroup != "")
							{
								text = text + text169 + ":" + vendorGroupSelector.FromVendorGroup + " " + text170 + ":" + vendorGroupSelector.ToVendorGroup + "; ";
							}
							list2.Add(vendorGroupSelector);
						}
						catch (Exception ex34)
						{
							ErrorHelper.ErrorMessage(ex34.Message);
						}
					}
					else if (item is LeaveSelector && !list2.Contains(item))
					{
						try
						{
							LeaveSelector leaveSelector = (LeaveSelector)item;
							string text171 = AddColumns(ref filterTable, item.Tag + "Leave From", typeof(string));
							string text172 = AddColumns(ref filterTable, item.Tag + "Leave To", typeof(string));
							filterTable.Rows[0][text171] = leaveSelector.FromLeave;
							filterTable.Rows[0][text172] = leaveSelector.ToLeave;
							if (leaveSelector.FromLeave != "")
							{
								text = text + text171 + ":" + leaveSelector.FromLeave + " " + text172 + ":" + leaveSelector.ToLeave + "; ";
							}
							list2.Add(leaveSelector);
						}
						catch (Exception ex35)
						{
							ErrorHelper.ErrorMessage(ex35.Message);
						}
					}
					else if (item is VehicleSelector && !list2.Contains(item))
					{
						try
						{
							VehicleSelector vehicleSelector = (VehicleSelector)item;
							string text173 = AddColumns(ref filterTable, item.Tag + "Vehicle From", typeof(string));
							string text174 = AddColumns(ref filterTable, item.Tag + "Vehicle To", typeof(string));
							filterTable.Rows[0][text173] = vehicleSelector.FromVehicle;
							filterTable.Rows[0][text174] = vehicleSelector.ToVehicle;
							if (vehicleSelector.FromVehicle != "")
							{
								text = text + text173 + ":" + vehicleSelector.FromVehicle + " " + text174 + ":" + vehicleSelector.ToVehicle + "; ";
							}
							list2.Add(vehicleSelector);
						}
						catch (Exception ex36)
						{
							ErrorHelper.ErrorMessage(ex36.Message);
						}
					}
					else if (item is ServiceSelector && !list2.Contains(item))
					{
						try
						{
							ServiceSelector serviceSelector = (ServiceSelector)item;
							string text175 = AddColumns(ref filterTable, item.Tag + "ServiceItem From", typeof(string));
							string text176 = AddColumns(ref filterTable, item.Tag + "ServiceItem To", typeof(string));
							filterTable.Rows[0][text175] = serviceSelector.FromServiceItem;
							filterTable.Rows[0][text176] = serviceSelector.ToServiceItem;
							if (serviceSelector.FromServiceItem != "")
							{
								text = text + text175 + ":" + serviceSelector.FromServiceItem + " " + text176 + ":" + serviceSelector.ToServiceItem + "; ";
							}
							list2.Add(serviceSelector);
						}
						catch (Exception ex37)
						{
							ErrorHelper.ErrorMessage(ex37.Message);
						}
					}
					else if (item is LeadSourceSelector && !list2.Contains(item))
					{
						try
						{
							LeadSourceSelector leadSourceSelector = (LeadSourceSelector)item;
							string text177 = AddColumns(ref filterTable, item.Tag + "LeadSource From", typeof(string));
							string text178 = AddColumns(ref filterTable, item.Tag + "LeadSource To", typeof(string));
							filterTable.Rows[0][text177] = leadSourceSelector.FromSource;
							filterTable.Rows[0][text178] = leadSourceSelector.ToSource;
							if (leadSourceSelector.FromSource != "")
							{
								text = text + text177 + ":" + leadSourceSelector.FromSource + " " + text178 + ":" + leadSourceSelector.ToSource + "; ";
							}
							list2.Add(leadSourceSelector);
						}
						catch (Exception ex38)
						{
							ErrorHelper.ErrorMessage(ex38.Message);
						}
					}
					else if (item is EventSelector && !list2.Contains(item))
					{
						try
						{
							EventSelector eventSelector = (EventSelector)item;
							string text179 = AddColumns(ref filterTable, item.Tag + "EventLead From", typeof(string));
							string text180 = AddColumns(ref filterTable, item.Tag + "EventLead To", typeof(string));
							string text181 = AddColumns(ref filterTable, item.Tag + "Event AssignedTo From", typeof(string));
							string text182 = AddColumns(ref filterTable, item.Tag + "Event AssignedTo To", typeof(string));
							filterTable.Rows[0][text179] = eventSelector.FromLead;
							filterTable.Rows[0][text180] = eventSelector.ToLead;
							filterTable.Rows[0][text181] = eventSelector.FromUser;
							filterTable.Rows[0][text182] = eventSelector.ToUser;
							if (eventSelector.FromLead != "")
							{
								text = text + text179 + ":" + eventSelector.FromLead + " " + text180 + ":" + eventSelector.ToLead + "; ";
							}
							if (eventSelector.FromUser != "")
							{
								text = text + text181 + ":" + eventSelector.FromUser + " " + text182 + ":" + eventSelector.ToUser + "; ";
							}
							list2.Add(eventSelector);
						}
						catch (Exception ex39)
						{
							ErrorHelper.ErrorMessage(ex39.Message);
						}
					}
					else if (item is LeadSelector && !list2.Contains(item))
					{
						try
						{
							LeadSelector leadSelector = (LeadSelector)item;
							string text183 = AddColumns(ref filterTable, item.Tag + "Lead From", typeof(string));
							string text184 = AddColumns(ref filterTable, item.Tag + "Lead To", typeof(string));
							string text185 = AddColumns(ref filterTable, item.Tag + "LeadLocation From", typeof(string));
							string text186 = AddColumns(ref filterTable, item.Tag + "LeadLocation To", typeof(string));
							filterTable.Rows[0][text183] = leadSelector.FromLead;
							filterTable.Rows[0][text184] = leadSelector.ToLead;
							filterTable.Rows[0][text185] = leadSelector.FromLocation;
							filterTable.Rows[0][text186] = leadSelector.ToLocation;
							if (leadSelector.FromLead != "")
							{
								text = text + text183 + ":" + leadSelector.FromLead + " " + text184 + ":" + leadSelector.ToLead + "; ";
							}
							if (leadSelector.FromLocation != "")
							{
								text = text + text185 + ":" + leadSelector.FromLocation + " " + text186 + ":" + leadSelector.ToLocation + "; ";
							}
							list2.Add(leadSelector);
						}
						catch (Exception ex40)
						{
							ErrorHelper.ErrorMessage(ex40.Message);
						}
					}
					if ((item.GetType() == typeof(RadioButton) || item.GetType() == typeof(CheckBox) || item.GetType() == typeof(ComboBox)) && !(item is RadioButton) && !(item.Text == "Base Currency") && !(item.Text == "Foreign Currency") && !(item is CheckBox))
					{
						_ = (item is ComboBox);
					}
				}
				filterTable.Rows[0]["AllFilter"] = text;
				data.Tables.Add(filterTable);
			}
		}

		public void ShowReport(XtraReport report)
		{
			PrintPreviewForm printPreviewForm = new PrintPreviewForm();
			printPreviewForm.Document = report;
			printPreviewForm.Show();
		}

		public void ShowReport(DataSet data, string filter, string reportName)
		{
			AddGeneralReportData(ref data, filter);
			XtraReport report = GetReport(reportName);
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file. Please make sure you have access to reports path and the files are not corrupted.", "'" + reportName + ".repx'");
				return;
			}
			report.DataSource = data;
			ShowReport(report);
		}
	}
}
