using Andrusenko_Lab2_WPF;
using System;
using System.Collections.Generic;

namespace Andrusenko_Lab04_WPF.Tools
{
    public class RandomPersonGenerator
    {
        private static List<String> _names = new List<string>()
        {
            "James", "Robert", "John",
            "Michael", "David", "William",
            "Richard", "Joseph", "Thomas",
            "Charles", "Christopher", "Daniel",
            "Matthew", "Anthony", "Mark"
        };
        private static List<String> _surnames = new List<string>()
        {
            "Smith", "Jones", "Williams",
            "Taylor", "Brown", "Davies",
            "Evans", "Thomas", "Wilson",
            "Johnson", "Roberts", "Robinson",
            "Thompson", "Wright", "Walker"
        };
        private static List<String> _domens = new List<string>()
        {
            "gmail.com", "yahoo.com", "hotmail.com",
            "aol.com", "hotmail.co.uk", "msn.com",

        };
        private static List<String> _emailParts = new List<string>()
        {
            "abc", "work", "-coop",
            "real", "sleepy", "uuu",
            "theOne", "_toast", "no",
            "wise", "eee", "0my0email"
        };
        public static Person GenerateRandomPerson()
        {
            Random random = new Random();
            string name = _names[random.Next(_names.Count)];
            string surname = _surnames[random.Next(_surnames.Count)];
            string email = name.ToLower() + _emailParts[random.Next(_emailParts.Count)] + "@" + _domens[random.Next(_domens.Count)];
            DateOnly birthdate = new DateOnly(DateTime.Now.Year - random.Next(100) - 1, random.Next(12)+1, random.Next(26) + 1);
            Person person = new Person(name, surname, email, birthdate);
            return person;
        }
    }
}
