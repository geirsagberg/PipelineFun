namespace PipelineFun.Web.Extensions
{
    public static class NumberExtensions
    {
        /// <summary>
        ///     Returns true if and only if the number is between the lower and upper bound, inclusive.
        /// </summary>
        public static bool InRange(this int i, int lower, int upper) => i >= lower && i <= upper;
    }
}