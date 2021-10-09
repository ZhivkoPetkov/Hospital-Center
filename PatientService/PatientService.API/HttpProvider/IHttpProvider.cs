using PatientService.Models.Patient;
using System.Threading.Tasks;

namespace PatientService.API.HttpProvider
{
    public interface IHttpProvider
    {
        Task SendPatientData(PatientInputModel patient);
    }
}
