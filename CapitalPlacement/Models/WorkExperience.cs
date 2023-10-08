using CapitalPlacement.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CapitalPlacement.Models
{
    public class WorkExperience : BaseModel
    {
        [DataMember]
        [Browsable(false)]
        public string WorkExperienceId { get; set; }

        [DataMember]
        public string ApplicationId { get; set; }

        [DataMember]
        public string? Company { get; set; }

        [DataMember]
        public string? Title { get; set; }

        [DataMember]
        public string? Location { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }

        [DataMember]
        public bool CurrentlyWorking { get; set; }

        public string EntityId
        {
            get
            {
                return WorkExperienceId;
            }
        }
    }
}
