namespace Micromind.Common.Data
{
	public enum PaymentTermTypes
	{
		FromInvoiceDate = 1,
		FromEOM,
		AfterPODate,
		AfterATD,
		AfterPackingList,
		BeforeETA,
		AfterETA,
		AfterBL,
		AfterGRN
	}
}
