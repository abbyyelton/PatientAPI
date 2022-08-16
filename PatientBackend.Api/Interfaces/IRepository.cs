using System;

namespace PatientBackend.Api.Interfaces
{
    public interface IRepository
    {
        public bool IsValidEmail(string email);
        public void SetGuid(string email, Guid guid);

        public Guid GetSafeGuid(Guid guid);
        public Guid GetTrueGuid(Guid guid);
        public bool IsValidToken(Guid token);
    }
}
