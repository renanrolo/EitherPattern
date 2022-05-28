namespace EitherPatternConsole
{
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
