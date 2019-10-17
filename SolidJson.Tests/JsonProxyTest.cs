using NUnit.Framework;
using SolidJson;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SolidRpc.Tests.Json
{
    /// <summary>
    /// Tests parsing some json structures
    /// </summary>
    public class JsonProxyTest : JsonTestBase
    {
        /// <summary>
        /// A test interface
        /// </summary>
        public interface ITestInterface
        {
            /// <summary>
            /// Tests getting and setting a string
            /// </summary>
            [DataMember(Name = "test-string", EmitDefaultValue = true)]
            string TestString { get; set; }

            /// <summary>
            /// Tests getting and setting a string
            /// </summary>
            [DataMember(Name = "test-int", EmitDefaultValue = false)]
            int TestInteger { get; set; }

            /// <summary>
            /// Tests getting and setting a string
            /// </summary>
            [DataMember(Name = "child", EmitDefaultValue = true)]
            IEnumerable<ITestInterface> ChildObjects { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public async Task TestStringValue()
        {
            var i = (await Parser.ParseAsync("{'test-string':\"testing\"}")).As<ITestInterface>();
            Assert.AreEqual("testing", i.TestString);
        }


        /// <summary>
        /// 
        /// </summary>
        [Test]
        public async Task TestIntegerValue()
        {
            var i = (await Parser.ParseAsync("{'test-int': 42}")).As<ITestInterface>();
            Assert.AreEqual(42, i.TestInteger);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public async Task TestDefaultValuesOnEmptyObject()
        {
            var i = (await Parser.ParseAsync("{}")).As<ITestInterface>();
            Assert.IsNull(i.TestString);
            Assert.AreEqual(0, i.TestInteger);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public async Task TestCastProxyToJsonStruct()
        {
            var i = (await Parser.ParseAsync ("{\"test-string\": \"wohoo\"}")).As<ITestInterface>();
            var s = (IJsonObject)i;
            Assert.AreEqual("wohoo", s["test-string"].As<string>());
        }
    }
}
