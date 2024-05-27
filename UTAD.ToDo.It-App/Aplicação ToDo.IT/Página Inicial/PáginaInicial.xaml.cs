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
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using System.ComponentModel;

namespace Aplicação_ToDo.IT.Página_Inicial
{
    public partial class PáginaInicial : Window
    {
        public PáginaInicial()
        {
            InitializeComponent();

            // Exibir o nome de Utilizador e o e-mail do Utilizador
            UsernameTextBlock.Text = CurrentUser.User.Username;
            EmailTextBlock.Text =  CurrentUser.User.Email;

            PáginaCalendário páginaCalendário = new PáginaCalendário();
            List<Evento> eventos = páginaCalendário.MostrarEventos();

            ListaEventos.ItemsSource = eventos;
            ListaEventos2.ItemsSource = eventos;
            ListaEventos.SelectionChanged += ListaEventos_SelectionChanged;

        }

        private void NovaTarefa_Click(object sender, RoutedEventArgs e)
        {
            // Cria uma nova instância da janela de criação de tarefa
            PáginaNovaTarefa novaTarefaWindow = new PáginaNovaTarefa();

            // Exibe a Nova janela como um diálogo modal
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

        private void ListaEventos_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica se um evento da ListBox foi clicado
            var item = ItemsControl.ContainerFromElement((ListBox)sender, e.OriginalSource as DependencyObject) as ListBoxItem;

            if (item != null && e.LeftButton == MouseButtonState.Pressed)
            {
                PáginaCalendário mainWindow = new PáginaCalendário();
                mainWindow.Show();
                this.Close();
            }
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private Evento eventoSelecionado;

        private void ListaEventos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Obtem o evento selecionado
            eventoSelecionado = (Evento)ListaEventos.SelectedItem;
        }
        private void MenuEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Verifica se um evento foi selecionado
            if (eventoSelecionado != null)
            {
                // Remove o evento selecionado da lista
                PáginaCalendário.eventos.Remove(eventoSelecionado);

                // Salve a lista atualizada de eventos no arquivo JSON
                string json = JsonConvert.SerializeObject(PáginaCalendário.eventos, Formatting.Indented);
                File.WriteAllText(@"C:\Users\bruno\Source\Repos\PL5_G04\UTAD.ToDo.It-App\Aplicação ToDo.IT\SaveData\eventos.json", json);

                // Limpa a seleção
                ListaEventos.SelectedItem = null;
                eventoSelecionado = null;

                // Cria uma nova instância da página inicial e exiba-a
                PáginaInicial mainWindow = new PáginaInicial();
                mainWindow.Show();

                // Fecha a janela atual
                this.Close();
            }
        }

        private void MenuEditar_Click(object sender, RoutedEventArgs e)
        {
            // Cria uma nova instância da janela de criação de tarefa
            PáginaNovaTarefa novaTarefaWindow = new PáginaNovaTarefa();

            // Exibe a nova janela como um diálogo modal
            novaTarefaWindow.Owner = this;
            novaTarefaWindow.ShowDialog();
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedOption = (sender as ComboBox).SelectedItem as ComboBoxItem;

            // Limpa as descrições de classificação existentes
            ListaEventos.Items.SortDescriptions.Clear();

            switch (selectedOption.Content.ToString())
            {
                case "Data De Início":
                    ListaEventos.Items.SortDescriptions.Add(new SortDescription("DataInicio", ListSortDirection.Ascending));
                    break;
                case "Data de Fim":
                    ListaEventos.Items.SortDescriptions.Add(new SortDescription("DataFim", ListSortDirection.Descending));
                    break;
            }
        }
    }
}