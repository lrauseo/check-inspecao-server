using System.Threading.Tasks;


namespace CheckInspecao.Repository
{
    public interface ICadastroRepository
    {
         //Geral
         void Add<T>(T entity)where T:class;
         void Update<T>(T entity)where T:class;
         void Delete<T>(T entity)where T:class;

         Task AddAsync<T>(T entity)where T:class;

         Task<bool> SaveChangesAsync();
        void DeleteRange<T>(T[] entity) where T : class;
    }
}