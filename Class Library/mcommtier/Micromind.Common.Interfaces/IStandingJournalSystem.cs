using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IStandingJournalSystem
	{
		bool CreateStandingJournal(StandingJournalData journalData);

		bool UpdateStandingJournal(StandingJournalData journalData);

		bool DeleteStandingJournal(string journalID);

		StandingJournalData GetStandingJournalVoucherByID(string journalID);

		DataSet GetStandingJournalToPrint(string[] journalID);

		DataSet GetStandingJournalToPrint(string journalID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		bool ProcessStandingJournal(string standingJournalID, int year, int month);

		DataSet GetStandingJournalToProcess(int year, int month, bool status);

		GLData GetJournalVoucherToPrint(int year, int month);
	}
}
