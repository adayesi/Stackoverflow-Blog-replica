using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DecaBlog.Data.Repositories.Interfaces;
using DecaBlog.Models;
using Microsoft.EntityFrameworkCore;
using NinjaNye.SearchExtensions;

namespace DecaBlog.Data.Repositories.Implementations
{
    public class InviteeRepository : IInviteeRepository
    {
        private readonly DecaBlogDbContext _context;
        public InviteeRepository(DecaBlogDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddInvitee(Invitee invitee)
        {
            _context.Invitees.Add(invitee);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<Invitee> GetInviteeByEmail(string Email)
        {
            return await _context.Invitees.FirstOrDefaultAsync(x => x.Email.ToLower() == Email.ToLower());
        }
        public IOrderedQueryable<Invitee> GetInvitees()
        {
            var users = _context.Invitees.OrderBy(x => x.FirstName);
            return users;
        }
        public async Task<Invitee> GetInviteeById(string inviteId)
        {
            return await _context.Invitees.FirstOrDefaultAsync(x => x.InviteeId == inviteId);
        }
        public IQueryable<Invitee> SearchInviteeByName(string[] Name)
        {
            return Name.Length < 1 ? _context.Invitees
                .OrderBy(x => x.FirstName).ThenBy(i => i.LastName) : 
                _context.Invitees.Search(x => x.FirstName.ToLower(), x => x.LastName.ToLower())
                .Containing(Name).ToRanked()
                .OrderByDescending(x => x.Hits)
                .Select(x => x.Item);
        }

        public async Task DeleteInvitee(Invitee invitee)
        {
            _context.Invitees.Remove(invitee);
            await _context.SaveChangesAsync();
        }
    }


}
