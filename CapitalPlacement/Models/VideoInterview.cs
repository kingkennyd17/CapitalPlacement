using CapitalPlacement.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CapitalPlacement.Models
{
    public class VideoInterview : BaseModel
    {
        [DataMember]
        [Browsable(false)]
        public string VideoInterviewId { get; set; }

        [DataMember]
        public string WorkFlowId { get; set; }

        [DataMember]
        public string VideoQuestion { get; set; }

        [DataMember]
        public string AdditionalVideoInfo { get; set; }

        [DataMember]
        public int VideoMaxDuration { get; set; }

        [DataMember]
        public string TimeType { get; set; }

        [DataMember]
        public int SubmissionDeadline { get; set; }


        public string EntityId
        {
            get
            {
                return VideoInterviewId;
            }
        }
    }
}
