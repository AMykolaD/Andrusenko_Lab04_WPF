using Andrusenko_Lab2_WPF;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Andrusenko_Lab04_WPF.Tools
{
    public class PersonListSaver
    {
        public static bool Save(ObservableCollection<Person> list)
        {
            using (Stream stream = File.Open("save.bin", FileMode.Create))
            {
                (new BinaryFormatter()).Serialize(stream, list);
            }
            return true;
        }
        public static ObservableCollection<Person> Get()
        {
            ObservableCollection<Person> list = new ObservableCollection<Person>();
            if (File.Exists("save.bin") &&File.ReadAllText("save.bin") !="")
            {
                using (Stream stream = File.Open("save.bin", FileMode.Open))
                {
                    list = (ObservableCollection<Person>)new BinaryFormatter().Deserialize(stream);
                }
            }
            for(int i = 0; i < list.Count; i++)
            {
                list[i] = new Person(list[i].Name, list[i].Surname, list[i].Email, DateOnly.Parse(list[i].Birthdate));
            }
            return list;
        }
    }
}
