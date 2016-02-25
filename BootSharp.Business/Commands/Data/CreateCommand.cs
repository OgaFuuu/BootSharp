using BootSharp.Business.Interfaces.Commands;
using BootSharp.Data.Interfaces;

namespace BootSharp.Business.Commands.Data
{
    /// <summary>
    /// Verify basic creation between Business and Data Layer.
    /// </summary>
    public class CreateCommand<T> : DataCommandBase<T>
        where T : class, IDataObject
    {
        private T _item;

        public CreateCommand(IUnitOfWork uow, T item) : base(uow)
        {
            _item = item;
        }

        public override ICanRunResult CanRun()
        {
            if (_item == null || _item.Id > 0)
            {
                return new CanRunResult(false, Properties.Resources.Command_Create_Unable);
            }

            return new CanRunResult();
        }
        public override T Run()
        {
            var repo = _uow.GetRepository<T>();
            repo.Create(_item);

            return _item;
        }
    }
}
