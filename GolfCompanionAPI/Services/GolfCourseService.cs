using GolfCompanionAPI.Models;
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

        public async Task<GolfCourse?> GetGolfCourseAsync(int id) 
        {
            Console.WriteLine(_settings.BaseUrl + $"courses/{id}");
            var response = await _httpClient.GetAsync($"courses/{id}");
            response.EnsureSuccessStatusCode();

            var jsonStr = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonStr);
            try
            {
                var courseWrapper = System.Text.Json.JsonSerializer.Deserialize<CourseWrapper>(jsonStr, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (courseWrapper != null)
                {
                    var course = courseWrapper.Course;
                    Console.WriteLine($"Course Info: {course.ToString()}");
                    return course.ToGolfCourse();
                }
                else return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

    }

    // Wrapper class to match the JSON structure
    public class CourseWrapper
    {
        public required Course Course { get; set; }
    }

}
