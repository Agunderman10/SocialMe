using System.Windows;

namespace SocialMe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel MainWindowViewModel = new MainWindowViewModel();
        public MainWindow()
        {
            this.DataContext = MainWindowViewModel;
            InitializeComponent();
        }
    }
}
