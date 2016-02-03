using Microsoft.VisualStudio.TestTools.UnitTesting;
using BootSharp.Data.EntityFramework;
using BootSharp.Data.Interfaces;

namespace BootSharp.Tests.Data.EntityFramework
{
    [TestClass]
    public class EfDataTest : DataTest
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
            return new EfDataContextTest();
        }
        protected override IUnitOfWork CreateUnitOfWork(IDataContext context)
        {
            return new EfUnitOfWork(context as EfDataContext);
        }
    }
}
