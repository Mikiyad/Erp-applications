using DevExpress.XtraLayout;
using DevExpress.XtraReports.UI;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Reports.CustomDashboards;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Common.Libraries;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.Libraries
{
	public class FormHelper
	{
		public void EditEntityDoc()
		{
			FormActivator.BringFormToFront(FormActivator.DocManagementFormObj);
		}

		public void EditCustomerClass(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CustomerClassDetailsFormObj);
			if (id != "")
			{
				FormActivator.CustomerClassDetailsFormObj.LoadData(id);
			}
		}

		public void EditTenantClass(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PropertyTenantClassDetailsFormObj);
			if (id != "")
			{
				FormActivator.PropertyTenantClassDetailsFormObj.LoadData(id);
			}
		}

		public void EditCountry(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CountryDetailsFormObj);
			if (id != "")
			{
				FormActivator.CountryDetailsFormObj.LoadData(id);
			}
		}

		public void EditCustomerCategory(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CustomerCategoryFormObj);
			if (id != "")
			{
				FormActivator.CustomerCategoryFormObj.LoadData(id);
			}
		}

		public void EditContactsCategory(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ContactsCategoryFormObj);
			if (id != "")
			{
				FormActivator.ContactsCategoryFormObj.LoadData(id);
			}
		}

		public void EditLeadCategory(string id)
		{
			FormActivator.BringFormToFront(FormActivator.LeadCategoryFormObj);
			if (id != "")
			{
				FormActivator.LeadCategoryFormObj.LoadData(id);
			}
		}

		public void EditVendorCategory(string id)
		{
			FormActivator.BringFormToFront(FormActivator.VendorCategoryFormObj);
			if (id != "")
			{
				FormActivator.VendorCategoryFormObj.LoadData(id);
			}
		}

		public void EditContact(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ContactDetailsFormObj);
			if (id != "")
			{
				FormActivator.ContactDetailsFormObj.LoadData(id);
			}
		}

		public void EditCustomer(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CustomerDetailsFormObj);
			if (id != "")
			{
				FormActivator.CustomerDetailsFormObj.LoadData(id);
			}
		}

		public void EditServiceProvider(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ServiceProviderFormObj);
			if (id != "")
			{
				FormActivator.ServiceProviderFormObj.LoadData(id);
			}
		}

		public void EditArea(string id)
		{
			FormActivator.BringFormToFront(FormActivator.AreaDetailsFormObj);
			if (id != "")
			{
				FormActivator.AreaDetailsFormObj.LoadData(id);
			}
		}

		public void EditPriceLevel(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PriceLevelDetailsFormObj);
			if (id != "")
			{
				FormActivator.PriceLevelDetailsFormObj.LoadData(id);
			}
		}

		public void EditSalesperson(string id)
		{
			FormActivator.BringFormToFront(FormActivator.SalespersonDetailsFormObj);
			if (id != "")
			{
				FormActivator.SalespersonDetailsFormObj.LoadData(id);
			}
		}

		public void EditPaymentMethod(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PaymentMethodDetailsFormObj);
			if (id != "")
			{
				FormActivator.PaymentMethodDetailsFormObj.LoadData(id);
			}
		}

		public void EditPaymentTerm(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PaymentTermDetailsFormObj);
			if (id != "")
			{
				FormActivator.PaymentTermDetailsFormObj.LoadData(id);
			}
		}

		public void EditShippingMethod(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ShippingMethodDetailsFormObj);
			if (id != "")
			{
				FormActivator.ShippingMethodDetailsFormObj.LoadData(id);
			}
		}

		public void EditAccountGroup(string id)
		{
			FormActivator.BringFormToFront(FormActivator.AccountGroupDetailsFormObj);
			if (id != "")
			{
				FormActivator.AccountGroupDetailsFormObj.LoadData(id);
			}
		}

		public void EditAccount(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CompanyAccountDetailsFormObj);
			if (id != "")
			{
				FormActivator.CompanyAccountDetailsFormObj.LoadData(id);
			}
		}

		public void EditCompanyDivision(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CompanyDivisionDetailsFormObj);
			if (id != "")
			{
				FormActivator.CompanyDivisionDetailsFormObj.LoadData(id);
			}
		}

		public void EditAnalysis(string id)
		{
			FormActivator.BringFormToFront(FormActivator.AnalysisDetailsFormObj);
			if (id != "")
			{
				FormActivator.AnalysisDetailsFormObj.LoadData(id);
			}
		}

		public void EditAnalysisGroup(string id)
		{
			FormActivator.BringFormToFront(FormActivator.AnalysisGroupDetailsFormObj);
			if (id != "")
			{
				FormActivator.AnalysisGroupDetailsFormObj.LoadData(id);
			}
		}

		public void EditEmployee(string id)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeDetailsFormObj);
			if (id != "")
			{
				FormActivator.EmployeeDetailsFormObj.LoadData(id);
			}
		}

		public void EditJob(string id)
		{
			FormActivator.BringFormToFront(FormActivator.JobDetailsFormObj);
			if (id != "")
			{
				FormActivator.JobDetailsFormObj.LoadData(id);
			}
		}

		public void EditOpportunity(string id)
		{
			FormActivator.BringFormToFront(FormActivator.OpportunityDetailsFormObj);
			if (id != "")
			{
				FormActivator.OpportunityDetailsFormObj.LoadData(id);
			}
		}

		public void EditCompetitor(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CompetitorDetailsFormObj);
			if (id != "")
			{
				FormActivator.CompetitorDetailsFormObj.LoadData(id);
			}
		}

		public void EditActivity(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ActivityDetailsFormObj);
			if (id != "")
			{
				FormActivator.ActivityDetailsFormObj.LoadData(id);
			}
		}

		public void EditCampaign(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CampaignDetailsFormObj);
			if (id != "")
			{
				FormActivator.CampaignDetailsFormObj.LoadData(id);
			}
		}

		public void EditEvent(string id)
		{
			FormActivator.BringFormToFront(FormActivator.EventDetailsFormObj);
			if (id != "")
			{
				FormActivator.EventDetailsFormObj.LoadData(id);
			}
		}

		public void EditBankFacility(string id)
		{
			FormActivator.BringFormToFront(FormActivator.BankFacilityFormObj);
			if (id != "")
			{
				FormActivator.BankFacilityFormObj.LoadData(id);
			}
		}

		public void EditBankFacilityGroup(string id)
		{
			FormActivator.BringFormToFront(FormActivator.BankFacilityGroupFormObj);
			if (id != "")
			{
				FormActivator.BankFacilityGroupFormObj.LoadData(id);
			}
		}

		public void EditCity(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CityDetailsFormObj);
			if (id != "")
			{
				FormActivator.CityDetailsFormObj.LoadData(id);
			}
		}

		public void EditAgent(string id)
		{
			FormActivator.BringFormToFront(FormActivator.AgentDetailsFormObj);
			if (id != "")
			{
				FormActivator.AgentDetailsFormObj.GenericListType = GenericListTypes.Agent;
				FormActivator.AgentDetailsFormObj.LoadData(id);
			}
		}

		public void EditJobFee(string id)
		{
			FormActivator.BringFormToFront(FormActivator.JobFeeFormObj);
			if (id != "")
			{
				FormActivator.JobFeeFormObj.LoadData(id);
			}
		}

		public void EditVehicle(string id)
		{
			FormActivator.BringFormToFront(FormActivator.VehicleDetailsFormObj);
			if (id != "")
			{
				FormActivator.VehicleDetailsFormObj.LoadData(id);
			}
		}

		public void EditQualification(string id)
		{
			GenericListDetailsForm genericListDetailsForm = new GenericListDetailsForm();
			FormActivator.BringFormToFront(genericListDetailsForm);
			genericListDetailsForm.GenericListType = GenericListTypes.Qualification;
			genericListDetailsForm.LoadData(id);
		}

		public void EditLanguage(string id)
		{
			GenericListDetailsForm genericListDetailsForm = new GenericListDetailsForm();
			FormActivator.BringFormToFront(genericListDetailsForm);
			genericListDetailsForm.GenericListType = GenericListTypes.Language;
			genericListDetailsForm.LoadData(id);
		}

		public void EditItem(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ProductDetailsFormObj);
			if (id != "")
			{
				FormActivator.ProductDetailsFormObj.LoadData(id);
			}
		}

		public void EditProductCategory(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ProductCategoryDetailsFormObj);
			if (id != "")
			{
				FormActivator.ProductCategoryDetailsFormObj.LoadData(id);
			}
		}

		public void EditProductBrand(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ProductBrandDetailsFormObj);
			if (id != "")
			{
				FormActivator.ProductBrandDetailsFormObj.LoadData(id);
			}
		}

		public void EditProductManufacturer(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ProductManufacturerDetailsFormObj);
			if (id != "")
			{
				FormActivator.ProductManufacturerDetailsFormObj.LoadData(id);
			}
		}

		public void EditProductStyle(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ProductStyleDetailsFormObj);
			if (id != "")
			{
				FormActivator.ProductStyleDetailsFormObj.LoadData(id);
			}
		}

		public void EditProductSpecification(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ProductSpecificationDetailsFormObj);
			if (id != "")
			{
				FormActivator.ProductSpecificationDetailsFormObj.LoadData(id);
			}
		}

		public void EditProductClass(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ProductClassDetailsFormObj);
			if (id != "")
			{
				FormActivator.ProductClassDetailsFormObj.LoadData(id);
			}
		}

		public void EditUOM(string id)
		{
			FormActivator.BringFormToFront(FormActivator.UnitDetailsFormObj);
			if (id != "")
			{
				FormActivator.UnitDetailsFormObj.LoadData(id);
			}
		}

		public void EditVendor(string id)
		{
			FormActivator.BringFormToFront(FormActivator.VendorDetailsFormObj);
			if (id != "")
			{
				FormActivator.VendorDetailsFormObj.LoadData(id);
			}
		}

		public void EditServiceItem(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ServiceItemFormObj);
			if (id != "")
			{
				FormActivator.ServiceItemFormObj.LoadData(id);
			}
		}

		public void EditVendorClass(string id)
		{
			FormActivator.BringFormToFront(FormActivator.VendorClassDetailsFormObj);
			if (id != "")
			{
				FormActivator.VendorClassDetailsFormObj.LoadData(id);
			}
		}

		public void EditCurrency(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CurrencyDetailsFormObj);
			if (id != "")
			{
				FormActivator.CurrencyDetailsFormObj.LoadData(id);
			}
		}

		public void EditCustomerAddress(string customerID, string addressID)
		{
			FormActivator.BringFormToFront(FormActivator.CustomerAddressDetailsFormObj);
			if (customerID != "")
			{
				FormActivator.CustomerAddressDetailsFormObj.LoadData(customerID, addressID);
			}
		}

		public void EditLeadAddress(string leadID, string addressID)
		{
			FormActivator.BringFormToFront(FormActivator.LeadAddressDetailsFormObj);
			if (leadID != "")
			{
				FormActivator.LeadAddressDetailsFormObj.LoadData(leadID, addressID);
			}
		}

		public void EditCompanyAddress(string addressID)
		{
			FormActivator.BringFormToFront(FormActivator.CompanyAddressDetailsFormObj);
			if (addressID != "")
			{
				FormActivator.CompanyAddressDetailsFormObj.LoadData(addressID);
			}
		}

		public void EditEmployeeLoan(string sysDocID, string voucherID)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeLoanFormObj);
			if (voucherID != "")
			{
				FormActivator.EmployeeLoanFormObj.LoadData(sysDocID, voucherID);
			}
		}

		public void EditEmployeeAddress(string employeeID, string addressID)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeAddressDetailsFormObj);
			if (employeeID != "")
			{
				FormActivator.EmployeeAddressDetailsFormObj.LoadData(employeeID, addressID);
			}
		}

		public void EditVendorAddress(string vendorID, string addressID)
		{
			FormActivator.BringFormToFront(FormActivator.VendorAddressDetailsFormObj);
			if (vendorID != "")
			{
				FormActivator.VendorAddressDetailsFormObj.LoadData(vendorID, addressID);
			}
		}

		public void EditBuyer(string buyerID)
		{
			FormActivator.BringFormToFront(FormActivator.BuyerDetailsFormObj);
			if (buyerID != "")
			{
				FormActivator.BuyerDetailsFormObj.LoadData(buyerID);
			}
		}

		public void EditGrade(string gradeID)
		{
			FormActivator.BringFormToFront(FormActivator.GradeDetailsFormObj);
			if (gradeID != "")
			{
				FormActivator.GradeDetailsFormObj.LoadData(gradeID);
			}
		}

		public void EditSponsor(string sponsorID)
		{
			FormActivator.BringFormToFront(FormActivator.SponsorDetailsFormObj);
			if (sponsorID != "")
			{
				FormActivator.SponsorDetailsFormObj.LoadData(sponsorID);
			}
		}

		public void EditNationality(string nationalityID)
		{
			FormActivator.BringFormToFront(FormActivator.NationalityDetailsFormObj);
			if (nationalityID != "")
			{
				FormActivator.NationalityDetailsFormObj.LoadData(nationalityID);
			}
		}

		public void EditReligion(string religionID)
		{
			FormActivator.BringFormToFront(FormActivator.ReligionDetailsFormObj);
			if (religionID != "")
			{
				FormActivator.ReligionDetailsFormObj.LoadData(religionID);
			}
		}

		public void EditDivision(string divisionID)
		{
			FormActivator.BringFormToFront(FormActivator.DivisionDetailsFormObj);
			if (divisionID != "")
			{
				FormActivator.DivisionDetailsFormObj.LoadData(divisionID);
			}
		}

		public void EditPosition(string positionID)
		{
			FormActivator.BringFormToFront(FormActivator.PositionDetailsFormObj);
			if (positionID != "")
			{
				FormActivator.PositionDetailsFormObj.LoadData(positionID);
			}
		}

		public void EditDepartment(string departmentID)
		{
			FormActivator.BringFormToFront(FormActivator.DepartmentDetailsFormObj);
			if (departmentID != "")
			{
				FormActivator.DepartmentDetailsFormObj.LoadData(departmentID);
			}
		}

		public void EditDegree(string degreeID)
		{
			FormActivator.BringFormToFront(FormActivator.DegreeDetailsFormObj);
			if (degreeID != "")
			{
				FormActivator.DegreeDetailsFormObj.LoadData(degreeID);
			}
		}

		public void EditSkill(string skillID)
		{
			FormActivator.BringFormToFront(FormActivator.SkillDetailsFormObj);
			if (skillID != "")
			{
				FormActivator.SkillDetailsFormObj.LoadData(skillID);
			}
		}

		public void EditJobTask(string taskID)
		{
			FormActivator.BringFormToFront(FormActivator.JobTaskDetailsFormObj);
			if (taskID != "")
			{
				FormActivator.JobTaskDetailsFormObj.LoadData(taskID);
			}
		}

		public void EditJobTaskGroup(string taskGroupID)
		{
			FormActivator.BringFormToFront(FormActivator.JobTaskGroupDetailsFormObj);
			if (taskGroupID != "")
			{
				FormActivator.JobTaskGroupDetailsFormObj.LoadData(taskGroupID);
			}
		}

		public void EditFollowup(string FollowupID)
		{
			FormActivator.BringFormToFront(FormActivator.FollowupDetailsFormObj);
			if (FollowupID != "")
			{
				FormActivator.FollowupDetailsFormObj.LoadData(FollowupID);
			}
		}

		public void EditEntityCategory(string CategoryID)
		{
			FormActivator.BringFormToFront(FormActivator.CustomerCategoryFormObj);
			if (CategoryID != "")
			{
				FormActivator.CustomerCategoryFormObj.LoadData(CategoryID);
			}
		}

		public void EditGenericList(GenericListTypes type, string id)
		{
			FormActivator.BringFormToFront(FormActivator.GenericListDetailsFormObj);
			if (id != "")
			{
				FormActivator.GenericListDetailsFormObj.LoadData(type, id);
			}
			else
			{
				FormActivator.GenericListDetailsFormObj.GenericListType = type;
			}
		}

		public void EditGenericProductTypeList(GenericListTypes type, string id)
		{
			FormActivator.BringFormToFront(FormActivator.GenericProductTypeListDetailsFormObj);
			if (id != "")
			{
				FormActivator.GenericProductTypeListDetailsFormObj.LoadData(type, id);
			}
			else
			{
				FormActivator.GenericProductTypeListDetailsFormObj.GenericListType = type;
			}
		}

		public void EditEmployeeDocType(string employeeDocTypeID)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeDocTypeDetailsFormObj);
			if (employeeDocTypeID != "")
			{
				FormActivator.EmployeeDocTypeDetailsFormObj.LoadData(employeeDocTypeID);
			}
		}

		public void EditVehicleDocType(string vehicleDocTypeID)
		{
			FormActivator.BringFormToFront(FormActivator.VehicleDocTypeDetailsFormObj);
			if (vehicleDocTypeID != "")
			{
				FormActivator.VehicleDocTypeDetailsFormObj.LoadData(vehicleDocTypeID);
			}
		}

		public void EditCustomerGroup(string groupID)
		{
			FormActivator.BringFormToFront(FormActivator.CustomerGroupDetailsFormObj);
			if (groupID != "")
			{
				FormActivator.CustomerGroupDetailsFormObj.LoadData(groupID);
			}
		}

		public void EditCustomerRelation(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CustomerNationalAccountFormObj);
			if (id != "")
			{
				FormActivator.CustomerNationalAccountFormObj.LoadData(id);
			}
		}

		public void EditCustomerVendorRelation(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CustomerVendorLinkFormObj);
			if (id != "")
			{
				FormActivator.CustomerVendorLinkFormObj.LoadData(id);
			}
		}

		public void EditEquipment(string id)
		{
			FormActivator.BringFormToFront(FormActivator.EquipmentDetailFormObj);
			if (id != "")
			{
				FormActivator.EquipmentDetailFormObj.LoadData(id);
			}
		}

		public void EditPriceList(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PriceListDetailsFormObj);
			if (id != "")
			{
				FormActivator.PriceListDetailsFormObj.LoadData(id);
			}
		}

		public void EditCandidate(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CandidateDetailsFormObj);
			if (id != "")
			{
				FormActivator.CandidateDetailsFormObj.LoadData(id);
			}
		}

		public void EditAppointment(string id)
		{
			FormActivator.BringFormToFront(FormActivator.AppointmentDetailsFormObj);
			if (id != "")
			{
				FormActivator.AppointmentDetailsFormObj.LoadData(id);
			}
		}

		public void EditWorkLocation(string id)
		{
			FormActivator.BringFormToFront(FormActivator.WorkLocationDetailsFormObj);
			if (id != "")
			{
				FormActivator.WorkLocationDetailsFormObj.LoadData(id);
			}
		}

		public void EditProvisionType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ProvisionTypeDetailsFormObj);
			if (id != "")
			{
				FormActivator.ProvisionTypeDetailsFormObj.LoadData(id);
			}
		}

		public void EditExpense(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ExpenseCodeDetailsFormObj);
			if (id != "")
			{
				FormActivator.ExpenseCodeDetailsFormObj.LoadData(id);
			}
		}

		public void EditTransporter(string id)
		{
			FormActivator.BringFormToFront(FormActivator.TransporterDetailsFormObj);
			if (id != "")
			{
				FormActivator.TransporterDetailsFormObj.LoadData(id);
			}
		}

		public void EditINCO(string id)
		{
			FormActivator.BringFormToFront(FormActivator.INCODetailsFormObj);
			if (id != "")
			{
				FormActivator.INCODetailsFormObj.LoadData(id);
			}
		}

		public void EditCollateral(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CollateralDetailsFormObj);
			if (id != "")
			{
				FormActivator.CollateralDetailsFormObj.LoadData(id);
			}
		}

		public void EditCustomReport(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CustomReportDetailFormObj);
			if (id != "")
			{
				FormActivator.CustomReportDetailFormObj.LoadData(id);
			}
		}

		public void EditCustomGadget(string id)
		{
			new CustomGadgetDetailForm();
			if (id != "")
			{
				FormActivator.BringFormToFront(FormActivator.CustomGadgetDetailsFormObj);
				FormActivator.CustomGadgetDetailsFormObj.LoadData(id);
			}
		}

		public void EditVendorGroup(string groupID)
		{
			FormActivator.BringFormToFront(FormActivator.VendorGroupDetailsFormObj);
			if (groupID != "")
			{
				FormActivator.VendorGroupDetailsFormObj.LoadData(groupID);
			}
		}

		public void EditEmployeeGroup(string id)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeGroupDetailsFormObj);
			if (id != "")
			{
				FormActivator.EmployeeGroupDetailsFormObj.LoadData(id);
			}
		}

		public void EditEmployeeType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeTypeDetailsFormObj);
			if (id != "")
			{
				FormActivator.EmployeeTypeDetailsFormObj.LoadData(id);
			}
		}

		public void EditLocation(string id)
		{
			FormActivator.BringFormToFront(FormActivator.LocationDetailsFormObj);
			if (id != "")
			{
				FormActivator.LocationDetailsFormObj.LoadData(id);
			}
		}

		public void EditPOSLocation(string id)
		{
			FormActivator.BringFormToFront(FormActivator.POSLocationDetailsFormObj);
			if (id != "")
			{
				FormActivator.POSLocationDetailsFormObj.LoadData(id);
			}
		}

		public void EditConsignLocation(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ConsignLocationDetailsFormObj);
			if (id != "")
			{
				FormActivator.ConsignLocationDetailsFormObj.LoadData(id);
			}
		}

		public void EditLeaveType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.LeaveTypeDetailsFormObj);
			if (id != "")
			{
				FormActivator.LeaveTypeDetailsFormObj.LoadData(id);
			}
		}

		public void EditJobType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.JobTypeDetailsFormObj);
			if (id != "")
			{
				FormActivator.JobTypeDetailsFormObj.LoadData(id);
			}
		}

		public void EditCostCategory(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CostCategoryDetailsFormObj);
			if (id != "")
			{
				FormActivator.CostCategoryDetailsFormObj.LoadData(id);
			}
		}

		public void EditPayrollItem(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PayrollItemDetailsFormObj);
			if (id != "")
			{
				FormActivator.PayrollItemDetailsFormObj.LoadData(id);
			}
		}

		public void EditReleaseType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ReleaseTypeFormObj);
			if (id != "")
			{
				FormActivator.ReleaseTypeFormObj.LoadData(id);
			}
		}

		public void EditDeduction(string id)
		{
			FormActivator.BringFormToFront(FormActivator.DeductionDetailsFormObj);
			if (id != "")
			{
				FormActivator.DeductionDetailsFormObj.LoadData(id);
			}
		}

		public void EditBenefit(string id)
		{
			FormActivator.BringFormToFront(FormActivator.BenefitDetailsFormObj);
			if (id != "")
			{
				FormActivator.BenefitDetailsFormObj.LoadData(id);
			}
		}

		public void EditFiscalYear(string id)
		{
			FormActivator.BringFormToFront(FormActivator.FiscalYearDetailsFormObj);
			if (id != "")
			{
				FormActivator.FiscalYearDetailsFormObj.LoadData(id);
			}
		}

		public void EditBank(string id)
		{
			FormActivator.BringFormToFront(FormActivator.BankDetailsFormObj);
			if (id != "")
			{
				FormActivator.BankDetailsFormObj.LoadData(id);
			}
		}

		public void EditDestination(string id)
		{
			FormActivator.BringFormToFront(FormActivator.DestinationDetailsFormObj);
			if (id != "")
			{
				FormActivator.DestinationDetailsFormObj.LoadData(id);
			}
		}

		public void EditEOSRule(string id)
		{
			FormActivator.BringFormToFront(FormActivator.EOSRuleDetailsFormObj);
			if (id != "")
			{
				FormActivator.EOSRuleDetailsFormObj.LoadData(id);
			}
		}

		public void EditCompanyDocType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CompanyDocTypeDetailsFormObj);
			if (id != "")
			{
				FormActivator.CompanyDocTypeDetailsFormObj.LoadData(id);
			}
		}

		public void EditCompanyDocument(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CompanyDocumentsFormObj);
			if (id != "")
			{
				FormActivator.CompanyDocumentsFormObj.LoadData(id);
			}
		}

		public void EditTenancyContract(string id)
		{
			FormActivator.BringFormToFront(FormActivator.TenancyContractDetailsFormObj);
			if (id != "")
			{
				FormActivator.TenancyContractDetailsFormObj.LoadData(id);
			}
		}

		public void EditTradeLicense(string id)
		{
			FormActivator.BringFormToFront(FormActivator.TradeLicenseDetailsFormObj);
			if (id != "")
			{
				FormActivator.TradeLicenseDetailsFormObj.LoadData(id);
			}
		}

		public void EditVisa(string id)
		{
			FormActivator.BringFormToFront(FormActivator.VisaDetailsFormObj);
			if (id != "")
			{
				FormActivator.VisaDetailsFormObj.LoadData(id);
			}
		}

		public void EditCostCenter(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CostCenterDetailsFormObj);
			if (id != "")
			{
				FormActivator.CostCenterDetailsFormObj.LoadData(id);
			}
		}

		public void EditQualityTask(string TaskID)
		{
			FormActivator.BringFormToFront(FormActivator.QualityTaskFormObj);
			if (TaskID != "")
			{
				FormActivator.QualityTaskFormObj.LoadData(TaskID);
			}
		}

		public void EditArrivalReportTemplate(string TaskID)
		{
			FormActivator.BringFormToFront(FormActivator.ArrivalReportTemplateFormObj);
			if (TaskID != "")
			{
				FormActivator.ArrivalReportTemplateFormObj.LoadData(TaskID);
			}
		}

		public void EditSurveyor(string id)
		{
			FormActivator.BringFormToFront(FormActivator.SurveyorFormObj);
			if (id != "")
			{
				FormActivator.SurveyorFormObj.LoadData(id);
			}
		}

		public void EditSysDoc(string id, SysDocTypes docType)
		{
			FormActivator.BringFormToFront(FormActivator.SysDocDetailsFormObj);
			FormActivator.SysDocDetailsFormObj.SysDocTypeID = docType;
			if (id != "")
			{
				FormActivator.SysDocDetailsFormObj.LoadData(id);
			}
		}

		public void EditLeadStatus(string id)
		{
			FormActivator.BringFormToFront(FormActivator.LeadStatusFormObj);
			if (id != "")
			{
				FormActivator.LeadStatusFormObj.LoadData(id);
			}
		}

		public void EditEmployeeLoanType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeLoanTypeDetailsFormObj);
			if (id != "")
			{
				FormActivator.EmployeeLoanTypeDetailsFormObj.LoadData(id);
			}
		}

		public void EditEmployeeLeave(string id)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeLeaveRequestFormObj);
			if (id != "")
			{
				FormActivator.EmployeeLeaveRequestFormObj.LoadData(int.Parse(id));
			}
		}

		public void EditEmployeeLeaveResumption(string id)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeResumptionFormObj);
			if (id != "")
			{
				FormActivator.EmployeeResumptionFormObj.LoadData(int.Parse(id));
			}
		}

		public void EditRegister(string id)
		{
			FormActivator.BringFormToFront(FormActivator.RegisterDetailsFormObj);
			if (id != "")
			{
				FormActivator.RegisterDetailsFormObj.LoadData(id);
			}
		}

		public void EditChequebook(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ChequebookDetailsFormObj);
			if (id != "")
			{
				FormActivator.ChequebookDetailsFormObj.LoadData(id);
			}
		}

		public void EditReturnedChequeReason(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ReturnedChequeReasonDetailsFormObj);
			if (id != "")
			{
				FormActivator.ReturnedChequeReasonDetailsFormObj.LoadData(id);
			}
		}

		public void EditAdjustmentType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.AdjustmentTypeDetailsFormObj);
			if (id != "")
			{
				FormActivator.AdjustmentTypeDetailsFormObj.LoadData(id);
			}
		}

		public void EditProduct(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ProductDetailsFormObj);
			if (id != "")
			{
				FormActivator.ProductDetailsFormObj.LoadData(id);
			}
		}

		public void EditCheckList(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CheckListDetailsFormObj);
			if (id != "")
			{
				FormActivator.CheckListDetailsFormObj.LoadData(id);
			}
		}

		public void EditDriver(string id)
		{
			FormActivator.BringFormToFront(FormActivator.DriverDetailsFormObj);
			if (id != "")
			{
				FormActivator.DriverDetailsFormObj.LoadData(id);
			}
		}

		public void EditPort(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PortDetailsFormObj);
			if (id != "")
			{
				FormActivator.PortDetailsFormObj.LoadData(id);
			}
		}

		public void EditDisciplineActionType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.DisciplineActionTypeDetailsFormObj);
			if (id != "")
			{
				FormActivator.DisciplineActionTypeDetailsFormObj.LoadData(id);
			}
		}

		public void EditEmployeeActivityType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeActivityTypeDetailsFormObj);
			if (id != "")
			{
				FormActivator.EmployeeActivityTypeDetailsFormObj.LoadData(id);
			}
		}

		public void EditVehicleType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.VehicleTypeForm);
		}

		public void EditUser(string id)
		{
			FormActivator.BringFormToFront(FormActivator.UserDetailsFormObj);
			if (id != "")
			{
				FormActivator.UserDetailsFormObj.LoadData(id);
			}
		}

		public void EditUserGroup(string id)
		{
			FormActivator.BringFormToFront(FormActivator.UserGroupDetailsFormObj);
			if (id != "")
			{
				FormActivator.UserGroupDetailsFormObj.LoadData(id);
			}
		}

		public void EditEmployeeDocument(string id)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeDocumentsFormObj);
			if (id != "")
			{
				FormActivator.EmployeeDocumentsFormObj.LoadData(id);
			}
		}

		public void EditPropertyAgent(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PropertyAgentDetailsFormObj);
			if (id != "")
			{
				FormActivator.PropertyAgentDetailsFormObj.LoadData(id);
			}
		}

		public void EditPropertyClass(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PropertyClassDetailsFormObj);
			if (id != "")
			{
				FormActivator.PropertyClassDetailsFormObj.LoadData(id);
			}
		}

		public void EditProperty(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PropertyDetailsFormObj);
			if (id != "")
			{
				FormActivator.PropertyDetailsFormObj.LoadData(id);
			}
		}

		public void EditPropertyUnit(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PropertyUnitDetailsFormObj);
			if (id != "")
			{
				FormActivator.PropertyUnitDetailsFormObj.LoadData(id);
			}
		}

		public void EditPropertyCategory(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PropertyCategoryDetailsFormObj);
			if (id != "")
			{
				FormActivator.PropertyCategoryDetailsFormObj.LoadData(id);
			}
		}

		public void EditPropertyIncomeCode(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PropertyIncomeCodeDetailsFormObj);
			if (id != "")
			{
				FormActivator.PropertyIncomeCodeDetailsFormObj.LoadData(id);
			}
		}

		public void EditContainerSize(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ContainerSizeDetailsFormObj);
			if (id != "")
			{
				FormActivator.ContainerSizeDetailsFormObj.LoadData(id);
			}
		}

		public void EditEmployeeClass(string id)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeTypeDetailsFormObj);
			if (id != "")
			{
				FormActivator.EmployeeTypeDetailsFormObj.LoadData(id);
			}
		}

		public void EditFixedAsset(string id)
		{
			FormActivator.BringFormToFront(FormActivator.FixedAssetDetailsFormObj);
			if (id != "")
			{
				FormActivator.FixedAssetDetailsFormObj.LoadData(id);
			}
		}

		public void EditInventoryTransferType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.InventoryTransferTypeDetailsFormObj);
			if (id != "")
			{
				FormActivator.InventoryTransferTypeDetailsFormObj.LoadData(id);
			}
		}

		public void EditFixedAssetGroup(string id)
		{
			FormActivator.BringFormToFront(FormActivator.FixedAssetGroupDetailsFormObj);
			if (id != "")
			{
				FormActivator.FixedAssetGroupDetailsFormObj.LoadData(id);
			}
		}

		public void EditFixedAssetLocation(string id)
		{
			FormActivator.BringFormToFront(FormActivator.FixedAssetLocationDetailsFormObj);
			if (id != "")
			{
				FormActivator.FixedAssetLocationDetailsFormObj.LoadData(id);
			}
		}

		public void EditFixedAssetClass(string id)
		{
			FormActivator.BringFormToFront(FormActivator.FixedAssetClassDetailsFormObj);
			if (id != "")
			{
				FormActivator.FixedAssetClassDetailsFormObj.LoadData(id);
			}
		}

		public void EditTenant(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PropertyTenantFormObj);
			if (id != "")
			{
				FormActivator.PropertyTenantFormObj.LoadData(id);
			}
		}

		public void EditBOM(string id)
		{
			FormActivator.BringFormToFront(FormActivator.BOMDetailsFormObj);
			if (id != "")
			{
				FormActivator.BOMDetailsFormObj.LoadData(id);
			}
		}

		public void EditJobBOM(string id)
		{
			FormActivator.BringFormToFront(FormActivator.JobBOMDetailsFormObj);
			if (id != "")
			{
				FormActivator.JobBOMDetailsFormObj.LoadData(id);
			}
		}

		public void EditPackage(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PackageDetailsFormObj);
			if (id != "")
			{
				FormActivator.PackageDetailsFormObj.LoadData(id);
			}
		}

		public void EditBin(string id)
		{
			FormActivator.BringFormToFront(FormActivator.BinDetailsFormObj);
			if (id != "")
			{
				FormActivator.BinDetailsFormObj.LoadData(id);
			}
		}

		public void EditRoute(string id)
		{
			FormActivator.BringFormToFront(FormActivator.RouteDetailsFormObj);
			if (id != "")
			{
				FormActivator.RouteDetailsFormObj.LoadData(id);
			}
		}

		public void EditRouteGroup(string id)
		{
			FormActivator.BringFormToFront(FormActivator.RouteGroupDetailsFormObj);
			if (id != "")
			{
				FormActivator.RouteGroupDetailsFormObj.LoadData(id);
			}
		}

		public void EditPassportControl(string id)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeePassportControlFormObj);
			if (id != "")
			{
				FormActivator.EmployeePassportControlFormObj.LoadData(int.Parse(id));
			}
		}

		public void EditApproval(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ApprovalDetailsFormObj);
			if (id != "")
			{
				FormActivator.ApprovalDetailsFormObj.LoadData(id);
			}
		}

		public void EditVerification(string id)
		{
			FormActivator.BringFormToFront(FormActivator.VerificationDetailsFormObj);
			if (id != "")
			{
				FormActivator.VerificationDetailsFormObj.LoadData(id);
			}
		}

		public void EditLead(string id)
		{
			FormActivator.BringFormToFront(FormActivator.LeadDetailsFormObj);
			if (id != "")
			{
				FormActivator.LeadDetailsFormObj.LoadData(id);
			}
		}

		public void EditClientAsset(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ClientAssetFormObj);
			if (id != "")
			{
				FormActivator.ClientAssetFormObj.LoadData(id);
			}
		}

		public void EditEmployeeAppraisal(string id)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeAppraisalFormObj);
			if (id != "")
			{
				FormActivator.EmployeeAppraisalFormObj.LoadData(id);
			}
		}

		public void EditInsuranceProvider(string id)
		{
			FormActivator.BringFormToFront(FormActivator.InsurancePRoviderFormObj);
			if (id != "")
			{
				FormActivator.InsurancePRoviderFormObj.LoadData(id);
			}
		}

		public void EditCRMCustomerActivity(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ActivityDetailsFormObj);
			if (id != "")
			{
				FormActivator.ActivityDetailsFormObj.LoadData(id);
			}
		}

		public void EditRiderSummary(string id)
		{
			FormActivator.BringFormToFront(FormActivator.RiderSummaryDetailsFormObj);
			if (id != "")
			{
				FormActivator.RiderSummaryDetailsFormObj.LoadData(id);
			}
		}

		public void EditHorseSummary(string id)
		{
			FormActivator.BringFormToFront(FormActivator.HorseSummaryDetailsFormObj);
			if (id != "")
			{
				FormActivator.HorseSummaryDetailsFormObj.LoadData(id);
			}
		}

		public void EditHorseType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.HorseTypeFormObj);
			if (id != "")
			{
				FormActivator.HorseTypeFormObj.LoadData(id);
			}
		}

		public void EditHorseSex(string id)
		{
			FormActivator.BringFormToFront(FormActivator.HorseSexFormObj);
			if (id != "")
			{
				FormActivator.HorseSexFormObj.LoadData(id);
			}
		}

		public void EditInsuranceCategory(string id)
		{
			FormActivator.BringFormToFront(FormActivator.MedicalInsuranceCategoryForm);
			if (id != "")
			{
				FormActivator.MedicalInsuranceCategoryForm.LoadData(id);
			}
		}

		public void EditHorseCategory(string id)
		{
			FormActivator.BringFormToFront(FormActivator.HorseCategoryFormObj);
			if (id != "")
			{
				FormActivator.HorseCategoryFormObj.LoadData(id);
			}
		}

		public void EditHorseOwnershipType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.HorseOwnershipTypeFormObj);
			if (id != "")
			{
				FormActivator.HorseOwnershipTypeFormObj.LoadData(id);
			}
		}

		public void EditHolidayCalendar(string id)
		{
			FormActivator.BringFormToFront(FormActivator.HolidayCalendarFormObj);
			if (id != "")
			{
				FormActivator.HolidayCalendarFormObj.LoadData(id);
			}
		}

		public void EditOverTime(string id)
		{
			FormActivator.BringFormToFront(FormActivator.OverTimeDetailsFormObj);
			if (id != "")
			{
				FormActivator.OverTimeDetailsFormObj.LoadData(id);
			}
		}

		public void EditEquipmentCategory(string id)
		{
			FormActivator.BringFormToFront(FormActivator.EquipmentCategoryFormObj);
			if (id != "")
			{
				FormActivator.EquipmentCategoryFormObj.LoadData(id);
			}
		}

		public void EditEquipmentType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.EquipmentTypeFormObj);
			if (id != "")
			{
				FormActivator.EquipmentTypeFormObj.LoadData(id);
			}
		}

		public void EditEAEquipment(string id)
		{
			FormActivator.BringFormToFront(FormActivator.EAEquipmentFormObj);
			if (id != "")
			{
				FormActivator.EAEquipmentFormObj.LoadData(id);
			}
		}

		public void EditRequisitionType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.RequisitionTypeFormObj);
			if (id != "")
			{
				FormActivator.RequisitionTypeFormObj.LoadData(id);
			}
		}

		public void EditType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.RequisitionTypeFormObj);
			if (id != "")
			{
				FormActivator.RequisitionTypeFormObj.LoadData(id);
			}
		}

		public void EditLawyer(string id)
		{
			FormActivator.BringFormToFront(FormActivator.LawyerDetailsFormObj);
			if (id != "")
			{
				FormActivator.LawyerDetailsFormObj.LoadData(id);
			}
		}

		public void EditCaseParty(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CasePartyDetailsFormObj);
			if (id != "")
			{
				FormActivator.CasePartyDetailsFormObj.LoadData(id);
			}
		}

		public void EditTax(string id)
		{
			FormActivator.BringFormToFront(FormActivator.TaxEntryFormObj);
			if (id != "")
			{
				FormActivator.TaxEntryFormObj.LoadData(id);
			}
		}

		public void EditTaxGroup(string id)
		{
			FormActivator.BringFormToFront(FormActivator.TaxGroupFormObj);
			if (id != "")
			{
				FormActivator.TaxGroupFormObj.LoadData(id);
			}
		}

		public void EditSysDoc(string id)
		{
			FormActivator.BringFormToFront(FormActivator.SysDocDetailsFormObj);
			if (id != "")
			{
				FormActivator.SysDocDetailsFormObj.LoadData(id);
			}
		}

		public void EditShippingCompany(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ShippingCompanyFormObj);
			if (id != "")
			{
				FormActivator.ShippingCompanyFormObj.LoadData(id);
			}
		}

		public void EditCaseClient(string id)
		{
			FormActivator.BringFormToFront(FormActivator.CaseClientDetailsFormObj);
			if (id != "")
			{
				FormActivator.CaseClientDetailsFormObj.LoadData(id);
			}
		}

		public void EditProductMake(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ProductMakeDetilsFormObj);
			if (id != "")
			{
				FormActivator.ProductMakeDetilsFormObj.LoadData(id);
			}
		}

		public void EditProductType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ProductTypeDetilsFormObj);
			if (id != "")
			{
				FormActivator.ProductTypeDetilsFormObj.LoadData(id);
			}
		}

		public void EditProductModel(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ProductModelDetailsFormObj);
			if (id != "")
			{
				FormActivator.ProductModelDetailsFormObj.LoadData(id);
			}
		}

		public void EditSalespersonGroup(string id)
		{
			FormActivator.BringFormToFront(FormActivator.SalespersonGroupDetailsFormObj);
			if (id != "")
			{
				FormActivator.SalespersonGroupDetailsFormObj.LoadData(id);
			}
		}

		public void EditTaskSteps(string id)
		{
			FormActivator.BringFormToFront(FormActivator.TaskStepsFormObj);
			if (id != "")
			{
				FormActivator.TaskStepsFormObj.LoadData(id);
			}
		}

		public void EditTaskType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.TaskTypeFormObj);
			if (id != "")
			{
				FormActivator.TaskTypeFormObj.LoadData(id);
			}
		}

		public void EditLegalActionStatus(string id)
		{
			FormActivator.BringFormToFront(FormActivator.LegalActionStatusFormObj);
			if (id != "")
			{
				FormActivator.LegalActionStatusFormObj.LoadData(id);
			}
		}

		public void EditRack(string id)
		{
			FormActivator.BringFormToFront(FormActivator.RackDetailsformObj);
			if (id != "")
			{
				FormActivator.RackDetailsformObj.LoadData(id);
			}
		}

		public void EditPropertyDocType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PropertyDocTypeFormObj);
			if (id != "")
			{
				FormActivator.PropertyDocTypeFormObj.LoadData(id);
			}
		}

		public void EditPropertyTenantDocType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PropertyTenantDocTypeFormObj);
			if (id != "")
			{
				FormActivator.PropertyTenantDocTypeFormObj.LoadData(id);
			}
		}

		public void EditPropertyVirtualUnit(string id)
		{
			FormActivator.BringFormToFront(FormActivator.VirtualUnitDetailsFormObj);
			if (id != "")
			{
				FormActivator.VirtualUnitDetailsFormObj.LoadData(id);
			}
		}

		public void EditPropertyServiceType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PropertyServiceTypeForm);
			if (id != "")
			{
				FormActivator.PropertyServiceTypeForm.LoadData(id);
			}
		}

		public void EditPropertyFacilityType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PropertyFacilityForm);
			if (id != "")
			{
				FormActivator.PropertyFacilityForm.LoadData(id);
			}
		}

		public void EditServiceRequest(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PropertyServiceRequestFormObj);
			if (id != "")
			{
				FormActivator.PropertyServiceRequestFormObj.LoadData(id);
			}
		}

		public void EditPrintTemplateMap(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PrintTemplateMappingFormObj);
			if (id != "")
			{
				FormActivator.PrintTemplateMappingFormObj.LoadData(id);
			}
		}

		public void EditEmployeeAbsconding(int id)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeAbscondingEntryFormObj);
			if (id > 0)
			{
				FormActivator.EmployeeAbscondingEntryFormObj.LoadData(id);
			}
		}

		public void EditPatient(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PatientFormObj);
			if (id != "")
			{
				FormActivator.PatientFormObj.LoadData(id);
			}
		}

		public void EditChronics(string id)
		{
			FormActivator.BringFormToFront(FormActivator.ChronicsDetailsFormObj);
			if (id != "")
			{
				FormActivator.ChronicsDetailsFormObj.LoadData(id);
			}
		}

		public void EditAllergy(string id)
		{
			FormActivator.BringFormToFront(FormActivator.AllergyDetailsFormObj);
			if (id != "")
			{
				FormActivator.AllergyDetailsFormObj.LoadData(id);
			}
		}

		public void EditPatientDocType(string id)
		{
			FormActivator.BringFormToFront(FormActivator.PatientDocTypeDetailsFormObj);
			if (id != "")
			{
				FormActivator.PatientDocTypeDetailsFormObj.LoadData(id);
			}
		}

		public void EditDataSyncSetupDetails(string id)
		{
			FormActivator.BringFormToFront(FormActivator.DataSyncSetupDetailsFormObj);
			if (id != "")
			{
				FormActivator.DataSyncSetupDetailsFormObj.LoadData(id);
			}
		}

		public void ShowList(DataComboType type)
		{
			switch (type)
			{
			case DataComboType.EmployeeAppraisal:
				return;
			case DataComboType.AccountGroup:
				FormActivator.AccountGroupListFormObj.Text = "Account Groups";
				FormActivator.AccountGroupListFormObj.ListType = DataComboType.AccountGroup;
				FormActivator.BringFormToFront(FormActivator.AccountGroupListFormObj);
				return;
			case DataComboType.PayrollItem:
				FormActivator.PayrollItemListFormObj.Text = "PayrollItems";
				FormActivator.PayrollItemListFormObj.ListType = DataComboType.PayrollItem;
				FormActivator.BringFormToFront(FormActivator.PayrollItemListFormObj);
				return;
			case DataComboType.Analysis:
				FormActivator.AnalysisListFormObj.Text = "Analyses";
				FormActivator.AnalysisListFormObj.ListType = DataComboType.Analysis;
				FormActivator.BringFormToFront(FormActivator.AnalysisListFormObj);
				return;
			case DataComboType.AnalysisGroup:
				FormActivator.AnalysisGroupListFormObj.Text = "Analysis Groups";
				FormActivator.AnalysisGroupListFormObj.ListType = DataComboType.AnalysisGroup;
				FormActivator.BringFormToFront(FormActivator.AnalysisGroupListFormObj);
				return;
			case DataComboType.Area:
				FormActivator.AreaListFormObj.Text = "Areas";
				FormActivator.AreaListFormObj.ListType = DataComboType.Area;
				FormActivator.BringFormToFront(FormActivator.AreaListFormObj);
				return;
			case DataComboType.ServiceItem:
				FormActivator.ServiceItemListFormObj.Text = "Service Item";
				FormActivator.ServiceItemListFormObj.ListType = DataComboType.ServiceItem;
				FormActivator.BringFormToFront(FormActivator.ServiceItemListFormObj);
				return;
			case DataComboType.EmployeeProvisionType:
				FormActivator.ProvisionTypeListFormObj.Text = "Provision Type";
				FormActivator.ProvisionTypeListFormObj.ListType = DataComboType.EmployeeProvisionType;
				FormActivator.BringFormToFront(FormActivator.ProvisionTypeListFormObj);
				return;
			case DataComboType.ServiceProvider:
				FormActivator.ServiceProviderListFormObj.Text = "Service Provider";
				FormActivator.ServiceProviderListFormObj.ListType = DataComboType.ServiceProvider;
				FormActivator.BringFormToFront(FormActivator.ServiceProviderListFormObj);
				return;
			case DataComboType.Bank:
				FormActivator.BankListFormObj.Text = "Banks";
				FormActivator.BankListFormObj.ListType = DataComboType.Bank;
				FormActivator.BringFormToFront(FormActivator.BankListFormObj);
				return;
			case DataComboType.Benefit:
				FormActivator.BenefitListFormObj.Text = "Benefits";
				FormActivator.BenefitListFormObj.ListType = DataComboType.Benefit;
				FormActivator.BringFormToFront(FormActivator.BenefitListFormObj);
				return;
			case DataComboType.EOSRule:
				FormActivator.EOSRuleListFormObj.Text = "Eos Rule";
				FormActivator.EOSRuleListFormObj.ListType = DataComboType.EOSRule;
				FormActivator.BringFormToFront(FormActivator.EOSRuleListFormObj);
				return;
			case DataComboType.Buyer:
				FormActivator.BuyerListFormObj.Text = "Buyers";
				FormActivator.BuyerListFormObj.ListType = DataComboType.Buyer;
				FormActivator.BringFormToFront(FormActivator.BuyerListFormObj);
				return;
			case DataComboType.CompanyDocType:
				FormActivator.CompanyDocTypeListFormObj.Text = "Company Document Types";
				FormActivator.CompanyDocTypeListFormObj.ListType = DataComboType.CompanyDocType;
				FormActivator.BringFormToFront(FormActivator.CompanyDocTypeListFormObj);
				return;
			case DataComboType.CompanyDocument:
				FormActivator.CompanyDocumentListFormObj.Text = "Company Document";
				FormActivator.CompanyDocumentListFormObj.ListType = DataComboType.CompanyDocument;
				FormActivator.BringFormToFront(FormActivator.CompanyDocumentListFormObj);
				return;
			case DataComboType.Contact:
				FormActivator.ContactListFormObj.Text = "Contacts";
				FormActivator.ContactListFormObj.ListType = DataComboType.Contact;
				FormActivator.BringFormToFront(FormActivator.ContactListFormObj);
				return;
			case DataComboType.Country:
				FormActivator.CountryListFormObj.Text = "Countries";
				FormActivator.CountryListFormObj.ListType = DataComboType.Country;
				FormActivator.BringFormToFront(FormActivator.CountryListFormObj);
				return;
			case DataComboType.Currency:
				FormActivator.CurrencyListFormObj.Text = "Currencies";
				FormActivator.CurrencyListFormObj.ListType = DataComboType.Currency;
				FormActivator.BringFormToFront(FormActivator.CurrencyListFormObj);
				return;
			case DataComboType.CustomerClass:
				FormActivator.CustomerClassListFormObj.Text = "Customer Classes";
				FormActivator.CustomerClassListFormObj.ListType = DataComboType.CustomerClass;
				FormActivator.BringFormToFront(FormActivator.CustomerClassListFormObj);
				return;
			case DataComboType.TenantClass:
				FormActivator.CustomerClassListFormObj.Text = "Tenant Classes";
				FormActivator.CustomerClassListFormObj.ListType = DataComboType.TenantClass;
				FormActivator.BringFormToFront(FormActivator.CustomerClassListFormObj);
				return;
			case DataComboType.CustomerGroup:
				FormActivator.CustomerGroupListFormObj.Text = "Customer Groups";
				FormActivator.CustomerGroupListFormObj.ListType = DataComboType.CustomerGroup;
				FormActivator.BringFormToFront(FormActivator.CustomerGroupListFormObj);
				return;
			case DataComboType.CheckList:
				FormActivator.CheckListFormObj.Text = "Check Lists";
				FormActivator.CheckListFormObj.ListType = DataComboType.CheckList;
				FormActivator.BringFormToFront(FormActivator.CheckListFormObj);
				return;
			case DataComboType.Deduction:
				FormActivator.DeductionListFormObj.Text = "Deductions";
				FormActivator.DeductionListFormObj.ListType = DataComboType.Deduction;
				FormActivator.BringFormToFront(FormActivator.DeductionListFormObj);
				return;
			case DataComboType.Degree:
				FormActivator.DegreeListFormObj.Text = "Degrees";
				FormActivator.DegreeListFormObj.ListType = DataComboType.Degree;
				FormActivator.BringFormToFront(FormActivator.DegreeListFormObj);
				return;
			case DataComboType.Department:
				FormActivator.DepartmentListFormObj.Text = "Departments";
				FormActivator.DepartmentListFormObj.ListType = DataComboType.Department;
				FormActivator.BringFormToFront(FormActivator.DepartmentListFormObj);
				return;
			case DataComboType.Destination:
				FormActivator.DestinationListFormObj.Text = "Destinations";
				FormActivator.DestinationListFormObj.ListType = DataComboType.Destination;
				FormActivator.BringFormToFront(FormActivator.DestinationListFormObj);
				return;
			case DataComboType.Division:
				FormActivator.DivisionListFormObj.Text = "Divisions";
				FormActivator.DivisionListFormObj.ListType = DataComboType.Division;
				FormActivator.BringFormToFront(FormActivator.DivisionListFormObj);
				return;
			case DataComboType.CompanyDivision:
				FormActivator.CompanyDivisionListFormObj.Text = "Company Divisions";
				FormActivator.CompanyDivisionListFormObj.ListType = DataComboType.CompanyDivision;
				FormActivator.BringFormToFront(FormActivator.CompanyDivisionListFormObj);
				return;
			case DataComboType.EmployeeDocType:
				FormActivator.EmployeeDocTypeListFormObj.Text = "Employee Document Types";
				FormActivator.EmployeeDocTypeListFormObj.ListType = DataComboType.EmployeeDocType;
				FormActivator.BringFormToFront(FormActivator.EmployeeDocTypeListFormObj);
				return;
			case DataComboType.EmployeeGroup:
				FormActivator.EmployeeGroupListFormObj.Text = "Employee Groups";
				FormActivator.EmployeeGroupListFormObj.ListType = DataComboType.EmployeeGroup;
				FormActivator.BringFormToFront(FormActivator.EmployeeGroupListFormObj);
				return;
			case DataComboType.EmployeeType:
				FormActivator.EmployeeTypeListFormObj.Text = "Employee Class";
				FormActivator.EmployeeTypeListFormObj.ListType = DataComboType.EmployeeType;
				FormActivator.BringFormToFront(FormActivator.EmployeeTypeListFormObj);
				return;
			case DataComboType.Grade:
				FormActivator.GradeListFormObj.Text = "Grades";
				FormActivator.GradeListFormObj.ListType = DataComboType.Grade;
				FormActivator.BringFormToFront(FormActivator.GradeListFormObj);
				return;
			case DataComboType.LeaveType:
				FormActivator.LeaveTypeListFormObj.Text = "Leave Types";
				FormActivator.LeaveTypeListFormObj.ListType = DataComboType.LeaveType;
				FormActivator.BringFormToFront(FormActivator.LeaveTypeListFormObj);
				return;
			case DataComboType.VehicleDocType:
				FormActivator.VehicleDocTypeListFormObj.Text = "Vehicle Document Types";
				FormActivator.VehicleDocTypeListFormObj.ListType = DataComboType.VehicleDocType;
				FormActivator.BringFormToFront(FormActivator.VehicleDocTypeListFormObj);
				return;
			case DataComboType.JobType:
				FormActivator.JobTypeListFormObj.Text = "Job Types";
				FormActivator.JobTypeListFormObj.ListType = DataComboType.JobType;
				FormActivator.BringFormToFront(FormActivator.JobTypeListFormObj);
				return;
			case DataComboType.CostCategory:
				FormActivator.CostCategoryListFormObj.Text = "Cost Categories";
				FormActivator.CostCategoryListFormObj.ListType = DataComboType.CostCategory;
				FormActivator.BringFormToFront(FormActivator.CostCategoryListFormObj);
				return;
			case DataComboType.Location:
				FormActivator.LocationListFormObj.Text = "Locations";
				FormActivator.LocationListFormObj.ListType = DataComboType.Location;
				FormActivator.BringFormToFront(FormActivator.LocationListFormObj);
				return;
			case DataComboType.Unit:
				FormActivator.UnitListFormObj.Text = "Units";
				FormActivator.UnitListFormObj.ListType = DataComboType.Unit;
				FormActivator.BringFormToFront(FormActivator.UnitListFormObj);
				return;
			case DataComboType.Nationality:
				FormActivator.NationalityListFormObj.Text = "Nationalities";
				FormActivator.NationalityListFormObj.ListType = DataComboType.Nationality;
				FormActivator.BringFormToFront(FormActivator.NationalityListFormObj);
				return;
			case DataComboType.PaymentMethod:
				FormActivator.PaymentMethodListFormObj.Text = "Payment Methods";
				FormActivator.PaymentMethodListFormObj.ListType = DataComboType.PaymentMethod;
				FormActivator.BringFormToFront(FormActivator.PaymentMethodListFormObj);
				return;
			case DataComboType.PaymentTerm:
				FormActivator.PaymentTermListFormObj.Text = "Payment Terms";
				FormActivator.PaymentTermListFormObj.ListType = DataComboType.PaymentTerm;
				FormActivator.BringFormToFront(FormActivator.PaymentTermListFormObj);
				return;
			case DataComboType.Position:
				FormActivator.PositionListFormObj.Text = "Positions";
				FormActivator.PositionListFormObj.ListType = DataComboType.Position;
				FormActivator.BringFormToFront(FormActivator.PositionListFormObj);
				return;
			case DataComboType.CustomerCategory:
				FormActivator.CustomerCategoryListObj.Text = "CustomerCategory";
				FormActivator.CustomerCategoryListObj.ListType = DataComboType.CustomerCategory;
				FormActivator.BringFormToFront(FormActivator.CustomerCategoryListObj);
				return;
			case DataComboType.ContactsCategory:
				FormActivator.ContactsCategoryListObj.Text = "ContactsCategory";
				FormActivator.ContactsCategoryListObj.ListType = DataComboType.ContactsCategory;
				FormActivator.BringFormToFront(FormActivator.ContactsCategoryListObj);
				return;
			case DataComboType.PriceLevel:
				FormActivator.PricelevelListFormObj.Text = "Price Levels";
				FormActivator.PricelevelListFormObj.ListType = DataComboType.PriceLevel;
				FormActivator.BringFormToFront(FormActivator.PricelevelListFormObj);
				return;
			case DataComboType.ProductBrand:
				FormActivator.ProductBrandListFormObj.Text = "Product Brands";
				FormActivator.ProductBrandListFormObj.ListType = DataComboType.ProductBrand;
				FormActivator.BringFormToFront(FormActivator.ProductBrandListFormObj);
				return;
			case DataComboType.ProductCategory:
				FormActivator.ProductCategoryListFormObj.Text = "Product Categories";
				FormActivator.ProductCategoryListFormObj.ListType = DataComboType.ProductCategory;
				FormActivator.BringFormToFront(FormActivator.ProductCategoryListFormObj);
				return;
			case DataComboType.ProductClass:
				FormActivator.ProductClassListFormObj.Text = "Product Classes";
				FormActivator.ProductClassListFormObj.ListType = DataComboType.ProductClass;
				FormActivator.BringFormToFront(FormActivator.ProductClassListFormObj);
				return;
			case DataComboType.ProductManufacturer:
				FormActivator.ProductManufacturerListFormObj.Text = "Product Manufacturers";
				FormActivator.ProductManufacturerListFormObj.ListType = DataComboType.ProductManufacturer;
				FormActivator.BringFormToFront(FormActivator.ProductManufacturerListFormObj);
				return;
			case DataComboType.ProductStyle:
				FormActivator.ProductStyleListFormObj.Text = "Product Styles";
				FormActivator.ProductStyleListFormObj.ListType = DataComboType.ProductStyle;
				FormActivator.BringFormToFront(FormActivator.ProductStyleListFormObj);
				return;
			case DataComboType.ProductSpecification:
				FormActivator.ProductSpecificationListFormObj.Text = "Product Specification";
				FormActivator.ProductSpecificationListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.ProductSpecificationListFormObj);
				return;
			case DataComboType.Religion:
				FormActivator.ReligionListFormObj.Text = "Religions";
				FormActivator.ReligionListFormObj.ListType = DataComboType.Religion;
				FormActivator.BringFormToFront(FormActivator.ReligionListFormObj);
				return;
			case DataComboType.Salesperson:
				FormActivator.SalespersonListFormObj.Text = "Salespersons";
				FormActivator.SalespersonListFormObj.ListType = DataComboType.Salesperson;
				FormActivator.BringFormToFront(FormActivator.SalespersonListFormObj);
				return;
			case DataComboType.ShippingMethod:
				FormActivator.ShippingMethodListFormObj.Text = "Shipping Methods";
				FormActivator.ShippingMethodListFormObj.ListType = DataComboType.ShippingMethod;
				FormActivator.BringFormToFront(FormActivator.ShippingMethodListFormObj);
				return;
			case DataComboType.Skill:
				FormActivator.SkillListFormObj.Text = "Skills";
				FormActivator.SkillListFormObj.ListType = DataComboType.Skill;
				FormActivator.BringFormToFront(FormActivator.SkillListFormObj);
				return;
			case DataComboType.Sponsor:
				FormActivator.SponsorListFormObj.Text = "Sponsors";
				FormActivator.SponsorListFormObj.ListType = DataComboType.Sponsor;
				FormActivator.BringFormToFront(FormActivator.SponsorListFormObj);
				return;
			case DataComboType.TenancyContract:
				FormActivator.TenancyContractListFormObj.Text = "Tenancy Contracts";
				FormActivator.TenancyContractListFormObj.ListType = DataComboType.TenancyContract;
				FormActivator.BringFormToFront(FormActivator.TenancyContractListFormObj);
				return;
			case DataComboType.TradeLicense:
				FormActivator.TradeLicenseListFormObj.Text = "Trade Licenses";
				FormActivator.TradeLicenseListFormObj.ListType = DataComboType.TradeLicense;
				FormActivator.BringFormToFront(FormActivator.TradeLicenseListFormObj);
				return;
			case DataComboType.FiscalYear:
				FormActivator.FiscalYearListFormObj.Text = "FiscalYear";
				FormActivator.FiscalYearListFormObj.ListType = DataComboType.FiscalYear;
				FormActivator.BringFormToFront(FormActivator.FiscalYearListFormObj);
				return;
			case DataComboType.VendorClass:
				FormActivator.VendorClassListFormObj.Text = "Vendor Classes";
				FormActivator.VendorClassListFormObj.ListType = DataComboType.VendorClass;
				FormActivator.BringFormToFront(FormActivator.VendorClassListFormObj);
				return;
			case DataComboType.VendorGroup:
				FormActivator.VendorGroupListFormObj.Text = "Vendor Groups";
				FormActivator.VendorGroupListFormObj.ListType = DataComboType.VendorGroup;
				FormActivator.BringFormToFront(FormActivator.VendorGroupListFormObj);
				return;
			case DataComboType.ContainerSize:
				FormActivator.ContainerSizeListFormObj.Text = "Container Size";
				FormActivator.ContainerSizeListFormObj.ListType = DataComboType.ContainerSize;
				FormActivator.BringFormToFront(FormActivator.ContainerSizeListFormObj);
				return;
			case DataComboType.INCO:
				FormActivator.INCODetailsListFormObj.Text = "Incoterm";
				FormActivator.INCODetailsListFormObj.ListType = DataComboType.INCO;
				FormActivator.BringFormToFront(FormActivator.INCODetailsListFormObj);
				return;
			case DataComboType.CustomerRelation:
				FormActivator.NationalAccountListFormObj.Text = "Customer Parent-Child Relation";
				FormActivator.NationalAccountListFormObj.ListType = DataComboType.CustomerRelation;
				FormActivator.BringFormToFront(FormActivator.NationalAccountListFormObj);
				return;
			case DataComboType.Visa:
				FormActivator.VisaListFormObj.Text = "Visas";
				FormActivator.VisaListFormObj.ListType = DataComboType.Visa;
				FormActivator.BringFormToFront(FormActivator.VisaListFormObj);
				return;
			case DataComboType.CostCenter:
				FormActivator.CostCenterListFormObj.Text = "Cost Centers";
				FormActivator.CostCenterListFormObj.ListType = DataComboType.CostCenter;
				FormActivator.BringFormToFront(FormActivator.CostCenterListFormObj);
				return;
			case DataComboType.ReturnedChequeReason:
				FormActivator.ReturnedChequeReasonListFormObj.Text = "Returned Cheque Reasons";
				FormActivator.ReturnedChequeReasonListFormObj.ListType = DataComboType.ReturnedChequeReason;
				FormActivator.BringFormToFront(FormActivator.ReturnedChequeReasonListFormObj);
				return;
			case DataComboType.Chequebook:
				FormActivator.ChequebookListFormObj.Text = "Chequebooks";
				FormActivator.ChequebookListFormObj.ListType = DataComboType.Chequebook;
				FormActivator.BringFormToFront(FormActivator.ChequebookListFormObj);
				return;
			case DataComboType.Register:
				FormActivator.RegisterListFormObj.Text = "Registers";
				FormActivator.RegisterListFormObj.ListType = DataComboType.Register;
				FormActivator.BringFormToFront(FormActivator.RegisterListFormObj);
				return;
			case DataComboType.Port:
				FormActivator.PortListFormObj.Text = "Ports";
				FormActivator.PortListFormObj.ListType = DataComboType.Port;
				FormActivator.BringFormToFront(FormActivator.PortListFormObj);
				return;
			case DataComboType.DisciplineActionType:
				FormActivator.DisciplineActionTypeListFormObj.Text = "Discipline Action Types";
				FormActivator.DisciplineActionTypeListFormObj.ListType = DataComboType.DisciplineActionType;
				FormActivator.BringFormToFront(FormActivator.DisciplineActionTypeListFormObj);
				return;
			case DataComboType.EmployeeActivityType:
				FormActivator.EmployeeActivityTypeListFormObj.Text = "Employee Activity Types";
				FormActivator.EmployeeActivityTypeListFormObj.ListType = DataComboType.EmployeeActivityType;
				FormActivator.BringFormToFront(FormActivator.EmployeeActivityTypeListFormObj);
				return;
			case DataComboType.User:
				FormActivator.UserListFormObj.Text = "Users List";
				FormActivator.UserListFormObj.ListType = DataComboType.User;
				FormActivator.BringFormToFront(FormActivator.UserListFormObj);
				return;
			case DataComboType.UserGroup:
				FormActivator.UserGroupListFormObj.Text = "User Group List";
				FormActivator.UserGroupListFormObj.ListType = DataComboType.UserGroup;
				FormActivator.BringFormToFront(FormActivator.UserGroupListFormObj);
				return;
			case DataComboType.VendorAddress:
				FormActivator.VendorAddressListFormObj.Text = "Vendor Addresses";
				FormActivator.VendorAddressListFormObj.ListType = DataComboType.VendorAddress;
				FormActivator.BringFormToFront(FormActivator.VendorAddressListFormObj);
				return;
			case DataComboType.EmployeeDocument:
				FormActivator.EmployeeDocumentListFormObj.Text = "Employee Documents";
				FormActivator.EmployeeDocumentListFormObj.ListType = DataComboType.EmployeeDocument;
				FormActivator.BringFormToFront(FormActivator.EmployeeDocumentListFormObj);
				return;
			case DataComboType.CustomerAddress:
				FormActivator.CustomerAddressListFormObj.Text = "Customer Addresses";
				FormActivator.CustomerAddressListFormObj.ListType = DataComboType.CustomerAddress;
				FormActivator.BringFormToFront(FormActivator.CustomerAddressListFormObj);
				return;
			case DataComboType.Driver:
				FormActivator.DriverListFormObj.Text = "Drivers";
				FormActivator.DriverListFormObj.ListType = DataComboType.Driver;
				FormActivator.BringFormToFront(FormActivator.DriverListFormObj);
				return;
			case DataComboType.Customer:
				FormActivator.CustomerListFormObj.Text = "Customers";
				return;
			case DataComboType.Vendor:
				FormActivator.VendorListFormObj.Text = "Vendors";
				return;
			case DataComboType.Employee:
				FormActivator.BringFormToFront(FormActivator.EmployeeListFormObj);
				return;
			case DataComboType.ExpenseCode:
				FormActivator.ExpenseCodeListFormObj.Text = "Expense Code";
				FormActivator.ExpenseCodeListFormObj.ListType = DataComboType.ExpenseCode;
				FormActivator.BringFormToFront(FormActivator.ExpenseCodeListFormObj);
				return;
			case DataComboType.FixedAsset:
				FormActivator.FixedAssetListFormObj.Text = "Fixed Assets";
				FormActivator.FixedAssetListFormObj.ListType = DataComboType.FixedAsset;
				FormActivator.BringFormToFront(FormActivator.FixedAssetListFormObj);
				return;
			case DataComboType.FixedAssetGroup:
				FormActivator.FixedAssetGroupListFormObj.Text = "Fixed Asset Groups";
				FormActivator.FixedAssetGroupListFormObj.ListType = DataComboType.FixedAssetGroup;
				FormActivator.BringFormToFront(FormActivator.FixedAssetGroupListFormObj);
				return;
			case DataComboType.FixedAssetLocation:
				FormActivator.FixedAssetLocationListFormObj.Text = "Fixed Asset Locations";
				FormActivator.FixedAssetLocationListFormObj.ListType = DataComboType.FixedAssetLocation;
				FormActivator.BringFormToFront(FormActivator.FixedAssetLocationListFormObj);
				return;
			case DataComboType.FixedAssetClass:
				FormActivator.FixedAssetClassListFormObj.Text = "Fixed Asset Classes";
				FormActivator.FixedAssetClassListFormObj.ListType = DataComboType.FixedAssetClass;
				FormActivator.BringFormToFront(FormActivator.FixedAssetClassListFormObj);
				return;
			case DataComboType.Job:
				FormActivator.JobListFormObj.Text = "Jobs";
				FormActivator.JobListFormObj.ListType = DataComboType.Job;
				FormActivator.BringFormToFront(FormActivator.JobListFormObj);
				return;
			case DataComboType.ClientAsset:
				FormActivator.ClientAssetListFormObj.Text = "ClientAsset";
				FormActivator.ClientAssetListFormObj.ListType = DataComboType.ClientAsset;
				FormActivator.BringFormToFront(FormActivator.ClientAssetListFormObj);
				return;
			case DataComboType.Opportunity:
				FormActivator.OpportunityListFormObj.Text = "Opportunities";
				FormActivator.OpportunityListFormObj.ListType = DataComboType.Opportunity;
				FormActivator.BringFormToFront(FormActivator.OpportunityListFormObj);
				return;
			case DataComboType.Competitor:
				FormActivator.CompetitorListFormObj.Text = "Competitors";
				FormActivator.CompetitorListFormObj.ListType = DataComboType.Competitor;
				FormActivator.BringFormToFront(FormActivator.CompetitorListFormObj);
				return;
			case DataComboType.Activity:
				FormActivator.ActivityListFormObj.Text = "Activities";
				FormActivator.ActivityListFormObj.ListType = DataComboType.Activity;
				FormActivator.BringFormToFront(FormActivator.ActivityListFormObj);
				return;
			case DataComboType.Campaign:
				FormActivator.CampaignListFormObj.Text = "Campaigns";
				FormActivator.CampaignListFormObj.ListType = DataComboType.Campaign;
				FormActivator.BringFormToFront(FormActivator.CampaignListFormObj);
				return;
			case DataComboType.Event:
				FormActivator.EventListFormObj.Text = "Events";
				FormActivator.EventListFormObj.ListType = DataComboType.Event;
				FormActivator.BringFormToFront(FormActivator.EventListFormObj);
				return;
			case DataComboType.BankFacilityGroup:
				FormActivator.BankFacilityGroupListFormObj.Text = "Bank Facility Group";
				FormActivator.BankFacilityGroupListFormObj.ListType = DataComboType.BankFacilityGroup;
				FormActivator.BringFormToFront(FormActivator.BankFacilityGroupListFormObj);
				return;
			case DataComboType.BankFacility:
				FormActivator.BankFacilityListFormObj.Text = "Bank Facility";
				FormActivator.BankFacilityListFormObj.ListType = DataComboType.BankFacility;
				FormActivator.BringFormToFront(FormActivator.BankFacilityListFormObj);
				return;
			case DataComboType.City:
				FormActivator.CityListFormObj.Text = "City";
				FormActivator.CityListFormObj.ListType = DataComboType.City;
				FormActivator.BringFormToFront(FormActivator.CityListFormObj);
				return;
			case DataComboType.JobFee:
				FormActivator.JobFeeListFormObj.Text = "Job Fee";
				FormActivator.JobFeeListFormObj.ListType = DataComboType.JobFee;
				FormActivator.BringFormToFront(FormActivator.JobFeeListFormObj);
				return;
			case DataComboType.Vehicle:
				FormActivator.VehicleListFormObj.Text = "Vehicle";
				FormActivator.VehicleListFormObj.ListType = DataComboType.Vehicle;
				FormActivator.BringFormToFront(FormActivator.VehicleListFormObj);
				return;
			case DataComboType.CustomerVendorLink:
				FormActivator.CustomerVendorListFormObj.Text = "Party";
				FormActivator.CustomerVendorListFormObj.ListType = DataComboType.CustomerVendorLink;
				FormActivator.BringFormToFront(FormActivator.CustomerVendorListFormObj);
				return;
			case DataComboType.Equipment:
				FormActivator.EquipmentListFormObj.Text = "Equipments";
				FormActivator.EquipmentListFormObj.ListType = DataComboType.Equipment;
				FormActivator.BringFormToFront(FormActivator.EquipmentListFormObj);
				return;
			case DataComboType.PriceList:
				FormActivator.PriceListFormObj.Text = "Price List";
				FormActivator.PriceListFormObj.ListType = DataComboType.PriceList;
				FormActivator.BringFormToFront(FormActivator.PriceListFormObj);
				return;
			case DataComboType.Candidate:
				FormActivator.CandidateListFormObj.Text = "Candidate List";
				FormActivator.CandidateListFormObj.ListType = DataComboType.Candidate;
				FormActivator.BringFormToFront(FormActivator.CandidateListFormObj);
				return;
			case DataComboType.Appointment:
				FormActivator.AppointmentListFormObj.Text = "Appointment List";
				FormActivator.AppointmentListFormObj.ListType = DataComboType.Appointment;
				FormActivator.BringFormToFront(FormActivator.AppointmentListFormObj);
				return;
			case DataComboType.WorkLocation:
				FormActivator.WorkLocationListFormObj.Text = "Work Locations";
				FormActivator.WorkLocationListFormObj.ListType = DataComboType.WorkLocation;
				FormActivator.BringFormToFront(FormActivator.WorkLocationListFormObj);
				return;
			case DataComboType.Transporter:
				FormActivator.TransporterListFormObj.Text = "Transporter List";
				FormActivator.TransporterListFormObj.ListType = DataComboType.Transporter;
				FormActivator.BringFormToFront(FormActivator.TransporterListFormObj);
				return;
			case DataComboType.Collateral:
				FormActivator.CollateralListFormObj.Text = "Collateral List";
				FormActivator.CollateralListFormObj.ListType = DataComboType.Collateral;
				FormActivator.BringFormToFront(FormActivator.CollateralListFormObj);
				return;
			case DataComboType.JobTask:
				FormActivator.JobTaskListFormObj.Text = "Job Task List";
				FormActivator.JobTaskListFormObj.ListType = DataComboType.JobTask;
				FormActivator.BringFormToFront(FormActivator.JobTaskListFormObj);
				return;
			case DataComboType.JobTaskGroup:
				FormActivator.JobTaskGroupListFormObj.Text = "Job Task Group List";
				FormActivator.JobTaskGroupListFormObj.ListType = DataComboType.JobTaskGroup;
				FormActivator.BringFormToFront(FormActivator.JobTaskGroupListFormObj);
				return;
			case DataComboType.CustomGadget:
				FormActivator.CustomGadgetListFormObj.Text = "Custom Gadgets";
				FormActivator.CustomGadgetListFormObj.ListType = DataComboType.CustomGadget;
				FormActivator.BringFormToFront(FormActivator.CustomGadgetListFormObj);
				return;
			case DataComboType.CustomReport:
				FormActivator.CustomReportListFormObj.Text = "Custom Reports";
				FormActivator.CustomReportListFormObj.ListType = DataComboType.CustomReport;
				FormActivator.BringFormToFront(FormActivator.CustomReportListFormObj);
				return;
			case DataComboType.Followup:
				FormActivator.followupDetailsListFormObj.Text = "Followup List";
				FormActivator.followupDetailsListFormObj.ListType = DataComboType.Followup;
				FormActivator.BringFormToFront(FormActivator.followupDetailsListFormObj);
				return;
			case DataComboType.LeadCategory:
				FormActivator.LeadCategoryListObj.Text = "Lead Category List";
				FormActivator.LeadCategoryListObj.ListType = DataComboType.LeadCategory;
				FormActivator.BringFormToFront(FormActivator.LeadCategoryListObj);
				return;
			case DataComboType.QualityTask:
				FormActivator.QualityTaskListFormObj.Text = "Quality Task";
				FormActivator.QualityTaskListFormObj.ListType = DataComboType.QualityTask;
				FormActivator.BringFormToFront(FormActivator.QualityTaskListFormObj);
				return;
			case DataComboType.ArrivalReportTemplate:
				FormActivator.ArrivalReportTemplateListFormObj.Text = "ArrivalReportTemplate";
				FormActivator.ArrivalReportTemplateListFormObj.ListType = DataComboType.ArrivalReportTemplate;
				FormActivator.BringFormToFront(FormActivator.ArrivalReportTemplateListFormObj);
				return;
			case DataComboType.Surveyor:
				FormActivator.SurveyorListFormObj.Text = "Surveyor";
				FormActivator.SurveyorListFormObj.ListType = DataComboType.Surveyor;
				FormActivator.BringFormToFront(FormActivator.SurveyorListFormObj);
				return;
			case DataComboType.EmployeeLoanType:
				FormActivator.EmployeeLoanTypeListFormObj.Text = "EmployeeLoanType";
				FormActivator.EmployeeLoanTypeListFormObj.ListType = DataComboType.EmployeeLoanType;
				FormActivator.BringFormToFront(FormActivator.EmployeeLoanTypeListFormObj);
				return;
			case DataComboType.EmployeeLeave:
				FormActivator.EmployeeLeaveListFormObj.Text = "EmployeeLeaveRequest";
				FormActivator.EmployeeLeaveListFormObj.ListType = DataComboType.EmployeeLeave;
				FormActivator.BringFormToFront(FormActivator.EmployeeLeaveListFormObj);
				return;
			case DataComboType.EmployeeLeaveResumption:
				FormActivator.EmployeeLeaveResumptionListFormObj.Text = "EmployeeLeaveResumption";
				FormActivator.EmployeeLeaveResumptionListFormObj.ListType = DataComboType.EmployeeLeaveResumption;
				FormActivator.BringFormToFront(FormActivator.EmployeeLeaveResumptionListFormObj);
				return;
			case DataComboType.PassportControl:
				FormActivator.EmployeePassportControlListFormObj.Text = "EmployeePassportControl";
				FormActivator.EmployeePassportControlListFormObj.ListType = DataComboType.PassportControl;
				FormActivator.BringFormToFront(FormActivator.EmployeePassportControlListFormObj);
				return;
			case DataComboType.AdjustmentType:
				FormActivator.AdjustmentTypeDetailsListFormObj.Text = "AdjustmentTypeDetails";
				FormActivator.AdjustmentTypeDetailsListFormObj.ListType = DataComboType.AdjustmentType;
				FormActivator.BringFormToFront(FormActivator.AdjustmentTypeDetailsListFormObj);
				return;
			case DataComboType.JobBOM:
				FormActivator.JobBOMListFormObj.Text = "JobBOM";
				FormActivator.JobBOMListFormObj.ListType = DataComboType.JobBOM;
				FormActivator.BringFormToFront(FormActivator.JobBOMListFormObj);
				return;
			case DataComboType.Package:
				FormActivator.PackageListFormObj.Text = "Package";
				FormActivator.PackageListFormObj.ListType = DataComboType.Package;
				FormActivator.BringFormToFront(FormActivator.PackageListFormObj);
				return;
			case DataComboType.PropertyAgent:
				FormActivator.PropertyAgentListFormObj.Text = "Property Agent";
				FormActivator.PropertyAgentListFormObj.ListType = DataComboType.PropertyAgent;
				FormActivator.BringFormToFront(FormActivator.PropertyAgentListFormObj);
				return;
			case DataComboType.PropertyClass:
				FormActivator.PropertyClassListFormObj.Text = "PropertyClass";
				FormActivator.PropertyClassListFormObj.ListType = DataComboType.PropertyClass;
				FormActivator.BringFormToFront(FormActivator.PropertyClassListFormObj);
				return;
			case DataComboType.Property:
				FormActivator.PropertyListFormObj.Text = "Property";
				FormActivator.PropertyListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.PropertyListFormObj);
				return;
			case DataComboType.PropertyUnit:
				FormActivator.PropertyUnitListFormObj.Text = "Property Unit";
				FormActivator.PropertyUnitListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.PropertyUnitListFormObj);
				return;
			case DataComboType.PropertyVirtualUnit:
				FormActivator.PropertyVirtualUnitListFormObj.Text = "Property Virtual Unit";
				FormActivator.PropertyVirtualUnitListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.PropertyVirtualUnitListFormObj);
				return;
			case DataComboType.PropertyIncomeCode:
				FormActivator.PropertyIncomeCodeListFormObj.Text = "Property Income Code";
				FormActivator.PropertyIncomeCodeListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.PropertyIncomeCodeListFormObj);
				return;
			case DataComboType.Tenant:
				FormActivator.PropertyTenantListFormObj.Text = "Tenants";
				FormActivator.PropertyTenantListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.PropertyTenantListFormObj);
				return;
			case DataComboType.InventoryTransferType:
				FormActivator.InventoryTransferTypeListFormObj.Text = "InventoryTransfer Type";
				FormActivator.InventoryTransferTypeListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.InventoryTransferTypeListFormObj);
				return;
			case DataComboType.ReleaseType:
				FormActivator.ReleaseTypeListFormObj.Text = "Release Type";
				FormActivator.ReleaseTypeListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.ReleaseTypeListFormObj);
				return;
			}
			switch (type)
			{
			case DataComboType.ServiceProvider:
				FormActivator.ServiceProviderListFormObj.Text = "Service Provider";
				FormActivator.ServiceProviderListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.ServiceProviderListFormObj);
				return;
			case DataComboType.Approval:
				FormActivator.ApprovalDetailsListFormObj.Text = "Approvals";
				FormActivator.ApprovalDetailsListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.ApprovalDetailsListFormObj);
				return;
			case DataComboType.Verification:
				FormActivator.VerificationDetailsListFormObj.Text = "Verifications";
				FormActivator.VerificationDetailsListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.VerificationDetailsListFormObj);
				return;
			case DataComboType.Bin:
				FormActivator.BinDetailsListFormObj.Text = "Bins";
				FormActivator.BinDetailsListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.BinDetailsListFormObj);
				return;
			case DataComboType.Route:
				FormActivator.RouteDetailsListFormObj.Text = "Routes";
				FormActivator.RouteDetailsListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.RouteDetailsListFormObj);
				return;
			case DataComboType.RouteGroup:
				FormActivator.RouteGroupDetailsListFormObj.Text = "RouteGroups";
				FormActivator.RouteGroupDetailsListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.RouteGroupDetailsListFormObj);
				return;
			case DataComboType.ServiceActivity:
				FormActivator.ServiceActivityListFormObj.Text = "Service Activity";
				FormActivator.ServiceActivityListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.ServiceActivityListFormObj);
				return;
			case DataComboType.RiderSummary:
				FormActivator.RiderSummaryListFormObj.Text = "Rider Summary";
				FormActivator.RiderSummaryListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.RiderSummaryListFormObj);
				return;
			case DataComboType.HorseSummary:
				FormActivator.HorseSummaryListFormObj.Text = "Horse Summary";
				FormActivator.HorseSummaryListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.HorseSummaryListFormObj);
				return;
			case DataComboType.HorseType:
				FormActivator.HorseTypeListFormObj.Text = "Horse Type";
				FormActivator.HorseTypeListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.HorseTypeListFormObj);
				return;
			case DataComboType.EquipmentCategory:
				FormActivator.EquipmentCategoryListFormObj.Text = "Equipment Category List";
				FormActivator.EquipmentCategoryListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.EquipmentCategoryListFormObj);
				return;
			case DataComboType.EquipmentType:
				FormActivator.EquipmentTypeListFormObj.Text = "Equipment Type List";
				FormActivator.EquipmentTypeListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.EquipmentTypeListFormObj);
				return;
			case DataComboType.HorseSex:
				FormActivator.HorseSexListFormObj.Text = "Horse Sex";
				FormActivator.HorseSexListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.HorseSexListFormObj);
				return;
			case DataComboType.HolidayCalendar:
				FormActivator.HolidayCalendarListFormObj.Text = "Holiday Calendar";
				FormActivator.HolidayCalendarListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.HolidayCalendarListFormObj);
				return;
			case DataComboType.OverTime:
				FormActivator.OverTimeDetailsListFormObj.Text = "OverTime Details List";
				FormActivator.OverTimeDetailsListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.OverTimeDetailsListFormObj);
				return;
			case DataComboType.CaseParty:
				FormActivator.CasePartyListFormObj.Text = "CaseParty";
				FormActivator.CasePartyListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.CasePartyListFormObj);
				return;
			case DataComboType.Lawyer:
				FormActivator.LawyerListFormObj.Text = "Lawyer";
				FormActivator.LawyerListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.LawyerListFormObj);
				return;
			case DataComboType.LegalActionStatus:
				FormActivator.LegalActionStatusListFormObj.Text = "Legal Action Status";
				FormActivator.LegalActionStatusListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.LegalActionStatusListFormObj);
				return;
			case DataComboType.Tax:
				FormActivator.TaxListFormObj.Text = "Tax";
				FormActivator.TaxListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.TaxListFormObj);
				return;
			case DataComboType.TaxGroup:
				FormActivator.TaxGroupListFormObj.Text = "Tax Group";
				FormActivator.TaxGroupListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.TaxGroupListFormObj);
				return;
			case DataComboType.SysDoc:
				FormActivator.SysDocListFormObj.Text = "Sytem Documents";
				FormActivator.SysDocListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.SysDocListFormObj);
				return;
			case DataComboType.CaseClient:
				FormActivator.CaseClientListFormObj.Text = "Case Client Details";
				FormActivator.CaseClientListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.CaseClientListFormObj);
				return;
			case DataComboType.EAEquipment:
				FormActivator.EAEquipmentListFormObj.Text = "Enterprise Asset Equipment";
				FormActivator.EAEquipmentListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.EAEquipmentListFormObj);
				return;
			case DataComboType.RequisitionType:
				FormActivator.RequisitionTypeListFormObj.Text = "Requisition Type";
				FormActivator.RequisitionTypeListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.RequisitionTypeListFormObj);
				return;
			case DataComboType.ProductMake:
				FormActivator.ProductMakeListFormObj.Text = "Product Make";
				FormActivator.ProductMakeListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.ProductMakeListFormObj);
				return;
			case DataComboType.ProductType:
				FormActivator.ProductTypeListFormObj.Text = "Product Type";
				FormActivator.ProductTypeListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.ProductTypeListFormObj);
				return;
			case DataComboType.ProductModel:
				FormActivator.ProductModelListFormObj.Text = "Product Model";
				FormActivator.ProductModelListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.ProductModelListFormObj);
				return;
			case DataComboType.TaskSteps:
				FormActivator.TaskStepsListFormObj.Text = "Task Steps";
				FormActivator.TaskStepsListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.TaskStepsListFormObj);
				return;
			case DataComboType.TaskType:
				FormActivator.TaskTypeListFormObj.Text = "Task Types";
				FormActivator.TaskTypeListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.TaskTypeListFormObj);
				return;
			case DataComboType.Rack:
				FormActivator.RackListFormObj.Text = "Racks";
				FormActivator.RackListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.RackListFormObj);
				return;
			case DataComboType.EmployeeAbsconding:
				FormActivator.EmployeeAbscondingListFormObj.Text = "Employee Absconding Entry";
				FormActivator.EmployeeAbscondingListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.EmployeeAbscondingListFormObj);
				return;
			case DataComboType.PrintTemplateMap:
				FormActivator.PrintTemplateMapListFormObj.Text = "Print Template Map";
				FormActivator.PrintTemplateMapListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.PrintTemplateMapListFormObj);
				return;
			case DataComboType.Patient:
				FormActivator.PatientListFormObj.Text = "Patients";
				FormActivator.PatientListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.PatientListFormObj);
				return;
			case DataComboType.PatientDocType:
				FormActivator.PatientDocTypeListFormObj.Text = "Document Types";
				FormActivator.PatientDocTypeListFormObj.ListType = DataComboType.PatientDocType;
				FormActivator.BringFormToFront(FormActivator.PatientDocTypeListFormObj);
				return;
			case DataComboType.DataSync:
				FormActivator.DataSyncListFormObj.Text = "Data Sync";
				FormActivator.DataSyncListFormObj.ListType = DataComboType.DataSync;
				FormActivator.BringFormToFront(FormActivator.DataSyncListFormObj);
				return;
			case DataComboType.SalesReturnReason:
				FormActivator.GenericListFormObj.Text = "Sales Return Reason";
				FormActivator.GenericListFormObj.ListType = DataComboType.SalesReturnReason;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			case DataComboType.CollateralType:
				FormActivator.GenericListFormObj.Text = "Collateral Type";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			case DataComboType.Industry:
				FormActivator.GenericListFormObj.Text = "Industry";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			case DataComboType.Language:
				FormActivator.GenericListFormObj.Text = "Language";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			case DataComboType.LeadSource:
				FormActivator.GenericListFormObj.Text = "Lead Source";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			case DataComboType.LeadStatus:
				FormActivator.GenericListFormObj.Text = "Lead Status";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			case DataComboType.LegalStatus:
				FormActivator.GenericListFormObj.Text = "Legal Status";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			case DataComboType.Qualification:
				FormActivator.GenericListFormObj.Text = "Qualification";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			case DataComboType.VehicleType:
				FormActivator.GenericListFormObj.Text = "Vehicle Type";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			case DataComboType.PropertyUnitType:
				FormActivator.GenericListFormObj.Text = "Property Unit Type";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			case DataComboType.PropertyFacility:
				FormActivator.GenericListFormObj.Text = "Property Facility";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			case DataComboType.PropertyView:
				FormActivator.GenericListFormObj.Text = "Property View";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			case DataComboType.PropertyOwner:
				FormActivator.GenericListFormObj.Text = "Property Owner";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			case DataComboType.PropertyCategory:
				FormActivator.GenericListFormObj.Text = "Property Category";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			case DataComboType.EntityCategory:
				FormActivator.GenericListFormObj.Text = "Entity Category";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			case DataComboType.PropertyServiceType:
				FormActivator.GenericListFormObj.Text = "Property ServiceType";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			case DataComboType.FixedAssetCompany:
				FormActivator.GenericListFormObj.Text = "FixedAsset Company";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			case DataComboType.MedicalInsuranceCategory:
				FormActivator.GenericListFormObj.Text = "Medical Insurance Category";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			case DataComboType.HorseOwnershipType:
				FormActivator.GenericListFormObj.Text = "Horse Ownership Type";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			case DataComboType.HorseCategory:
				FormActivator.GenericListFormObj.Text = "Horse Category";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			case DataComboType.CaseType:
				FormActivator.GenericListFormObj.Text = "Case Type";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				return;
			}
			switch (type)
			{
			case DataComboType.LegalStatus:
				FormActivator.GenericListFormObj.Text = "Legal Status";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.CRMActivityReason:
				FormActivator.GenericListFormObj.Text = "Activity Reason";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.CRMCustomerActivity:
				FormActivator.GenericListFormObj.Text = "Customer Activity";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.ContainerType:
				FormActivator.GenericListFormObj.Text = "Container Type";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.PartsMakeType:
				FormActivator.GenericListFormObj.Text = "Parts Make Type";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.PartsType:
				FormActivator.GenericListFormObj.Text = "Parts Type";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.PartsFamily:
				FormActivator.GenericListFormObj.Text = "Parts Family";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.VehicleMake:
				FormActivator.GenericListFormObj.Text = "Vehicle Make";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.VehicleModel:
				FormActivator.GenericListFormObj.Text = "Vehicle Model";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.ShippingCompany:
				FormActivator.GenericListFormObj.Text = "Shipping Company";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.InsuranceProvider:
				FormActivator.GenericListFormObj.Text = "Insurance Provider";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.Material:
				FormActivator.GenericListFormObj.Text = "Product Material";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.Finish:
				FormActivator.GenericListFormObj.Text = "Product Finish";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.Color:
				FormActivator.GenericListFormObj.Text = "Product Color";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.ProductGrade:
				FormActivator.GenericListFormObj.Text = "Product Grade";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.Standard:
				FormActivator.GenericListFormObj.Text = "Product Standard";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.TransactionDoc:
				FormActivator.GenericListFormObj.Text = "SalesPerson Group";
				FormActivator.GenericListFormObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.VendorCategory:
				FormActivator.VendorCategoryListObj.Text = "Vendor Category List";
				FormActivator.VendorCategoryListObj.ListType = DataComboType.VendorCategory;
				FormActivator.BringFormToFront(FormActivator.VendorCategoryListObj);
				break;
			case DataComboType.KitchenType:
				FormActivator.GenericListFormObj.Text = "Kitchen Type";
				FormActivator.GenericListFormObj.ListType = DataComboType.KitchenType;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.Stage:
				FormActivator.GenericListFormObj.Text = "Stage";
				FormActivator.GenericListFormObj.ListType = DataComboType.Stage;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.Chronics:
				FormActivator.GenericListFormObj.Text = "Chronics";
				FormActivator.GenericListFormObj.ListType = DataComboType.Chronics;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.Allergy:
				FormActivator.GenericListFormObj.Text = "Allergy";
				FormActivator.GenericListFormObj.ListType = DataComboType.Allergy;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.CollateralCustodian:
				FormActivator.GenericListFormObj.Text = "CollateralCustodian";
				FormActivator.GenericListFormObj.ListType = DataComboType.CollateralCustodian;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.LegalPosition:
				FormActivator.GenericListFormObj.Text = "LegalPosition";
				FormActivator.GenericListFormObj.ListType = DataComboType.LegalPosition;
				FormActivator.BringFormToFront(FormActivator.GenericListFormObj);
				break;
			case DataComboType.ProductType1:
			case DataComboType.ProductType2:
			case DataComboType.ProductType3:
			case DataComboType.ProductType4:
			case DataComboType.ProductType5:
			case DataComboType.ProductType6:
			case DataComboType.ProductType7:
			case DataComboType.ProductType8:
				FormActivator.GenericProductTypeListObj.Text = "Generic Product Type";
				FormActivator.GenericProductTypeListObj.ListType = type;
				FormActivator.BringFormToFront(FormActivator.GenericProductTypeListObj);
				break;
			default:
				ErrorHelper.ErrorMessage("'ShowList' method is not implemented for " + type.ToString());
				break;
			}
		}

		public DataTable GetDataForms()
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("FormName", typeof(string));
			dataTable.Columns.Add("FormText", typeof(string));
			dataTable.Columns.Add("FormCategory", typeof(string));
			dataTable.Columns.Add("FormSubCategory", typeof(string));
			dataTable.Rows.Add("CashExpenseEntryForm", "Cash Expense Entry", "Account", "Account");
			dataTable.Rows.Add("CashPaymentForm", "Cash Payment", "Account", "Account");
			dataTable.Rows.Add("CashReceiptForm", "Cash Receipt", "Account", "Account");
			dataTable.Rows.Add("ChequebookDetailsForm", "Chequebook Details", "Account", "Account");
			dataTable.Rows.Add("ChequeDepositForm", "Cheque Deposit", "Account", "Account");
			dataTable.Rows.Add("ChequeExpenseEntryForm", "Cheque Expense Entry", "Account", "Account");
			dataTable.Rows.Add("ChequePaymentForm", "Cheque Payment", "Account", "Account");
			dataTable.Rows.Add("ChequeReceiptForm", "Cheque Receipt", "Account", "Account");
			dataTable.Rows.Add("ChequeRegisterForm", "Cheque Register", "Account", "Account");
			dataTable.Rows.Add("ChequeReturnForm", "Cheque Return", "Account", "Account");
			dataTable.Rows.Add("CompanyAccountDetailsForm", "Account Details", "Account", "Account");
			dataTable.Rows.Add("CompanyAccountsListForm", "Accounts List", "Account", "Account");
			dataTable.Rows.Add("CreditNoteEntryForm", "Credit Note Entry", "Account", "Account");
			dataTable.Rows.Add("DebitNoteEntryForm", "Debit Note Entry", "Account", "Account");
			dataTable.Rows.Add("FundTransferForm", "Fund Transfer", "Account", "Account");
			dataTable.Rows.Add("IssuedChequeCancellationForm", "Issued Cheque Cancellation", "Account", "Account");
			dataTable.Rows.Add("IssuedChequeClearanceForm", "Issued Cheque Clearance", "Account", "Account");
			dataTable.Rows.Add("IssuedChequeReturnForm", "Issued Cheque Return", "Account", "Account");
			dataTable.Rows.Add("JournalEntryForm", "Journal Entry", "Account", "Account");
			dataTable.Rows.Add("ReceivedChequeCancellationForm", "Received Cheque Cancellation", "Account", "Account");
			dataTable.Rows.Add("ReceivedChequeClearanceForm", "Received Cheque Clearance", "Account", "Account");
			dataTable.Rows.Add("SecurityChequeForm", "Security Cheque", "Account", "Account");
			dataTable.Rows.Add("VoidBlankChequeForm", "Void Blank Cheque", "Account", "Account");
			dataTable.Rows.Add("PrintChequeForm", "Print Cheque", "Account", "Account");
			dataTable.Rows.Add("CustomerAddressDetailsForm", "Customer Address Details", "Customer", "Customer");
			dataTable.Rows.Add("CustomerDetailsForm", "Customer Details", "Customer", "Customer");
			dataTable.Rows.Add("DeliveryNoteForm", "Delivery Note", "Customer", "Customer");
			dataTable.Rows.Add("DeliveryReturnForm", "Delivery Return", "Customer", "Customer");
			dataTable.Rows.Add("SalesInvoiceForm", "Sales Invoice", "Customer", "Customer");
			dataTable.Rows.Add("SalesEnquiryForm", "Sales Enquiry", "Customer", "Customer");
			dataTable.Rows.Add("SalesOrderForm", "Sales Order", "Customer", "Customer");
			dataTable.Rows.Add("SalesProformaInvoiceForm", "Sales Proforma", "Customer", "Customer");
			dataTable.Rows.Add("SalespersonDetailsForm", "Salesperson Details", "Customer", "Customer");
			dataTable.Rows.Add("SalesQuoteForm", "Sales Quote", "Customer", "Customer");
			dataTable.Rows.Add("SalesReceiptForm", "Sales Receipt", "Customer", "Customer");
			dataTable.Rows.Add("SalesReturnCashForm", "Sales Return - Cash", "Customer", "Customer");
			dataTable.Rows.Add("SalesReturnCreditForm", "Sales Return - Credit", "Customer", "Customer");
			dataTable.Rows.Add("ExportSalesInvoiceForm", "Export Sales Invoice", "Customer", "Customer");
			dataTable.Rows.Add("ExportDeliveryNoteForm", "Export Delivery Note", "Customer", "Customer");
			dataTable.Rows.Add("ExportSalesOrderForm", "Export Sales Order", "Customer", "Customer");
			dataTable.Rows.Add("ConsignLocationDetailsForm", "Consignment Location Details", "Customer", "Customer");
			dataTable.Rows.Add("ConsignOutForm", "Consignment Out", "Customer", "Customer");
			dataTable.Rows.Add("ConsignOutReturnForm", "Consignment Out Return", "Customer", "Customer");
			dataTable.Rows.Add("ConsignOutSettlementForm", "Consignment Out Settlement", "Customer", "Customer");
			dataTable.Rows.Add("CustomerAgingListForm", "Customer Aging List", "Customer", "Customer");
			dataTable.Rows.Add("CustomerCategoryDetailsForm", "Customer Category Details", "Customer", "Customer");
			dataTable.Rows.Add("CustomerLedgerForm", "Customer Ledger", "Customer", "Customer");
			dataTable.Rows.Add("CLVoucherForm", "Credit Limit Voucher", "Customer", "Customer");
			dataTable.Rows.Add("SalesInvoiceNonInvForm", "Sales Invoice(Non Inventory)", "Customer", "Customer");
			dataTable.Rows.Add("BuyerDetailsForm", "Buyer Details", "Vendor", "Vendor");
			dataTable.Rows.Add("CashPurchaseForm", "Cash Purchase", "Vendor", "Vendor");
			dataTable.Rows.Add("POShipmentForm", "PO Shipment", "Vendor", "Vendor");
			dataTable.Rows.Add("PurchaseInvoiceForm", "Purchase Invoice", "Vendor", "Vendor");
			dataTable.Rows.Add("PurchaseOrderForm", "Purchase Order", "Vendor", "Vendor");
			dataTable.Rows.Add("PurchaseQuoteForm", "Purchase Quote", "Vendor", "Vendor");
			dataTable.Rows.Add("PurchaseGRNForm", "Goods Received Note", "Vendor", "Vendor");
			dataTable.Rows.Add("ImportPurchaseGRNForm", "Import Goods Received Note", "Vendor", "Vendor");
			dataTable.Rows.Add("PurchaseOrderImportForm", "Import Purchase Order", "Vendor", "Vendor");
			dataTable.Rows.Add("PurchaseInvoiceImportForm", "Import Purchase Order", "Vendor", "Vendor");
			dataTable.Rows.Add("ProformaInvoiceForm", "Proforma Invoice", "Vendor", "Vendor");
			dataTable.Rows.Add("PurchaseReturnCashForm", "Purchase Return - Cash", "Vendor", "Vendor");
			dataTable.Rows.Add("PurchaseReturnCreditForm", "Purchase Return - Credit", "Vendor", "Vendor");
			dataTable.Rows.Add("VendorAddressDetailsForm", "Vendor Address Details", "Vendor", "Vendor");
			dataTable.Rows.Add("VendorDetailsForm", "Vendor Details", "Vendor", "Vendor");
			dataTable.Rows.Add("GRNReturnForm", "GRN Return", "Vendor", "Vendor");
			dataTable.Rows.Add("PurchaseCostEntryForm", "Purchase Cost Entry", "Vendor", "Vendor");
			dataTable.Rows.Add("DirectInventoryTransferForm", "Direct Inventory Transfer", "Inventory", "Inventory");
			dataTable.Rows.Add("InventoryAdjustmentsForm", "Inventory Adjustments", "Inventory", "Inventory");
			dataTable.Rows.Add("InventoryTransferAcceptanceForm", "Inventory Transfer Acceptance", "Inventory", "Inventory");
			dataTable.Rows.Add("InventoryTransferForm", "Inventory Transfer", "Inventory", "Inventory");
			dataTable.Rows.Add("InventoryTransferReturnForm", "Inventory Transfer Return", "Inventory", "Inventory");
			dataTable.Rows.Add("ProductDetailsForm", "Product Details", "Inventory", "Inventory");
			dataTable.Rows.Add("MatrixProductDetailsForm", "Matrix Product Details", "Inventory", "Inventory");
			dataTable.Rows.Add("DimensionDetailsForm", "Matrix Dimension Details", "Inventory", "Inventory");
			dataTable.Rows.Add("ProductListForm", "Product List", "Inventory", "Inventory");
			dataTable.Rows.Add("MatrixTemplateDetailsForm", "Matrix Template Detail", "Inventory", "Inventory");
			dataTable.Rows.Add("ProductAttributeDetailsForm", "Product Attribute Details", "Inventory", "Inventory");
			dataTable.Rows.Add("EmployeeAddressDetailsForm", "Employee Address Details", "HR", "HR");
			dataTable.Rows.Add("EmployeeDependentDetailsForm", "Employee Dependent Details", "HR", "HR");
			dataTable.Rows.Add("EmployeeDetailsForm", "Employee Details", "HR", "HR");
			dataTable.Rows.Add("EmployeeDocumentsForm", "Employee Documents", "HR", "HR");
			dataTable.Rows.Add("EmployeeLeaveDetailForm", "Employee Leave Detail", "HR", "HR");
			dataTable.Rows.Add("EmployeeSalaryDetailForm", "Employee Salary Detail", "HR", "HR");
			dataTable.Rows.Add("ArrivalReportForm", "Arrival Report", "QC", "QC");
			dataTable.Rows.Add("QualityClaimForm", "Quality Claim", "QC", "QC");
			dataTable.Rows.Add("QualityTaskForm", "QC Task", "QC", "QC");
			dataTable.Rows.Add("GLReportForm", "GL Report", "Report", "Account");
			dataTable.Rows.Add("JournalReportForm", "Journal", "Report", "Account");
			dataTable.Rows.Add("CustomerBalanceDetailsReport", "Customer Balance Details", "Report", "Customer");
			dataTable.Rows.Add("CustomerBalanceSummaryReport", "Customer Balance Summary", "Report", "Customer");
			dataTable.Rows.Add("VendorBalanceDetailsReport", "Vendor Balance Details", "Report", "Vendor");
			dataTable.Rows.Add("VendorBalanceSummaryReport", "Vendor Balance Summary", "Report", "Vendor");
			return dataTable;
		}

		public DataSet GetSecurityFormList()
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("ScreenArea");
			dataTable.Columns.Add("ScreenID", typeof(string));
			dataTable.Columns.Add("ScreenName", typeof(string));
			dataTable.Columns.Add("View", typeof(bool)).DefaultValue = false;
			dataTable.Columns.Add("New", typeof(bool)).DefaultValue = false;
			dataTable.Columns.Add("Edit", typeof(bool)).DefaultValue = false;
			dataTable.Columns.Add("Delete", typeof(bool)).DefaultValue = false;
			DataTable dataTable2 = dataSet.Tables.Add("ScreenSubArea");
			dataTable2.Columns.Add("ScreenID", typeof(string));
			dataTable2.Columns.Add("ScreenName", typeof(string));
			dataTable2.Columns.Add("ParentID", typeof(string));
			dataTable2.Columns.Add("View", typeof(bool)).DefaultValue = false;
			dataTable2.Columns.Add("New", typeof(bool)).DefaultValue = false;
			dataTable2.Columns.Add("Edit", typeof(bool)).DefaultValue = false;
			dataTable2.Columns.Add("Delete", typeof(bool)).DefaultValue = false;
			DataTable dataTable3 = dataSet.Tables.Add("Screen");
			dataTable3.Columns.Add("ScreenID", typeof(string));
			dataTable3.Columns.Add("ScreenName", typeof(string));
			dataTable3.Columns.Add("ScreenArea", typeof(string));
			dataTable3.Columns.Add("ScreenType", typeof(string));
			dataTable3.Columns.Add("ScreenSubArea", typeof(string));
			dataTable3.Columns.Add("View", typeof(bool)).DefaultValue = false;
			dataTable3.Columns.Add("New", typeof(bool)).DefaultValue = false;
			dataTable3.Columns.Add("Edit", typeof(bool)).DefaultValue = false;
			dataTable3.Columns.Add("Delete", typeof(bool)).DefaultValue = false;
			dataTable.Rows.Add(ScreenAreas.Accounts.ToString(), "Accounts");
			dataTable.Rows.Add(ScreenAreas.Sales.ToString(), "Customer & Sales");
			dataTable.Rows.Add(ScreenAreas.Purchases.ToString(), "Vendor & Purchasing");
			dataTable.Rows.Add(ScreenAreas.Products.ToString(), "Inventory");
			if (GlobalRules.IsFeatureAllowed(EditionFeatures.HRPayroll))
			{
				dataTable.Rows.Add(ScreenAreas.HR.ToString(), "HR");
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.CRM))
			{
				dataTable.Rows.Add(ScreenAreas.CRM.ToString(), "CRM");
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.ProjectAccounting))
			{
				dataTable.Rows.Add(ScreenAreas.Project.ToString(), "Project");
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.QC))
			{
				dataTable.Rows.Add(ScreenAreas.QC.ToString(), "Quality Control");
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.PropertyRental))
			{
				dataTable.Rows.Add(ScreenAreas.PropertyRental.ToString(), "Property Rental");
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.AdvancedManufacturing))
			{
				dataTable.Rows.Add(ScreenAreas.Manufacturing.ToString(), "Manufacturing");
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.POS))
			{
				dataTable.Rows.Add(ScreenAreas.POS.ToString(), "POS");
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.OCR))
			{
				dataTable.Rows.Add(ScreenAreas.General.ToString(), "OCR Scanning");
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.Garment))
			{
				dataTable.Rows.Add(ScreenAreas.Garment.ToString(), "Garment");
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.Vehicle))
			{
				dataTable.Rows.Add(ScreenAreas.Vehicle.ToString(), "Vehicle");
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.Horse))
			{
				dataTable.Rows.Add(ScreenAreas.Horse.ToString(), "Horse");
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.EnterpriseAsset))
			{
				dataTable.Rows.Add(ScreenAreas.EnterpriseAsset.ToString(), "Enterprise Asset");
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.Legal))
			{
				dataTable.Rows.Add(ScreenAreas.Legal.ToString(), "Legal");
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.Medical))
			{
				dataTable.Rows.Add(ScreenAreas.Medical.ToString(), "Medical");
			}
			if (GlobalRules.IsFeatureAllowed(EditionFeatures.FixedAsset))
			{
				dataTable.Rows.Add(ScreenAreas.FixedAsset.ToString(), "Fixed Asset");
			}
			dataTable.Rows.Add(ScreenAreas.General.ToString(), "General");
			dataTable.Rows.Add(ScreenAreas.System.ToString(), "System");
			dataTable.Rows.Add(ScreenAreas.Reports.ToString(), "Reports");
			dataTable2.Rows.Add(ScreenAreas.ReportsAccounts.ToString(), "Accounts", ScreenAreas.Reports.ToString());
			dataTable2.Rows.Add(ScreenAreas.ReportsCustomer.ToString(), "Customers", ScreenAreas.Reports.ToString());
			dataTable2.Rows.Add(ScreenAreas.ReportsSales.ToString(), "Sales", ScreenAreas.Reports.ToString());
			dataTable2.Rows.Add(ScreenAreas.ReportsVendor.ToString(), "Vendors", ScreenAreas.Reports.ToString());
			dataTable2.Rows.Add(ScreenAreas.ReportsPurchase.ToString(), "Purchase", ScreenAreas.Reports.ToString());
			dataTable2.Rows.Add(ScreenAreas.ReportsInventory.ToString(), "Inventory", ScreenAreas.Reports.ToString());
			dataTable2.Rows.Add(ScreenAreas.ReportsHR.ToString(), "HR", ScreenAreas.Reports.ToString());
			dataTable2.Rows.Add(ScreenAreas.ReportsCRM.ToString(), "CRM", ScreenAreas.Reports.ToString());
			dataTable2.Rows.Add(ScreenAreas.ReportsConsignIn.ToString(), "Consignment In", ScreenAreas.Reports.ToString());
			dataTable2.Rows.Add(ScreenAreas.ReportsConsignOut.ToString(), "Consignment Out", ScreenAreas.Reports.ToString());
			dataTable2.Rows.Add(ScreenAreas.ReportsFixedAsset.ToString(), "Fixed Asset", ScreenAreas.Reports.ToString());
			dataTable2.Rows.Add(ScreenAreas.ReportsProject.ToString(), "Project", ScreenAreas.Reports.ToString());
			dataTable2.Rows.Add(ScreenAreas.ReportsManufacturing.ToString(), "Manufacturing", ScreenAreas.Reports.ToString());
			dataTable2.Rows.Add(ScreenAreas.ReportsCustom.ToString(), "Custom", ScreenAreas.Reports.ToString());
			dataTable2.Rows.Add(ScreenAreas.ReportsEnterprise.ToString(), "Enterprise Asset", ScreenAreas.Reports.ToString());
			dataTable2.Rows.Add(ScreenAreas.ReportsLegal.ToString(), "Legal", ScreenAreas.Reports.ToString());
			dataTable2.Rows.Add(ScreenAreas.ReportsPropertyRental.ToString(), "Property Rental", ScreenAreas.Reports.ToString());
			dataTable3.Rows.Add("POSLocationDetailsForm", "POS Location Detail", ScreenAreas.POS.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("POSCashRegisterDetailsForm", "POS Cash Register Detail", ScreenAreas.POS.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("ChangeCashRegisterForm", "Change Cash Register", ScreenAreas.POS.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("formPOSHome", "POS Client", ScreenAreas.POS.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("CustomerAddressDetailsForm", "Customer Address Detail", ScreenAreas.Sales.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("CustomerClassDetailsForm", "Customer Class", ScreenAreas.Sales.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("CustomerDetailsForm", "Customer Detail", ScreenAreas.Sales.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("CustomerGroupDetailsForm", "Customer Group Details", ScreenAreas.Sales.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("SalespersonDetailsForm", "Salesperson Details", ScreenAreas.Sales.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("ConsignLocationDetailsForm", "Consignment Location Details", ScreenAreas.Sales.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("CustomerCategoryDetailsForm", "Customer Category Details", ScreenAreas.Sales.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("ActivityDetailsForm", "Customer Activity Details", ScreenAreas.Sales.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("SalesReturnReasonForm", "Sales Return Reason", ScreenAreas.Sales.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("CustomerPaymentAllocationForm", "Payment Allocation", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("CustomerStatementForm", "Customer Statement", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("DeliveryNoteForm", "Delivery Note", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("DeliveryReturnForm", "Delivery Return", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("SalesInvoiceForm", "Sales Invoice", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("SalesInvoiceNonInvForm", "Sales Invoice(Non Inventory)", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ExportSalesInvoiceForm", "Export Sales Invoice", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ExportPackingListForm", "Packing List", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("SalesEnquiryForm", "Sales Enquiry", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("SalesOrderForm", "Sales Order", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("SalesProformaInvoiceForm", "Sales Proforma", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("SalesQuoteForm", "Sales Quote", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("SalesReceiptForm", "Sales Receipt", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("SalesReturnCashForm", "Sales Return - Cash", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("SalesReturnCreditForm", "Sales Return - Credit Memo", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ConsignOutForm", "Consignment Out", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ConsignOutReturnForm", "Consignment Out Return", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ConsignOutSettlementForm", "Consignment Out Settlement", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("CustomerPaymentAllocationList", "Customer Unallocated Payments List", ScreenAreas.Sales.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("CustomerListForm", "Customer List", ScreenAreas.Sales.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("SalesHistoryListForm", "Sales History", ScreenAreas.Sales.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("CustomerCenterForm", "Customer Center", ScreenAreas.Sales.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("CustomerOpeningBalanceBatchForm", "Customer Opening Balance Entry", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("CityDetailsForm", "City", ScreenAreas.Sales.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("NationalAccountForm", "National Account", ScreenAreas.Sales.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("CustomerVendorLinkForm", "Customer / Vendor Relationship", ScreenAreas.Sales.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("PriceListDetailsForm", "Price List", ScreenAreas.Sales.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("UnallocatedPaymentsListForm", "Customer Payment Allocation", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("InventorySalesStatisticForm", "Item Sales Statistics", ScreenAreas.Sales.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("CustomerSalesStatisticForm", "Customer Sales Statistis", ScreenAreas.Sales.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("CustomerLedgerForm", "Customer Ledger", ScreenAreas.Sales.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("CustomerAgingListForm", "Customer Aging List", ScreenAreas.Sales.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("CLVoucherForm", "Credit Limit Voucher", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("InventorySalesStatisticForm", "Sale Statistics", ScreenAreas.Sales.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("ShipmentForm", "Shipment", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			if (GlobalRules.IsFeatureAllowed(EditionFeatures.ImportDocuments))
			{
				dataTable3.Rows.Add("ExportSalesOrderForm", "Export Sales Order", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("ExportSalesProformaInvoiceForm", "Export Sales Proforma Invoice", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("ExportDeliveryNoteForm", "Export Delivery Note", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("ExportSalesInvoiceForm", "Export Sales Invoice", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("ExportPickListForm", "Export Pick List", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			}
			dataTable3.Rows.Add("CustomerInsuranceDetailsForm", "Customer Insurance", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("InsuranceProviderForm", "Insurance Provider", ScreenAreas.Sales.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("CreditLimitReviewForm", "Credit Limit Review", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("CreditLimitPreviewListForm", "Credit Limit Review List", ScreenAreas.Sales.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("CustomerInsuranceListForm", "Customer Insurance List", ScreenAreas.Sales.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("CustomerOutStandingInvoiceListForm", "Customer Outstanding Invoice List", ScreenAreas.Sales.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("SalesManTargetForm", "Sales Man Target", ScreenAreas.Sales.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("CustomerInsuranceClaimForm", "Customer Insurance Claim", ScreenAreas.Sales.ToString(), ScreenTypes.Card.ToString());
			if (GlobalRules.IsModuleAvailable(AxolonModules.Garment))
			{
				dataTable3.Rows.Add("GarmentRentalForm", "Garment Rental", ScreenAreas.Garment.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("GarmentRentalReturnForm", "GarmentRental Return", ScreenAreas.Garment.ToString(), ScreenTypes.Transaction.ToString());
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.EnterpriseAsset))
			{
				dataTable3.Rows.Add("EAEquipmentForm", "Equipment Details", ScreenAreas.EnterpriseAsset.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EquipmentCategoryForm", "Equipment Category", ScreenAreas.EnterpriseAsset.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("EquipmentTypeForm", "Equipment Type", ScreenAreas.EnterpriseAsset.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("RequisitionTypeForm", "Requisition Type", ScreenAreas.EnterpriseAsset.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("RequisitionDetailsForm", "Requisition details", ScreenAreas.EnterpriseAsset.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("MobilisationForm", "Mobilisation details", ScreenAreas.EnterpriseAsset.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EquipmentTransferForm", "Equipment Transfer", ScreenAreas.EnterpriseAsset.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("WorkOrderForm", "Equipment Work Order", ScreenAreas.EnterpriseAsset.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("WorkOrderInventoryIssueForm", "Work Order Inventory Issue", ScreenAreas.EnterpriseAsset.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("WorkOrderInventoryReturnForm", "Work Order Inventory Return", ScreenAreas.EnterpriseAsset.ToString(), ScreenTypes.Transaction.ToString());
			}
			dataTable3.Rows.Add("BuyerDetailsForm", "Buyer Detail", ScreenAreas.Purchases.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("VendorAddressDetailsForm", "Vendor Address Detail", ScreenAreas.Purchases.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("VendorClassDetailsForm", "Vendor Class", ScreenAreas.Purchases.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("VendorGroupDetailsForm", "VendorGroup", ScreenAreas.Purchases.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("VendorCategoryDetailsForm", "VendorCategory", ScreenAreas.Purchases.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("VendorOutStandingInvoiceListForm", "Vendor Outstanding Invoice List", ScreenAreas.Purchases.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("CashPurchaseForm", "Cash Purchase", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("PurchaseInvoiceForm", "Purchase Invoice", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("PurchaseOrderForm", "Purchase Order", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			if (GlobalRules.IsFeatureAllowed(EditionFeatures.ImportDocuments))
			{
				dataTable3.Rows.Add("PurchaseOrderImportForm", "Import Purchase Order", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("PurchaseInvoiceImportForm", "Import Purchase Invoice", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("POShipmentForm", "PO Shipment", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			}
			if (GlobalRules.IsFeatureAllowed(EditionFeatures.Consignment))
			{
				dataTable3.Rows.Add("ConsignInForm", "Receive Consignment In", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("ConsignInReturnForm", "Consignment In Return", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("ConsignInSettlementForm", "Consignment In Settlement", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("ConsignInClosingForm", "Close Consignment In", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			}
			dataTable3.Rows.Add("PurchaseQuoteForm", "Purchase Quote", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ProformaInvoiceForm", "Proforma Invoice", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("PurchaseGRNForm", "Goods Received Note", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ImportPurchaseGRNForm", "Import Goods Received Note", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("PurchaseReturnCashForm", "Purchase Return - Cash", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("PurchaseReturnCreditForm", "Purchase Return - Credit Memo", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("GRNReturnForm", "GRN Return", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("VendorStatementForm", "Vendor Balance Detail", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("PurchasePackingListForm", "Purchase Packing List", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("VendorDetailsForm", "Vendor Detail", ScreenAreas.Purchases.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("VendorAgingListForm", "Vendor Aging List", ScreenAreas.Purchases.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("VendorPaymentAllocationList", "Vendor Unallocated Payments List", ScreenAreas.Purchases.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("VendorListForm", "Vendor List", ScreenAreas.Purchases.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("InventoryPurchasesStatisticForm", "Purchase Statistics", ScreenAreas.Purchases.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("VendorCenterForm", "Vendor Center", ScreenAreas.Purchases.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("VendorOpeningBalanceBatchForm", "Vendor Opening Balance Entry", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("TransporterDetailsForm", "Transporter Detail", ScreenAreas.Purchases.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("UnallocatedPaymentsListForm", "Vendor Payment Allocation", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("PurchaseClaimForm", "Purchase Claim", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("PortDetailsForm", "Port Detail", ScreenAreas.Purchases.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("PurchaseInvoiceNonInvForm", "Purchase Invoice(Non Inv)", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("PurchaseOrderNonInvForm", "Purchase Order(Non Inv)", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("PurchaseCostEntryForm", "Purchase Cost Entry", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("BillOfLadingListForm", "Bill Of Lading List", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("FreightChargesForm", "Freight Charges", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ProductPriceBulkUpdateForm", "Product Price Bulk Update", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("PurchasePrepaymentInvoiceForm", "Purchase Prepayment Invoice", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("PaymentAdviceDetailsForm", "Payment Advice Details", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("VendorLedgerForm", "Vendor Ledger", ScreenAreas.Purchases.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("ShipmentsPerformanceAnalyseForm", "Shipments Performance Analyse", ScreenAreas.Purchases.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("UpdateShipmentClaimStatusForm", "Update Shipment Claim Status", ScreenAreas.Purchases.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("CompanyAccountDetailsForm", "Accounts Details", ScreenAreas.Accounts.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("AccountGroupDetailsForm", "Accounts Group", ScreenAreas.Accounts.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("AnalysisDetailsForm", "Analysis", ScreenAreas.Accounts.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("AnalysisGroupDetailsForm", "Analysis Group", ScreenAreas.Accounts.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("BankDetailsForm", "Bank", ScreenAreas.Accounts.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("ChequebookDetailsForm", "Chequebook Details", ScreenAreas.Accounts.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("CostCenterDetailsForm", "Cost Center", ScreenAreas.Accounts.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("RegisterDetailsForm", "Register", ScreenAreas.Accounts.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("ReturnedChequeReasonDetailsForm", "Returned Cheque Reason", ScreenAreas.Accounts.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("CurrencyRateUpdateForm", "Currency Exchange Rate Update", ScreenAreas.Accounts.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("CompanyAccountsListForm", "Account List", ScreenAreas.Accounts.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("InventoryLedgerForm", "Trail Balance", ScreenAreas.Accounts.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("CashExpenseEntryForm", "Cash Expense Entry", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("PaymentRequestForm", "Payment Request", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("CashPaymentForm", "Cash Payment Voucher", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("CashReceiptForm", "Cash/Card Receipt Voucher", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ChequeDepositForm", "Cheque Deposit", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ChequeExpenseEntryForm", "Cheque Expense Entry", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ChequePaymentParentForm", "Cheque Payment Voucher", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ChequePaymentForm", "Cheque Payment Voucher", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ChequeReceiptForm", "Cheque Receipt Voucher", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ChequeReturnForm", "Returned Cheque Entry", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ChequeDiscountForm", "Cheque Discount Entry", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("SendChequesToBankForm", "Send Cheque to Bank", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ChequeReceiptMultiEntryForm", "Cheque Receipt - Multiple Account", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("CreditNoteEntryForm", "Credit Note", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("DebitNoteEntryForm", "Debit Note", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("FundTransferForm", "Fund Transfer", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("IssuedChequeCancellationForm", "Issued Cheque Cancellation", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("IssuedChequeClearanceForm", "Issued Cheques Clearance", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("IssuedChequeReturnForm", "Issued Cheque Return", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("JournalEntryForm", "Journal Entry", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ReceivedChequeCancellationForm", "Received Cheque Cancellation", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ReceivedChequeClearanceForm", "Cheque Clearance", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("SecurityChequeForm", "Issue Security Cheque", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("VoidBlankChequeForm", "Void Blank Cheque", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("AccountAnalysisDetailsForm", "Accounts Analysis Setup", ScreenAreas.Accounts.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("AccountCenterForm", "Account Center", ScreenAreas.Accounts.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("ChequeRegisterForm", "Chequebook Cheque Register", ScreenAreas.Accounts.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("PrintChequeForm", "Print Cheque", ScreenAreas.Accounts.ToString(), ScreenTypes.Other.ToString());
			dataTable3.Rows.Add("BankReconciliationOpeningForm", "Bank Reconciliation Opening", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("TRPaymentForm", "TR Payment", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("TRApplicationForm", "TR Application", ScreenAreas.Accounts.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("TREntryForm", "TR Entry", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("BankReconciliationForm", "Bank Reconciliation", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("CashReceiptMultiPayeeForm", "Cash Receipt - Multiple Account", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("BankFacilityGroupDetailsForm", "Bank Facility Group", ScreenAreas.Accounts.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("BankFacilityDetailsForm", "Bank Facility", ScreenAreas.Accounts.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("TTPaymentForm", "Bank Transfer Payment Voucher", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("TTReceiptForm", "Bank Transfer Receipt Voucher (TT)", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("StandingJournalEntryForm", "Standing Journal Entry", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ExpenseCodeDetailsForm", "Expense Code Entry", ScreenAreas.Accounts.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("LPOReceiptForm", "LPO Receipt", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("CollateralDetailsForm", "Collateral Management", ScreenAreas.Accounts.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("OpeningChequeReceiptEntryForm", "Opening Cheque Receipt Entry", ScreenAreas.Accounts.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("TaxEntryForm", "Tax Entry", ScreenAreas.Accounts.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("TaxGroupForm", "Tax Group Entry", ScreenAreas.Accounts.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("LoanEntryForm", "Loan Entry", ScreenAreas.Accounts.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("BillDiscountForm", "Bill Discount", ScreenAreas.Accounts.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("AdjustmentTypeDetailsForm", "Adjustment Type Detail", ScreenAreas.Products.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("ProductBrandDetailsForm", "Product Brand Detail", ScreenAreas.Products.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("ProductCategoryDetailsForm", "Product Category Detail", ScreenAreas.Products.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("ProductClassDetailsForm", "Item Class", ScreenAreas.Products.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("ProductDetailsForm", "Item Detail", ScreenAreas.Products.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("MatrixProductDetailsForm", "Matrix Item Detail", ScreenAreas.Products.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("ProductManufacturerDetailsForm", "Product Manufacturer Detail", ScreenAreas.Products.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("ProductStyleDetailsForm", "Product Style Detail", ScreenAreas.Products.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("ProductSpecificationDetailsForm", "Product Specification Detail", ScreenAreas.Products.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("UnitDetailsForm", "Unit", ScreenAreas.Products.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("MatrixTemplateDetailsForm", "Matrix Template Detail", ScreenAreas.Products.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("DimensionDetailsForm", "Matrix Dimension Detail", ScreenAreas.Products.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("ProductAttributeDetailsForm", "Product Attribute Detail", ScreenAreas.Products.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("ProductDataDetailsForm", "Product Data", ScreenAreas.Products.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("InventoryTransferTypeDetailsForm", "Inventory Transfer Type Detail", ScreenAreas.Products.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("CityDetailsForm", "City Detail", ScreenAreas.Products.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("GenericProductTypeDetailsForm", "Generic Product Type", ScreenAreas.Products.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("PackageDetailsForm", "Package", ScreenAreas.Products.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("ContainerTrackingForm", "Container Tracking", ScreenAreas.Products.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("DirectInventoryTransferForm", "Direct Inventory Transfer", ScreenAreas.Products.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("InventoryAdjustmentsForm", "Inventory Adjustment", ScreenAreas.Products.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("InventoryTransferAcceptanceForm", "Inventory Transfer - IN", ScreenAreas.Products.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("InventoryTransferForm", "Inventory Transfer", ScreenAreas.Products.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("InventoryTransferReturnForm", "Inventory Transfer Return", ScreenAreas.Products.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("InventoryCenterForm", "Inventory Center", ScreenAreas.Products.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("ProductListForm", "Item List", ScreenAreas.Products.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("InventoryLedgerForm", "Inventory Ledger", ScreenAreas.Products.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("InventoryOpeningBalanceBatchForm", "Inventory Opening Balance Entry", ScreenAreas.Products.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ItemTransactionForm", "Item Transaction", ScreenAreas.Products.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("InventoryDismantleForm", "Inventory Dismantle", ScreenAreas.Products.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("JobMaterialRequisitionForm", "Material Requisition", ScreenAreas.Products.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("BarCodePrintForm", "BarCode Print", ScreenAreas.Products.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("SalesForecastingForm", "Sales Forecasting", ScreenAreas.Products.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("InventoryRepackingForm", "Inventory Repacking", ScreenAreas.Products.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("InventoryDamageForm", "Non Sale Issue", ScreenAreas.Products.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("DriverDetailsForm", "Driver", ScreenAreas.Products.ToString(), ScreenTypes.Card.ToString());
			if (GlobalRules.IsModuleAvailable(AxolonModules.Vehicle))
			{
				dataTable3.Rows.Add("VehicleDetailsForm", "Vehicle Detail", ScreenAreas.Vehicle.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("VehicleDocTypeDetailsForm", "Vehicle DocTypeDetails", ScreenAreas.Vehicle.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("ServiceItemForm", "Service Item", ScreenAreas.Vehicle.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("ServiceProviderForm", "Service Provider", ScreenAreas.Vehicle.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("MaintenanceSchedulerForm", "Maintenance Scheduler", ScreenAreas.Vehicle.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("VehicleMaintenanceEntryForm", "Vehicle Maintenance Entry", ScreenAreas.Vehicle.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("VehicleMileageTrackForm", "Vehicle Mileage Track", ScreenAreas.Vehicle.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("VehicleTypeForm", "Vehicle Type", ScreenAreas.Vehicle.ToString(), ScreenTypes.Card.ToString());
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.Horse))
			{
				dataTable3.Rows.Add("HorseSummaryDetailsForm", "Horse Summary Details", ScreenAreas.Horse.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("RiderSummaryDetailsForm", "Rider Summary Details", ScreenAreas.Horse.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("HorseTypeForm", "Horse Type", ScreenAreas.Horse.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("HorseSexForm", "Horse Sex", ScreenAreas.Horse.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("HorseOwnershipType", "Horse Ownership Type", ScreenAreas.Horse.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("HorseCategory", "Horse Category", ScreenAreas.Horse.ToString(), ScreenTypes.Card.ToString());
			}
			if (GlobalRules.IsFeatureAllowed(EditionFeatures.HRPayroll))
			{
				dataTable3.Rows.Add("BenefitDetailsForm", "Benefit", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("CompanyDocTypeDetailsForm", "Company Document Type", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("CompanyDocumentsForm", "Company Documents", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("DegreeDetailsForm", "Degree", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("DepartmentDetailsForm", "Department", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("DestinationDetailsForm", "Destination", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("DisciplineActionTypeDetailsForm", "Disciplinary Action Type", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("DivisionDetailsForm", "Division", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("EmployeeActivityTypeDetailsForm", "Employee Activity Type", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("EmployeeAddressDetailsForm", "Employee Address Detail", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("EmployeeDependentDetailsForm", "Employee Dependent Detail", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("EmployeeDetailsForm", "Employee Detail", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("EmployeeDocTypeDetailsForm", "Employee Document Type", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("EmployeeDocumentsForm", "Employee Documents", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("EmployeeGroupDetailsForm", "Employee Group", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("EmployeeLeaveDetailForm", "Employee Leaves", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("EmployeeLoanTypeDetailsForm", "Emploee LoanType", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("EmployeeSkillsForm", "Employee Skills", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("EmployeeTypeDetailsForm", "Employee Type", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("GradeDetailsForm", "Grade", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("LeaveTypeDetailsForm", "Leave Type", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("NationalityDetailsForm", "Nationality", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("PayrollItemDetailsForm", "Payment Items", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("PositionDetailsForm", "Position", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("ReligionDetailsForm", "Religion", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("SkillDetailsForm", "Skill", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("SponsorDetailsForm", "Sponsor", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("TenancyContractDetailsForm", "Tenancy Contracts", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("TradeLicenseDetailsForm", "Trade License Details", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("VisaDetailsForm", "Visa Details", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("ReleaseTypeForm", "Release Type", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("HolidayCalendarForm", "Holiday Calendar", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("EmployeeSalaryDetailsForm", "Employee Salary Detail", ScreenAreas.HR.ToString(), ScreenTypes.Setup.ToString());
				dataTable3.Rows.Add("EmployeeCancellationForm", "Employee Cancellation Entry", ScreenAreas.HR.ToString(), ScreenTypes.Setup.ToString());
				dataTable3.Rows.Add("EmployeeOpeningBalanceBatchForm", "Employee Leave Process", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EmployeeAbscondingEntryForm", "Employee Absconding Entry", ScreenAreas.HR.ToString(), ScreenTypes.Setup.ToString());
				dataTable3.Rows.Add("CashSalaryPaymentForm", "Cash Salary Payment", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("ChequeSalaryPaymentForm", "Cash Salary Payment", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EmployeeDisciplinaryActionForm", "Employee Disciplinary Action Entry", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EmployeeGeneralActivityForm", "Employee General Activity Entry", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EmployeeLeaveApprovalForm", "Employee Leave Approval Entry", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EmployeeLeaveRequestForm", "Employee Leave Request Entry", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EmployeeLoanForm", "Employee Loan Entry", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EmployeeLoanSettlementForm", "Employee Loan Settlement", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EmployeePromotionForm", "Employee Promotion Entry", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EmployeeRehireForm", "Employee Rehire Entry", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EmployeeAppraisalForm", "Employee Appraisal Entry", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EmployeeResumptionForm", "Employee Duty Resumption Entry", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EmployeeTerminationForm", "Employee Termination Entry", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EmployeeTransferForm", "Employee Transfer", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("PostSalarySheetForm", "Post Salary Sheet", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("SalarySheetForm", "Salary Sheet", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("TransferSalaryPaymentForm", "Bank Transfer Salary Payment", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EmployeeEOSSettlementForm", "EOS Settlement Form", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("EmployeeEOSForm", "EOS Form", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("HRCenterForm", "HR Center", ScreenAreas.HR.ToString(), ScreenTypes.Setup.ToString());
				dataTable3.Rows.Add("AppointmentDetailsForm", "Appointment Details", ScreenAreas.HR.ToString(), ScreenTypes.Setup.ToString());
				dataTable3.Rows.Add("CandidateDetailsForm", "Candidate Details", ScreenAreas.HR.ToString(), ScreenTypes.Setup.ToString());
				dataTable3.Rows.Add("CandidateCancellationForm", "Candidate Cancellation", ScreenAreas.HR.ToString(), ScreenTypes.Setup.ToString());
				dataTable3.Rows.Add("EmployeePassportControlForm", "Passport Control", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("OverTimeEntryForm", "Over-Time Entry", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("OverTimeDetailsForm", "Overtime", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EOSRuleDetailsForm", "End of Service Benefits Rules", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("EmployeeLoanPaymentForm", "Employee Loan Payment", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EmployeeLeaveProcessForm", "Employee Leave Process", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("WorkLocationDetailsForm", "Work Location", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("LeaveEncashmentForm", "Leave Encashment", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("LeaveSalaryPaymentForm", "Leave Payment", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("MedicalInsuranceCategory", "Medical Insurance Category", ScreenAreas.HR.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("EmployeeProvisionEntry", "Employee Provision", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("SalaryAdditionForm", "Salary Addition", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("SalaryDeductionForm", "Salary Deduction", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("ProjectExpenseAllocationForm", "Project Expense", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EmployeePerformanceCardForm", "Employee Performance Card", ScreenAreas.HR.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EmployeeAppraisalHistoryListForm", "Employee Appraisal History List", ScreenAreas.HR.ToString(), ScreenTypes.List.ToString());
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.CRM))
			{
				dataTable3.Rows.Add("LeadDetailsForm", "Lead", ScreenAreas.CRM.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("CRMCustomerDetailsForm", "Customer", ScreenAreas.CRM.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("EntityCategoryDetailsForm", "Entity Category ", ScreenAreas.CRM.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("EventDetailsForm", "Event", ScreenAreas.CRM.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("ActivityDetailsForm", "Activity", ScreenAreas.CRM.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("ContactDetailsForm", "Contact Details", ScreenAreas.CRM.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("OpportunityDetailsForm", "Opportunity", ScreenAreas.CRM.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("CompetitorDetailsForm", "Competitor", ScreenAreas.CRM.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("CampaignDetailsForm", "Campaign", ScreenAreas.CRM.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("FollowupDetailsForm", "Followup", ScreenAreas.CRM.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("LeadAddressDetailsForm", "Lead Address", ScreenAreas.CRM.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("LeadStatusForm", "Lead Status", ScreenAreas.CRM.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("TaskStepsForm", "Task Steps", ScreenAreas.CRM.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("TaskTypeForm", "Task Type", ScreenAreas.CRM.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("TaskTransactionForm", "Task Transaction", ScreenAreas.CRM.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("TaskTransactionStatusForm", "Task Transaction Status", ScreenAreas.CRM.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("UpdateAssigneeDialog", "Change Assignee", ScreenAreas.CRM.ToString(), ScreenTypes.Dialog.ToString());
				dataTable3.Rows.Add("UpdateTaskStepDialog", "Change Task Step", ScreenAreas.CRM.ToString(), ScreenTypes.Dialog.ToString());
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.Medical))
			{
				dataTable3.Rows.Add("PatientForm", "Patient", ScreenAreas.Medical.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("PatientDocTypeDetailsForm", "Patient Document Type", ScreenAreas.Medical.ToString(), ScreenTypes.Card.ToString());
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.Legal))
			{
				dataTable3.Rows.Add("LawyerDetailsForm", "Lawyer", ScreenAreas.Legal.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("CasePartyDetailsForm", "Case Party", ScreenAreas.Legal.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("CaseClientDetailsForm", "Case Client", ScreenAreas.Legal.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("LegalActivityDetailsForm", "LegalActivity", ScreenAreas.Legal.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("LegalActionDetailsForm", "LegalAction", ScreenAreas.Legal.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("LegalActionStatusForm", "Legal Action Status", ScreenAreas.Legal.ToString(), ScreenTypes.Transaction.ToString());
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.QC))
			{
				dataTable3.Rows.Add("ArrivalReportForm", "Arrival Report", ScreenAreas.QC.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("QualityClaimForm", "Quality Claim", ScreenAreas.QC.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("QualityTaskForm", "QC Task", ScreenAreas.QC.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("Surveyor", "Surveyor", ScreenAreas.QC.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("ArrivalReportTemplateForm", "Arrival Report Template", ScreenAreas.QC.ToString(), ScreenTypes.Card.ToString());
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.ProjectAccounting))
			{
				dataTable3.Rows.Add("JobMaterialEstimateForm", "Material Estimation", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("JobClosingForm", "Project Closing", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("EquipmentDetailsForm", "Equipment", ScreenAreas.Project.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("JobMaterialRequisitionForm", "Material Requisition Entry", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("JobTimesheetForm", "Project Timesheet Entry", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("JobTimesheetOrionForm", "Orion Project Timesheet Entry", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("JobExpenseIssueForm", "Project Expense Entry", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("JobInventoryReturnForm", "Project Inventory Return", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("CostCategoryDetailsForm", "Cost Category", ScreenAreas.Project.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("JobInventoryIssueForm", "Project Inventory Issue", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("JobDetailsForm", "Job/Project Details", ScreenAreas.Project.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("ProjectBillingForm", "Project Billing", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("JobTypeDetailsForm", "Job Type", ScreenAreas.Project.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("JobBOMDetailsForm", "Job BOM Detail", ScreenAreas.Project.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("JobMaintenanceScheduleForm", "Job Maintenance Schedule", ScreenAreas.Project.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("JobMaintenanceServiceEntryForm", "MaintenanceServiceEntry", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("JobEstimationDetailsForm", "Estimation", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("ProjectSubcontractPOForm", "Purchase Order SubContract", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("ProjectSubContractPIForm", "Purchase Invoice SubContract", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("JobManHrsBudgetingForm", "Man Hour Estimation", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("JobFeeDetailForm", "Project Fees", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("JobBudgetDetailForm", "Project Budget", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("JobEquipmentDetailForm", "Project Equipment", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("JobTaskDetailsForm", "Project Task", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("ClientAssetForm", "Client Asset", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("ServiceActivityDetailsForm", "Service Activity", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("WorkLocationDetailsForm", "Work Location", ScreenAreas.Project.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("JobFeeForm", "Job Fee", ScreenAreas.Project.ToString(), ScreenTypes.Card.ToString());
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.AdvancedManufacturing))
			{
				dataTable3.Rows.Add("WorkOrderDetailsForm", "Work Order", ScreenAreas.Manufacturing.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("BuildAssemblyForm", "Build Assembly", ScreenAreas.Manufacturing.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("ProductionDetailsForm", "Production", ScreenAreas.Manufacturing.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("BOMDetailsForm", "BOM Detail", ScreenAreas.Manufacturing.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("RouteDetailsForm", "Route Details", ScreenAreas.Manufacturing.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("RouteGroupDetailsForm", "Route Group Details", ScreenAreas.Manufacturing.ToString(), ScreenTypes.Card.ToString());
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.Garment))
			{
				dataTable3.Rows.Add("GarmentRentalForm", "Garment Rental", ScreenAreas.Garment.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("GarmentRentalReturnForm", "Garment Rental Return", ScreenAreas.Garment.ToString(), ScreenTypes.Transaction.ToString());
			}
			if (GlobalRules.IsFeatureAllowed(EditionFeatures.FixedAsset))
			{
				dataTable3.Rows.Add("FixedAssetSaleForm", "Fixed Asset Sale", ScreenAreas.FixedAsset.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("FixedAssetPurchaseForm", "Fixed Asset Aquesition", ScreenAreas.FixedAsset.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("FixedAssetClassDetailsForm", "Fixed Asset Class", ScreenAreas.FixedAsset.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("FixedAssetGroupDetailsForm", "Fixed Asset Group", ScreenAreas.FixedAsset.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("FixedAssetLocationDetailsForm", "Fixed Asset Location", ScreenAreas.FixedAsset.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("FixedAssetDetailsForm", "Fixed Asset Detail", ScreenAreas.FixedAsset.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("FixedAssetTransferForm", "Fixed Asset Transfer", ScreenAreas.FixedAsset.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("FixedAssetDepForm", "Fixed Asset Depreciation", ScreenAreas.FixedAsset.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("VehicleDetailsForm", "Vehicle", ScreenAreas.FixedAsset.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("FixedAssetBulkPurchaseForm", "Fixed Asset Bulk Aquesition", ScreenAreas.FixedAsset.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("FixedAssetCompanyForm", "Fixed Asset Company Form", ScreenAreas.FixedAsset.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("FixedAssetPurchaseOrderForm", "Fixed Asset Purchase Order Form", ScreenAreas.FixedAsset.ToString(), ScreenTypes.Transaction.ToString());
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.PropertyRental))
			{
				dataTable3.Rows.Add("PropertyDetailsForm", "Property", ScreenAreas.PropertyRental.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("PropertyUnitDetailsForm", "Unit ", ScreenAreas.PropertyRental.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("VirtualUnitDetailsForm", "VirtualUnit ", ScreenAreas.PropertyRental.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("PropertyTenantForm", "Tenant", ScreenAreas.PropertyRental.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("PropertyTenantClassDetailsForm", "Tenanat Class", ScreenAreas.PropertyRental.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("PropertyClassDetailsForm", "Class", ScreenAreas.PropertyRental.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("PropertyAgentDetailsForm", "Agent", ScreenAreas.PropertyRental.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("PropertyCategoryDetailsForm", "Category", ScreenAreas.PropertyRental.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("PropertyIncomeCodeDetailsForm", "Income Code", ScreenAreas.PropertyRental.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("KitchenTypeForm", "Kitchen Type", ScreenAreas.PropertyRental.ToString(), ScreenTypes.Card.ToString());
				dataTable3.Rows.Add("PropertyRentDetailsForm", "Rental Registration", ScreenAreas.PropertyRental.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("PropertyRentRenewDetailsForm", "Rental Renewal", ScreenAreas.PropertyRental.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("PropertyRentCancellationForm", "Rental Cancellation", ScreenAreas.PropertyRental.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("RentIncomePostingDetails", "Rental Income Posting", ScreenAreas.PropertyRental.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("PropertyServiceRequestForm", "Service Request", ScreenAreas.PropertyRental.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("PropertyServiceRequestForm", "Service Assign", ScreenAreas.PropertyRental.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("PropertyServiceInvoice", "Service Invoice", ScreenAreas.PropertyRental.ToString(), ScreenTypes.Transaction.ToString());
				dataTable3.Rows.Add("RecurringTransactionPostForm", "Recurring Transaction Post", ScreenAreas.PropertyRental.ToString(), ScreenTypes.Transaction.ToString());
			}
			dataTable3.Rows.Add("AreaDetailsForm", "Area", ScreenAreas.General.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("ContactDetailsForm", "Contact Detail", ScreenAreas.General.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("CountryDetailsForm", "Country", ScreenAreas.General.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("CurrencyDetailsForm", "Currency", ScreenAreas.General.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("LocationDetailsForm", "Location", ScreenAreas.General.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("PaymentMethodDetailsForm", "Payment Method", ScreenAreas.General.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("PaymentTermDetailsForm", "Payment Term", ScreenAreas.General.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("PriceLevelDetailsForm", "Price Level Setup", ScreenAreas.General.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("ShippingMethodDetailsForm", "Shipping Method", ScreenAreas.General.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("SysDocDetailsForm", "Document Groups", ScreenAreas.General.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("ExpenseCodeDetailsForm", "Expense Code", ScreenAreas.General.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("GenericListDetailsForm", "Generic Detail", ScreenAreas.General.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("ReminderListForm", "Reminders", ScreenAreas.General.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("CompanyOptionsForm", "Company Preferences", ScreenAreas.General.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("CompanyInformationForm", "Company Information", ScreenAreas.General.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("JournalDistibutionDialog", "Journal Distibution", ScreenAreas.General.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("DocumentOCRForm", "Document OCR", ScreenAreas.General.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("OCRSettingsForm", "OCR Settings", ScreenAreas.General.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("CompanyDivisionDetailsForm", "Company Division", ScreenAreas.General.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("DocManagementForm", "File Attachment", ScreenAreas.General.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("EntityCommentsForm", "Entity Comments Form", ScreenAreas.General.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("EntityFlagDetailsForm", "Entity Flag", ScreenAreas.General.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("AttachementDetailsForm", "MultiPrint", ScreenAreas.General.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("VendorItemListForm", "Item List", ScreenAreas.General.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("InventoryAccountsForm", "InventoryAccounts", ScreenAreas.General.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("BackupDatabaseForm", "Backup Database", ScreenAreas.System.ToString(), ScreenTypes.System.ToString());
			dataTable3.Rows.Add("RestoreDatabaseForm", "Restore Database", ScreenAreas.System.ToString(), ScreenTypes.System.ToString());
			dataTable3.Rows.Add("UpgradeDatabaseForm", "Upgrade Database", ScreenAreas.System.ToString(), ScreenTypes.System.ToString());
			dataTable3.Rows.Add("DatabaseAttachmentForm", "Attach Database", ScreenAreas.System.ToString(), ScreenTypes.System.ToString());
			dataTable3.Rows.Add("DatabaseDetachmentForm", "Detach Database", ScreenAreas.System.ToString(), ScreenTypes.System.ToString());
			dataTable3.Rows.Add("ConnectionSettingsDialog", "Connection Settings", ScreenAreas.System.ToString(), ScreenTypes.System.ToString());
			dataTable3.Rows.Add("UserDetailsForm", "User Details", ScreenAreas.System.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("UserGroupDetailsForm", "User Group Details", ScreenAreas.System.ToString(), ScreenTypes.Card.ToString());
			dataTable3.Rows.Add("AccessLevelAssignForm", "Access Level Setting", ScreenAreas.System.ToString(), ScreenTypes.System.ToString());
			dataTable3.Rows.Add("ActiveUsersDialog", "Active Users", ScreenAreas.System.ToString(), ScreenTypes.List.ToString());
			dataTable3.Rows.Add("ReminderSetupForm", "Reminder Setup", ScreenAreas.System.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("FiscalYearDetailsForm", "Fiscal Year Details", ScreenAreas.System.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("UDFSetupForm", "User Defined Field Setup", ScreenAreas.System.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("UserGroupDetailsForm", "User Group Detail", ScreenAreas.System.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("CheckListDetailsForm", "Check List Detail", ScreenAreas.System.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("ApprovalDetailsForm", "Document Approval", ScreenAreas.System.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("VerificationDetailsForm", "Document Verification", ScreenAreas.System.ToString(), ScreenTypes.Setup.ToString());
			dataTable3.Rows.Add("EmployeeOpeningBalanceLeaveForm", "Employee Opening Balance Leave Entry", ScreenAreas.System.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ActivityLogListForm", "Activity Log", ScreenAreas.System.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("ReportTemplateUpdateForm", "Report Template Update", ScreenAreas.System.ToString(), ScreenTypes.Transaction.ToString());
			dataTable3.Rows.Add("AccountAnalysisReportForm", "Account Analysis Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("AccountAnalysisPivotReportForm", "Account Analysis Pivot Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("AccountTransactionsReportForm", "Account Transactions Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("AccountLedgerReportForm", "Account Ledger Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("AccountCostCenterReportForm", "CostCenter Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("BalanceSheetReportForm", "Balance Sheet Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("CashFlowReportForm", "Cash Flow Statement Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("GLReportForm", "General Ledger Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("JournalReportForm", "Journal Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("ProfitAndLossReportForm", "Profit and Loss Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("ProfitAndLossMonthwiseReportForm", "Profit and Loss Report Monthwise", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("ProfitAndLossComparisonReportForm", "Profit and Loss Comparison", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("TrialBalanceReportForm", "Trial Balance Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("PDCReceivedReportForm", "PDC Received", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("PDCIssuedReportForm", "PDC Issued", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("DailyCashReportForm", "Daily Cash Sales Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("BankReconciliationsReportForm", "Bank Reconciliations", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("BankNotReconciledReportForm", "Bank NotReconciled", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("BankLedgerReportForm", "Bank Book", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("ChequeMaturityReportForm", "Cheque Maturity Report Form", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("TaxDetailsReport", "Tax Details Report Form", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("BalanceSheetComparisonReportForm", "Balance Sheet Comparison", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsAccounts.ToString());
			dataTable3.Rows.Add("CustomReportDetailForm", "Custom Reports", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsCustom.ToString());
			dataTable3.Rows.Add("SmartListForm", "Smart List", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsCustom.ToString());
			dataTable3.Rows.Add("ChartCenterForm", "Pivot & Chart Center", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsCustom.ToString());
			dataTable3.Rows.Add("ExternalReportForm", "External Reports", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsCustom.ToString());
			dataTable3.Rows.Add("MatrixProductStockListReport", "Matrix Product Stock List Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsInventory.ToString());
			dataTable3.Rows.Add("InventoryTransferReport", "Inventory Transfer Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsInventory.ToString());
			dataTable3.Rows.Add("InventoryAgingReport", "Inventory Aging Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsInventory.ToString());
			dataTable3.Rows.Add("InventoryAdjustmentReport", "Inventory Adjustment Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsInventory.ToString());
			dataTable3.Rows.Add("ProductStockListReport", "Inventory Valuation Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsInventory.ToString());
			dataTable3.Rows.Add("ProductStockListReport", "Inventory Valuation Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsInventory.ToString());
			dataTable3.Rows.Add("InventoryTransactionsReport", "Inventory Transaction Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsInventory.ToString());
			dataTable3.Rows.Add("ProductCatalogReport", "Item Catalog Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsInventory.ToString());
			dataTable3.Rows.Add("ProductListReport", "Item List Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsInventory.ToString());
			dataTable3.Rows.Add("ProductPriceListReport", "Item Price List Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsInventory.ToString());
			dataTable3.Rows.Add("ProductStockPivotListReport", "Item Stock List Pivot", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsInventory.ToString());
			dataTable3.Rows.Add("InventoryTransactionsLotwiseReport", "Inventory Transaction Lotwise Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsInventory.ToString());
			dataTable3.Rows.Add("W3PLInventoryTransactionsReport", "3PL Item Ledger", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsInventory.ToString());
			dataTable3.Rows.Add("W3PLProductStockPivotListReport", "3PL Item Stock", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsInventory.ToString());
			dataTable3.Rows.Add("W3PLInventoryAgingReport", "3PL Stock Aging", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsInventory.ToString());
			dataTable3.Rows.Add("ContainerTrackingReportForm", "Container Tracking", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsInventory.ToString());
			dataTable3.Rows.Add("BarCodeReport", "Bar Code", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsInventory.ToString());
			dataTable3.Rows.Add("SalesByCustomerGroupReport", "Sales by Customer Group Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesByCustomerReport", "Sales by Customer Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesByItemReport", "Sales by Item Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesByLocationReport", "Sales by Location Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesByProductCategoryReport", "Sales by Product Category Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesBySalespersonReport", "Sales by Salesperson Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesByItemCustomerSalespersonReport", "Sales by Item, Customer, Salesperson Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesByGRNReport", "Sales by GRN Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesByGRNSummaryReport", "Sales by GRN Summary Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesManDueReport", "Sales Man Due Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("PickListReport", "Export PickList Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesEnquiryReport", "Sales Enquiry Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesInvoiceReport", "Sales Invoice Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesOrderReport", "Sales Order Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesQuoteReport", "Sales Quote Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesReceiptReport", "Sales Receipt Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("DeliveryNoteReport", "Delivery Note Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("ProformaInvoiceReport", "Proforma Invoice Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("DailyCashSaleReport", "Daily Cash Register", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("DailySalesAnalysisReport", "Daily Sales Analysis", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesByProductClassandCategoryReport", "Sales By Product Class and Category Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesByProductBrandReport", "Sales By Product Brand Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalespersonCommisionReport", "Salesperson Commission Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesProfitabilityReport", "Sales Profitability Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesByMainCategory", "SalesByMainCategory", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("MonthlySalesPivotReport", "Monthly Sales Pivot Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesOrderDetailReport", "Sales Order Detail Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("MultipleInvoiceReport", "Multiple Invoice", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesPurchaseAnalysisReport", "Sales Purchase Analysis", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesComparisonReport", "Sales Comparison", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesReport", "Sales Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("SalesByCustomerFilterReport", "Sales By Customer Multi Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsSales.ToString());
			dataTable3.Rows.Add("PurchaseByBuyerReport", "Purchase by Buyer Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPurchase.ToString());
			dataTable3.Rows.Add("PurchaseByItemReport", "Purchase by Item Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPurchase.ToString());
			dataTable3.Rows.Add("PurchaseByLocationReport", "Purchase by Location Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPurchase.ToString());
			dataTable3.Rows.Add("PurchaseByProductCategoryReport", "Purchase by Item Category Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPurchase.ToString());
			dataTable3.Rows.Add("PurchaseByVendorGroupReport", "Purchase by Vendor Group Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPurchase.ToString());
			dataTable3.Rows.Add("PurchaseByVendorReport", "Purchase by Vendor Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPurchase.ToString());
			dataTable3.Rows.Add("PurchaseComparisonReport", "Quotation Comparison", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPurchase.ToString());
			dataTable3.Rows.Add("PendingGRNReport", "Pending GRN", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPurchase.ToString());
			dataTable3.Rows.Add("PurchaseExpenseAllocationReport", "Purchase Expense Allocation", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPurchase.ToString());
			dataTable3.Rows.Add("ItemMovementGRNReport", "Item Movement GRN", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPurchase.ToString());
			dataTable3.Rows.Add("PurchaseOrderDetailReport", "Purchase Order Detail", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPurchase.ToString());
			dataTable3.Rows.Add("PurchaseLogReport", "Purchase Log", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPurchase.ToString());
			dataTable3.Rows.Add("PurchaseByItemVendorBuyerReport", "Purchase ByItemVendorBuyer", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPurchase.ToString());
			dataTable3.Rows.Add("PurchaseCostEntryReport", "Purchase Cost Entry", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPurchase.ToString());
			dataTable3.Rows.Add("PurchaseQuoteReport", "Purchase Quote Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPurchase.ToString());
			dataTable3.Rows.Add("PurchaseInvoiceReport", "Purchase Invoice Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPurchase.ToString());
			dataTable3.Rows.Add("PurchasePackingListReport", "Purchase Packing List Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPurchase.ToString());
			dataTable3.Rows.Add("PurchaseGRNReport", "Purchase GRN Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPurchase.ToString());
			dataTable3.Rows.Add("PurchaseByInventoryItemVendorBuyerReport ", "Purchase By Inventory Item Vendor Buyer Report ", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPurchase.ToString());
			dataTable3.Rows.Add("CustomerActivityReport", "Customer Activity Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsCustomer.ToString());
			dataTable3.Rows.Add("CustomerTopListReport", "Customer Top List Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsCustomer.ToString());
			dataTable3.Rows.Add("CustomerAgingSummaryReport", "Receivables Aging Summary", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsCustomer.ToString());
			dataTable3.Rows.Add("CustomerBalanceDetailsReport", "Customer Ledger Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsCustomer.ToString());
			dataTable3.Rows.Add("CustomerBalanceSummaryReport", "Customer Balance Summary", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsCustomer.ToString());
			dataTable3.Rows.Add("CustomerContactListReport", "Customer Contact List Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsCustomer.ToString());
			dataTable3.Rows.Add("CustomerListReport", "Customer List Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsCustomer.ToString());
			dataTable3.Rows.Add("CustomerProfileReport", "Customer Profile Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsCustomer.ToString());
			dataTable3.Rows.Add("CustomerOutstandingInvoicesReport", "Customer Outstanding Invoices", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsCustomer.ToString());
			dataTable3.Rows.Add("RequestForCreditLimitReport", "Customer Credit Analysis", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsCustomer.ToString());
			dataTable3.Rows.Add("CustomerOutstandingSummaryReport", "Customer Outstanding Summary", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsCustomer.ToString());
			dataTable3.Rows.Add("CustomerDueReport", "Customer Due", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsCustomer.ToString());
			dataTable3.Rows.Add("VendorActivityReport", "Vendor Activity Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsVendor.ToString());
			dataTable3.Rows.Add("VendorAgingSummaryReport", "Payables Aging Summary", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsVendor.ToString());
			dataTable3.Rows.Add("VendorBalanceDetailsReport", "Vendor Balance Detail Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsVendor.ToString());
			dataTable3.Rows.Add("VendorBalanceSummaryReport", "Vendor Balance Summary Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsVendor.ToString());
			dataTable3.Rows.Add("VendorContactListReport", "Vendor Contact List Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsVendor.ToString());
			dataTable3.Rows.Add("VendorListReport", "Vendor List Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsVendor.ToString());
			dataTable3.Rows.Add("VendorProfileReport", "Vendor Profile Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsVendor.ToString());
			dataTable3.Rows.Add("VendorOutstandingInvoicesReport", "Vendor Outstanding Invoices", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsVendor.ToString());
			dataTable3.Rows.Add("FreightChargeReport", "Freight Charge Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsVendor.ToString());
			dataTable3.Rows.Add("VendorOutstandingSummaryReport", "Vendor Outstanding Invoice Summary", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsVendor.ToString());
			dataTable3.Rows.Add("EmployeeActivityReport", "Employee Activity Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsHR.ToString());
			dataTable3.Rows.Add("EmployeeBalanceDetailsReport", "Employee Ledger Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsHR.ToString());
			dataTable3.Rows.Add("EmployeeBalanceSummaryReport", "Employee Balance Summary Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsHR.ToString());
			dataTable3.Rows.Add("EmployeeListReport", "Employee List Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsHR.ToString());
			dataTable3.Rows.Add("EmployeeProfileReport", "Employee Profile Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsHR.ToString());
			dataTable3.Rows.Add("EmployeeSalaryReport", "Employee Payroll Transaction Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsHR.ToString());
			dataTable3.Rows.Add("EmployeeGraduityEligibilityReport", "Employee Graduity Eligibility Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsHR.ToString());
			dataTable3.Rows.Add("EmployeeHistoryReport", "Employee History Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsHR.ToString());
			dataTable3.Rows.Add("EmployeeLeaveReport", "Employee Leave Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsHR.ToString());
			dataTable3.Rows.Add("EmployeeLeaveStatusReport", "Employee Leave Status Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsHR.ToString());
			dataTable3.Rows.Add("EmployeeAnnualLeaveDueReport", "Employee Annual Leave Due Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsHR.ToString());
			dataTable3.Rows.Add("EmployeeOverTimeReport", "Employee OverTime Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsHR.ToString());
			dataTable3.Rows.Add("EmployeeSalaryReport", "Employee Salary Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsHR.ToString());
			dataTable3.Rows.Add("EmployeeSalarySlipReport", "Employee Salary Slip Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsHR.ToString());
			dataTable3.Rows.Add("EmployeeLoanReport", "Employee Loan Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsHR.ToString());
			if (GlobalRules.IsModuleAvailable(AxolonModules.ProjectAccounting))
			{
				dataTable3.Rows.Add("MaterialVarianceReportForm", "Material Variance Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsProject.ToString());
				dataTable3.Rows.Add("ProjectStatusReportForm", "Project Status Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsProject.ToString());
				dataTable3.Rows.Add("JobBudgetVsActualReport", "JobBudget Vs Actual", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsProject.ToString());
				dataTable3.Rows.Add("JobSummaryReportForm", "Job SummaryReportForm", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsProject.ToString());
				dataTable3.Rows.Add("ServiceCallTrackReportForm", "Service Call Track Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsProject.ToString());
				dataTable3.Rows.Add("MaterialRequisitionReportForm", "Material Requisition", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsProject.ToString());
				dataTable3.Rows.Add("MaterialRequisitionFlowReport", "Material Requisition Flow", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsProject.ToString());
				dataTable3.Rows.Add("ProjectInvoiceReportForm", "Project Invoice Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsProject.ToString());
				dataTable3.Rows.Add("ProjectDueReportForm", "Project Due for Billing Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsProject.ToString());
				dataTable3.Rows.Add("ProjectManPowerReportForm", "Project Man Power Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsProject.ToString());
				dataTable3.Rows.Add("ProjectSubContractPOReportForm", "Project Sub Contract PO Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsProject.ToString());
				dataTable3.Rows.Add("SubContractPurchaseByItemVendorBuyerReport", "Project Sub Contract PI Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsProject.ToString());
				dataTable3.Rows.Add("ProjectAccountTransactionsReportForm", "Project Account Transaction Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsProject.ToString());
				dataTable3.Rows.Add("ProjectInventoryTransactionsReport", "Project Inventory Transaction Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsProject.ToString());
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.PropertyRental))
			{
				dataTable3.Rows.Add("PropertyAvailabiltyReport", "Property Availability", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPropertyRental.ToString());
				dataTable3.Rows.Add("PropertyRegistrationReport", "Property Registration", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPropertyRental.ToString());
				dataTable3.Rows.Add("PropertyUnitAvailabilityReportForm", "Property Unit Availability", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPropertyRental.ToString());
				dataTable3.Rows.Add("PropertyUnitHistoryReportForm", "Property Unit History", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsPropertyRental.ToString());
			}
			if (GlobalRules.IsFeatureAllowed(EditionFeatures.FixedAsset))
			{
				dataTable3.Rows.Add("FixedAssetListReport", "Fixed Asset List", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsFixedAsset.ToString());
				dataTable3.Rows.Add("FixedAssetDepreciationReport", "Fixed Asset Depreciation", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsFixedAsset.ToString());
				dataTable3.Rows.Add("FixedAssetTransactionsReport", "Fixed Asset Transaction", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsFixedAsset.ToString());
				dataTable3.Rows.Add("FixedAssetPurchaseReport", "Fixed Asset Purchase", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsFixedAsset.ToString());
				dataTable3.Rows.Add("FixedAssetSaleReport", "Fixed Asset Sales", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsFixedAsset.ToString());
				dataTable3.Rows.Add("FixedAssetTransferReport", "Fixed Asset Transfer", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsFixedAsset.ToString());
			}
			dataTable3.Rows.Add("PendingConsignInReport", "Open Consignment List Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsConsignIn.ToString());
			dataTable3.Rows.Add("ConsignmentInReport", "Consign In Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsConsignIn.ToString());
			dataTable3.Rows.Add("ItemMovementConsignInReport", "Item Movement Consign In", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsConsignIn.ToString());
			dataTable3.Rows.Add("ConsignInReceiptReport", "TruckConsignIn", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsConsignIn.ToString());
			dataTable3.Rows.Add("ConsignmentInSettlementReport", "Consign In-Awaiting  Settlement", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsConsignIn.ToString());
			dataTable3.Rows.Add("PendingConsignOutReport", "Open Consignment List Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsConsignOut.ToString());
			dataTable3.Rows.Add("ConsignmentOutReport", "Consignment Out Profitabiliy", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsConsignOut.ToString());
			dataTable3.Rows.Add("ConsignmentOutSettlementReport", " Consign Out-Awaiting  Settlement", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsConsignOut.ToString());
			dataTable3.Rows.Add("ConsignmentOutIssuedReport", "Consign Out-Issued", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsConsignOut.ToString());
			dataSet.Relations.Add("Rel1", dataSet.Tables["ScreenArea"].Columns["ScreenID"], dataSet.Tables["ScreenSubArea"].Columns["ParentID"], createConstraints: false);
			dataSet.Relations.Add("Rel2", dataSet.Tables["ScreenArea"].Columns["ScreenID"], dataSet.Tables["Screen"].Columns["ScreenArea"], createConstraints: false);
			dataSet.Relations.Add("Rel3", dataSet.Tables["ScreenSubArea"].Columns["ScreenID"], dataSet.Tables["Screen"].Columns["ScreenSubArea"], createConstraints: false);
			if (GlobalRules.IsModuleAvailable(AxolonModules.EnterpriseAsset))
			{
				dataTable3.Rows.Add("EquipmentListReport", "Equipment List", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsEnterprise.ToString());
				dataTable3.Rows.Add("EquipmentTransferReport", "Equipment Transfer Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsEnterprise.ToString());
				dataTable3.Rows.Add("MobilizationByWorkLocationProjectReport", "Mobilization Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsEnterprise.ToString());
				dataTable3.Rows.Add("RequisitionByWorkLocationProjectReport", "Requisition Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsEnterprise.ToString());
				dataTable3.Rows.Add("WorkOrderByLocationProjectReport", "Work Order Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsEnterprise.ToString());
				dataTable3.Rows.Add("EquipmentFlowReport", "Equipment Flow", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsEnterprise.ToString());
				dataTable3.Rows.Add("WorkOrderInventoryTransactionsReport", "WorkOrde rInventory Transactions", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsEnterprise.ToString());
			}
			if (GlobalRules.IsModuleAvailable(AxolonModules.Legal))
			{
				dataTable3.Rows.Add("CaseHistoryReport", "Case History Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsLegal.ToString());
				dataTable3.Rows.Add("CaseLawyerTrack", "Case Lawyer Track", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsLegal.ToString());
				dataTable3.Rows.Add("PendingCaseReport", "Pending Case Report", DBNull.Value, ScreenTypes.Report.ToString(), ScreenAreas.ReportsLegal.ToString());
			}
			return dataSet;
		}

		public DataSet CreateFormList()
		{
			string str = "";
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("Forms");
			dataTable.Columns.Add("ScreenID", typeof(int));
			dataTable.Columns.Add("ScreenName");
			dataTable.Columns.Add("ScreenArea", typeof(byte));
			dataTable.Columns.Add("ScreenType", typeof(byte));
			try
			{
				StreamWriter streamWriter = new StreamWriter("C:\\xx.txt");
				Type[] types = GetType().Assembly.GetTypes();
				foreach (Type type in types)
				{
					Form form = null;
					if (type.BaseType == typeof(Form))
					{
						str = type.Name;
						try
						{
							form = UIRefelector.GetForm(type);
						}
						catch (Exception e)
						{
							ErrorHelper.ProcessError(e);
							continue;
						}
						IForm form2 = form as IForm;
						if (form2 != null)
						{
							streamWriter.WriteLine(checked(form.Name + "," + form.Text + "," + ((byte)form2.ScreenArea).ToString() + "," + (byte)form2.ScreenType));
						}
					}
				}
				streamWriter.Close();
				return dataSet;
			}
			catch (Exception ex)
			{
				ErrorHelper.WarningMessage(str + " " + ex.Message);
				return null;
			}
		}

		public void PrintPreviewTransaction(string sysDocID, string voucherID)
		{
		}

		public Image GetTransactionPreviewImage(string sysDocID, string voucherID)
		{
			XtraReport transactionPrintDocument = GetTransactionPrintDocument(sysDocID, voucherID);
			if (transactionPrintDocument == null)
			{
				return null;
			}
			return PrintHelper.GetTransactionPreviewImage(transactionPrintDocument);
		}

		public void GetTransactionPreviewDoc(string sysDocID, string voucherID, string DocumentName, bool isPrint)
		{
			GetTransactionMultiplePrintDocument(sysDocID, voucherID, DocumentName, isPrint);
		}

		public XtraReport GetTransactionPreviewDoc(string sysDocID, string voucherID)
		{
			return GetTransactionPrintDocument(sysDocID, voucherID);
		}

		public MemoryStream GetTransactionPreviewPDF(string sysDocID, string voucherID)
		{
			XtraReport transactionPrintDocument = GetTransactionPrintDocument(sysDocID, voucherID);
			if (transactionPrintDocument == null)
			{
				return null;
			}
			return PrintHelper.GetTransactionPreviewPDF(transactionPrintDocument);
		}

		private XtraReport GetTransactionPrintDocument(string sysDocID, string voucherID)
		{
			return GetTransactionPrintDocument(sysDocID, voucherID, null, null);
		}

		private XtraReport GetTransactionPrintDocument(string sysDocID, string voucherID, object option1, object option2)
		{
			try
			{
				SysDocTypes sysDocTypes = (SysDocTypes)int.Parse(Factory.DatabaseSystem.GetFieldValue("System_Document", "SysDocType", "SysDocID", sysDocID).ToString());
				DataSet data = null;
				switch (sysDocTypes)
				{
				case SysDocTypes.SalesInvoice:
					data = Factory.SalesInvoiceSystem.GetSalesInvoiceToPrint(sysDocID, voucherID, mergeMatrixItems: false, showLotDetail: false);
					return PrintHelper.GetPrintDocument(data, "Sales Invoice", sysDocID, SysDocTypes.SalesInvoice);
				case SysDocTypes.SalesReceipt:
					data = Factory.SalesInvoiceSystem.GetSalesInvoiceToPrint(sysDocID, voucherID, mergeMatrixItems: false, showLotDetail: false);
					return PrintHelper.GetPrintDocument(data, "Cash Sale", sysDocID, SysDocTypes.SalesReceipt);
				case SysDocTypes.SalesPOS:
					data = Factory.SalesPOSSystem.GetSalesPOSToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Cash Sale", sysDocID, SysDocTypes.SalesPOS);
				case SysDocTypes.ExportSalesInvoice:
					data = Factory.SalesInvoiceSystem.GetSalesInvoiceToPrint(sysDocID, voucherID, mergeMatrixItems: false, showLotDetail: false);
					return PrintHelper.GetPrintDocument(data, "Export Sales Invoice", sysDocID, SysDocTypes.ExportSalesInvoice);
				case SysDocTypes.JobInvoice:
					data = Factory.JobInvoiceSystem.GetJobInvoiceToPrint(sysDocID, voucherID, mergeMatrixItems: false);
					return PrintHelper.GetPrintDocument(data, "Job Invoice", sysDocID, SysDocTypes.JobInvoice);
				case SysDocTypes.JobInventoryIssue:
					data = Factory.JobInventoryIssueSystem.GetJobInventoryIssueToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Project Inventory Issue", sysDocID, SysDocTypes.JobInventoryIssue);
				case SysDocTypes.SalesOrder:
					data = Factory.SalesOrderSystem.GetSalesOrderToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Sales Order", sysDocID, SysDocTypes.SalesOrder);
				case SysDocTypes.ConsignOutSettlement:
					data = Factory.ConsignOutSettlementSystem.GetSettlementToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Consignment Out Settlement", sysDocID, SysDocTypes.ConsignOutSettlement);
				case SysDocTypes.SalesQuote:
					data = Factory.SalesQuoteSystem.GetSalesQuoteToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Sales Quote", sysDocID, SysDocTypes.SalesQuote);
				case SysDocTypes.CashSalesReturn:
					data = Factory.SalesReturnSystem.GetSalesReturnToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Cash Sales Return", sysDocID, SysDocTypes.CashSalesReturn);
				case SysDocTypes.CreditSalesReturn:
					data = Factory.SalesReturnSystem.GetSalesReturnToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Credit Sales Return", sysDocID, SysDocTypes.CreditNote);
				case SysDocTypes.DeliveryNote:
					data = Factory.DeliveryNoteSystem.GetDeliveryNoteToPrint(sysDocID, voucherID, showLotDetail: false);
					return PrintHelper.GetPrintDocument(data, "Delivery Note", sysDocID, SysDocTypes.DeliveryNote);
				case SysDocTypes.DeliveryReturn:
					data = Factory.DeliveryReturnSystem.GetDeliveryReturnToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Delivery Return", sysDocID, SysDocTypes.DeliveryReturn);
				case SysDocTypes.PurchaseInvoice:
					data = Factory.PurchaseInvoiceSystem.GetPurchaseInvoiceToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Purchase Invoice", sysDocID, SysDocTypes.PurchaseInvoice);
				case SysDocTypes.CashPurchase:
					data = Factory.PurchaseInvoiceSystem.GetPurchaseInvoiceToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Cash Purchase", sysDocID, SysDocTypes.CashPurchase);
				case SysDocTypes.ImportPurchaseInvoice:
					data = Factory.PurchaseInvoiceSystem.GetPurchaseInvoiceToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Import Purchase Invoice", sysDocID, SysDocTypes.ImportPurchaseInvoice);
				case SysDocTypes.PurchaseOrder:
					data = Factory.PurchaseOrderSystem.GetPurchaseOrderToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Purchase Order", sysDocID, SysDocTypes.PurchaseOrder);
				case SysDocTypes.ImportPurchaseOrder:
					data = Factory.PurchaseOrderSystem.GetPurchaseOrderToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Import Purchase Order", sysDocID, SysDocTypes.ImportPurchaseOrder);
				case SysDocTypes.PurchaseQuote:
					data = Factory.PurchaseQuoteSystem.GetPurchaseQuoteToPrint(sysDocID, voucherID, mergeMatrixItems: false);
					return PrintHelper.GetPrintDocument(data, "Purchase Quote", sysDocID, SysDocTypes.PurchaseQuote);
				case SysDocTypes.PackingList:
					data = Factory.POShipmentSystem.GetPOShipmentToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "PO Shipment", sysDocID, SysDocTypes.PackingList);
				case SysDocTypes.ProformaInvoice:
					data = Factory.PurchaseQuoteSystem.GetPurchaseQuoteToPrint(sysDocID, voucherID, mergeMatrixItems: false);
					return PrintHelper.GetPrintDocument(data, "Proforma Invoice", sysDocID, SysDocTypes.ProformaInvoice);
				case SysDocTypes.CreditPurchaseReturn:
					data = Factory.PurchaseReturnSystem.GetPurchaseReturnToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Credit Purchase Return", sysDocID, SysDocTypes.CreditPurchaseReturn);
				case SysDocTypes.CashPurchaseReturn:
					data = Factory.PurchaseReturnSystem.GetPurchaseReturnToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Cash Purchase Return", sysDocID, SysDocTypes.CashPurchaseReturn);
				case SysDocTypes.GRNReturn:
					data = Factory.GRNReturnSystem.GetGRNReturnToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "GRN Return", sysDocID, SysDocTypes.GRNReturn);
				case SysDocTypes.SendChequesToBank:
					data = Factory.SendChequeSystem.GetSendChequeToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Cheque Sent", sysDocID, SysDocTypes.ChequeDeposit);
				case SysDocTypes.GJournal:
					data = Factory.JournalSystem.GetJournalToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Journal Entry", sysDocID, SysDocTypes.GJournal);
				case SysDocTypes.DebitNote:
					data = Factory.TransactionSystem.GetTransactionToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Debit Note", sysDocID, SysDocTypes.DebitNote);
				case SysDocTypes.CreditNote:
					data = Factory.TransactionSystem.GetTransactionToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Credit Note", sysDocID, SysDocTypes.CreditNote);
				case SysDocTypes.FundTransfer:
					data = Factory.TransactionSystem.GetTransactionToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Fund Transfer", sysDocID, SysDocTypes.FundTransfer);
				case SysDocTypes.FixedAssetPurchase:
					data = Factory.FixedAssetPurchaseSystem.GetFixedAssetPurchaseToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Credit Note", sysDocID, SysDocTypes.FixedAssetPurchase);
				case SysDocTypes.FixedAssetPurchaseOrder:
					data = Factory.FixedAssetPurchaseOrderSystem.GetPurchaseOrderToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Fixed Asset Purchase Order", sysDocID, SysDocTypes.FixedAssetPurchaseOrder);
				case SysDocTypes.TR:
					data = Factory.BankFacilityTransactionSystem.GetBankFacilityTransactionToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "TR Entry", sysDocID, SysDocTypes.TR);
				case SysDocTypes.TRPayment:
					data = Factory.BankFacilityPaymentSystem.GetBankFacilityPaymentToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "TR Payment", sysDocID, SysDocTypes.TRPayment);
				case SysDocTypes.TRApplication:
					data = Factory.TRApplicationSystem.GetTRApplicationToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "TR Application", sysDocID, SysDocTypes.TRApplication);
				case SysDocTypes.ChequeExpense:
					data = Factory.TransactionSystem.GetTransactionToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Cheque Expense", sysDocID, SysDocTypes.ChequeExpense);
				case SysDocTypes.CashExpense:
					data = Factory.TransactionSystem.GetTransactionToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Cash Expense", sysDocID, SysDocTypes.CashExpense);
				case SysDocTypes.ChequePayment:
					data = Factory.TransactionSystem.GetTransactionToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Cheque Payment", sysDocID, SysDocTypes.ChequePayment);
				case SysDocTypes.ChequeReceipt:
					data = Factory.TransactionSystem.GetTransactionToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Cheque Receipt", sysDocID, SysDocTypes.ChequeReceipt);
				case SysDocTypes.TTReceipt:
					data = Factory.TransactionSystem.GetTransactionToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "TT Receipt", sysDocID, SysDocTypes.TTReceipt);
				case SysDocTypes.CashReceipt:
					data = Factory.TransactionSystem.GetTransactionToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Cash Receipt", sysDocID, SysDocTypes.CashReceipt);
				case SysDocTypes.TTPayment:
					data = Factory.TransactionSystem.GetTransactionToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "TT Payment", sysDocID, SysDocTypes.TTPayment);
				case SysDocTypes.CashPayment:
					data = Factory.TransactionSystem.GetTransactionToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Cash Payment", sysDocID, SysDocTypes.CashPayment);
				case SysDocTypes.AssemblyBuild:
					data = Factory.AssemblyBuildSystem.GetAssemblyBuildToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Assembly Build", sysDocID, SysDocTypes.AssemblyBuild);
				case SysDocTypes.InventoryAdjustment:
					data = Factory.InventoryAdjustmentSystem.GetInventoryAdjustmentToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Inventory Adjustment", sysDocID, SysDocTypes.InventoryAdjustment);
				case SysDocTypes.InventoryNoneSale:
					data = Factory.InventoryDamageSystem.GetInventoryDamageToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Inventory None Sale", sysDocID, SysDocTypes.InventoryNoneSale);
				case SysDocTypes.InventoryRepacking:
					data = Factory.InventoryRepackingSystem.GetInventoryRepackingToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Inventory Repacking", sysDocID, SysDocTypes.InventoryRepacking);
				case SysDocTypes.EmployeeGeneralActivity:
					data = Factory.EmployeeActivitySystem.GetEmployeeGeneralActivityToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "EmployeeGeneralActivity", sysDocID, SysDocTypes.EmployeeGeneralActivity);
				case SysDocTypes.JobMaterialRequisition:
					data = Factory.JobMaterialRequisitionSystem.GetJobMaterialRequisitionToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Project Material Requisition", sysDocID, SysDocTypes.JobMaterialRequisition);
				case SysDocTypes.ProjectSubContractPO:
					data = Factory.ProjectSubContractSystem.GetProjectSubContractPOToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "ProjectSubContractPO", sysDocID, SysDocTypes.ProjectSubContractPO);
				case SysDocTypes.ProjectSubContractPI:
					data = Factory.ProjectSubContractPISystem.GetProjectSubContractPIToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "ProjectSubContractPI", sysDocID, SysDocTypes.ProjectSubContractPI);
				case SysDocTypes.ServiceCallTrack:
					data = Factory.ServiceCallTrackSystem.GetJobMaterialRequisitionToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Service Call Track", sysDocID, SysDocTypes.ServiceCallTrack);
				case SysDocTypes.JobEstimation:
					data = Factory.JobEstimationSystem.GetJobEstimationToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Job Estimation", sysDocID, SysDocTypes.JobEstimation);
				case SysDocTypes.JobTimesheet:
					data = Factory.JobTimesheetSystem.GetJobTimesheetToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Job Timesheet", sysDocID, SysDocTypes.JobTimesheet);
				case SysDocTypes.JobExpenseIssue:
					data = Factory.JobExpenseIssueSystem.GetJobExpenseIssueToPrint(sysDocID, voucherID);
					return PrintHelper.GetPrintDocument(data, "Job ExpenseIssue", sysDocID, SysDocTypes.JobExpenseIssue);
				default:
					switch (sysDocTypes)
					{
					case SysDocTypes.JobInventoryIssue:
						data = Factory.JobInventoryIssueSystem.GetJobInventoryIssueToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "Job InventoryIssue", sysDocID, SysDocTypes.JobInventoryIssue);
					case SysDocTypes.JobInventoryReturn:
						data = Factory.JobInventoryReturnSystem.GetJobInventoryReturnToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "Job InventoryReturn", sysDocID, SysDocTypes.JobInventoryReturn);
					case SysDocTypes.JobInvoice:
						data = Factory.JobInvoiceSystem.GetJobInvoiceToPrint(sysDocID, voucherID, mergeMatrixItems: false);
						return PrintHelper.GetPrintDocument(data, "Job Invoice", sysDocID, SysDocTypes.JobInvoice);
					case SysDocTypes.JobClosing:
						return PrintHelper.GetPrintDocument(data, "Job Closing", sysDocID, SysDocTypes.JobClosing);
					case SysDocTypes.JobMaintenanceServiceEntry:
						data = Factory.JobMaintenanceServiceSystem.GetJobMaintenanceServiceToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "Job Maintenance Service Entry", sysDocID, SysDocTypes.JobMaintenanceServiceEntry);
					case SysDocTypes.OverTimeEntry:
						data = Factory.OverTimeEntrySystem.GetOverTimeEntryToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "Over Time Entry", sysDocID, SysDocTypes.OverTimeEntry);
					case SysDocTypes.SalaryPaymentCash:
						data = Factory.PayrollTransactionSystem.GetEmployeeSalaryToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "Salary Payment Cash", sysDocID, SysDocTypes.SalaryPaymentCash);
					case SysDocTypes.SalaryPaymentBank:
						data = Factory.PayrollTransactionSystem.GetEmployeeSalaryToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "Salary Payment Bank", sysDocID, SysDocTypes.SalaryPaymentBank);
					case SysDocTypes.SalaryPaymentCheque:
						data = Factory.PayrollTransactionSystem.GetEmployeeSalaryToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "Salary Payment Cheque", sysDocID, SysDocTypes.SalaryPaymentCheque);
					case SysDocTypes.ProjectExpenseAllocation:
						data = Factory.ProjectExpenseAllocationSystem.GetEmployeeSalaryToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "Project Expense Allocation", sysDocID, SysDocTypes.ProjectExpenseAllocation);
					case SysDocTypes.CashReceiptMultiple:
						data = Factory.TransactionSystem.GetTransactionToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "Cash Receipt-Multiple Payee", sysDocID, SysDocTypes.CashReceiptMultiple);
					case SysDocTypes.SalarySheet:
						data = Factory.SalarySheetSystem.GetSalarySheetByID(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "Salary Sheet", sysDocID, SysDocTypes.SalarySheet);
					case SysDocTypes.ChequeReceipt:
						data = Factory.TransactionSystem.GetTransactionToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "Cheque Receipt", sysDocID, SysDocTypes.ChequeReceipt);
					case SysDocTypes.LPOReceipt:
						data = Factory.LPOReceiptSystem.GetLPOReceiptToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "LPO Receipt", sysDocID, SysDocTypes.LPOReceipt);
					case SysDocTypes.SendChequesToBank:
						data = Factory.LPOReceiptSystem.GetLPOReceiptToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "LPO Receipt", sysDocID, SysDocTypes.SendChequesToBank);
					case SysDocTypes.ChequeDeposit:
						data = Factory.TransactionSystem.GetTransactionToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "LPO Receipt", sysDocID, SysDocTypes.ChequeDeposit);
					case SysDocTypes.ChequeDiscount:
						data = Factory.LPOReceiptSystem.GetLPOReceiptToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "Cheque Deposit", sysDocID, SysDocTypes.ChequeDiscount);
					case SysDocTypes.ReturnedCheque:
						data = Factory.TransactionSystem.GetTransactionToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "LPO Receipt", sysDocID, SysDocTypes.ReturnedCheque);
					case SysDocTypes.ReceivedChequeCancellation:
						data = Factory.TransactionSystem.GetTransactionToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "LPO Receipt", sysDocID, SysDocTypes.ReceivedChequeCancellation);
					case SysDocTypes.IssuedChequeClearance:
						data = Factory.TransactionSystem.GetTransactionToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "LPO Receipt", sysDocID, SysDocTypes.IssuedChequeClearance);
					case SysDocTypes.IssuedChequeReturn:
						data = Factory.TransactionSystem.GetTransactionToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "LPO Receipt", sysDocID, SysDocTypes.IssuedChequeReturn);
					case SysDocTypes.IssuedChequeCancellation:
						data = Factory.TransactionSystem.GetTransactionToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "LPO Receipt", sysDocID, SysDocTypes.IssuedChequeCancellation);
					case SysDocTypes.IssuedSecurityCheque:
						data = Factory.IssuedChequeSystem.GetSecurityChequeToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "LPO Receipt", sysDocID, SysDocTypes.IssuedSecurityCheque);
					case SysDocTypes.PurchaseOrderNI:
						data = Factory.PurchaseOrderNISystem.GetPurchaseOrderToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "PO NonInv", sysDocID, SysDocTypes.PurchaseOrderNI);
					case SysDocTypes.MaintenanceEntry:
						data = Factory.MaintenanceEntrySystem.GetMaintenanceEntryToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "Vehicle Maintenance Entry", sysDocID, SysDocTypes.MaintenanceEntry);
					case SysDocTypes.ConsignInSettlement:
						data = Factory.ConsignInSettlementSystem.GetSettlementToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "Consign In Settlement", sysDocID, SysDocTypes.ConsignInSettlement);
					case SysDocTypes.PropertyServiceInvoice:
						data = Factory.SalesInvoiceNISystem.GetSalesInvoiceToPrint(sysDocID, voucherID, mergeMatrixItems: false, showLotDetail: false, NonInventoryInvoiceType.PropertySalesInvoice);
						return PrintHelper.GetPrintDocument(data, "Property Service Invoice", sysDocID, SysDocTypes.PropertyServiceInvoice);
					case SysDocTypes.PropertyRental:
						data = Factory.PropertyRentSystem.GetPropertyRentToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "Property Rental", sysDocID, SysDocTypes.PropertyRental);
					case SysDocTypes.PropertyRenew:
						data = Factory.PropertyRentSystem.GetPropertyRentToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "Property Rental", sysDocID, SysDocTypes.PropertyRenew);
					case SysDocTypes.PropertyCancel:
						data = Factory.PropertyCancelSystem.GetPropertyCancelToPrint(sysDocID, voucherID);
						return PrintHelper.GetPrintDocument(data, "Property Cancel", sysDocID, SysDocTypes.PropertyCancel);
					default:
						throw new Exception("Preview Transaction not implimented for type: " + sysDocTypes.ToString());
					}
				}
			}
			catch
			{
				throw;
			}
		}

		private void GetTransactionMultiplePrintDocument(string sysDocID, string voucherID, object templateName, object isPrint)
		{
			try
			{
				SysDocTypes sysDocTypes = (SysDocTypes)int.Parse(Factory.DatabaseSystem.GetFieldValue("System_Document", "SysDocType", "SysDocID", sysDocID).ToString());
				switch (sysDocTypes)
				{
				case SysDocTypes.SalesInvoice:
					PrintHelper.PrintDocument(Factory.SalesInvoiceSystem.GetSalesInvoiceToPrint(sysDocID, voucherID, mergeMatrixItems: false, showLotDetail: false), sysDocID, templateName.ToString(), SysDocTypes.SalesInvoice, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.SalesReceipt:
					PrintHelper.PrintDocument(Factory.SalesInvoiceSystem.GetSalesInvoiceToPrint(sysDocID, voucherID, mergeMatrixItems: false, showLotDetail: false), sysDocID, templateName.ToString(), SysDocTypes.SalesReceipt, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.ExportSalesInvoice:
					PrintHelper.PrintDocument(Factory.SalesInvoiceSystem.GetSalesInvoiceToPrint(sysDocID, voucherID, mergeMatrixItems: false, showLotDetail: false), sysDocID, templateName.ToString(), SysDocTypes.ExportSalesInvoice, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.ExportSalesProfoma:
					PrintHelper.PrintDocument(Factory.SalesProformaSystem.GetSalesOrderToPrint(sysDocID, voucherID), sysDocID, templateName.ToString(), SysDocTypes.ExportSalesProfoma, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.SalesEnquiry:
					PrintHelper.PrintDocument(Factory.SalesEnquirySystem.GetSalesOrderToPrint(sysDocID, voucherID), sysDocID, templateName.ToString(), SysDocTypes.SalesEnquiry, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.SalesQuote:
					PrintHelper.PrintDocument(Factory.SalesQuoteSystem.GetSalesQuoteToPrint(sysDocID, voucherID), sysDocID, templateName.ToString(), SysDocTypes.SalesQuote, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.SalesOrder:
					PrintHelper.PrintDocument(Factory.SalesOrderSystem.GetSalesOrderToPrint(sysDocID, voucherID), sysDocID, templateName.ToString(), SysDocTypes.SalesOrder, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.SalesProforma:
					PrintHelper.PrintDocument(Factory.SalesProformaSystem.GetSalesOrderToPrint(sysDocID, voucherID), sysDocID, templateName.ToString(), SysDocTypes.SalesProforma, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.DeliveryNote:
					PrintHelper.PrintDocument(Factory.DeliveryNoteSystem.GetDeliveryNoteToPrint(sysDocID, voucherID, showLotDetail: false), sysDocID, templateName.ToString(), SysDocTypes.DeliveryNote, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.DeliveryReturn:
					PrintHelper.PrintDocument(Factory.DeliveryReturnSystem.GetDeliveryReturnToPrint(sysDocID, voucherID), sysDocID, templateName.ToString(), SysDocTypes.DeliveryReturn, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.ExportDeliveryNote:
					PrintHelper.PrintDocument(Factory.DeliveryNoteSystem.GetDeliveryNoteToPrint(sysDocID, voucherID, showLotDetail: false), sysDocID, templateName.ToString(), SysDocTypes.ExportDeliveryNote, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.ExportPackingList:
					PrintHelper.PrintDocument(Factory.ExportPackingListSystem.GetExportPackingListToPrint(sysDocID, voucherID), sysDocID, templateName.ToString(), SysDocTypes.ExportPackingList, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.ExportSalesOrder:
					PrintHelper.PrintDocument(Factory.SalesOrderSystem.GetSalesOrderToPrint(sysDocID, voucherID), sysDocID, templateName.ToString(), SysDocTypes.ExportSalesOrder, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.CashSalesReturn:
					PrintHelper.PrintDocument(Factory.SalesReturnSystem.GetSalesReturnToPrint(sysDocID, voucherID), sysDocID, templateName.ToString(), SysDocTypes.CashSalesReturn, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.CreditSalesReturn:
					PrintHelper.PrintDocument(Factory.SalesReturnSystem.GetSalesReturnToPrint(sysDocID, voucherID), sysDocID, templateName.ToString(), SysDocTypes.CreditSalesReturn, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.ConsignOut:
					PrintHelper.PrintDocument(Factory.ConsignOutSystem.GetConsignOutToPrint(sysDocID, voucherID), sysDocID, templateName.ToString(), SysDocTypes.ConsignOut, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.ConsignOutReturn:
					PrintHelper.PrintDocument(Factory.ConsignOutReturnSystem.GetConsignOutReturnToPrint(sysDocID, voucherID), sysDocID, templateName.ToString(), SysDocTypes.ConsignOutReturn, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.ConsignOutSettlement:
					PrintHelper.PrintDocument(Factory.ConsignOutSettlementSystem.GetSettlementToPrint(sysDocID, voucherID), sysDocID, templateName.ToString(), SysDocTypes.ConsignOutSettlement, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.ExportPickList:
					PrintHelper.PrintDocument(Factory.ExportPickListSystem.GetExportPickListToPrint(sysDocID, voucherID, showLotDetail: false), sysDocID, templateName.ToString(), SysDocTypes.ExportPickList, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.JobInvoice:
					PrintHelper.PrintDocument(Factory.JobInvoiceSystem.GetJobInvoiceToPrint(sysDocID, voucherID, mergeMatrixItems: false), sysDocID, templateName.ToString(), SysDocTypes.JobInvoice, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.TRApplication:
					PrintHelper.PrintDocument(Factory.TRApplicationSystem.GetTRApplicationToPrint(sysDocID, voucherID), sysDocID, templateName.ToString(), SysDocTypes.TRApplication, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.PropertyServiceInvoice:
					PrintHelper.PrintDocument(Factory.SalesInvoiceNISystem.GetSalesInvoiceToPrint(sysDocID, voucherID, mergeMatrixItems: false, showLotDetail: false, NonInventoryInvoiceType.PropertySalesInvoice), sysDocID, templateName.ToString(), SysDocTypes.PropertyServiceInvoice, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.PropertyRental:
					PrintHelper.PrintDocument(Factory.PropertyRentSystem.GetPropertyRentToPrint(sysDocID, voucherID), sysDocID, templateName.ToString(), SysDocTypes.PropertyRental, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.PropertyRenew:
					PrintHelper.PrintDocument(Factory.PropertyRentSystem.GetPropertyRentToPrint(sysDocID, voucherID), sysDocID, templateName.ToString(), SysDocTypes.PropertyRenew, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.PropertyCancel:
					PrintHelper.PrintDocument(Factory.PropertyCancelSystem.GetPropertyCancelToPrint(sysDocID, voucherID), sysDocID, templateName.ToString(), SysDocTypes.PropertyCancel, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.PurchaseOrder:
					PrintHelper.PrintDocument(Factory.PurchaseOrderSystem.GetPurchaseOrderToPrint(sysDocID, voucherID), sysDocID, templateName.ToString(), SysDocTypes.PurchaseOrder, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				case SysDocTypes.ImportPurchaseOrder:
					PrintHelper.PrintDocument(Factory.PurchaseOrderSystem.GetPurchaseOrderToPrint(sysDocID, voucherID), sysDocID, templateName.ToString(), SysDocTypes.ImportPurchaseOrder, bool.Parse(isPrint.ToString()), showPrintDialog: false);
					break;
				default:
					throw new Exception("Preview Transaction not implimented for type: " + sysDocTypes.ToString());
				}
			}
			catch
			{
				throw;
			}
		}

		public void EditTransaction(string sysDocID, string voucherID)
		{
			try
			{
				if (!(voucherID == "0000000"))
				{
					if (sysDocID == "SYS_010")
					{
						FormActivator.BringFormToFront(FormActivator.CustomerOpeningBalanceBatchFormObj);
						if (voucherID != "")
						{
							FormActivator.CustomerOpeningBalanceBatchFormObj.EditDocument(sysDocID, voucherID);
						}
					}
					else if (sysDocID == "SYS_011")
					{
						FormActivator.BringFormToFront(FormActivator.VendorOpeningBalanceBatchFormObj);
						if (voucherID != "")
						{
							FormActivator.VendorOpeningBalanceBatchFormObj.EditDocument(sysDocID, voucherID);
						}
					}
					else if (sysDocID == "SYS_012")
					{
						FormActivator.BringFormToFront(FormActivator.EmployeeOpeningBalanceBatchFormObj);
						if (voucherID != "")
						{
							FormActivator.EmployeeOpeningBalanceBatchFormObj.EditDocument(sysDocID, voucherID);
						}
					}
					else
					{
						SysDocTypes sysDocTypes = (SysDocTypes)int.Parse(Factory.DatabaseSystem.GetFieldValue("System_Document", "SysDocType", "SysDocID", sysDocID).ToString());
						switch (sysDocTypes)
						{
						case SysDocTypes.SalesInvoice:
							FormActivator.BringFormToFront(FormActivator.SalesInvoiceFormObj);
							if (voucherID != "")
							{
								FormActivator.SalesInvoiceFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.SalesReceipt:
							FormActivator.BringFormToFront(FormActivator.SalesReceiptFormObj);
							if (voucherID != "")
							{
								FormActivator.SalesReceiptFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.ExportSalesInvoice:
							FormActivator.BringFormToFront(FormActivator.ExportSalesInvoiceFormObj);
							if (voucherID != "")
							{
								FormActivator.ExportSalesInvoiceFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.JobInvoice:
							FormActivator.BringFormToFront(FormActivator.JobInvoiceFormObj);
							if (voucherID != "")
							{
								FormActivator.JobInvoiceFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.JobInventoryIssue:
							FormActivator.BringFormToFront(FormActivator.JobInventoryIssueFormObj);
							if (voucherID != "")
							{
								FormActivator.JobInventoryIssueFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.SalesEnquiry:
							FormActivator.BringFormToFront(FormActivator.SalesEnquiryFormObj);
							if (voucherID != "")
							{
								FormActivator.SalesEnquiryFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.SalesOrder:
							FormActivator.BringFormToFront(FormActivator.SalesOrderFormObj);
							if (voucherID != "")
							{
								FormActivator.SalesOrderFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.ConsignOutSettlement:
							FormActivator.BringFormToFront(FormActivator.ConsignOutSettlementFormObj);
							if (voucherID != "")
							{
								FormActivator.ConsignOutSettlementFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.SalesQuote:
							FormActivator.BringFormToFront(FormActivator.SalesQuoteFormObj);
							if (voucherID != "")
							{
								FormActivator.SalesQuoteFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.SalesInvoiceNI:
							FormActivator.BringFormToFront(FormActivator.SalesInvoiceNIFormObj);
							if (voucherID != "")
							{
								FormActivator.SalesInvoiceNIFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.TRApplication:
							FormActivator.BringFormToFront(FormActivator.TRApplicationFormObj);
							if (voucherID != "")
							{
								FormActivator.TRApplicationFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.CashSalesReturn:
							FormActivator.BringFormToFront(FormActivator.SalesReturnCashFormObj);
							if (voucherID != "")
							{
								FormActivator.SalesReturnCashFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.CreditSalesReturn:
							FormActivator.BringFormToFront(FormActivator.SalesReturnCreditFormObj);
							if (voucherID != "")
							{
								FormActivator.SalesReturnCreditFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.DeliveryNote:
							FormActivator.BringFormToFront(FormActivator.DeliveryNoteFormObj);
							if (voucherID != "")
							{
								FormActivator.DeliveryNoteFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.ExportDeliveryNote:
							FormActivator.BringFormToFront(FormActivator.ExportDeliveryNoteFormObj);
							if (voucherID != "")
							{
								FormActivator.ExportDeliveryNoteFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.DeliveryReturn:
							FormActivator.BringFormToFront(FormActivator.DeliveryReturnFormObj);
							if (voucherID != "")
							{
								FormActivator.DeliveryReturnFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.PurchaseInvoice:
							FormActivator.BringFormToFront(FormActivator.PurchaseInvoiceFormObj);
							if (voucherID != "")
							{
								FormActivator.PurchaseInvoiceFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.CashPurchase:
							FormActivator.BringFormToFront(FormActivator.CashPurchaseFormObj);
							if (voucherID != "")
							{
								FormActivator.CashPurchaseFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.ImportPurchaseInvoice:
							FormActivator.BringFormToFront(FormActivator.PurchaseInvoiceImportFormObj);
							if (voucherID != "")
							{
								FormActivator.PurchaseInvoiceImportFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.PurchaseOrder:
							FormActivator.BringFormToFront(FormActivator.PurchaseOrderFormObj);
							if (voucherID != "")
							{
								FormActivator.PurchaseOrderFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.ImportPurchaseOrder:
							FormActivator.BringFormToFront(FormActivator.PurchaseOrderImportFormObj);
							if (voucherID != "")
							{
								FormActivator.PurchaseOrderImportFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.PurchaseQuote:
							FormActivator.BringFormToFront(FormActivator.PurchaseQuoteFormObj);
							if (voucherID != "")
							{
								FormActivator.PurchaseQuoteFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.PackingList:
							FormActivator.BringFormToFront(FormActivator.POShipmentFormObj);
							if (voucherID != "")
							{
								FormActivator.POShipmentFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.ProformaInvoice:
							FormActivator.BringFormToFront(FormActivator.ProformaInvoiceFormObj);
							if (voucherID != "")
							{
								FormActivator.ProformaInvoiceFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.CreditPurchaseReturn:
							FormActivator.BringFormToFront(FormActivator.PurchaseReturnCreditFormObj);
							if (voucherID != "")
							{
								FormActivator.PurchaseReturnCreditFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.CashPurchaseReturn:
							FormActivator.BringFormToFront(FormActivator.PurchaseReturnCashFormObj);
							if (voucherID != "")
							{
								FormActivator.PurchaseReturnCashFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.GRNReturn:
							FormActivator.BringFormToFront(FormActivator.GRNReturnFormObj);
							if (voucherID != "")
							{
								FormActivator.GRNReturnFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.SendChequesToBank:
							FormActivator.BringFormToFront(FormActivator.SendChequesToBankFormObj);
							if (voucherID != "")
							{
								FormActivator.SendChequesToBankFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.JobMaterialRequisition:
							FormActivator.BringFormToFront(FormActivator.JobMaterialRequesitionFormObj);
							if (voucherID != "")
							{
								FormActivator.JobMaterialRequesitionFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.GoodsReceivedNote:
							FormActivator.BringFormToFront(FormActivator.PurchaseReceiptFormObj);
							if (voucherID != "")
							{
								FormActivator.PurchaseReceiptFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.ImportGoodsReceivedNote:
							FormActivator.BringFormToFront(FormActivator.ImportPurchaseGRNFormObj);
							if (voucherID != "")
							{
								FormActivator.ImportPurchaseGRNFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.GJournal:
							FormActivator.BringFormToFront(FormActivator.JournalEntryFormObj);
							if (voucherID != "")
							{
								FormActivator.JournalEntryFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.DebitNote:
							FormActivator.BringFormToFront(FormActivator.DebitNoteEntryFormObj);
							if (voucherID != "")
							{
								FormActivator.DebitNoteEntryFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.CreditNote:
							FormActivator.BringFormToFront(FormActivator.CreditNoteEntryFormObj);
							if (voucherID != "")
							{
								FormActivator.CreditNoteEntryFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.FundTransfer:
							FormActivator.BringFormToFront(FormActivator.FundTransferFormObj);
							if (voucherID != "")
							{
								FormActivator.FundTransferFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.FixedAssetPurchase:
							FormActivator.BringFormToFront(FormActivator.FixedAssetPurchaseFormObj);
							if (voucherID != "")
							{
								FormActivator.FixedAssetPurchaseFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.FixedAssetTransfer:
							FormActivator.BringFormToFront(FormActivator.FixedAssetTransferFormObj);
							if (voucherID != "")
							{
								FormActivator.FixedAssetTransferFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.TR:
							FormActivator.BringFormToFront(FormActivator.TREntryFormObj);
							if (voucherID != "")
							{
								FormActivator.TREntryFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.TRPayment:
							FormActivator.BringFormToFront(FormActivator.TRPaymentFormObj);
							if (voucherID != "")
							{
								FormActivator.TRPaymentFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.ChequeExpense:
							FormActivator.BringFormToFront(FormActivator.ChequeExpenseEntryFormObj);
							if (voucherID != "")
							{
								FormActivator.ChequeExpenseEntryFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.CashExpense:
							FormActivator.BringFormToFront(FormActivator.CashPaymentParentFormObj);
							if (voucherID != "")
							{
								FormActivator.CashPaymentParentFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.ChequeReceipt:
							FormActivator.BringFormToFront(FormActivator.ChequeReceiptFormObj);
							if (voucherID != "")
							{
								FormActivator.ChequeReceiptFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.ChequeReceiptMultiple:
							FormActivator.BringFormToFront(FormActivator.ChequeReceiptMultiEntryFormObj);
							if (voucherID != "")
							{
								FormActivator.ChequeReceiptMultiEntryFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.OpeningChequeReceipt:
							FormActivator.BringFormToFront(FormActivator.OpeningChequeReceiptEntryFormObj);
							if (voucherID != "")
							{
								FormActivator.OpeningChequeReceiptEntryFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.TTReceipt:
							FormActivator.BringFormToFront(FormActivator.TTReceiptFormObj);
							if (voucherID != "")
							{
								FormActivator.TTReceiptFormObj.EditDocument(sysDocID, voucherID);
							}
							else
							{
								FormActivator.BringFormToFront(FormActivator.CashReceiptFormObj);
								if (voucherID != "")
								{
									FormActivator.CashReceiptFormObj.EditDocument(sysDocID, voucherID);
								}
							}
							break;
						case SysDocTypes.CashReceipt:
							FormActivator.BringFormToFront(FormActivator.CashReceiptFormObj);
							if (voucherID != "")
							{
								FormActivator.CashReceiptFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.ChequePayment:
							FormActivator.BringFormToFront(FormActivator.ChequePaymentFormObj);
							if (voucherID != "")
							{
								FormActivator.ChequePaymentFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.TTPayment:
							FormActivator.BringFormToFront(FormActivator.TTPaymentFormObj);
							if (voucherID != "")
							{
								FormActivator.TTPaymentFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.CashPayment:
							FormActivator.BringFormToFront(FormActivator.CashPaymentParentFormObj);
							if (voucherID != "")
							{
								FormActivator.CashPaymentParentFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.CRMActivity:
							FormActivator.BringFormToFront(FormActivator.ActivityDetailsFormObj);
							if (voucherID != "")
							{
								FormActivator.ActivityDetailsFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.AssemblyBuild:
							FormActivator.BringFormToFront(FormActivator.BuildAssemblyFormObj);
							if (voucherID != "")
							{
								FormActivator.BuildAssemblyFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.QualityClaim:
							FormActivator.BringFormToFront(FormActivator.QualityClaimFormObj);
							if (voucherID != "")
							{
								FormActivator.QualityClaimFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.InventoryRepacking:
							FormActivator.BringFormToFront(FormActivator.InventoryRepackingFormObj);
							if (voucherID != "")
							{
								FormActivator.InventoryRepackingFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.TransitTransferIn:
							FormActivator.BringFormToFront(FormActivator.InventoryTransferAcceptanceFormObj);
							if (voucherID != "")
							{
								FormActivator.InventoryTransferAcceptanceFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.TransitTransferOut:
							FormActivator.BringFormToFront(FormActivator.InventoryTransferFormObj);
							if (voucherID != "")
							{
								FormActivator.InventoryTransferFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.ConsignIn:
							FormActivator.BringFormToFront(FormActivator.ConsignInFormObj);
							if (voucherID != "")
							{
								FormActivator.ConsignInFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.SalesProforma:
							FormActivator.BringFormToFront(FormActivator.SalesProformaInvoiceFormObj);
							if (voucherID != "")
							{
								FormActivator.SalesProformaInvoiceFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.InventoryAdjustment:
							FormActivator.BringFormToFront(FormActivator.InventoryAdjustmentsFormObj);
							if (voucherID != "")
							{
								FormActivator.InventoryAdjustmentsFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.ChequeDeposit:
							FormActivator.BringFormToFront(FormActivator.ChequeDepositFormObj);
							if (voucherID != "")
							{
								FormActivator.ChequeDepositFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.LPOReceipt:
							FormActivator.BringFormToFront(FormActivator.LPOReceiptFormObj);
							if (voucherID != "")
							{
								FormActivator.LPOReceiptFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.InventoryNoneSale:
							FormActivator.BringFormToFront(FormActivator.InventoryDamageFormObj);
							if (voucherID != "")
							{
								FormActivator.InventoryDamageFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						case SysDocTypes.ConsignOut:
							FormActivator.BringFormToFront(FormActivator.ConsignOutFormObj);
							if (voucherID != "")
							{
								FormActivator.ConsignOutFormObj.EditDocument(sysDocID, voucherID);
							}
							break;
						default:
							switch (sysDocTypes)
							{
							case SysDocTypes.ConsignIn:
								FormActivator.BringFormToFront(FormActivator.ConsignInFormObj);
								if (voucherID != "")
								{
									FormActivator.ConsignInFormObj.EditDocument(sysDocID, voucherID);
								}
								break;
							case SysDocTypes.ProjectSubContractPI:
								FormActivator.BringFormToFront(FormActivator.ProjectSubContractPIFormObj);
								if (voucherID != "")
								{
									FormActivator.ProjectSubContractPIFormObj.EditDocument(sysDocID, voucherID);
								}
								break;
							case SysDocTypes.ProjectSubContractPO:
								FormActivator.BringFormToFront(FormActivator.ProjectSubContractPOFormObj);
								if (voucherID != "")
								{
									FormActivator.ProjectSubContractPOFormObj.EditDocument(sysDocID, voucherID);
								}
								break;
							case SysDocTypes.PurchaseOrderNI:
								FormActivator.BringFormToFront(FormActivator.PurchaseOrderNonInvFormObj);
								if (voucherID != "")
								{
									FormActivator.PurchaseOrderNonInvFormObj.EditDocument(sysDocID, voucherID);
								}
								break;
							case SysDocTypes.MaintenanceEntry:
								FormActivator.BringFormToFront(FormActivator.VehicleMaintenanceEntryFormObj);
								if (voucherID != "")
								{
									FormActivator.VehicleMaintenanceEntryFormObj.EditDocument(sysDocID, voucherID);
								}
								break;
							case SysDocTypes.ConsignInSettlement:
								FormActivator.BringFormToFront(FormActivator.ConsignInSettlementFormObj);
								if (voucherID != "")
								{
									FormActivator.ConsignInSettlementFormObj.EditDocument(sysDocID, voucherID);
								}
								break;
							case SysDocTypes.PurchaseInvoiceNI:
								FormActivator.BringFormToFront(FormActivator.PurchaseInvoiceNonInvFormObj);
								if (voucherID != "")
								{
									FormActivator.PurchaseInvoiceNonInvFormObj.EditDocument(sysDocID, voucherID);
								}
								break;
							case SysDocTypes.PurchasePrepaymentInvoice:
								FormActivator.BringFormToFront(FormActivator.PurchasePrepaymentInvoiceFormObj);
								if (voucherID != "")
								{
									FormActivator.PurchasePrepaymentInvoiceFormObj.EditDocument(sysDocID, voucherID);
								}
								break;
							case SysDocTypes.IssuedChequeClearance:
								FormActivator.BringFormToFront(FormActivator.IssuedChequeClearanceFormObj);
								if (voucherID != "")
								{
									FormActivator.IssuedChequeClearanceFormObj.EditDocument(sysDocID, voucherID);
								}
								break;
							case SysDocTypes.PayrollTransaction:
							case SysDocTypes.SalaryPaymentBank:
								FormActivator.BringFormToFront(FormActivator.TransferSalaryPaymentFormObj);
								if (voucherID != "")
								{
									FormActivator.TransferSalaryPaymentFormObj.EditDocument(sysDocID, voucherID);
								}
								break;
							case SysDocTypes.SalaryPaymentCash:
								FormActivator.BringFormToFront(FormActivator.CashSalaryPaymentFormObj);
								if (voucherID != "")
								{
									FormActivator.CashSalaryPaymentFormObj.EditDocument(sysDocID, voucherID);
								}
								break;
							case SysDocTypes.SalaryPaymentCheque:
								FormActivator.BringFormToFront(FormActivator.ChequeSalaryPaymentFormObj);
								if (voucherID != "")
								{
									FormActivator.ChequeSalaryPaymentFormObj.EditDocument(sysDocID, voucherID);
								}
								break;
							case SysDocTypes.PropertyRental:
								FormActivator.BringFormToFront(FormActivator.PropertyRentDetailsFormObj);
								if (voucherID != "")
								{
									FormActivator.PropertyRentDetailsFormObj.EditDocument(sysDocID, voucherID);
								}
								break;
							case SysDocTypes.PropertyRenew:
								FormActivator.BringFormToFront(FormActivator.PropertyRenewDetailsFormObj);
								if (voucherID != "")
								{
									FormActivator.PropertyRenewDetailsFormObj.EditDocument(sysDocID, voucherID);
								}
								break;
							case SysDocTypes.OverTimeEntry:
								FormActivator.BringFormToFront(FormActivator.OverTimeEntryFormObj);
								if (voucherID != "")
								{
									FormActivator.OverTimeEntryFormObj.EditDocument(sysDocID, voucherID);
								}
								break;
							default:
								throw new Exception("EditTransaction not implemented for type: " + sysDocTypes.ToString());
							}
							break;
						}
					}
				}
			}
			catch
			{
				throw;
			}
		}

		public void EditTransaction(TransactionListType transactionType, string sysDocID, string voucherID)
		{
			if (!(sysDocID == "") && !(voucherID == ""))
			{
				EditTransaction(transactionType, sysDocID, voucherID, null);
			}
		}

		public void EditTransaction(TransactionListType transactionType, string sysDocID, string voucherID, object subType)
		{
			switch (transactionType)
			{
			case TransactionListType.SalesInvoice:
				if (subType == null || subType.ToString() == "Credit")
				{
					FormActivator.BringFormToFront(FormActivator.SalesInvoiceFormObj);
					if (voucherID != "")
					{
						FormActivator.SalesInvoiceFormObj.EditDocument(sysDocID, voucherID);
					}
				}
				else
				{
					FormActivator.BringFormToFront(FormActivator.SalesReceiptFormObj);
					if (voucherID != "")
					{
						FormActivator.SalesReceiptFormObj.EditDocument(sysDocID, voucherID);
					}
				}
				return;
			case TransactionListType.SalesInvoiceNI:
				FormActivator.BringFormToFront(FormActivator.SalesInvoiceNIFormObj);
				if (voucherID != "")
				{
					FormActivator.SalesInvoiceNIFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.SalesReceipt:
				FormActivator.BringFormToFront(FormActivator.SalesReceiptFormObj);
				if (voucherID != "")
				{
					FormActivator.SalesReceiptFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ExportSalesOrder:
				FormActivator.BringFormToFront(FormActivator.ExportSalesOrderFormObj);
				if (voucherID != "")
				{
					FormActivator.ExportSalesOrderFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ExportSalesProforma:
				FormActivator.BringFormToFront(FormActivator.ExportSalesProformaInvoiceFormObj);
				if (voucherID != "")
				{
					FormActivator.ExportSalesProformaInvoiceFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ExportSalesInvoice:
				FormActivator.BringFormToFront(FormActivator.ExportSalesInvoiceFormObj);
				if (voucherID != "")
				{
					FormActivator.ExportSalesInvoiceFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ExportDeliveryNote:
				FormActivator.BringFormToFront(FormActivator.ExportDeliveryNoteFormObj);
				if (voucherID != "")
				{
					FormActivator.ExportDeliveryNoteFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			}
			switch (transactionType)
			{
			case TransactionListType.ARPaymentAllocation:
				return;
			case TransactionListType.ExportSalesOrder:
				FormActivator.BringFormToFront(FormActivator.ExportSalesOrderFormObj);
				if (voucherID != "")
				{
					FormActivator.ExportSalesOrderFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ExportSalesProforma:
				FormActivator.BringFormToFront(FormActivator.ExportSalesProformaInvoiceFormObj);
				if (voucherID != "")
				{
					FormActivator.ExportSalesProformaInvoiceFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.PaymentRequest:
				FormActivator.BringFormToFront(FormActivator.PaymentRequestFormObj);
				if (voucherID != "")
				{
					FormActivator.PaymentRequestFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.JobInvoice:
				FormActivator.BringFormToFront(FormActivator.JobInvoiceFormObj);
				if (voucherID != "")
				{
					FormActivator.JobInvoiceFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.JobInventoryIssue:
				FormActivator.BringFormToFront(FormActivator.JobInventoryIssueFormObj);
				if (voucherID != "")
				{
					FormActivator.JobInventoryIssueFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.SalesOrder:
				FormActivator.BringFormToFront(FormActivator.SalesOrderFormObj);
				if (voucherID != "")
				{
					FormActivator.SalesOrderFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ConsignOutSettlement:
				FormActivator.BringFormToFront(FormActivator.ConsignOutSettlementFormObj);
				if (voucherID != "")
				{
					FormActivator.ConsignOutSettlementFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ConsignInSettlement:
				FormActivator.BringFormToFront(FormActivator.ConsignInSettlementFormObj);
				if (voucherID != "")
				{
					FormActivator.ConsignInSettlementFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.SalesQuote:
				FormActivator.BringFormToFront(FormActivator.SalesQuoteFormObj);
				if (voucherID != "")
				{
					FormActivator.SalesQuoteFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.SalesReturn:
				if (subType == null || subType.ToString() == "Credit")
				{
					FormActivator.BringFormToFront(FormActivator.SalesReturnCreditFormObj);
					if (voucherID != "")
					{
						FormActivator.SalesReturnCreditFormObj.EditDocument(sysDocID, voucherID);
					}
				}
				else
				{
					FormActivator.BringFormToFront(FormActivator.SalesReturnCashFormObj);
					if (voucherID != "")
					{
						FormActivator.SalesReturnCashFormObj.EditDocument(sysDocID, voucherID);
					}
				}
				return;
			case TransactionListType.DeliveryNote:
				FormActivator.BringFormToFront(FormActivator.DeliveryNoteFormObj);
				if (voucherID != "")
				{
					FormActivator.DeliveryNoteFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.CLVoucher:
				FormActivator.BringFormToFront(FormActivator.CLVoucherFormObj);
				if (voucherID != "")
				{
					FormActivator.CLVoucherFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.DeliveryReturn:
				FormActivator.BringFormToFront(FormActivator.DeliveryReturnFormObj);
				if (voucherID != "")
				{
					FormActivator.DeliveryReturnFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.PurchaseInvoice:
				if (subType == null || subType.ToString() == "Credit")
				{
					FormActivator.BringFormToFront(FormActivator.PurchaseInvoiceFormObj);
					if (voucherID != "")
					{
						FormActivator.PurchaseInvoiceFormObj.EditDocument(sysDocID, voucherID);
					}
				}
				else if (subType.ToString() == "Cash")
				{
					FormActivator.BringFormToFront(FormActivator.CashPurchaseFormObj);
					if (voucherID != "")
					{
						FormActivator.CashPurchaseFormObj.EditDocument(sysDocID, voucherID);
					}
				}
				else
				{
					FormActivator.BringFormToFront(FormActivator.PurchaseInvoiceImportFormObj);
					if (voucherID != "")
					{
						FormActivator.PurchaseInvoiceImportFormObj.EditDocument(sysDocID, voucherID);
					}
				}
				return;
			case TransactionListType.ImportPurchaseInvoice:
				FormActivator.BringFormToFront(FormActivator.PurchaseInvoiceImportFormObj);
				if (voucherID != "")
				{
					FormActivator.PurchaseInvoiceImportFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.PurchaseInvoiceNI:
				FormActivator.BringFormToFront(FormActivator.PurchaseInvoiceNonInvFormObj);
				if (voucherID != "")
				{
					FormActivator.PurchaseInvoiceNonInvFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.PurchaseOrder:
				FormActivator.BringFormToFront(FormActivator.PurchaseOrderFormObj);
				if (voucherID != "")
				{
					FormActivator.PurchaseOrderFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.PurchaseOrderNI:
				FormActivator.BringFormToFront(FormActivator.PurchaseOrderNonInvFormObj);
				if (voucherID != "")
				{
					FormActivator.PurchaseOrderNonInvFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ImportPurchaseOrder:
				FormActivator.BringFormToFront(FormActivator.PurchaseOrderImportFormObj);
				if (voucherID != "")
				{
					FormActivator.PurchaseOrderImportFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.PurchaseQuote:
				FormActivator.BringFormToFront(FormActivator.PurchaseQuoteFormObj);
				if (voucherID != "")
				{
					FormActivator.PurchaseQuoteFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.POShipment:
				FormActivator.BringFormToFront(FormActivator.POShipmentFormObj);
				if (voucherID != "")
				{
					FormActivator.POShipmentFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ProformaInvoice:
				FormActivator.BringFormToFront(FormActivator.ProformaInvoiceFormObj);
				if (voucherID != "")
				{
					FormActivator.ProformaInvoiceFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.PurchaseReturn:
				if (subType == null || subType.ToString() == "Credit")
				{
					FormActivator.BringFormToFront(FormActivator.PurchaseReturnCreditFormObj);
					if (voucherID != "")
					{
						FormActivator.PurchaseReturnCreditFormObj.EditDocument(sysDocID, voucherID);
					}
				}
				else
				{
					FormActivator.BringFormToFront(FormActivator.PurchaseReturnCashFormObj);
					if (voucherID != "")
					{
						FormActivator.PurchaseReturnCashFormObj.EditDocument(sysDocID, voucherID);
					}
				}
				return;
			case TransactionListType.GRNReturn:
				FormActivator.BringFormToFront(FormActivator.GRNReturnFormObj);
				if (voucherID != "")
				{
					FormActivator.GRNReturnFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.CashPurchase:
				FormActivator.BringFormToFront(FormActivator.CashPurchaseFormObj);
				if (voucherID != "")
				{
					FormActivator.CashPurchaseFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ChequeDeposit:
				FormActivator.BringFormToFront(FormActivator.ChequeDepositFormObj);
				if (voucherID != "")
				{
					FormActivator.ChequeDepositFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ChequeClearance:
				FormActivator.BringFormToFront(FormActivator.IssuedChequeClearanceFormObj);
				if (voucherID != "")
				{
					FormActivator.IssuedChequeClearanceFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.IssuedChequeClearance:
				FormActivator.BringFormToFront(FormActivator.IssuedChequeClearanceFormObj);
				if (voucherID != "")
				{
					FormActivator.IssuedChequeClearanceFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.IssuedChequeReturn:
				FormActivator.BringFormToFront(FormActivator.IssuedChequeReturnFormObj);
				if (voucherID != "")
				{
					FormActivator.IssuedChequeReturnFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ConsignIn:
				FormActivator.BringFormToFront(FormActivator.ConsignInFormObj);
				if (voucherID != "")
				{
					FormActivator.ConsignInFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ConsignOut:
				FormActivator.BringFormToFront(FormActivator.ConsignOutFormObj);
				if (voucherID != "")
				{
					FormActivator.ConsignOutFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.SendChequeToBank:
				FormActivator.BringFormToFront(FormActivator.SendChequesToBankFormObj);
				if (voucherID != "")
				{
					FormActivator.SendChequesToBankFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.PurchaseGRN:
				FormActivator.BringFormToFront(FormActivator.PurchaseReceiptFormObj);
				if (voucherID != "")
				{
					FormActivator.PurchaseReceiptFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ImportGRN:
				FormActivator.BringFormToFront(FormActivator.ImportPurchaseGRNFormObj);
				if (voucherID != "")
				{
					FormActivator.ImportPurchaseGRNFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.JournalEntry:
				FormActivator.BringFormToFront(FormActivator.JournalEntryFormObj);
				if (voucherID != "")
				{
					FormActivator.JournalEntryFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.DebitNote:
				FormActivator.BringFormToFront(FormActivator.DebitNoteEntryFormObj);
				if (voucherID != "")
				{
					FormActivator.DebitNoteEntryFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.CreditNote:
				FormActivator.BringFormToFront(FormActivator.CreditNoteEntryFormObj);
				if (voucherID != "")
				{
					FormActivator.CreditNoteEntryFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.FundTransfer:
				FormActivator.BringFormToFront(FormActivator.FundTransferFormObj);
				if (voucherID != "")
				{
					FormActivator.FundTransferFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.FixedAssetPurchase:
				FormActivator.BringFormToFront(FormActivator.FixedAssetPurchaseFormObj);
				if (voucherID != "")
				{
					FormActivator.FixedAssetPurchaseFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.FixedAssetPurchaseOrder:
				FormActivator.BringFormToFront(FormActivator.FixedAssetPurchaseOrderFormObj);
				if (voucherID != "")
				{
					FormActivator.FixedAssetPurchaseOrderFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.FixedAssetTransfer:
				FormActivator.BringFormToFront(FormActivator.FixedAssetTransferFormObj);
				if (voucherID != "")
				{
					FormActivator.FixedAssetTransferFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.TR:
				FormActivator.BringFormToFront(FormActivator.TREntryFormObj);
				if (voucherID != "")
				{
					FormActivator.TREntryFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.TRPayment:
				FormActivator.BringFormToFront(FormActivator.TRPaymentFormObj);
				if (voucherID != "")
				{
					FormActivator.TRPaymentFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.Expense:
				if (subType == null || subType.ToString() == "Cheque")
				{
					FormActivator.BringFormToFront(FormActivator.ChequeExpenseEntryFormObj);
					if (voucherID != "")
					{
						FormActivator.ChequeExpenseEntryFormObj.EditDocument(sysDocID, voucherID);
					}
				}
				else
				{
					FormActivator.BringFormToFront(FormActivator.CashPaymentParentFormObj);
					if (voucherID != "")
					{
						FormActivator.CashPaymentParentFormObj.EditDocument(sysDocID, voucherID);
					}
				}
				return;
			case TransactionListType.ReceiptVoucher:
				if (subType == null || subType.ToString() == "Cheque")
				{
					FormActivator.BringFormToFront(FormActivator.ChequeReceiptFormObj);
					if (voucherID != "")
					{
						FormActivator.ChequeReceiptFormObj.EditDocument(sysDocID, voucherID);
					}
				}
				else if (subType.ToString() == "TT")
				{
					FormActivator.BringFormToFront(FormActivator.TTReceiptFormObj);
					if (voucherID != "")
					{
						FormActivator.TTReceiptFormObj.EditDocument(sysDocID, voucherID);
					}
				}
				else
				{
					FormActivator.BringFormToFront(FormActivator.CashReceiptFormObj);
					if (voucherID != "")
					{
						FormActivator.CashReceiptFormObj.EditDocument(sysDocID, voucherID);
					}
				}
				return;
			case TransactionListType.ReceiptVoucherMultiple:
				FormActivator.BringFormToFront(FormActivator.CashReceiptMultiPayeeFormObj);
				if (voucherID != "")
				{
					FormActivator.CashReceiptMultiPayeeFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.PaymentVoucher:
				if (subType == null || subType.ToString() == "Cheque")
				{
					FormActivator.BringFormToFront(FormActivator.ChequePaymentFormObj);
					if (voucherID != "")
					{
						FormActivator.ChequePaymentFormObj.EditDocument(sysDocID, voucherID);
					}
				}
				else if (subType.ToString() == "TT")
				{
					FormActivator.BringFormToFront(FormActivator.TTPaymentFormObj);
					if (voucherID != "")
					{
						FormActivator.TTPaymentFormObj.EditDocument(sysDocID, voucherID);
					}
				}
				else
				{
					FormActivator.BringFormToFront(FormActivator.CashPaymentParentFormObj);
					if (voucherID != "")
					{
						FormActivator.CashPaymentParentFormObj.EditDocument(sysDocID, voucherID);
					}
				}
				return;
			case TransactionListType.ReturnVoucher:
				FormActivator.BringFormToFront(FormActivator.ChequeReturnFormObj);
				if (voucherID != "")
				{
					FormActivator.ChequeReturnFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ReceivedChequeCancel:
				FormActivator.BringFormToFront(FormActivator.ReceivedChequeCancellationFormObj);
				if (voucherID != "")
				{
					FormActivator.ReceivedChequeCancellationFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.InventoryRepacking:
				FormActivator.BringFormToFront(FormActivator.InventoryRepackingFormObj);
				if (voucherID != "")
				{
					FormActivator.InventoryRepackingFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.InventoryTransfer:
				FormActivator.BringFormToFront(FormActivator.InventoryTransferFormObj);
				if (voucherID != "")
				{
					FormActivator.InventoryTransferFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.DirectInventoryTransfer:
				FormActivator.BringFormToFront(FormActivator.DirectInventoryTransferFormObj);
				if (voucherID != "")
				{
					FormActivator.DirectInventoryTransferFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.InventoryTransferAcceptance:
				FormActivator.BringFormToFront(FormActivator.InventoryTransferAcceptanceFormObj);
				if (voucherID != "")
				{
					FormActivator.InventoryTransferAcceptanceFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.InventoryTransferReturn:
				FormActivator.BringFormToFront(FormActivator.InventoryTransferReturnFormObj);
				if (voucherID != "")
				{
					FormActivator.InventoryTransferReturnFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ArrivalReport:
				FormActivator.BringFormToFront(FormActivator.ArrivalReportFormObj);
				if (voucherID != "")
				{
					FormActivator.ArrivalReportFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.QualityClaim:
				FormActivator.BringFormToFront(FormActivator.QualityClaimFormObj);
				if (voucherID != "")
				{
					FormActivator.QualityClaimFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.CashSalaryPayment:
				FormActivator.BringFormToFront(FormActivator.CashSalaryPaymentFormObj);
				if (voucherID != "")
				{
					FormActivator.CashSalaryPaymentFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ChequeSalaryPayment:
				FormActivator.BringFormToFront(FormActivator.ChequeSalaryPaymentFormObj);
				if (voucherID != "")
				{
					FormActivator.ChequeSalaryPaymentFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.TransferSalaryPayment:
				FormActivator.BringFormToFront(FormActivator.TransferSalaryPaymentFormObj);
				if (voucherID != "")
				{
					FormActivator.TransferSalaryPaymentFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.EmployeeEOS:
				FormActivator.BringFormToFront(FormActivator.EmployeeEOSFormObj);
				if (voucherID != "")
				{
					FormActivator.EmployeeEOSFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.EmployeeLoan:
				FormActivator.BringFormToFront(FormActivator.EmployeeLoanFormObj);
				if (voucherID != "")
				{
					FormActivator.EmployeeLoanFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.EmployeeProvision:
				FormActivator.BringFormToFront(FormActivator.EmployeeProvisionFormObj);
				if (voucherID != "")
				{
					FormActivator.EmployeeProvisionFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.EmployeeLoanPayment:
				FormActivator.BringFormToFront(FormActivator.EmployeeLoanPaymentFormObj);
				if (voucherID != "")
				{
					FormActivator.EmployeeLoanPaymentFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.LeaveSalaryPayemnt:
				FormActivator.BringFormToFront(FormActivator.LeaveSalaryPaymentFormObj);
				if (voucherID != "")
				{
					FormActivator.LeaveSalaryPaymentFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.EmployeeLoanSettlement:
				FormActivator.BringFormToFront(FormActivator.EmployeeLoanSettlementFormObj);
				if (voucherID != "")
				{
					FormActivator.EmployeeLoanSettlementFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.SalarySheet:
				FormActivator.BringFormToFront(FormActivator.SalarySheetFormObj);
				if (voucherID != "")
				{
					FormActivator.SalarySheetFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.CRMActivity:
				FormActivator.BringFormToFront(FormActivator.ActivityDetailsFormObj);
				if (voucherID != "")
				{
					FormActivator.ActivityDetailsFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.PropertyRental:
				FormActivator.BringFormToFront(FormActivator.PropertyRentDetailsFormObj);
				if (voucherID != "")
				{
					FormActivator.PropertyRentDetailsFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.PropertyRenew:
				FormActivator.BringFormToFront(FormActivator.PropertyRenewDetailsFormObj);
				if (voucherID != "")
				{
					FormActivator.PropertyRenewDetailsFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.PropertyCancel:
				FormActivator.BringFormToFront(FormActivator.PropertyRentCancellationFormObj);
				if (voucherID != "")
				{
					FormActivator.PropertyRentCancellationFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.PropertyServiceInvoice:
				FormActivator.BringFormToFront(FormActivator.PropertyServiceInvoiceFormObj);
				if (voucherID != "")
				{
					FormActivator.PropertyServiceInvoiceFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.PropertyIncomePosting:
				FormActivator.BringFormToFront(FormActivator.RentIncomePostingDetailsFormObj);
				if (voucherID != "")
				{
					FormActivator.RentIncomePostingDetailsFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.PurchaseClaim:
				FormActivator.BringFormToFront(FormActivator.PurchaseClaimFormObj);
				if (voucherID != "")
				{
					FormActivator.PurchaseClaimFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.JobEstimation:
				FormActivator.BringFormToFront(FormActivator.JobEstimationFormObj);
				if (voucherID != "")
				{
					FormActivator.JobEstimationFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.JobTimesheet:
				FormActivator.BringFormToFront(FormActivator.JobTimesheetFormObj);
				if (voucherID != "")
				{
					FormActivator.JobTimesheetFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.JobInventoryReturn:
				FormActivator.BringFormToFront(FormActivator.JobInventoryReturnFormObj);
				if (voucherID != "")
				{
					FormActivator.JobInventoryReturnFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.JobExpenseIssue:
				FormActivator.BringFormToFront(FormActivator.JobExpenseIssueFormObj);
				if (voucherID != "")
				{
					FormActivator.JobExpenseIssueFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.AssemblyBuild:
				FormActivator.BringFormToFront(FormActivator.BuildAssemblyFormObj);
				if (voucherID != "")
				{
					FormActivator.BuildAssemblyFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.WorkOrder:
				FormActivator.BringFormToFront(FormActivator.WorkOrderDetailsFormObj);
				if (voucherID != "")
				{
					FormActivator.WorkOrderDetailsFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.Production:
				FormActivator.BringFormToFront(FormActivator.ProductionDetailsFormObj);
				if (voucherID != "")
				{
					FormActivator.ProductionDetailsFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.OverTimeEntry:
				FormActivator.BringFormToFront(FormActivator.OverTimeEntryFormObj);
				if (voucherID != "")
				{
					FormActivator.OverTimeEntryFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.GarmentRental:
				FormActivator.BringFormToFront(FormActivator.GarmentRentalFormObj);
				if (voucherID != "")
				{
					FormActivator.GarmentRentalFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.GarmentRentalReturn:
				FormActivator.BringFormToFront(FormActivator.GarmentRentalReturnFormObj);
				if (voucherID != "")
				{
					FormActivator.GarmentRentalReturnFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.JobMaterialRequisition:
				FormActivator.BringFormToFront(FormActivator.JobMaterialRequesitionFormObj);
				if (voucherID != "")
				{
					FormActivator.JobMaterialRequesitionFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ServiceCallTrack:
				FormActivator.BringFormToFront(FormActivator.ServiceCallTrackFormObj);
				if (voucherID != "")
				{
					FormActivator.ServiceCallTrackFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ExportPickList:
				FormActivator.BringFormToFront(FormActivator.ExportPickListFormObj);
				if (voucherID != "")
				{
					FormActivator.ExportPickListFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.SalesProforma:
				FormActivator.BringFormToFront(FormActivator.SalesProformaInvoiceFormObj);
				if (voucherID != "")
				{
					FormActivator.SalesProformaInvoiceFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ExportPackingList:
				FormActivator.BringFormToFront(FormActivator.ExportPackingListFormObj);
				if (voucherID != "")
				{
					FormActivator.ExportPackingListFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.Shipment:
				FormActivator.BringFormToFront(FormActivator.ShipmentFormObj);
				if (voucherID != "")
				{
					FormActivator.ShipmentFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.MaintenanceScheduler:
				FormActivator.BringFormToFront(FormActivator.MaintenanceScheduleFormObj);
				if (voucherID != "")
				{
					FormActivator.MaintenanceScheduleFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.MaintenanceEntry:
				FormActivator.BringFormToFront(FormActivator.VehicleMaintenanceEntryFormObj);
				if (voucherID != "")
				{
					FormActivator.VehicleMaintenanceEntryFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.JobMaintenanceEntry:
				FormActivator.BringFormToFront(FormActivator.JobMaintenanceServiceEntryFormObj);
				if (voucherID != "")
				{
					FormActivator.JobMaintenanceServiceEntryFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.JobMaintenanceSchedule:
				FormActivator.BringFormToFront(FormActivator.JobMaintenanceSheduleFormObj);
				if (voucherID != "")
				{
					FormActivator.JobMaintenanceSheduleFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.PurchaseCostEntry:
				FormActivator.BringFormToFront(FormActivator.PurchaseCostEntryFormObj);
				if (voucherID != "")
				{
					FormActivator.PurchaseCostEntryFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.EmployeeAppraisal:
				FormActivator.BringFormToFront(FormActivator.EmployeeAppraisalFormObj);
				if (voucherID != "")
				{
					FormActivator.EmployeeAppraisalFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ContainerTracking:
				FormActivator.BringFormToFront(FormActivator.ContainerTrackingWizardFormObj);
				if (voucherID != "")
				{
					FormActivator.ContainerTrackingWizardFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.CRMCustomerActivity:
				FormActivator.BringFormToFront(FormActivator.ActivityDetailsFormObj);
				if (voucherID != "")
				{
					FormActivator.ActivityDetailsFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ProjectSubContractPO:
				FormActivator.BringFormToFront(FormActivator.ProjectSubContractPOFormObj);
				if (voucherID != "")
				{
					FormActivator.ProjectSubContractPOFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ProjectSubContractPI:
				FormActivator.BringFormToFront(FormActivator.ProjectSubContractPIFormObj);
				if (voucherID != "")
				{
					FormActivator.ProjectSubContractPIFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.EmployeeGeneralActivity:
				FormActivator.BringFormToFront(FormActivator.EmployeeGeneralActivityFormObj);
				if (voucherID != "")
				{
					FormActivator.EmployeeGeneralActivityFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.EmployeeOpeningBalanceLeave:
				FormActivator.BringFormToFront(FormActivator.EmployeeOpeningBalanceLeaveFormObj);
				if (voucherID != "")
				{
					FormActivator.EmployeeOpeningBalanceLeaveFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.InventoryDamage:
				FormActivator.BringFormToFront(FormActivator.InventoryDamageFormObj);
				if (voucherID != "")
				{
					FormActivator.InventoryDamageFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.EmployeePerformance:
				FormActivator.BringFormToFront(FormActivator.EmployeePerformanceCardFormObj);
				if (voucherID != "")
				{
					FormActivator.EmployeePerformanceCardFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.BillOfLading:
				FormActivator.BringFormToFront(FormActivator.BillOfLadingFormObj);
				if (voucherID != "")
				{
					FormActivator.BillOfLadingFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.PriceList:
				FormActivator.BringFormToFront(FormActivator.PriceListDetailsFormObj);
				if (voucherID != "")
				{
					FormActivator.PriceListDetailsFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.Requisition:
				FormActivator.BringFormToFront(FormActivator.RequisitionDetailsFormObj);
				if (voucherID != "")
				{
					FormActivator.RequisitionDetailsFormObj.EditRequisition(sysDocID, voucherID);
				}
				return;
			case TransactionListType.Mobilization:
				FormActivator.BringFormToFront(FormActivator.MobilisationFormObj);
				if (voucherID != "")
				{
					FormActivator.MobilisationFormObj.EditMobilization(sysDocID, voucherID);
				}
				return;
			case TransactionListType.FixedAssetBulkPurchase:
				FormActivator.BringFormToFront(FormActivator.FixedAssetBulkPurchaseFormObj);
				if (voucherID != "")
				{
					FormActivator.FixedAssetBulkPurchaseFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.FixedAssetDep:
				FormActivator.BringFormToFront(FormActivator.FixedAssetDepFormObj);
				if (voucherID != "")
				{
					FormActivator.FixedAssetDepFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.FixedAssetSale:
				FormActivator.BringFormToFront(FormActivator.FixedAssetSaleFormObj);
				if (voucherID != "")
				{
					FormActivator.FixedAssetSaleFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ItemTransaction:
				FormActivator.BringFormToFront(FormActivator.ItemTransactionFormObj);
				if (voucherID != "")
				{
					FormActivator.ItemTransactionFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.EquipmentTransfer:
				FormActivator.BringFormToFront(FormActivator.EquipmentTransferFormObj);
				if (voucherID != "")
				{
					FormActivator.EquipmentTransferFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.EquipmentWorkOrder:
				FormActivator.BringFormToFront(FormActivator.EquipmentWorkOrderFormObj);
				if (voucherID != "")
				{
					FormActivator.EquipmentWorkOrderFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.InventoryAdjustment:
				FormActivator.BringFormToFront(FormActivator.InventoryAdjustmentsFormObj);
				if (voucherID != "")
				{
					FormActivator.InventoryAdjustmentsFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.CustomerOpeningBalance:
				FormActivator.BringFormToFront(FormActivator.CustomerOpeningBalanceBatchFormObj);
				if (voucherID != "")
				{
					FormActivator.CustomerOpeningBalanceBatchFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.VendorOpeningBalance:
				FormActivator.BringFormToFront(FormActivator.VendorOpeningBalanceBatchFormObj);
				if (voucherID != "")
				{
					FormActivator.VendorOpeningBalanceBatchFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.EmployeeOpeningBalance:
				FormActivator.BringFormToFront(FormActivator.EmployeeOpeningBalanceBatchFormObj);
				if (voucherID != "")
				{
					FormActivator.EmployeeOpeningBalanceBatchFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.InventoryOpeningBalance:
				FormActivator.BringFormToFront(FormActivator.InventoryOpeningBalanceBatchFormObj);
				if (voucherID != "")
				{
					FormActivator.InventoryOpeningBalanceBatchFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.WorkOrderInventoryReturn:
				FormActivator.BringFormToFront(FormActivator.WorkOrderInventoryReturnFormObj);
				if (voucherID != "")
				{
					FormActivator.WorkOrderInventoryReturnFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.WorkOrderInventoryIssue:
				FormActivator.BringFormToFront(FormActivator.WorkOrderInventoryIssueFormObj);
				if (voucherID != "")
				{
					FormActivator.WorkOrderInventoryIssueFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.LegalActivity:
				FormActivator.BringFormToFront(FormActivator.LegalActivityDetailsFormObj);
				if (voucherID != "")
				{
					FormActivator.LegalActivityDetailsFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.FreightCharges:
				FormActivator.BringFormToFront(FormActivator.FreightChargesFormObj);
				if (voucherID != "")
				{
					FormActivator.FreightChargesFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.LPOReceipt:
				FormActivator.BringFormToFront(FormActivator.LPOReceiptFormObj);
				if (voucherID != "")
				{
					FormActivator.LPOReceiptFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ConsignOutReturn:
				FormActivator.BringFormToFront(FormActivator.ConsignOutReturnFormObj);
				if (voucherID != "")
				{
					FormActivator.ConsignOutReturnFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.InventoryDismantle:
				FormActivator.BringFormToFront(FormActivator.InventoryDismantleFormObj);
				if (voucherID != "")
				{
					FormActivator.InventoryDismantleFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.SalaryAddition:
				FormActivator.BringFormToFront(FormActivator.SalaryAdditionFormObj);
				if (voucherID != "")
				{
					FormActivator.SalaryAdditionFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.SalaryDeduction:
				FormActivator.BringFormToFront(FormActivator.SalaryDeductionFormObj);
				if (voucherID != "")
				{
					FormActivator.SalaryDeductionFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ProductPriceBulkupdate:
				FormActivator.BringFormToFront(FormActivator.ProductPriceBulkUpdateFormObj);
				if (voucherID != "")
				{
					FormActivator.ProductPriceBulkUpdateFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			}
			switch (transactionType)
			{
			case TransactionListType.InventoryTransferReturn:
				FormActivator.BringFormToFront(FormActivator.InventoryTransferReturnFormObj);
				if (voucherID != "")
				{
					FormActivator.InventoryTransferReturnFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ChequeReceiptOpening:
				FormActivator.BringFormToFront(FormActivator.OpeningChequeReceiptEntryFormObj);
				if (voucherID != "")
				{
					FormActivator.OpeningChequeReceiptEntryFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.MaterialReservation:
				FormActivator.BringFormToFront(FormActivator.MaterialReservationFormObj);
				if (voucherID != "")
				{
					FormActivator.MaterialReservationFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.LegalAction:
				FormActivator.BringFormToFront(FormActivator.LegalActionDetailsFormObj);
				if (voucherID != "")
				{
					FormActivator.LegalActionDetailsFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.SalesForecasting:
				FormActivator.BringFormToFront(FormActivator.SalesForecastingFormObj);
				if (voucherID != "")
				{
					FormActivator.SalesForecastingFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.PurchasePrepaymentInvoice:
				FormActivator.BringFormToFront(FormActivator.PurchasePrepaymentInvoiceFormObj);
				if (voucherID != "")
				{
					FormActivator.PurchasePrepaymentInvoiceFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.TaskTransaction:
				FormActivator.BringFormToFront(FormActivator.TaskTransactionFormObj);
				if (voucherID != "")
				{
					FormActivator.TaskTransactionFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.TRApplication:
				FormActivator.BringFormToFront(FormActivator.TRApplicationFormObj);
				if (voucherID != "")
				{
					FormActivator.TRApplicationFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.VehicleMileageTrack:
				FormActivator.BringFormToFront(FormActivator.VehicleMileageTrackFormObj);
				if (voucherID != "")
				{
					FormActivator.VehicleMileageTrackFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.SalesManTarget:
				FormActivator.BringFormToFront(FormActivator.SalesManTargetFormObj);
				if (voucherID != "")
				{
					FormActivator.SalesManTargetFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.CustomerInsuranceClaim:
				FormActivator.BringFormToFront(FormActivator.CustomerInsuranceClaimFormObj);
				if (voucherID != "")
				{
					FormActivator.CustomerInsuranceClaimFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ChequeDiscount:
				FormActivator.BringFormToFront(FormActivator.ChequeDiscountFormObj);
				if (voucherID != "")
				{
					FormActivator.ChequeDiscountFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.SalesEnquiry:
				FormActivator.BringFormToFront(FormActivator.SalesEnquiryFormObj);
				if (voucherID != "")
				{
					FormActivator.SalesEnquiryFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.ChequeReceiptMultiple:
				FormActivator.BringFormToFront(FormActivator.ChequeReceiptMultiEntryFormObj);
				if (voucherID != "")
				{
					FormActivator.ChequeReceiptMultiEntryFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.StandingJournal:
				FormActivator.BringFormToFront(FormActivator.StandingJournalEntryFormObj);
				if (voucherID != "")
				{
					FormActivator.StandingJournalEntryFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.IssuedChequeCancellation:
				FormActivator.BringFormToFront(FormActivator.IssuedChequeCancellationFormObj);
				if (voucherID != "")
				{
					FormActivator.IssuedChequeCancellationFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.JobMaterialEstimate:
				FormActivator.BringFormToFront(FormActivator.JobMaterialEstimateFormObj);
				if (voucherID != "")
				{
					FormActivator.JobMaterialEstimateFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.LoanEntry:
				FormActivator.BringFormToFront(FormActivator.LoanEntryFormObj);
				if (voucherID != "")
				{
					FormActivator.LoanEntryFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.PropertyServiceRequest:
				FormActivator.BringFormToFront(FormActivator.PropertyServiceRequestFormObj);
				if (voucherID != "")
				{
					FormActivator.PropertyServiceRequestFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.JobManHrsBudgeting:
				FormActivator.BringFormToFront(FormActivator.JobManHrsBudgetingFormObj);
				if (voucherID != "")
				{
					FormActivator.JobManHrsBudgetingFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			case TransactionListType.PropertyServiceAssign:
				FormActivator.BringFormToFront(FormActivator.PropertyServiceAssignFormObj);
				if (voucherID != "")
				{
					FormActivator.PropertyServiceAssignFormObj.EditDocument(sysDocID, voucherID);
				}
				return;
			}
			switch (transactionType)
			{
			case TransactionListType.Journal:
			case TransactionListType.IssuedCheque:
			case TransactionListType.ReceivedCheque:
			case TransactionListType.SalesHistory:
			case TransactionListType.ActivityLog:
			case TransactionListType.InventoryLedger:
				break;
			case TransactionListType.PurchasePrepaymentInvoice:
				FormActivator.BringFormToFront(FormActivator.PurchasePrepaymentInvoiceFormObj);
				if (voucherID != "")
				{
					FormActivator.PurchasePrepaymentInvoiceFormObj.EditDocument(sysDocID, voucherID);
				}
				break;
			case TransactionListType.SecurityCheque:
				FormActivator.BringFormToFront(FormActivator.SecurityChequeFormObj);
				if (voucherID != "")
				{
					FormActivator.SecurityChequeFormObj.EditDocument(sysDocID, voucherID);
				}
				break;
			case TransactionListType.EmployeeSalary:
				FormActivator.BringFormToFront(FormActivator.EmployeeSalaryDetailsFormObj);
				if (voucherID != "")
				{
					FormActivator.EmployeeSalaryDetailsFormObj.EditDocument(voucherID);
				}
				break;
			case TransactionListType.BillDiscount:
				FormActivator.BringFormToFront(FormActivator.BillDiscountFormObj);
				if (voucherID != "")
				{
					FormActivator.BillDiscountFormObj.EditDocument(sysDocID, voucherID);
				}
				break;
			case TransactionListType.EmployeeLeave:
				FormActivator.BringFormToFront(FormActivator.EmployeeLeaveProcessFormObj);
				if (voucherID != "")
				{
					FormActivator.EmployeeLeaveProcessFormObj.EditDocument(voucherID);
				}
				break;
			default:
				ErrorHelper.ErrorMessage("Transaction editing is not implimented for type:", transactionType.ToString());
				break;
			}
		}

		public void EditTransaction(TransactionListType transactionType, string voucherID, int activityID)
		{
			if (transactionType == TransactionListType.LeaveRequest)
			{
				FormActivator.BringFormToFront(FormActivator.EmployeeLeaveRequestFormObj);
				if (activityID > 0)
				{
					FormActivator.EmployeeLeaveRequestFormObj.EditDocument(voucherID, activityID);
				}
			}
		}

		public void OpenCardForEdit(DataComboType cardType, string id)
		{
			GenericListTypes result = GenericListTypes.All;
			FormHelper formHelper = new FormHelper();
			switch (cardType)
			{
			case DataComboType.AccountGroup:
				formHelper.EditAccountGroup(id);
				return;
			case DataComboType.Customer:
				formHelper.EditCustomer(id);
				return;
			case DataComboType.ServiceProvider:
				formHelper.EditServiceProvider(id);
				return;
			case DataComboType.LeadStatus:
				formHelper.EditLeadStatus(id);
				return;
			case DataComboType.Item:
			case DataComboType.Product:
				formHelper.EditItem(id);
				return;
			case DataComboType.Vendor:
				formHelper.EditVendor(id);
				return;
			case DataComboType.EmployeeProvisionType:
				formHelper.EditProvisionType(id);
				return;
			case DataComboType.ServiceItem:
				formHelper.EditServiceItem(id);
				return;
			case DataComboType.Accounts:
				formHelper.EditAccount(id);
				return;
			case DataComboType.Employee:
				formHelper.EditEmployee(id);
				return;
			case DataComboType.PayrollItem:
				formHelper.EditPayrollItem(id);
				return;
			case DataComboType.ReleaseType:
				formHelper.EditReleaseType(id);
				return;
			}
			switch (cardType)
			{
			case DataComboType.ServiceProvider:
				formHelper.EditServiceProvider(id);
				return;
			case DataComboType.AnalysisGroup:
				formHelper.EditAnalysisGroup(id);
				return;
			case DataComboType.Area:
				formHelper.EditArea(id);
				return;
			case DataComboType.Bank:
				formHelper.EditBank(id);
				return;
			case DataComboType.Driver:
				formHelper.EditDriver(id);
				return;
			case DataComboType.Benefit:
				formHelper.EditBenefit(id);
				return;
			case DataComboType.EOSRule:
				formHelper.EditEOSRule(id);
				return;
			case DataComboType.FiscalYear:
				formHelper.EditFiscalYear(id);
				return;
			case DataComboType.Buyer:
				formHelper.EditBuyer(id);
				return;
			case DataComboType.CompanyDocType:
				formHelper.EditCompanyDocType(id);
				return;
			case DataComboType.Chequebook:
				formHelper.EditChequebook(id);
				return;
			case DataComboType.CheckList:
				formHelper.EditCheckList(id);
				return;
			case DataComboType.ReturnedChequeReason:
				formHelper.EditReturnedChequeReason(id);
				return;
			case DataComboType.CompanyDocument:
				formHelper.EditCompanyDocument(id);
				return;
			case DataComboType.Contact:
				formHelper.EditContact(id);
				return;
			case DataComboType.UserGroup:
				formHelper.EditUserGroup(id);
				return;
			case DataComboType.Country:
				formHelper.EditCountry(id);
				return;
			case DataComboType.CustomerCategory:
				formHelper.EditCustomerCategory(id);
				return;
			case DataComboType.ContactsCategory:
				formHelper.EditContactsCategory(id);
				return;
			case DataComboType.LeadCategory:
				formHelper.EditLeadCategory(id);
				return;
			case DataComboType.VendorCategory:
				formHelper.EditVendorCategory(id);
				return;
			case DataComboType.User:
				formHelper.EditUser(id);
				return;
			case DataComboType.Currency:
				formHelper.EditCurrency(id);
				return;
			case DataComboType.CustomerClass:
				formHelper.EditCustomerClass(id);
				return;
			case DataComboType.CustomerGroup:
				formHelper.EditCustomerGroup(id);
				return;
			case DataComboType.Deduction:
				formHelper.EditDeduction(id);
				return;
			case DataComboType.Degree:
				formHelper.EditDegree(id);
				return;
			case DataComboType.Department:
				formHelper.EditDepartment(id);
				return;
			case DataComboType.Destination:
				formHelper.EditDestination(id);
				return;
			case DataComboType.Division:
				formHelper.EditDivision(id);
				return;
			case DataComboType.EmployeeDocType:
				formHelper.EditEmployeeDocType(id);
				return;
			case DataComboType.EmployeeGroup:
				formHelper.EditEmployeeGroup(id);
				return;
			case DataComboType.Grade:
				formHelper.EditGrade(id);
				return;
			case DataComboType.LeaveType:
				formHelper.EditLeaveType(id);
				return;
			case DataComboType.JobType:
				formHelper.EditJobType(id);
				return;
			case DataComboType.CostCategory:
				formHelper.EditCostCategory(id);
				return;
			case DataComboType.Location:
				formHelper.EditLocation(id);
				return;
			case DataComboType.Nationality:
				formHelper.EditNationality(id);
				return;
			case DataComboType.PaymentMethod:
				formHelper.EditPaymentMethod(id);
				return;
			case DataComboType.PaymentTerm:
				formHelper.EditPaymentTerm(id);
				return;
			case DataComboType.Position:
				formHelper.EditPosition(id);
				return;
			case DataComboType.PriceLevel:
				formHelper.EditPriceLevel(id);
				return;
			case DataComboType.ProductBrand:
				formHelper.EditProductBrand(id);
				return;
			case DataComboType.ProductCategory:
				formHelper.EditProductCategory(id);
				return;
			case DataComboType.ProductClass:
				formHelper.EditProductClass(id);
				return;
			case DataComboType.ProductManufacturer:
				formHelper.EditProductManufacturer(id);
				return;
			case DataComboType.ProductStyle:
				formHelper.EditProductStyle(id);
				return;
			case DataComboType.Religion:
				formHelper.EditReligion(id);
				return;
			case DataComboType.Salesperson:
				formHelper.EditSalesperson(id);
				return;
			case DataComboType.ShippingMethod:
				formHelper.EditShippingMethod(id);
				return;
			case DataComboType.Skill:
				formHelper.EditSkill(id);
				return;
			case DataComboType.Sponsor:
				formHelper.EditSponsor(id);
				return;
			case DataComboType.TenancyContract:
				formHelper.EditTenancyContract(id);
				return;
			case DataComboType.TradeLicense:
				formHelper.EditTradeLicense(id);
				return;
			case DataComboType.Unit:
				formHelper.EditUOM(id);
				return;
			case DataComboType.VendorClass:
				formHelper.EditVendorClass(id);
				return;
			case DataComboType.VendorGroup:
				formHelper.EditVendorGroup(id);
				return;
			case DataComboType.Visa:
				formHelper.EditVisa(id);
				return;
			case DataComboType.CostCenter:
				formHelper.EditCostCenter(id);
				return;
			case DataComboType.Register:
				formHelper.EditRegister(id);
				return;
			case DataComboType.FixedAsset:
				formHelper.EditFixedAsset(id);
				return;
			case DataComboType.FixedAssetGroup:
				formHelper.EditFixedAssetGroup(id);
				return;
			case DataComboType.FixedAssetLocation:
				formHelper.EditFixedAssetLocation(id);
				return;
			case DataComboType.FixedAssetClass:
				formHelper.EditFixedAssetClass(id);
				return;
			}
			switch (cardType)
			{
			case DataComboType.Register:
				formHelper.EditFixedAssetLocation(id);
				return;
			case DataComboType.Employee:
				formHelper.EditEmployee(id);
				return;
			case DataComboType.Job:
				formHelper.EditJob(id);
				return;
			case DataComboType.Opportunity:
				formHelper.EditOpportunity(id);
				return;
			case DataComboType.Competitor:
				formHelper.EditCompetitor(id);
				return;
			case DataComboType.Activity:
				formHelper.EditActivity(id);
				return;
			case DataComboType.Campaign:
				formHelper.EditCampaign(id);
				return;
			case DataComboType.Event:
				formHelper.EditEvent(id);
				return;
			case DataComboType.BankFacility:
				formHelper.EditBankFacility(id);
				return;
			case DataComboType.BankFacilityGroup:
				formHelper.EditBankFacilityGroup(id);
				return;
			case DataComboType.City:
				formHelper.EditCity(id);
				return;
			case DataComboType.JobFee:
				formHelper.EditJobFee(id);
				return;
			case DataComboType.Vehicle:
				formHelper.EditVehicle(id);
				return;
			case DataComboType.CustomerRelation:
				formHelper.EditCustomerRelation(id);
				return;
			case DataComboType.CustomerVendorLink:
				formHelper.EditCustomerVendorRelation(id);
				return;
			case DataComboType.Equipment:
				formHelper.EditEquipment(id);
				return;
			case DataComboType.PriceList:
				formHelper.EditPriceList(id);
				return;
			case DataComboType.Candidate:
				formHelper.EditCandidate(id);
				return;
			case DataComboType.Appointment:
				formHelper.EditAppointment(id);
				return;
			case DataComboType.WorkLocation:
				formHelper.EditWorkLocation(id);
				return;
			case DataComboType.ExpenseCode:
				formHelper.EditExpense(id);
				return;
			case DataComboType.Transporter:
				formHelper.EditTransporter(id);
				return;
			case DataComboType.INCO:
				formHelper.EditINCO(id);
				return;
			case DataComboType.Collateral:
				formHelper.EditCollateral(id);
				return;
			case DataComboType.CustomReport:
				formHelper.EditCustomReport(id);
				return;
			case DataComboType.CustomGadget:
				formHelper.EditCustomGadget(id);
				return;
			case DataComboType.JobTask:
				formHelper.EditJobTask(id);
				return;
			case DataComboType.JobTaskGroup:
				formHelper.EditJobTaskGroup(id);
				return;
			case DataComboType.Followup:
				formHelper.EditFollowup(id);
				return;
			case DataComboType.EntityCategory:
				formHelper.EditEntityCategory(id);
				return;
			case DataComboType.QualityTask:
				formHelper.EditQualityTask(id);
				return;
			case DataComboType.ArrivalReportTemplate:
				formHelper.EditArrivalReportTemplate(id);
				return;
			case DataComboType.Surveyor:
				formHelper.EditSurveyor(id);
				return;
			case DataComboType.EmployeeLoanType:
				formHelper.EditEmployeeLoanType(id);
				return;
			case DataComboType.EmployeeLeave:
				formHelper.EditEmployeeLeave(id);
				return;
			case DataComboType.EmployeeLeaveResumption:
				formHelper.EditEmployeeLeaveResumption(id);
				return;
			case DataComboType.AdjustmentType:
				formHelper.EditAdjustmentType(id);
				return;
			case DataComboType.PropertyAgent:
				formHelper.EditPropertyAgent(id);
				return;
			case DataComboType.PropertyClass:
				formHelper.EditPropertyClass(id);
				return;
			case DataComboType.Property:
				formHelper.EditProperty(id);
				return;
			case DataComboType.PropertyUnit:
				formHelper.EditPropertyUnit(id);
				return;
			case DataComboType.PropertyCategory:
				formHelper.EditPropertyCategory(id);
				return;
			case DataComboType.PropertyIncomeCode:
				formHelper.EditPropertyIncomeCode(id);
				return;
			case DataComboType.Tenant:
				formHelper.EditTenant(id);
				return;
			case DataComboType.ContainerSize:
				formHelper.EditContainerSize(id);
				return;
			case DataComboType.EmployeeType:
				formHelper.EditEmployeeClass(id);
				return;
			case DataComboType.InventoryTransferType:
				formHelper.EditInventoryTransferType(id);
				return;
			case DataComboType.JobBOM:
				formHelper.EditJobBOM(id);
				return;
			case DataComboType.Package:
				formHelper.EditPackage(id);
				return;
			case DataComboType.Bin:
				formHelper.EditBin(id);
				return;
			case DataComboType.Route:
				formHelper.EditRoute(id);
				return;
			case DataComboType.RouteGroup:
				formHelper.EditRouteGroup(id);
				return;
			case DataComboType.PassportControl:
				formHelper.EditPassportControl(id);
				return;
			case DataComboType.Approval:
				formHelper.EditApproval(id);
				return;
			case DataComboType.Verification:
				formHelper.EditVerification(id);
				return;
			case DataComboType.Lead:
				formHelper.EditLead(id);
				return;
			case DataComboType.ClientAsset:
				formHelper.EditClientAsset(id);
				return;
			case DataComboType.EmployeeAppraisal:
				formHelper.EditEmployeeAppraisal(id);
				return;
			case DataComboType.InsuranceProvider:
				formHelper.EditInsuranceProvider(id);
				return;
			case DataComboType.CRMCustomerActivity:
				formHelper.EditCRMCustomerActivity(id);
				return;
			case DataComboType.RiderSummary:
				formHelper.EditRiderSummary(id);
				return;
			case DataComboType.HorseSummary:
				formHelper.EditHorseSummary(id);
				return;
			case DataComboType.HorseType:
				formHelper.EditHorseType(id);
				return;
			case DataComboType.HorseSex:
				formHelper.EditHorseSex(id);
				return;
			case DataComboType.HolidayCalendar:
				formHelper.EditHolidayCalendar(id);
				return;
			case DataComboType.OverTime:
				formHelper.EditOverTime(id);
				return;
			case DataComboType.EquipmentCategory:
				formHelper.EditEquipmentCategory(id);
				return;
			case DataComboType.EquipmentType:
				formHelper.EditEquipmentType(id);
				return;
			case DataComboType.EAEquipment:
				formHelper.EditEAEquipment(id);
				return;
			case DataComboType.RequisitionType:
				formHelper.EditRequisitionType(id);
				return;
			case DataComboType.Lawyer:
				formHelper.EditLawyer(id);
				return;
			case DataComboType.CaseParty:
				formHelper.EditCaseParty(id);
				return;
			case DataComboType.LegalActionStatus:
				formHelper.EditLegalActionStatus(id);
				return;
			case DataComboType.Analysis:
				formHelper.EditAnalysis(id);
				return;
			case DataComboType.Tax:
				formHelper.EditTax(id);
				return;
			case DataComboType.TaxGroup:
				formHelper.EditTaxGroup(id);
				return;
			case DataComboType.SysDoc:
				formHelper.EditSysDoc(id);
				return;
			case DataComboType.CaseClient:
				formHelper.EditCaseClient(id);
				return;
			case DataComboType.ProductMake:
				formHelper.EditProductMake(id);
				return;
			case DataComboType.ProductType:
				formHelper.EditProductType(id);
				return;
			case DataComboType.ProductModel:
				formHelper.EditProductModel(id);
				return;
			case DataComboType.TransactionDoc:
				formHelper.EditSalespersonGroup(id);
				return;
			case DataComboType.TaskSteps:
				formHelper.EditTaskSteps(id);
				return;
			case DataComboType.TaskType:
				formHelper.EditTaskType(id);
				return;
			case DataComboType.Rack:
				formHelper.EditRack(id);
				return;
			case DataComboType.PropertyVirtualUnit:
				formHelper.EditPropertyVirtualUnit(id);
				return;
			case DataComboType.ProductSpecification:
				formHelper.EditProductSpecification(id);
				return;
			case DataComboType.CompanyDivision:
				formHelper.EditCompanyDivision(id);
				return;
			case DataComboType.PrintTemplateMap:
				formHelper.EditPrintTemplateMap(id);
				return;
			case DataComboType.EmployeeAbsconding:
				formHelper.EditEmployeeAbsconding(int.Parse(id));
				return;
			case DataComboType.Patient:
				formHelper.EditPatient(id);
				return;
			case DataComboType.PatientDocType:
				formHelper.EditPatientDocType(id);
				return;
			case DataComboType.DataSync:
				formHelper.EditDataSyncSetupDetails(id);
				return;
			case DataComboType.ProductType1:
			case DataComboType.ProductType2:
			case DataComboType.ProductType3:
			case DataComboType.ProductType4:
			case DataComboType.ProductType5:
			case DataComboType.ProductType6:
			case DataComboType.ProductType7:
			case DataComboType.ProductType8:
				Enum.TryParse(cardType.ToString(), out result);
				formHelper.EditGenericProductTypeList(result, id);
				return;
			}
			if (Enum.TryParse(cardType.ToString(), out result))
			{
				formHelper.EditGenericList(result, id);
			}
			else
			{
				ErrorHelper.ErrorMessage("General List Open is not implemented for type: " + cardType.ToString());
			}
		}

		public static void ShowDocumentInfo(string entityID, Form parent)
		{
			ShowDocumentInfo(entityID, "", parent);
		}

		public static void ShowDocumentInfo(string voucherID, string sysDocID, Form parent)
		{
			DocumentInformationDialog documentInformationDialog = new DocumentInformationDialog();
			documentInformationDialog.VoucherID = voucherID;
			documentInformationDialog.SysDocID = sysDocID;
			documentInformationDialog.ShowDialog(parent);
		}

		public static void ShowDocumentInfo(string voucherID, string sysDocID, int comboType, Form parent)
		{
			DocumentInformationDialog documentInformationDialog = new DocumentInformationDialog();
			documentInformationDialog.VoucherID = voucherID;
			documentInformationDialog.SysDocID = sysDocID;
			documentInformationDialog.ComboType = comboType;
			documentInformationDialog.ShowDialog(parent);
		}

		public DataSet GetTransaction(string sysDocID, string voucherID)
		{
			try
			{
				DataSet result = new DataSet();
				switch (sysDocID)
				{
				case "SYS_011":
				case "SYS_010":
					return Factory.OpeningBalanceBatchSystem.GetOpeningBalanceBatchByID(sysDocID, voucherID);
				case "SYS_017":
					return null;
				default:
				{
					SysDocTypes sysDocTypes = (SysDocTypes)int.Parse(Factory.DatabaseSystem.GetFieldValue("System_Document", "SysDocType", "SysDocID", sysDocID).ToString());
					switch (sysDocTypes)
					{
					case SysDocTypes.SalesInvoice:
						result = Factory.SalesInvoiceSystem.GetSalesInvoiceByID(sysDocID, voucherID);
						break;
					case SysDocTypes.SalesReceipt:
						result = Factory.SalesReceiptSystem.GetSalesReceiptByID(sysDocID, voucherID);
						break;
					case SysDocTypes.ExportSalesInvoice:
						result = Factory.SalesInvoiceSystem.GetSalesInvoiceByID(sysDocID, voucherID);
						break;
					case SysDocTypes.JobInvoice:
						result = Factory.JobInvoiceSystem.GetJobInvoiceByID(sysDocID, voucherID);
						break;
					case SysDocTypes.JobInventoryIssue:
						if (voucherID != "")
						{
							result = Factory.JobInventoryIssueSystem.GetJobInventoryIssueByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.SalesOrder:
						if (voucherID != "")
						{
							result = Factory.SalesOrderSystem.GetSalesOrderByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.ConsignOutSettlement:
						if (voucherID != "")
						{
							result = Factory.ConsignOutSettlementSystem.GetSettlementByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.SalesQuote:
						if (voucherID != "")
						{
							result = Factory.SalesQuoteSystem.GetSalesQuoteByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.CashSalesReturn:
						if (voucherID != "")
						{
							result = Factory.SalesReturnSystem.GetSalesReturnByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.CreditSalesReturn:
						if (voucherID != "")
						{
							result = Factory.SalesReturnSystem.GetSalesReturnByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.DeliveryNote:
						if (voucherID != "")
						{
							result = Factory.DeliveryNoteSystem.GetDeliveryNoteByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.ExportDeliveryNote:
						if (voucherID != "")
						{
							result = Factory.DeliveryNoteSystem.GetDeliveryNoteByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.DeliveryReturn:
						if (voucherID != "")
						{
							result = Factory.DeliveryReturnSystem.GetDeliveryReturnByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.PurchaseInvoice:
						if (voucherID != "")
						{
							result = Factory.PurchaseInvoiceSystem.GetPurchaseInvoiceByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.ImportPurchaseInvoice:
						if (voucherID != "")
						{
							result = Factory.PurchaseInvoiceSystem.GetPurchaseInvoiceByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.PurchaseOrder:
						if (voucherID != "")
						{
							result = Factory.PurchaseOrderSystem.GetPurchaseOrderByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.ImportPurchaseOrder:
						if (voucherID != "")
						{
							result = Factory.PurchaseOrderSystem.GetPurchaseOrderByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.PurchaseQuote:
						if (voucherID != "")
						{
							result = Factory.PurchaseQuoteSystem.GetPurchaseQuoteByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.PackingList:
						if (voucherID != "")
						{
							result = Factory.ExportPackingListSystem.GetExportPackingListByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.ProformaInvoice:
						if (voucherID != "")
						{
							result = Factory.SalesProformaSystem.GetSalesOrderByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.CreditPurchaseReturn:
						if (voucherID != "")
						{
							result = Factory.PurchaseReturnSystem.GetPurchaseReturnByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.CashPurchaseReturn:
						if (voucherID != "")
						{
							result = Factory.PurchaseReturnSystem.GetPurchaseReturnByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.GRNReturn:
						if (voucherID != "")
						{
							result = Factory.GRNReturnSystem.GetGRNReturnByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.SendChequesToBank:
						if (voucherID != "")
						{
							result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.GoodsReceivedNote:
						if (voucherID != "")
						{
							result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.ImportGoodsReceivedNote:
						if (voucherID != "")
						{
							result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.GJournal:
						if (voucherID != "")
						{
							result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.DebitNote:
						if (voucherID != "")
						{
							result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.CreditNote:
						if (voucherID != "")
						{
							result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.FundTransfer:
						if (voucherID != "")
						{
							result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.FixedAssetPurchase:
						if (voucherID != "")
						{
							result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.FixedAssetTransfer:
						if (voucherID != "")
						{
							result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.TR:
						if (voucherID != "")
						{
							result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.TRPayment:
						if (voucherID != "")
						{
							result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.ChequeExpense:
						if (voucherID != "")
						{
							result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.CashExpense:
						if (voucherID != "")
						{
							result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.ChequeReceipt:
						if (voucherID != "")
						{
							result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.TTReceipt:
						if (voucherID != "")
						{
							result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
						}
						break;
					case SysDocTypes.TTPayment:
						if (voucherID != "")
						{
							result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
						}
						break;
					default:
						if (sysDocTypes != SysDocTypes.GRNReturn)
						{
							if (sysDocTypes == SysDocTypes.SendChequesToBank)
							{
								FormActivator.BringFormToFront(FormActivator.SendChequesToBankFormObj);
								if (voucherID != "")
								{
									FormActivator.SendChequesToBankFormObj.EditDocument(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.GoodsReceivedNote)
							{
								if (voucherID != "")
								{
									result = Factory.PurchaseReceiptSystem.GetPurchaseReceiptByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.ImportGoodsReceivedNote)
							{
								if (voucherID != "")
								{
									result = Factory.PurchaseReceiptSystem.GetPurchaseReceiptByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.GJournal)
							{
								if (voucherID != "")
								{
									result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.DebitNote)
							{
								if (voucherID != "")
								{
									result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.CreditNote)
							{
								if (voucherID != "")
								{
									result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.FundTransfer)
							{
								if (voucherID != "")
								{
									result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.FixedAssetPurchase)
							{
								if (voucherID != "")
								{
									result = Factory.FixedAssetPurchaseSystem.GetFixedAssetPurchaseByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.FixedAssetTransfer)
							{
								if (voucherID != "")
								{
									result = Factory.FixedAssetTransferSystem.GetFixedAssetTransferByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.TR)
							{
								if (voucherID != "")
								{
									result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.TRPayment)
							{
								if (voucherID != "")
								{
									result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.ChequeExpense)
							{
								if (voucherID != "")
								{
									result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.CashExpense)
							{
								if (voucherID != "")
								{
									result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.ChequeReceipt)
							{
								if (voucherID != "")
								{
									result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.TTReceipt)
							{
								if (voucherID != "")
								{
									result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.ChequePayment)
							{
								if (voucherID != "")
								{
									result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.TTPayment)
							{
								if (voucherID != "")
								{
									result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.CashPayment)
							{
								if (voucherID != "")
								{
									result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.CRMActivity)
							{
								if (voucherID != "")
								{
									result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.AssemblyBuild)
							{
								if (voucherID != "")
								{
									result = Factory.AssemblyBuildSystem.GetAssemblyBuildByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.QualityClaim)
							{
								if (voucherID != "")
								{
									result = Factory.QualityClaimSystem.GetQualityClaimByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes == SysDocTypes.InventoryRepacking)
							{
								if (voucherID != "")
								{
									result = Factory.InventoryRepackingSystem.GetInventoryRepackingByID(sysDocID, voucherID);
								}
							}
							else if (sysDocTypes != SysDocTypes.TransitTransferIn && sysDocTypes != SysDocTypes.TransitTransferOut)
							{
								if (sysDocTypes == SysDocTypes.ConsignIn)
								{
									if (voucherID != "")
									{
										result = Factory.ConsignInSystem.GetConsignInByID(sysDocID, voucherID);
									}
								}
								else if (sysDocTypes == SysDocTypes.SalesProforma)
								{
									if (voucherID != "")
									{
										result = Factory.SalesProformaSystem.GetSalesOrderByID(sysDocID, voucherID);
									}
								}
								else if (sysDocTypes == SysDocTypes.InventoryAdjustment)
								{
									if (voucherID != "")
									{
										result = Factory.InventoryAdjustmentSystem.GetInventoryAdjustmentByID(sysDocID, voucherID);
									}
								}
								else if (sysDocTypes == SysDocTypes.ChequeDeposit)
								{
									if (voucherID != "")
									{
										result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
									}
								}
								else if (sysDocTypes == SysDocTypes.CashReceipt)
								{
									if (voucherID != "")
									{
										result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
									}
								}
								else
								{
									if (sysDocTypes != SysDocTypes.LPOReceipt)
									{
										if (sysDocTypes != SysDocTypes.PurchaseQuote && sysDocTypes != SysDocTypes.DirectInventoryTransfer && sysDocTypes != SysDocTypes.ImportPurchaseInvoice && sysDocTypes != SysDocTypes.ImportPurchaseOrder && sysDocTypes != SysDocTypes.PurchaseOrder && sysDocTypes != SysDocTypes.DeliveryReturn)
										{
											_ = 32;
										}
										return null;
									}
									if (voucherID != "")
									{
										result = Factory.TransactionSystem.GetTransactionByID(sysDocID, voucherID);
									}
								}
							}
						}
						break;
					case SysDocTypes.CashPurchase:
						break;
					}
					return result;
				}
				}
			}
			catch
			{
				throw;
			}
		}

		public void CustomizeLayout(LayoutControl layoutControl)
		{
			try
			{
				layoutControl.OptionsCustomizationForm.ShowPropertyGrid = true;
				layoutControl.HideCustomization += LayoutControl_HideCustomization;
				layoutControl.ShowCustomizationForm();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void LayoutControl_HideCustomization(object sender, EventArgs e)
		{
			try
			{
				LayoutControl layoutControl = sender as LayoutControl;
				if (ErrorHelper.QuestionMessageYesNo("Do you want to save the changes to the form layout?") == DialogResult.Yes)
				{
					MemoryStream stream = new MemoryStream();
					layoutControl.SaveLayoutToStream(stream);
					SaveControlLayout(ControlLayoutTypes.Form, layoutControl.ParentForm.Name, stream);
				}
				layoutControl.HideCustomization -= LayoutControl_HideCustomization;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public void SaveControlLayout(ControlLayoutTypes type, string formName, MemoryStream stream)
		{
			try
			{
				ControlLayoutData controlLayoutData = new ControlLayoutData();
				DataRow dataRow = controlLayoutData.ControlLayoutTable.NewRow();
				dataRow["ControlType"] = checked((byte)type);
				dataRow["ControlName"] = formName;
				dataRow["LayoutName"] = "Default";
				dataRow["LayoutData"] = stream.ToArray();
				controlLayoutData.ControlLayoutTable.Rows.Add(dataRow);
				Factory.ControlLayoutSystem.SaveControlLayout(controlLayoutData);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public void LoadFormLayout(LayoutControl layoutControl, string formName, string layoutName)
		{
			try
			{
				Stream stream = LoadControlLayout(ControlLayoutTypes.Form, formName, layoutName);
				if (stream != null)
				{
					layoutControl.RestoreLayoutFromStream(stream);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private Stream LoadControlLayout(ControlLayoutTypes type, string controlName, string layoutName)
		{
			try
			{
				byte[] controlLayoutByID = Factory.ControlLayoutSystem.GetControlLayoutByID(type, controlName, layoutName);
				if (controlLayoutByID == null)
				{
					return null;
				}
				controlLayoutByID = CommonLib.DecompressData(controlLayoutByID);
				return new MemoryStream(controlLayoutByID);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return null;
			}
		}

		public void AddCustomFieldsToForm(string formName, string tableName, LayoutControl layoutControl)
		{
			try
			{
				foreach (DataRow row in Factory.UDFSystem.GetUDFList(tableName).Tables["UDF_Setup"].Rows)
				{
					UDFDataTypes uDFDataTypes = (UDFDataTypes)byte.Parse(row["DataType"].ToString());
					string text = row["FieldName"].ToString();
					string text2 = row["DisplayName"].ToString();
					string customListCode = row["UDListName"].ToString();
					int result = 0;
					int.TryParse(row["FieldSize"].ToString(), out result);
					switch (uDFDataTypes)
					{
					case UDFDataTypes.Text:
					{
						TextBox textBox2 = new TextBox();
						textBox2.Name = text;
						textBox2.MaxLength = result;
						LayoutControlItem layoutControlItem7 = layoutControl.AddItem(text2, textBox2);
						layoutControlItem7.Name = "layoutItemUDF" + text;
						layoutControlItem7.HideToCustomization();
						break;
					}
					case UDFDataTypes.Number:
						switch (result)
						{
						case 1:
						{
							NumberTextBox numberTextBox2 = new NumberTextBox();
							numberTextBox2.Name = text;
							numberTextBox2.MaxLength = 9;
							numberTextBox2.AllowDecimal = false;
							LayoutControlItem layoutControlItem6 = layoutControl.AddItem(text2, numberTextBox2);
							layoutControlItem6.Name = "layoutItemUDF" + text;
							layoutControlItem6.HideToCustomization();
							break;
						}
						case 2:
						{
							NumberTextBox numberTextBox = new NumberTextBox();
							numberTextBox.Name = text;
							numberTextBox.MaxLength = 20;
							numberTextBox.AllowDecimal = true;
							LayoutControlItem layoutControlItem5 = layoutControl.AddItem(text2, numberTextBox);
							layoutControlItem5.Name = "layoutItemUDF" + text;
							layoutControlItem5.HideToCustomization();
							break;
						}
						case 3:
						{
							AmountTextBox amountTextBox = new AmountTextBox();
							amountTextBox.Name = text;
							amountTextBox.MaxLength = 20;
							amountTextBox.AllowDecimal = true;
							LayoutControlItem layoutControlItem4 = layoutControl.AddItem(text2, amountTextBox);
							layoutControlItem4.Name = "layoutItemUDF" + text;
							layoutControlItem4.HideToCustomization();
							break;
						}
						}
						break;
					case UDFDataTypes.Date:
					{
						MMSDateTimePicker mMSDateTimePicker = new MMSDateTimePicker();
						mMSDateTimePicker.Name = text;
						mMSDateTimePicker.Format = DateTimePickerFormat.Short;
						LayoutControlItem layoutControlItem3 = layoutControl.AddItem(text2, mMSDateTimePicker);
						layoutControlItem3.Name = "layoutItemUDF" + text;
						layoutControlItem3.HideToCustomization();
						break;
					}
					case UDFDataTypes.Time:
					{
						MMSDateTimePicker mMSDateTimePicker2 = new MMSDateTimePicker();
						mMSDateTimePicker2.Name = text;
						mMSDateTimePicker2.Format = DateTimePickerFormat.Time;
						LayoutControlItem layoutControlItem9 = layoutControl.AddItem(text2, mMSDateTimePicker2);
						layoutControlItem9.Name = "layoutItemUDF" + text;
						layoutControlItem9.HideToCustomization();
						break;
					}
					case UDFDataTypes.CheckBox:
					{
						CheckBox checkBox = new CheckBox();
						checkBox.Name = text;
						LayoutControlItem layoutControlItem8 = layoutControl.AddItem(text2, checkBox);
						layoutControlItem8.Name = "layoutItemUDF" + text;
						layoutControlItem8.HideToCustomization();
						break;
					}
					case UDFDataTypes.List:
					{
						DataComboType dataComboType = (DataComboType)int.Parse(row["ListType"].ToString());
						Control control = new Control();
						switch (dataComboType)
						{
						case DataComboType.AccountGroup:
							control = new AccountGroupComboBox();
							break;
						case DataComboType.Accounts:
							control = new AllAccountsComboBox();
							break;
						case DataComboType.Activity:
							control = new ActivityComboBox();
							break;
						case DataComboType.Agent:
							control = new AgentComboBox();
							break;
						case DataComboType.Area:
							control = new AreaComboBox();
							break;
						case DataComboType.Bank:
							control = new BankComboBox();
							break;
						case DataComboType.Buyer:
							control = new BuyerComboBox();
							break;
						case DataComboType.City:
							control = new CityComboBox();
							break;
						case DataComboType.Color:
							control = new GenericListComboBox();
							((GenericListComboBox)control).GenericListType = GenericListTypes.Color;
							break;
						case DataComboType.CompanyDivision:
							control = new CompanyDivisionComboBox();
							break;
						case DataComboType.Competitor:
							control = new CompetitorComboBox();
							break;
						case DataComboType.Contact:
							control = new ContactsComboBox();
							break;
						case DataComboType.ContainerSize:
							control = new ContainerSizeComboBox();
							break;
						case DataComboType.ContainerType:
							control = new GenericListComboBox();
							((GenericListComboBox)control).GenericListType = GenericListTypes.ContainerType;
							break;
						case DataComboType.CostCenter:
							control = new CostCenterComboBox();
							break;
						case DataComboType.Country:
							control = new CountryComboBox();
							break;
						case DataComboType.Currency:
							control = new CurrencyComboBox();
							break;
						case DataComboType.Customer:
							control = new customersFlatComboBox();
							break;
						case DataComboType.CustomerClass:
							control = new CustomerClassComboBox();
							break;
						case DataComboType.CustomerGroup:
							control = new CustomerGroupComboBox();
							break;
						case DataComboType.Department:
							control = new DepartmentComboBox();
							break;
						case DataComboType.Destination:
							control = new DestinationComboBox();
							break;
						case DataComboType.Division:
							control = new DivisionComboBox();
							break;
						case DataComboType.Driver:
							control = new DriverComboBox();
							break;
						case DataComboType.Employee:
							control = new EmployeeComboBox();
							break;
						case DataComboType.Grade:
							control = new GradeComboBox();
							break;
						case DataComboType.Item:
							control = new ProductComboBox();
							break;
						case DataComboType.Job:
							control = new JobComboBox();
							break;
						case DataComboType.Lead:
							control = new leadsFlatComboBox();
							break;
						case DataComboType.Location:
							control = new LocationComboBox();
							break;
						case DataComboType.Nationality:
							control = new NationalityComboBox();
							break;
						case DataComboType.Opportunity:
							control = new OpportunityComboBox();
							break;
						case DataComboType.PaymentTerm:
							control = new PaymentTermComboBox();
							break;
						case DataComboType.Port:
							control = new PortComboBox();
							break;
						case DataComboType.Position:
							control = new PositionComboBox();
							break;
						case DataComboType.ProductBrand:
							control = new ProductBrandComboBox();
							break;
						case DataComboType.ProductCategory:
							control = new ProductCategoryComboBox();
							break;
						case DataComboType.ProductClass:
							control = new ItemClassComboBox();
							break;
						case DataComboType.Property:
							control = new PropertyComboBox();
							break;
						case DataComboType.Salesperson:
							control = new SalespersonComboBox();
							break;
						case DataComboType.User:
							control = new UserComboBox();
							break;
						case DataComboType.UserGroup:
							control = new UserGroupComboBox();
							break;
						case DataComboType.Vehicle:
							control = new VehicleComboBox();
							break;
						case DataComboType.Vendor:
							control = new vendorsFlatComboBox();
							break;
						case DataComboType.CustomList:
							control = new CustomListComboBox();
							((CustomListComboBox)control).CustomListCode = customListCode;
							break;
						default:
							continue;
						}
						LayoutControlItem layoutControlItem = layoutControl.AddItem(text2, control);
						control.Name = text;
						layoutControlItem.Name = "layoutItemUDF" + text;
						layoutControlItem.HideToCustomization();
						if (control.GetType().BaseType != null && control.GetType().BaseType == typeof(MultiColumnComboBox))
						{
							TextBox textBox = new TextBox();
							textBox.Name = text + "Name";
							textBox.ReadOnly = true;
							textBox.TabStop = false;
							((MultiColumnComboBox)control).DescriptionTextBox = textBox;
							LayoutControlItem layoutControlItem2 = layoutControl.AddItem(text2 + "_Name", textBox);
							layoutControlItem2.TextVisible = false;
							layoutControlItem2.HideToCustomization();
						}
						break;
					}
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public DataSet GetEntityUDFData(string tableName, string idColumn1, string idColumn2, DataSet data, LayoutControl layout)
		{
			DataSet entityUDFSchema = Factory.UDFSystem.GetEntityUDFSchema(tableName, idColumn1, idColumn2);
			if (entityUDFSchema == null)
			{
				return data;
			}
			data.Tables[tableName].Merge(entityUDFSchema.Tables[0]);
			DataRow dataRow = data.Tables[tableName].Rows[0];
			foreach (DataColumn column in entityUDFSchema.Tables[0].Columns)
			{
				if (!(column.ColumnName == idColumn1) && !(column.ColumnName == idColumn2))
				{
					string name = "layoutItemUDF" + column.ColumnName;
					BaseLayoutItem baseLayoutItem = layout.Items.FindByName(name);
					if (baseLayoutItem != null && baseLayoutItem.Visible)
					{
						Control control = ((LayoutControlItem)baseLayoutItem).Control;
						if (control != null)
						{
							if (control.GetType() == typeof(CheckBox))
							{
								dataRow[column.ColumnName] = ((CheckBox)control).Checked;
							}
							else if (control.GetType() == typeof(TextBox))
							{
								dataRow[column.ColumnName] = ((TextBox)control).Text;
							}
							else if (control.GetType() == typeof(NumberTextBox))
							{
								if (!Micromind.ClientLibraries.ExtensionMethods.IsNullOrEmpty(((NumberTextBox)control).Text))
								{
									dataRow[column.ColumnName] = ((NumberTextBox)control).Text;
								}
								else
								{
									dataRow[column.ColumnName] = 0;
								}
							}
							else if (control.GetType().BaseType == typeof(MultiColumnComboBox))
							{
								string selectedID = ((MultiColumnComboBox)control).SelectedID;
								if (selectedID == "")
								{
									dataRow[column.ColumnName] = DBNull.Value;
								}
								else
								{
									dataRow[column.ColumnName] = selectedID;
								}
							}
						}
					}
				}
			}
			return data;
		}

		public DataSet FillEntityUDFData(string tableName, string idColumn1, string idColumn2, DataSet data, LayoutControl layout)
		{
			DataSet entityUDFSchema = Factory.UDFSystem.GetEntityUDFSchema(tableName, idColumn1, idColumn2);
			if (entityUDFSchema == null)
			{
				return data;
			}
			data.Tables[tableName].Merge(entityUDFSchema.Tables[0]);
			DataRow dataRow = data.Tables[tableName].Rows[0];
			foreach (DataColumn column in entityUDFSchema.Tables[0].Columns)
			{
				if (!(column.ColumnName == idColumn1) && !(column.ColumnName == idColumn2))
				{
					string name = "layoutItemUDF" + column.ColumnName;
					BaseLayoutItem baseLayoutItem = layout.Items.FindByName(name);
					if (baseLayoutItem != null)
					{
						Control control = ((LayoutControlItem)baseLayoutItem).Control;
						if (control != null)
						{
							if (control.GetType() == typeof(CheckBox))
							{
								if (Micromind.ClientLibraries.ExtensionMethods.IsDBNullOrEmpty(dataRow[column.ColumnName]))
								{
									((CheckBox)control).Checked = false;
								}
								else
								{
									((CheckBox)control).Checked = bool.Parse(dataRow[column.ColumnName].ToString());
								}
							}
							else if (control.GetType() == typeof(TextBox))
							{
								((TextBox)control).Text = dataRow[column.ColumnName].ToString();
							}
							else if (control.GetType() == typeof(NumberTextBox))
							{
								if (Micromind.ClientLibraries.ExtensionMethods.IsDBNullOrEmpty(dataRow[column.ColumnName]))
								{
									((NumberTextBox)control).Clear();
								}
								else
								{
									((NumberTextBox)control).Text = decimal.Parse(dataRow[column.ColumnName].ToString()).ToString();
								}
							}
							else if (control.GetType().BaseType == typeof(MultiColumnComboBox))
							{
								_ = ((MultiColumnComboBox)control).SelectedID;
								if (Micromind.ClientLibraries.ExtensionMethods.IsDBNullOrEmpty(dataRow[column.ColumnName]))
								{
									((MultiColumnComboBox)control).Clear();
								}
								else
								{
									((MultiColumnComboBox)control).SelectedID = dataRow[column.ColumnName].ToString();
								}
							}
						}
					}
				}
			}
			return data;
		}

		public void ClearUDFData(string tableName, string idColumn1, string idColumn2, LayoutControl layout)
		{
			DataSet entityUDFSchema = Factory.UDFSystem.GetEntityUDFSchema(tableName, idColumn1, idColumn2);
			if (entityUDFSchema != null)
			{
				foreach (DataColumn column in entityUDFSchema.Tables[0].Columns)
				{
					if (!(column.ColumnName == idColumn1) && !(column.ColumnName == idColumn2))
					{
						string name = "layoutItemUDF" + column.ColumnName;
						BaseLayoutItem baseLayoutItem = layout.Items.FindByName(name);
						if (baseLayoutItem != null)
						{
							Control control = ((LayoutControlItem)baseLayoutItem).Control;
							if (control != null)
							{
								if (control.GetType() == typeof(CheckBox))
								{
									((CheckBox)control).Checked = false;
								}
								else if (control.GetType() == typeof(TextBox))
								{
									((TextBox)control).Clear();
								}
								else if (control.GetType() == typeof(NumberTextBox))
								{
									((NumberTextBox)control).Clear();
								}
								else if (control.GetType().BaseType == typeof(MultiColumnComboBox))
								{
									((MultiColumnComboBox)control).Clear();
								}
							}
						}
					}
				}
			}
		}

		public void InitLayoutControl(LayoutControl layoutControl)
		{
			layoutControl.AllowCustomizationMenu = false;
		}
	}
}
