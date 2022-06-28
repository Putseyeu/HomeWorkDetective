using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkDetective
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            bool isWork = true;
            Console.WriteLine("Программа для поиска преступников.");

            while (isWork == true)
            {
                Console.WriteLine(" 1 - поиск преступника по заданным параметрам. 2 - выход из программы.");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        database.SearchPeople();
                        break;

                    case "2":
                        isWork = false;
                        break;
                }
            }
        }
    }

    class Database
    {
        private List<Criminal> _criminals = new List<Criminal>();

        public Database()
        {
            CreateList();
        }

        public void SearchPeople()
        {
            int growth;
            int weight;
            string nationality;
            Console.WriteLine("Поиск человека");
            Console.Write("Укажите рост: ");
            growth = GetPositiveNumber();

            if (growth != 0)
            {
                Console.Write("Укажите вес: ");
                weight = GetPositiveNumber();

                if (weight != 0)
                {
                    Console.Write("Укажите национальность: ");
                    nationality = GetInputText();

                    if (nationality != null)
                    {
                        var filteredCriminal = _criminals.Where(criminal => criminal.InCustody == false && criminal.Growth == growth
                         && criminal.Weight == weight
                         && criminal.Nationality == nationality);

                        if (filteredCriminal.Count() <= 0)
                        {
                            Console.WriteLine("Нету совпадений по параметрам");
                        }
                        else
                        {
                            foreach (var criminal in filteredCriminal)
                            {
                                criminal.ShowInfo();
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Введены не коректные данные");
            }
        }

        private int GetPositiveNumber()
        {
            int maxLength = 3;
            bool isNumber = int.TryParse(Console.ReadLine(), out int inputNumber);

            if (isNumber == true && inputNumber >= maxLength)
            {
                return inputNumber;
            }
            else
            {
                inputNumber = 0;
                return inputNumber;
            }
        }

        private string GetInputText()
        {
            string text = Console.ReadLine();

            foreach (char symbol in text)
            {
                if (char.IsLetter(symbol) == false)
                {
                    return null;
                }
            }

            return text;
        }

        private void CreateList()
        {
            _criminals.Add(new Criminal("Евгений", "Рыкавец", "Анатольевичь", 185, 90, "китаец", false));
            _criminals.Add(new Criminal("Кирилл", "Фидотов", "Андреевичь", 160, 100, "мангол", true));
            _criminals.Add(new Criminal("Игорь", "Зйцев", "Петровичь", 200, 120, "мараканец", false));
            _criminals.Add(new Criminal("Евгений", "Мухавец", "Дмитриевичь", 170, 90, "китаец", true));
        }
    }

    class Criminal
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Patronymic { get; private set; }
        public bool InCustody { get; private set; }
        public int Growth { get; private set; }
        public int Weight { get; private set; }
        public string Nationality { get; private set; }

        public Criminal(string name, string surname, string patronymic, int growth, int weight, string nationality, bool inCustody)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Growth = growth;
            Weight = weight;
            Nationality = nationality;
            InCustody = inCustody;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"ФИО : {Name} {Surname} {Patronymic} | Рост {Growth} | Вес {Weight} | Национальность {Nationality}|");
        }
    }
}
