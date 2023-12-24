using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсач
{
    class Client
    {
        private static int counter = 0;
        public int ID { get; }
        public string? AnimalName { get; set; }
        public string? OwnerName { get; set; }
        public string? Breed { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public string? PhoneNumber { get; set; }
        public int Visits { get; set; }
        public Client(string animalName, string ownerName, string breed, int age, int weight, string phoneNumber)
        {
            ID = counter++;
            AnimalName = animalName;
            OwnerName = ownerName;
            Breed = breed;
            Age = age;
            Weight = weight;
            PhoneNumber = phoneNumber;
        }
        public Client() { }

        public void ChangeClient()
        {
            Console.WriteLine("Для изменения выберите нужный номер: Кличка => 1, Возраст => 2, Вес => 3, Телефонный номер => 4");
            string? number = Console.ReadLine();
            switch (number)
            {
                case "1":
                    Console.WriteLine("Введите новую кличку животного");
                    string? petName = Console.ReadLine();
                    this.AnimalName = IValidator.ValidateName(petName);
                    break;
                case "2":
                    Console.WriteLine("Введите возраст.");
                    int age = IValidator.ValidateAge(Int32.Parse(Console.ReadLine()));
                    Age = age;
                    break;
                case "3":
                    Console.WriteLine("Введите вес");
                    int newWeight = IValidator.ValidateWeight(Int32.Parse(Console.ReadLine()));
                    Weight = newWeight;
                    break;
                case "4":
                    Console.WriteLine("Введите новый номер");
                    string? newPhoneNumber = IValidator.ValidatePhoneNumber(Console.ReadLine());
                    PhoneNumber = newPhoneNumber;
                    break;
            }

        }
    }
}
