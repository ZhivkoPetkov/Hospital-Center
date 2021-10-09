using Microsoft.Extensions.Configuration;
using PatientService.Models.Patient;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PatientService.API.HttpProvider
{
    public class HttpProvider : IHttpProvider
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public HttpProvider(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }
        public async Task SendPatientData(PatientInputModel patient)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(patient), Encoding.UTF8, "application/json");

            var response = await this.httpClient.PostAsync(this.configuration["VaccineConfig"], stringContent);

            if (response.IsSuccessStatusCode)
            {
                System.Console.WriteLine("Patient synced with Vaccince center db!");
            }
            else
            {
                System.Console.WriteLine("Patient sync didnt worked!");
            }
        }
    }
}
