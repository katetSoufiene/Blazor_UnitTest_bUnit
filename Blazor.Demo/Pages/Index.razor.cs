using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Demo.Pages
{
    partial class Index
    {
        [Inject]
        private IAuthetificationService AuthetificationService { get; set; }

        private User User = new();
        private string ErrorMessage = string.Empty;
        private void Login()
        {
            ErrorMessage = string.Empty;

            if (!AuthetificationService.IsValidLogin(User.Email, User.Password))
            {
                ErrorMessage = "Email/Password Invalid";
            }
        }
    }
}
