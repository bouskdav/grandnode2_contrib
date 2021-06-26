using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grand.Plugin.ExchangeRate.CzechNationalBank
{
    internal interface IRateProvider
    {
        Task<IList<Domain.Directory.ExchangeRate>> GetCurrencyLiveRates();
    }
}
