using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Micromind.ClientUI.CurrencyConvertor
{
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class CurrencyConvertorSoapClient : ClientBase<CurrencyConvertorSoap>, CurrencyConvertorSoap
	{
		public CurrencyConvertorSoapClient()
		{
		}

		public CurrencyConvertorSoapClient(string endpointConfigurationName)
			: base(endpointConfigurationName)
		{
		}

		public CurrencyConvertorSoapClient(string endpointConfigurationName, string remoteAddress)
			: base(endpointConfigurationName, remoteAddress)
		{
		}

		public CurrencyConvertorSoapClient(string endpointConfigurationName, EndpointAddress remoteAddress)
			: base(endpointConfigurationName, remoteAddress)
		{
		}

		public CurrencyConvertorSoapClient(Binding binding, EndpointAddress remoteAddress)
			: base(binding, remoteAddress)
		{
		}

		public double ConversionRate(Currency FromCurrency, Currency ToCurrency)
		{
			return base.Channel.ConversionRate(FromCurrency, ToCurrency);
		}

		public Task<double> ConversionRateAsync(Currency FromCurrency, Currency ToCurrency)
		{
			return base.Channel.ConversionRateAsync(FromCurrency, ToCurrency);
		}
	}
}
