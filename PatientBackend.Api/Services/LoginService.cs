using PatientBackend.Api.Interfaces;
using System;

namespace PatientBackend.Api.Services
{
    public class LoginService :ILoginService
    {
        private readonly IRepository _repository;

        public LoginService(IRepository repository)
        {
            _repository = repository;
        }

        public string Login(string email, string password)
        {
            if (_repository.IsValidEmail(email))
            {
                var token = GetToken();
                _repository.SetGuid(email, new Guid(token));
                return token;
            }
            return null;
        }

        public bool IsValidToken(string token)
        {
            return _repository.IsValidToken(new Guid(token));
        }

        private string GetToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
