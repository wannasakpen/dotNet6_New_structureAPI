namespace VestaAPI.Utilities.IHeadersUtil
{
    public interface IHeadersUtils
    {
        string GetBodyRequest();
        string GetQueryString();
        HttpRequest GetHttpRequest();
        Guid? GetUserID();
        string GetRoleCode();
        string GetMethod();
        string SetMethod(string Method);
        string RoleCode { get; set; }
        string ControllerName { get; set; }
    }
}
