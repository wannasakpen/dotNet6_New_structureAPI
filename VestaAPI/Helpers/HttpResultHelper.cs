using VestaAPI.MappingErrors;
using VestaAPI.Model.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using VestaAPI.Extensions;

namespace VestaAPI.Helpers
{
    public class HttpResultHelper
    {

        public static ActionResult CustomResult(ErrorCodes statuscode, Object data)
        {
            return new ResponseResult(data, statuscode);
        }

        public class ResponseResult : JsonResult
        {

            public ResponseResult(object data, ErrorCodes code)
                           : base(data)
            {
                StatusCode = code.GetIntIndex3Digit();
            }
        }

        //public static HttpStatusCode GetHttpStatusCode(string errorCode)
        //{
        //    int code = int.Parse(errorCode.Substring(0, 3));
        //    return (HttpStatusCode)Enum.ToObject(typeof(HttpStatusCode), code);
        //}


        public static IActionResult CustomResult(ErrorCodes errorCodes, ModelStateDictionary modelState)
        {
            List<DetailsModel> Details = null;

            if (modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }
            else
            {
                Details = new List<DetailsModel>();
                Details.AddRange(modelState.Where(w => w.Value.Errors.Count() > 0).Select(
                    kvp => new DetailsModel(kvp.Key, kvp.Value.Errors[0].ErrorMessage)
                    ));
            }


            Response responseBase = new Response<List<DetailsModel>>()
            {
                RespCode = errorCodes,
                Message = errorCodes.GetEnumDescription(),
                Data = Details
            };

            return new HttpActionResult(errorCodes.GetIntIndex3Digit(), Details, responseBase);
        }

        public static IActionResult CustomResult(ErrorCodes errorCodes)
        {
            Response responseBase = new Response<object>()
            {
                RespCode = errorCodes,
                Message = errorCodes.GetEnumDescription(),
            };

            return new HttpActionResult(errorCodes.GetIntIndex3Digit(), responseBase, responseBase);
        }


        public class HttpActionResult : IActionResult
        {
            private readonly int _statuscode;
            public readonly object _data;

            public HttpActionResult(int statuscode, object data, Response responseBase)
            {
                _statuscode = statuscode;
                _data = responseBase;
                //OperationStatus = responseBase ?? throw new ArgumentNullException(nameof(responseBase));
            }

            //public OperationStatus OperationStatus { get; }

            public async Task ExecuteResultAsync(ActionContext context)
            {
                var objectResult = new ObjectResult(_data)
                {
                    StatusCode = _statuscode
                };

                await objectResult.ExecuteResultAsync(context);
            }
        }

        public static string GetExceptionMessage(Exception eMessage, string OrderRefResp)
        {
            StringBuilder message = new StringBuilder();

            message.Append("\r\n");
            message.Append("Exception\r\n");
            message.Append("=======================\r\n");

            message.Append("Message: ");
            message.Append(OrderRefResp);
            message.Append("\r\n");

            message.Append("Message: ");
            if (eMessage.Message != null)
                message.Append(eMessage.Message);
            message.Append("\r\n");

            message.Append("DateTime: ");
            message.Append(DateTime.Now.ToString());
            message.Append("\r\n");

            message.Append("Source: ");
            if (eMessage.Source != null)
                message.Append(eMessage.Source);
            message.Append("\r\n");

            message.Append("TargetSite: ");
            if (eMessage.TargetSite != null)
                message.Append(eMessage.TargetSite.ToString());
            message.Append("\r\n");

            message.Append("Type: ");
            if (eMessage.GetType() != null)
                message.Append(eMessage.GetType().ToString());
            message.Append("\r\n");

            message.Append("StackTrace: ");
            if (eMessage.StackTrace != null)
                message.Append(eMessage.StackTrace);
            message.Append("\r\n");

            message.Append("InnerException: ");
            if (eMessage.InnerException != null)
                message.Append((eMessage.InnerException).ToString());
            message.Append("\r\n");

            message.Append("HelpLink: ");
            if (eMessage.HelpLink != null)
                message.Append(eMessage.HelpLink);
            message.Append("\r\n");

            return message.ToString();
        }

    }
}
