using BootSharp.Business.Interfaces.Commands;

namespace BootSharp.Business.Commands
{
    public abstract class CommandBase<T> : ICommand<T>
    {
        public abstract ICanRunResult CanRun();

        public abstract T Run();

        public virtual void Dispose()
        {
        }
    }
}
