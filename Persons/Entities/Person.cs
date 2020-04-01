using System;

namespace Persons.Entities
{
    public class Person
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();

        protected string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                Age = CalculateAge();
            }
        }

        private DateTime _birthDay;
        public DateTime BirthDay
        {
            get => _birthDay;
            set
            {
                _birthDay = value <= DateTime.Now
                    ? value
                    : throw new ArgumentException("BirthDay cannot be greater than today's date.");
                Age = CalculateAge();
            }
        }

        // Вычисляемое поле (по требованию заказчика).
        public int? Age { get; protected set; }

        // Конструктор без параметров для работы Dapper.
        protected Person()
        {
        }

        // Пользовательский конструктор с 2-мя параметрами.
        public Person(string name, DateTime birthDay)
        {
            _name = name;
            _birthDay = birthDay;
            Age = CalculateAge();
        }

        protected int? CalculateAge()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return null;

            // Сначала найдём разницу между годами дат.
            int age = DateTime.Now.Year - BirthDay.Year;

            // Затем проверим, был ли у человека день рождения в этом году. Если ещё не было - отнимем от возраста 1.
            if (DateTime.Now.DayOfYear < BirthDay.DayOfYear)
                age -= 1;

            return age > 120 ? (int?)null : age;
        }
    }
}

