namespace OG.Zoo.Infraestructure.Data.Repository.Generics
{
    using Google.Cloud.Firestore;
    using System;
    using Utils.Firebase;

    /// <summary>
    /// Db Factory
    /// </summary>
    /// <seealso cref="OG.Zoo.Infraestructure.Data.Repository.Generics.IDbFactory" />
    public class DbFactory : IDbFactory
    {
        private FirestoreDb db;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbFactory"/> class.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="credentials">The credentials.</param>
        public DbFactory()
        {
        }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public FirestoreDb GetDb()
        {
            if (this.db == null)
            {
                this.db = FirestoreDb.Create(Environment.GetEnvironmentVariable(FirebaseConstants.GoogleProjectId));
            }
            return this.db;
        }
    }
}
