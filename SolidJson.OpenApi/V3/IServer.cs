using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SolidJson.OpenApi.V3
{
    /// <summary>
    /// An object representing a Server.
    /// </summary>
    /// <a href="https://swagger.io/specification/#serverObject">see</a>
    public interface IServer
    {
        /// <summary>
        /// REQUIRED.A URL to the target host.This URL supports Server Variables and MAY be relative, to indicate that the host location is relative to the location where the OpenAPI document is being served. Variable substitutions will be made when a variable is named in { brackets}.
        /// </summary>
        [DataMember(Name = "url", IsRequired = false, EmitDefaultValue = false)]
        string Url { get; set; }

        /// <summary>
        /// An optional string describing the host designated by the URL.CommonMark syntax MAY be used for rich text representation.
        /// </summary>
        [DataMember(Name = "description", IsRequired = false, EmitDefaultValue = false)]
        string Description { get; set; }

        /// <summary>
        /// A map between a variable name and its value.The value is used for substitution in the server's URL template.
        /// </summary>
        [DataMember(Name = "variables", IsRequired = false, EmitDefaultValue = false)]
        IDictionary<string, IServerVariable> Variables { get; set; }
    }
}
