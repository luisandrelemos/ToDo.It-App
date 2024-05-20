using Aplicação_ToDo.IT.Página_Ajuda;
using Aplicação_ToDo.IT.Página_Autenticação;
using Aplicação_ToDo.IT.Página_Calendário;
using Aplicação_ToDo.IT.Página_Inicial;
using Aplicação_ToDo.IT.Página_Personalizar;
using Aplicação_ToDo.IT.Página_Tarefas;
using Aplicação_ToDo.IT.SaveData;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace Aplicação_ToDo.IT.Página_Definições
{
    public partial class PáginaDefinições : Window
    {
        public PáginaDefinições()
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

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar se as caixas de texto estão vazias
            if (string.IsNullOrWhiteSpace(tb_primeironome.Text) ||
                string.IsNullOrWhiteSpace(tb_email.Text) ||
                string.IsNullOrWhiteSpace(tb_passwordNova.Text) ||
                string.IsNullOrWhiteSpace(tb_password.Text))
            {
                MessageBox.Show("Por favor, preencha todos os campos antes de Guardar as alterações.");
                return;
            }

            // Carregar o arquivo XML
            XDocument doc = XDocument.Load("C:\\Users\\Luís Lemos\\source\\repos\\ToDo.It-App\\UTAD.ToDo.It-App\\Aplicação ToDo.IT\\SaveData\\registros.xml");

            // Encontrar o usuário atual no arquivo XML
            var user = doc.Descendants("userData")
                          .Where(u => u.Element("username").Value == UserData.Username)
                          .FirstOrDefault();

            // Se o usuário foi encontrado, verificar a senha antiga
            if (user != null)
            {
                if (user.Element("password").Value != tb_password.Text)
                {
                    MessageBox.Show("A senha antiga está incorreta.");
                    return;
                }

                user.Element("username").Value = tb_primeironome.Text;
                user.Element("email").Value = tb_email.Text;
                user.Element("password").Value = tb_passwordNova.Text;

                // Atualizar as propriedades da classe UserData
                UserData.Username = tb_primeironome.Text;
                UserData.Email = tb_email.Text;
            }

            // Salvar o arquivo XML
            doc.Save("C:\\Users\\Luís Lemos\\source\\repos\\ToDo.It-App\\UTAD.ToDo.It-App\\Aplicação ToDo.IT\\SaveData\\registros.xml");
            PáginaDefinições mainWindow = new PáginaDefinições();
            mainWindow.Show();
            this.Close();
        }

        private void TerminarSessao_Click(object sender, RoutedEventArgs e)
        {
            PaginaLogin mainWindow = new PaginaLogin();
            mainWindow.Show();
            this.Close();
        }

        private void tb_primeironome_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void ChangeImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.bmp)|*.png;*.jpeg;*.jpg;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                userImage.Source = new BitmapImage(fileUri);

                // Mantém a dimensão da imagem
                userImage.Width = 95;
                userImage.Height = 101;
            }
        }
    }
}