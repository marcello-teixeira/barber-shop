using System.Globalization;
using System.Reflection;

namespace BarberShop_Api.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        List<T> Get(int id, string column);
        T? Get(int id);
        List<T> Get();

        void Add(T entity);
        void Delete(int id);
        string UploadArchive(IFormFile file, string doc);
        void Patch(int id, dynamic info, string column);

    }
}
