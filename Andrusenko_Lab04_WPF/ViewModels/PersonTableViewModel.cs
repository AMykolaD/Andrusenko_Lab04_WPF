using Andrusenko_Lab04_WPF.Tools;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Andrusenko_Lab04_WPF.Models;
using Andrusenko_Lab04_WPF.Views;

namespace Andrusenko_Lab04_WPF.ViewModels
{
    internal class PersonTableViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        ObservableCollection<Person> list;

        private RelayCommand? commandSave;
        private RelayCommand? commandDelete;
        private RelayCommand? commandEdit;
        private RelayCommand? commandAdd;
        private Person? selectedPerson;

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
        public RelayCommand CommandEdit
        {
            get
            {
                return commandEdit ??= new RelayCommand(_ => ExecuteEdit(), CanExecuteDelete);
            }
        }
        public RelayCommand CommandAdd
        {
            get
            {
                return commandAdd ??= new RelayCommand(_ => ExecuteAdd(), CanExecuteSave);
            }
        }

        public ObservableCollection<Person> List
        {
            get => list;
            set => list = value;
        }

        public Person? SelectedPerson { get => selectedPerson; set
            {
                selectedPerson = value;
                OnPropertyChanged();
            }
            }

        public PersonTableViewModel()
        {
            IsEnabled = true;
            bool isFileEmpty;
            (list, isFileEmpty) = PersonListSaver.Get();
            if (!isFileEmpty)
            {
                for (int i = 0; i < 50; i++) list.Add(RandomPersonGenerator.GenerateRandomPerson());
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
            return IsEnabled&&SelectedPerson!=null;
        }
        private void ExecuteDelete()
        {
            list.RemoveAt(list.IndexOf(SelectedPerson));
        }

        private void ExecuteAdd()
        {
            var window = new PersonCreationWindow(list);
            window.Show();
        }
        private void ExecuteEdit()
        {
            var window = new PersonCreationWindow(list, SelectedPerson);
            window.Show();
        }
    }
}
