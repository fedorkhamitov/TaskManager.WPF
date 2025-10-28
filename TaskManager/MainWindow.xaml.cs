using System.Windows;
using TaskManager.ViewModels;

namespace TaskManager
{
    /// <summary>
    /// Взаимодействие логики для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Устанавливаем DataContext на ViewModel
            // Это связывает UI с логикой
            this.DataContext = new MainViewModel();
        }
    }
}
