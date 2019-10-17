using NUnit.Framework;
using SolidJson;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SolidRpc.Tests.Json
{
    /// <summary>
    /// Tests parsing some json structures
    /// </summary>
    public class JsonFactoryTest : JsonTestBase
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
            [DataMember(Name = "children", EmitDefaultValue = true)]
            IEnumerable<ITestInterface> ChildObjects { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestCreateAnonumousInstance()
        {
            var jo = Factory.New();
            Assert.AreEqual("{}", jo.AsJson());
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestCreateTypedInstance()
        {
            var jo = Factory.New<ITestInterface>();
            jo.TestString = "testing";
            Assert.AreEqual("{\"test-string\":\"testing\",\"children\":[]}", ((IJsonStruct)jo).AsJson());
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestCreateTypedInstanceNested()
        {
            var jo = Factory.New<ITestInterface>();
            jo.TestString = "testing";
            jo.ChildObjects = new[]
            {
                Factory.New<ITestInterface>((IJsonStruct)jo)
            };
            Assert.AreEqual("{\"test-string\":\"testing\",\"children\":[{\"test-string\":null,\"children\":[]}]}", ((IJsonStruct)jo).AsJson());
        }

    }
}
