using System.Text.Json;
using System.Text.Json.Serialization;

namespace web_app.Models
{
    public class ActionItem
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        
        [JsonPropertyName("target")]
        public string? Target { get; set; }
        
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("campaign")]
        public string? Campaign { get; set; }
        
        [JsonPropertyName("expiry")]
        public string? Expiry { get; set; }
        
        [JsonPropertyName("when_created")]
        public string? When_created { get; set; }
        
        [JsonPropertyName("when_updated")]
        public string? When_updated { get; set; }
        
        [JsonPropertyName("content")]
        public string? Content { get; set; }
       
        [JsonPropertyName("country")]
        public string? Country { get; set; }
        
        [JsonPropertyName("language")]
        public string? Language { get; set; }
      
        [JsonPropertyName("customerset")]
        public string? CustomerSet { get; set; }

        public override string ToString() => JsonSerializer.Serialize<ActionItem>(this);

    }
}
