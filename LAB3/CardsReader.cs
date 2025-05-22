using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryCards;

namespace ConsoleLoader
{
    /// <summary>
    ///Чтение библиотечной карточки с клавиатуры
    /// </summary>
    public class CardsReader
    {
        /// <summary>
        /// Метод чтения типа и параметров движения с клавиатуры
        /// </summary>
        /// <returns>Движение</returns>
        /// <exception cref="ArgumentException"></exception>
        public static CardBase ReadCard()
        {
            CardBase card = new Article();

            var actions = new List<Action>()
            {
                () =>
                {
                    Console.Write("1 - книга\n" +
                        "2 - статья из журнала\n" +
                        "3 - статья из сборника\n" +
                        "4 - диссертация\n" +
                        "Выберите тип карточки: ");
                },

                () =>
                {
                    int inputCard = Convert.ToInt32(Console.ReadLine());
                    switch (inputCard)
                    {
                        case 1:
                        {
                            card = ReadBook();
                            break;
                        }
                        case 2:
                        {
                            card = ReadMagazine();
                            break;
                        }
                        case 3:
                        {
                            card = ReadSbornik();
                            break;
                        }
                        case 4:
                        {
                            card = ReadDissertation();
                            break;
                        }
                        default:
                        {
                            throw new ArgumentException
                                ("\nВыберите тип карточки из" +
                                " представленных на экране");
                        }
                    }
                },
            };

            ActionHandler(actions);

            return card;
        }

        /// <summary>
        /// Метод чтения книги
        /// </summary>
        /// <returns>Карточка книги</returns>
        public static Book ReadBook()
        {
            Book book = new Book();
            var actions = new List<Action>()
            {
                () =>
                {
                    Console.Write("Введите фамилию автора: ");
                    book.Surname =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите имя автора: ");
                    book.Name =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите отчество автора: ");
                    book.Patronymic =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите название работы: ");
                    book.Title = Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите сведения об издания (если есть): ");
                    book.AdditionalInformation = Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите место издательства: ");
                    book.PlaceOfPublication = Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите название издательства: ");
                    book.PublishingHouse = Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите год издания: ");
                    book.Year = Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите количество страниц: ");
                    book.Sheet = ReadFromConsoleAndParse();
                }
            };

            ActionHandler(actions);
            return book;
        }
        /// <summary>
        /// Чтение с консоли и преобразование в int
        /// </summary>
        public static int ReadFromConsoleAndParse()
        {
            return int.Parse(Console.ReadLine());
        }

        /// <summary>
        /// Метод чтения карточки статьи из журнала
        /// </summary>
        /// <returns>Карточка статьи из журнала</returns>
        public static Magazine ReadMagazine()
        {
            Magazine magazine = new Magazine();
            var actions = new List<Action>()
            {
                () =>
                {
                    Console.Write("Введите фамилию автора: ");
                    magazine.Surname =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите имя автора: ");
                    magazine.Name =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите отчество автора: ");
                    magazine.Patronymic =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите название работы: ");
                    magazine.Title =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите название журнала: ");
                    magazine.NameOfMagazine =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите год издания: ");
                    magazine.Year =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите начальную страницу: ");
                    magazine.StartSheet =ReadFromConsoleAndParse();
                },

                () =>
                {
                    Console.Write("Введите последнюю страницу: ");
                    magazine.EndSheet =ReadFromConsoleAndParse();
                }
            };

            ActionHandler(actions);
            return magazine;
        }

        /// <summary>
        /// Метод чтения карточки статьи из сборника
        /// </summary>
        /// <returns>Карточка статьи из сборника.</returns>
        public static Article ReadSbornik()
        {
            Article sbornik = new Article();
            var actions = new List<Action>()
            {
                () =>
                {
                    Console.Write("Введите фамилию автора: ");
                    sbornik.Surname =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите имя автора: ");
                    sbornik.Name =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите отчество автора: ");
                    sbornik.Patronymic =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите название работы: ");
                    sbornik.Title =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите название сборника: ");
                    sbornik.NameOfSbornik =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите место публикации: ");
                    sbornik.PlaceOfPublication =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите издательство: ");
                    sbornik.PublishingHouse =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите год издания: ");
                    sbornik.Year =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите начальную страницу: ");
                    sbornik.StartSheet =ReadFromConsoleAndParse();
                },

                () =>
                {
                    Console.Write("Введите последнюю страницу: ");
                    sbornik.EndSheet =ReadFromConsoleAndParse();
                }
            };

            ActionHandler(actions);
            return sbornik;
        }

        /// <summary>
        /// Метод чтения карточки диссертации
        /// </summary>
        /// <returns>Карточка диссертации</returns>
        public static Dissertation ReadDissertation()
        {
            Dissertation dissertation = new Dissertation();
            var actions = new List<Action>()
            {
                () =>
                {
                    Console.Write("Введите фамилию автора: ");
                    dissertation.Surname =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите имя автора: ");
                    dissertation.Name =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите отчество автора: ");
                    dissertation.Patronymic =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите название работы: ");
                    dissertation.Title =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите вид диссертации: ");
                    dissertation.KindOfDissertation =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите отрасль наук: ");
                    dissertation.BranchOfScience =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите код специальности формата __.__.__: ");
                    dissertation.SpecialtyCode =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите название специальности: ");
                    dissertation.NameOfSpeciality =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите код организации: ");
                    dissertation.SpecialtyCode =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите город, где получена специальность: ");
                    dissertation.City =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите год издания: ");
                    dissertation.Year =Console.ReadLine();
                },

                () =>
                {
                    Console.Write("Введите количество страниц: ");
                    dissertation.Sheet =ReadFromConsoleAndParse();
                }

            };

            ActionHandler(actions);
            return dissertation;
        }

        /// <summary>
        /// Обработчик действий
        /// </summary>
        /// <param name="actions"></param>
        private static void ActionHandler(List<Action> actions)
        {
            Dictionary<Type, Action<string>> catchDictionary =
                new Dictionary<Type, Action<string>>()
            {
                {
                    typeof(FormatException),
                    (string message) =>
                    {
                        Console.WriteLine($"Некорректный формат ввода");
                    }
                },

                {
                    typeof(ArgumentException),
                    Console.WriteLine
                },
            };

            foreach (var action in actions)
            {
                while (true)
                {
                    try
                    {
                        action.Invoke();
                        break;
                    }
                    catch (Exception exception)
                    {
                        catchDictionary[exception.GetType()].
                            Invoke(exception.Message);
                    }
                }
            }
        }
    }
}