﻿using Aplicação_ToDo.IT.Página_Ajuda;
using Aplicação_ToDo.IT.Página_Definições;
using Aplicação_ToDo.IT.Página_Inicial;
using Aplicação_ToDo.IT.Página_Personalizar;
using Aplicação_ToDo.IT.SaveData;
using Syncfusion.UI.Xaml.Scheduler;
using System.Windows;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Net.Mail;
using System.Net;
using Aplicação_ToDo.IT.Página_Tarefas;

namespace Aplicação_ToDo.IT.Página_Calendário
{
    public partial class PáginaCalendário : Window
    {
        public ScheduleAppointmentCollection Appointments { get; set; } = new ScheduleAppointmentCollection();

        public ScheduleAppointment SelectedAppointment { get; set; }

        public DateTime? SelectedCellDateTime { get; set; }

        public PáginaCalendário()
        {
            InitializeComponent();

            // Exibir o nome de Utilizador e o e-mail do Utilizador
            UsernameTextBlock.Text = CurrentUser.User.Username;
            EmailTextBlock.Text = CurrentUser.User.Email;

            Calendário.AppointmentDeleting += Calendário_AppointmentDeleting;
            Calendário.ReminderAlertOpening += Scheduler_ReminderAlertOpening;
            Calendário.ReminderAlertActionChanged += OnScheduleReminderAlertActionChanged;

            CarregarEventos();
            MostrarEventos();
            CarregarEventosNoCalendario();

            // Vincular a coleção de compromissos ao controle Scheduler
            Calendário.ItemsSource = Appointments;
        }     

        public class Reminder
        {
            public bool Dismissed { get; set; }
            public TimeSpan TimeInterval { get; set; }
        }

        private static PáginaInicial páginaInicial;
        private void PáginaInicial_Click(object sender, RoutedEventArgs e)
        {
            if (páginaInicial == null || !páginaInicial.IsVisible)
            {
                páginaInicial = new PáginaInicial();
            }

            páginaInicial.Show();
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
            System.Windows.Application.Current.Shutdown();
        }

        private string arquivoEventos = @"C:\Users\Luís Lemos\source\repos\PL5_G04\UTAD.ToDo.It-App\Aplicação ToDo.IT\SaveData\eventos.json";
        private void SalvarEventos()
        {
            List<Evento> todosEventos = new List<Evento>();

            if (File.Exists(arquivoEventos))
            {
                string json = File.ReadAllText(arquivoEventos);
                todosEventos = JsonConvert.DeserializeObject<List<Evento>>(json);
            }

            // Remove os eventos do Utilizador atual
            todosEventos.RemoveAll(e => e.UserID == CurrentUser.User.Id);

            // Adiciona os eventos atualizados do Utilizador atual
            todosEventos.AddRange(eventos);

            // Opção de formatação para tornar o JSON mais legível
            string jsonToWrite = JsonConvert.SerializeObject(todosEventos, Formatting.Indented);
            File.WriteAllText(arquivoEventos, jsonToWrite);
        }

        private void CarregarEventos()
        {
            if (File.Exists(arquivoEventos))
            {
                string json = File.ReadAllText(arquivoEventos);
                eventos = JsonConvert.DeserializeObject<List<Evento>>(json).Where(e => e.UserID == CurrentUser.User.Id).ToList();
            }
        }
        public enum Importancia
        {
            Muito_Importante,
            Importante,
            Pouco_Importante,
            Indiferente
        }

        public class Evento
        {
            public int Id { get; set; }
            public string UserID { get; set; }
            public string Titulo { get; set; }
            public string Local { get; set; }
            public string Descrição { get; set; }
            public DateTime DataInicio { get; set; }
            public DateTime DataFim { get; set; }
            public TimeSpan TimeInicio { get; set; }
            public TimeSpan TimeFim { get; set; }
            public string DataInicioFormatada
            {
                get { return DataInicio.ToString("dd/MM/yyyy"); }
            }
            public string DataFimFormatada
            {
                get { return AllDay ? string.Empty : DataFim.ToString("dd/MM/yyyy"); }
            }
            public string HoraInicioFormatada
            {
                get { return AllDay ? string.Empty : DataInicio.ToString("HH:mm"); }
            }

            public string HoraFimFormatada
            {
                get { return AllDay ? string.Empty : DataFim.ToString("HH:mm"); }
            }
            public bool AllDay { get; set; }
            public Brush Cor { get; set; }
            public Brush CorTexto { get; set; }
            public Importancia Importancia { get; set; }
            public ObservableCollection<Reminder> Reminders { get; set; }
            public string RecurrenceRule { get; set; }
            public bool IsCompleted { get; set; }
        }

        public static List<Evento> eventos = new List<Evento>();
        public static List<Evento> tarefasConcluidas = new List<Evento>();
        public List<Evento> MostrarEventos()
        {
            return eventos;
        }

        private ObservableCollection<SchedulerReminder> ConverterLembretes(ObservableCollection<Reminder> reminders)
        {
            if (reminders == null)
                return null;

            var schedulerReminders = new ObservableCollection<SchedulerReminder>();
            foreach (var reminder in reminders)
            {
                schedulerReminders.Add(new SchedulerReminder
                {
                    IsDismissed = reminder.Dismissed,
                    ReminderTimeInterval = reminder.TimeInterval
                });
            }
            return schedulerReminders;
        }

        private void CarregarEventosNoCalendario()
        {
            Appointments.Clear();

            foreach (var evento in eventos)
            {
                ObservableCollection<SchedulerReminder> schedulerReminders = new ObservableCollection<SchedulerReminder>();

                if (evento.Reminders != null)
                {
                    foreach (var reminder in evento.Reminders)
                    {
                        schedulerReminders.Add(new SchedulerReminder
                        {
                            IsDismissed = reminder.Dismissed,
                            ReminderTimeInterval = reminder.TimeInterval
                        });
                    }
                }

                ScheduleAppointment appointment = new ScheduleAppointment
                {
                    Id = evento.Id,
                    Subject = evento.Titulo,
                    Location = evento.Local,
                    Notes = evento.Descrição,
                    StartTime = evento.DataInicio,
                    EndTime = evento.DataFim,
                    IsAllDay = evento.AllDay,
                    AppointmentBackground = evento.Cor,
                    Foreground = evento.CorTexto,
                    Reminders = schedulerReminders,
                    RecurrenceRule = evento.RecurrenceRule
                };

                Appointments.Add(appointment);
            }
        }

        private void Calendário_AppointmentEditorClosing(object sender, Syncfusion.UI.Xaml.Scheduler.AppointmentEditorClosingEventArgs e)
        {
            if (e.Appointment != null)
            {
                ObservableCollection<SchedulerReminder> reminders = e.Appointment.Reminders ?? new ObservableCollection<SchedulerReminder>();

                Evento novoEvento = new Evento
                {
                    Id = (int)e.Appointment.Id,
                    UserID = CurrentUser.User.Id,
                    Titulo = e.Appointment.Subject,
                    Local = string.IsNullOrEmpty(e.Appointment.Location) ? "Não Definida" : e.Appointment.Location,
                    Descrição = e.Appointment.Notes,
                    DataInicio = e.Appointment.IsAllDay ? e.Appointment.StartTime.Date : e.Appointment.StartTime,
                    DataFim = e.Appointment.IsAllDay ? e.Appointment.EndTime.Date : e.Appointment.EndTime,
                    AllDay = e.Appointment.IsAllDay,
                    Cor = e.Appointment.AppointmentBackground,
                    RecurrenceRule = e.Appointment.RecurrenceRule,
                    CorTexto = e.Appointment.Foreground,
                    Reminders = new ObservableCollection<Reminder>(
                        reminders.Select(r => new Reminder
                        {
                            Dismissed = r.IsDismissed,
                            TimeInterval = r.ReminderTimeInterval
                        })
                    ),
                    // Adiciona a importância como vazia por padrão
                    Importancia = Importancia.Indiferente
                };

                // Verifica se o evento já existe na lista
                var eventoExistente = eventos.FirstOrDefault(x => x.Id == novoEvento.Id);

                if (eventoExistente != null)
                {
                    // Atualiza o evento existente
                    eventoExistente.Titulo = novoEvento.Titulo;
                    eventoExistente.Local = novoEvento.Local;
                    eventoExistente.Descrição = novoEvento.Descrição;
                    eventoExistente.DataInicio = novoEvento.DataInicio;
                    eventoExistente.DataFim = novoEvento.DataFim;
                    eventoExistente.AllDay = novoEvento.AllDay;
                    eventoExistente.Cor = novoEvento.Cor;
                    eventoExistente.CorTexto = novoEvento.CorTexto;
                    eventoExistente.Reminders = novoEvento.Reminders;
                    eventoExistente.RecurrenceRule = novoEvento.RecurrenceRule;
                }
                else
                {
                    // Adiciona o novo evento à lista
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
                // Encontra o evento correspondente na lista
                var evento = eventos.FirstOrDefault(x => x.Id == (int)e.Appointment.Id);

                if (evento != null)
                {
                    // Remove o evento da lista
                    eventos.Remove(evento);
                }

                MostrarEventos();
                SalvarEventos();
                CarregarEventosNoCalendario();
            }
        }


        private ScheduleAppointment draggedAppointment;

        private void Calendário_AppointmentDragStarting(object sender, Syncfusion.UI.Xaml.Scheduler.AppointmentDragStartingEventArgs e)
        {
            draggedAppointment = e.Appointment as ScheduleAppointment;
        }

        private void Calendário_AppointmentDropping(object sender, Syncfusion.UI.Xaml.Scheduler.AppointmentDroppingEventArgs e)
        {
            if (draggedAppointment != null)
            {
                // Encontra o evento correspondente na lista
                var evento = eventos.FirstOrDefault(x => x.Id == (int)e.Appointment.Id);

                if (evento != null)
                {
                    // Calcula a duração do compromisso
                    var duration = draggedAppointment.EndTime - draggedAppointment.StartTime;

                    // Atualiza a data de início e fim do evento
                    evento.DataInicio = e.DropTime;
                    evento.DataFim = e.DropTime.Add(duration);
                }

                MostrarEventos();
                SalvarEventos();
            }

            draggedAppointment = null;
        }

        private void Scheduler_ReminderAlertOpening(object sender, ReminderAlertOpeningEventArgs e)
        {
            var reminders = e.Reminders;
            var appointment = e.Reminders[0].Appointment;
        }

        private void OnScheduleReminderAlertActionChanged(object sender, Syncfusion.UI.Xaml.Scheduler.ReminderAlertActionChangedEventArgs e)
        {
            if (e.ReminderAction == ReminderAction.Dismiss)
            {
                var reminder = e.Reminders[0];

                var evento = eventos.FirstOrDefault(x => x.Id == (int)reminder.Appointment.Id);
                if (evento != null)
                {
                    var eventoReminder = evento.Reminders.FirstOrDefault(r => r.TimeInterval == reminder.ReminderTimeInterval);
                    if (eventoReminder != null)
                    {
                        evento.Reminders.Remove(eventoReminder);
                    }

                    SalvarEventos();
                }
            }
            else if (e.ReminderAction == ReminderAction.DismissAll)
            {
                var reminders = e.Reminders;

                foreach (var reminder in reminders)
                {
                    var evento = eventos.FirstOrDefault(x => x.Id == (int)reminder.Appointment.Id);
                    if (evento != null)
                    {
                        var eventoReminder = evento.Reminders.FirstOrDefault(r => r.TimeInterval == reminder.ReminderTimeInterval);
                        if (eventoReminder != null)
                        {
                            eventoReminder.Dismissed = true;
                        }
                    }
                }

                SalvarEventos();
            }
            else if (e.ReminderAction == ReminderAction.Snooze)
            {
                var reminder = e.Reminders[0];
                var snoozeTime = e.SnoozeTime;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            if (menuItem.Header is "Editar")
            {
                if (this.SelectedAppointment != null)
                {
                    var evento = eventos.FirstOrDefault(x => x.Id == (int)SelectedAppointment.Id);
                    PáginaEditarTarefa editarTarefaWindow = new PáginaEditarTarefa(evento);
                    editarTarefaWindow.Owner = this;
                    editarTarefaWindow.ShowDialog();
                }
            }

            else if (menuItem.Header is "Adicionar")
            {
                Evento newEvent = new Evento();
                newEvent.DataInicio = (DateTime)this.SelectedCellDateTime;
                newEvent.DataFim = (DateTime)this.SelectedCellDateTime;
                PáginaEditarTarefa editarTarefaWindow = new PáginaEditarTarefa(newEvent);
                editarTarefaWindow.Owner = this;
                editarTarefaWindow.ShowDialog();
            }
        }

        private void Calendário_SchedulerContextMenuOpening(object sender, SchedulerContextMenuOpeningEventArgs e)
        {
            this.SelectedAppointment = e.MenuInfo.Appointment;
            this.SelectedCellDateTime = e.MenuInfo.DateTime;
        }

        private void Calendário_ReminderAlertOpening(object sender, ReminderAlertOpeningEventArgs e)
        {
            Evento evento = eventos.FirstOrDefault(x => x.Id == (int)e.Reminders[0].Appointment.Id);
            string fromMail = "todoitapplab@gmail.com";
            string fromPassword = "vyte qpsw zktv xovx";
            string toMail = CurrentUser.User.Email;
            string subject = "Lembrete";
            string body = $"Tem uma tarefa prestes a começar: \n Titulo:{evento.Titulo},\n Data:{evento.DataInicio}, \n Importância:{evento.Importancia}, \n Localização:{evento.Local}";

            MailMessage messagetouser = new MailMessage(fromMail, toMail)
            {
                Subject = subject,
                Body = body,

            };

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            try
            {
                smtpClient.Send(messagetouser);



            }
            catch (System.Exception ex)
            {

            }
        }
    }
}