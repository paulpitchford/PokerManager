using Microsoft.EntityFrameworkCore;
using PokerManager.API.Data;
using PokerManager.API.Models;
using PokerManager.API.Services.Interfaces;

namespace PokerManager.API.Services.Implementations
{
    public class TournamentService : ITournamentService
    {
        private readonly PokerManagerContext _context;

        public TournamentService(PokerManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tournament>> GetAllTournamentsAsync()
        {
            return await _context.Tournaments.ToListAsync();
        }

        public async Task<Tournament?> GetTournamentByIdAsync(int id)
        {
            return await _context.Tournaments.FindAsync(id);
        }

        public async Task<Tournament> CreateTournamentAsync(Tournament tournament)
        {
            _context.Tournaments.Add(tournament);
            await _context.SaveChangesAsync();
            return tournament;
        }

        public async Task UpdateTournamentAsync(Tournament tournament)
        {
            _context.Entry(tournament).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTournamentAsync(int id)
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament != null)
            {
                _context.Tournaments.Remove(tournament);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> TournamentExistsAsync(int id)
        {
            return await _context.Tournaments.AnyAsync(e => e.Id == id);
        }
    }
}
