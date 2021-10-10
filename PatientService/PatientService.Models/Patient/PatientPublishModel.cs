using System.ComponentModel.DataAnnotations;

namespace PatientService.Models.Patient
{
    public class PatientPublishModel : PatientOutputModel
    {
        public PatientPublishModel()
        {
            this.Event = "Patient Added";
        }
        public string Event { get; set; }
    }
}
