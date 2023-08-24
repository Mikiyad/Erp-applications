using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICustomFieldSystem
	{
		int CreateCustomFieldGroup(string groupName);

		int CreateCustomFieldGroup(CustomFieldGroupData customFieldGroupData);

		bool UpdateCustomFieldGroup(CustomFieldGroupData customFieldGroupData);

		DataSet GetGroups();

		CustomFieldGroupData GetGroupByID(int groupID);

		CustomFieldGroupData GetGroupByID(int[] groupsID);

		bool CreateUpdateCustomFieldSetup(CustomFieldSetupData customFieldSetupData, CustomFieldSetupTypes customFieldSetupType);

		CustomFieldSetupData GetCustomFieldSetup(CustomFieldSetupTypes customFieldSetupType);

		CustomFieldData GetCustomField(CustomFieldSetupTypes customFieldSetupType, int recordID);
	}
}
