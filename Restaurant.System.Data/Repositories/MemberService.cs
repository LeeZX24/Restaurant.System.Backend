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

        public async Task<Member> GetMemberDetail(string MemberId)
        {
            var member = (await _memberRepository.GetByFieldAsync(e => e.MemberId == MemberId)).FirstOrDefault();
            
            return member;
        }

        public async Task<Member> GetMemberByCustomer(string CustomerId)
        {
            var member = (await _memberRepository.GetByFieldAsync(e => e.CustomerId == CustomerId)).FirstOrDefault();
            
            return member;
        }
    }
}

