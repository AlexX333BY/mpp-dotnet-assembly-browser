using System.Windows;
using AssemblyBrowser.ViewModel;

namespace AssemblyBrowser.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModelConnector();
        }
    }
}
