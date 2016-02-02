using BootSharp.Data.EntityFramework;

namespace BootSharp.Tests.Data.EntityFramework
{
    public class EfDataContextTest : EfDataContext
    {
        private const string ConnectionString = @"Data Source=.\SqlExpress;Initial Catalog=BootSharp;User ID=BootSharp;Password=BootSharp;";

        public EfDataContextTest() : base(ConnectionString)
        {

        }
    }
}
