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
                Console.WriteLine($"Searching courses with URL: {url}");
                Console.WriteLine($"Original search query: '{searchQuery}'");
                Console.WriteLine($"Escaped search query: '{Uri.EscapeDataString(searchQuery)}'");
                
                var response = await _httpClient.GetAsync(url);
                Console.WriteLine($"Search response status: {response.StatusCode}");
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Search response JSON: {jsonContent}");
                    
                    var courses = await response.Content.ReadFromJsonAsync<IEnumerable<GolfCourse>>();
                    var courseList = courses?.ToList() ?? new List<GolfCourse>();
                    Console.WriteLine($"Found {courseList.Count} courses");
                    return courseList;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Search error response: {errorContent}");
                }
                
                return new List<GolfCourse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching courses: {ex.Message}");
                Console.WriteLine($"Error type: {ex.GetType().Name}");
                return new List<GolfCourse>();
            }
        }
    }
} 