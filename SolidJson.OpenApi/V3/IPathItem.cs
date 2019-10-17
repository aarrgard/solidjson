using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SolidJson.OpenApi.V3
{
    /// <summary>
    /// Holds the relative paths to the individual endpoints and their operations. The path is appended to the URL from the Server Object in order to construct the full URL. The Paths MAY be empty, due to ACL constraints.
    /// </summary>
    /// <a href="https://swagger.io/specification/#pathItemObject">see</a>
    public interface IPathItem
    {

        /// <summary>
        /// Allows for an external definition of this path item. The referenced structure MUST be in the format of a Path Item Object.If there are conflicts between the referenced definition and this Path Item's definition, the behavior is undefined.
        /// </summary>
        [DataMember(Name = "$ref", IsRequired = false, EmitDefaultValue = false)]
        IPathItem Ref { get; set; }

        /// <summary>
        /// An optional, string summary, intended to apply to all operations in this path.
        /// </summary>
        [DataMember(Name = "summary", IsRequired = false, EmitDefaultValue = false)]
        string Summary { get; set; }

        /// <summary>
        /// An optional, string description, intended to apply to all operations in this path.CommonMark syntax MAY be used for rich text representation.
        /// </summary>
        [DataMember(Name = "description", IsRequired = false, EmitDefaultValue = false)]
        string Description { get; set; }

        /// <summary>
        /// Object A definition of a GET operation on this path.
        /// </summary>
        [DataMember(Name = "get", IsRequired = false, EmitDefaultValue = false)]
        IOperation Get { get; set; }

        /// <summary>
        /// Object A definition of a DELETE operation on this path.
        /// </summary>
        [DataMember(Name = "delete", IsRequired = false, EmitDefaultValue = false)]
        IOperation Delete { get; set; }

        /// <summary>
        /// Object A definition of a OPTIONS operation on this path.
        /// </summary>
        [DataMember(Name = "options", IsRequired = false, EmitDefaultValue = false)]
        IOperation Options { get; set; }

        /// <summary>
        /// Object A definition of a HEAD operation on this path.
        /// </summary>
        [DataMember(Name = "head", IsRequired = false, EmitDefaultValue = false)]
        IOperation Head { get; set; }

        /// <summary>
        /// Object A definition of a TRACE operation on this path.
        /// </summary>
        [DataMember(Name = "trace", IsRequired = false, EmitDefaultValue = false)]
        IOperation Trace { get; set; }

        /// <summary>
        /// An alternative server array to service all operations in this path.
        /// </summary>
        [DataMember(Name = "servers", IsRequired = false, EmitDefaultValue = false)]
        IEnumerable<IServer> Servers { get; set; }

        /// <summary>
        /// A list of parameters that are applicable for all the operations described under this path.These parameters can be overridden at the operation level, but cannot be removed there.The list MUST NOT include duplicated parameters.A unique parameter is defined by a combination of a name and location.The list can use the Reference Object to link to parameters that are defined at the OpenAPI Object's components/parameters.
        /// </summary>
        [DataMember(Name = "parameters", IsRequired = false, EmitDefaultValue = false)]
        IEnumerable<IParameter> Parameters { get; set; }
    }
}
