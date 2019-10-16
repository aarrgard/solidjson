using NUnit.Framework;
using SolidRpc.Json;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SolidRpc.Tests.Json
{
    /// <summary>
    /// Tests parsing some json structures
    /// </summary>
    public class DefaultValueTest : JsonTestBase
    {
        /// <summary>
        /// A test interface
        /// </summary>
        public interface ITestInterfaceEmitStringDefaultValueTrue
        {
            /// <summary>
            /// Tests getting and setting a string
            /// </summary>
            [DataMember(Name = "test-string", EmitDefaultValue = true)]
            string TestString { get; set; }
        }
        /// <summary>
        /// A test interface
        /// </summary>
        public interface ITestInterfaceEmitStringDefaultValueFalse
        {
            /// <summary>
            /// Tests getting and setting a string
            /// </summary>
            [DataMember(Name = "test-string", EmitDefaultValue = false)]
            string TestString { get; set; }
        }
        /// <summary>
        /// A test interface
        /// </summary>
        public interface ITestInterfaceEmitArrayDefaultValueTrue
        {
            /// <summary>
            /// Tests getting and setting a string
            /// </summary>
            [DataMember(Name = "d", EmitDefaultValue = true)]
            IEnumerable<ITestInterfaceEmitArrayDefaultValueTrue> Data { get; set; }
        }

        /// <summary>
        /// A test interface
        /// </summary>
        public interface ITestInterfaceEmitArrayDefaultValueFalse
        {
            /// <summary>
            /// Tests getting and setting a string
            /// </summary>
            [DataMember(Name = "d", EmitDefaultValue = false)]
            IEnumerable<ITestInterfaceEmitArrayDefaultValueFalse> Data { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestEmitDefaultStringValueTrue()
        {
            var jo = Factory.New<ITestInterfaceEmitStringDefaultValueTrue>();
            Assert.AreEqual("{\"test-string\":null}", ((IJsonStruct)jo).AsJson());
            jo.TestString = "testing";
            Assert.AreEqual("{\"test-string\":\"testing\"}", ((IJsonStruct)jo).AsJson());
            jo.TestString = null;
            Assert.AreEqual("{\"test-string\":null}", ((IJsonStruct)jo).AsJson());
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestEmitDefaultStringValueFalse()
        {
            var jo = Factory.New<ITestInterfaceEmitStringDefaultValueFalse>();
            Assert.AreEqual("{}", ((IJsonStruct)jo).AsJson());
            jo.TestString = "testing";
            Assert.AreEqual("{\"test-string\":\"testing\"}", ((IJsonStruct)jo).AsJson());
            jo.TestString = null;
            Assert.AreEqual("{}", ((IJsonStruct)jo).AsJson());
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestEmitDefaultArrayValueTrue()
        {
            var jo = Factory.New<ITestInterfaceEmitArrayDefaultValueTrue>();
            Assert.AreEqual("{\"d\":[]}", ((IJsonStruct)jo).AsJson());
            var nestedJo = Factory.New<ITestInterfaceEmitArrayDefaultValueTrue>(jo);
            Assert.AreEqual("{\"d\":[]}", ((IJsonStruct)nestedJo).AsJson());
            jo.Data = new[] { nestedJo };
            Assert.AreEqual("{\"d\":[]}", ((IJsonStruct)jo.Data.First()).AsJson());
            Assert.AreEqual("{\"d\":[{\"d\":[]}]}", ((IJsonStruct)jo).AsJson());
        }
    }
}
