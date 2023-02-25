using ePizza.Entities.Concrete;
using ePizza.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Services.Implementations
{
    public class AuthenticationManager : IAuthenticationService
    {
        protected SignInManager<User> _signManager;
        protected UserManager<User> _userManager;
        protected RoleManager<Role> _roleManager;

        public AuthenticationManager(SignInManager<User> signManager, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _signManager = signManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public User AuthenticateUser(string userName, string password)
        {
            var result = _signManager.PasswordSignInAsync(userName, password, false ,lockoutOnFailure: false).Result;
            if (result.Succeeded)
            {
                var user = _userManager.FindByNameAsync(userName).Result;
                var roles = _userManager.GetRolesAsync(user).Result;
                user.Roles = roles.ToArray();
                return user;
            }
            return null;
        }

        public bool CreateUser(User user, string password)
        {
            var result = _userManager.CreateAsync(user, password).Result;
            if (result.Succeeded)
            {
                //projenin ilerleyen süreçlerinde burası dinamik olmalıdır. googleden asp ıdentity örneklerine bakabilirsiniz.
                string role = "User";
                //string role = "Admin";
                var res = _userManager.AddToRoleAsync(user, role).Result;
                if (res.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public User GetUser(string userName)
        {
            return _userManager.FindByNameAsync(userName).Result;
        }

        public async Task<bool> SignOut()
        {
            //Signout yapısı hata mekanızması gönderir hale getirmelidir. geriye muhakkak bir değer gönmelidir. ve bu değer işlemin hatalı olduğunu göstermelidir.
            await _signManager.SignOutAsync();
            return true;
        }
    }
}
