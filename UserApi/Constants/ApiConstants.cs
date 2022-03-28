using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi.Constants
{
    public static class JwtConfig
    {
        public static string JwtSecret = "somerandomsecret";
    }

    public class FieldConstants
    {
        public const string EMAIL_PATTERN = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public const int MAX_NAME_LEN = 100;
        public const int MAX_USER_NAME_LEN = 80;
        public const int MAX_SECRET_LEN = 32;
    }
}
