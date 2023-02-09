namespace TransactionSyncAPI.Services.Interfaces.InternalServices
{
    public interface IPasswordHasher
    {
        public string ComputeHash(string password, string salt);

        public string GenerateSalt();
    }
}
