using System.Runtime.CompilerServices;

namespace LearnCSharp.Examples
{
    public static class ArgExpression
    {
        #region CallerArgumentExpression

        /* CallerArgumentExpression attribute enables you to write diagnostic utilities that provide more details.
         * Developers can more quickly understand what changes are needed.
         * You can also use the CallerArgumentExpressionAttribute to determine what expression was used as the receiver for extension methods. <summary>
         * CallerArgumentExpression attribute enables you to write diagnostic utilities that provide more details.
         */

        private static void ValidateArgument(string parameterName, bool condition, [CallerArgumentExpression("parameterName")] string? message = null)
        {
            if (!condition)
            {
                throw new ArgumentException($"Argument failed validation: <{message}>", parameterName);
            }
        }

        private static void Operation(Action func)
        {
            ValidateArgument(nameof(func), func is not null);
            func();
        }

        public static void Sample<T>(this IEnumerable<T> sequence, int frequency, [CallerArgumentExpression(nameof(sequence))] string? message = null)
        {
            if (sequence.Count() < frequency)
                throw new ArgumentException($"Expression doesn't have enough elements: {message}", nameof(sequence));
        }

        public static void Example_1()
        {
            var PrintText = () => Console.WriteLine("Hello world!!!");
            Operation(PrintText);
        }

        public static void Example_2()
        {
            new List<string> { "VietNam", "Korea", "Japanse", "China" }.Sample(1);
        }

        #endregion
    }
}