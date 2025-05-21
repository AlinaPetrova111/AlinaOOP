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
    /// Класс создания библ. карточки по книге.
    /// </summary>
    /// <returns>Объект класса Book.</returns>
    public class Book : CardBase
    {
        /// <summary>
        /// Место издания.
        /// </summary>
        private string _placeOfPublication;

        /// <summary>
        /// Издательство.
        /// </summary>
        private string _publishingHouse;

        /// <summary>
        /// Сведение об издании.
        /// </summary>
        private string _additionalInformation;

        /// <summary>
        /// Количество страниц.
        /// </summary>
        private string _sheet;

        /// <summary>
        /// Регулярное выражение, выявляющее цифры.
        /// </summary>
        private const string _ageRegex = @"^-?\d+$";

        /// <summary>
        /// Объект класс Book по умолчанию.
        /// </summary>
        public Book() : this("Неизвестно", "Неизвестно", "Неизвестно",
            "Неизвестно", "Неизвестно", "Неизвестно", null, "1900", "100")
        { }

        /// <summary>
        /// Конструктор класса Book.
        /// </summary>
        /// <param name="surname">Фамилия автора.</param>
        /// <param name="name">ФИО автора.</param>
        /// <param name="patronymic">ФИО автора.</param>
        /// <param name="title">Название работы.</param>
        /// <param name="placeOfPublication">Место издания.</param>
        /// <param name="publishingHouse">Издательство.</param>
        /// <param name="additionalInformation">Сведение об издании.</param>
        /// <param name="year">Год издания.</param>
        /// <param name="sheet">Количество страниц.</param>
        public Book(string surname, string name, string patronymic,
            string title, string placeOfPublication, string publishingHouse,
            string additionalInformation, string year,
            string sheet) : base(surname, name, patronymic, title, year)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Title = title;
            PlaceOfPublication = placeOfPublication;
            PublishingHouse = publishingHouse;
            AdditionalInformation = additionalInformation;
            Year = year;
            Sheet = sheet;
        }

        /// <summary>
        /// Место издания.
        /// </summary>
        public string PlaceOfPublication
        {
            get
            {
                return _placeOfPublication;
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
                    _placeOfPublication = TitleSplitAndJoin(value);
                }
            }
        }

        /// <summary>
        /// Издательство.
        /// </summary>
        public string PublishingHouse
        {
            get
            {
                return _publishingHouse;
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
                    _publishingHouse = TitleSplitAndJoin(value);
                }
            }
        }

        /// <summary>
        /// Сведение об издании.
        /// </summary>
        public string AdditionalInformation
        {
            get => _additionalInformation;
            set
            {
                if (value == "")
                {
                    _additionalInformation = null;
                }
                else
                {
                    _additionalInformation = $"- {value}";
                }
            }
        }

        /// <summary>
        /// Количество страниц.
        /// </summary>
        public string Sheet
        {
            get => _sheet;

            set
            {
                _sheet = IsCorrectSheet(value);
            }
        }

        /// <summary>
        /// Проверяет страницу на корректность./>.
        /// </summary>
        /// <param name="sheet">Имя объекта.</param>
        /// <returns>Страницы/>.</returns>
        public string IsCorrectSheet(string sheet)
        {
            if (Regex.IsMatch(sheet, _ageRegex)
                && !string.IsNullOrEmpty(sheet))
            {
                try
                {
                    int sheetInt = Convert.ToInt16(sheet);
                    if (sheetInt > MaxSheet || sheetInt < MinSheet)
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
                catch (OverflowException ex)
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
        /// Метод вывода библиотечной карточки.
        /// </summary>
        /// <returns>Данные об издании.</returns>
        public override string GetInfo()
        {
            return $"{MakeSample(Surname, Name, Patronymic)} {Title} /" +
                   $"{ReverseFullname(MakeSample(Surname, Name, Patronymic))}." +
                   $" {AdditionalInformation}–{PlaceOfPublication} :" +
                   $" {PublishingHouse}, {Year}. - {Sheet} с.";
        }

    }
}
