using System;

namespace Persons.Entities
{
    /// <summary>
    /// Сущность "Человек".
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        public Guid Id { get; protected set; } = Guid.NewGuid();

        protected string _name;
        /// <summary>
        /// Имя.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                if (string.IsNullOrWhiteSpace(_name))
                    Age = null;
            }
        }

        private DateTime _birthDay;
        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime BirthDay
        {
            get => _birthDay;
            set
            {
                _birthDay = value;
                Age = CalculateAge();
            }
        }

        private readonly int _ageLimit = 120;
        /// <summary>
        /// Возраст. Вычисляемое поле.
        /// </summary>
        public int? Age { get; protected set; }

        #region Constructors
        protected Person()
        {
        }

        public Person(string name, DateTime birthDay)
        {
            _name = name;
            _birthDay = birthDay;
            Age = CalculateAge();
        }
        #endregion

        /// <summary>
        /// Вычисляет возраст человека.
        /// </summary>
        /// <returns>
        /// Челочисленное значение возраста человека или null, если не задано имя или расчитанный возраст больше 120 лет.
        /// </returns>
        protected int? CalculateAge()
        {
            if (string.IsNullOrWhiteSpace(Name) || (!string.IsNullOrWhiteSpace(Name) && BirthDay > DateTime.Now))
                return null;

            // Сначала найдём разницу между годами дат.
            int age = DateTime.Now.Year - BirthDay.Year;

            // Затем проверим, был ли у человека день рождения в этом году. Если ещё не было - отнимем от возраста 1.
            if (DateTime.Now.DayOfYear < BirthDay.DayOfYear)
                age -= 1;

            return age > _ageLimit ? (int?)null : age;
        }

        /// <summary>
        /// Валидирует состояние сущности.
        /// </summary>
        /// <returns>Результат валидации сущности в виде объекта класса <see cref="EntityValidationResult"/>.</returns>
        public EntityValidationResult Validate()
        {
            var result = new EntityValidationResult();
            if (string.IsNullOrWhiteSpace(Name))
                result.Errors.Add($"{nameof(Name)} равно пустой строке или null.");
            if (BirthDay > DateTime.Now)
                result.Errors.Add($"{nameof(BirthDay)} позднее текущей даты.");
            if (Age == null && string.IsNullOrWhiteSpace(Name))
                result.Errors.Add($"{nameof(Age)} равно null, т.к. не задано {nameof(Name)}.");
            else if (Age == null)
                result.Errors.Add($"{nameof(Age)} равно null, т.к. возраст {nameof(Person)} больше {_ageLimit} лет.");
            if (result.Errors.Count > 0)
                result.IsValid = false;
            return result;
        }
    }
}

