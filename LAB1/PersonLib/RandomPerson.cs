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
            //TODO: RSDN
            string[] MaleNames = new string[]
            {
                "Mickey", "Bugs", "Darth", "James",
                "Peter", "Harry", "Vito",
                "Homer", "Hannibal", "Tony", "Willy"
            };

            string[] FemaleNames = new string[]
            {
                "Mulan", "Sarah", "Katniss", "Belle",
                "Maleficent", "Rapunzel", "Moana",
                "Bellatrix", "Fiona", "Alice", "Mystique"
            };

            string[] AllSurnames = new string[]
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
                    name = MaleNames[random.Next(MaleNames.Length)];
                    break;
                case Sex.Female:
                    name = FemaleNames[random.Next(FemaleNames.Length)];
                    break;
                default:
                    return new Person("Default", "Person", 0, Sex.Male);
            }

            string surname = AllSurnames[random.Next(AllSurnames.Length)];

            int age = random.Next(0, Person.AgeMax);

            return new Person(name, surname, age, sex);
        }
    }
}
