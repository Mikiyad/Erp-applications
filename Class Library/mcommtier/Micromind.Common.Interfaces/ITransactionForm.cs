namespace Micromind.Common.Interfaces
{
	public interface ITransactionForm
	{
		string SystemDocID
		{
			get;
			set;
		}

		bool IsNewRecord
		{
			get;
		}

		string VoucherID
		{
			get;
		}

		bool CanClose();

		void LoadData(string voucherID);

		void Preview();

		void Print();

		void EditDocument(string sysDocID, string voucherID);

		void DuplicateData();
	}
}
