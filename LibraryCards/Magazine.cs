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
        /// Начальная страница
        /// </summary>
        private int _startSheet;

        /// <summary>
        /// Последняя странца
        /// </summary>
        private int _endSheet;

        /// <summary>
        /// Объект класс Magazine по умолчанию
        /// </summary>
        public Magazine() : this("Неизвестно", "Неизвестно", "Неизвестно",
            "Неизвестно", "1", "1900", 1, 1)
        { }

        /// <summary>
        /// Конструктор класса Magazine
        /// </summary>
        /// <param name="surname">Фамилия автора</param>
        /// <param name="name">ФИО автора</param>
        /// <param name="patronymic">ФИО автора</param>
        /// <param name="title">Название работы</param>
        /// <param name="nameOfMagazine">Название журнала</param>
        /// <param name="year">Год издания</param>
        /// <param name="startSheet">Начальная страница</param>
        /// <param name="endSheet">Последняя страница</param>
        public Magazine(string surname, string name, string patronymic,
            string title, string nameOfMagazine, string year,
            int startSheet, int endSheet)
            : base(surname, name, patronymic, title, year)

        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Title = title;
            NameOfMagazine = nameOfMagazine;
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
        /// Начальная страница
        /// </summary>
        public int StartSheet
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
        public int EndSheet
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
        public int IsCorrectStartSheet(int sheet)
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
        public int IsCorrectSheet(int endSheet)
        {
            string sheetStr = Convert.ToString(endSheet);
            if (Regex.IsMatch(sheetStr, _ageRegex))
            {
                try
                {

                    if (endSheet > MaxSheet || endSheet < MinSheet)
                    {
                        throw new ArgumentOutOfRangeException(nameof(endSheet),
                           $"Введите страницу из диапазона от {MinSheet} до {MaxSheet}.");
                    }
                    return endSheet;
                }
                catch (OverflowException)
                {
                    throw new ArgumentException(
                            $"Введите страницу из диапазона от {MinSheet} до {MaxSheet}.");
                }
            }
            else
            {
                throw new ArgumentException($"Введите последнюю страницу.");
            }
        }

        /// <summary>
        /// Возвращает название типа для отображения в UI.
        /// </summary>
        public override string GetTypeName()
        {
            return "Журнал";
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
                   $" {StartSheet}-{EndSheet}.";
        }
    }
}
