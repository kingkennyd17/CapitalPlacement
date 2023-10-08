using CapitalPlacement.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CapitalPlacement.Models
{
    public class WorkFlow : BaseModel
    {
        [DataMember]
        [Browsable(false)]
        public string WorkFlowId { get; set; }

        [DataMember]
        [Required]
        public string StageName { get; set; }

        [DataMember]
        public int ProgramId { get; set; }

        [DataMember]
        public int StageType { get; set; }

        [DataMember]
        public bool CandidateCanView { get; set; }


        public string EntityId
        {
            get
            {
                return WorkFlowId;
            }
        }
    }
}
