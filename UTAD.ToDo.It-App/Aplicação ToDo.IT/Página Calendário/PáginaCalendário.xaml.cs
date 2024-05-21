﻿using Aplicação_ToDo.IT.Página_Ajuda;
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
            public int Id { get; set; }
            public string Assunto { get; set; }
            public DateTime DataInicio { get; set; }
            public DateTime DataFim { get; set; }

            // Adicione mais propriedades conforme necessário
        }

        List<Evento> eventos = new List<Evento>
        {

        };
        public List<string> MostrarEventos()
        {
            List<string> eventosFormatados = new List<string>();
            foreach (var evento in eventos)
            {
                eventosFormatados.Add($"ID: {evento.Id}, Assunto: {evento.Assunto}, Data de Início: {evento.DataInicio}, Data de Fim: {evento.DataFim}");
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
                    Subject = evento.Assunto,
                    StartTime = evento.DataInicio,
                    EndTime = evento.DataFim,
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
                    Assunto = e.Appointment.Subject,
                    DataInicio = e.Appointment.StartTime,
                    DataFim = e.Appointment.EndTime,
                };

                // Verifique se o evento já existe na lista
                var eventoExistente = eventos.FirstOrDefault(x => x.Id == novoEvento.Id);

                if (eventoExistente != null)
                {
                    // Atualize o evento existente
                    eventoExistente.Assunto = novoEvento.Assunto;
                    eventoExistente.DataInicio = novoEvento.DataInicio;
                    eventoExistente.DataFim = novoEvento.DataFim;
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

        private ScheduleAppointment draggedAppointment;

        private void Calendário_AppointmentDragStarting(object sender, Syncfusion.UI.Xaml.Scheduler.AppointmentDragStartingEventArgs e)
        {
            draggedAppointment = e.Appointment as ScheduleAppointment;
        }

        private void Calendário_AppointmentDropping(object sender, Syncfusion.UI.Xaml.Scheduler.AppointmentDroppingEventArgs e)
        {
            if (draggedAppointment != null)
            {
                // Encontre o evento correspondente na lista
                var evento = eventos.FirstOrDefault(x => x.Id == (int)draggedAppointment.Id);

                if (evento != null)
                {
                    // Calcule a duração do compromisso
                    var duration = draggedAppointment.EndTime - draggedAppointment.StartTime;

                    // Atualize a data de início e fim do evento
                    evento.DataInicio = e.DropTime;
                    evento.DataFim = e.DropTime.Add(duration);
                }

                MostrarEventos();
                SalvarEventos();
            }

            draggedAppointment = null;
        }
    }
}
