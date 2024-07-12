namespace BarberShop_Api.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        List<T> Get();
        void Post(T entity);
        void Delete(int id);
    }
}
