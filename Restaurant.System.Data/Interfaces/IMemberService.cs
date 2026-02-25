using Restaurant.System.Models.Entities;

namespace Restaurant.System.Data.Interfaces
{
    public interface IMemberService
    {
        Task<List<Member>> GetMemberList();
        Task<Member> GetMemberDetail(Guid MemberId);
        Task<Member> GetMemberByCustomer(Guid CustomerId);
        Task<Member> GetMemberByEmail(string Email);
        Task AddNewMember(Member newMember);
    }
}