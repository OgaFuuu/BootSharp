using System;

namespace BootSharp.Business.Interfaces.Commands
{
    /// <summary>
    /// Base Command interface.
    /// </summary>
    public interface ICommand : IDisposable
    {
        /// <summary>
        /// Ensure the command can be run properly.
        /// </summary>
        /// <returns></returns>
        ICanRunResult CanRun(params object[] args);

        void Run(params object[] args);
    }
}
