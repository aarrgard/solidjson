using System.Runtime.Serialization;

namespace SolidJson.OpenApi.V3
{
    /// <summary>
    /// Contact information for the exposed API.
    /// </summary>
    /// <a href="https://swagger.io/specification/#contactObject">see</a>
    public interface IContact
    {
        /// <summary>
        /// The identifying name of the contact person/organization.
        /// </summary>
        [DataMember(Name = "name", IsRequired = false, EmitDefaultValue = false)]
        string Name { get; set; }

        /// <summary>
        /// The URL pointing to the contact information.MUST be in the format of a URL.
        /// </summary>
        [DataMember(Name = "url", IsRequired = false, EmitDefaultValue = false)]
        string Url { get; set; }

        /// <summary>
        /// The email address of the contact person/organization.MUST be in the format of an email address.
        /// </summary>
        [DataMember(Name = "email", IsRequired = false, EmitDefaultValue = false)]
        string Email { get; set; }
    }
}