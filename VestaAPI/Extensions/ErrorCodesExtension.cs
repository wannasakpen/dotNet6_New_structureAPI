using VestaAPI.MappingErrors;
using System.ComponentModel;
using System.Net;
using System.Reflection;

namespace VestaAPI.Extensions
{
    public static class ErrorCodesExtension
    {
        public static string GetEnumDescription(this ErrorCodes value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        public static string GetEnumDescription(this ErrorCodes value, string customerror)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                string description = string.Format(attributes.First().Description.ToString(), customerror);
                return description;
            }

            return value.ToString();

        }

        public static HttpStatusCode GetHttpStatusCode(this ErrorCodes enumCode)
        {
            HttpStatusCode Code = new HttpStatusCode();
            foreach (HttpStatusCode o in System.Enum.GetValues(typeof(HttpStatusCode)))
            {
                if (((int)o).Equals((enumCode.GetIntIndex3Digit())))
                    Code = o;
            }
            return Code;
        }

        public static int GetIntIndex3Digit(this ErrorCodes eValue)
        {
            return Convert.ToInt32(((int)eValue).ToString().Substring(0, 3));
        }

        public static bool GetSuccessFlag(this ErrorCodes enumError)
        {
            if ((int)enumError >= 20000 && (int)enumError < 30000)
                return true;
            return false;
        }

        public static string GetErrorCode(this ErrorCodes enumError)
        {
            return ((int)enumError).ToString();
        }

        public static string GetErrorName(this ErrorCodes enumError)
        {
            return System.Enum.GetName(typeof(ErrorCodes), enumError);
        }
    }
}
