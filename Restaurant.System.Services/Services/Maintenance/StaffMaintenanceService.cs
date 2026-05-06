using Restaurant.System.Data.Interfaces;
using Restaurant.System.Models.Dtos;
using Restaurant.System.Models.Entities;
using Restaurant.System.Models.Enums;
using Restaurant.System.Services.Interfaces;

namespace Restaurant.System.Services.Services
{
    public class StaffMaintenanceService : IStaffMaintenanceService
    {
        private readonly IStaffService _staffService;
        private readonly IStaffRolesService _staffRolesService;

        public StaffMaintenanceService(
            IStaffService StaffService,
            IStaffRolesService StaffRolesService
        )
        {
            _staffService = StaffService;
            _staffRolesService = StaffRolesService;
        }
        public async Task<List<StaffDto>> GetStaffListAsync()
        {
            List<StaffDto> staffDetailsList = new List<StaffDto>();

            var staffList = await _staffService.GetStaffList();

            // foreach (Staff staff in staffList)
            // {
            //     staffDetailsList.Add()
            // }
            return staffDetailsList;
        }

        public async Task<StaffDto> GetStaffDetailsAsync(string username)
        {
            var staff = _staffService.GetStaffDetail(username);

            return new StaffDto();
        }

        public async Task<StaffDto> AddNewStaffAsync(StaffDto staffDetails)
        {
            var staff = await _staffService.GetStaffDetail(staffDetails.Identifier);
            if (staff != null) throw new UnauthorizedAccessException("Staff Already Existed.");
            else
            {
                staff = new Staff
                {
                    StaffId = Guid.CreateVersion7(),
                    Username = staffDetails.IsEmail ? staffDetails.Username ?? string.Empty : staffDetails.Identifier,
                    Email = staffDetails.IsEmail ? staffDetails.Identifier : staffDetails.Email ?? string.Empty,
                    Password = BCrypt.Net.BCrypt.HashPassword(staffDetails.Password),
                    FirstName = staffDetails.FirstName ?? string.Empty,
                    LastName = staffDetails.LastName ?? string.Empty,
                };

                await _staffService.AddNewStaff(staff);

                foreach (Role role in staffDetails.RoleList)
                {
                    var staffRoles = new StaffRoles
                    {
                        StaffUsername = staff.Username,
                        RoleCode = role.RoleCode
                    };

                    await _staffRolesService.AddStaffRoles(staffRoles);
                }
            }

            staffDetails.Status = Status.Success;
            staffDetails.ResponseDetails = new ResponseDto
            {
                Message = "Staff Adding Success."
            };

            return staffDetails;
        }

        public async Task<StaffDto> UpdateStaffDetailsAsync(StaffDto staffDetails)
        {
            var staff = await _staffService.GetStaffDetail(staffDetails.Identifier);

            staff.Email = staffDetails.IsEmail ? staffDetails.Identifier : staffDetails.Email ?? string.Empty;
            staff.Password = BCrypt.Net.BCrypt.HashPassword(staffDetails.Password);
            staff.FirstName = staffDetails.FirstName ?? string.Empty;
            staff.LastName = staffDetails.LastName ?? string.Empty;

            var staffRolesList = await _staffRolesService.GetStaffRolesList(staff.Username);
            var incomingRoleList = staffDetails.RoleList.Select(r => r.RoleCode).ToList();
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

            staffDetails.Status = Status.Success;
            staffDetails.ResponseDetails = new ResponseDto
            {
                Message = "Staff Updated Success."
            };

            return staffDetails;
        }

        public async Task<StaffDto> DeleteStaff(string username)
        {
            await _staffRolesService.RemoveAllStaffRoles(username);
            await _staffService.DeleteStaff(username);

            var result = new StaffDto
            {
                Status = Status.Success,
                ResponseDetails = new ResponseDto
                {
                    Message = "Staff Delete Success."
                },
            };

            return result;
        }
    }
}