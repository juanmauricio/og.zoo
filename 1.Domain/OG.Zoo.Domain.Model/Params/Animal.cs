namespace OG.Zoo.Domain.Entities.Params
{
    using Google.Cloud.Firestore;
    using Infraestructure.Utils.Generics;

    /// <summary>
    /// Animal class
    /// </summary>
    [FirestoreData]
    public class Animal : Base
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [FirestoreProperty]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        [FirestoreProperty]
        public long Age { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [FirestoreProperty]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the species.
        /// </summary>
        /// <value>
        /// The species.
        /// </value>
        [FirestoreProperty]
        public string Species { get; set; }

        /// <summary>
        /// Gets or sets the subspecies.
        /// </summary>
        /// <value>
        /// The subspecies.
        /// </value>
        [FirestoreProperty]
        public string Subspecies { get; set; }

        /// <summary>
        /// Gets or sets the eating habits.
        /// </summary>
        /// <value>
        /// The eating habits.
        /// </value>
        [FirestoreProperty]
        public string EatingHabits { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [FirestoreProperty]
        public string Type { get; set; }

    }
}
