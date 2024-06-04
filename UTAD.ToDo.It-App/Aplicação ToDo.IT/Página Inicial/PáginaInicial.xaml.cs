using Aplicação_ToDo.IT.Página_Ajuda;
using Aplicação_ToDo.IT.Página_Calendário;
using Aplicação_ToDo.IT.Página_Definições;
using Aplicação_ToDo.IT.Página_Personalizar;
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
using System.Windows.Threading;
using Aplicação_ToDo.IT.Página_Tarefas;

namespace Aplicação_ToDo.IT.Página_Inicial
{
    public partial class PáginaInicial : Window
    {
        public PáginaInicial()
        {
            InitializeComponent();

            // Exibir o nome de Utilizador e o e-mail do Utilizador
            UsernameTextBlock.Text = CurrentUser.User.Username;
            EmailTextBlock.Text = CurrentUser.User.Email;

            PáginaCalendário páginaCalendário = new PáginaCalendário();
            PáginaCalendário.eventos = páginaCalendário.MostrarEventos();

            // Filtra a lista de eventos para incluir apenas os eventos do usuário atual
            List<Evento> eventosDoUsuarioAtual = PáginaCalendário.eventos.Where(e => e.UserID == CurrentUser.User.Id).ToList();

            ListaEventos.ItemsSource = eventos;
            ListaEventos2.ItemsSource = eventos;
            ListaEventos.SelectionChanged += ListaEventos_SelectionChanged;
        }

        private void NovaTarefa_Click(object sender, RoutedEventArgs e)
        {
            // Cria uma nova instância da janela de criação de tarefa
            PáginaEditarTarefa editarTarefaWindow = new PáginaEditarTarefa();

            // Exibe a Nova janela como um diálogo modal
            editarTarefaWindow.Owner = this;
            editarTarefaWindow.ShowDialog();
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
            // Verifica se o evento pertence ao usuário atual
            if (eventoSelecionado.UserID == CurrentUser.User.Id)
            {
                // Remove o evento selecionado da lista
                PáginaCalendário.eventos.Remove(eventoSelecionado);

                // Salve a lista atualizada de eventos no arquivo JSON
                string json = JsonConvert.SerializeObject(PáginaCalendário.eventos, Formatting.Indented);
                File.WriteAllText(@"C:\Users\Luís Lemos\source\repos\PL5_G04\UTAD.ToDo.It-App\Aplicação ToDo.IT\SaveData\eventos.json", json);

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
            // Verifica se um evento foi selecionado
            if (eventoSelecionado != null)
            {
                // Cria uma nova instância da janela de criação de tarefa e passa o evento selecionado
                PáginaEditarTarefa editarTarefaWindow = new PáginaEditarTarefa(eventoSelecionado);

                // Exibe a nova janela como um diálogo modal
                editarTarefaWindow.Owner = this;
                editarTarefaWindow.ShowDialog();
            }
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
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var evento = (Evento)checkBox.DataContext;
            evento.IsCompleted = true;

            // Mova o evento para a lista de tarefas concluídas
            PáginaCalendário.eventos.Remove(evento);
            tarefasConcluidas.Add(evento);

            // Salve a lista atualizada de eventos e tarefas concluídas no arquivo JSON
            string jsonEventos = JsonConvert.SerializeObject(PáginaCalendário.eventos, Formatting.Indented);
            File.WriteAllText(@"C:\Users\Luís Lemos\source\repos\PL5_G04\UTAD.ToDo.It-App\Aplicação ToDo.IT\SaveData\eventos.json", jsonEventos);

            string jsonTarefasConcluidas = JsonConvert.SerializeObject(tarefasConcluidas, Formatting.Indented);
            File.WriteAllText(@"C:\Users\Luís Lemos\source\repos\PL5_G04\UTAD.ToDo.It-App\Aplicação ToDo.IT\SaveData\tarefasConcluidas.json", jsonTarefasConcluidas);

            // Cria uma nova instância da página inicial e exiba-a
            PáginaInicial mainWindow = new PáginaInicial();
            mainWindow.Show();

            // Fecha a janela atual
            this.Close();
        }

        private void CheckBox_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            checkBox.IsChecked = !checkBox.IsChecked;
            checkBox.ContextMenu = null;
            e.Handled = true;
        }
    }
}