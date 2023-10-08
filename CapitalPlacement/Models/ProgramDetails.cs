using CapitalPlacement.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CapitalPlacement.Models
{
    public class ProgramDetails : BaseModel
    {
        [DataMember]
        [Browsable(false)]
        public string ProgramId { get; set; }

        [DataMember]
        [Required]
        public string Title { get; set; }

        [DataMember]
        public string? Summary { get; set; }

        [DataMember]
        [Required]
        public string Description { get; set; }

        [DataMember]
        public string? RequiredSkills { get; set; }

        [DataMember]
        public string? Benefits { get; set; }

        [DataMember]
        public string? ApplicationCriteria { get; set; }

        [DataMember]
        [Required]
        public string Type { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        [Required]
        public DateTime ApplicationOpenDate { get; set; }

        [DataMember]
        [Required]
        public DateTime ApplicationCloseDate { get; set; }

        [DataMember]
        public int Duration { get; set; }

        [DataMember]
        [Required]
        public string Location { get; set; }

        [DataMember]
        [Required]
        public bool Remote { get; set; }

        [DataMember]
        public string? MinQualification { get; set; }

        [DataMember]
        public int MaxNumberOfApplication { get; set; }


        public string EntityId
        {
            get
            {
                return ProgramId;
            }
        }
    }
}
