using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerManager.API.Models;
using PokerManager.API.Services.Interfaces;
using Serilog;

namespace PokerManager.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;
        private readonly ILogger<TournamentsController> _logger;

        public TournamentsController(ITournamentService tournamentService, ILogger<TournamentsController> logger)
        {
            _tournamentService = tournamentService;
            _logger = logger;
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournaments()
        {
            try
            {
                var tournaments = await _tournamentService.GetAllTournamentsAsync();
                _logger.LogInformation("Retrieved {Count} tournaments", tournaments.Count());
                return Ok(tournaments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching tournaments");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tournament>> GetTournament(int id)
        {
            try
            {
                var tournament = await _tournamentService.GetTournamentByIdAsync(id);

                if (tournament == null)
                {
                    _logger.LogWarning("Tournament with ID {TournamentId} not found", id);
                    return NotFound($"Tournament with ID {id} not found.");
                }

                _logger.LogInformation("Retrieved tournament {@Tournament}", tournament);
                return Ok(tournament);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching tournament with ID {TournamentId}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // POST: api/Tournaments
        [HttpPost]
        public async Task<ActionResult<Tournament>> PostTournament(Tournament tournament)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTournament = await _tournamentService.CreateTournamentAsync(tournament);
            return CreatedAtAction(nameof(GetTournament), new { id = createdTournament.Id }, createdTournament);
        }

        // PUT: api/Tournaments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, Tournament tournament)
        {
            if (id != tournament.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _tournamentService.UpdateTournamentAsync(tournament);
            return NoContent();
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            try
            {
                var tournament = await _tournamentService.GetTournamentByIdAsync(id);
                if (tournament == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent tournament with ID {TournamentId}", id);
                    return NotFound($"Tournament with ID {id} not found.");
                }

                await _tournamentService.DeleteTournamentAsync(id);
                _logger.LogInformation("Deleted tournament {@Tournament}", tournament);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting tournament with ID {TournamentId}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}