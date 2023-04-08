using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Andrusenko_Lab04_WPF.Models;

namespace Andrusenko_Lab04_WPF.Tools
{
    public class PersonListSaver
    {
        public static bool Save(ObservableCollection<Person> list)
        {
            if(list == null) list = new ObservableCollection<Person>();
            using (Stream stream = File.Open("save.bin", FileMode.Create))
            {
                (new BinaryFormatter()).Serialize(stream, list);
            }
            return true;
        }
        //Returns list and if the file did not exist of was empty
        public static (ObservableCollection<Person>, bool) Get()
        {
            ObservableCollection<Person> list = new ObservableCollection<Person>();
            if (File.Exists("save.bin") && File.ReadAllText("save.bin") != "")
            {
                using (Stream stream = File.Open("save.bin", FileMode.Open))
                {
                    try
                    {
                        list = (ObservableCollection<Person>)new BinaryFormatter().Deserialize(stream);
                    }
                    catch (Exception)
                    {
                        list = new ObservableCollection<Person>();
                    }
                }
            }
            else return (list, false);
            for(int i = 0; i < list.Count; i++)
            {
                list[i] = new Person(list[i].Name, list[i].Surname, list[i].Email, DateOnly.Parse(list[i].birthdateString));
            }
            return (list, true);
        }
    }
}
