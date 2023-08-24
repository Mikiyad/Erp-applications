using Micromind.Common.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPrintTemplateSystem
	{
		int CreatePrintTemplate(PrintTemplateData printTemplateData);

		bool UpdatePrintTemplate(PrintTemplateData printTemplateData);

		bool UpdatePrintTemplate(int templateID, string data);

		PrintTemplateData GetPrintByID(int templateID);

		PrintTemplateData GetPrintTemplates(bool readOnly);

		PrintTemplateData GetPrintTemplates(PrintTemplateTypes[] types, bool readOnly);

		bool DeletePrintTemplate(int templateID);

		bool ExistPrintTemplate(string name);

		string GetTemplateLayout(int id);

		bool LoadPrintTemplateFile(string fileName, string templateName);

		bool LoadPrintTemplateStream(byte[] templateStream, string templateName);

		bool LoadPrintTemplateStream(byte[] stream, string templateName, bool overwrite);
	}
}
