using ConsoleLoader;
using LibraryCards;
using System;

namespace ConsoleLoader
{
    /// <summary>
    /// Класс Program
    /// </summary>
    internal class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Для выхода из программы нажмите \'x\'\n" +
                    "Для начала работы нажмите любую другую клавишу...");

                ConsoleKeyInfo userInput = Console.ReadKey(true);
                Console.WriteLine();

                switch (userInput.KeyChar)
                {
                    case 'x':
                    case 'X':
                    case 'х':
                    case 'Х':
                        {
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                CardBase card = CardsReader.ReadCard();
                Console.WriteLine(card.GetInfo());
            }
        }
    }
}