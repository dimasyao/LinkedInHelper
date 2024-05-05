namespace LH.Models
{
    public class AccessToken
    {
        public string Value { get; set; }
        public DateTime ExpiresIn { get; set; }
        public bool IsTokenValid()
        {
            return DateTime.Now < ExpiresIn;
        }
    }
}
