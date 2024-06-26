﻿using Aplicação_ToDo.IT.Página_Inicial;
using System.Net.Mail;
using System.Net;
using System.Windows;
using Aplicação_ToDo.IT.Página_Personalizar;
using Aplicação_ToDo.IT.Página_Definições;
using Aplicação_ToDo.IT.SaveData;
using Aplicação_ToDo.IT.Página_Calendário;
using Aplicação_ToDo.IT.Página_Tarefas;

namespace Aplicação_ToDo.IT.Página_Ajuda
{

    public partial class PáginaAjuda : Window
    {
        public PáginaAjuda()
        {
            InitializeComponent();

            // Exibir o nome de Utilizador e o e-mail do Utilizador
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tb_primeironome.Text) ||
                string.IsNullOrWhiteSpace(tb_ultimonome.Text) ||
                string.IsNullOrWhiteSpace(tb_email.Text) ||
                string.IsNullOrWhiteSpace(tb_mensagem.Text))
            {
                MessageBox.Show("Por favor, preencha todos os campos.");
                return;
            }

            if (!IsValidEmail(tb_email.Text))
            {
                MessageBox.Show("Por favor, insira um endereço de email válido.");
                return;
            }

            string fromMail = "todoitapplab@gmail.com";
            string fromPassword = "vyte qpsw zktv xovx";

            string toMail = tb_email.Text;
            string subject = "Confirmação de Envio ao Suporte";
            string body = $"Querido Utilizador/a {tb_primeironome.Text} {tb_ultimonome.Text},\n\nPor parte do grupo 4 de laboratório de Planeamento e Desenvolvimento de Software, agradecemos o contacto com o nosso suporte.\nA sua mensagem foi recebida com sucesso e será tratada o mais rápido possível.\n\nAtenciosamente,\nBruno Costa\nCarolina Machado\nLuis Lemos\nPedro Pereira";


            MailMessage messageToSupport = new MailMessage(fromMail, "emaildosuporte@exemplo.com")
            {
                Subject = "Solicitação de Suporte",
                Body = $"Nome: {tb_primeironome.Text}\nApelido: {tb_ultimonome.Text}\nEmail: {tb_email.Text}\n\n{tb_ultimonome.Text}"
            };

            MailMessage messageToUser = new MailMessage(fromMail, toMail)
            {
                Subject = subject,
                Body = body
            };

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            try
            {
                smtpClient.Send(messageToSupport);
                smtpClient.Send(messageToUser);
                MessageBox.Show("Email enviado com sucesso! Confirmação enviada para o seu email.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao enviar o email: " + ex.Message);
            }
            tb_primeironome.Clear();
            tb_ultimonome.Clear();
            tb_email.Clear();
            tb_mensagem.Clear();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}


