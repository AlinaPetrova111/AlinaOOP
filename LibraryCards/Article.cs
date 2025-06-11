using System;
using System.Text.RegularExpressions;

namespace LibraryCards
{
    /// <summary>
    /// Класс создания библ. карточки по статье из сборника
    /// </summary>
    /// <returns>Объект класса Article</returns>
    public class Article : CardBase
    {
        /// <summary>
        /// Название сборника
        /// </summary>
        private string _nameOfArticle;

        /// <summary>
        /// Издательство
        /// </summary>
        private string _publishingHouse;

        /// <summary>
        /// Место издания
        /// </summary>
        private string _placeOfPublication;

        /// <summary>
        /// Начальная страница
        /// </summary>
        private int _startSheet;

        /// <summary>
        /// Последняя странца
        /// </summary>
        private int _endSheet;

        /// <summary>
        /// Объект класс Article по умолчанию
        /// </summary>
        public Article() : this("Неизвестно", "Неизвестно", "Неизвестно",
            "Неизвестно", "Неизвестно", "Неизвестно", "Неизвестно", "1900", 1, 106)
        { }

        /// <summary>
        /// Конструктор класса Article
        /// </summary>
        /// <param name="surname">Фамилия автора</param>
        /// <param name="name">ФИО автора</param>
        /// <param name="patronymic">ФИО автора</param>
        /// <param name="title">Название работы</param>
        /// <param name="placeOfPublication">Место публикации</param>
        /// <param name="publishingHouse">Издательство</param>
        /// <param name="nameOfArticle">Название сборника</param>
        /// <param name="year">Год издания</param>
        /// <param name="startSheet">Начальная страница</param>
        /// <param name="endSheet">Последняя страница</param>
        public Article(string surname, string name, string patronymic,
            string title, string nameOfArticle, string placeOfPublication,
            string publishingHouse, string year, int startSheet,
            int endSheet) : base(surname, name, patronymic, title, year)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Title = title;
            NameOfArticle = nameOfArticle;
            PlaceOfPublication = placeOfPublication;
            PublishingHouse = publishingHouse;
            Year = year;
            StartSheet = startSheet;
            EndSheet = endSheet;
        }

        /// <summary>
        /// Название сборника
        /// </summary>
        public string NameOfArticle
        {
            get
            {
                return _nameOfArticle;
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
                    _nameOfArticle = TitleSplitAndJoin(value);
                }
            }
        }

        /// <summary>
        /// Место издания
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
        /// Издательство
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
        /// Проверяет страницу на корректность/>
        /// </summary>
        /// <param name="sheet">Имя объекта</param>
        /// <returns>Страницы/>.</returns>
        public int IsCorrectStartSheet(int sheet)
        {
            string sheetStr= Convert.ToString(sheet);
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
        /// Проверяет страницу на корректность/>
        /// </summary>
        /// <param name="endSheet">Имя объекта.</param>
        /// <returns>True or False/>.</returns>
        public int IsCorrectSheet(int endSheet)
        {
            string sheetStr = Convert.ToString(endSheet);
            if (Regex.IsMatch(sheetStr, _ageRegex))
            {
                try
                {
                    if (endSheet <= StartSheet)
                    {
                        throw new ArgumentException($"Введите число больше {StartSheet}.");
                    }
                    else if (endSheet > MaxSheet || endSheet < MinSheet)
                    {
                        throw new ArgumentException(
                            $"Введите страницу из диапазона " +
                            $"от {MinSheet} до {MaxSheet}.");
                    }
                    else
                    {
                        return endSheet;
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
        /// Возвращает название типа для отображения в UI.
        /// </summary>
        public override string GetTypeName()
        {
            return "Статья";
        }

        /// <summary>
        /// Метод вывода библиотечной карточки
        /// </summary>
        /// <returns>Данные об издании</returns>
        public override string GetInfo()
        {
            return $"{MakeSample(Surname, Name, Patronymic)} {Title} /" +
                   $"{ReverseFullname(MakeSample(Surname, Name, Patronymic))}. " +
                   $"// {NameOfArticle}. –{PlaceOfPublication}: - №{PublishingHouse}," +
                   $" {Year}. - С. {StartSheet}-{EndSheet}.";
        }

    }
}