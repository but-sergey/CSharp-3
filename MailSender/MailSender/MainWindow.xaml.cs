using System.Windows;

namespace MailSender
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void miClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
