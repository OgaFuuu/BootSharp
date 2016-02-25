using BootSharp.Business.Interfaces.Commands;
using BootSharp.Data.Interfaces;
using System.Linq;

namespace BootSharp.Business.Commands.Data
{
    /// <summary>
    /// Verify basic creation between Business and Data Layer.
    /// </summary>
    public class DeleteCommand<T> : DataCommandBase<T>
        where T : class, IDataObject
    {
        private long _id;

        public DeleteCommand(IUnitOfWork uow, long id) : base(uow)
        {
            _id = id;
        }

        public override ICanRunResult CanRun()
        {
            var repo = _uow.GetRepository<T>();
            if (_id <= 0 || repo.Query(e => e.Id == _id).Count() == 0)
            {
                return new CanRunResult(false, Properties.Resources.Command_Delete_Unable);
            }

            return new CanRunResult();
        }
        public override T Run()
        {
            var repo = _uow.GetRepository<T>();
            var _item = repo.Read(_id);
            repo.Delete(_item); // TODO check re-attach exists.

            return null;
        }
    }
}
