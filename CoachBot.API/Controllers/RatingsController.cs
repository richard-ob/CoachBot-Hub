using CoachBot.Domain.Services;
using CoachBot.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoachBot.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : Controller
    {
        private readonly RatingService _ratingService;

        public RatingController(RatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet("current")]
        public IEnumerable<Player> GetAllCurrent()
        {
            return _ratingService.GetAllPlayerRatings();
        }

        [HttpGet("rateable")]
        public IEnumerable<Player> GetAllRateable()
        {
            return _ratingService.GetRateablePlayers();
        }
    }
}
