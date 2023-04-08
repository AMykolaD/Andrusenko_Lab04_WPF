using Andrusenko_Lab04_WPF.Models;
using Andrusenko_Lab04_WPF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace Andrusenko_Lab04_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для PersonCreationWindow.xaml
    /// </summary>
    public partial class PersonCreationWindow : Window
    {
        public PersonCreationWindow(ObservableCollection<Person> list, Person? person = null)
        {
            InitializeComponent();
            Application.Current.MainWindow.IsEnabled = false;
            DataContext = new PersonCreationViewModel(list, person, this);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.MainWindow.IsEnabled = true;
        }
    }
}
