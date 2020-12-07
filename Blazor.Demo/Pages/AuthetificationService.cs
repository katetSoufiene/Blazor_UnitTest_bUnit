using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Demo.Pages
{
    public class AuthetificationService : IAuthetificationService
    {
        public bool IsValidLogin(string email, string password)
        {
            if (string.IsNullOrEmpty(email)) return false;
            if (string.IsNullOrEmpty(password)) return false;
            return true;
        }
    }
}
