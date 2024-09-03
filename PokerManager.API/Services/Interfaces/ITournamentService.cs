using PokerManager.API.Models;

namespace PokerManager.API.Services.Interfaces
{
    public interface ITournamentService
    {
        Task<IEnumerable<Tournament>> GetAllTournamentsAsync();
        Task<Tournament?> GetTournamentByIdAsync(int id);
        Task<Tournament> CreateTournamentAsync(Tournament tournament);
        Task UpdateTournamentAsync(Tournament tournament);
        Task DeleteTournamentAsync(int id);
        Task<bool> TournamentExistsAsync(int id);
    }
}
