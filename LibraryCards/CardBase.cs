using System;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace LibraryCards
{
    [XmlInclude(typeof(Book))]
    [XmlInclude(typeof(Magazine))]
    [XmlInclude(typeof(Article))]
    [XmlInclude(typeof(Dissertation))]

    /// <summary>
    /// Базовый класс создания библиотечной карточки
    /// </summary>
    /// <returns>Объект класса CardBase</returns>
    public abstract class CardBase
    {
        /// <summary>
        /// Фамилия автора
        /// </summary>
        private string _surname;

        /// <summary>
        /// Имя автора
        /// </summary>
        private string _name;

        /// <summary>
        /// Отчество автора
        /// </summary>
        private string _patronymic;

        /// <summary>
        /// Название
        /// </summary>
        private string _title;

        /// <summary>
        /// Регулярное выражение, выявляющее русские буквы
        /// </summary>
        private const string _russianRegex = @"(^[а-яА-Я]+(-[а-яА-Я]+)*$)";

        /// <summary>
        /// Регулярное выражение, выявляющее цифры
        /// </summary>
        protected const string _ageRegex = @"^-?\d+$";

        /// <summary>
        /// Год издания
        /// </summary>
        private string _year;

        /// <summary>
        /// Минимальный год издания
        /// </summary>
        public int MinYear { get; } = 1;

        /// <summary>
        /// Масимальный год издания
        /// </summary>
        public int MaxYear { get; } = 2025;

        /// <summary>
        /// Минимальное количество страниц
        /// </summary>
        public int MinSheet { get; } = 1;

        /// <summary>
        /// Масимальное количество страниц
        /// </summary>
        public int MaxSheet { get; } = 5000;

        /// <summary>
        /// Объект класс CardBase по умолчанию
        /// </summary>
        public CardBase() : this("Неизвестно", "Неизвестно",
            "Неизвестно", "Неизвестно", "1900")
        { }

        /// <summary>
        /// Конструктор класса CardBase
        /// </summary>
        /// <param name="surname">Фамилия автора</param>
        /// <param name="name">ФИО автора</param>
        /// <param name="patronymic">ФИО автора</param>
        /// <param name="title">Название работы</param>
        /// <param name="year">Возраст</param>
        public CardBase(string surname, string name, string patronymic,
            string title, string year)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Title = title;
            Year = year;
        }

        /// <summary>
        /// Имя автора
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
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
                    _name = IsCorrectName(value);
                }
            }
        }

        /// <summary>
        /// Фамилия автора
        /// </summary>
        public string Surname
        {
            get
            {
                return _surname;
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
                    _surname = IsCorrectName(value);
                }
            }
        }

        /// <summary>
        /// Отчество автора
        /// </summary>
        public string Patronymic
        {
            get
            {
                return _patronymic;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _patronymic = value;
                }
                else
                {
                    _patronymic = IsCorrectName(value);
                }
            }
        }

        /// <summary>
        /// Название
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
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
                    _title = TitleSplitAndJoin(value);
                }
            }
        }

        /// <summary>
        /// Возраст
        /// </summary>
        public virtual string Year
        {
            get => _year;

            set
            {
                _year = IsCorrectYear(value);
            }
        }

        /// <summary>
        /// Проверяет корректность введенных данных./>
        /// </summary>
        /// <param name="name">Имя объекта</param>
        /// <returns>Корректное имя или фамилия./>.</returns>
        public static string IsCorrectName(string name)
        {
            string correctName;
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

            if (name.Contains("-"))
            {

                string[] words = name.Split(new char[] { '-' });
                if (words.Length == 2)
                {
                    if ((Regex.IsMatch(words[0], _russianRegex)
                        && Regex.IsMatch(words[1], _russianRegex)))
                    {
                        words[0] = textInfo.ToTitleCase(words[0]);
                        words[1] = textInfo.ToTitleCase(words[1]);
                        correctName = string.Join("-", words);
                    }
                    else
                    {
                        throw new ArgumentException("Составное имя и фамилия" +
                            "(отчество) должны" +
                            " содержать только русские буквы.");
                    }
                }
                else
                {
                    throw new ArgumentException("Некорректное значение");
                }
            }
            else
            {
                if (Regex.IsMatch(name, _russianRegex))
                {
                    correctName = textInfo.ToTitleCase(name);
                }
                else
                {
                    throw new ArgumentException("Имя и фамилия(отчество) должны " +
                        "содержать только русские буквы.");
                }
            }
            return correctName;
        }

        /// <summary>
        /// Проверяет корректность введенных данных./>
        /// </summary>
        /// <param name="title">Имя объекта.</param>
        /// <returns>Корректное имя или фамилия./>.</returns>
        public static string TitleSplitAndJoin(string title)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string[] words;
            words = title.Split(' ');
            words[0] = textInfo.ToTitleCase(words[0]);

            return string.Join(" ", words);
        }

        /// <summary>
        /// Метод формирования шаблона ФИО
        /// </summary>
        /// <param name="surname">Имя объекта</param>
        /// <param name="name">Имя объекта</param>
        /// <param name="patronymic">Имя объекта</param>
        /// <returns>Данные об издании</returns>
        public static string MakeSample(
            string surname, string name, string patronymic)
        {
            string shortFullname = $"{surname} {name[0]}." +
                $"{patronymic[0]}.";

            return shortFullname;

        }

        /// <summary>
        /// Метод изменения порядка ФИО в ИОФ
        /// </summary>
        /// <param name="fullName">Имя объекта</param>
        /// <returns>Данные об издании</returns>
        public static string ReverseFullname(string fullName)
        {
            string[] parts = fullName.Split(' ');
            string reverseFullname = parts[1] + " " + parts[0];

            return reverseFullname;

        }

        /// <summary>
        /// Проверяет возраст на корректность./>
        /// </summary>
        /// <param name="year">Имя объекта</param>
        /// <returns>Возраст/>.</returns>
        public string IsCorrectYear(string year)
        {
            if (Regex.IsMatch(year, _ageRegex)
                && !string.IsNullOrEmpty(year))
            {
                try
                {
                    int yearInt = Convert.ToInt16(year);
                    if (yearInt > MaxYear || yearInt < MinYear)
                    {
                        throw new ArgumentException(
                            $"Введите год из диапазона " +
                            $"от {MinYear} до {MaxYear}.");
                    }
                    else
                    {
                        return year;
                    }
                }
                catch (OverflowException ex)
                {
                    throw new ArgumentException(
                            $"Введите год из диапазона " +
                            $"от {MinYear} до {MaxYear}.");
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
        public virtual string GetTypeName()
        {
            return this.GetType().Name;
        }

        /// <summary>
        /// Метод вывода библиотечной карточки
        /// </summary>
        /// <returns>Данные об издании</returns>
        public virtual string GetInfo()
        {
            return $"{MakeSample(Surname, Name, Patronymic)} {Title}/" +
                   $"{ReverseFullname(MakeSample(Surname, Name, Patronymic))}." +
                   $"— {Year}.";
        }
    }
}