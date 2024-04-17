using System.Configuration;
using System.Data;
using System.Windows;

namespace Página_Inicial
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class HomePageApp : Application
    {
        public HomePageApp()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cWWNCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXtfd3RTQmNcV0F+W0E=");
        }
        
    }

}
