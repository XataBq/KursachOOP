using Kursach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсач
{
    class Record
    {
        private static int counter = 0;
        public int Id { get; }
        public Client Client { get; set; } = default!;
        public Groomer Groomer { get; set; } = default!;
        public DateTime? Time {  get; set; }

        public int Cost { get; set; }
        public Record(Client client, Groomer groomer, DateTime time, int cost) { Id = counter++; Client = client; Groomer = groomer; Time = time; Cost = cost; }

        public void ChangeRecord(ref DataBase db)
        {
            Console.WriteLine("Для изменения выберите нужный номер: Клиент => 1, Мастер => 2, Время => 3, Цена => 4");
            string? number = Console.ReadLine();
            switch (number)
            {
                case "1":
                    Console.WriteLine("Введите ID нового клиента...");
                    string? clientId = Console.ReadLine();
                    Client newClient = db.FindClient(Int32.Parse(clientId!));
                    Client = newClient;
                    break;
                case "2":
                    Console.WriteLine("Введите ID нового грумера...");
                    string? groomerId = Console.ReadLine();
                    Client newGroomer = db.FindClient(Int32.Parse(groomerId!));
                    Client = newGroomer;
                    break;
                case "3":
                    Console.WriteLine("Введите новое время");
                    DateTime? newDate = Convert.ToDateTime(Console.ReadLine());
                    Time = newDate;
                    break;
                case "4":
                    Console.WriteLine("Введите новую стоимость");
                    string? newCost = Console.ReadLine();
                    Cost = Int32.Parse(newCost!);
                    break;
            }
        }
    }
}
