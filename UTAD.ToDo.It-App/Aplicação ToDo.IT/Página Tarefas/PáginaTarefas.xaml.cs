using Aplicação_ToDo.IT.Página_Ajuda;
using Aplicação_ToDo.IT.Página_Definições;
using Aplicação_ToDo.IT.Página_Inicial;
using Aplicação_ToDo.IT.Página_Personalizar;
using System.Windows;

namespace Aplicação_ToDo.IT.Página_Tarefas
{
    public partial class PáginaTarefas : Window
    {
        public PáginaTarefas()
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
            PáginaTarefas mainWindow = new PáginaTarefas();
            mainWindow.Show();
            this.Close();
        }

        private void Personalizar_Click(object sender, RoutedEventArgs e)
        {
            PáginaPersonalizar mainWindow = new PáginaPersonalizar();
            mainWindow.Show();
            this.Close();
        }

        private void Definições_Click(object sender, RoutedEventArgs e)
        {
            PáginaDefinições mainWindow = new PáginaDefinições();
            mainWindow.Show();
            this.Close();
        }

        private void Ajuda_Click(object sender, RoutedEventArgs e)
        {
            PáginaAjuda mainWindow = new PáginaAjuda();
            mainWindow.Show();
            this.Close();
        }

        private void Sair_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NovaTarefa_Click(object sender, RoutedEventArgs e)
        {
            // Crie uma nova instância da janela de criação de tarefa
            PáginaNovaTarefa novaTarefaWindow = new PáginaNovaTarefa();

            // Exiba a nova janela como um diálogo modal
            novaTarefaWindow.Owner = this;
            novaTarefaWindow.ShowDialog();
        }
    }
}
