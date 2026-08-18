using Restaurant.System.Data.Interfaces;
using Restaurant.System.Data.Repositories;
using Restaurant.System.Models.Dtos;
using Restaurant.System.Models.Entities;
using Restaurant.System.Services.Interfaces;

namespace Restaurant.System.Services.Services
{
    public class DropdownSelectionService : IDropdownSelectionService
    {
        private readonly IDropdownService _dropdownService;

        public DropdownSelectionService(IDropdownService dropdownService)
        {
            _dropdownService = dropdownService;
        }

        public async Task<List<DropdownDto>> GetDropdownList()
        {
            List<DropdownDto> dropdownList = new List<DropdownDto>();
            var dropdownDataList = await _dropdownService.GetDropdownList();

            foreach (Dropdown dropdownData in dropdownDataList)
            {
                dropdownList.Add(new DropdownDto
                {
                    CategoryDD = dropdownData.Category,
                    Category = dropdownData.Category,
                    Code = dropdownData.Code,
                    Description = dropdownData.Description,
                    SeqNo = dropdownData.Sequence,
                    Tags = dropdownData.Tags ?? "" ,
                    UpdatedDate = dropdownData.UpdatedDate
                });
            }

            return dropdownList;
        }

        public async Task<List<DropdownDto>> GetDropdownListByCategory(string category)
        {
            List<DropdownDto> dropdownList = new List<DropdownDto>();
            var dropdownDataList = await _dropdownService.GetDropdownListByCategory(category);

            foreach (Dropdown dropdownData in dropdownDataList)
            {
                dropdownList.Add(new DropdownDto
                {
                    Category = dropdownData.Category,
                    Code = dropdownData.Code,
                    Description = dropdownData.Description,
                    SeqNo = dropdownData.Sequence,
                    Tags = dropdownData.Tags,
                    UpdatedDate = dropdownData.UpdatedDate
                });
            }

            return dropdownList;
        }

        public async Task<List<DropdownDto>> GetDropdownListByCategoryTags(string category, string tags)
        {
            List<DropdownDto> dropdownList = new List<DropdownDto>();
            var dropdownDataList = await _dropdownService.GetDropdownListByCategoryTag(category, tags);

            foreach (Dropdown dropdownData in dropdownDataList)
            {
                dropdownList.Add(new DropdownDto
                {
                    Category = dropdownData.Category,
                    Code = dropdownData.Code,
                    Description = dropdownData.Description,
                    SeqNo = dropdownData.Sequence,
                    Tags = dropdownData.Tags,
                    UpdatedDate = dropdownData.UpdatedDate
                });
            }

            return dropdownList;
        }
    }
}