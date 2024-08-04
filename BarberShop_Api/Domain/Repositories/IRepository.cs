using System.Globalization;

namespace BarberShop_Api.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        List<T> Get();
        void Add(T entity);
        void Delete(int id);
        public string UploadArchive(IFormFile file, string doc);

    }
}
