using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Net;
using PatientBackend.Api.Interfaces;
using PatientBackend.Api.Models;

namespace PatientBackend.Api.Services
{
    public class PatientsService : IPatientsService
    {
        private readonly string BaseUrl;
        private readonly IRepository _repository;

        public PatientsService(IConfiguration config, IRepository repostiory)
        {
            _repository = repostiory;
            BaseUrl = config.GetSection("Settings").GetSection("baseServiceAddress").Value;
        }

        public async Task<List<Patient>> GetPatients()
        {
            using (var client = new HttpClient())
            {
                var result = client.GetAsync($"{BaseUrl}/patients").Result;

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var patients = JsonConvert.DeserializeObject<List<Patient>>(content);
                    patients.ForEach(p => p.PatientId = _repository.GetSafeGuid(p.PatientId));
                    return patients;
                }
                else if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("Not Authorized");
                }
                else
                {
                    throw new Exception(string.Format("Error code {0}: {1}", result.StatusCode.ToString(), result.ReasonPhrase));
                }
            }
        }

        public async Task<Patient> GetPatientById(Guid patientId)
        {
            var trueGuid = _repository.GetTrueGuid(patientId);
            using (var client = new HttpClient())
            {
                var result = client.GetAsync($"{BaseUrl}/patient/{trueGuid}").Result;

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Patient>(content);
                }
                else if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("Not Authorized");
                }
                else
                {
                    throw new Exception(string.Format("Error code {0}: {1}", result.StatusCode.ToString(), result.ReasonPhrase));
                }
            }
        }
    }
}
