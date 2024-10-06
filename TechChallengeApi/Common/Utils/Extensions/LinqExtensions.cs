namespace Common.Utils.Extensions
{
    public static class LinqExtensions
    {
        public static bool NotNullOrEmpty<T>(this IEnumerable<T>? source)
        {
            return source is not null && source.Any();
        }

        public static bool NotNullOrEmpty<T>(this IList<T>? source)
        {
            return source is not null && source.Any();
        }

        public static bool NotNullOrEmpty<T>(this List<T>? source)
        {
            return source is not null && source.Any();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? enumerable)
        {
            if (enumerable is not null)
            {
                return !enumerable.Any();
            }

            return true;
        }

        public static bool IsNullOrEmpty<T>(this IList<T> enumerable)
        {
            if (enumerable is not null)
            {
                return !enumerable.Any();
            }

            return true;
        }

        public static bool IsNullOrEmpty<T>(this List<T> enumerable)
        {
            if (enumerable is not null)
            {
                return !enumerable.Any();
            }

            return true;
        }
    }
}
