using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibraryCards
{
    /// <summary>
    /// Класс создания библ. карточки по диссертации
    /// </summary>
    /// <returns>Объект класса Book</returns>
    public class Dissertation : CardBase
    {
        /// <summary>
        /// Вид диссертации
        /// </summary>
        private string _kindOfDissertation;

        /// <summary>
        /// Отрасль наук
        /// </summary>
        private string _branchOfScience;

        /// <summary>
        /// Код специальности
        /// </summary>
        private string _specialtyCode;

        /// <summary>
        /// Организация, где проходила защита
        /// </summary>
        private string _organization;

        /// <summary>
        /// Название специальности
        /// </summary>
        private string _nameOfSpeciality;

        /// <summary>
        /// Город, где получена специальность
        /// </summary>
        private string _city;

        /// <summary>
        /// Количество страниц
        /// </summary>
        private int _sheet;

        /// <summary>
        /// Регулярное выражение, определяющее код диссертации
        /// </summary>
        private const string _codeRegex = @"^\d{2}\.\d{2}\.\d{2}$";

        /// <summary>
        /// Объект класс Dissertation по умолчанию
        /// </summary>
        public Dissertation() : this("Неизвестно", "Неизвестно", 
            "Неизвестно", "Неизвестно",
            "Неизвестно", "Неизвестно", "Неизвестно",
            "00.00.00", "Неизвестно", "Неизвестно", "1900", 100)
        { }

        /// <summary>
        /// Конструктор класса Dissertation
        /// </summary>
        /// <param name="surname">Фамилия автора.</param>
        /// <param name="name">ФИО автора.</param>
        /// <param name="patronymic">ФИО автора.</param>
        /// <param name="title">Название работы.</param>
        /// <param name="kindOfDissert">Вид диссертации.</param>
        /// <param name="branchOfScience">Отрасль наук.</param>
        /// <param name="specialtyCode">Код специальности.</param>
        /// <param name="organization">Организация, где проходила защита.</param>
        /// <param name="nameOfSpeciality">Название специальности.</param>
        /// <param name="city">Город, где получена специальность.</param>
        /// <param name="year">Год издания.</param>
        /// <param name="sheet">Количество страниц.</param>
        public Dissertation(string surname, string name, string patronymic,
            string title, string kindOfDissert, string city,
            string branchOfScience, string specialtyCode, string organization,
            string nameOfSpeciality, string year, int sheet)
            : base(surname, name, patronymic, title, year)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Title = title;
            KindOfDissertation = kindOfDissert;
            BranchOfScience = branchOfScience;
            SpecialtyCode = specialtyCode;
            Organization = organization;
            City = city;
            NameOfSpeciality = nameOfSpeciality;
            Year = year;
            Sheet = sheet;
        }

        /// <summary>
        /// Количество страниц
        /// </summary>
        public int Sheet
        {
            get => _sheet;

            set
            {
                _sheet = IsCorrectSheet(value);
            }
        }

        /// <summary>
        /// Специальный код диссертации
        /// </summary>
        public string SpecialtyCode
        {
            get => _specialtyCode;

            set
            {
                if (Regex.IsMatch(value, _codeRegex))
                {
                    _specialtyCode = value;
                }
                else
                {
                    throw new ArgumentException(
                        $"Введите код в формате __.__.__ .");
                }
            }
        }

        /// <summary>
        /// Вид диссертации
        /// </summary>
        public string KindOfDissertation
        {
            get
            {
                return _kindOfDissertation;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(
                        "Введена пустая строка.");
                }
                else
                {
                    _kindOfDissertation = value;
                }
            }
        }

        /// <summary>
        /// Город, где получена специальность
        /// </summary>
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(
                        "Введена пустая строка.");
                }
                else
                {
                    _city = TitleSplitAndJoin(value);
                }
            }
        }

        /// <summary>
        /// Отрасль наук
        /// </summary>
        public string BranchOfScience
        {
            get
            {
                return _branchOfScience;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(
                        "Введена пустая строка.");
                }
                else
                {
                    _branchOfScience = TitleSplitAndJoin(value);
                }
            }
        }

        /// <summary>
        /// Организация
        /// </summary>
        public string Organization
        {
            get
            {
                return _organization;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(
                        "Введена пустая строка.");
                }
                else
                {
                    _organization = TitleSplitAndJoin(value);
                }
            }
        }

        /// <summary>
        /// Навазние специальности
        /// </summary>
        public string NameOfSpeciality
        {
            get
            {
                return _nameOfSpeciality;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(
                        "Введена пустая строка.");
                }
                else
                {
                    _nameOfSpeciality = TitleSplitAndJoin(value);
                }
            }
        }

        /// <summary>
        /// Проверяет страницу на корректность/>
        /// </summary>
        /// <param name="sheet">Имя объекта</param>
        /// <returns>Страницы/>.</returns>
        public int IsCorrectSheet(int sheet)
        {
            string sheetStr = Convert.ToString(sheet);
            if (Regex.IsMatch(sheetStr, _ageRegex)
                && !string.IsNullOrEmpty(sheetStr))
            {
                try
                {
                    if (sheet > MaxSheet || sheet < MinSheet)
                    {
                        throw new ArgumentException(
                            $"Введите страницу из диапазона " +
                            $"от {MinSheet} до {MaxSheet}.");
                    }
                    else
                    {
                        return sheet;
                    }
                }
                catch (OverflowException)
                {
                    throw new ArgumentException(
                            $"Введите страницу из диапазона " +
                            $"от {MinSheet} до {MaxSheet}.");
                }
            }
            else
            {
                throw new ArgumentException("Введите только число.");
            }
        }

        /// <summary>
        /// Возвращает название типа для отображения в UI.
        /// </summary>
        public override string GetTypeName()
        {
            return "Диссертация";
        }

        /// <summary>
        /// Метод вывода библиотечной карточки
        /// </summary>
        /// <returns>Данные об издании.</returns>
        public override string GetInfo()
        {
            return $"{MakeSample(Surname, Name, Patronymic)} {Title} :" +
                   $" специальность {SpecialtyCode} «{NameOfSpeciality}» :" +
                   $" Диссертация на соискание {KindOfDissertation} {BranchOfScience} / " +
                   $"{Surname} {Name} {Patronymic} ; " +
                   $"{Organization}. - {City}. - {Year}. - {Sheet} с.";
        }

    }
}