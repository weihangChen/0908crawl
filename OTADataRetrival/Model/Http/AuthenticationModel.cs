namespace ConsoleApplication1.Model.Http
{
    /// <summary>
    /// http authentication
    /// </summary>
    public class AuthenticationModel
    {

    }

    /// <summary>
    /// for basic authentication
    /// </summary>
    public class BasicAuthenticationModel : AuthenticationModel
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public BasicAuthenticationModel(string Name, string Password)
        {
            this.Name = Name;
            this.Password = Password;
        }

    }
}

