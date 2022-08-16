namespace PatientBackend.Api.Interfaces
{
    public interface ILoginService
    {
        public string Login(string email, string password);
        public bool IsValidToken(string token);
    }
}
