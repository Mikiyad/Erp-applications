using Micromind.ClientLibraries;
using Micromind.ClientUI.WindowsForms.Others.HelpSupports;
using Micromind.Common.Data;
using Micromind.DataControls;
using System;

namespace Micromind.ClientUI.Libraries
{
	public sealed class EventHelper
	{
		private static bool isCalled;

		public static event EventHandler FormCleared;

		public static event EventHandler LogoutRequested;

		public static event EventHandler HomeTabChanged;

		public static event EventHandler RefreshApplicationRequested;

		private EventHelper()
		{
		}

		internal static void AddEvents()
		{
			if (!isCalled)
			{
				isCalled = true;
				ErrorHelper.SendErrorMessage += ErrorHelper_SendErrorMessage;
				ErrorHelper.ViewErrorMessage += ErrorHelper_ViewErrorMessage;
				ComboEvents.QuickAddRequested += ComboEvents_QuickAddRequested;
			}
		}

		internal static void OnFormCleared(object sender, EventArgs e)
		{
			if (EventHelper.FormCleared != null)
			{
				EventHelper.FormCleared(sender, e);
			}
		}

		internal static void OnRefreshApplicationRequested(object sender, EventArgs e)
		{
			if (EventHelper.RefreshApplicationRequested != null)
			{
				EventHelper.RefreshApplicationRequested(sender, e);
			}
		}

		internal static void OnHomeTabChanged(object sender, EventArgs args)
		{
			if (EventHelper.HomeTabChanged != null)
			{
				EventHelper.HomeTabChanged(sender, args);
			}
		}

		private static void ComboEvents_QuickAddRequested(object sender, EventArgs e)
		{
			ComboQuickAddData comboQuickAddData = sender as ComboQuickAddData;
			if (comboQuickAddData.ComboType == DataComboType.Customer)
			{
				new FormHelper().EditCustomer(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Accounts)
			{
				new FormHelper().EditAccount(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Analysis)
			{
				new FormHelper().EditAnalysis(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.AnalysisGroup)
			{
				new FormHelper().EditAnalysisGroup(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Area)
			{
				new FormHelper().EditArea(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Contact)
			{
				new FormHelper().EditContact(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Country)
			{
				new FormHelper().EditCountry(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Currency)
			{
				new FormHelper().EditCurrency(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.CustomerClass)
			{
				new FormHelper().EditCustomerClass(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Employee)
			{
				new FormHelper().EditEmployee(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Item)
			{
				new FormHelper().EditItem(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.PaymentMethod)
			{
				new FormHelper().EditPaymentMethod(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.PaymentTerm)
			{
				new FormHelper().EditPaymentTerm(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.PriceLevel)
			{
				new FormHelper().EditPriceLevel(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Salesperson)
			{
				new FormHelper().EditSalesperson(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.ShippingMethod)
			{
				new FormHelper().EditShippingMethod(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Vendor)
			{
				new FormHelper().EditVendor(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.VendorClass)
			{
				new FormHelper().EditVendorClass(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Buyer)
			{
				new FormHelper().EditBuyer(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.ProductBrand)
			{
				new FormHelper().EditProductBrand(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.ProductManufacturer)
			{
				new FormHelper().EditProductManufacturer(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.ProductStyle)
			{
				new FormHelper().EditProductStyle(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.ProductCategory)
			{
				new FormHelper().EditProductCategory(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.ProductClass)
			{
				new FormHelper().EditProductClass(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Unit)
			{
				new FormHelper().EditUOM(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Department)
			{
				new FormHelper().EditDepartment(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Grade)
			{
				new FormHelper().EditGrade(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Sponsor)
			{
				new FormHelper().EditSponsor(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Nationality)
			{
				new FormHelper().EditNationality(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Religion)
			{
				new FormHelper().EditReligion(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Division)
			{
				new FormHelper().EditDivision(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Position)
			{
				new FormHelper().EditPosition(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.EmployeeDocType)
			{
				new FormHelper().EditEmployeeDocType(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Degree)
			{
				new FormHelper().EditDegree(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Skill)
			{
				new FormHelper().EditSkill(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.CustomerGroup)
			{
				new FormHelper().EditCustomerGroup(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.VendorGroup)
			{
				new FormHelper().EditVendorGroup(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.EmployeeGroup)
			{
				new FormHelper().EditEmployeeGroup(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.EmployeeType)
			{
				new FormHelper().EditEmployeeType(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Location)
			{
				new FormHelper().EditLocation(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.LeaveType)
			{
				new FormHelper().EditLeaveType(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.PayrollItem)
			{
				new FormHelper().EditPayrollItem(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Deduction)
			{
				new FormHelper().EditDeduction(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Benefit)
			{
				new FormHelper().EditBenefit(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Bank)
			{
				new FormHelper().EditBank(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Destination)
			{
				new FormHelper().EditDestination(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.CompanyDocType)
			{
				new FormHelper().EditCompanyDocType(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.CostCenter)
			{
				new FormHelper().EditCostCenter(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Register)
			{
				new FormHelper().EditRegister(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Chequebook)
			{
				new FormHelper().EditChequebook(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.ReturnedChequeReason)
			{
				new FormHelper().EditReturnedChequeReason(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.AdjustmentType)
			{
				new FormHelper().EditAdjustmentType(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Product)
			{
				new FormHelper().EditProduct(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Driver)
			{
				new FormHelper().EditDriver(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.CustomerAddress)
			{
				new FormHelper().EditCustomerAddress("", "");
			}
			else if (comboQuickAddData.ComboType == DataComboType.Port)
			{
				new FormHelper().EditPort("");
			}
			else if (comboQuickAddData.ComboType == DataComboType.DisciplineActionType)
			{
				new FormHelper().EditDisciplineActionType("");
			}
			else if (comboQuickAddData.ComboType == DataComboType.EmployeeActivityType)
			{
				new FormHelper().EditEmployeeActivityType("");
			}
			else if (comboQuickAddData.ComboType == DataComboType.EmployeeLoanType)
			{
				new FormHelper().EditEmployeeLoanType("");
			}
			else if (comboQuickAddData.ComboType == DataComboType.User)
			{
				new FormHelper().EditUser("");
			}
			else if (comboQuickAddData.ComboType == DataComboType.UserGroup)
			{
				new FormHelper().EditUserGroup("");
			}
			else if (comboQuickAddData.ComboType == DataComboType.VendorAddress)
			{
				new FormHelper().EditVendorAddress("", "");
			}
			else if (comboQuickAddData.ComboType == DataComboType.EmployeeDocument)
			{
				new FormHelper().EditEmployeeDocument("");
			}
			else if (comboQuickAddData.ComboType == DataComboType.City)
			{
				new FormHelper().EditCity(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Agent)
			{
				new FormHelper().EditAgent(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Qualification)
			{
				new FormHelper().EditQualification(comboQuickAddData.ID);
			}
			else if (comboQuickAddData.ComboType == DataComboType.Language)
			{
				new FormHelper().EditLanguage(comboQuickAddData.ID);
			}
		}

		private static void ErrorHelper_SendErrorMessage(object sender, EventArgs e)
		{
			ErrorEvent errorEvent = e as ErrorEvent;
			try
			{
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private static void ErrorHelper_ViewErrorMessage(object sender, EventArgs e)
		{
			ErrorEvent errorEvent = e as ErrorEvent;
			if (errorEvent != null)
			{
				using (ErrorHelperForm errorHelperForm = new ErrorHelperForm())
				{
					errorHelperForm.ErrorEventData = errorEvent;
					errorHelperForm.IsShowSendButton = errorEvent.isShowSendButton;
					errorHelperForm.ShowDialog();
				}
			}
		}

		public static void OnLogoutRequested(object sender, EventArgs e)
		{
			if (EventHelper.LogoutRequested != null)
			{
				EventHelper.LogoutRequested(sender, e);
			}
		}
	}
}
