using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ILetterSystem
	{
		int CreateLetter(LetterData letterData);

		bool UpdateLetter(LetterData letterData);

		LetterData GetLetters();

		bool DeleteLetter(int letterID);

		DataSet GetLettersByFields(params string[] columns);

		DataSet GetLettersByFields(int[] letterID, params string[] columns);

		DataSet GetLettersByFields(int[] letterID, bool isInactive, params string[] columns);

		LetterData GetLetterByID(int letterID);

		bool ExistLetter(string shortName);
	}
}
