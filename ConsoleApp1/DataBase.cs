using Kursach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсач
{
    class DataBase
    {
        private List<Client> clients = new();
        public List<Client> Clients
        {
            get { return clients; }
            set { clients = value; }
        }

        private List<Administrator> administrators = new();
        public List<Administrator> Administrators
        {
            get { return administrators; }
            set { administrators = value; }
        }

        private List<Record> records = new();
        public List<Record> Records
        {
            get { return records; }
            set { records = value; }
        }

        private List<Groomer> groomers= new();
        public List<Groomer> Groomers
        {
            get { return groomers; }
            set { groomers = value; }
        }

        public void AddClient(string animalName, string ownerName, string breed, int age, int weight, string phoneNumber)
        {
            Client client = new Client(animalName, ownerName, breed, age, weight, phoneNumber);
            Clients.Add(client);
        }

        public void AddRecord(Client client, Groomer groomer, DateTime time, int cost)
        {
            Record record = new Record(client, groomer, time, cost);
            client.Visits++;
            Records.Add(record);
        }

        public void AddStaff(string name, string workplace, int age, int salary)
        {
            switch (workplace)
            {
                case "Администратор":
                    Console.WriteLine("Придумайте пароль для админа:");
                    string? password = Console.ReadLine();
                    Administrator administrator = new Administrator(password!, name, workplace, age, salary);
                    Administrators.Add(administrator);
                    break;
                case "Грумер":
                    Groomer groomer = new Groomer(name, workplace, age, salary);
                    Groomers.Add(groomer);
                    break;
                default:
                    Console.WriteLine("Не верная должность, повторите попытку введя сущетсвующую должность: Администратор или Грумер");
                    break;
            }
        }

        public void DeleteStaff(int id)
        {
            for (int i = 0; i < Administrators.Count; i++)
            {
                if (Administrators[i].ID == id)
                {
                    Administrators.Remove(Administrators[i]);
                }
            }
        }

        public void DeleteRecord(int id)
        {
            for (int i = 0; i < Records.Count; i++)
            {
                if (Records[i].Id == id)
                {
                    Records.Remove(Records[i]);
                    Console.WriteLine("Успешно удалено!");
                }
            }
        }

        public void DeleteClient(int id)
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                if (Clients[i].ID == id)
                {
                    Clients.Remove(Clients[i]);
                    Console.WriteLine("Успешно удалено!");
                }
            }
        }

        public Client FindClient(int id)
        {
            try
            {
                return Clients.Find(x => x.ID == id)!;
            }
            catch { Console.WriteLine("Клиент с таким ID не найден, попробуйте снова"); return null!; }
        }

        public Staff FindStaff(int id, string workplace)
        {
            try
            {
                if (workplace == "Администратор")
                {
                    return Administrators.Find(x => x.ID == id)!;
                }
                else
                {
                    return Groomers.Find(x => x.ID == id)!;
                }
            }
            catch { Console.WriteLine("Клиент с таким ID не найден, попробуйте снова"); return null!; }
        }

        public Record FindRecord(int id)
        {
            try
            {
                return Records.Find(x => x.Id == id)!;
            }
            catch { Console.WriteLine("Клиент с таким ID не найден, попробуйте снова"); return null!; }
        }

        public string ViewRecords()
        {
            string recs = "\n";
            foreach (var record in Records)
            {
                recs += $"Id: {record.Id}, Кличка животного: {record.Client.AnimalName}, Имя хозяина: {record.Client.OwnerName}, Время: {record.Time}, Мастер: {record.Groomer}, Стоимость: {record.Cost}\n\t";
            }
            return recs;
        }

        public string ViewClients()
        {
            string clientsInfo = "\n";
            foreach (var client in Clients)
            {
                clientsInfo += $"ID: {client.ID},Кличка животного: {client.AnimalName}, Имя хозяина: {client.OwnerName}, Визиты: {client.Visits}, телефон: {client.PhoneNumber}";
            }
            return clientsInfo;
        }
    }
}
