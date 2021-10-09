using PatientService.Models.Patient;
using System.Threading.Tasks;

namespace PatientService.HttpClient
{
    internal interface IHttpProvider
    {
        Task SendPatientData(PatientInputModel patient);
    }
}
