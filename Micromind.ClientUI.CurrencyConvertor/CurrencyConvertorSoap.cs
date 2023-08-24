using System.CodeDom.Compiler;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Micromind.ClientUI.CurrencyConvertor
{
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[ServiceContract(Namespace = "http://www.webserviceX.NET/", ConfigurationName = "CurrencyConvertor.CurrencyConvertorSoap")]
	public interface CurrencyConvertorSoap
	{
		[OperationContract(Action = "http://www.webserviceX.NET/ConversionRate", ReplyAction = "*")]
		[XmlSerializerFormat(SupportFaults = true)]
		double ConversionRate(Currency FromCurrency, Currency ToCurrency);

		[OperationContract(Action = "http://www.webserviceX.NET/ConversionRate", ReplyAction = "*")]
		Task<double> ConversionRateAsync(Currency FromCurrency, Currency ToCurrency);
	}
}
