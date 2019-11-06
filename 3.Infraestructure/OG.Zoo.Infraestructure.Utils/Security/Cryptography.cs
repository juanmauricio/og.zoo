namespace OG.Zoo.Infraestructure.Utils.Security
{
    using Isopoh.Cryptography.Argon2;

    /// <summary>
    /// Cryptography Class
    /// </summary>
    public static class Cryptography
    {
        /// <summary>
        /// Gets the hash.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string GetHash(string text)
        {
            return Argon2.Hash(text);
        }

        /// <summary>
        /// Validates the specified hash.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static bool Validate(string hash, string text)
        {
            return Argon2.Verify(hash, text);
        }
    }
}
