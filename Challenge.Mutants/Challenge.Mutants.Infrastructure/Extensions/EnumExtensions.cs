using System;

namespace Challenge.Mutants.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static T FromInt<T>(int value)
            => (T)Enum.GetValues(typeof(T)).GetValue(value);

        public static T FromString<T>(string value)
            => (T)Enum.Parse(typeof(T), value);
    }
}
