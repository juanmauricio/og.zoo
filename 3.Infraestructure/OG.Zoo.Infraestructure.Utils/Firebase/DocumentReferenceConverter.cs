namespace OG.Zoo.Infraestructure.Utils.Firebase
{
    using Google.Cloud.Firestore;
    using System;

    /// <summary>
    /// Document Reference Converter
    /// </summary>
    /// <seealso cref="Google.Cloud.Firestore.IFirestoreConverter{System.String}" />
    public class DocumentReferenceConverter : IFirestoreConverter<string>
    {
        /// <summary>
        /// Converts a value from its Firestore representation.
        /// </summary>
        /// <param name="value">The value to convert. When called by Google.Cloud.Firestore,
        /// this will never be null.</param>
        /// <returns>
        /// The converted value. Must not be null.
        /// </returns>
        public string FromFirestore(object value)
        {
            var document = ((DocumentReference)value);
            var parts = document.Path.Split("/");
            return $"{parts[parts.Length - 2]}/{parts[parts.Length - 1]}";
        }

        /// <summary>
        /// Converts to firestore.
        /// </summary>
        /// <param name="value">The value to convert. When called by Google.Cloud.Firestore,
        /// this will never be null.</param>
        /// <returns>
        /// The converted value. Must not be null.
        /// </returns>
        public object ToFirestore(string value)
        {
            var id = Environment.GetEnvironmentVariable(FirebaseConstants.GoogleProjectId);
            return FirestoreDb.Create(id).Document(value);
        }
    }
}
