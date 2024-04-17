using Syncfusion.SfSkinManager;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ajuda;

namespace Página_Inicial
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PáginaInicial_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Calendário_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Tarefas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Personalizar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Definições_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Ajuda_Click(object sender, RoutedEventArgs e)
        {
            MainAjuda mainWindow = new MainAjuda();
            mainWindow.Show();
            this.Close();
        }

        private void Sair_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}