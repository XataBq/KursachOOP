using ConsoleApp1;
using System;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Курсач;
using static System.Net.Mime.MediaTypeNames;

namespace Kursach
{

    class Program
    {
        static bool logIn = false;
        static void LogIn(DataBase dataBase)
        {
            Console.WriteLine("Введите свой ID и пароль от аккаунта:");
            string? id = Console.ReadLine();
            string? password = Console.ReadLine();
            Administrator administrator;
            administrator = (Administrator)dataBase.FindStaff(Int32.Parse(id!), "Администратор");
            if (administrator.CheckPassword(password!) == true)
            {
                logIn = true;
                Console.WriteLine("Вы успешно авторизовались");
            }
        }
        static void LogOut() { logIn = false; }
        static void Main()
        {
            DataBase db = new();
            DataAccess.DataBaseRepository.ReadDB(ref db);
            while (true)
            {
                try
                {
                    if (logIn == false)
                    {
                        Console.WriteLine("Выберите действие: 1 - авторизация, 2 - посмотреть записи");
                        Console.WriteLine("Для создания аккаунта администратора напишите 3");
                        string? number = Console.ReadLine();
                        try
                        {
                            switch (number)
                            {
                                case "1":
                                    LogIn(db);
                                    break;
                                case "2":
                                    Console.WriteLine("Информация о существующих записях: ");
                                    Console.WriteLine(db.ViewRecords());
                                    break;
                                case "3":
                                    Console.WriteLine("Введите пароль из README");
                                    if (Console.ReadLine() == "123")
                                    {
                                        Console.WriteLine("Введите имя, должность(Администратор или Грумер), возраст и зарплату");
                                        db.AddStaff(Console.ReadLine()!, Console.ReadLine()!, Int32.Parse(Console.ReadLine()!), Int32.Parse(Console.ReadLine()!));
                                    }
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Выберите действие:\n\nDEL - удаление, ADD - добавление, CHANGE - изменение(доступно для записей и клиентов), VIEW - просмотр данных(client, records)\nВыберите обьект: record - запись," +
                            " staff - персонал, client - клиент\nНапишите действие и обьект через пробел! EXIT - выход и сохранение, LOGOUT - выходит из админки\n");
                        string? activity = Console.ReadLine();
                        switch (activity)
                        {
                            case "DEL record":
                                Console.WriteLine("Введите ID записи или слово back для возвращения назад");
                                string? word1 = Console.ReadLine();
                                if (word1 == "back") { continue; }
                                else { db.DeleteRecord(Int32.Parse(word1!)); }
                                Console.WriteLine("Запись удалена");
                                break;
                            case "DEL client":
                                Console.WriteLine("Введите ID записи или слово back для возвращения назад");
                                string? word2 = Console.ReadLine();
                                if (word2 == "back") { continue; }
                                else { db.DeleteClient(Int32.Parse(word2!)); }
                                Console.WriteLine("Клиент удален");
                                break;
                            case "DEL staff":
                                Console.WriteLine("Введите ID персонала или слово back для возвращения назад");
                                string? word3 = Console.ReadLine();
                                if (word3 == "back") { continue; }
                                else { db.DeleteStaff(Int32.Parse(word3!)); }
                                Console.WriteLine("Персонал удален!");
                                break;
                            case "ADD record":
                                Console.WriteLine("Слово back для возвращения назад либо начните вводить =>");
                                Console.WriteLine("ID клиента, грумера, время в формате <<16.12.23 14:22>> M.dd.yy HH:mm и стоимость услуги");
                                string? word4 = Console.ReadLine();
                                if (word4 == "back") { continue; }
                                else
                                {
                                    string? IDC = word4;
                                    string? IDG = Console.ReadLine();
                                    DateTime time = Convert.ToDateTime(Console.ReadLine());
                                    string? cost = Console.ReadLine();
                                    db.AddRecord(db.FindClient(Int32.Parse(IDC!)), (Groomer)db.FindStaff(Int32.Parse(IDG!), "Грумер"), time, Int32.Parse(cost!));
                                    Console.WriteLine("Запись успешно создана");
                                }
                                break;
                            case "ADD client":
                                Console.WriteLine("Слово back для возвращения назад либо начните вводить =>");
                                Console.WriteLine("кличку животного, имя клиента, породу, возраст и вес животного, телефонный номер");
                                string? word5 = Console.ReadLine();
                                if (word5 == "back") { continue; }
                                else
                                {
                                    string? animalName = IValidator.ValidateName(word5);
                                    string? ownerName = IValidator.ValidateName(Console.ReadLine());
                                    string? breed = IValidator.ValidateName(Console.ReadLine());
                                    int age = IValidator.ValidateAge(Int32.Parse(Console.ReadLine()));
                                    int weight = IValidator.ValidateWeight(Int32.Parse(Console.ReadLine()));
                                    string? phoneNumber = IValidator.ValidatePhoneNumber(Console.ReadLine());
                                    db.AddClient(animalName, ownerName, breed, age, weight, phoneNumber);
                                    Console.WriteLine("Клиент успешно добавлен!");
                                }
                                break;
                            case "ADD staff":
                                Console.WriteLine("Слово back для возвращения назад либо начните вводить =>");
                                Console.WriteLine("Введите имя, должность(Администратор или Грумер), возраст и зарплату");
                                string? word6 = Console.ReadLine();
                                if (word6 == "back") { continue; }
                                else
                                {
                                    db.AddStaff(IValidator.ValidateName(word6!), Console.ReadLine()!, IValidator.ValidateAge(Int32.Parse(Console.ReadLine()!)),IValidator.ValidateSalary(Int32.Parse(Console.ReadLine()!)));
                                    Console.WriteLine("Персонал успешно зарегистрирован");
                                }
                                break;
                            case "CHANGE record":
                                Console.WriteLine("Слово back для возвращения назад либо начните вводить =>");
                                Console.WriteLine("ID записи для изменения");
                                string? word7 = Console.ReadLine();
                                if (word7 == "back") { continue; }
                                else
                                {
                                    try
                                    {
                                        Record changingRec = db.FindRecord(Int32.Parse(word7!));
                                        changingRec.ChangeRecord(ref db);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                                break;
                            case "CHANGE client":
                                Console.WriteLine("Слово back для возвращения назад либо начните вводить =>");
                                Console.WriteLine("ID клиента для изменения");
                                string? backOrContinue = Console.ReadLine();
                                if (backOrContinue == "back") { continue; }
                                else
                                {
                                    try
                                    {
                                        Client changingClient = db.FindClient(Int32.Parse(backOrContinue));
                                        changingClient.ChangeClient();
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                                break;
                            case "VIEW records":
                                Console.WriteLine("Информация о существующих записях: ");
                                Console.WriteLine(db.ViewRecords());
                                break;
                            case "VIEW clients":
                                Console.WriteLine("Информация о клиентах:");
                                Console.WriteLine(db.ViewClients());
                                break;
                            case "EXIT":
                                DataAccess.DataBaseRepository.SaveDB(ref db);
                                Environment.Exit(0);
                                break;
                            case "LOGOUT":
                                LogOut();
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

    }
}

namespace DataAccess
{
    static class DataBaseRepository
    {
        public static string filename = "DataBase.json";
        public static void SaveDB(ref DataBase dataBase)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            string jsonstring = JsonSerializer.Serialize(dataBase, options);
            File.WriteAllText(filename, jsonstring);
        }

        public static void ReadDB(ref DataBase dataBase)
        {
            string jsonstring = File.ReadAllText(filename);
            Console.WriteLine(jsonstring);
            if (string.IsNullOrEmpty(jsonstring))
            {
                return;
            }
            dataBase = JsonSerializer.Deserialize<DataBase>(jsonstring)!;
        }
    }
}