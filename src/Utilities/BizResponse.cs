using CleanArchitectureSample.Utilities.Constants;
using System.Text.Json.Serialization;

namespace CleanArchitectureSample.Utilities
{
    public class BizResponse
    {
        [JsonPropertyName("sign_type")]
        public string SignType { set; get; }

        [JsonPropertyName("sign")]
        public string Sign { set; get; }

        [JsonPropertyName("code")]
        public string Code { set; get; }

        [JsonPropertyName("msg")]
        public string Msg { set; get; }

        [JsonPropertyName("sub_code")]
        public string SubCode { set; get; }

        [JsonPropertyName("sub_msg")]
        public string SubMsg { set; get; }

        [JsonIgnore]
        public bool IsError
        {
            get { return Code != ExecutionResult.Success; }
        }
        [JsonIgnore]
        public bool IsSuccess
        {
            get { return !IsError; }
        }
        public void Fail(ErrorInfo subError, string errorMsg = "error")
        {
            Code = ExecutionResult.Fail;
            Msg = errorMsg;
            SubCode = subError.Code;
            SubMsg = subError.Message;
        }
        public void Success()
        {
            //default with success
            Code = ExecutionResult.Success;
            Msg = "ok";
        }

    }
    public class BizResponse<TContent> : BizResponse
        where TContent : class, new()
    {
        [JsonPropertyName("content")]
        public TContent Content { set; get; }
        public BizResponse()
        {
            this.Content = new TContent();
        }
    }
}
