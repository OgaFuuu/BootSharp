using System;

namespace BootSharp.Business.Interfaces.Commands
{
    /// <summary>
    /// Base Command interface.
    /// </summary>
    public interface ICommand<T> : IDisposable
    {
        /// <summary>
        /// Ensure the command can be run properly.
        /// </summary>
        ICanRunResult CanRun();

        /// <summary>
        /// Run the Command.
        /// </summary>
        T Run();
    }
}
