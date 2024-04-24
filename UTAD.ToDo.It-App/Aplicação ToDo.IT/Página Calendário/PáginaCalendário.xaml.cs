using Aplicação_ToDo.IT.Página_Ajuda;
using Aplicação_ToDo.IT.Página_Definições;
using Aplicação_ToDo.IT.Página_Inicial;
using Aplicação_ToDo.IT.Página_Personalizar;
using Aplicação_ToDo.IT.Página_Tarefas;
using Aplicação_ToDo.IT.SaveData;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Aplicação_ToDo.IT.Página_Calendário
{

    public partial class PáginaCalendário : Window
    {
        
        public PáginaCalendário()
        {
            InitializeComponent();

            // Exibir o nome de usuário e o e-mail do usuário
            UsernameTextBlock.Text = UserData.Username;
            EmailTextBlock.Text = UserData.Email;
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

        //-----------------------Create an event Data Model----------------------------

        public class Meeting : INotifyPropertyChanged
        {
            DateTime from, to;
            string eventName;
            bool isAllDay;
            string startTimeZone, endTimeZone;
            //Brush color;
            public Meeting()
            {
            }

            public DateTime From
            {
                get { return from; }
                set
                {
                    from = value;
                    RaisePropertyChanged("From");
                }
            }

            public DateTime To
            {
                get { return to; }
                set
                {
                    to = value;
                    RaisePropertyChanged("To");
                }
            }

            public bool IsAllDay
            {
                get { return isAllDay; }
                set
                {
                    isAllDay = value;
                    RaisePropertyChanged("IsAllDay");
                }
            }
            public string EventName
            {
                get { return eventName; }
                set
                {
                    eventName = value;
                    RaisePropertyChanged("EventName");
                }
            }
            public string StartTimeZone
            {
                get { return startTimeZone; }
                set
                {
                    startTimeZone = value;
                    RaisePropertyChanged("StartTimeZone");
                }
            }
            public string EndTimeZone
            {
                get { return endTimeZone; }
                set
                {
                    endTimeZone = value;
                    RaisePropertyChanged("EndTimeZone");
                }
            }

            /*public Brush Color
            {
                get { return color; }
                set
                {
                    color = value;
                    RaisePropertyChanged("Color");
                }
            }*/

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void RaisePropertyChanged(string propertyName, object oldValue = null)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        //-----------------------------------------------------------------------------------//

        
    }
}

