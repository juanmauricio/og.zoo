namespace OG.Zoo.Domain.Entities.Params
{
    using Google.Cloud.Firestore;
    using Infraestructure.Utils.Generics;
    using Infraestructure.Utils.Firebase;
    using System;

    /// <summary>
    /// Infirmary Chat
    /// </summary>
    [FirestoreData]
    public class Infirmary : Base
    {
        /// <summary>
        /// Gets or sets the identifier animal.
        /// </summary>
        /// <value>
        /// The identifier animal.
        /// </value>
        [FirestoreProperty(ConverterType = typeof(DocumentReferenceConverter))]
        public string IdAnimal { get; set; }

        /// <summary>
        /// Gets or sets the admission date.
        /// </summary>
        /// <value>
        /// The admission date.
        /// </value>
        [FirestoreProperty]
        public DateTime AdmissionDate { get; set; }

        /// <summary>
        /// Gets or sets the diagnosis.
        /// </summary>
        /// <value>
        /// The diagnosis.
        /// </value>
        [FirestoreProperty]
        public string Diagnosis { get; set; }

        /// <summary>
        /// Gets or sets the animal.
        /// </summary>
        /// <value>
        /// The animal.
        /// </value>
        [FirestoreProperty]
        public Animal Animal { get; set; }
    }
}
