using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memory_spel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonStartKind_Click(object sender, RoutedEventArgs e)
        {
            SpeelschermKind game = new SpeelschermKind();
            game.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        //private void ButtonStartVolw_Click(object sender, RoutedEventArgs e)
        //{
        //    OptiesSpeelschermVolw game = new OptiesSpeelschermVolw();
        //    game.Visibility = Visibility.Visible;
        //    this.Visibility = Visibility.Hidden;
        //}
    }
}