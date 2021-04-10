using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace KinderApi
{
    public class AuthOptions
    {
        const string KEY = "Super secret key";
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
