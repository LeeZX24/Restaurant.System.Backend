using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Restaurant.System.Data;
using Restaurant.System.Data.Interfaces;
using Restaurant.System.Models.Dtos;
using Restaurant.System.Models.Dtos.Shared;
using Restaurant.System.Models.Entities;
using Restaurant.System.Services.Interfaces;

namespace Restaurant.System.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly ICustomerService _customerService;
        private readonly IMemberService _memberService;
        private readonly IStaffService _staffService;
        private readonly IConfiguration _configuration;
        private readonly IRepository<Member> _memberRepository;

        public AuthService(
            ICustomerService CustomerService,
            IMemberService MemberService,
            IStaffService StaffService,
            IConfiguration Configuration,
            IRepository<Member> MemberRepository)
        {
            _customerService = CustomerService;
            _memberService = MemberService;
            _staffService = StaffService;
            _configuration = Configuration;
            _memberRepository = MemberRepository;
        }

        public async Task<UserDto> LoginAsync(UserDto user)
        {
            var userDto = new UserDto();
            if(!string.IsNullOrEmpty(user.Email))
            {
                var customer = await _customerService.GetCurrentCustomer("123");
                if(customer != null)
                {
                    var member = await _memberService.GetMemberByCustomer(customer.CustomerId);

                if (member != null)
                {
                    //Check Password
                    // var passwordValid = BCrypt.Net.BCrypt.Verify(user.Password, member.Password);
                    // if(passwordValid)

                    if(user.Password == member.Password)
                    {
                        userDto.Email = user.Email;
                        userDto.Password = user.Password;
                        userDto.Token = GenerateJwtToken(member.MemberId.ToString(), member.Email, "User", "Customer") ?? "1";
                    }
                    else return null;
                }
                else return null;
                }
                else return null;
            }

            if(!string.IsNullOrEmpty(user.UserName))
            {
                var staff = await _staffService.GetStaffDetail(user.UserName);
                if (staff != null)
                {
                    //Check Password
                    // var passwordValid = BCrypt.Net.BCrypt.Verify(user.Password, staff.Password);
                    // if (!passwordValid) return null;

                    if(user.Password == staff.Password)
                    {
                        if(staff.Roles != null && staff.Roles.Count > 0)
                        {
                            var role = staff.Roles.FirstOrDefault();
                            userDto.UserName = user.UserName;
                            userDto.Password = user.Password;
                            userDto.Token = GenerateJwtToken(staff.Id.ToString(), staff.UserName, role.RoleCode, role.RoleName) ?? "1";
                        }
                    }
                    else return null;
                }
                else return null;
            }

            return userDto;
        }

        public async Task<UserDto> RegisterAsync(CustomerDto customer)
        {
            var cust = await _customerService.GetCurrentCustomer(customer.CurrentCustomerNumber);
            if(!cust.IsAnonymous) return null;

            var member = await _memberService.GetMemberByCustomer(cust.CustomerId);
            if (member != null) return null;

            string Password = BCrypt.Net.BCrypt.HashPassword(customer.Password);
            
            var newMember = new Member
            {
              Email = customer.Email,
              Username = customer.UserName,
              Password = Password,
              MemberId = "1",
              Customer = new Customer{ CustomerId = customer.CurrentCustomerNumber, IsAnonymous=false, CreatedAt = DateTime.Now },
              CustomerId = customer.CurrentCustomerNumber,
              FirstName = customer.FirstName,
              LastName = customer.LastName,
              DateOfBirth = customer.DateOfBirth,
              LoginDateTime = DateTime.Now,
              LogoutDateTime = DateTime.Now,
            };

            await _memberRepository.AddAsync(newMember);
            
            await _memberRepository.SaveChangesAsync();

            var userDto = new UserDto
            {
                Email = customer.Email,
                UserName = customer.UserName,
                Password = Password,
                Token = GenerateJwtToken(member.MemberId.ToString(), member.Email, "User", "Customer")
            };
            
            return userDto;
        }

        private string GenerateJwtToken(string userId, string userName, string role, string userType)
        {
            // A. Create Claims (The data inside the token)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId), // Standard ID claim
                new Claim(ClaimTypes.Name, userName),         // Standard Name claim
                new Claim(ClaimTypes.Role, role),             // Standard Role claim
                new Claim("UserType", userType)               // Custom claim (Staff vs Customer)
            };

            // B. Get Key from Config
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));

            // C. Create Credentials (Sign the token)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // D. Define Token Descriptor
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: creds
            );

            // E. Write Token to String
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}