using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonLib
{
    /// <summary>
    /// Формирование случайного человека
    /// </summary>
    public class GetRandomPerson
    {
        /// <summary>
        /// Объект класса Random
        /// </summary>
        private static Random _randNum = new Random(DateTime.Now.Second);

        /// <summary>
        /// Мужские имена
        /// </summary>
        private static string[] _maleNames = new string[]
        {
            "Mickey", "Bugs", "Darth", "James",
             "Peter", "Harry", "Vito",
             "Homer", "Hannibal", "Tony", "Willy"
        };

        /// <summary>
        /// Женские имена
        /// </summary>
        private static string[] _femaleNames = new string[]
        {
            "Mulan", "Sarah", "Katniss", "Belle",
            "Maleficent", "Rapunzel", "Moana",
            "Bellatrix", "Fiona", "Alice", "Mystique"
        };

        /// <summary>
        /// Фамилии
        /// </summary>
        private static string[] _allSurnames = new string[]
        {
            "Mouse", "Bunny", "Vader", "Bond",
            "Pan", "Potter", "Corleone",
            "Simpson", "Lector", "Soprano", "Wonka"
        };

        /// <summary>
        /// Генерация случайного человека: взрослый или ребенок
        /// </summary>
        public static PersonBase CreateRandomPerson()
        {
            if (_randNum.Next(0, 2) != 0)
            {
                return CreateRandomChild();
            }
            else
            {
                return CreateRandomAdult();
            }
        }

        /// <summary>
        /// Заполнение базовых полей человека
        /// </summary>
        /// <param name="person">человек</param>
        public static void RandomPerson(PersonBase person)
        {
            var sex = _randNum.Next(0, 2);
            switch (sex)
            {
                case 0:
                    {
                        person.Sex = Sex.Male;
                        person.Name = _maleNames[_randNum.Next(_maleNames.Length)];
                        break;
                    }
                case 1:
                    {
                        person.Sex = Sex.Female;
                        person.Name = _femaleNames[_randNum.Next(_femaleNames.Length)];
                        break;
                    }
            }
            person.Surname = _allSurnames[_randNum.Next(_allSurnames.Length)];
        }

        
        private static void GetPasportData(Adult adult)
        {
            var _passport = _randNum.Next(100000000, 999999999).ToString();
            adult.Passport = _passport;
        }

        /// <summary>
        /// Взрослый человек
        /// </summary>
        /// <param name="married">генерирует супруга</param>
        /// <param name="partner">Супруг</param>
        /// <returns></returns>
        public static Adult CreateRandomAdult(bool married = false, Adult partner = null)
        {
            var randomAdult = new Adult();
            RandomPerson(randomAdult);
            randomAdult.Age = _randNum.Next(Adult.MinAdultAge, Adult.MaxAdultAge);
            if (!married)
            {
                randomAdult.MaritalStatus = (MaritalStatus)_randNum.Next(0, 4);
                if (randomAdult.MaritalStatus == MaritalStatus.Married)
                {
                    randomAdult.Partner = CreateRandomAdult(true, randomAdult);
                }
            }
            else
            {
                randomAdult.MaritalStatus = MaritalStatus.Married;
                randomAdult.Partner = partner;
            }
            string[] jobs = new string[]
            {
                "OZON", "WB", "Gold Apple",
                "Lamoda", "Rostics", "Magnit",
                "Novex", "VK", "TPU"
            };

            randomAdult.Job = jobs[_randNum.Next(0, jobs.Length)];
            GetPasportData(randomAdult);
            return randomAdult;
        }

        /// <summary>
        /// Ребенок
        /// </summary>
        /// <returns>рандомный ребенок</returns>
        public static Child CreateRandomChild()
        {
            Child randomChild = new Child();
            RandomPerson(randomChild);
            randomChild.Age = _randNum.Next(Child.MinChildAge, Child.MaxChildAge);

            bool hasMother = _randNum.Next(0, 2) != 0;

            if (hasMother)
            {
                randomChild.ParentOne = CreateRandomAdult();
            }

            bool hasFather = _randNum.Next(0, 2) != 0;

            if (hasFather)
            {
                randomChild.ParentTwo = CreateRandomAdult();
            }

            string[] schools = new string[]
            {
                "#1", "#12",
                "Hogwarts", "#100", "#88",
                "Xavier's School", "56","667"
            };

            randomChild.School = schools[_randNum.Next(0, schools.Length)];

            return randomChild;
        }
    }
}