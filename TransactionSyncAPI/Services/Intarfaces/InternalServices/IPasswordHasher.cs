namespace TransactionSyncAPI.Services.Intarfaces.InternalServices
{
    public interface IPasswordHasher
    {
        public string ComputeHash(string password, string salt);

        public string GenerateSalt();
    }
}
