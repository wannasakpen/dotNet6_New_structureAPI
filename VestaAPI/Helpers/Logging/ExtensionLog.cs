using System.Runtime.CompilerServices;

namespace VestaAPI.Helpers.Logging
{
    public class ExtensionLog
    {
        private DateTime StartTime;
        public ExtensionLog()
        {
            StartTime = DateTime.Now;
        }

        public static string GetActualMethodName([CallerMemberName] string name = null) => name;

    }
}
