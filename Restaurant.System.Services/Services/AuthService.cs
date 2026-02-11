using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Restaurant.System.Data.Interfaces;
using Restaurant.System.Models.Dtos;
using Restaurant.System.Models.Dtos.Shared;
using Restaurant.System.Models.Entities;
using Restaurant.System.Models.Enums;
using Restaurant.System.Services.Interfaces;

namespace Restaurant.System.Services.Services
{
  public class AuthService : IAuthService
  {
    private readonly ICustomerService _customerService;
    private readonly IMemberService _memberService;
    private readonly IStaffService _staffService;
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public AuthService(
        ICustomerService CustomerService,
        IMemberService MemberService,
        IStaffService StaffService,
        IUserService UserService,
        IConfiguration Configuration)
    {
      _customerService = CustomerService;
      _memberService = MemberService;
      _staffService = StaffService;
      _userService = UserService;
      _configuration = Configuration;
    }

    public async Task<UserDto> LoginAsync(UserDto login)
    {
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

        login.UserType = UserType.Member;
        login.Token = GenerateJwtToken(member.MemberId, member.Email, "Member", UserType.Member.ToString());
        login.ExpireAt = DateTime.UtcNow.AddHours(1);
        login.Status = Status.Success;
        login.ResponseDetails = new ResponseDto
        {
          Message = "Login Success."
        };

        return login;
      }
      else
      {
        var staff = await _staffService.GetStaffDetail(login.Identifier)
          ?? throw new UnauthorizedAccessException("Staff Not Found. Please Contact Administrator.");

        if (login.Password == null || !BCrypt.Net.BCrypt.Verify(login.Password, staff.Password))
        {
          throw new UnauthorizedAccessException("Invalid Username or Password.");
        }

        var role = staff.Roles.FirstOrDefault();

        var userSession = await _userService.GetUserSession(staff.Id.ToString());
        if (userSession == null)
        {
          await _userService.AddUserSession(staff.Id.ToString(), UserType.Staff);
        }
        else
        {
          await _userService.UpdateUserLogin(staff.Id.ToString());
        }

        login.UserType = UserType.Member;
        login.Token = GenerateJwtToken(staff.Id.ToString(), staff.UserName, role.RoleCode, role.RoleName);
        login.ExpireAt = DateTime.UtcNow.AddHours(1);
        login.Status = Status.Success;
        login.ResponseDetails = new ResponseDto
        {
          Message = "Login Success."
        };

        return login;
      }
    }

    public async Task<UserDto> RegisterAsync(UserDto register)
    {
      var member = await _memberService.GetMemberByEmail(register.Identifier);
      if (member != null) throw new UnauthorizedAccessException("Email Already Registered.");
      else
      {
        var customer = await _customerService.GetCurrentCustomer(register.CustomerId);
        if (customer != null)
        {
          customer.IsAnonymous = false;
        }
        else
        {
          //Registering
          customer = new Customer
          {
            CustomerId = register.CustomerId,
            IsAnonymous = false
          };

          await _customerService.AddNewCustomer(customer);
        }

        member = new Member
        {
          MemberId = new Guid().ToString(),
          CustomerId = register.CustomerId,
          Email = register.IsEmail ? register.Identifier : string.Empty,
          Username = register.IsEmail ? string.Empty : register.Identifier,
          Password = BCrypt.Net.BCrypt.HashPassword(register.Password)
        };

        await _memberService.AddNewMember(member);
      }
      register.Status = Status.Success;
      register.ResponseDetails = new ResponseDto
      {
        Message = "Member Registration Success."
      };

      return register;


      // var cust = await _customerService.GetCurrentCustomer(customer.CurrentCustomerNumber);
      // if (!cust.IsAnonymous) return null;

      // var member = await _memberService.GetMemberByCustomer(cust.CustomerId);
      // if (member != null) return null;

      // string Password = BCrypt.Net.BCrypt.HashPassword(member.Password);

      // var newMember = new Member
      // {
      //   Email = customer.Email,
      //   Username = customer.UserName,
      //   Password = Password,
      //   MemberId = "1",
      //   Customer = new Customer { CustomerId = customer.CurrentCustomerNumber, IsAnonymous = false, CreatedAt = DateTime.UtcNow },
      //   CustomerId = customer.CurrentCustomerNumber,
      //   FirstName = customer.FirstName,
      //   LastName = customer.LastName,
      //   DateOfBirth = customer.DateOfBirth,
      //   LoginDateTime = DateTime.UtcNow,
      //   LogoutDateTime = DateTime.UtcNow,
      // };

      // await _memberRepository.AddAsync(newMember);

      // await _memberRepository.SaveChangesAsync();

      // var userDto = new UserDto
      // {
      //   Email = customer.Email,
      //   UserName = customer.UserName,
      //   Password = Password,
      //   Token = GenerateJwtToken(member.MemberId.ToString(), member.Email, "User", "Customer")
      // };

      // return userDto;
    }

    private string GenerateJwtToken(string userId, string userName, string role, string userType)
    {
      var keyString = _configuration["JwtSettings:Key"]
      ?? throw new InvalidOperationException("JWT Key is missing from configuration.");
      var _issuer = _configuration["JwtSettings:Issuer"] ?? throw new InvalidOperationException("Issuer nut found.");
      var _audience = _configuration["JwtSettings:Audience"] ?? throw new InvalidOperationException("Audience nut found.");

      double durationMinutes = 60;
      if (!double.TryParse(_configuration["JwtSettings:DurationInMinutes"], out durationMinutes))
        durationMinutes = 60;

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var claims = new List<Claim>
      {
        new(JwtRegisteredClaimNames.Sub, userId), // Standard ID claim
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