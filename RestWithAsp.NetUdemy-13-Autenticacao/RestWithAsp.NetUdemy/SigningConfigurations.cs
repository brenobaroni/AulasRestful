using Microsoft.IdentityModel.Tokens;

namespace RestWithAsp.NetUdemy
{
    internal class SigningConfigurations
    {
        public SigningConfigurations()
        {
        }

        public SecurityKey Key { get; internal set; }
    }
}