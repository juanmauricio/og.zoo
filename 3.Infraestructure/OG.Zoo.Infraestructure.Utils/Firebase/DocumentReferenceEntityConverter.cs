namespace OG.Zoo.Infraestructure.Utils.Firebase
{
    using Generics;
    using Google.Cloud.Firestore;
    using System;

    /// <summary>
    /// Document Reference Converter
    /// </summary>
    /// <seealso cref="Google.Cloud.Firestore.IFirestoreConverter{System.String}" />
    public class DocumentReferenceEntityConverter<T> : IFirestoreConverter<T> where T : Base
    {
        /// <summary>
        /// Converts a value from its Firestore representation.
        /// </summary>
        /// <param name="value">The value to convert. When called by Google.Cloud.Firestore,
        /// this will never be null.</param>
        /// <returns>
        /// The converted value. Must not be null.
        /// </returns>
        public T FromFirestore(object value)
        {
            var result = ((DocumentReference)value).GetSnapshotAsync().Result;
            var document = result.ConvertTo<T>();
            return document;
        }

        /// <summary>
        /// Converts to firestore.
        /// </summary>
        /// <param name="value">The value to convert. When called by Google.Cloud.Firestore,
        /// this will never be null.</param>
        /// <returns>
        /// The converted value. Must not be null.
        /// </returns>
        public object ToFirestore(T value)
        {
            var id = Environment.GetEnvironmentVariable(FirebaseConstants.GoogleProjectId);
            return FirestoreDb.Create(id).Document(value.Id);
        }
    }
}
