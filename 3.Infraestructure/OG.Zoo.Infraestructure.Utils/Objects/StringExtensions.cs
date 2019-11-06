namespace OG.Zoo.Infraestructure.Utils.Objects
{
    /// <summary>
    /// String Extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Firsts the letter to upper.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string FirstLetterToUpper(this string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
    }
}
