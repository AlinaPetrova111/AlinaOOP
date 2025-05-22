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
    /// Класс создания библ. карточки по стате из журнала
    /// </summary>
    /// <returns>Объект класса Magazine.</returns>
    public class Magazine : CardBase
    {
        /// <summary>
        /// Название журнала
        /// </summary>
        private string _nameOfMagazine;

        /// <summary>
        /// Номер журнала
        /// </summary>
        private string _numberOfMagazine;

        /// <summary>
        /// Начальная страница
        /// </summary>
        private string _startSheet;

        /// <summary>
        /// Последняя странца
        /// </summary>
        private string _endSheet;

        //TODO: duplication
        /// <summary>
        /// Регулярное выражение, выявляющее цифры
        /// </summary>
        private const string _ageRegex = @"^-?\d+$";

        /// <summary>
        /// Объект класс Magazine по умолчанию
        /// </summary>
        public Magazine() : this("Неизвестно", "Неизвестно", "Неизвестно",
            "Неизвестно", "Неизвестно", "1", "1900", "1", null)
        { }

        /// <summary>
        /// Конструктор класса Magazine
        /// </summary>
        /// <param name="surname">Фамилия автора</param>
        /// <param name="name">ФИО автора</param>
        /// <param name="patronymic">ФИО автора</param>
        /// <param name="title">Название работы</param>
        /// <param name="nameOfMagazine">Название журнала</param>
        /// <param name="numberOfMagazine">Номер журнала</param>
        /// <param name="year">Год издания</param>
        /// <param name="startSheet">Начальная страница</param>
        /// <param name="endSheet">Последняя страница</param>
        public Magazine(string surname, string name, string patronymic,
            string title, string nameOfMagazine, string numberOfMagazine, string year,
            string startSheet, string endSheet)
            : base(surname, name, patronymic, title, year)

        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Title = title;
            NameOfMagazine = nameOfMagazine;
            NumberOfMagazine = numberOfMagazine;
            Year = year;
            StartSheet = startSheet;
            EndSheet = endSheet;
        }

        /// <summary>
        /// Название журнала
        /// </summary>
        public string NameOfMagazine
        {
            get
            {
                return _nameOfMagazine;
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
                    _nameOfMagazine = TitleSplitAndJoin(value);
                }
            }
        }

        /// <summary>
        /// Номер магазина
        /// </summary>
        public string NumberOfMagazine
        {
            get => _numberOfMagazine;

            set
            {
                _numberOfMagazine = IsCorrectStartSheet(value);
            }
        }

        /// <summary>
        /// Начальная страница
        /// </summary>
        public string StartSheet
        {
            get => _startSheet;

            set
            {
                _startSheet = IsCorrectStartSheet(value);
            }
        }

        /// <summary>
        /// Последняя страница
        /// </summary>
        public string EndSheet
        {
            get => _endSheet;

            set
            {
                _endSheet = IsCorrectSheet(value);
            }
        }

        /// <summary>
        /// Проверяет страницу на корректность./>
        /// </summary>
        /// <param name="sheet">Имя объекта.</param>
        /// <returns>Страницы/>.</returns>
        public string IsCorrectStartSheet(string sheet)
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
                            $"Введите число из диапазона " +
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
                            $"Введите число из диапазона " +
                            $"от {MinSheet} до {MaxSheet}.");
                }
            }
            else
            {
                throw new ArgumentException("Введите только число.");
            }
        }

        /// <summary>
        /// Проверяет страницу на корректность./>
        /// </summary>
        /// <param name="endSheet">Имя объекта.</param>
        /// <returns>True or False/>.</returns>
        public string IsCorrectSheet(string endSheet)
        {
            if (endSheet == null)
            {
                return null;
            }
            if (Regex.IsMatch(endSheet, _ageRegex))
            {
                try
                {
                    int intEndSheet = Convert.ToInt16(endSheet);
                    int startSheet = Convert.ToInt16(StartSheet);
                    if (intEndSheet < startSheet)
                    {
                        throw new ArgumentException($"Введите число больше {startSheet}.");
                    }
                    else if (intEndSheet == startSheet)
                    {
                        return null;
                    }
                    else
                    {
                        return $"-{endSheet}";
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
                throw new ArgumentException($"Введите последнюю страницу.");
            }
        }

        /// <summary>
        /// Метод вывода библиотечной карточки
        /// </summary>
        /// <returns>Данные об издании.</returns>
        public override string GetInfo()
        {
            return $"{MakeSample(Surname, Name, Patronymic)} {Title} /" +
                   $"{ReverseFullname(MakeSample(Surname, Name, Patronymic))}." +
                   $" // {NameOfMagazine}. – {Year}. - №" +
                   $"{NumberOfMagazine}. - С. {StartSheet}{EndSheet}.";
        }
    }
}
