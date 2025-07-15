using GolfCompanionAPI.Models;
using GolfCompanionAPI.Services;
using SharedGolfClasses;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.InteropServices;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace GolfCompanionAPI.Services
{
    public class GolfCourseService
    {
        private readonly HttpClient _httpClient;
        private readonly GolfCourseApiSettings _settings;

        public GolfCourseService(HttpClient httpClient, IOptions<GolfCourseApiSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;

            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
            _httpClient.DefaultRequestHeaders.Remove("Authorization"); // Just in case
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Key {_settings.ApiKey}");
        }

        public async Task<GolfCourse?> GetGolfCourseAsync(int? id) 
        {
            if (id == null)
            {
                Console.WriteLine("ID is null");
                return null;
            }

            Console.WriteLine($"Getting course with ID: {id}");
            Console.WriteLine($"Full URL: {_settings.BaseUrl}courses/{id}");
            
            try
            {
                var response = await _httpClient.GetAsync($"courses/{id}");
                response.EnsureSuccessStatusCode();

                var jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Course Response JSON: {jsonStr}");
                Console.WriteLine($"JSON Length: {jsonStr.Length}");
                
                var courseWrapper = System.Text.Json.JsonSerializer.Deserialize<CourseWrapper>(jsonStr, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                if (courseWrapper?.Course != null)
                {
                    var course = courseWrapper.Course;
                    Console.WriteLine($"Course Info: {course.ToString()}");
                    var golfCourse = course.ToGolfCourse();
                    
                    return golfCourse;
                }
                else 
                {
                    Console.WriteLine("Course wrapper or course is null");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting course {id}: {ex.Message}");
                Console.WriteLine($"Error Type: {ex.GetType().Name}");
                return null;
            }
        }

        public async Task<IEnumerable<GolfCourse>> SearchGolfCoursesAsync(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Search query is null or empty");
                return new List<GolfCourse>();
            }

            string query = $"search?search_query={Uri.EscapeDataString(name)}";
            Console.WriteLine($"Search URL: {_settings.BaseUrl}{query}");

            try
            {
                var response = await _httpClient.GetAsync(query);
                response.EnsureSuccessStatusCode();
                
                var jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Search Response JSON: {jsonStr}");
                Console.WriteLine($"JSON Length: {jsonStr.Length}");

                var searchResponse = System.Text.Json.JsonSerializer.Deserialize<SearchResponseWrapper>(jsonStr, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (searchResponse?.Courses != null)
                {
                    Console.WriteLine($"Found {searchResponse.Courses.Count} courses in response");
                    return searchResponse.Courses.Select(c => c.ToGolfCourse());
                }
                else
                {
                    Console.WriteLine("No courses found in response or null response");
                    return new List<GolfCourse>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Search Error: {ex.Message}");
                Console.WriteLine($"Error Type: {ex.GetType().Name}");
                return new List<GolfCourse>();
            }
        }
    }

    // Wrapper class to match the JSON structure
    public class CourseWrapper
    {
        public required Course Course { get; set; }
    }

    // Wrapper class for search response
    public class SearchResponseWrapper
    {
        public List<Course>? Courses { get; set; }
    }

}
