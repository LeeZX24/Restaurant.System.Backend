using Restaurant.System.Data.Interfaces;
using Restaurant.System.Models.Dtos;
using Restaurant.System.Models.Dtos.Shared;
using Restaurant.System.Models.Entities;
using Restaurant.System.Models.Enums;
using Restaurant.System.Models.Utils;
using Restaurant.System.Services.Interfaces.Maintenance;

namespace Restaurant.System.Services.Services.Maintenance
{
    public class StaffMaintenanceService : IStaffMaintenanceService
    {
        private readonly IStaffService _staffService;
        private readonly IRoleService _roleService;
        private readonly IStaffRolesService _staffRolesService;

        public StaffMaintenanceService(
            IStaffService StaffService,
            IRoleService RoleService,
            IStaffRolesService StaffRolesService
        )
        {
            _staffService = StaffService;
            _roleService = RoleService;
            _staffRolesService = StaffRolesService;
        }
        public async Task<List<StaffDto>> GetStaffListAsync()
        {
            List<StaffDto> staffDetailsList = new();

            var staffList = await _staffService.GetStaffList();

            foreach (Staff staff in staffList)
            {
                staffDetailsList.Add(new StaffDto
                {
                    Username = staff.Username,
                    Email = staff.Email,
                    LastName = staff.LastName,
                    FirstName = staff.FirstName
                });
            }

            return staffDetailsList;
        }

        public async Task<StaffDto> GetStaffDetailsAsync(string username)
        {
            var staff = _staffService.GetStaffDetail(username);

            return new StaffDto();
        }

        public async Task<StaffDto> AddNewStaffAsync(StaffDto staffDetails)
        {
            var staff = await _staffService.GetStaffDetail(staffDetails.Username);
            if (staff != null) throw new UnauthorizedAccessException("Staff Already Existed.");
            else
            {
                var tempPassword = AuthUtil.GenerateRandomPassword(12);
                staff = new Staff
                {
                    StaffId = Guid.CreateVersion7(),
                    Username = staffDetails.Username,
                    Email = staffDetails.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(tempPassword),
                    FirstName = staffDetails.FirstName ?? string.Empty,
                    LastName = staffDetails.LastName ?? string.Empty
                };

                await _staffService.AddNewStaff(staff);

                if (staffDetails.UserInfo.Roles != null)
                {

                    foreach (RoleDto role in staffDetails.UserInfo.Roles)
                    {
                        var userRole = await _roleService.GetRoleDetail(role.RoleCode) ?? throw new UnauthorizedAccessException($"Role '{role.RoleCode}' Not Found.");
                        
                        var staffRoles = new StaffRoles
                        {
                            StaffUsername = staff.Username,
                            RoleCode = userRole.RoleCode
                        };

                        await _staffRolesService.AddStaffRoles(staffRoles);
                    }
                }

                Console.WriteLine(tempPassword);
            }

            staffDetails.ResponseDetails.Status = Status.Success;
            staffDetails.ResponseDetails.Message = "Staff Adding Success.";

            

            return staffDetails;
        }

        public async Task<StaffDto> UpdateStaffDetailsAsync(StaffDto staffDetails)
        {
            var staff = await _staffService.GetStaffDetail(staffDetails.Username);

            staff.Email = staffDetails.UserInfo.AccountInfo.IsEmail ? staffDetails.Username : staffDetails.Email ?? string.Empty;
            staff.FirstName = staffDetails.FirstName ?? string.Empty;
            staff.LastName = staffDetails.LastName ?? string.Empty;

            var staffRolesList = await _staffRolesService.GetStaffRolesList(staff.Username);
            var incomingRoleList = staffDetails.UserInfo.Roles.Select(r => r.RoleCode).ToList();
            var existingRolesList = staffRolesList.Select(r => r.RoleCode);

            var toRemove = staffRolesList.Where(r => !incomingRoleList.Contains(r.RoleCode)).ToList();
            var toAdd = incomingRoleList.Where(r => !existingRolesList.Contains(r));

            foreach (string role in toAdd)
            {
                var staffRole = new StaffRoles
                {
                    StaffUsername = staff.Username,
                    RoleCode = role
                };

                await _staffRolesService.AddStaffRoles(staffRole);
            }

            foreach (StaffRoles staffRole in toRemove)
            {
                await _staffRolesService.RemoveStaffRoles(staffRole);
            }

            await _staffService.UpdateStaff(staff);

            staffDetails.ResponseDetails.Status = Status.Success;
            staffDetails.ResponseDetails.Message = "Staff Updated Success.";

            return staffDetails;
        }

        public async Task<StaffDto> DeleteStaffAsync(string username)
        {
            await _staffRolesService.RemoveAllStaffRoles(username);
            await _staffService.DeleteStaff(username);

            var result = new StaffDto
            {
                ResponseDetails = new ResponseDto
                {
                    Status = Status.Success,
                    Message = "Staff Delete Success."
                },
            };

            return result;
        }
    }
}