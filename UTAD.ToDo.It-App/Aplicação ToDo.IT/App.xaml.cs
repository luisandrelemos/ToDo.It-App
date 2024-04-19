using System.Configuration;
using System.Data;
using System.Windows;

namespace Aplicação_ToDo.IT
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class ToDoItApp : Application
    {
        public ToDoItApp()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cWWNCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXtfeXRdRmJfUEZyVko=");
        }
    }
}
