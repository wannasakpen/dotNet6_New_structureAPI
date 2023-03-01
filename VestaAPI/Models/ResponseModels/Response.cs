using VestaAPI.MappingErrors;
using Newtonsoft.Json;
using System.Security.Cryptography.Xml;

namespace VestaAPI.Model.ResponseModels
{
    public class Response<T> : Response
    {

        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonProperty(Order = 2)]
        public string Message { get; set; }
        [JsonProperty(Order = 3)]
        public T Data { get; set; }


    }

    public abstract class Response
    {
        [JsonProperty(Order = 1)]
        public ErrorCodes RespCode { get; set; }

    }


    public class DetailsModel
    {
        public DetailsModel(string target, string developerMessage)
        {
            if (string.IsNullOrWhiteSpace(target))
            {
                throw new ArgumentException("message", nameof(target));
            }

            if (string.IsNullOrWhiteSpace(developerMessage))
            {
                throw new ArgumentException("message", nameof(developerMessage));
            }

            Target = target;
            DeveloperMessage = developerMessage;
        }

        public string Target { get; }

        public string DeveloperMessage { get; }
    }

    public class ResponseModel<T>
    {
        public ResponseModel()
        {
            Status = new Status();
        }

        [JsonProperty("status", IsReference = true)]
        public virtual Status Status { get; set; }

        [JsonProperty("data")]
        public virtual T Data { get; set; }
    }

    public class Status
    {
        [JsonProperty("code")] public virtual int Code { get; set; }

        [JsonProperty("message")] public virtual string Message { get; set; }
    }
}
