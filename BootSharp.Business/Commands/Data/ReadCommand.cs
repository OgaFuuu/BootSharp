using BootSharp.Business.Interfaces.Commands;
using BootSharp.Data.Interfaces;

namespace BootSharp.Business.Commands.Data
{
    /// <summary>
    /// Verify basic creation between Business and Data Layer.
    /// </summary>
    public class ReadCommand<T> : DataCommandBase<T>
        where T : class, IDataObject
    {
        private long _id;

        public ReadCommand(IUnitOfWork uow, long id) : base(uow)
        {
            _id = id;
        }

        public override ICanRunResult CanRun()
        {
            if (_id <= 0)
            {
                return new CanRunResult(false, Properties.Resources.Command_Read_Unable);
            }

            return new CanRunResult();
        }
        public override T Run()
        {
            var repo = _uow.GetRepository<T>();
            var item = repo.Read(_id); // TODO check re-attach exists.

            return item;
        }
    }
}
