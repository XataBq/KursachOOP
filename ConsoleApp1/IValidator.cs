using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{
    internal interface IValidator
    {
        public static string ValidateName(string name)
        {
            bool validationSuccess = false;
            while (validationSuccess == false)
            {
                if (name.All(char.IsLetter) && name.Length>=1) { validationSuccess=true; }
                else
                {
                    Console.WriteLine("Попробуйте ввести еще раз, разрешено использовать только буквы!");
                    name = Console.ReadLine();
                }
            }
            return name;
        }

        public static int ValidateAge (int age)
        {
            bool validationSuccess = false;
            while (validationSuccess == false)
            {
                if (0 <= age && age <= 130) { validationSuccess = true; }
                else
                {
                    Console.WriteLine("Попробуйте ввести еще раз, возраст более 120 лет или отрицательный не доступен");
                    age = Int32.Parse(Console.ReadLine());
                }
            }
            return age;
        }
        public static int ValidateSalary(int salary)
        {
            if (salary <= 0)
            {
                Console.WriteLine("Вы ввели зарплату ниже 0, возможно вы ошиблись, попробуйте ввести еще раз. Если нет, ввидети указанную зарплату повторно");
                salary = Int32.Parse(Console.ReadLine());
                return salary;
            }
            else return salary;
        }
        public static int ValidateWeight(int weight)
        {
            if ((weight < 0) || (weight >200))
            {
                Console.WriteLine("Попробуйте ввести более реальный вес животного в килограммах. Если хотите оставить такой, введите его еще раз");
                weight = Int32.Parse(Console.ReadLine());
                return weight;
            }
            else return weight;
        }
        public static string ValidatePhoneNumber(string phoneNumber)
        {
            Regex rg = new Regex(@"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})");  
            Match match = rg.Match(phoneNumber);
            if (match.Success) { return phoneNumber; }
            else
            {
                Console.WriteLine("Вы ввели номер телефона в неверном формате, попробуйте ввести еще раз 10 символов без кода страны или введите как считаете нужным");
                phoneNumber = Console.ReadLine();
                return phoneNumber;
            }
        }
    }
}
