using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Aplicação_ToDo.IT.Página_Inicial;
using Aplicação_ToDo.IT.SaveData;

namespace Aplicação_ToDo.IT.Página_Autenticação
{
    public partial class PaginaLogin : Window
    {
        public PaginaLogin()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(password.Password) && password.Password.Length > 0)
                textPassword.Visibility = Visibility.Collapsed;
            else
                textPassword.Visibility = Visibility.Visible;
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            password.Focus();
        }

        private void Login_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Carregar o arquivo JSON
                string jsonPath = @"C:\Users\bruno\Source\Repos\ToDo.It-App\UTAD.ToDo.It-App\Aplicação ToDo.IT\SaveData\utilizadores.json";
                string json = File.ReadAllText(jsonPath);

                // Deserializar o JSON para uma lista de usuários
                List<UserData> users = JsonConvert.DeserializeObject<List<UserData>>(json);

                // Encontrar o usuário correspondente ao e-mail e senha
                var user = users.FirstOrDefault(u => u.Email == email.Text && u.Password == password.Password);

                if (user != null)
                {
                    // Salvar o usuário na classe CurrentUser
                    CurrentUser.User = user;

                    // Aplicar o tema do usuário
                    ThemeManager.ChangeTheme($"Temas/{CurrentUser.User.Tema}.xaml");

                    PáginaInicial mainWindow = new PáginaInicial();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("O E-Mail ou a Password estão incorretos!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao tentar fazer login: " + ex.Message);
            }
        }

        private void txtEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(email.Text) && email.Text.Length > 0)
                textEmail.Visibility = Visibility.Collapsed;
            else
                textEmail.Visibility = Visibility.Visible;
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            email.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PáginaRegisto r1 = new PáginaRegisto();
            this.Close();
            r1.Show();
        }
    }
}