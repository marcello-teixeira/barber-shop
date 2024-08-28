using System.Globalization;
using System.Reflection;

namespace BarberShop_Api.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>Return all entities</returns>
        List<T> Get();

        /// <summary>
        /// Get all entity referecing a foreign key
        /// </summary>
        /// <param name="foreignKey">Foreign key of the entity</param>
        /// <param name="column">Column where the foreing keys are</param>
        /// <returns>If true return entities, if isn't return null </returns>
        List<T> Get(int foreignKey, string column);

        /// <summary>
        /// A method that returns all claims
        /// </summary>
        /// <returns>If there are claim return them, if aren't return null</returns>
        T? GetByClaim();

        /// <summary>
        /// Add a entity to database
        /// </summary>
        /// <param name="entity">A class of entity model</param>
        void Add(T entity);

        /// <summary>
        /// Delete a entity in database
        /// </summary>
        void Delete(int id);

        /// <summary>
        /// Upload a image in storage
        /// <param name="file">An image that wiil be uploaded</param>
        /// <param name="doc">A document that will be used as a path</param>
        /// </summary>
        string UploadArchive(IFormFile file, string doc);

        /// <summary>
        /// Update a column value
        /// </summary>
        /// <param name="info">Value for be update</param>
        /// <param name="column">Column that will be change</param>
        void Patch(int id, dynamic info, string column);

    }
}
