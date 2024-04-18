using System.Windows;

namespace Página_Inicial
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void NovaTarefa_Click(object sender, RoutedEventArgs e)
        {
            // Crie uma nova instância da janela de criação de tarefa
            NovaTarefaWindow novaTarefaWindow = new NovaTarefaWindow();

            // Exiba a nova janela como um diálogo modal
            novaTarefaWindow.Owner = this;
            novaTarefaWindow.ShowDialog();
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

        }

        private void Sair_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}