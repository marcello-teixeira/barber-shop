namespace BarberShop_Api.Domain.Models
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        void Post(T entity);
        void Update(T up_entity);
        void Delete(int id);
    }
}
