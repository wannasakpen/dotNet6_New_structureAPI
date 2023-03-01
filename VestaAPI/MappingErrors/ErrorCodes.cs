using System.ComponentModel;

namespace VestaAPI.MappingErrors
{
    public enum ErrorCodes : int
    {
        [Description("Success")]
        Success = 20000,

        [Description("Success Accepted")]
        SuccessAccepted = 20002,

        [Description("Data not found")]
        DataNotFound = 40400,

        [Description("Internal Server Error")]
        InternalServerError = 50000,

        [Description("Forbidden")]
        Forbidden = 40300,

        [Description("Data Conflict")]
        Conflict = 40900, 

        [Description("Unauthorized")]
        Unauthorized = 40100,

        #region 400 Bad Request
        [Description("The client requested missing or invalid  format.")]
        BadRequestInvalid = 40000,

        [Description("The client requested Bad Request.")]
        BadRequestWithNoBody = 40010,
        #endregion 400 Bad Request




        [Description("An unknown error has occurred.")]
        UnknowError = 50099
    }
}
