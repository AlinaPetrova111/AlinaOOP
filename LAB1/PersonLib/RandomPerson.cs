using System;

namespace PersonLib
{
    /// <summary>
    /// Класс для создания случайного человека
    /// </summary>
    public class RandomPerson
    {
        /// <summary>
        /// Генерирует случайного человека
        /// </summary>
        /// <returns>Персона со случайными данными</returns>
        public static Person GetRandomPerson()
        {
            string[] maleNames = new string[]
            {
                "Mickey", "Bugs", "Darth", "James",
                "Peter", "Harry", "Vito",
                "Homer", "Hannibal", "Tony", "Willy"
            };

            string[] femaleNames = new string[]
            {
                "Mulan", "Sarah", "Katniss", "Belle",
                "Maleficent", "Rapunzel", "Moana",
                "Bellatrix", "Fiona", "Alice", "Mystique"
            };

            string[] allSurnames = new string[]
            {
                "Mouse", "Bunny", "Vader", "Bond",
                "Pan", "Potter", "Corleone",
                "Simpson", "Lector", "Soprano", "Wonka"
            };

            Random random = new Random();

            string name;
            Sex sex = (Sex)random.Next(0, 2);
            switch (sex)
            {
                case Sex.Male:
                    name = maleNames[random.Next(maleNames.Length)];
                    break;
                case Sex.Female:
                    name = femaleNames[random.Next(femaleNames.Length)];
                    break;
                default:
                    return new Person("Default", "Person", 0, Sex.Male);
            }

            string surname = allSurnames[random.Next(allSurnames.Length)];

            int age = random.Next(0, Person.AgeMax);

            return new Person(name, surname, age, sex);
        }
    }
}
