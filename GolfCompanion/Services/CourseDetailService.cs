using SharedGolfClasses;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GolfCompanion.Services
{
    public class CourseDetailService
    {
        private readonly HttpClient _httpClient;

        public CourseDetailService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<GolfCourse?> GetCourseDetailsAsync(int courseId)
        {
            try
            {
                var url = $"http://localhost:5189/api/golfcourse/course?id={courseId}";
                Console.WriteLine($"Getting course details from: {url}");
                
                var response = await _httpClient.GetAsync(url);
                Console.WriteLine($"Response status: {response.StatusCode}");
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response JSON: {jsonContent}");
                    
                    var course = await response.Content.ReadFromJsonAsync<GolfCourse>();
                    Console.WriteLine($"Deserialized course: {course?.ClubName} - {course?.CourseName}");
                    return course;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response: {errorContent}");
                }
                
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting course details: {ex.Message}");
                Console.WriteLine($"Error type: {ex.GetType().Name}");
                return null;
            }
        }
    }
} 