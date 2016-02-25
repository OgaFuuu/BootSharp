using BootSharp.Business.Commands;
using BootSharp.Business.Commands.Data;
using BootSharp.Business.Commands.Helpers;
using BootSharp.Business.Interfaces;
using BootSharp.Business.Interfaces.Commands;
using BootSharp.Data.Interfaces;
using System;
using System.Text;

namespace BootSharp.Business
{
    public abstract class DataServiceBase : ServiceBase, IDataService
    {
        #region Properties and Constructor

        private IUnitOfWork _unitOfWork;

        public virtual IUnitOfWork UnitOfWork { get { return _unitOfWork; } }

        protected DataServiceBase(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Protected methods

        protected virtual bool TryCreate<T>(T item, out ICanRunResult canRunResult)
            where T : class, IDataObject
        {
            try
            {
                var command = new CreateCommand<T>(UnitOfWork, item);
                canRunResult = new CanRunResult(false);

                return command.TryRun(out canRunResult, out item);
            }
            catch(Exception ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.Source);
                sb.AppendLine(ex.StackTrace);

                canRunResult = new CanRunResult(false, sb.ToString());

                return false;
            }
        }
        protected virtual bool TryRead<T>(long id, out T item, out ICanRunResult canRunResult)
           where T : class, IDataObject
        {
            try
            {
                item = default(T);
                var command = new ReadCommand<T>(UnitOfWork, id);
                canRunResult = new CanRunResult(false);

                return command.TryRun(out canRunResult, out item);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.Source);
                sb.AppendLine(ex.StackTrace);

                item = default(T);
                canRunResult = new CanRunResult(false, sb.ToString());

                return false;
            }
        }
        protected virtual bool TryUpdate<T>(long id, T item, out ICanRunResult canRunResult)
            where T : class, IDataObject
        {
            try
            {
                var command = new UpdateCommand<T>(UnitOfWork, id, item);
                canRunResult = new CanRunResult(false);

                return command.TryRun(out canRunResult, out item);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.Source);
                sb.AppendLine(ex.StackTrace);

                canRunResult = new CanRunResult(false, sb.ToString());

                return false;
            }
        }
        protected virtual bool TryDelete<T>(long id, out ICanRunResult canRunResult)
            where T : class, IDataObject
        {
            try
            {
                var command = new DeleteCommand<T>(UnitOfWork, id);
                canRunResult = new CanRunResult(false);

                var item = default(T);
                return command.TryRun(out canRunResult, out item);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.Source);
                sb.AppendLine(ex.StackTrace);

                canRunResult = new CanRunResult(false, sb.ToString());

                return false;
            }
        }

        #endregion
    }
}
