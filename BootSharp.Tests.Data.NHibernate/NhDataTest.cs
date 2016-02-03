using Microsoft.VisualStudio.TestTools.UnitTesting;
using BootSharp.Data.Interfaces;
using BootSharp.Data.NHibernate;

namespace BootSharp.Tests.Data.NHibernate
{
    [TestClass]
    public class NhDataTest : DataTest
    {
        [TestMethod]
        public void TestContext()
        {
            DataContextCanInstantiate();
        }

        [TestMethod]
        public void TestUnitOfWork()
        {
            UnitOfWorkPersistencyIsCorrect();
        }

        protected override IDataContext CreateContext()
        {
            return new NhDataContextTest();
        }
    }
}
