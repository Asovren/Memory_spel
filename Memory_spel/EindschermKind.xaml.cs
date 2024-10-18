using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Memory_spel
{
    /// <summary>
    /// Interaction logic for EindschermKind.xaml
    /// </summary>
    public partial class EindschermKind : Window
    {
        public EindschermKind()
        {
            InitializeComponent();
        }

        private void TerugNaarSpel_Click(object sender, RoutedEventArgs e)
        {
            // Sluit dit venster om terug te gaan naar het spel
            this.Close();
            SpeelschermKind spel = new SpeelschermKind();
            spel.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        // Methode voor home knop
        private void TerugNaarBeginscherm_Click(object sender, RoutedEventArgs e)
        {
            MainWindow begin = new MainWindow();
            begin.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }
    }
}
