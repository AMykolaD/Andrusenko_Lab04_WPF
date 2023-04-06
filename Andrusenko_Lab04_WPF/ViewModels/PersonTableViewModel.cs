using Andrusenko_Lab04_WPF.Tools;
using Andrusenko_Lab2_WPF;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Andrusenko_Lab04_WPF.ViewModels
{
    internal class PersonTableViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        ObservableCollection<Person> list;

        private RelayCommand? commandSave;
        private RelayCommand? commandDelete;
        private int selectedRow;

        private bool isEnabled = true;

        public bool IsEnabled { get=> isEnabled; set
            {
                isEnabled = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand CommandSave
        {
            get
            {
                return commandSave ??= new RelayCommand(_ => ExecuteSave(), CanExecuteSave);
            }
        }
        public RelayCommand CommandDelete
        {
            get
            {
                return commandDelete ??= new RelayCommand(_ => ExecuteDelete(), CanExecuteDelete);
            }
        }

        public ObservableCollection<Person> List
        {
            get => list;
            set => list = value;
        }
        public int SelectedRow
        {
            get => selectedRow; set
            {
                selectedRow = value;
                OnPropertyChanged();
            }
        }

        public PersonTableViewModel()
        {
            SelectedRow = -1;
            IsEnabled = true;
            list = PersonListSaver.Get();
            if (list.Count == 0)
            {
                for (int i = 0; i < 49; i++) list.Add(RandomPersonGenerator.GenerateRandomPerson());
            }
            PersonListSaver.Save(list);
        }
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private bool CanExecuteSave(object obj)
        {
            return IsEnabled;
        }
        private void ExecuteSave()
        {
            PersonListSaver.Save(list);
        }
        private bool CanExecuteDelete(object obj)
        {
            return IsEnabled&&SelectedRow!=-1;
        }
        private void ExecuteDelete()
        {
            list.RemoveAt(SelectedRow);
        }
    }
}
