using BarberShop_Api.Application.Services;
using BarberShop_Api.Domain.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using System.Text;

namespace BarberShop_Api.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ConnectionContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ConnectionContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id) ?? throw new Exception("ID entered is null or no such");


            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public List<T> Get() => _dbSet.ToList();

        public T? GetByClaim()
        {
            var claims = TokenService.GetClaims();

            if(claims == null)
            {
                return null;
            }

            int id = Convert.ToInt32(claims.First(item => item.Type == "Id").Value);

            return _dbSet.Find(id);
        }

        public List<T> Get(int id, string column)
        {
            var idProperty = typeof(T).GetProperties().First(props => props.Name == column);

            if (idProperty == null)
            {
                return new List<T>();
            }

            return _dbSet.ToList().Where(entity =>
            {
                var value = idProperty.GetValue(entity);

                if (value != null && Convert.ToInt32(value) == id)
                {
                    return true;
                }

                return false;
            }).ToList();

        }

        public string UploadArchive(IFormFile file, string doc)
        {
            if (Directory.Exists($"Storage/{doc}/")) {
                Directory.Delete($"Storage/{doc}/", true);
            }
            Directory.CreateDirectory($"Storage/{doc}/");

            string pathPhoto = Path.Combine($"Storage/{doc}/", file.FileName);

            var stream = new FileStream(pathPhoto, FileMode.Create);

            try
            {
                file.CopyTo(stream);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                stream.Close();
            }
            
            return pathPhoto;
        }

        public void Patch(int id, dynamic info, string column)
        {
            var entity = _dbSet.Find(id) ?? throw new Exception("ID entered is null or no such");

            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.Name == column)
                {
                    prop.SetValue(entity, info);
                    _context.SaveChanges();
                }
            }            
        }



    }
}
