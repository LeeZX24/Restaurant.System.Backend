using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Restaurant.System.Data.Interfaces;
using Restaurant.System.Models.Dtos.Requests;
using Restaurant.System.Models.Dtos.Responses;
using Restaurant.System.Models.Entities;
using Restaurant.System.Models.Enums;
using Restaurant.System.Services.Interfaces.Auth;

namespace Restaurant.System.Services.Services.Auth
{
  public class AuthService : IAuthService
  {
    private readonly ICustomerService _customerService;
    private readonly IMemberService _memberService;
    private readonly IStaffService _staffService;
    private readonly IUserService _userService;
    private readonly IStaffRolesService _staffRolesService;
    private readonly IConfiguration _configuration;

    public AuthService(
        ICustomerService CustomerService,
        IMemberService MemberService,
        IStaffService StaffService,
        IUserService UserService,
        IStaffRolesService StaffRolesService,
        IConfiguration Configuration)
    {
      _customerService = CustomerService;
      _memberService = MemberService;
      _staffService = StaffService;
      _userService = UserService;
      _staffRolesService = StaffRolesService;
      _configuration = Configuration;
    }

    public async Task<AppLoginResponse> LoginAsync(AppLoginRequest login)
    {
      AppLoginResponse response = new();

      if (login.IsEmail)
      {
        var member = await _memberService.GetMemberByEmail(login.Identifier);
        if (member == null) throw new UnauthorizedAccessException("Member Not Found. Please Register.");

        if (login == null || !BCrypt.Net.BCrypt.Verify(login.Password, member.Password))
        {
          throw new UnauthorizedAccessException("Invalid Email or Password.");
        }

        var userSession = await _userService.GetUserSession(member.MemberId);
        if (userSession == null)
        {
          await _userService.AddUserSession(member.MemberId, UserType.Member);
          await _userService.UpdateUserLogin(member.MemberId);
        }
        else
        {
          await _userService.UpdateUserLogin(member.MemberId);
        }

        response.Token = GenerateJwtToken(member.MemberId, member.Email, "Member", UserType.Member.ToString());
        response.ExpireAt = DateTime.UtcNow.AddHours(1);

        response.Status = Status.Success;
        response.Message = "Login Success.";

        response.UserInfo = new() {
          UserType = UserType.Member,
          CustomerId = member.CustomerId,
          ProfileInfo = new() {
            LastName = member.LastName,
            FirstName = member.FirstName,
          },
          AccountInfo = new(){
            Identifier = login.Identifier,
            Email = member.Email
          },
          SecurityInfo = new(){
            IsEmailVerified = true,
            IsMfaEnabled = true
          }
        };

        return response;
      }
      else
      {
        Console.WriteLine("Login Details -> ", login);

        var staff = await _staffService.GetStaffDetail(login.Identifier)
          ?? throw new UnauthorizedAccessException("Staff Not Found. Please Contact Administrator.");

        if (login.Password == null || !BCrypt.Net.BCrypt.Verify(login.Password, staff.Password))
        {
          throw new UnauthorizedAccessException("Invalid Username or Password.");
        }

        var staffRolesList = await _staffRolesService.GetStaffRolesList(staff.Username);

        Role role = new Role
        {
          RoleCode = "ADM",
          RoleName = "Administrator"
        };

        if (staffRolesList != null)
        {
          if (staffRolesList.Count != 0)
          {
            role = staffRolesList.FirstOrDefault().Role;
          }
        }

        var userSession = await _userService.GetUserSession(staff.StaffId);
        if (userSession == null)
        {
          await _userService.AddUserSession(staff.StaffId, UserType.Staff);
        }
        else
        {
          await _userService.UpdateUserLogin(staff.StaffId);
        }

        response.Token = GenerateJwtToken(staff.StaffId, staff.Username, role.RoleCode, role.RoleName);
        response.ExpireAt = DateTime.UtcNow.AddHours(1);

        response.Status = Status.Success;
        response.Message = "Login Success.";

        response.UserInfo = new() {
          UserType = UserType.Staff,
          ProfileInfo = new() {
            LastName = staff.LastName,
            FirstName = staff.FirstName,
          },
          AccountInfo = new(){
            Identifier = login.Identifier,
            Username = staff.Username,
            Email = staff.Email,
          },
          SecurityInfo = new(){
            IsEmailVerified = true,
            IsMfaEnabled = true
          }
        };

        return response;
      }
    }

    public async Task<AppLogoutResponse> LogoutAsync(AppLogoutRequest logout)
    {
      var userId = Guid.Empty;
      var Identifier = logout.UserInfo.AccountInfo.Identifier;
      
      if (logout.UserInfo.AccountInfo.IsEmail)
        userId = (await _memberService.GetMemberByEmail(Identifier)).MemberId;
      else
        userId = (await _staffService.GetStaffDetail(Identifier)).StaffId;

      await _userService.UpdateUserLogout(userId);

      return new() { Message = "Logout Success.", Status = Status.Success };
    }

    public async Task<AppRegisterResponse> RegisterAsync(AppRegisterRequest register)
    {
      var response = new AppRegisterResponse();

      var member = await _memberService.GetMemberByEmail(register.Identifier);
      if (member != null) throw new UnauthorizedAccessException("Email Already Registered.");
      else
      {
        var customerId = register.CustomerId ?? Guid.Empty;
        if(customerId == Guid.Empty) throw new UnauthorizedAccessException("Please register as Customer.");
        else
        {
          var customer = await _customerService.GetCurrentCustomer(customerId);
          
          if (customer != null) customer.IsAnonymous = false;
          else
          {
            //Registering
            customer = new()
            {
              CustomerId = customerId,
              IsAnonymous = false
            };

            await _customerService.AddNewCustomer(customer);
          }

          member = new Member
          {
            MemberId = Guid.CreateVersion7(),
            CustomerId = customerId,
            Email = register.IsEmail ? register.Identifier : string.Empty,
            Username = register.IsEmail ? string.Empty : register.Identifier,
            Password = BCrypt.Net.BCrypt.HashPassword(register.Password)
          };
          
          await _memberService.AddNewMember(member);

          response.Status = Status.Success;
          response.Message = "Member Registration Success.";
        }
      }

      return response;
    }

    private string GenerateJwtToken(Guid userId, string userName, string role, string userType)
    {
      var keyString = _configuration["JwtSettings:Key"]
      ?? throw new InvalidOperationException("JWT Key is missing from configuration.");
      var _issuer = _configuration["JwtSettings:Issuer"] ?? throw new InvalidOperationException("Issuer not found.");
      var _audience = _configuration["JwtSettings:Audience"] ?? throw new InvalidOperationException("Audience not found.");

      double durationMinutes = 60;
      if (!double.TryParse(_configuration["JwtSettings:DurationInMinutes"], out durationMinutes))
        durationMinutes = 60;

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var claims = new List<Claim>
      {
        new(JwtRegisteredClaimNames.Sub, userId.ToString()), // Standard ID claim
        new(JwtRegisteredClaimNames.UniqueName, userName), // Standard Name claim
        new(ClaimTypes.Role, role),             // Standard Role claim
        new("UserType", userType),            // Custom claim (Staff vs Customer)
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new(JwtRegisteredClaimNames.Iat,
                  DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
      };

      // D. Define Token Descriptor
      var token = new JwtSecurityToken(
          issuer: _issuer,
          audience: _audience,
          claims: claims,
          expires: DateTime.UtcNow.AddMinutes(durationMinutes),
          signingCredentials: creds
      );

      // E. Write Token to String
      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}