using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEventSystem
	{
		bool CreateEvent(EventData eventData);

		bool UpdateEvent(EventData eventData);

		EventData GetEvent();

		bool DeleteEvent(string ID);

		EventData GetEventByID(string id);

		DataSet GetEventByFields(params string[] columns);

		DataSet GetEventByFields(string[] ids, params string[] columns);

		DataSet GetEventByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetEventList();

		DataSet GetEventComboList();

		DataSet GetEventSourceComboList();

		DataSet GetEventDocumentAddress(string eventID, string addressField);

		DataSet GetEventListReport(string fromEvent, string toEvent, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, bool showInactive);

		DataSet GetUpcomingEventsReport(string fromLead, string toLead, string fromUser, string toUser, bool showInactive);

		bool SetFlag(string eventID, byte flagID);
	}
}
