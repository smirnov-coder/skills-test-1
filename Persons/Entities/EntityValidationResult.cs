using System.Collections.Generic;

namespace Persons.Entities
{
    /// <summary>
    /// Результат валидации сущности.
    /// </summary>
    public class EntityValidationResult
    {
        /// <summary>
        /// Показывает результат валидации сущности.
        /// </summary>
        public bool IsValid { get; set; } = true;

        /// <summary>
        /// Коллекция ошибок валидации сущности.
        /// </summary>
        public IList<string> Errors { get; set; } = new List<string>();
    }
}

