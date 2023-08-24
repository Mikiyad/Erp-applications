using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;

namespace Micromind.Facade
{
	public sealed class PrintTemplateSystem : MarshalByRefObject, IPrintTemplateSystem, IDisposable
	{
		private Config config;

		public PrintTemplateSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public int CreatePrintTemplate(PrintTemplateData printTemplateData)
		{
			using (PrintTemplates printTemplates = new PrintTemplates(config))
			{
				if (printTemplates.InsertPrint(printTemplateData))
				{
					return int.Parse(printTemplateData.PrintTemplatesTable.Rows[0]["TemplateID"].ToString());
				}
				return -1;
			}
		}

		public bool UpdatePrintTemplate(PrintTemplateData printTemplateData)
		{
			using (PrintTemplates printTemplates = new PrintTemplates(config))
			{
				return printTemplates.UpdatePrint(printTemplateData);
			}
		}

		public bool UpdatePrintTemplate(int templateID, string data)
		{
			using (PrintTemplates printTemplates = new PrintTemplates(config))
			{
				return printTemplates.UpdatePrintTemplate(templateID, data);
			}
		}

		public PrintTemplateData GetPrintByID(int templateID)
		{
			using (PrintTemplates printTemplates = new PrintTemplates(config))
			{
				return printTemplates.GetPrintByID(templateID);
			}
		}

		public PrintTemplateData GetPrintTemplates(bool readOnly)
		{
			using (PrintTemplates printTemplates = new PrintTemplates(config))
			{
				return printTemplates.GetPrintTemplates(readOnly);
			}
		}

		public PrintTemplateData GetPrintTemplates(PrintTemplateTypes[] types, bool readOnly)
		{
			using (PrintTemplates printTemplates = new PrintTemplates(config))
			{
				return printTemplates.GetPrintTemplates(types, readOnly);
			}
		}

		public bool DeletePrintTemplate(int templateID)
		{
			using (PrintTemplates printTemplates = new PrintTemplates(config))
			{
				return printTemplates.DeletePrintTemplate(templateID);
			}
		}

		public bool ExistPrintTemplate(string name)
		{
			using (PrintTemplates printTemplates = new PrintTemplates(config))
			{
				return printTemplates.ExistPrintTemplate(name);
			}
		}

		public string GetTemplateLayout(int id)
		{
			using (PrintTemplates printTemplates = new PrintTemplates(config))
			{
				return printTemplates.GetTemplateLayout(id);
			}
		}

		public bool LoadPrintTemplateFile(string fileName, string templateName)
		{
			using (PrintTemplates printTemplates = new PrintTemplates(config))
			{
				return printTemplates.LoadPrintTemplateFile(fileName, templateName);
			}
		}

		public bool LoadPrintTemplateStream(byte[] stream, string templateName)
		{
			using (PrintTemplates printTemplates = new PrintTemplates(config))
			{
				return printTemplates.LoadPrintTemplateStream(stream, templateName);
			}
		}

		public bool LoadPrintTemplateStream(byte[] template, string templateName, bool overwrite)
		{
			using (PrintTemplates printTemplates = new PrintTemplates(config))
			{
				return printTemplates.LoadPrintTemplateStream(template, templateName, overwrite);
			}
		}
	}
}
