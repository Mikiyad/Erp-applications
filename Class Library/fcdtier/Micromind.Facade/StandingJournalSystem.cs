using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class StandingJournalSystem : MarshalByRefObject, IStandingJournalSystem, IDisposable
	{
		private Config config;

		public StandingJournalSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateStandingJournal(StandingJournalData journalData)
		{
			return new StandingJournal(config).InsertUpdateStandingJournal(journalData, isUpdate: false);
		}

		public bool UpdateStandingJournal(StandingJournalData journalData)
		{
			return new StandingJournal(config).InsertUpdateStandingJournal(journalData, isUpdate: true);
		}

		public bool DeleteStandingJournal(string voucherID)
		{
			using (StandingJournal standingJournal = new StandingJournal(config))
			{
				return standingJournal.DeleteStandingJournal(voucherID);
			}
		}

		public StandingJournalData GetStandingJournalVoucherByID(string journalID)
		{
			using (StandingJournal standingJournal = new StandingJournal(config))
			{
				return standingJournal.GetStandingJournalVoucherByID(journalID);
			}
		}

		public DataSet GetStandingJournalToPrint(string[] voucherID)
		{
			return new StandingJournal(config).GetStandingJournalVoucherToPrint(voucherID);
		}

		public DataSet GetStandingJournalToPrint(string voucherID)
		{
			return new StandingJournal(config).GetStandingJournalVoucherToPrint(new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new StandingJournal(config).GetList(from, to, showVoid);
		}

		public bool ProcessStandingJournal(string standingJournalID, int year, int month)
		{
			return new StandingJournal(config).ProcessStandingJournal(standingJournalID, year, month);
		}

		public DataSet GetStandingJournalToProcess(int year, int month, bool status)
		{
			return new StandingJournal(config).GetStandingJournalToProcess(year, month, status);
		}

		public GLData GetJournalVoucherToPrint(int year, int month)
		{
			return new StandingJournal(config).GetJournalVoucherToPrint(year, month);
		}
	}
}
