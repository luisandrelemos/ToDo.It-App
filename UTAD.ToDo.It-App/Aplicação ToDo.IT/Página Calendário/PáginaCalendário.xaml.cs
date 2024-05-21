using Aplicação_ToDo.IT.Página_Ajuda;
using Aplicação_ToDo.IT.Página_Definições;
using Aplicação_ToDo.IT.Página_Inicial;
using Aplicação_ToDo.IT.Página_Personalizar;
using Aplicação_ToDo.IT.Página_Tarefas;
using Aplicação_ToDo.IT.SaveData;
using Syncfusion.UI.Xaml.Scheduler;
using System.Diagnostics.Eventing.Reader;
using System.Windows;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Aplicação_ToDo.IT.Página_Calendário
{
    public partial class PáginaCalendário : Window
    {
        public ScheduleAppointmentCollection Appointments { get; set; } = new ScheduleAppointmentCollection();

        public PáginaCalendário()
        {
            InitializeComponent();

            // Exibir o nome de usuário e o e-mail do usuário
            UsernameTextBlock.Text = UserData.Username;
            EmailTextBlock.Text = UserData.Email;

            Calendário.AppointmentDeleting += Calendário_AppointmentDeleting;

            CarregarEventos();
            MostrarEventos();
            CarregarEventosNoCalendario();

            // Vincular a coleção de compromissos ao controle Scheduler
            Calendário.ItemsSource = Appointments;
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

        private string arquivoEventos = "eventos.json";

        private void SalvarEventos()
        {
            string json = JsonConvert.SerializeObject(eventos);
            File.WriteAllText(arquivoEventos, json);
        }

        private void CarregarEventos()
        {
            if (File.Exists(arquivoEventos))
            {
                string json = File.ReadAllText(arquivoEventos);
                eventos = JsonConvert.DeserializeObject<List<Evento>>(json);
            }
        }

        public class Evento
        {
            public Evento()
            {
            }
            public DateTime From { get; set; }
            public DateTime To { get; set; }
            public bool IsAllDay { get; set; }
            public string EventName { get; set; }
            public string Notes { get; set; }
            public string StartTimeZone { get; set; }
            public string EndTimeZone { get; set; }
            public string Importance { get; set; }
            public object RecurrenceId { get; set; }
            public int  Id { get; set; }
            public string RecurrenceRule { get; set; }
        }

        List<Evento> eventos = new List<Evento>
        {

        };
        public List<string> MostrarEventos()
        {
            List<string> eventosFormatados = new List<string>();
            foreach (var evento in eventos)
            {
                eventosFormatados.Add($"Nome: {evento.EventName}, Data de Início: {evento.From}, Data de Fim: {evento.To}, Importância: {evento.Importance}");
            }
            return eventosFormatados;
        }

        private void CarregarEventosNoCalendario()
        {
            Appointments.Clear();

            foreach (var evento in eventos)
            {
                ScheduleAppointment appointment = new ScheduleAppointment
                {
                    Id = evento.Id,
                    Subject = evento.EventName,
                    StartTime = evento.From,
                    EndTime = evento.To,
                };

                Appointments.Add(appointment);
            }
        }

        private void Calendário_AppointmentEditorClosing(object sender, Syncfusion.UI.Xaml.Scheduler.AppointmentEditorClosingEventArgs e)
        {
            if (e.Appointment != null)
            {
                Evento novoEvento = new Evento
                {
                    Id = (int)e.Appointment.Id,
                    EventName = e.Appointment.Subject,
                    From = e.Appointment.StartTime,
                    To = e.Appointment.EndTime,
                };

                // Verifique se o evento já existe na lista
                var eventoExistente = eventos.FirstOrDefault(x => x.Id == novoEvento.Id);

                if (eventoExistente != null)
                {
                    // Atualize o evento existente
                    eventoExistente.EventName = novoEvento.EventName;
                    eventoExistente.From = novoEvento.From;
                    eventoExistente.To = novoEvento.To;
                }
                else
                {
                    // Adicione o novo evento à lista
                    eventos.Add(novoEvento);
                }

                MostrarEventos();
                SalvarEventos();
            }
        }

        private void Calendário_AppointmentDeleting(object sender, Syncfusion.UI.Xaml.Scheduler.AppointmentDeletingEventArgs e)
        {
            if (e.Appointment != null)
            {
                // Encontre o evento correspondente na lista
                var evento = eventos.FirstOrDefault(x => x.Id == (int)e.Appointment.Id);

                if (evento != null)
                {
                    // Remova o evento da lista
                    eventos.Remove(evento);
                }

                MostrarEventos();
                SalvarEventos();
                CarregarEventosNoCalendario();
            }
        }
        public class Reminder
        {
            public bool Dismissed { get; set; }
            public TimeSpan TimeInterval { get; set; }
        }
    }
}
