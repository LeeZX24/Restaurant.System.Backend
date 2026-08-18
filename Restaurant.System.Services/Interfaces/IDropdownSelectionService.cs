using Restaurant.System.Models.Dtos;

namespace Restaurant.System.Services.Interfaces
{
    public interface IDropdownSelectionService
    {
        public Task<List<DropdownDto>> GetDropdownList();
        public Task<List<DropdownDto>> GetDropdownListByCategory(string category);
        public Task<List<DropdownDto>> GetDropdownListByCategoryTags(string category, string tags);
    }
}