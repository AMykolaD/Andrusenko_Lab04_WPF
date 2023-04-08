using Andrusenko_Lab04_WPF.ViewModels;
using System.Windows;
using Andrusenko_Lab04_WPF.Tools;

namespace Andrusenko_Lab04_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if(UserDataGrid==null) Close();
            else
            DataContext = new PersonTableViewModel(UserDataGrid);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PersonListSaver.Save(((PersonTableViewModel)DataContext).List);
        }
        /*
        private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            try
            {
                Dispatcher.BeginInvoke(delegate ()
                {
                ObservableCollection<Person> persons = new ObservableCollection<Person>();
                for (int i = 0; i < UserDataGrid.Items.Count; i++)
                {
                    persons.Add((Person)UserDataGrid.Items.GetItemAt(i));
                }
                ((PersonTableViewModel)DataContext).List.Clear();
                for (int i = 0; i < persons.Count; i++) ((PersonTableViewModel)DataContext).List.Add(persons[i]);

                }, null);
                UserDataGrid.Items.Filter = new Predicate<object>(item => ((Person)item).Name.Contains("e"));
            }
            catch (Exception)
            {
            }
        }*/
    }
}
