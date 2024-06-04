using Aplicação_ToDo.IT.Página_Ajuda;
using Aplicação_ToDo.IT.Página_Calendário;
using Aplicação_ToDo.IT.Página_Definições;
using Aplicação_ToDo.IT.Página_Inicial;
using Aplicação_ToDo.IT.Página_Personalizar;
using Aplicação_ToDo.IT.SaveData;
using Newtonsoft.Json;
using Syncfusion.UI.Xaml.Scheduler;
using System.IO;
using System.Windows;
using static Aplicação_ToDo.IT.Página_Calendário.PáginaCalendário;

namespace Aplicação_ToDo.IT.Página_Tarefas
{
    public partial class PáginaTarefas : Window
    {

        public PáginaTarefas()
        {
            InitializeComponent();

            // Exibir o nome de usuário e o e-mail do usuário
            UsernameTextBlock.Text = CurrentUser.User.Username;
            EmailTextBlock.Text = CurrentUser.User.Email;

            // Carregue as tarefas concluídas do arquivo JSON
            string jsonTarefasConcluidas = File.ReadAllText(@"C:\Users\Luís Lemos\source\repos\PL5_G04\UTAD.ToDo.It-App\Aplicação ToDo.IT\SaveData\tarefasConcluidas.json");
            List<Evento> tarefasConcluidas = JsonConvert.DeserializeObject<List<Evento>>(jsonTarefasConcluidas);

            // Filtrar tarefas concluídas para o usuário atual
            List<Evento> tarefasConcluidasDoUsuarioAtual = tarefasConcluidas.Where(tarefa => tarefa.UserID == CurrentUser.User.Id).ToList();

            // Defina as tarefas concluídas do usuário atual como a fonte de itens para o ListBox
            TarefasConcluidasListBox.ItemsSource = tarefasConcluidasDoUsuarioAtual;
        }

        public void MarcarComoConcluida(Evento tarefa)
        {
            // Carregue as tarefas concluídas do arquivo JSON
            string jsonTarefasConcluidas = File.ReadAllText(@"C:\Users\Luís Lemos\source\repos\PL5_G04\UTAD.ToDo.It-App\Aplicação ToDo.IT\SaveData\tarefasConcluidas.json");
            List<Evento> tarefasConcluidas = JsonConvert.DeserializeObject<List<Evento>>(jsonTarefasConcluidas);

            // Adicione a nova tarefa à lista
            tarefasConcluidas.Add(tarefa);

            // Salve a lista atualizada no arquivo JSON
            string jsonTarefasConcluidasAtualizadas = JsonConvert.SerializeObject(tarefasConcluidas);
            File.WriteAllText(@"C:\Users\Luís Lemos\source\repos\PL5_G04\UTAD.ToDo.It-App\Aplicação ToDo.IT\SaveData\tarefasConcluidas.json", jsonTarefasConcluidasAtualizadas);
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
    }
}
