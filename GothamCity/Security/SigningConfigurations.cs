using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GothamCity.Security
{
    public class SigningConfigurations
    {
        private const string SECRET_KEY = "c1f51f42-5227-4d15-b787-c6bbbb645024";

        public SigningCredentials SigningCredentials { get; }

        private readonly SymmetricSecurityKey _signingkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY));

        public SigningConfigurations()
        {
            SigningCredentials = new SigningCredentials(_signingkey, SecurityAlgorithms.HmacSha256);
        }
    }
}
