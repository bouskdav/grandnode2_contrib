using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace Grand.Plugin.ExchangeRate.CzechNationalBank
{
    internal class CnbExchange : IRateProvider
    {
        public async Task<IList<Domain.Directory.ExchangeRate>> GetCurrencyLiveRates()
        {
            //var request = (HttpWebRequest)WebRequest.Create("http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml");
            var request = (HttpWebRequest)WebRequest.Create("https://www.cnb.cz/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/denni_kurz.txt");
            using (var response = await request.GetResponseAsync())
            {
                List<List<string>> currencyLines = new List<List<string>>();

                // pøeète výsledek a vytvoøí list mìn
                using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    string tempLine;
                    while ((tempLine = reader.ReadLine()) != null)
                    {
                        List<string> line = tempLine.Split('|').ToList();
                        currencyLines.Add(line);
                    }
                }

                var updateDate = DateTime.ParseExact(currencyLines[0][0].Split(' ')[0], "dd.MM.yyyy", null);

                var provider = new NumberFormatInfo();
                provider.NumberDecimalSeparator = ",";
                provider.NumberGroupSeparator = "";

                var exchangeRates = new List<Grand.Domain.Directory.ExchangeRate>();
                foreach (var currencyLine in currencyLines.Skip(2))
                {
                    double amount = Double.Parse(currencyLine[2]);
                    double rate = Double.Parse(currencyLine[4], provider);

                    exchangeRates.Add(new Domain.Directory.ExchangeRate()
                        {
                            CurrencyCode = currencyLine[3],
                            Rate = 1 / (rate / amount),
                            UpdatedOn = updateDate
                        }
                    );
                }
                return exchangeRates;
            }
        }
    }
}
