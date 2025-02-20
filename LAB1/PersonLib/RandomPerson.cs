using System;

namespace PersonLib
{
    //TODO: XML
    public class RandomPerson
    {
        /// <summary>
        /// Генерирует случайного человека
        /// </summary>
        /// <returns>Персона со случайными данными</returns>
        public static Person GetRandomPerson()
        {
            //TODO: RSDN
            string[] _maleNames = new string[]
            {
                "Mickey", "Bugs", "Darth", "James",
                "Peter", "Harry", "Vito",
                "Homer", "Hannibal", "Tony", "Willy"
            };

            string[] _femaleNames = new string[]
            {
                "Mulan", "Sarah", "Katniss", "Belle",
                "Maleficent", "Rapunzel", "Moana",
                "Bellatrix", "Fiona", "Alice", "Mystique"
            };

            string[] _allSurnames = new string[]
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
                    name = _maleNames[random.Next(_maleNames.Length)];
                    break;
                case Sex.Female:
                    name = _femaleNames[random.Next(_femaleNames.Length)];
                    break;
                default:
                    return new Person("Default", "Person", 0, Sex.Male);
            }

            string surname = _allSurnames[random.Next(_allSurnames.Length)];

            int age = random.Next(0, Person.AgeMax);

            return new Person(name, surname, age, sex);
        }
    }
}
