using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentCapitalBlazor.Data;

namespace ParentCapitalBlazor.Services
{
    public class Registration
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<Registration> _logger;

        public Registration(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<Registration> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task AddDefaultRolesAsync()
        {
            try
            {
                await AddRolesHelper("Пользователи"); // по умолчанию роль для всех пользователей (без доступа к ПДн)
                await AddRolesHelper("Администраторы"); // роль администратора (с доступом к справочникам, админке и ПДН)
                await AddRolesHelper("Департамент"); // роль Департамента (просмотр всего, редактирование Сертификатов)
                await AddRolesHelper("ОСЗН"); // роль ОСЗН (просмотр всего, редактирование распоряжений)
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task AddRolesHelper(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public async Task AddDefaultUserRole(ApplicationUser user)
        {
            try
            {
                // Если в системе зарегистрирован один пользователь, даем ему роль "Администраторы".
                // иначе, по умолчанию присваеваем всем роль "Пользователи"
                var users = _userManager.Users.ToList();
                var administrators = await _userManager.GetUsersInRoleAsync("Администраторы");
                if (users.Count == 1 && administrators.Count == 0)
                    await _userManager.AddToRoleAsync(user, "Администраторы");
                else
                    await _userManager.AddToRoleAsync(user, "Пользователи");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
