using VestaAPI.MappingErrors;
using VestaAPI.Model.ResponseModels;
using VestaAPI.Utilities.IHeadersUtil;
using VestaAPI.Utilities;
using Microsoft.AspNetCore.Mvc.Filters;
using VestaAPI.Helpers.Logging;
using VestaAPI.Helpers;

namespace VestaAPI.ActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        private readonly IHeadersUtils _headersUtil;
        private readonly ILogger _logger;
        public ValidateModelAttribute(ILogger<ValidateModelAttribute> logger, IHeadersUtils headersUtil)
        {
            _logger = logger;
            _headersUtil = headersUtil;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                switch (context.RouteData.Values["Action"])
                {
                    case "Post":
                        ControllerHeader.setCommandName("Create");
                        break;
                    case "Put":
                        ControllerHeader.setCommandName("Update");
                        break;
                    case "Delete":
                        ControllerHeader.setCommandName("Delete");
                        break;
                    default:
                        break;
                }
                LogModel logModel = new LogModel(ControllerHeader.CommandName, null);

                Response result = null;

                Dictionary<string, string> errorList = context.ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
                    );

                result = new Response<Dictionary<string, string>>()
                {
                    RespCode = ErrorCodes.Forbidden,
                    Data = errorList
                };


                context.Result = HttpResultHelper.CustomResult(result.RespCode, result);
                logModel.ResponseData = result;
                //new LogService(_logger, _headersUtil, logModel);

            }
            return base.OnActionExecutionAsync(context, next);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = HttpResultHelper.CustomResult(ErrorCodes.BadRequestInvalid, context.ModelState);
            }
        }

        //#region private
        //public class CustomValidateModelStateResult : JsonResult
        //{
        //    public CustomValidateModelStateResult(Dictionary<string, string> data)
        //        : base(new Response<Dictionary<string, string>>()
        //        {
        //            RespCode = 40300,
        //            Data = data
        //        })
        //    {
        //        StatusCode = StatusCodes.Status403Forbidden;
        //    }
        //}
        //#endregion
    }
}
