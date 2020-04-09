using System.Windows;
using System.Windows.Controls;

namespace CarRentApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            Details.Visibility = (sender as ListView)?.SelectedItem != null ? Visibility.Visible : Visibility.Hidden;
    }
}
