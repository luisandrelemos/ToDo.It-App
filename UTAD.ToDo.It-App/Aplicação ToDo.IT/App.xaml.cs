using System.Configuration;
using System.Data;
using System.Windows;
using Syncfusion.Licensing;

namespace Aplicação_ToDo.IT
{

    public partial class App : Application
    {
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzI0MDk4NkAzMjM1MmUzMDJlMzBIZURTL25TUW8vaFRRVVc4V1pRSGVyVWFBYk1pK3g1NWFQc2w2Qng1WUF3PQ==");
        }
    }
}
