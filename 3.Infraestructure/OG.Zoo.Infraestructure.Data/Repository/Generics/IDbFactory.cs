namespace OG.Zoo.Infraestructure.Data.Repository.Generics
{
    using Google.Cloud.Firestore;

    public interface IDbFactory
    {
        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        FirestoreDb GetDb();
    }
}
