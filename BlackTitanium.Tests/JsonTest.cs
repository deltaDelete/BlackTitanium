using BlackTitanium.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit.Abstractions;

namespace BlackTitanium.Tests;

public class JsonTest {
    private readonly ITestOutputHelper output;

    private const string ExpectedJson = """
                                               {
                                                 "$type": "BlackTitanium.Models.Error, BlackTitanium",
                                                 "message": "Test error",
                                                 "code": -1
                                               }
                                               """;

    public JsonTest(ITestOutputHelper output) {
        this.output = output;
    }

    [Fact]
    public void Derived_type_serialization() {
        var error = new Models.Error() {
            Code = -1,
            Message = "Test error"
        };
        var jsonSerializerSettings = new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        var result = JsonConvert.SerializeObject(
            error,
            typeof(Models.Response),
            jsonSerializerSettings
        );

        output.WriteLine(result);
        Assert.Equal(ExpectedJson, result);
    }

    [Fact]
    public void Derived_type_deserialization() {
        var jsonSerializerSettings = new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        var result = JsonConvert.DeserializeObject<Response>(ExpectedJson, jsonSerializerSettings);

        Assert.IsType<Error>(result);
        Assert.Equal(-1, (result as Error)?.Code);
        Assert.Equal("Test error", (result as Error)?.Message);
    }
}