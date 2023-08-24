using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ReceivedChequeSystem : MarshalByRefObject, IReceivedChequeSystem, IDisposable
	{
		private Config config;

		public ReceivedChequeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public DataSet GetChequesToDepositList(DateTime chequeDate)
		{
			using (ReceivedCheques receivedCheques = new ReceivedCheques(config))
			{
				return receivedCheques.GetChequesToDepositList(chequeDate);
			}
		}

		public DataSet GetChequesToDepositList(DateTime chequeDate, string bankAccountID)
		{
			using (ReceivedCheques receivedCheques = new ReceivedCheques(config))
			{
				return receivedCheques.GetChequesToDepositList(chequeDate, bankAccountID);
			}
		}

		public DataSet GetChequesToDepositList(DateTime chequeDate, string bankAccountID, string locationID)
		{
			using (ReceivedCheques receivedCheques = new ReceivedCheques(config))
			{
				return receivedCheques.GetChequesToDepositList(chequeDate, bankAccountID, locationID);
			}
		}

		public DataSet GetChequesToSendToBankList(DateTime chequeDate)
		{
			using (ReceivedCheques receivedCheques = new ReceivedCheques(config))
			{
				return receivedCheques.GetChequesToSendToBankList(chequeDate);
			}
		}

		public DataSet GetChequesToSendToBankList(DateTime chequeDate, string bankAccountID)
		{
			using (ReceivedCheques receivedCheques = new ReceivedCheques(config))
			{
				return receivedCheques.GetChequesToSendToBankList(chequeDate, bankAccountID);
			}
		}

		public DataSet GetChequesToDiscountList(DateTime chequeDate)
		{
			using (ReceivedCheques receivedCheques = new ReceivedCheques(config))
			{
				return receivedCheques.GetChequesToDiscountList(chequeDate);
			}
		}

		public DataSet GetChequesToDiscountList(DateTime chequeDate, string bankAccountID)
		{
			using (ReceivedCheques receivedCheques = new ReceivedCheques(config))
			{
				return receivedCheques.GetChequesToDiscountList(chequeDate, bankAccountID);
			}
		}

		public DataSet GetReceivedChequeComboList()
		{
			using (ReceivedCheques receivedCheques = new ReceivedCheques(config))
			{
				return receivedCheques.GetReceivedChequeComboList();
			}
		}

		public DataSet GetChequeByID(string chequeID)
		{
			using (ReceivedCheques receivedCheques = new ReceivedCheques(config))
			{
				return receivedCheques.GetChequeByID(chequeID);
			}
		}

		public DataSet GetReceivedChequeToCancelList()
		{
			using (ReceivedCheques receivedCheques = new ReceivedCheques(config))
			{
				return receivedCheques.GetReceivedChequeToCancelList();
			}
		}

		public DataSet GetReceivedChequeToReturnList()
		{
			using (ReceivedCheques receivedCheques = new ReceivedCheques(config))
			{
				return receivedCheques.GetReceivedChequeToReturnList();
			}
		}

		public DataSet GetChequesToClearList()
		{
			using (ReceivedCheques receivedCheques = new ReceivedCheques(config))
			{
				return receivedCheques.GetChequesToClearList();
			}
		}

		public DataSet GetChangeChequeStatusList()
		{
			using (ReceivedCheques receivedCheques = new ReceivedCheques(config))
			{
				return receivedCheques.GetChangeChequeStatusList();
			}
		}

		public bool UpdateChequeStatus(string[] chequeIDs, ReceivedChequeStatus status)
		{
			using (ReceivedCheques receivedCheques = new ReceivedCheques(config))
			{
				return receivedCheques.UpdateChequeStatus(chequeIDs, status);
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new ReceivedCheques(config).GetList(from, to, showVoid);
		}

		public DataSet GetReceivedChequeAsOfDate(DateTime chkDatefrom, DateTime chkDateto, DateTime from, DateTime to, string fromBank, string toBank, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string fromLocation, string toLocation, bool cleared, bool bounded, bool cancelled, bool discounted, bool senttobank, string strGroupBy, string customerIDs)
		{
			using (ReceivedCheques receivedCheques = new ReceivedCheques(config))
			{
				return receivedCheques.GetReceivedChequeAsOfDate(chkDatefrom, chkDateto, from, to, fromBank, toBank, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, fromLocation, toLocation, cleared, bounded, cancelled, discounted, senttobank, strGroupBy, customerIDs);
			}
		}

		public DataSet GetForSettleChequesToSendToBankList(DateTime chequeDate, string[] chequeIDs)
		{
			using (ReceivedCheques receivedCheques = new ReceivedCheques(config))
			{
				return receivedCheques.GetForSettleChequesToSendToBankList(chequeDate, chequeIDs);
			}
		}

		public DataSet GetChequeMaturityReport(DateTime from, DateTime to, DateTime chequedatefrom, DateTime chequedateto, string fromAccountID, string toAccountID, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, bool cleared, bool bounced, bool cancelled, string customerIDs)
		{
			using (ReceivedCheques receivedCheques = new ReceivedCheques(config))
			{
				return receivedCheques.GetChequeMaturityReport(from, to, chequedatefrom, chequedateto, fromAccountID, toAccountID, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, cleared, bounced, cancelled, customerIDs);
			}
		}
	}
}
