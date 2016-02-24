using BootSharp.Business.Interfaces.Commands;

namespace BootSharp.Business.Commands
{
    public abstract class CommandBase : ICommand
    {
        public abstract ICanRunResult CanRun(params object[] args);

        public abstract void Run(params object[] args);

        public virtual void Dispose()
        {
        }

    }
}
