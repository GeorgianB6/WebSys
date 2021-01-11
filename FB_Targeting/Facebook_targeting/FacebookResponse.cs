using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Facebook_targeting
{
    public class FacebookResponse
    {   [JsonObject(Title = "error")]
        public class ErrorResponse
        {
            [JsonProperty(PropertyName ="message")]
            public string Message { get; set; }
            [JsonProperty(PropertyName = "type")]
            public string Type { get; set; }
            [JsonProperty(PropertyName = "code")]
            public string Code { get; set; }
            [JsonProperty(PropertyName = "error_subcode")]
            public string ErrorSubcode { get; set; }
            [JsonProperty(PropertyName = "is_transient", NullValueHandling = NullValueHandling.Include)]
            public bool IsTransient { get; set; }
            [JsonProperty(PropertyName = "error_user_title", NullValueHandling = NullValueHandling.Include)]
            public string ErrorUserTitle { get; set; }           
            [JsonProperty(PropertyName = "error_user_msg", NullValueHandling = NullValueHandling.Include)]
            public string ErrorUserMsg { get; set; }
            [JsonProperty(PropertyName = "fbtrace_id", NullValueHandling = NullValueHandling.Include)]
            public string FbTraceId { get; set; }

            public ErrorResponse(string message, string type, string code, string errorSubcode, string errorUserTitle, bool isTransient, string errorUserMsg, string fbTraceId)
            {
                Message = message;
                Type = type;
                Code = code;
                ErrorSubcode = errorSubcode;
                ErrorUserTitle = errorUserTitle;
                IsTransient = isTransient;
                ErrorUserMsg = errorUserMsg;
                FbTraceId = fbTraceId;
            }

            public override string ToString()
            {
                return $"Graph API error! Code: {Code} | Subcode: {ErrorSubcode} | Type: {Type} | Message: {Message} | Details: {ErrorUserMsg}";
            }
        }
        public class RootObject
        {
            public ErrorResponse error { get; set; }
        }

    }   
}
