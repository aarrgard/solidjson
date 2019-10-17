using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SolidJson.OpenApi.V3
{
    /// <summary>
    /// This is the root document object of the OpenAPI document.
    /// </summary>
    /// <a href="https://swagger.io/specification/#oasObject">see</a>
    public interface IOpenAPI
    {
        /// <summary>
        /// REQUIRED. This string MUST be the semantic version number of the OpenAPI Specification version that the OpenAPI document uses. The openapi field SHOULD be used by tooling specifications and clients to interpret the OpenAPI document. This is not related to the API info.version string.
        /// </summary>
        [DataMember(Name = "openapi", IsRequired = true, EmitDefaultValue = true)]
        string OpenAPI { get; set; }

        /// <summary>
        /// REQUIRED. Provides metadata about the API. The metadata MAY be used by tooling as required.
        /// </summary>
        [DataMember(Name = "info", IsRequired = true, EmitDefaultValue = true)]
        IInfo Info { get; set; }

        /// <summary>
        /// An array of Server Objects, which provide connectivity information to a target server. If the servers property is not provided, or is an empty array, the default value would be a Server Object with a url value of /.    /// </summary>
        [DataMember(Name = "servers", IsRequired = false, EmitDefaultValue = false)]
        ICollection<IServer> Servers { get; set; }

        /// <summary>
        /// REQUIRED. The available paths and operations for the API.
        /// </summary>
        [DataMember(Name = "paths", IsRequired = true, EmitDefaultValue = true)]
        IPaths Paths { get; set; }
    }
}
