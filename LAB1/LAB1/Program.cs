using System;
using PersonLib;

namespace LAB1
{
    /// <summary>
    /// Основной класс  
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа в программу
        /// </summary>
        /// <param name="args">Параметры</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start...");
            Console.WriteLine();
            Console.ReadKey();
            Console.WriteLine("1.Создать два списка персон, каждый из которых " +
                " содержит по три человека...");
            Console.WriteLine();
            Console.ReadKey();

            var listOne = new PersonList();
            var listTwo = new PersonList();

            var arrayOne = new Person[]
            {
                new Person("Tyrion", "Lannister", 45, Sex.Male),
                new Person("Jon", "Snow", 28, Sex.Male),
                new Person("Sansa", "Stark", 18, Sex.Female),
            };

            var arrayTwo = new Person[]
            {
                new Person("Joffrey", "Baratheon", 12, Sex.Male),
                new Person("Khal", "Drogo", 35, Sex.Male),
                new Person("Viserys", "Targaryen", 40, Sex.Male),
            };

            listOne.AddArrayOfPeople(arrayOne);
            listTwo.AddArrayOfPeople(arrayTwo);

            Console.WriteLine("Успешно созданы два списка персон!");
            Console.ReadKey();

            Console.WriteLine();
            Console.WriteLine("2.Отобразить содержимое каждого списка на консоли...");
            ShowListOfPersons(listOne, listTwo);
            Console.WriteLine();

            Console.WriteLine("3.Добавить нового пользователя в первый список... ");
            listOne.AddPerson(new Person("King", "Kong", 45, Sex.Male));
            ShowListOfPersons(listOne, listTwo);
            Console.WriteLine();
            Console.WriteLine("Новый пользователь успешно добавлен в первый список");
            Console.ReadKey();

            Console.WriteLine();
            Console.WriteLine("4.Копирование второго пользователя из первого " +
                " списка в конец второго списка... ");
            listTwo.AddPerson(listOne.FindByIndex(1));
            ShowListOfPersons(listOne, listTwo);
            Console.WriteLine();
            Console.WriteLine("Теперь в обоих списках значится один и тот же человек!");
            Console.WriteLine();
            Console.ReadKey();

            Console.WriteLine("5.Удаление второго человека из первого списка...");
            listOne.DeleteByIndex(1);
            ShowListOfPersons(listOne, listTwo);
            Console.WriteLine();
            Console.WriteLine("Удаление человека из 1-го списка не привело к удалению " +
                "того же человека из 2-го списка!");
            Console.WriteLine();
            Console.ReadKey();

            Console.WriteLine("6.Очистка второго списка...");
            listTwo.DeleteAllPeople();
            ShowListOfPersons(listOne, listTwo);
            Console.WriteLine("Все лица были успешно удалены из 2-го списка!");
            Console.WriteLine();
            Console.ReadKey();

            Console.WriteLine("7.Давайте добавим нового человека " +
                "во второй список с клавиатуры...");
            Console.WriteLine();
            listTwo.AddPerson(AddConsolePerson.NewPerson());
            ShowListOfPersons(listOne, listTwo);
            Console.WriteLine();

            Console.WriteLine("8.Добавить случайного человека " +
                "ко второму списку...");
            Person randPerson = RandomPerson.GetRandomPerson();
            listTwo.AddPerson(randPerson);
            ShowListOfPersons(listOne, listTwo);
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Вывод списка персон
        /// </summary>
        /// <param name="listOne">Список один</param>
        /// <param name="listTwo">Список два</param>
        public static void ShowListOfPersons(PersonList listOne, PersonList listTwo)
        {
            var personLists = new PersonList[]
            {
                listOne,
                listTwo
            };
            Console.ReadKey();
            for (int i = 0; i < personLists.Length; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"List {i + 1}");
                Console.WriteLine();

                for (int j = 0; j < personLists[i].NumberOfPersons; j++)
                {
                    Console.WriteLine(personLists[i].FindByIndex(j).Info);
                }
            }
            Console.ReadKey();
        }
    }
}
