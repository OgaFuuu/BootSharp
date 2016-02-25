using BootSharp.Business.Interfaces.Commands;

namespace BootSharp.Business.Commands
{
    public class CanRunResult : ICanRunResult
    {
        public CanRunResult(bool canRun = true, string message = null)
        {
            CanRun = canRun;
            Message = message;
        }

        public bool CanRun { get; protected set; }

        public string Message { get; protected set; }
    }
}
