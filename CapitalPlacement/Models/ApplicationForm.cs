using CapitalPlacement.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CapitalPlacement.Models
{
    public class ApplicationForm : BaseModel
    {
        [DataMember]
        [Browsable(false)]
        public string ApplicationId { get; set; }

        [DataMember]
        public string ProgramId { get; set; }

        [DataMember]
        public byte[] CoverImage { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public Dictionary<string, object> AdditionalPersonalInfo { get; set; } = new Dictionary<string, object>();

        [DataMember]
        public List<object>? Resume { get; set; }

        [DataMember]
        public Dictionary<string, object> AdditionalProfile { get; set; } = new Dictionary<string, object>();

        [DataMember]
        public Dictionary<string, object> AdditionalQuestion { get; set; } = new Dictionary<string, object>();


        public string EntityId
        {
            get
            {
                return ApplicationId;
            }
        }
    }
}
