using SharedGolfClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GolfCompanion.Services
{
    public class CourseSearchService
    {
        private readonly HttpClient _httpClient;

        public CourseSearchService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<GolfCourse>> SearchCoursesAsync(string searchQuery)
        {
            try
            {
                var url = $"http://localhost:5189/api/golfcourse/search?search_query={Uri.EscapeDataString(searchQuery)}";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                
                if (response.IsSuccessStatusCode)
                {
                    var courses = await response.Content.ReadFromJsonAsync<IEnumerable<GolfCourse>>();
                    return courses ?? new List<GolfCourse>();
                }
                
                return new List<GolfCourse>();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error searching courses: {ex.Message}");
                return new List<GolfCourse>();
            }
        }
    }
} 