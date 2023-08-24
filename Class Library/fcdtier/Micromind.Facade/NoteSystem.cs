using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Collections;
using System.Data;

namespace Micromind.Facade
{
	public sealed class NoteSystem : MarshalByRefObject, INoteSystem, IDisposable
	{
		private Config config;

		public NoteSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public int CreateNote(string noteText, string noteCreatorID, string noteColor, NoteFlags noteFlag, int noteHeight, int noteWidth)
		{
			NoteData noteData = new NoteData();
			DataRow dataRow = noteData.NoteTable.NewRow();
			dataRow["NoteText"] = noteText;
			dataRow["CreatedBy"] = noteCreatorID;
			dataRow["NoteColor"] = noteColor;
			dataRow["NoteFlag"] = checked((byte)noteFlag);
			dataRow["NoteHeight"] = noteHeight;
			dataRow["NoteWidth"] = noteWidth;
			noteData.NoteTable.Rows.Add(dataRow);
			return CreateNote(noteData);
		}

		public int CreateNote(NoteData noteData)
		{
			int result = -1;
			if (new Notes(config).InsertNote(noteData))
			{
				try
				{
					string text = noteData.NoteTable.Rows[0]["NoteID"].ToString();
					if (text.Length != 0)
					{
						return int.Parse(text);
					}
					return result;
				}
				catch
				{
					return -1;
				}
			}
			return result;
		}

		public bool UpdateNote(NoteData noteData)
		{
			return new Notes(config).UpdateNote(noteData);
		}

		public bool UpdateNoteLocation(NoteData noteData)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.UpdateNoteLocation(noteData);
			}
		}

		public NoteData GetNotesByUserID(int userID)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.GetNotesByUserID(userID);
			}
		}

		public NoteData GetNoteUsers(int noteID)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.GetNoteUsers(noteID);
			}
		}

		public bool AssignNoteUsers(int noteID, ArrayList users, NoteFlags noteFlag)
		{
			if (users == null)
			{
				throw new NullReferenceException("Users array is null.");
			}
			NoteData noteData = new NoteData();
			foreach (int user in users)
			{
				DataRow dataRow = noteData.NoteUsersTable.NewRow();
				dataRow["UserID"] = user;
				dataRow["NoteID"] = noteID;
				dataRow["NoteFlag"] = noteFlag;
				noteData.NoteUsersTable.Rows.Add(dataRow);
			}
			using (Notes notes = new Notes(config))
			{
				if (users.Count == 0)
				{
					return ClearAllNoteUsers(noteID);
				}
				return notes.AssignNoteUsers(noteData);
			}
		}

		public bool RemoveNoteUser(int noteID, int userID)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.RemoveNoteUser(noteID, userID);
			}
		}

		public bool DeleteNote(int noteID)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.DeleteNote(noteID);
			}
		}

		public bool DeleteNote(int[] notesID)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.DeleteNote(notesID);
			}
		}

		public NoteData GetNoteByScreenID(int userID, int screendID)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.GetNoteByScreenID(userID, screendID);
			}
		}

		public DateTime GetNoteDateTimeStamp(int noteID)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.GetNoteDateTimeStamp(noteID);
			}
		}

		public DataSet GetReminderNote(int userID, DateTime reminderDate)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.GetReminderNote(userID, reminderDate);
			}
		}

		public NoteData GetNotesByID(int id)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.GetNotesByID(id);
			}
		}

		public NoteData GetNotesByType(NoteTypes noteType)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.GetNotesByType(noteType);
			}
		}

		public NoteData GetNotesByType(NoteTypes noteType, int referenceID)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.GetNotesByType(noteType, referenceID);
			}
		}

		public DataSet GetNotesByFields(NoteTypes noteType, params string[] columns)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.GetNotesByFields(noteType, columns);
			}
		}

		public DataSet GetNotesByFields(int noteID, params string[] columns)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.GetNotesByFields(noteID, columns);
			}
		}

		public DataSet GetNotesByFields(NoteTypes noteType, int referenceID, params string[] columns)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.GetNotesByFields(noteType, referenceID, columns);
			}
		}

		public int GetReminderNoteCount(int userID, DateTime reminderDate)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.GetReminderNoteCount(userID, reminderDate);
			}
		}

		public bool SetNoteFlag(int userID, int noteID, NoteFlags noteFlag)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.SetNoteFlag(userID, noteID, noteFlag);
			}
		}

		public bool ActivateNote(object id, bool activate)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.ActivateNote(id, activate);
			}
		}

		public NoteData GetNotesByUserID(int userID, bool isInactive)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.GetNotesByUserID(userID, isInactive);
			}
		}

		public DataSet GetNotesByFields(NoteFlags[] noteFlags, NoteTypes[] noteTypes, int[] references, int maxNotes, DateTime reminderFrom, DateTime reminderTo, bool isInactive, params string[] columns)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.GetNotesByFields(noteFlags, noteTypes, references, maxNotes, reminderFrom, reminderTo, isInactive, columns);
			}
		}

		public bool SetAlarm(object id, bool isAlarm)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.SetAlarm(id, isAlarm);
			}
		}

		public bool SetAlarm(object id, bool isAlarm, DateTime nextReminderDate)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.SetAlarm(id, isAlarm, nextReminderDate);
			}
		}

		public bool ClearAllNoteUsers(int notID)
		{
			using (Notes notes = new Notes(config))
			{
				return notes.ClearAllNoteUsers(notID);
			}
		}
	}
}
