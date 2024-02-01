using BlackTitanium.Models;
using JsonSubTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit.Abstractions;

namespace BlackTitanium.Tests;

public class JsonTest {
    private readonly ITestOutputHelper _output;

    private readonly JsonSerializerSettings _jsonSerializerSettings =
        new JsonSerializerSettings {
            Formatting = Formatting.Indented,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        };

    private const string ExpectedJson = """
                                        {
                                          "message": "Test error",
                                          "code": -1,
                                          "type": "error"
                                        }
                                        """;

    public JsonTest(ITestOutputHelper output) {
        this._output = output;
        _jsonSerializerSettings.Converters.Add(
            JsonSubtypesConverterBuilder
                .Of<Response>("type")
                .RegisterSubtype<Error>("error")
                .RegisterSubtype<Ok>("ok")
                .SerializeDiscriminatorProperty(true)
                .Build()
        );
        _jsonSerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
        JsonConvert.DefaultSettings = () => _jsonSerializerSettings;
    }

    [Fact]
    public void Derived_type_serialization() {
        Response error = new Error() {
            Code = -1,
            Message = "Test error"
        };

        var result = JsonConvert.SerializeObject(
            error
        );

        _output.WriteLine(result);
        Assert.Equal(ExpectedJson, result);
    }

    [Fact]
    public void Derived_type_deserialization() {
        var result = JsonConvert.DeserializeObject<Response>(ExpectedJson);

        Assert.IsType<Error>(result);
        Assert.Equal(-1, (result as Error)?.Code);
        Assert.Equal("Test error", (result as Error)?.Message);
    }
}