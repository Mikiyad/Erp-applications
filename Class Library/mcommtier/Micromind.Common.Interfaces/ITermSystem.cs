using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ITermSystem
	{
		bool CreateTerm(PaymentTermData termData);

		bool UpdateTerm(PaymentTermData termData);

		PaymentTermData GetTermByID(string termID);

		PaymentTermData GetTerms();

		PaymentTermData GetTermsByFields(params string[] columns);

		PaymentTermData GetTermsByFields(int[] termsID, params string[] columns);

		bool DeleteTerm(string termID);

		bool ExistTermName(string termName);

		bool CreateUpdateTermsBatch(DataSet listData, bool checkConcurrency);

		DataSet GetPaymentTermsComboList();

		DataSet GetPaymentTermsList();
	}
}
