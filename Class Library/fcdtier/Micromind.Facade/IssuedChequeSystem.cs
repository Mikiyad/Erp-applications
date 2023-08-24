using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class IssuedChequeSystem : MarshalByRefObject, IIssuedChequeSystem, IDisposable
	{
		private Config config;

		public IssuedChequeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public int ValidateBlankCheque(string chequebookID, string chequeNumber, string sysDocID, string voucherID)
		{
			using (IssuedCheques issuedCheques = new IssuedCheques(config))
			{
				return issuedCheques.ValidateBlankCheque(chequebookID, chequeNumber, sysDocID, voucherID);
			}
		}

		public DataSet GetChequesToClearList(DateTime chequeDate)
		{
			using (IssuedCheques issuedCheques = new IssuedCheques(config))
			{
				return issuedCheques.GetChequesToClearList(chequeDate);
			}
		}

		public DataSet GetIssuedChequeToCancelList()
		{
			using (IssuedCheques issuedCheques = new IssuedCheques(config))
			{
				return issuedCheques.GetIssuedChequeToCancelList();
			}
		}

		public DataSet GetIssuedChequeToReturnList()
		{
			using (IssuedCheques issuedCheques = new IssuedCheques(config))
			{
				return issuedCheques.GetIssuedChequeToReturnList();
			}
		}

		public DataSet GetIssuedChequeToPrint(DateTime startDate, DateTime endDate, bool includePrinted)
		{
			using (IssuedCheques issuedCheques = new IssuedCheques(config))
			{
				return issuedCheques.GetIssuedChequeToPrint(startDate, endDate, includePrinted);
			}
		}

		public DataSet GetSecurityChequeToPrint(string sysDocID, string[] voucherID)
		{
			using (IssuedCheques issuedCheques = new IssuedCheques(config))
			{
				return issuedCheques.GetSecurityChequeToPrint(sysDocID, voucherID);
			}
		}

		public DataSet GetSecurityChequeToPrint(string sysDocID, string voucherID)
		{
			using (IssuedCheques issuedCheques = new IssuedCheques(config))
			{
				return issuedCheques.GetSecurityChequeToPrint(sysDocID, voucherID);
			}
		}

		public DataSet GetChequeByID(string chequeID)
		{
			using (IssuedCheques issuedCheques = new IssuedCheques(config))
			{
				return issuedCheques.GetChequeByID(chequeID);
			}
		}

		public DataSet GetChequeIssuedandSecirutyChqByID(string chequeID, bool IsSecurityChq)
		{
			using (IssuedCheques issuedCheques = new IssuedCheques(config))
			{
				return issuedCheques.GetChequeIssuedandSecirutyChqByID(chequeID, IsSecurityChq);
			}
		}

		public DataSet GetRegisteredBlankChequeList()
		{
			using (IssuedCheques issuedCheques = new IssuedCheques(config))
			{
				return issuedCheques.GetRegisteredBlankChequeList();
			}
		}

		public DataSet GetBlankCheque(string chequebookID, string chequeNumber)
		{
			using (IssuedCheques issuedCheques = new IssuedCheques(config))
			{
				return issuedCheques.GetBlankCheque(chequebookID, chequeNumber);
			}
		}

		public bool VoidBlankCheque(string chequebookID, string chequeNumber, string reasonID, string note, bool isVoid)
		{
			using (IssuedCheques issuedCheques = new IssuedCheques(config))
			{
				return issuedCheques.VoidBlankCheque(chequebookID, chequeNumber, reasonID, note, isVoid);
			}
		}

		public bool InsertUpdateSecurityCheque(DataSet chequeData, bool isUpdate)
		{
			using (IssuedCheques issuedCheques = new IssuedCheques(config))
			{
				return issuedCheques.InsertUpdateSecurityCheque(chequeData, isUpdate);
			}
		}

		public DataSet GetTransactionByID(string sysDocID, string voucherID)
		{
			using (IssuedCheques issuedCheques = new IssuedCheques(config))
			{
				return issuedCheques.GetTransactionByID(sysDocID, voucherID);
			}
		}

		public bool VoidSecurityCheque(string chequebookID, string chequeNumber, DateTime voidDate)
		{
			using (IssuedCheques issuedCheques = new IssuedCheques(config))
			{
				return issuedCheques.VoidSecurityCheque(chequebookID, chequeNumber, voidDate);
			}
		}

		public bool DeleteSecurityCheque(string sysDocID, string voucherID)
		{
			using (IssuedCheques issuedCheques = new IssuedCheques(config))
			{
				return issuedCheques.DeleteSecurityCheque(sysDocID, voucherID);
			}
		}

		public bool MarkAsPrinted(int chequeID)
		{
			using (IssuedCheques issuedCheques = new IssuedCheques(config))
			{
				return issuedCheques.MarkAsPrinted(chequeID);
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new IssuedCheques(config).GetList(from, to, showVoid);
		}

		public DataSet GetIssuedChequeReturnList(DateTime from, DateTime to, bool showVoid)
		{
			return new IssuedCheques(config).GetIssuedChequeReturnList(from, to, showVoid);
		}

		public DataSet GetChequeCancelledList(DateTime from, DateTime to, bool showVoid)
		{
			return new IssuedCheques(config).GetChequeCancelledList(from, to, showVoid);
		}

		public DataSet GetSecurityChequeList(DateTime from, DateTime to, bool showVoid)
		{
			return new IssuedCheques(config).GetSecurityChequeList(from, to, showVoid);
		}

		public DataSet GetIssuedChequeAsOfDate(DateTime chkDatefrom, DateTime chkDateto, DateTime from, DateTime to, string fromBank, string toBank, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromLocationID, string toLocationID, bool cleared, bool bounced, bool cancelled, string strGroupBy, string vendorIDs)
		{
			using (IssuedCheques issuedCheques = new IssuedCheques(config))
			{
				return issuedCheques.GetIssuedChequeAsOfDate(chkDatefrom, chkDateto, from, to, fromBank, toBank, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromLocationID, toLocationID, cleared, bounced, cancelled, strGroupBy, vendorIDs);
			}
		}

		public DataSet GetChequeMaturityReport(DateTime from, DateTime to, string fromAccountID, string toAccountID, bool cleared, bool bounced, bool cancelled)
		{
			using (IssuedCheques issuedCheques = new IssuedCheques(config))
			{
				return issuedCheques.GetChequeMaturityReport(from, to, fromAccountID, toAccountID, cleared, bounced, cancelled);
			}
		}

		public string GetAnalysisID(string sysDocID, string voucherID)
		{
			using (IssuedCheques issuedCheques = new IssuedCheques(config))
			{
				return issuedCheques.GetAnalysisID(sysDocID, voucherID);
			}
		}
	}
}
