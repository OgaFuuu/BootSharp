using Microsoft.VisualStudio.TestTools.UnitTesting;
using BootSharp.Data.Interfaces;
using System.Collections.Generic;

namespace BootSharp.Tests.Data
{
    [TestClass]
    public abstract class DataTest
    {
        [TestInitialize]
        public void Initialize()
        {
            // Clear datas before starting
            ClearData();
        }

        [TestCleanup]
        public void Cleanup()
        {
        }

        [TestMethod]
        protected void DataContextCanInstantiate()
        {
            using (var context = CreateContext())
            {
                Assert.IsNotNull(context);
            }
        }

        [TestMethod]
        protected void UnitOfWorkPersistencyIsCorrect()
        {
            #region SAVE A

            long aId = 0;
            using (var context = CreateContext())
            {
                Assert.IsNotNull(context);

                using (var unitOfWork = CreateUnitOfWork(context))
                {
                    Assert.IsNotNull(unitOfWork);

                    var a = new A { Name = "a test" };
                    var aRepo = unitOfWork.GetRepository<A>();
                    aRepo.Create(a);
                    unitOfWork.Save();

                    aId = a.Id;
                }
            }
            Assert.IsTrue(aId > 0);

            #endregion

            #region SAVE B REFERENCING A

            long bId = 0;
            using (var context = CreateContext())
            {
                Assert.IsNotNull(context);

                using (var unitOfWork = CreateUnitOfWork(context))
                {
                    Assert.IsNotNull(unitOfWork);

                    var aRepo = unitOfWork.GetRepository<A>();
                    var a = aRepo.Read(aId);
                    Assert.IsNotNull(a);

                    var b = new B { Name = "b test", A = a };
                    var bRepo = unitOfWork.GetRepository<B>();
                    bRepo.Create(b);
                    unitOfWork.Save();

                    bId = b.Id;
                }
            }
            Assert.IsTrue(bId > 0);

            #endregion

            #region RELOAD AND CHECK CROSS REFS

            using (var context = CreateContext())
            {
                Assert.IsNotNull(context);

                using (var unitOfWork = CreateUnitOfWork(context))
                {
                    Assert.IsNotNull(unitOfWork);

                    var aRepo = unitOfWork.GetRepository<A>();
                    var bRepo = unitOfWork.GetRepository<B>();
                    var a = aRepo.Read(aId);
                    var b = bRepo.Read(bId);

                    Assert.IsNotNull(a);
                    Assert.IsNotNull(b);
                    Assert.IsTrue(a.BCollection.Contains(b));
                    Assert.IsTrue(b.A == a);
                }
            }

            #endregion

            #region SAVE C REFERENCING A

            long cId = 0;
            using (var context = CreateContext())
            {
                Assert.IsNotNull(context);

                using (var unitOfWork = CreateUnitOfWork(context))
                {
                    Assert.IsNotNull(unitOfWork);

                    var aRepo = unitOfWork.GetRepository<A>();
                    var a = aRepo.Read(aId);
                    Assert.IsNotNull(a);

                    var c = new C { Name = "c test" };
                    if (c.ACollection == null)
                        c.ACollection = new List<A>();

                    c.ACollection.Add(a);
                    var cRepo = unitOfWork.GetRepository<C>();
                    cRepo.Create(c);
                    unitOfWork.Save();

                    cId = c.Id;
                }
            }
            Assert.IsTrue(cId > 0);

            #endregion

            #region RELOAD AND CHECK CROSS REFS

            using (var context = CreateContext())
            {
                Assert.IsNotNull(context);

                using (var unitOfWork = CreateUnitOfWork(context))
                {
                    Assert.IsNotNull(unitOfWork);

                    var aRepo = unitOfWork.GetRepository<A>();
                    var a = aRepo.Read(aId);
                    Assert.IsNotNull(a);

                    var cRepo = unitOfWork.GetRepository<C>();
                    var c = cRepo.Read(cId);
                    Assert.IsNotNull(c);

                    Assert.IsTrue(c.ACollection.Contains(a));
                    Assert.IsTrue(a.CCollection.Contains(c));
                }
            }

            #endregion
        }

        /// <summary>
        /// Child must return the datacontext at the end of this method.
        /// </summary>
        protected abstract IDataContext CreateContext();

        /// <summary>
        /// Child must return the unit of work at the end of this method.
        /// </summary>
        protected virtual IUnitOfWork CreateUnitOfWork(IDataContext dataContext)
        {
            return dataContext.CreateUnitOfWork();
        }

        /// <summary>
        /// Clear all the datas in table for <see cref="A"/>, <see cref="B"/> and <see cref="C"/>.
        /// </summary>
        protected void ClearData()
        {
            using (var context = CreateContext())
            {
                using (var unitOfWork = CreateUnitOfWork(context))
                {
                    // DELETE B
                    var repoB = unitOfWork.GetRepository<B>();
                    var listB = repoB.Read();
                    repoB.Delete(listB);
                    unitOfWork.Save();

                    // DELETE AC
                    context.Command("TRUNCATE TABLE AC");
                    unitOfWork.Save();

                    // DELETE C
                    var repoC = unitOfWork.GetRepository<C>();
                    var listC = repoC.Read();
                    repoC.Delete(listC);
                    unitOfWork.Save();

                    // DELETE A
                    var repoA = unitOfWork.GetRepository<A>();
                    var listA = repoA.Read();
                    repoA.Delete(listA);
                    unitOfWork.Save();
                }
            }                
        }
    }
}
