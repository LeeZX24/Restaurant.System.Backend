using Restaurant.System.Data.Interfaces;
using Restaurant.System.Models.Entities;

namespace Restaurant.System.Data.Repositories
{
    public class MemberService : IMemberService
    {
        private readonly IRepository<Member> _memberRepository;

        public MemberService(IRepository<Member> MemberRepository)
        {
            _memberRepository = MemberRepository;
        }

        public async Task<List<Member>> GetMemberList() => await _memberRepository.GetAllAsync();

        public async Task<Member> GetMemberDetail(Guid MemberId)
        {
            var member = (await _memberRepository.GetByFieldAsync(e => e.MemberId == MemberId)).FirstOrDefault();

            return member;
        }

        public async Task<Member> GetMemberByCustomer(Guid CustomerId)
        {
            var member = (await _memberRepository.GetByFieldAsync(e => e.CustomerId == CustomerId)).FirstOrDefault();

            return member;
        }
        public async Task<Member> GetMemberByEmail(string Email)
        {
            var member = (await _memberRepository.GetByFieldAsync(e => e.Email == Email)).FirstOrDefault();

            return member;
        }

        public async Task AddNewMember(Member newMember)
        {
            await _memberRepository.AddAsync(newMember);
            await _memberRepository.SaveChangesAsync();
        }

        public async Task UpdateMember(Member Member)
        {
            await _memberRepository.UpdateAsync(Member);
            await _memberRepository.SaveChangesAsync();
        }

        public async Task MemberChangePassword(Guid MemberId, string NewPassword)
        {
            await _memberRepository.UpdateByFieldAsync(member => 
                member.MemberId == MemberId,
                member => member.Password, 
                member => NewPassword);
            await _memberRepository.SaveChangesAsync();
        }
    }
}

