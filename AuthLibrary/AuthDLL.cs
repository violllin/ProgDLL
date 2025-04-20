using System;
using System.Collections.Generic;
using System.IO;

namespace AuthDLL
{
    public class AuthUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Dictionary<string, int> MenuPermissions { get; set; } = new Dictionary<string, int>();
    }

    public class AuthManager
    {
        private List<AuthUser> users = new List<AuthUser>();

        public AuthManager(string usersFile = "USERS.txt")
        {
            LoadUsers(usersFile);
        }

        private void LoadUsers(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"File {filename} not found");
            }

            AuthUser currentUser = null;
            var lines = File.ReadAllLines(filename);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (line.StartsWith("#"))
                {
                    var parts = line.Substring(1).Split(new[] { ' ' }, 2);
                    if (parts.Length == 2)
                    {
                        currentUser = new AuthUser { Username = parts[0], Password = parts[1] };
                        users.Add(currentUser);
                    }
                }
                else if (currentUser != null)
                {
                    var parts = line.Split(new[] { ' ' }, 2);
                    if (parts.Length == 2 && int.TryParse(parts[1], out int status))
                    {
                        currentUser.MenuPermissions[parts[0]] = status;
                    }
                }
            }
        }

        public AuthUser Authenticate(string username, string password)
        {
            return users.Find(u => u.Username == username && u.Password == password);
        }
    }
}