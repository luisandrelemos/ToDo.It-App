using Aplicação_ToDo.IT.Página_Inicial;
using System.Windows;

namespace Aplicação_ToDo.IT.Página_Ajuda
{

    public partial class PáginaAjuda : Window
    {
        public PáginaAjuda()
        {
            InitializeComponent();
        }
        private void PáginaInicial_Click(object sender, RoutedEventArgs e)
        {
            PáginaInicial mainWindow = new PáginaInicial();
            mainWindow.Show();
            this.Close();
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
        }

        private void Sair_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}


