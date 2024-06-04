using System.IO;
using System.Windows;
using System;
using System.Xml.Linq;
using System.Windows.Input;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Aplicação_ToDo.IT.Página_Autenticação
{

    public partial class PáginaRegisto : Window
    {
        public class UserData
        {
            public string Id { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Tema { get; set; }
        }

        public PáginaRegisto()
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

        private void Registar_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Verificar se os campos foram preenchidos
                if (string.IsNullOrEmpty(username.Text) || string.IsNullOrEmpty(password.Password) || string.IsNullOrEmpty(email.Text))
                {
                    throw new Exception("Introduza todos os campos!");
                }

                // Verificar se o e-mail é válido
                if (!IsValidEmail(email.Text))
                {
                    throw new Exception("O E-Mail digitado não é válido!");
                }

                // Carregar o arquivo JSON ou criar um novo se não existir
                string caminhoArquivoJson = "C:\\Users\\Luís Lemos\\source\\repos\\PL5_G04\\UTAD.ToDo.It-App\\Aplicação ToDo.IT\\SaveData\\utilizadores.json";
                List<UserData> usuarios;
                string json;
                if (File.Exists(caminhoArquivoJson))
                {
                    json = File.ReadAllText(caminhoArquivoJson);
                    try
                    {
                        usuarios = JsonConvert.DeserializeObject<List<UserData>>(json);
                    }
                    catch
                    {
                        usuarios = new List<UserData>();
                    }
                }
                else
                {
                    usuarios = new List<UserData>();
                }

                // Verificar se o e-mail já está em uso
                var usuarioExistente = usuarios.FirstOrDefault(u => u.Email == email.Text);
                if (usuarioExistente != null)
                {
                    throw new Exception("O E-Mail já está em uso!");
                }

                // Criar objeto para o novo Utilizador
                UserData novoUsuario = new UserData
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = email.Text,
                    Username = username.Text,
                    Password = password.Password,
                    Tema = "dark"
                };

                // Adicionar o novo Utilizador à lista
                usuarios.Add(novoUsuario);

                // Salvar a lista de usuários no arquivo JSON
                json = JsonConvert.SerializeObject(usuarios, Formatting.Indented);
                File.WriteAllText(caminhoArquivoJson, json);

                // Limpar campos
                email.Text = "";
                username.Text = "";
                password.Password = "";

                // Exibir mensagem de sucesso
                MessageBox.Show("Registro concluído com sucesso!");
                PaginaLogin l1 = new PaginaLogin();
                this.Close();
                l1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao tentar registrar: " + ex.Message);
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

        private void txtUsername_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(username.Text) && username.Text.Length > 0)
                textUsername.Visibility = Visibility.Collapsed;
            else
                textUsername.Visibility = Visibility.Visible;
        }

        private void textUsername_MouseDown(object sender, MouseButtonEventArgs e)
        {
            username.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PaginaLogin l1 = new PaginaLogin();
            this.Close();
            l1.Show();
        }

        private bool IsValidEmail(string email)
        {
            // Expressão regular para verificar a validade do e-mail
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}
