using BootSharp.Business.Interfaces.Commands;
using System;
using System.Text;

namespace BootSharp.Business.Commands.Helpers
{
    public static class CommandHelper
    {
        public static bool TryRun<T>(this ICommand<T> command, out ICanRunResult result, out T runResult)
        {
            var run = false;
            try
            {
                result = command.CanRun();
                runResult = default(T);
                if (result.CanRun)
                {
                    runResult = command.Run();
                    run = true;
                }
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();

                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.Source);
                sb.AppendLine(ex.StackTrace);

                result = new CanRunResult(false, sb.ToString());
                runResult = default(T);
                return false;
            }

            return run;
        }
    }
}
