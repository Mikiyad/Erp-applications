using Micromind.Common.Data;
using System;
using System.Collections;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface INoteSystem
	{
		int CreateNote(string noteText, string noteCreatorID, string noteColor, NoteFlags noteFlag, int noteHeight, int noteWidth);

		int CreateNote(NoteData noteData);

		bool UpdateNote(NoteData noteData);

		bool UpdateNoteLocation(NoteData noteData);

		NoteData GetNotesByUserID(int userID);

		NoteData GetNoteUsers(int noteID);

		bool AssignNoteUsers(int noteID, ArrayList users, NoteFlags noteFlag);

		bool RemoveNoteUser(int noteID, int userID);

		bool DeleteNote(int noteID);

		bool DeleteNote(int[] notesID);

		NoteData GetNoteByScreenID(int userID, int screendID);

		DateTime GetNoteDateTimeStamp(int noteID);

		DataSet GetReminderNote(int userID, DateTime reminderDate);

		NoteData GetNotesByID(int id);

		NoteData GetNotesByType(NoteTypes noteType);

		NoteData GetNotesByType(NoteTypes noteType, int referenceID);

		DataSet GetNotesByFields(NoteTypes noteType, params string[] columns);

		DataSet GetNotesByFields(NoteTypes noteType, int referenceID, params string[] columns);

		DataSet GetNotesByFields(int noteID, params string[] columns);

		int GetReminderNoteCount(int userID, DateTime reminderDate);

		bool SetNoteFlag(int userID, int noteID, NoteFlags noteFlag);

		bool ActivateNote(object id, bool activate);

		NoteData GetNotesByUserID(int userID, bool isInactive);

		DataSet GetNotesByFields(NoteFlags[] noteFlags, NoteTypes[] noteTypes, int[] references, int maxNotes, DateTime reminderFrom, DateTime reminderTo, bool isInactive, params string[] columns);

		bool SetAlarm(object id, bool isAlarm);

		bool SetAlarm(object id, bool isAlarm, DateTime nextReminderDate);

		bool ClearAllNoteUsers(int notID);
	}
}
