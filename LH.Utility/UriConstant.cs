using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LH.Utility
{
    public static class UriConstant
    {
        public const string RedirectUri = "http://localhost:5000/api/LInCallback";
        public const string AccessTokenUri = "https://www.linkedin.com/oauth/v2/accessToken";
        public const string UserInfoUri = "https://api.linkedin.com/v2/userinfo";
    }
}
