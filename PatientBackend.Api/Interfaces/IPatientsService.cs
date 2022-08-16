using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PatientBackend.Api.Models;

namespace PatientBackend.Api.Interfaces
{
    public interface IPatientsService
    {
        public Task<List<Patient>> GetPatients();
        public Task<Patient> GetPatientById(Guid patientId);
    }
}
