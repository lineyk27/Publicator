namespace Publicator.ApplicationCore.Contracts
{
    public interface IPasswordService
    {
        public string Encrypt(string text);
        public bool IsEqual(string hash1, string hash2);
    }
}
