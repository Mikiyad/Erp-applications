using System;
using System.Collections;
using System.IO;
using System.IO.IsolatedStorage;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Micromind.ClientLibraries
{
	[Serializable]
	
	
	public class IsolatedStorage : Hashtable
	{
		private string settingsFileName = Assembly.GetEntryAssembly().GetName().Name + ".dat";

		public IsolatedStorage()
		{
			LoadData();
		}

		protected IsolatedStorage(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void LoadData()
		{
			try
			{
				IsolatedStorageFile store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);
				if (store.GetFileNames(settingsFileName).Length != 0)
				{
					Stream stream = new IsolatedStorageFileStream(settingsFileName, FileMode.OpenOrCreate, store);
					if (stream != null)
					{
						try
						{
							IFormatter formatter = new BinaryFormatter();
							Hashtable hashtable = null;
							try
							{
								hashtable = (Hashtable)formatter.Deserialize(stream);
							}
							catch
							{
								hashtable = new Hashtable();
							}
							IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
							while (enumerator.MoveNext())
							{
								this[enumerator.Key] = enumerator.Value;
							}
						}
						finally
						{
							stream.Close();
						}
					}
				}
			}
			catch
			{
				throw;
			}
		}

		public void ReLoad()
		{
			LoadData();
		}

		public void Save()
		{
			IsolatedStorageFile store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);
			Stream stream = new IsolatedStorageFileStream(settingsFileName, FileMode.Create, store);
			if (stream != null)
			{
				try
				{
					((IFormatter)new BinaryFormatter()).Serialize(stream, (object)this);
				}
				finally
				{
					stream.Close();
				}
			}
		}
	}
}
