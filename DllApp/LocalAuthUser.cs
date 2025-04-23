using System.Collections.Generic;

namespace DllApp
{
    public class LocalAuthUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Dictionary<string, int> MenuPermissions { get; set; } = new Dictionary<string, int>();
    }

}
