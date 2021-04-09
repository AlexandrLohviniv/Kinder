using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace KinderApi
{
    public class AuthOptions
    {
        public const string ISSUER = "Kinder";
        public const string AUDIENCE = "AuthClient";
        const string KEY = "Super secret key";
        public const int LIFETIME = 60;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
