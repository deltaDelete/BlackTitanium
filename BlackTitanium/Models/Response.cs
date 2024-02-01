using JsonSubTypes;
using Newtonsoft.Json;

namespace BlackTitanium.Models; 

[JsonConverter(typeof(JsonSubtypes), "type")]
[JsonSubtypes.KnownSubType(typeof(Error), "error")]
[JsonSubtypes.KnownSubType(typeof(Ok), "ok")]
public abstract class Response {
    [JsonProperty("type")] public virtual string ResponseType { get; set; } = "none";
}

public class Error : Response {
    public string Message { get; set; } = string.Empty;
    public int Code { get; set; }
    public override string ResponseType { get; set; } = "error";
}

public class Ok : Response {
    public string Message => "Ok";
    public int Code => 200;
    public override string ResponseType { get; set; } = "ok";
}