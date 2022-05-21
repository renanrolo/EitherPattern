#nullable enable

using System;

namespace EitherPatternConsole
{
    class Program
    {
        static void Main()
        {
            foreach (var name in new string[] { "Roald", "Grid", "Reidar", "Skuld", "Brandr" })
            {
                TryCreatePerson(name);
            }

            Console.ReadKey();
        }

        static void TryCreatePerson(string name)
        {
            var personToCreate = new Person(name);

            var result = CreatePerson(personToCreate);

            if (result.IsRight)
            {
                Console.WriteLine($"Person '{result.Right.Name}' created successfully");
                return;
            }

            Console.WriteLine($"Person was not created: {result.Left.Error}");
        }

        static Either<NameAlreadyTaken, Person> CreatePerson(Person person)
        {
            /* Here we can se the magic of the inplicity operator:
             * We dont need to do 'return new Either<NameAlreadyTaken, Person>(new NameAlreadyTaken())' on line 47,
             * the inplicity operator on line 73/74 do that for us. Its the same as doing:
             * Either<NameAlreadyTaken, Person> either = new NameAlreadyTaken(); //TLeft
             * Either<NameAlreadyTaken, Person> either = new Person(); //TRight
             */
            var random = new Random();
            var result = random.Next(0, 4);

            if (result <= 1)
            {
                return new NameAlreadyTaken();
            }

            return person;
        }
    }

    public class Either<TLeft, TRight>
    {
        public TLeft? Left { get; private set; }
        public TRight? Right { get; private set; }

        public bool IsRight { get; private set; }

        public Either(TLeft left)
        {
            IsRight = false;
            Left = left;
        }

        public Either(TRight right)
        {
            IsRight = true;
            Right = right;
        }

        public static implicit operator Either<TLeft, TRight>(TLeft left) => new(left);
        public static implicit operator Either<TLeft, TRight>(TRight right) => new(right);
    }
}
