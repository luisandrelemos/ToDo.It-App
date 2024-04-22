using System.IO;
using System.Windows;
using System.Xml.Linq;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace Aplicação_ToDo.IT.Página_Autenticação
{

    public partial class PáginaRegisto : Window
    {
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

                // Carregar o arquivo XML ou criar um novo se não existir
                XDocument doc;
                string caminhoArquivoXml = "SaveData\\registros.xml";
                if (File.Exists(caminhoArquivoXml))
                {
                    doc = XDocument.Load(caminhoArquivoXml);
                }
                else
                {
                    doc = new XDocument(new XElement("registros"));
                }

                // Verificar se o e-mail já está em uso
                var usuarioExistente = doc.Root.Elements("userData").FirstOrDefault(u => (string)u.Element("email") == email.Text);
                if (usuarioExistente != null)
                {
                    throw new Exception("O E-Mail já está em uso!");
                }

                // Criar elemento XML para o novo usuário
                XElement novoUsuario = new XElement("userData",
                    new XElement("email", email.Text),
                    new XElement("username", username.Text),
                    new XElement("password", password.Password)
                );

                // Adicionar o novo usuário ao arquivo XML
                doc.Root.Add(novoUsuario);

                // Salvar o arquivo XML
                doc.Save(caminhoArquivoXml);

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
