Quick exemple of Either Pattern on c#.

# Either class

``` csharp
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
```

# Usage

``` csharp
static Either<NameAlreadyTaken, Person> CreatePerson(Person person)
{
    /* Here we can se the magic of the inplicity operator:
       We dont need to do 'return new Either<NameAlreadyTaken, Person>(new NameAlreadyTaken())',
       the inplicity operator defined on the Either class do that for us. Its the same as doing:
       Either<NameAlreadyTaken, Person> either = new NameAlreadyTaken(); //For Left returns
       Either<NameAlreadyTaken, Person> either = new Person(); //For Right returns  */

    if (!NameIsUnique(person))
    {
        return new NameAlreadyTaken();
    }

    return person;
}
```
