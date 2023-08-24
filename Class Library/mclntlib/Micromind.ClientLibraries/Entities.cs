using System.Collections;
using System.Data;

namespace Micromind.ClientLibraries
{
	public class Entities
	{
		private ArrayList entityList = new ArrayList();

		public ArrayList EntityList => entityList;

		public void Load(DataSet dataSet, EntityTypes entityType)
		{
			switch (entityType)
			{
			case EntityTypes.employee:
				foreach (DataRow row in dataSet.Tables[0].Rows)
				{
					Entity entity = new Entity();
					entity.Value = row["EmployeeID"];
					entity.Name = row["LastName"].ToString() + ", " + row["FirstName"].ToString();
					entityList.Add(entity);
				}
				break;
			case EntityTypes.partner:
				foreach (DataRow row2 in dataSet.Tables[0].Rows)
				{
					Entity entity = new Entity();
					entity.Value = row2["PartnerID"];
					entity.Name = row2["Name"];
					entityList.Add(entity);
				}
				break;
			}
		}

		public void Load(DataSet dataSet, string id, string firstField, string lastField)
		{
			int num = 0;
			foreach (DataRow row in dataSet.Tables[0].Rows)
			{
				int length = row[firstField].ToString().Length;
				if (length > num)
				{
					num = length;
				}
			}
			checked
			{
				num += 15;
				foreach (DataRow row2 in dataSet.Tables[0].Rows)
				{
					string text = "";
					int length = row2[firstField].ToString().Length;
					int num2 = num - length;
					for (int i = 1; i < num2; i++)
					{
						text += " ";
					}
					Entity entity = new Entity();
					entity.Value = row2[id];
					entity.Name = row2[firstField].ToString() + text + "(" + row2[lastField].ToString() + ")";
					entityList.Add(entity);
				}
			}
		}

		public void Load(DataSet dataSet, string id, string firstField, string secondField, string lastField)
		{
			foreach (DataRow row in dataSet.Tables[0].Rows)
			{
				Entity entity = new Entity();
				entity.Value = row[id];
				entity.Name = row[firstField].ToString() + ", " + row[secondField].ToString() + ", " + row[lastField].ToString();
				entityList.Add(entity);
			}
		}
	}
}
