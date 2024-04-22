using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Aplicação_ToDo.IT.Página_Inicial
{

    public partial class PáginaNovaTarefa : Window
    {
        public PáginaNovaTarefa()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtAdd_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(add.Text) && add.Text.Length > 0)
                textAdd.Visibility = Visibility.Collapsed;
            else
                textAdd.Visibility = Visibility.Visible;
        }

        private void textAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            add.Focus();
        }

        private void PriorityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RepeatComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tb_mensagem_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void GuardarNovaTarefa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LembreteTarefa_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

