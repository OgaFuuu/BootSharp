using BootSharp.Data.NHibernate;
using FluentNHibernate.Cfg.Db;

namespace BootSharp.Tests.Data.NHibernate
{
    public class NhDataContextTest : NhDataContext
    {
        private const string ConnectionString = @"Data Source=.\SqlExpress;Initial Catalog=BootSharp;User ID=BootSharp;Password=BootSharp;";

        public NhDataContextTest() : base(MsSqlConfiguration.MsSql2012.ConnectionString(ConnectionString))
        {

        }
    }
}
