using PatientBackend.Api.Interfaces;
using System;

namespace PatientBackend.Api.Repositories
{
    public class Repository : IRepository
    {
        public bool IsValidEmail(string email)
        {
            return FakeDatabase.IsValidEmail(email);
        }

        public void SetGuid(string email, Guid guid)
        {
            FakeDatabase.SetGuid(email, guid);
        }

        public Guid GetSafeGuid(Guid guid)
        {
            return FakeDatabase.GetSafeGuid(guid);
        }

        public Guid GetTrueGuid(Guid guid)
        {
            return FakeDatabase.GetTrueGuid(guid);
        }

        public bool IsValidToken(Guid token)
        {
            return FakeDatabase.IsValidToken(token);
        }
    }
}
