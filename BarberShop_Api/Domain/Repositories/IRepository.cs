using System.Globalization;
using System.Reflection;

namespace BarberShop_Api.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        List<T> Get(int id, string column);
        List<T> Get();
        T? GetByClaim();

        void Add(T entity);
        void Delete(int id);
        string UploadArchive(IFormFile file, string doc);
        void Patch(int id, dynamic info, string column);

    }
}
