namespace WebApi.Extension
{
    public class AuthAttribute : Attribute
    {
        public string menuCode { get; }
        public string[] actions { get; }

        public AuthAttribute(string menuCode, params string[] actions)
        {
            this.menuCode = menuCode;
            this.actions = actions;
        }
    }
}
