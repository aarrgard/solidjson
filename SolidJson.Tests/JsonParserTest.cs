using NUnit.Framework;
using SolidJson;
using SolidJson.Impl;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SolidRpc.Tests.Json
{
    /// <summary>
    /// Tests parsing some json structures
    /// </summary>
    public class JsonParserTest : JsonTestBase
    {

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public async Task TestParseString()
        {
            Assert.AreEqual("test", (await Parser.ParseAsync("\"test\"")).As<string>());
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public async Task TestParseBoolean()
        {
            Assert.AreEqual(true, (await Parser.ParseAsync("true")).As<bool>());
            Assert.AreEqual(false, (await Parser.ParseAsync("false")).As<bool>());
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public async Task TestParseInteger()
        {
            Assert.AreEqual(13L, (await Parser.ParseAsync("13")).As<int>());
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public async Task TestParseFloat()
        {
            Assert.AreEqual(13.3d, (await Parser.ParseAsync("13.3")).As<double>());
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public async Task TestParseNull()
        {
            Assert.IsNull(await Parser.ParseAsync("null"));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public async Task TestParseArray()
        {
            var jArr = ((IJsonArray) await Parser.ParseAsync("[\"test\", 1, 1.1, true, false, null]"));
            var arr = jArr.As<IList<IJsonStruct>>();
            Assert.AreEqual("test", ((IJsonString)arr[0]).String);
            Assert.AreEqual(1L, ((IJsonInteger)arr[1]).Integer);
            Assert.AreEqual(1.1d, ((IJsonFloat)arr[2]).Float);
            Assert.AreEqual(true, ((IJsonBoolean)arr[3]).Boolean);
            Assert.AreEqual(false, ((IJsonBoolean)arr[4]).Boolean);
            Assert.AreEqual(null, arr[5]);
            Assert.AreSame(jArr[0], arr[0]);
            Assert.AreSame(jArr[1], arr[1]);
            Assert.AreSame(jArr[2], arr[2]);
            Assert.IsTrue(ReferenceEquals(jArr[0].Parent, arr[0].Parent));
            Assert.IsTrue(ReferenceEquals(jArr, ((IJsonString)arr[0]).Parent));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public async Task TestParseObject()
        {
            var obj = (IJsonObject) await Parser.ParseAsync("{Integer:1,Float:1.1}");
            Assert.AreEqual(1, ((IJsonInteger)obj["Integer"]).Integer);
            Assert.AreEqual(1.1d, ((IJsonFloat)obj["Float"]).Float);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public async Task TestProxyObject()
        {
            for (int i = 0; i < 1000; i++)
            {
                var obj = await Parser.ParseAsync("{Integer:1,Float:1.1}");
                var proxy = obj.As<IJsonObject>();
                Assert.IsNotNull(proxy["Integer"]);
            }
        }
    }
}
