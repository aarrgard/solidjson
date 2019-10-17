using NUnit.Framework;
using SolidJson;
using SolidJson.OpenApi.V3;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SolidRpc.Tests.Json
{
    /// <summary>
    /// Tests parsing some json structures
    /// </summary>
    public class OpenApiV3Test : JsonTestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public async Task TestCreateOpenApiV3()
        {
            var openApi = Factory.New<IOpenAPI>();
            openApi.OpenAPI = "3.0";
            
            openApi.Info.Title = "test";
            openApi.Info.License.Name = "MIT";
            openApi.Info.License.Url = "http://mit.license/";
            openApi.Info.Version = "2.1.4";

            openApi.Servers.Add(Factory.New<IServer>());
            openApi.Servers.First().Description = "A test server";

            openApi.Paths["/test/path"].Get.OperationId = "testing";

            var template = GetManifestResourceAsString("TestCreateOpenApiV3.json");
            var generated = ((IJsonObject)openApi).AsJson(true);
            Assert.AreEqual(template, generated);
        }
    }
}
