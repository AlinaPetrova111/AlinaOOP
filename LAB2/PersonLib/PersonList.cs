using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonLib
{
    /// <summary>
    /// Класс Список персон
    /// </summary>
    public class PersonList
    {
        /// <summary>
        /// Список людей
        /// </summary>
        private PersonBase[] _personArray = new PersonBase[0];

        /// <summary>
        /// Добавление персоны в список
        /// </summary>
        /// <param name="person">Экземпляр класса персона</param>
        public void AddPerson(PersonBase person)
        {
            Array.Resize(ref _personArray, _personArray.Length + 1);
            _personArray[_personArray.Length - 1] = person;
        }

        /// <summary>
        /// Очистить список 
        /// </summary>
        public void DeleteAllPeople()
        {
            Array.Resize(ref _personArray, 0);
        }

        /// <summary>
        /// Удаление персоны из списка
        /// </summary>
        /// <param name="person">Экземпляр класса Персона</param>
        public void DeletePersonByIndex(PersonBase person)
        {
            DeleteByIndex(GetIndexOfPerson(person));
        }

        /// <summary>
        /// Удаление персоны по ее индексу
        /// </summary>
        /// <param name="index">Индекс экземпляра класса 
        /// Человек</param>
        public void DeleteByIndex(int index)
        {
            if (index < 0 || index > _personArray.Length - 1)
            {
                throw new Exception("You entered " +
                    "an invalid index!");
            }

            for (int i = index; i < _personArray.Length - 2; i++)
            {
                _personArray[i] = _personArray[i + 1];
                Array.Resize(ref _personArray, _personArray.Length - 1);

            }
        }

        /// <summary>
        /// Возвращает индекс элемента при наличии его в списке
        /// </summary>
        /// <param name="person">Экземпляр класса Персона</param>
        /// <returns>Индекс экземпляра класса</returns>
        public int GetIndexOfPerson(PersonBase person)
        {
            for (int index = 0; index < _personArray.Length; index++)
            {
                if (_personArray[index] == person)
                {
                    return index;
                }
            }

            throw new Exception("The person you specified does not exist " +
                "in this list!");
        }

        /// <summary>
        /// Количество персон в списке
        /// </summary>
        public int NumberOfPersons => _personArray.Length;

        /// <summary>
        /// Добавление нескольких людей
        /// </summary>
        /// <param name="persons">Массив людей</param>
        public void AddArrayOfPeople(PersonBase[] persons)
        {
            foreach (PersonBase person in persons)
            {
                AddPerson(person);
            }
        }

        /// <summary>
        /// Поиск элемента по индексу
        /// </summary>
        /// <param name="index">Индекс человека</param>
        /// <returns>возвращает значение по указанному индексу</returns> 
        public PersonBase FindByIndex(int index)
        {
            if (index >= 0 && index < _personArray.Length)
            {
                return _personArray[index];
            }
            else
            {
                throw new Exception("The index you requested " +
                    "does not exist!");
            }
        }
    }
}