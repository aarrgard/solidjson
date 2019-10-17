using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SolidJson.OpenApi.V3
{
    /// <summary>
    /// 
    /// </summary>
    /// <a href="https://swagger.io/specification/#parameterObject">see</a>
    public interface IOperation
    {
        /// <summary>
        /// A list of tags for API documentation control. Tags can be used for logical grouping of operations by resources or any other qualifier.
        /// </summary>
        [DataMember(Name = "tags", IsRequired = false, EmitDefaultValue = false)]
        IEnumerable<string> Tags { get; set; }

        /// <summary>
        /// A short summary of what the operation does.	A short summary of what the operation does.	A short summary of what the operation does.	A short summary of what the operation does.
        /// </summary>
        [DataMember(Name = "summary", IsRequired = false, EmitDefaultValue = false)]
        string Summary { get; set; }

        /// <summary>
        /// A verbose explanation of the operation behavior. CommonMark syntax MAY be used for rich text representation.
        /// </summary>
        [DataMember(Name = "description", IsRequired = false, EmitDefaultValue = false)]
        string Description { get; set; }

        /// <summary>
        /// Additional external documentation for this operation.
        /// </summary>
        [DataMember(Name = "externalDocs", IsRequired = false, EmitDefaultValue = false)]
        IExternalDocumentation ExternalDocs { get; set; }


        /// <summary>
        /// Unique string used to identify the operation. The id MUST be unique among all operations described in the API. The operationId value is case-sensitive. Tools and libraries MAY use the operationId to uniquely identify an operation, therefore, it is RECOMMENDED to follow common programming naming conventions.
        /// </summary>
        [DataMember(Name = "operationId", IsRequired = false, EmitDefaultValue = false)]
        string OperationId { get; set; }
    }
}