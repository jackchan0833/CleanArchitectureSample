using System.Text.Json;
using System.Text.Json.Serialization;

namespace CleanArchitectureSample.Utilities
{
    public abstract class BizRequest<TContent>
        where TContent : class, new()
    {
        [JsonPropertyName("app_id")]
        public string AppId { set; get; }

        [JsonPropertyName("nonce")]
        public string Nonce { set; get; }

        [JsonPropertyName("sign_type")]
        public string SignType { set; get; }

        [JsonPropertyName("sign")]
        public string Sign { set; get; }

        [JsonPropertyName("content")]
        public TContent Content { set; get; }
        public BizRequest()
        {
            this.Content = new TContent();
        }
    }
}
