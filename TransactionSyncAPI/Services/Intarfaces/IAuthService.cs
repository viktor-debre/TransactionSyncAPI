namespace TransactionSyncAPI.Services.Intarfaces
{
    public interface IAuthService
    {
        public Task<string> AuthenticateUser(string email, string password);
    }
}
