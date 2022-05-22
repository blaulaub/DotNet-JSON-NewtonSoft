namespace JsonModelTest;

using NUnit.Framework;
using Newtonsoft.Json.Schema;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    string? getResourceContent(string resourceName)
    {
        var assembly = System.Reflection.Assembly.GetAssembly(typeof(JsonModelTest.Tests));
        var stream = assembly?.GetManifestResourceStream(resourceName);
        if (stream != null)
        {
            var reader = new System.IO.StreamReader(stream);
            var text = reader.ReadToEnd();
            return text;
        }
        else
        {
            return null;
        }

    }

    [Test]
    public void CanValidateFromResource()
    {
        var schemaJson = getResourceContent("JsonModelTest.powerline.schema.json");
        Assert.NotNull(schemaJson);
        var schema = JSchema.Parse(schemaJson);

        var lineJson = getResourceContent("JsonModelTest.powerline.ok.json");
        Assert.NotNull(lineJson);
        var line = Newtonsoft.Json.Linq.JObject.Parse(lineJson);

        Assert.True(line.IsValid(schema));
    }
}
