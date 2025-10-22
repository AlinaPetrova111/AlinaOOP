using System;
using System.Collections.Generic;
using LibraryCards;

namespace LibraryView
{
    /// <summary>
    /// Предоставляет методы для создания случайных данных для библиотечных карточек.
    /// </summary>
    public static class CardDataRandomizer
    {
        /// <summary>
        /// Генератор случайных чисел для заполнения полей тестовыми данными.
        /// </summary>
        private static readonly Random _random = new Random();

        /// <summary>
        /// Список фамилий 
        /// </summary>
        private static readonly List<string> _hpSurnames = new List<string>
        {
            "Поттер", "Грейнджер", "Уизли",
            "Малфой", "Лонгботтом", "Лавгуд",
            "Дамблдор", "Снейп", "Макгонагалл",
            "Блэк", "Люпин"
        };

        /// <summary>
        /// Список мужских имен персонажей.
        /// </summary>
        private static readonly List<string> _hpMaleNames = new List<string>
        {
            "Гарри", "Рон", "Драко", "Альбус", "Северус",
            "Сириус", "Римус", "Невилл", "Том"
        };

        /// <summary>
        /// Список женских имен персонажей.
        /// </summary>
        private static readonly List<string> _hpFemaleNames = new List<string>
        {
            "Гермиона", "Джинни", "Луна", "Минерва", "Беллатриса",
            "Нимфадора", "Лили"
        };

        /// <summary>
        /// Список мужских отчеств.
        /// </summary>
        private static readonly List<string> _hpMalePatronymics = new List<string>
        {
            "Джеймсович", "Артурович", "Люциусович", "Персивалевич",
            "Тобиасович",
            "Орионович", "Лайэллович", "Фрэнкович", "Реддлович"
        };

        /// <summary>
        /// Список женских отчеств.
        /// </summary>
        private static readonly List<string> _hpFemalePatronymics = new List<string>
        {
            "Дэниеловна", "Артуровна", "Ксенофилиусовна", "Робертовна", "Эвановна"
        };

        /// <summary>
        /// Список названий для книг и статей.
        /// </summary>
        private static readonly List<string> _hpBookTitles = new List<string>
        {
            "Современная зельеварение", "Тёмные искусства: Руководство",
            "История магии", "Квиддич сквозь века", "Фантастические твари" +
            " и где они обитают",
            "Тысяча магических трав и грибов", "Теория защитной магии"
        };

        /// <summary>
        /// Список названий для журналов.
        /// </summary>
        private static readonly List<string> _hpMagazineTitles = new List<string>
        {
            "Придира", "Ежедневный пророк", "Ведьмин досуг", "Трансфигурация сегодня"
        };

        /// <summary>
        /// Список названий магических организаций.
        /// </summary>
        private static readonly List<string> _hpOrganizations = new List<string>
        {
            "Министерство Магии", "Аврорат", "Отдел тайн", "Хогвартс", "Гринготтс"
        };

        /// <summary>
        /// Создает и возвращает библиотечную карточку указанного типа,
        /// заполненную случайными данными.
        /// </summary>
        /// <param name="cardTypeIndex">Индекс типа карточки, соответствующий
        /// порядку в ComboBox на форме.</param>
        /// <returns>Заполненный объект, унаследованный от <see cref="CardBase"/>,
        /// или null, если индекс некорректен.</returns>
        public static CardBase GenerateRandomCard(int cardTypeIndex)
        {
            bool isMale = _random.Next(0, 2) == 0;
            var namesList = isMale ? _hpMaleNames : _hpFemaleNames;
            var patronymicsList = isMale ? _hpMalePatronymics : _hpFemalePatronymics;

            string surname = _hpSurnames[_random.Next(_hpSurnames.Count)];
            string name = namesList[_random.Next(namesList.Count)];
            string patronymic = _random.Next(0, 3) > 0
                ? patronymicsList[_random.Next(patronymicsList.Count)]
                : string.Empty;

            string title = _hpBookTitles[_random.Next(_hpBookTitles.Count)] + 
               " №" + _random.Next(1, 100);
            string year = _random.Next(1950, DateTime.Now.Year + 1).ToString();

            CardBase card;

            switch (cardTypeIndex)
            {
                case 0:
                {
                    card = new Book
                    {
                        PlaceOfPublication = "Лондон",
                        PublishingHouse = "Издательство 'Мракоборец'",
                        //TODO: RSDN+
                        AdditionalInformation = _random.Next(0, 2) == 
                           0 ? "Расширенное издание" : "",
                        Sheet = _random.Next(50, 1000)
                    };
                    break;
                }
                case 1:
                {
                        int startM = _random.Next(1, 50);
                    card = new Magazine
                    {
                        NameOfMagazine = _hpMagazineTitles[_random.Next(_hpMagazineTitles.Count)],
                        StartSheet = startM,
                        EndSheet = _random.Next(startM + 1, startM + 30)
                    };
                    break;
                }
                case 2:
                {
                        int startA = _random.Next(1, 100);
                    card = new Article
                    {
                        NameOfArticle = "Ежегодник заклинаний",
                        PlaceOfPublication = "Хогсмид",
                        PublishingHouse = "Типография 'Флориш и Блоттс'",
                        StartSheet = startA,
                        EndSheet = _random.Next(startA + 1, startA + 25)
                    };
                    break;
                }
                case 3:
                {
                    card = new Dissertation
                    {
                        KindOfDissertation = "Магистерская",
                        BranchOfScience = "Защита от Тёмных искусств",
                        SpecialtyCode = $"{_random.Next(1, 10):D2}." +
                                      $"{_random.Next(1, 10):D2}.{_random.Next(1, 10):D2}",
                        Organization = _hpOrganizations[_random.Next(_hpOrganizations.Count)],
                        NameOfSpeciality = "Боевая магия",
                        City = "Годрикова впадина",
                        Sheet = _random.Next(100, 400)
                    };
                    break;
                }
                default:
                    return null;
            }
            card.Surname = surname;
            card.Name = name;
            card.Patronymic = patronymic;
            card.Title = title;
            card.Year = year;

            return card;

        }
    }
}