using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Restaurants.DTOS.Account.Read;
using Restaurant.Application.Restaurants.DTOS.Account.Write;
using Restaurant.Application.RestaurantService.IService;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Entities.Roles;
using System.Security.Claims;

namespace Restaurant.Application.RestaurantService.Service
{
    public class AccountService : IAccountService
    {
        #region Context
        private readonly UserManager<User> userManager;
        private readonly ILogger<AccountService> logger;
        private readonly RoleManager<User> roleManager;

        public AccountService(UserManager<User> userManager , 
            ILogger<AccountService> logger , RoleManager<User> roleManager)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.roleManager = roleManager;
        }
        #endregion

        //public async Task<IdentityResult> RegisterAsync(RegisterDto dto)
        //{
        //    // check if email is exist or not
        //    var existingUser = await userManager.FindByEmailAsync(dto.Email);
        //    if (existingUser != null)
        //    {
        //        return IdentityResult.Failed(new IdentityError
        //        {
        //            Code = "DuplicateEmail",
        //            Description = "Email is already registered."
        //        });
        //    }

        //    // Create user instance
        //    var user = new User
        //    {
        //        FirstName = dto.FirstName,
        //        LastName = dto.LastName,
        //        UserName = dto.Email.Split('@')[0],
        //        Email = dto.Email,
        //        PhoneNumber = dto.PhoneNumber,
        //    };

        //    // Create user in Identity
        //    var result = await userManager.CreateAsync(user, dto.Password);
        //    if (!result.Succeeded)
        //    {
        //        logger.LogWarning("User creation failed: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
        //        return result;
        //    }

        //    // Add Role to User


        //}


        //public Task<TokenDto?> LoginAsync(LoginDto dto)
        //{
        //    throw new NotImplementedException();
        //}

        #region Comman Methods
        //private async Task EnsureRolesExist()
        //{
        //    if (!await roleManager.RoleExistsAsync(Roles.Admin.ToString()))
        //        await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));

        //    if (!await roleManager.RoleExistsAsync(Roles.Visitor.ToString()))
        //        await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));

        //    if (!await roleManager.RoleExistsAsync(Roles.Visitor.ToString()))
        //        await roleManager.CreateAsync(new IdentityRole(Roles.Visitor.ToString()));
        //}
        //private async Task<IdentityResult> AssignUserRole(User user, string role)
        //{
        //    //Validate requested role
        //    if (role != "Admin" && role != "customer")
        //    {
        //        return IdentityResult.Failed(new IdentityError
        //        {
        //            Code = "InvalidRole",
        //            Description = "Invalid role specified"
        //        });
        //    }

        //    //Assign role
        //    return await userManager.AddToRoleAsync(user, role);
        //}
        //private async Task<IdentityResult> AddUserClaims(User user)
        //{
        //    var claims = new List<Claim>   // like identify for any one 
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, user.Id),
        //        new Claim(ClaimTypes.Email, user.Email),
        //    };

        //    return await userManager.AddClaimsAsync(user, claims);
        //}

        //private async Task CreateJwtToken (User user)
        //{

        //}
        #endregion
    }
}
