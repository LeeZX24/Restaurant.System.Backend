using Restaurant.System.Data.Interfaces;
using Restaurant.System.Models.Dtos;
using Restaurant.System.Models.Entities;

namespace Restaurant.System.Data.Repositories
{
    public class DropdownService : IDropdownService
    {
        private readonly IRepository<Dropdown> _dropdownRepository;

        public DropdownService(IRepository<Dropdown> DropdownRepository)
        {
            _dropdownRepository = DropdownRepository;
        }

        public async Task<List<Dropdown>> GetDropdownList() => await _dropdownRepository.GetAllAsync();

        public async Task<List<Dropdown>> GetDropdownListByCategory(string category)
            => await _dropdownRepository.GetByFieldAsync(e => e.Category == category);

        public async Task<List<Dropdown>> GetDropdownListByCategoryTag(string category, string tag)
            => await _dropdownRepository.GetByFieldAsync(e => e.Category == category && e.Tags == tag);

        public async Task<Dropdown> GetDropdownDetails(DropdownDto dropdownData)
        {
            var dropdown = (await _dropdownRepository.GetByFieldAsync(e => e.Category == dropdownData.Category && e.Code == dropdownData.Code)).FirstOrDefault();

            return dropdown;
        }

        public async Task CreateNewDropdown(Dropdown dropdownData)
        {
            await _dropdownRepository.AddAsync(dropdownData);
            await _dropdownRepository.SaveChangesAsync();
        }

        public async Task UpdateExistingDropdown(Dropdown dropdownData)
        {
            await _dropdownRepository.UpdateByFieldAsync(
                dropdown => dropdown.Category == dropdownData.Category && dropdown.Code == dropdownData.Code,
                dropdown => dropdown,
                dropdown => dropdownData
                );
            await _dropdownRepository.SaveChangesAsync();
        }

        public async Task DeleteDropdown(Dropdown dropdownData) => await _dropdownRepository.DeleteByFieldAsync(
            dropdown => dropdown.Category == dropdownData.Category && dropdown.Code == dropdownData.Code
        );
    }
}