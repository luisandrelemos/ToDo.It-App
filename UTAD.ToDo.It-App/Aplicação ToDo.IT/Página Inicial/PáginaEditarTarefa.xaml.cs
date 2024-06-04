using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Aplicação_ToDo.IT.Página_Calendário;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Aplicação_ToDo.IT.Página_Calendário.PáginaCalendário;

namespace Aplicação_ToDo.IT.Página_Inicial
{

    public partial class PáginaEditarTarefa : Window
    {
        private Evento evento;
        private List<Evento> eventos;

        public PáginaEditarTarefa()
        {
            InitializeComponent();
            CarregarEventos();
            evento = new Evento(); // Inicializa o objeto evento
            evento.Id = eventos.Any() ? eventos.Max(e => e.Id) + 1 : 1; // Gera um novo Id para o evento
        }

        public PáginaEditarTarefa(Evento evento) : this()
        {
            this.evento = evento ?? new Evento(); // Se o evento passado for null, inicializa um novo objeto Evento

            // Preenche os campos com os dados do evento
            add.Text = evento.Titulo;
            addlocal.Text = evento.Local;
            DateTimeInicio.Value = evento.DataInicio.Date;
            DateTimeFim.Value = evento.DataFim.Date;
            TimeInicio.Value = evento.DataInicio;
            TimeFim.Value = evento.DataFim;
            allDay.IsChecked = evento.AllDay;
            tb_mensagem.Text = evento.Descrição;
            switch (evento.Importancia)
            {
                case Importancia.Importante:
                    SortComboBox.SelectedIndex = 0;
                    break;
                case Importancia.Muito_Importante:
                    SortComboBox.SelectedIndex = 1;
                    break;
                case Importancia.Pouco_Importante:
                    SortComboBox.SelectedIndex = 2;
                    break;
                case Importancia.Indiferente:
                    SortComboBox.SelectedIndex = 3;
                    break;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtAdd_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(add.Text) && add.Text.Length > 0)
                textAdd.Visibility = Visibility.Collapsed;
            else
                textAdd.Visibility = Visibility.Visible;
        }

        private void textAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            add.Focus();
        }

        private void textAddLocal_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(addlocal.Text) && addlocal.Text.Length > 0)
                textAddLocal.Visibility = Visibility.Collapsed;
            else
                textAddLocal.Visibility = Visibility.Visible;
        }

        private void textAddLocal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            addlocal.Focus();
        }

        private void tb_mensagem_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CarregarEventos()
        {
            string path = @"C:\Users\Luís Lemos\source\repos\PL5_G04\UTAD.ToDo.It-App\Aplicação ToDo.IT\SaveData\eventos.json";
            if (System.IO.File.Exists(path))
            {
                string json = System.IO.File.ReadAllText(path);
                eventos = JsonConvert.DeserializeObject<List<Evento>>(json) ?? new List<Evento>();
            }
            else
            {
                eventos = new List<Evento>();
            }
        }

        private void GuardarNovaTarefa_Click(object sender, RoutedEventArgs e)
        {
            // Atualiza o evento com os novos dados
            evento.Titulo = string.IsNullOrEmpty(add.Text) ? "" : add.Text;
            evento.Local = string.IsNullOrEmpty(addlocal.Text) ? "" : addlocal.Text;
            evento.DataInicio = DateTimeInicio.Value?.Date.Add(this.TimeInicio.Value?.TimeOfDay ?? TimeSpan.Zero) ?? DateTime.Now;
            evento.DataFim = DateTimeFim.Value?.Date.Add(this.TimeFim.Value?.TimeOfDay ?? TimeSpan.Zero) ?? DateTime.Now;
            evento.AllDay = allDay.IsChecked ?? false;
            evento.Descrição = string.IsNullOrEmpty(tb_mensagem.Text) ? "" : tb_mensagem.Text;

            // Atualiza o evento na lista de eventos
            var eventoExistente = eventos.FirstOrDefault(x => x.Id == evento.Id);
            if (eventoExistente != null)
            {
                eventoExistente.Titulo = evento.Titulo;
                eventoExistente.Local = evento.Local;
                eventoExistente.DataInicio = evento.DataInicio;
                eventoExistente.DataFim = evento.DataFim;
                eventoExistente.AllDay = evento.AllDay;
                eventoExistente.Descrição = evento.Descrição;
                eventoExistente.Importancia = evento.Importancia;
            }
            else
            {
                eventos.Add(evento);
            }

            // Salva as alterações no arquivo JSON
            string json = JsonConvert.SerializeObject(eventos, Formatting.Indented);
            System.IO.File.WriteAllText(@"C:\Users\Luís Lemos\source\repos\PL5_G04\UTAD.ToDo.It-App\Aplicação ToDo.IT\SaveData\eventos.json", json);

            PáginaCalendário mainWindow = new PáginaCalendário();
            mainWindow.Show();
            this.Close();
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Obtém o item selecionado
            var selectedItem = (sender as ComboBox).SelectedItem as ComboBoxItem;

            // Verifica se o item selecionado não é null
            if (selectedItem != null)
            {
                // Atribui o valor do item selecionado à propriedade Importancia do objeto Evento
                switch (selectedItem.Content)
                {
                    case "Importante":
                        evento.Importancia = Importancia.Importante;
                        break;
                    case "Muito Importante":
                        evento.Importancia = Importancia.Muito_Importante;
                        break;
                    case "Pouco Importante":
                        evento.Importancia = Importancia.Pouco_Importante;
                        break;
                    case "Indiferente":
                        evento.Importancia = Importancia.Indiferente;
                        break;
                }
            }
        }
    }
}
