using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SolidJson.OpenApi.V3
{
    /// <summary>
    /// An object representing a Server Variable for server URL template substitution.
    /// </summary>
    /// <a href="https://swagger.io/specification/#serverVariableObject">see</a>
    public interface IServerVariable
    {
        /// <summary>
        /// An enumeration of string values to be used if the substitution options are from a limited set.
        /// </summary>
        [DataMember(Name = "enum", IsRequired = false, EmitDefaultValue = false)]
        IEnumerable<string> Enum { get; set; }

        /// <summary>
        /// REQUIRED. The default value to use for substitution, which SHALL be sent if an alternate value is not supplied. Note this behavior is different than the Schema Object's treatment of default values, because in those cases parameter values are optional.
        /// </summary>
        [DataMember(Name = "default", IsRequired = false, EmitDefaultValue = false)]
        string Default { get; set; }

        /// <summary>
        /// An optional description for the server variable. CommonMark syntax MAY be used for rich text representation.
        /// </summary>
        [DataMember(Name = "description", IsRequired = false, EmitDefaultValue = false)]
        string Description { get; set; }
    }
}
