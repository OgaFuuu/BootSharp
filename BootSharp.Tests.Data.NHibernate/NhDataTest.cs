using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BootSharp.Data.Interfaces;
using BootSharp.Data.NHibernate;
using System.Collections.Generic;

namespace BootSharp.Tests.Data.NHibernate
{
    [TestClass]
    public class NhDataTest : DataTest
    {
        [TestMethod]
        public void CanInstanciateContext()
        {
            Assert.IsNotNull(Context);
        }

        [TestMethod]
        public void CanQueryUnitOfWork()
        {
            Assert.IsNotNull(UnitOfWork);

            // Save A
            var a = new A { Name = "a test" };
            var aRepo = UnitOfWork.GetRepository<A>();
            aRepo.Create(a);
            UnitOfWork.Save();

            // Save B referencing A
            var b = new B { Name = "b test", A = a };
            var bRepo = UnitOfWork.GetRepository<B>();
            bRepo.Create(b);
            UnitOfWork.Save();

            // Reload them and check cross reference exists.
            var aFromBase = aRepo.Read(a.Id);
            var bFromBase = bRepo.Read(b.Id);
            Assert.IsNotNull(aFromBase);
            Assert.IsNotNull(bFromBase);
            Assert.IsTrue(aFromBase.BCollection.Contains(bFromBase));
            Assert.IsTrue(bFromBase.A == aFromBase);

            // Save C referencing A
            var c = new C { Name = "c test" };
            if (c.ACollection == null)
                c.ACollection = new List<A>();

            c.ACollection.Add(aFromBase);
            var cRepo = UnitOfWork.GetRepository<C>();
            cRepo.Create(c);
            UnitOfWork.Save();

            // Reload them and check cross reference exists.
            aFromBase = aRepo.Read(a.Id);
            var cFromBase = cRepo.Read(c.Id);
            Assert.IsNotNull(aFromBase);
            Assert.IsNotNull(cFromBase);
            Assert.IsTrue(cFromBase.ACollection.Contains(aFromBase));
            Assert.IsTrue(aFromBase.CCollection.Contains(cFromBase));
        }

        protected override IDataContext CreateContext()
        {
            return new NhDataContextTest();
        }
        protected override IUnitOfWork CreateUnitOfWork()
        {
            return new NhUnitOfWork(Context as NhDataContext);
        }
    }
}
