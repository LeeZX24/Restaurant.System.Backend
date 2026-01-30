using Restaurant.System.Models.Entities;

namespace Restaurant.System.Data.Interfaces
{
    public interface IMemberService
    {
        Task<List<Member>> GetMemberList();
        Task<Member> GetMemberDetail(string MemberId);
        Task<Member> GetMemberByCustomer(string CustomerId);
    }
}