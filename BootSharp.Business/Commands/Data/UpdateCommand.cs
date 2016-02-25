using BootSharp.Business.Interfaces.Commands;
using BootSharp.Data.Interfaces;

namespace BootSharp.Business.Commands.Data
{
    /// <summary>
    /// Verify basic creation between Business and Data Layer.
    /// </summary>
    public class UpdateCommand<T> : DataCommandBase<T>
        where T : class, IDataObject
    {
        private long _id;
        private T _item;

        public UpdateCommand(IUnitOfWork uow, long id, T item) : base(uow)
        {
            _id = id;
            _item = item;
        }

        public override ICanRunResult CanRun()
        {
            if (_item == null || _item.Id <= 0 || _item.Id != _id)
            {
                return new CanRunResult(false, Properties.Resources.Command_Update_Unable);
            }

            return new CanRunResult();
        }
        public override T Run()
        {
            var repo = _uow.GetRepository<T>();
            repo.Update(_item); // TODO check re-attach exists.

            return _item;
        }
    }
}
