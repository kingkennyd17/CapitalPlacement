using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace CapitalPlacement.Base
{
    public abstract class BaseModel : IExtensibleDataObject
    {
        public BaseModel()
        {
            Active = true;
            Deleted = false;
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
        }

        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public bool Deleted { get; set; }

        [DataMember]
        public string CreatedBy { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; set; }

        [DataMember]
        public string UpdatedBy { get; set; }

        [DataMember]
        public DateTime UpdatedOn { get; set; }

        [XmlIgnore]
        public ExtensionDataObject? ExtensionData { get; set; }
    }
}
