namespace VestaAPI.Helpers.Logging.Interface
{
    public interface ILogModel
    {
        bool SetRequestData(string command, string orderRef, object requestObject);
        bool SetRequestData(string command, string orderRef, RequestObjectModel requestObject);
        string Command { get; set; }
        string Status { get; set; }
        string OrderRef { get; set; }
        object RequestObject { get; set; }
        object ResponseObject { get; set; }
        ActivityLogModel ActivityLog { get; set; }
        List<EndPointsModel> EndPoint { get; set; }
        LogExceptionModel ExceptionMessage { get; set; }
        ActivityLogModel GetActivityLog();
    }
}
