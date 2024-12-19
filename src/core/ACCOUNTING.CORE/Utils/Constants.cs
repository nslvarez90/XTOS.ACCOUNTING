using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCOUNTING.CORE.Utils
{
    public static class Constants
    {
        public const string SERVER_URL = "base.routing";
        public const string API_VERSION = "v1";

        /**
         * AUTH ENDPOINTS
         */

        public const string END_LOGIN = "/login";
        public const string END_LOGIN_TOKEN = "/login";
        public const string END_REGISTER = "/register";

        /**
         * GET VALUES OF AGENTS
         */

        public const string ACTIVE_AGENTS = "/activeAgente?weeks=";
        public const string INACTIVE_AGENTS = "/inactiveAgente";

    }
}
