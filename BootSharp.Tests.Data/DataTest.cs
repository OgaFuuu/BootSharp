using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BootSharp.Data.Interfaces;

namespace BootSharp.Tests.Data
{
    [TestClass]
    public abstract class DataTest
    {
        protected IDataContext Context { get; private set; }
        protected IUnitOfWork UnitOfWork { get; private set; }

        [TestInitialize]
        public void Initialize()
        {
            Context = CreateContext();
            Assert.IsNotNull(Context);

            UnitOfWork = CreateUnitOfWork();
            Assert.IsNotNull(UnitOfWork);

            // Clear datas before starting
            ClearData();
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (UnitOfWork != null)
            {
                UnitOfWork.Dispose();
                UnitOfWork = null;
            }

            if (Context != null)
            {
                Context.Dispose();
                Context = null;
            }
        }

        /// <summary>
        /// Child must return the datacontext at the end of this method.
        /// </summary>
        protected abstract IDataContext CreateContext();

        /// <summary>
        /// Child must return the unit of work at the end of this method.
        /// </summary>
        protected abstract IUnitOfWork CreateUnitOfWork();

        /// <summary>
        /// Clear all the datas in table for <see cref="A"/>, <see cref="B"/> and <see cref="C"/>.
        /// </summary>
        protected void ClearData()
        {
            var repoB = UnitOfWork.GetRepository<B>();
            var listB = repoB.Read();
            repoB.Delete(listB);
            UnitOfWork.Save();

            Context.Command("TRUNCATE TABLE AC");
            UnitOfWork.Save();

            var repoC = UnitOfWork.GetRepository<C>();
            var listC = repoC.Read();
            repoC.Delete(listC);
            UnitOfWork.Save();

            var repoA = UnitOfWork.GetRepository<A>();
            var listA = repoA.Read();
            repoA.Delete(listA);
            UnitOfWork.Save();
        }
    }
}
