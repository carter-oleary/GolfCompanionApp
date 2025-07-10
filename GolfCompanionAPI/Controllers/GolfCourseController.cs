using GolfCompanionAPI.Models;
using GolfCompanionAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GolfCompanionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GolfCourseController : ControllerBase
    {
        private readonly GolfCourseService _courseSvc;
        public GolfCourseController(GolfCourseService crsSvc) => _courseSvc = crsSvc;

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> Get(int id) =>
            Ok(await _courseSvc.GetGolfCourseAsync(id));

    }
}
