using Aplicação_ToDo.IT.Página_Ajuda;
using Aplicação_ToDo.IT.Página_Calendário;
using Aplicação_ToDo.IT.Página_Definições;
using Aplicação_ToDo.IT.Página_Inicial;
using Aplicação_ToDo.IT.Página_Tarefas;
using Aplicação_ToDo.IT.SaveData;
using System.Windows;
using System.Xml.Linq;

namespace Aplicação_ToDo.IT.Página_Personalizar
{
    public partial class PáginaPersonalizar : Window
    {
        public PáginaPersonalizar()
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

        private void LightModeButton_Click(object sender, RoutedEventArgs e)
        {
            // Código para mudar para o tema light mode
            ChangeTheme("Temas/Light.xaml");

            // Salve a escolha do tema do usuário
            UserData.Theme = "Light";
            SaveUserTheme();

            PáginaPersonalizar mainWindow = new PáginaPersonalizar();
            mainWindow.Show();
            this.Close();
        }

        private void DarkModeButton_Click(object sender, RoutedEventArgs e)
        {
            // Código para mudar para o tema dark mode
            ChangeTheme("Temas/Dark.xaml");

            // Salve a escolha do tema do usuário
            UserData.Theme = "Dark";
            SaveUserTheme();

            PáginaPersonalizar mainWindow = new PáginaPersonalizar();
            mainWindow.Show();
            this.Close();
        }

        private void SaveUserTheme()
        {
            // Carregar o arquivo XML
            XDocument doc = XDocument.Load("C:\\Users\\Luís Lemos\\source\\repos\\ToDo.It-App\\UTAD.ToDo.It-App\\Aplicação ToDo.IT\\SaveData\\registros.xml");

            // Encontrar o usuário correspondente ao e-mail
            var user = (from u in doc.Root.Elements("userData")
                        where (string)u.Element("email") == UserData.Email
                        select u).FirstOrDefault();

            if (user != null)
            {
                // Atualizar o tema do usuário no arquivo XML
                user.Element("theme").Value = UserData.Theme;
                doc.Save("C:\\Users\\Luís Lemos\\source\\repos\\ToDo.It-App\\UTAD.ToDo.It-App\\Aplicação ToDo.IT\\SaveData\\registros.xml");
            }
        }

        private void ChangeTheme(string theme)
        {
            // Remover todos os dicionários de recursos atuais
            Application.Current.Resources.MergedDictionaries.Clear();

            // Adicionar o novo dicionário de recursos
            var resourceDictionary = new ResourceDictionary { Source = new Uri(theme, UriKind.Relative) };
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}