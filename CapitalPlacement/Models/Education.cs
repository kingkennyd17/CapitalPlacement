using CapitalPlacement.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CapitalPlacement.Models
{
    public class Education : BaseModel
    {
        [DataMember]
        [Browsable(false)]
        public string EducationId { get; set; }

        [DataMember]
        public string ApplicationId { get; set; }

        [DataMember]
        public string? School { get; set; }

        [DataMember]
        public string? Degree { get; set; }

        [DataMember]
        public string? Course { get; set; }

        [DataMember]
        public string? Location { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }

        [DataMember]
        public bool CurrentlyStudying { get; set; }

        public string EntityId
        {
            get
            {
                return EducationId;
            }
        }
    }
}
