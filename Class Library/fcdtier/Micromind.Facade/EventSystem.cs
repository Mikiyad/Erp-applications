using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EventSystem : MarshalByRefObject, IEventSystem, IDisposable
	{
		private Config config;

		public EventSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEvent(EventData data)
		{
			return new Events(config).InsertUpdateEvent(data, isUpdate: false);
		}

		public bool UpdateEvent(EventData data)
		{
			return UpdateEvent(data, checkConcurrency: false);
		}

		public bool UpdateEvent(EventData data, bool checkConcurrency)
		{
			return new Events(config).InsertUpdateEvent(data, isUpdate: true);
		}

		public EventData GetEvent()
		{
			using (Events events = new Events(config))
			{
				return events.GetEvent();
			}
		}

		public bool DeleteEvent(string eventID)
		{
			using (Events events = new Events(config))
			{
				return events.DeleteEvent(eventID);
			}
		}

		public EventData GetEventByID(string id)
		{
			using (Events events = new Events(config))
			{
				return events.GetEventByID(id);
			}
		}

		public DataSet GetEventByFields(params string[] columns)
		{
			using (Events events = new Events(config))
			{
				return events.GetEventByFields(columns);
			}
		}

		public DataSet GetEventByFields(string[] ids, params string[] columns)
		{
			using (Events events = new Events(config))
			{
				return events.GetEventByFields(ids, columns);
			}
		}

		public DataSet GetEventByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Events events = new Events(config))
			{
				return events.GetEventByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEventList()
		{
			using (Events events = new Events(config))
			{
				return events.GetEventList();
			}
		}

		public DataSet GetEventComboList()
		{
			using (Events events = new Events(config))
			{
				return events.GetEventComboList();
			}
		}

		public DataSet GetEventSourceComboList()
		{
			using (Events events = new Events(config))
			{
				return events.GetEventSourceComboList();
			}
		}

		public DataSet GetEventDocumentAddress(string crmEventID, string addressField)
		{
			using (Events events = new Events(config))
			{
				return events.GetEventDocumentAddress(crmEventID, addressField);
			}
		}

		public DataSet GetEventListReport(string fromEvent, string toEvent, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, bool showInactive)
		{
			using (Events events = new Events(config))
			{
				return events.GetEventListReport(fromEvent, toEvent, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, showInactive);
			}
		}

		public DataSet GetUpcomingEventsReport(string fromLead, string toLead, string fromUser, string toUser, bool showInactive)
		{
			using (Events events = new Events(config))
			{
				return events.GetUpcomingEventsReport(fromLead, toLead, fromUser, toUser, showInactive);
			}
		}

		public DataSet GetTopEvents(DateTime from, DateTime to, int count)
		{
			using (Events events = new Events(config))
			{
				return events.GetTopEvents(from, to, count);
			}
		}

		public bool SetFlag(string crmEventID, byte flagID)
		{
			using (Events events = new Events(config))
			{
				return events.SetFlag(crmEventID, flagID);
			}
		}
	}
}
