namespace OG.Zoo.Domain.Entities.Security
{
    using Google.Cloud.Firestore;
    using Infraestructure.Utils.Generics;

    /// <summary>
    /// User class
    /// </summary>
    [FirestoreData]
    public class User : Base
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.B
        /// </value>
        [FirestoreProperty]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [FirestoreProperty]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [FirestoreProperty]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token { get; set; }
    }
}
