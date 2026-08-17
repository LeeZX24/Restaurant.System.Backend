using Restaurant.System.Models.Dtos;
using Restaurant.System.Models.Entities;

namespace Restaurant.System.Data.Interfaces
{
    public interface IDropdownService
    {
        Task<List<Dropdown>> GetDropdownList();
        Task<List<Dropdown>> GetDropdownListByCategory(string category);
        Task<List<Dropdown>> GetDropdownListByCategoryTag(string category, string tag);
        Task<Dropdown> GetDropdownDetails(DropdownDto dropdownData);
        Task CreateNewDropdown(Dropdown dropdownData);
        Task UpdateExistingDropdown(Dropdown dropdownData);
        Task DeleteDropdown(Dropdown dropdownData);
    }
}