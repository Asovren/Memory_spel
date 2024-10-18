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
    /// Interaction logic for SpeelschermKind.xaml
    /// </summary>
    public partial class SpeelschermKind : Window
    {
        // Lijst maken met afbeeldingen
        private List<string> imagePaths = new List<string>
        {
            "pack://application:,,,/Afbeeldingen/Gruffalo/Gruffalo.bmp",
            "pack://application:,,,/Afbeeldingen/Gruffalo/Kikker.bmp",
            "pack://application:,,,/Afbeeldingen/Gruffalo/Muis.bmp",
            "pack://application:,,,/Afbeeldingen/Gruffalo/Slang.bmp",
            "pack://application:,,,/Afbeeldingen/Gruffalo/Uil.bmp",
            "pack://application:,,,/Afbeeldingen/Gruffalo/Vos.bmp"
        };

        private List<string> shuffledImages;
        private Button[] buttons;
        private Button firstClickedButton = null;
        private Button secondClickedButton = null;
        private bool isComparing = false;
        private int matchesFound = 0;

        public SpeelschermKind()
        {
            InitializeComponent();
            ShuffleImages();
            InitGameGrid();
        }

        private void InitGameGrid()
        {
            int rows = 3;
            int cols = 4;
            buttons = new Button[rows * cols];
            int imageIndex = 0;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Button btn = new Button();
                    btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A7891")); // Begin met kleur
                    btn.BorderThickness = new Thickness(1);
                    btn.Tag = shuffledImages[imageIndex]; // Koppel een afbeelding aan de knop via Tag
                    btn.Margin = new Thickness(5);

                    btn.Click += OnCardClick;
                    SpeelveldKind.Children.Add(btn);
                    Grid.SetRow(btn, row);
                    Grid.SetColumn(btn, col);

                    buttons[imageIndex] = btn;
                    imageIndex++;
                }
            }
        }

        // Methode voor het "omdraaien" van kaarten
        private void OnCardClick(object sender, RoutedEventArgs e)
        {
            if (isComparing) return; // Blokkeer klikken tijdens vergelijking

            Button clickedButton = sender as Button;

            if (clickedButton == null || clickedButton.Background is ImageBrush)
                return; // Blokkeer klikken op een reeds onthulde knop

            // Toon de afbeelding op de knop
            string imagePath = (string)clickedButton.Tag;
            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            clickedButton.Background = imgBrush;

            if (firstClickedButton == null)
            {
                // Eerste knop die wordt geklikt
                firstClickedButton = clickedButton;
            }
            else if (secondClickedButton == null)
            {
                // Tweede knop die wordt geklikt
                secondClickedButton = clickedButton;

                // Start vergelijking
                CompareCards();
            }
        }

        // Methode om kaarten te vergelijken
        private async void CompareCards()
        {
            isComparing = true;

            // Wacht even zodat de speler beide kaarten kan zien
            await Task.Delay(1000);

            // Controleer of de kaarten overeenkomen
            if ((string)firstClickedButton.Tag == (string)secondClickedButton.Tag)
            {
                // Match gevonden
                matchesFound++;
                firstClickedButton = null;
                secondClickedButton = null;

                // Controleer of alle matches zijn gevonden
                //if (matchesFound == shuffledImages.Count / 2)
                //{
                //    EindschermKind winWindow = new EindschermKind();
                //    winWindow.Show(); // Open het Eindscherm
                //    this.Close(); // Sluit het huidige venster, indien gewenst
                //}
            }
            else
            {
                // Geen match, draai de kaarten terug
                firstClickedButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A7891"));
                secondClickedButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A7891"));
                firstClickedButton = null;
                secondClickedButton = null;
            }

            isComparing = false;
        }

        // Methode om kaarten te shuffelen
        private void ShuffleImages()
        {
            // Verdubbel de afbeeldingen (elke afbeelding komt twee keer voor)
            shuffledImages = new List<string>(imagePaths);
            shuffledImages.AddRange(imagePaths); // Verdubbel de lijst

            // Willekeurig de lijst shuffelen
            Random rnd = new Random();
            shuffledImages = shuffledImages.OrderBy(x => rnd.Next()).ToList();

            // Controleer of er exact 12 afbeeldingen zijn
            if (shuffledImages.Count != 12)
            {
                throw new Exception("Er zijn niet genoeg afbeeldingen voor het speelveld.");
            }
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