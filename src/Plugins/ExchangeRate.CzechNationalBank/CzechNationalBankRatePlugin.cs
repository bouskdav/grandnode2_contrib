using Grand.Infrastructure.Plugins;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Grand.Plugin.Tests")]

namespace Grand.Plugin.ExchangeRate.CzechNationalBank
{
    public class CzechNationalBankRatePlugin : BasePlugin
    {
        public CzechNationalBankRatePlugin()
        {
        }

        public override async Task Install()
        {
            //locales
            await base.Install();
        }

        public override async Task Uninstall()
        {
            //locales
            await base.Uninstall();
        }
    }
}
