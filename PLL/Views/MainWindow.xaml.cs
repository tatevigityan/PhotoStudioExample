using PhotoStudio.ViewModels;
using System.Windows;

namespace PhotoStudio
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();
        }
    }
}