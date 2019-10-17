using System.Runtime.Serialization;

namespace SolidJson.OpenApi.V3
{
    /// <summary>
    /// License information for the exposed API.
    /// </summary>
    /// <a href="https://swagger.io/specification/#licenseObject">see</a>
    public interface ILicense
    {
        /// <summary>
        /// REQUIRED.The license name used for the API.
        /// </summary>
        [DataMember(Name = "name", IsRequired = true, EmitDefaultValue = true)]
        string Name { get; set; }

        /// <summary>
        /// A URL to the license used for the API. MUST be in the format of a URL.
        /// </summary>
        [DataMember(Name = "url", IsRequired = true, EmitDefaultValue = true)]
        string Url { get; set; }
    }
}