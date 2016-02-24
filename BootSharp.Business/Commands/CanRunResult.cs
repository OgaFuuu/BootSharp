using BootSharp.Business.Interfaces.Commands;

namespace BootSharp.Business.Commands
{
    public class CanRunResult : ICanRunResult
    {
        public bool CanRun { get; protected set; }

        public string Message { get; protected set; }
    }
}
