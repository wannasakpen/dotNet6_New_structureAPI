namespace VestaAPI.Utilities
{
    public class ControllerHeader
    {
        public static string ControllerName;
        public static string CommandName;

        public static void setControllerName(string name)
        {
            ControllerName = name;
        }

        public static void setCommandName(string name)
        {
            CommandName = name;
        }
    }
}
