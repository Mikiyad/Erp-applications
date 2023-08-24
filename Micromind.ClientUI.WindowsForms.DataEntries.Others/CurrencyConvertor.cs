using Micromind.UISupport;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class CurrencyConvertor : Form
	{
		private IContainer components;

		private NumberTextBox numberTextBox1;

		private NumberTextBox numberTextBox2;

		private ComboBox comboBox1;

		private ComboBox comboBox2;

		private Label label1;

		private Label label2;

		private Label label3;

		private Label label4;

		private Button button1;

		public CurrencyConvertor()
		{
			InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			numberTextBox1 = new Micromind.UISupport.NumberTextBox();
			numberTextBox2 = new Micromind.UISupport.NumberTextBox();
			comboBox1 = new System.Windows.Forms.ComboBox();
			comboBox2 = new System.Windows.Forms.ComboBox();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			button1 = new System.Windows.Forms.Button();
			SuspendLayout();
			numberTextBox1.AllowDecimal = true;
			numberTextBox1.IsComboTextBox = false;
			numberTextBox1.Location = new System.Drawing.Point(84, 21);
			numberTextBox1.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			numberTextBox1.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			numberTextBox1.Name = "numberTextBox1";
			numberTextBox1.NullText = "0";
			numberTextBox1.Size = new System.Drawing.Size(138, 20);
			numberTextBox1.TabIndex = 0;
			numberTextBox2.AllowDecimal = true;
			numberTextBox2.IsComboTextBox = false;
			numberTextBox2.Location = new System.Drawing.Point(84, 120);
			numberTextBox2.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			numberTextBox2.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			numberTextBox2.Name = "numberTextBox2";
			numberTextBox2.NullText = "0";
			numberTextBox2.Size = new System.Drawing.Size(159, 20);
			numberTextBox2.TabIndex = 0;
			comboBox1.FormattingEnabled = true;
			comboBox1.Items.AddRange(new object[168]
			{
				"ustralian Dollar\t AUD",
				"British Pound\t GBP",
				"Euro\t EUR",
				"Japanese Yen\t JPY",
				"Swiss Franc\t CHF",
				"US Dollar\t USD",
				"Afghanistan Afghani\t AFN",
				"Albanian Lek\t ALL",
				"Algerian Dinar\t DZD",
				"Angolan Kwanza\t AOA",
				"Argentine Peso\t ARS",
				"Armenian Dram\t AMD",
				"Aruban Florin\t AWG",
				"Australian Dollar\t AUD",
				"Azerbaijan New Manat\t AZN",
				"Bahamian Dollar\t BSD",
				"Bahraini Dinar\t BHD",
				"Bangladeshi Taka\t BDT",
				"Barbados Dollar\t BBD",
				"Belarusian Ruble\t BYR",
				"Belize Dollar\t BZD",
				"Bermudian Dollar\t BMD",
				"Bhutan Ngultrum\t BTN",
				"Bolivian Boliviano\t BOB",
				"Bosnian Mark\t BAM",
				"Botswana Pula\t BWP",
				"Brazilian Real\t BRL",
				"British Pound\t GBP",
				"Brunei Dollar\t BND",
				"Bulgarian Lev\t BGN",
				"Burundi Franc\t BIF",
				"CFA Franc BCEAO\t XOF",
				"CFA Franc BEAC\t XAF",
				"CFP Franc\t XPF",
				"Cambodian Riel\t KHR",
				"Canadian Dollar\t CAD",
				"Cape Verde Escudo\t CVE",
				"Cayman Islands Dollar\t KYD",
				"Chilean Peso\t CLP",
				"Chinese Yuan/Renminbi\t CNY",
				"Colombian Peso\t COP",
				"Comoros Franc\t KMF",
				"Congolese Franc\t CDF",
				"Costa Rican Colon\t CRC",
				"Croatian Kuna\t HRK",
				"Cuban Convertible Peso\t CUC",
				"Cuban Peso\t CUP",
				"Cyprus Pound\t CYP",
				"Czech Koruna\t CZK",
				"Danish Krone\t DKK",
				"Djibouti Franc\t DJF",
				"Dominican R Peso\t DOP",
				"East Caribbean Dollar\t XCD",
				"Egyptian Pound\t EGP",
				"El Salvador Colon\t SVC",
				"Estonian Kroon\t EEK",
				"Ethiopian Birr\t ETB",
				"Euro\t EUR",
				"Falkland Islands Pound\t FKP",
				"Fiji Dollar\t FJD",
				"Gambian Dalasi\t GMD",
				"Georgian Lari\t GEL",
				"Ghanaian New Cedi\t GHS",
				"Gibraltar Pound\t GIP",
				"Gold (oz)\t XAU",
				"Guatemalan Quetzal\t GTQ",
				"Guinea Franc\t GNF",
				"Guyanese Dollar\t GYD",
				"Haitian Gourde\t HTG",
				"Honduran Lempira\t HNL",
				"Hong Kong Dollar\t HKD",
				"Hungarian Forint\t HUF",
				"Iceland Krona\t ISK",
				"Indian Rupee\t INR",
				"Indonesian Rupiah\t IDR",
				"Iranian Rial\t IRR",
				"Iraqi Dinar\t IQD",
				"Israeli New Shekel\t ILS",
				"Jamaican Dollar\t JMD",
				"Japanese Yen\t JPY",
				"Jordanian Dinar\t JOD",
				"Kazakhstan Tenge\t KZT",
				"Kenyan Shilling\t KES",
				"Kuwaiti Dinar\t KWD",
				"Kyrgyzstanian Som\t KGS",
				"Lao Kip\t LAK",
				"Latvian Lats\t LVL",
				"Lebanese Pound\t LBP",
				"Lesotho Loti\t LSL",
				"Liberian Dollar\t LRD",
				"Libyan Dinar\t LYD",
				"Lithuanian Litas\t LTL",
				"Macau Pataca\t MOP",
				"Macedonian Denar\t MKD",
				"Malagasy Ariary\t MGA",
				"Malawi Kwacha\t MWK",
				"Malaysian Ringgit\t MYR",
				"Maldive Rufiyaa\t MVR",
				"Maltese Lira\t MTL",
				"Mauritanian Ouguiya\t MRO",
				"Mauritius Rupee\t MUR",
				"Mexican Peso\t MXN",
				"Moldovan Leu\t MDL",
				"Mongolian Tugrik\t MNT",
				"Moroccan Dirham\t MAD",
				"Mozambique New Metical\t MZN",
				"Myanmar Kyat\t MMK",
				"NL Antillian Guilder\t ANG",
				"Namibia Dollar\t NAD",
				"Nepalese Rupee\t NPR",
				"New Zealand Dollar\t NZD",
				"Nicaraguan Cordoba Oro\t NIO",
				"Nigerian Naira\t NGN",
				"North Korean Won\t KPW",
				"Norwegian Kroner\t NOK",
				"Omani Rial\t OMR",
				"Pakistan Rupee\t PKR",
				"Panamanian Balboa\t PAB",
				"Papua New Guinea Kina\t PGK",
				"Paraguay Guarani\t PYG",
				"Peruvian Nuevo Sol\t PEN",
				"Philippine Peso\t PHP",
				"Polish Zloty\t PLN",
				"Qatari Rial\t QAR",
				"Romanian New Lei\t RON",
				"Russian Rouble\t RUB",
				"Rwandan Franc\t RWF",
				"Samoan Tala\t WST",
				"Sao Tome/Principe Dobra\t STD",
				"Saudi Riyal\t SAR",
				"Serbian Dinar\t RSD",
				"Seychelles Rupee\t SCR",
				"Sierra Leone Leone\t SLL",
				"Silver (oz)\t XAG",
				"Singapore Dollar\t SGD",
				"Slovak Koruna\t SKK",
				"Slovenian Tolar\t SIT",
				"Solomon Islands Dollar\t SBD",
				"Somali Shilling\t SOS",
				"South African Rand\t ZAR",
				"South-Korean Won\t KRW",
				"Sri Lanka Rupee\t LKR",
				"St Helena Pound\t SHP",
				"Sudanese Pound\t SDG",
				"Suriname Dollar\t SRD",
				"Swaziland Lilangeni\t SZL",
				"Swedish Krona\t SEK",
				"Swiss Franc\t CHF",
				"Syrian Pound\t SYP",
				"Taiwan Dollar\t TWD",
				"Tanzanian Shilling\t TZS",
				"Thai Baht\t THB",
				"Tonga Pa’anga\t TOP",
				"Trinidad/Tobago Dollar\t TTD",
				"Tunisian Dinar\t TND",
				"Turkish New Lira\t TRY",
				"Turkmenistan Manat\t TMM",
				"US Dollar\t USD",
				"Uganda Shilling\t UGX",
				"Ukraine Hryvnia\t UAH",
				"Uruguayan Peso\t UYU",
				"United Arab Emir Dirham\t AED",
				"Vanuatu Vatu\t VUV",
				"Venezuelan Bolivar\t VEB",
				"Vietnamese Dong\t VND",
				"Yemeni Rial\t YER",
				"Zambian Kwacha\t ZMK",
				"Zimbabwe Dollar\t ZWD"
			});
			comboBox1.Location = new System.Drawing.Point(84, 50);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new System.Drawing.Size(237, 21);
			comboBox1.TabIndex = 2;
			comboBox2.FormattingEnabled = true;
			comboBox2.Location = new System.Drawing.Point(84, 74);
			comboBox2.Name = "comboBox2";
			comboBox2.Size = new System.Drawing.Size(237, 21);
			comboBox2.TabIndex = 2;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 24);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(46, 13);
			label1.TabIndex = 3;
			label1.Text = "Amount:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 53);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(33, 13);
			label2.TabIndex = 4;
			label2.Text = "From:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(12, 77);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(23, 13);
			label3.TabIndex = 5;
			label3.Text = "To:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(12, 123);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(40, 13);
			label4.TabIndex = 5;
			label4.Text = "Result:";
			button1.Location = new System.Drawing.Point(229, 170);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(92, 26);
			button1.TabIndex = 6;
			button1.Text = "Convert";
			button1.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(333, 208);
			base.Controls.Add(button1);
			base.Controls.Add(label4);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(comboBox2);
			base.Controls.Add(comboBox1);
			base.Controls.Add(numberTextBox2);
			base.Controls.Add(numberTextBox1);
			base.Name = "CurrencyConvertor";
			Text = "CurrencyConvertor";
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
