using BarberShop_Api.Domain.Models;

namespace BarberShop_Api.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public T Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Post(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T up_entity)
        {
            throw new NotImplementedException();
        }
    }
}
