namespace BootSharp.Business.Interfaces.Commands
{
    public interface ICanRunResult
    {
        /// <summary>
        /// Indicates wether or not the command can be run.
        /// </summary>
        bool CanRun { get; }

        /// <summary>
        /// Describes <see cref="CanRun"/> reasons.
        /// </summary>
        string Message { get; }
    }
}
