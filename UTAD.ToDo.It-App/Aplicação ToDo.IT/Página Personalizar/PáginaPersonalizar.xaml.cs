﻿using Aplicação_ToDo.IT.Página_Ajuda;
using Aplicação_ToDo.IT.Página_Calendário;
using Aplicação_ToDo.IT.Página_Definições;
using Aplicação_ToDo.IT.Página_Inicial;
using Aplicação_ToDo.IT.Página_Tarefas;
using Aplicação_ToDo.IT.SaveData;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows;
using System.IO;
using System.Xml.Linq;

namespace Aplicação_ToDo.IT.Página_Personalizar
{
    public partial class PáginaPersonalizar : Window
    {
        public PáginaPersonalizar()
        {
            InitializeComponent();

            // Exibir o nome de usuário e o e-mail do usuário
            UsernameTextBlock.Text = CurrentUser.User.Username;
            EmailTextBlock.Text = CurrentUser.User.Email;
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
            CurrentUser.User.Tema = "Light";
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
            CurrentUser.User.Tema = "Dark";
            SaveUserTheme();

            PáginaPersonalizar mainWindow = new PáginaPersonalizar();
            mainWindow.Show();
            this.Close();
        }

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveUserTheme()
        {
            // Carregar o arquivo JSON
            string jsonFilePath = "C:\\Users\\Luís Lemos\\source\\repos\\PL5_G04\\UTAD.ToDo.It-App\\Aplicação ToDo.IT\\SaveData\\utilizadores.json";
            string json = File.ReadAllText(jsonFilePath);
            List<UserData> users = JsonConvert.DeserializeObject<List<UserData>>(json);

            // Encontrar o usuário atual na lista
            UserData user = users.Where(u => u.Email == CurrentUser.User.Email).FirstOrDefault();

            // Se o usuário foi encontrado, atualizar o tema
            if (user != null)
            {
                user.Tema = CurrentUser.User.Tema;
            }

            // Salvar o arquivo JSON
            json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(jsonFilePath, json);
        }

        private void ChangeTheme(string theme)
        {
            // Remover todos os dicionários de recursos atuais
            Application.Current.Resources.MergedDictionaries.Clear();

            // Adicionar o novo dicionário de recursos
            var resourceDictionary = new ResourceDictionary { Source = new Uri(theme, UriKind.Relative) };
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }

        private void ColorREDButton_Click(object sender, RoutedEventArgs e)
        {
            // Código para mudar para o tema dark mode
            ChangeTheme("Temas/DarkRED.xaml");

            // Salve a escolha do tema do usuário
            CurrentUser.User.Tema = "DarkRED";
            SaveUserTheme();

            PáginaPersonalizar mainWindow = new PáginaPersonalizar();
            mainWindow.Show();
            this.Close();
        }
    }
}