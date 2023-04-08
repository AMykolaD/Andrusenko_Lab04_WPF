using Andrusenko_Lab04_WPF.Exceptions;
using System;
using System.Globalization;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Andrusenko_Lab04_WPF.Models
{
    [Serializable]
    public class Person
    {
        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }
        public string birthdateString;
        [NonSerialized]
        private readonly DateOnly birthdateVar;
        public DateOnly Birthdate => birthdateVar; 
        public bool IsAdult { get; }
        public string SunSign { get; }
        public string ChineseSign { get; }
        public bool IsBirthday { get; }
        public Person(string name, string surname, string email, DateOnly birthdate)
        {
            Name = name;
            Surname = surname;
            Email = email;
            birthdateVar = birthdate;
            birthdateString = birthdate.ToString();
            IsAdult = Age(birthdate) >= 18;
            SunSign = SunSignFromDate(birthdate);
            ChineseSign = ChineseSignFromDate(birthdate);
            IsBirthday = IsTodayBirthday(birthdate);
            try
            {
                new MailAddress(email);
            }
            catch
            {
                throw new InvalidEmailException("Email is invalid");
            }
            if (email.Contains(' ')) throw new InvalidEmailException("Email is invalid");
            if (Age(birthdate) >= 135) throw new TooOldException("You are older than the oldest human is");
            if (Age(birthdate) < 0) throw new BornInFutureException("You are born in the future");
        }
        public Person(string name, string surname, string email) : this(name, surname, email, DateOnly.FromDateTime(DateTime.Now))
        {

        }
        public Person(string name, string surname, DateOnly birthdate) : this(name, surname, "", birthdate)
        {

        }

        private static string SunSignFromDate(DateOnly dateTime)
        {
            string sunSign = "";
            switch (dateTime.Month)
            {
                case 1:
                    sunSign = dateTime.Day < 20 ? "The Goat" : "The Water-bearer";
                    break;
                case 2:
                    sunSign = dateTime.Day < 19 ? "The Water-bearer" : "The Fish";
                    break;
                case 3:
                    sunSign = dateTime.Day < 21 ? "The Fish" : "The Ram";
                    break;
                case 4:
                    sunSign = dateTime.Day < 20 ? "The Ram" : "The Bull";
                    break;
                case 5:
                    sunSign = dateTime.Day < 21 ? "The Bull" : "The Twins";
                    break;
                case 6:
                    sunSign = dateTime.Day < 22 ? "The Twins" : "The Crab";
                    break;
                case 7:
                    sunSign = dateTime.Day < 23 ? "The Crab" : "The Lion";
                    break;
                case 8:
                    sunSign = dateTime.Day < 23 ? "The Lion" : "The Maiden";
                    break;
                case 9:
                    sunSign = dateTime.Day < 23 ? "The Maiden" : "The Scales";
                    break;
                case 10:
                    sunSign = dateTime.Day < 23 ? "The Scales" : "The Scorpion";
                    break;
                case 11:
                    sunSign = dateTime.Day < 23 ? "The Scorpion" : "The Archer (Centaur)";
                    break;
                case 12:
                    sunSign = dateTime.Day < 22 ? "The Archer (Centaur)" : "The Goat";
                    break;
                default: break;
            }
            return sunSign;
        }
        private static string ChineseSignFromDate(DateOnly dateTime)
        {
            if (dateTime.Year < 1901 || dateTime.Year > 2100) return "";
            string chineseSign = "";
            DateTime chineseNewYearDate = new ChineseLunisolarCalendar().ToDateTime(dateTime.Year, 1, 1, 0, 0, 0, 0);
            int chineseYear = dateTime.Year - (dateTime.Month < chineseNewYearDate.Month ||
            dateTime.Month == chineseNewYearDate.Month &&
            dateTime.Day < chineseNewYearDate.Day ? 1 : 0);
            switch (chineseYear % 12)
            {
                case 0:
                    chineseSign = "Monkey";
                    break;
                case 1:
                    chineseSign = "Rooster";
                    break;
                case 2:
                    chineseSign = "Dog";
                    break;
                case 3:
                    chineseSign = "Pig";
                    break;
                case 4:
                    chineseSign = "Rat";
                    break;
                case 5:
                    chineseSign = "Ox";
                    break;
                case 6:
                    chineseSign = "Tiger";
                    break;
                case 7:
                    chineseSign = "Rabbit";
                    break;
                case 8:
                    chineseSign = "Dragon";
                    break;
                case 9:
                    chineseSign = "Snake";
                    break;
                case 10:
                    chineseSign = "Horse";
                    break;
                case 11:
                    chineseSign = "Goat";
                    break;
                default: break;
            }
            return chineseSign;
        }
        private static bool IsTodayBirthday(DateOnly dateTime)
        {
            return (dateTime.Day == DateTime.Now.Day) && (dateTime.Month == DateTime.Now.Month);
        }
        public static int Age(DateOnly dateTime)
        {
            int yearDifference;
            yearDifference = DateTime.Now.Year - dateTime.Year;
            if (DateTime.Now.Month < dateTime.Month ||
                DateTime.Now.Month == dateTime.Month &&
                DateTime.Now.Day < dateTime.Day) yearDifference--;
            return yearDifference;
        }
    }
}