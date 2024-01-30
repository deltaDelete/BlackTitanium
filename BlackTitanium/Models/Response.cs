using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace BlackTitanium.Models; 

[JsonDerivedType(typeof(Error), "type")]
public class Response {
    // [JsonProperty("type")]
    // public string ResponseType { get; set; }
}

public class Error : Response {
    public string Message { get; set; } = string.Empty;
    public int Code { get; set; }
}