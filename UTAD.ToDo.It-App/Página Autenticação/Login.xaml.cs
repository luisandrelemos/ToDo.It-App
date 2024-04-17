using System.Windows;
using System.Windows.Input;
using System;
using System.Xml.Linq;
using System.IO;
using System.Linq;
using Página_Inicial;

namespace Login_Page
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
                // Carregar o arquivo XML
                XDocument doc = XDocument.Load("C:\\Users\\Luís Lemos\\source\\repos\\TODO.IT LAB\\Página Autenticação\\SaveData\\registros.xml");

                // Encontrar o usuário correspondente ao e-mail e senha
                var user = (from u in doc.Root.Elements("userData")
                            where (string)u.Element("email") == email.Text &&
                                  (string)u.Element("password") == password.Password
                            select u).FirstOrDefault();

                if (user != null)
                {
                    MainWindow mainWindow = new MainWindow();
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
            Registar r1 = new Registar();
            this.Close();
            r1.Show();
        }
    }
}