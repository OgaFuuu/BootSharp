using BootSharp.Data.Interfaces;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection;

namespace BootSharp.Data.EntityFramework
{
    public abstract class EfDataContext : DbContext, IDataContext
    {
        protected EfDataContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            // Enable LazyLoading by default
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Enable mapping loading from calling assembly
            var localType = GetType();
            var assembly = Assembly.GetAssembly(localType);
            modelBuilder.Configurations.AddFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public IList<T> Query<T>(string sql, params object[] parameters)
        {
            var listTask = QueryAsync<T>(sql, parameters);
            Task.WaitAll(listTask);

            return listTask.Result;
        }

        public async Task<IList<T>> QueryAsync<T>(string sql, params object[] parameters)
        {
            var sqlQuery = Database.SqlQuery<T>(sql, parameters);
            var listTask = await sqlQuery.ToListAsync();

            return listTask;
        }

        public int Command(string sql, params object[] parameters)
        {
            var listTask = CommandAsync(sql, parameters);
            Task.WaitAll(listTask);

            return listTask.Result;
        }

        public async Task<int> CommandAsync(string sql, params object[] parameters)
        {
            var sqlQuery = Database.ExecuteSqlCommandAsync(sql, parameters);
            var listTask = await sqlQuery;

            return listTask;
        }
    }
}
