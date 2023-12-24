using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Курсач
{
    abstract class Staff
    {
        private static int counter = 0;
        public int ID { get; }
        public abstract string Name { get; set; }
        public abstract string Workplace { get; set; }
        public abstract int Age { get; set; }
        public abstract int Salary { get; set; }
        [JsonConstructor]
        public Staff(string name, string workplace, int age, int salary)
        {
            ID = counter++;
            Name = name;
            Workplace = workplace;
            Age = age;
            Salary = salary;
        }
        public Staff() { }
    }

    class Administrator : Staff
    {
        public string Password { get;} = default!;
        public override string Name { get; set; } = default!;
        public override string Workplace { set; get; } = default!;
        public override int Age { get; set; }
        public override int Salary { get; set; }
        [JsonConstructor]
        public Administrator(string password, string name, string workplace, int age, int salary) : base(name, workplace, age, salary)
        { Password = password; }
        public bool CheckPassword(string password)
        {
            if (Password == password)
            {
                return true;
            }
            else return false;
        }
        public Administrator() { }
    }

    class Groomer : Staff
    {
        public override string Name { get; set; } = default!;
        public override string Workplace { set; get; } = default!;
        public override int Age { get; set; }
        public override int Salary { get; set; }
        [JsonConstructor]
        public Groomer(string name, string workplace, int age, int salary) : base(name, workplace, age, salary)
        { }
        public Groomer() { }
    }
}
