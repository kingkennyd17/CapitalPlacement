using CapitalPlacement.Models;

namespace CapitalPlacement.DTOs
{
    public class ApplicationFormDTO
    {
        public ApplicationForm applicationForm { get; set; }
        public List<Education> education { get; set; }
        public List<WorkExperience> workExperience { get; set; }

    }
}
