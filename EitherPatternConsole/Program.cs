using System;
using System.Linq;

namespace EitherPatternConsole
{
    class Program
    {
        private static readonly string[] _existingNames = new string[] { "Grid", "Skuld" };
        private static readonly string[] _namesToAdd = new string[] { "Roald", "Grid", "Reidar", "Skuld", "Brandr" };

        static void Main()
        {
            foreach (var name in _namesToAdd)
            {
                TryCreatePerson(name);
            }

            Console.ReadKey();
        }

        static void TryCreatePerson(string name)
        {
            var personToCreate = new Person(name);

            var result = CreatePerson(personToCreate);

            var consolePrefix = name.PadRight(10, ' ');

            if (result.IsRight)
            {
                
                Console.WriteLine($"{consolePrefix}>  Created successfully");
                return;
            }

            Console.WriteLine($"{consolePrefix}>  Error when creating person, reason: {result.Left.Error}");
        }

        static Either<NameAlreadyTaken, Person> CreatePerson(Person person)
        {
            /* Here we can se the magic of the inplicity operator:
             * We dont need to do 'return new Either<NameAlreadyTaken, Person>(new NameAlreadyTaken())' on line 46,
             * the inplicity operator defined on the Either class do that for us. Its the same as doing:
             * Either<NameAlreadyTaken, Person> either = new NameAlreadyTaken(); //TLeft
             * Either<NameAlreadyTaken, Person> either = new Person(); //TRight
             */
            if (_existingNames.Contains(person.Name))
            {
                return new NameAlreadyTaken();
            }

            return person;
        }
    }
}
