using Aplicação_ToDo.IT.Página_Ajuda;
using Aplicação_ToDo.IT.Página_Calendário;
using Aplicação_ToDo.IT.Página_Definições;
using Aplicação_ToDo.IT.Página_Personalizar;
using Aplicação_ToDo.IT.Página_Tarefas;
using Aplicação_ToDo.IT.SaveData;
using System.Windows;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using static Aplicação_ToDo.IT.Página_Calendário.PáginaCalendário;
using System.Windows.Controls;
using System.Windows.Media;

namespace Aplicação_ToDo.IT.Página_Inicial
{
    public partial class PáginaInicial : Window
    {
        public PáginaInicial()
        {
            InitializeComponent();

            // Exibir o nome de usuário e o e-mail do usuário
            UsernameTextBlock.Text = CurrentUser.User.Username;
            EmailTextBlock.Text =  CurrentUser.User.Email;

            PáginaCalendário páginaCalendário = new PáginaCalendário();
            List<Evento> eventos = páginaCalendário.MostrarEventos();

            ListaEventos.ItemsSource = eventos;
            ListaEventos2.ItemsSource = eventos;
        }
       
  
        private void NovaTarefa_Click(object sender, RoutedEventArgs e)
        {
            // Crie uma nova instância da janela de criação de tarefa
            PáginaNovaTarefa novaTarefaWindow = new PáginaNovaTarefa();

            // Exiba a nova janela como um diálogo modal
            novaTarefaWindow.Owner = this;
            novaTarefaWindow.ShowDialog();
        }

        private void PáginaInicial_Click(object sender, RoutedEventArgs e)
        {
            PáginaInicial mainWindow = new PáginaInicial();
            mainWindow.Show();
            this.Close();
        }

        private void Calendário_Click(object sender, RoutedEventArgs e)
        {
            PáginaCalendário mainWindow = new PáginaCalendário();
            mainWindow.Show();
            this.Close();
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

        private void Perfil_Click(object sender, RoutedEventArgs e)
        {
            PáginaDefinições mainWindow = new PáginaDefinições();
            mainWindow.Show();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox.Background = new SolidColorBrush(Colors.Transparent); 
        }
    }
}