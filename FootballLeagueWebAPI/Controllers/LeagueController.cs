using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FootballLeagueWebAPI.Services;
using FootballLeagueWebAPI.Repositories;
using FootballLeagueWebAPI.Models;
using FootballLeagueWebAPI.Requests;
using FootballLeagueWebAPI.DTO;

namespace FootballLeagueWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeagueController : Controller
    {
        private readonly ILeagueInputService _inputService;
        private readonly ILeagueOutputService _outputService;

        public LeagueController(ILeagueInputService inputService, ILeagueOutputService outputService)
        {
            _inputService = inputService;
            _outputService = outputService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<List<TeamDTO>>> GetLegueTable()
        {
            return new JsonResult(_outputService.GetLeagueTable());
        }

        [HttpGet("teams/{id}")]
        public ActionResult<TeamDTO> GetTeam(int id)
        {
            return new JsonResult(_outputService.GetTeamById(id));
        }

        [HttpGet("matches")]
        public ActionResult<List<MatchDTO>> GetMatches()
        {
            return new JsonResult(_outputService.GetAllMatches());
        }

        [HttpGet("teams/players/{teamId}")]
        public ActionResult<List<PlayerDTO>> GetAllPlayersOfTheTeam(int teamId)
        {
            return new JsonResult(_outputService.GetAllPlayersOfTheTeam(teamId));
        }

        [HttpPost("teams/add")]
        public void AddTeam([FromBody]TeamRequest team)
        {
            _inputService.CreateTeam(team);
        }

        [HttpPost("players/add")]
        public void AddPlayer([FromBody]PlayerRequest player)
        {
            _inputService.AddPlayer(player);
        }

        [HttpPost("matches/add")]
        public void PlayMatch([FromBody]MatchRequest match)
        {
            _inputService.PlayMatch(match);
        }

        [HttpPost("players/transfer/{playerId}/{newTeamId}")]
        public void TransferPlayer(int playerId, int newTeamId)
        {
            _inputService.TransferPlayer(playerId, newTeamId);
        }

        [HttpPost("players/remove/{playerId}")]
        public void RemovePlayer(int playerId)
        {
            _inputService.RemovePlayerById(playerId);
        }

        [HttpPost("teams/remove/{teamId}")]
        public void RemoveTeam(int teamId)
        {
            _inputService.RemoveTeamById(teamId);
        }
    }
}