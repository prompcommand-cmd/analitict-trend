namespace Infrastructure.Abstracts
{
    public interface ICryptography
    {
        public string AesDecrypt(string text);
    }
}
