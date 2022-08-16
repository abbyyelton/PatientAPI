using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientBackend.Api.Repositories
{
    public static class FakeDatabase
    {
        private static Dictionary<string, Guid?> Emails = new Dictionary<string, Guid?>() 
        { 
            { "test@email.com", null },
            { "test1@email.com", null } 
        };

        private static Dictionary<Guid, Guid> GuidMap = new Dictionary<Guid, Guid>() {};

        public static bool IsValidEmail(string email)
        {
            return Emails.ContainsKey(email);
        }

        public static void SetGuid(string email, Guid guid)
        {
            Emails[email] = guid;
        }

        public static Guid GetSafeGuid(Guid guid)
        {
            // If user is not already known add to GuidMap
            if (!GuidMap.ContainsKey(guid))
            {
                GuidMap.Add(guid, Guid.NewGuid());
            }
            return GuidMap.GetValueOrDefault(guid);
        }

        public static Guid GetTrueGuid(Guid guid)
        {
            return GuidMap.FirstOrDefault(x => x.Value == guid).Key;
        }

        public static bool IsValidToken(Guid token)
        {
            return Emails.ContainsValue(token);
        }
    }
}
