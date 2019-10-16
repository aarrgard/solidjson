using System.Runtime.Serialization;

namespace SolidRpc.Json.OpenApi.V3
{
    /// <summary>
    /// The object provides metadata about the API. The metadata MAY be used by the clients if needed, and MAY be presented in editing or documentation generation tools for convenience.   
    /// </summary>
    /// <a href="https://swagger.io/specification/#infoObject">see</a>
    public interface IInfo
    {
        /// <summary>
        /// REQUIRED. The title of the application.
        /// </summary>
        [DataMember(Name = "title", IsRequired = true, EmitDefaultValue = true)]
        string Title { get; set; }

        /// <summary>
        /// A short description of the application.CommonMark syntax MAY be used for rich text representation.
        /// </summary>
        [DataMember(Name = "description", IsRequired = false, EmitDefaultValue = false)]
        string Description { get; set; }

        /// <summary>
        /// A URL to the Terms of Service for the API.MUST be in the format of a URL.
        /// </summary>
        [DataMember(Name = "termsOfService", IsRequired = false, EmitDefaultValue = false)]
        string TermsOfService { get; set; }

        /// <summary>
        /// Contact Object The contact information for the exposed API.
        /// </summary>
        [DataMember(Name = "contact", IsRequired = false, EmitDefaultValue = false)]
        IContact Contact { get; set; }

        /// <summary>
        /// License Object The license information for the exposed API.
        /// </summary>
        [DataMember(Name = "license", IsRequired = false, EmitDefaultValue = false)]
        ILicense License { get; set; }

        /// <summary>
        /// REQUIRED. The version of the OpenAPI document (which is distinct from the OpenAPI Specification version or the API implementation version).
        /// </summary>
        [DataMember(Name = "version", IsRequired = true, EmitDefaultValue = true)]
        string Version { get; set; }
    }
}