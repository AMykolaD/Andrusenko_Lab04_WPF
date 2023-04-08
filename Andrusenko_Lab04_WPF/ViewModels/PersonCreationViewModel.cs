using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Andrusenko_Lab04_WPF.Models;

namespace Andrusenko_Lab04_WPF.ViewModels
{
    internal class PersonCreationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private RelayCommand? commandSave;
        private RelayCommand? commandCancel;

        private string name = "";
        private string surname = "";
        private string email = "";
        private DateTime birthdate = DateTime.Now;
        Window _window;

        ObservableCollection<Person> _list;
        Person? _person;

        public RelayCommand CommandCancel
        {
            get
            {
                return commandCancel ??= new RelayCommand(_ => ExecuteCancel(), CanExecuteCancel);
            }
        }
        public RelayCommand CommandSave
        {
            get
            {
                return commandSave ??= new RelayCommand(_ => ExecuteSave(), CanExecuteSave);
            }
        }

        public string Name { get => name; 
            set 
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public string Surname { get => surname; set
            {
                surname = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }
        public DateTime Birthdate
        {
            get => birthdate;
            set
            {
                birthdate = value;
                OnPropertyChanged();
            }
        }

        public PersonCreationViewModel(ObservableCollection<Person> list, Person? person, Window window) {
            if (person!=null)
            {
                Name = list[list.IndexOf(person)].Name;
                Surname = list[list.IndexOf(person)].Surname;
                Email = list[list.IndexOf(person)].Email;
                Birthdate = new DateTime(list[list.IndexOf(person)].Birthdate.Year,
                    list[list.IndexOf(person)].Birthdate.Month,
                    list[list.IndexOf(person)].Birthdate.Day);
            }
            _list = list;
            _person = person;
            _window = window;
        }
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private bool CanExecuteSave(object obj)
        {
            return Name!=""&&Surname!=""&&Email!="";
        }
        private void ExecuteSave()
        {
            try
            {
                Person person = new Person(Name, Surname, Email, DateOnly.FromDateTime(Birthdate));
                if (_person==null) _list.Add(person);
                else _list[_list.IndexOf(_person)] = person;
                _window.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Something's wrong");
            }
        }
        private bool CanExecuteCancel(object obj)
        {
            return true;
        }
        private void ExecuteCancel()
        {
            _window.Close();
        }
    }
}
