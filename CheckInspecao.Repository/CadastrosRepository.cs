using System.Threading.Tasks;


namespace CheckInspecao.Repository
{
    public class CadastroRepository : ICadastroRepository
    {
        private readonly BancoContext context;

        public CadastroRepository(BancoContext context) => this.context = context;
        public void Add<T>(T entity) where T : class => this.context.Add(entity);

        public async Task AddAsync<T>(T entity) where T : class => await this.context.AddAsync(entity);

        public void Delete<T>(T entity) where T : class => this.context.Remove(entity);
        public void DeleteRange<T>(T[] entity) where T : class => this.context.RemoveRange(entity);
        public async Task<bool> SaveChangesAsync() => await this.context.SaveChangesAsync() > 0;

        public void Update<T>(T entity) where T : class => this.context.Update(entity);

    }
}