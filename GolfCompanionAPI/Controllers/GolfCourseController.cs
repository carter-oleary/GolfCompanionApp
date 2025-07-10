using GolfCompanionAPI.Models;
using GolfCompanionAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedGolfClasses;

namespace GolfCompanionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GolfCourseController : ControllerBase
    {
        private readonly GolfCourseService _courseSvc;
        public GolfCourseController(GolfCourseService crsSvc) => _courseSvc = crsSvc;

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Course>>> Search([FromQuery] string? search_query)
        {
            return Ok(await _courseSvc.SearchGolfCoursesAsync(search_query));
        }

        [HttpGet("course")]
        public async Task<ActionResult<Course>> Get([FromQuery] int? id) =>
            Ok(await _courseSvc.GetGolfCourseAsync(id));

        
    }
}
