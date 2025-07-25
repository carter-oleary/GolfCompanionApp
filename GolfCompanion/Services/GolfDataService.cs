using GolfCompanion.Data;
using GolfCompanion.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SharedGolfClasses;

namespace GolfCompanion.Services
{
    public class GolfDataService
    {
        private readonly GolfDbContext _context;

        public GolfDataService(GolfDbContext context)
        {
            _context = context;
        }

        public async Task SaveCourseAndTeesAsync(GolfCourse golfCourse)
        {
            try
            {
                // Check if course already exists
                var existingCourse = await _context.Courses
                    .FirstOrDefaultAsync(c => c.CourseId == golfCourse.CourseId);

                using var transaction = await _context.Database.BeginTransactionAsync();
                
                try
                {
                    if (existingCourse == null)
                    {
                        // Create new course
                        var course = new Course
                        {
                            CourseId = golfCourse.CourseId,
                            ClubName = golfCourse.ClubName ?? string.Empty,
                            CourseName = golfCourse.CourseName ?? string.Empty
                        };

                        _context.Courses.Add(course);
                        await _context.SaveChangesAsync();
                        existingCourse = course;
                        Console.WriteLine($"Created new course {golfCourse.CourseId} in database");
                    }

                    // Save tees if they exist
                    if (golfCourse.Tees != null)
                    {
                        // Save male tees
                        if (golfCourse.Tees.Male != null)
                        {
                            foreach (var tee in golfCourse.Tees.Male)
                            {
                                await SaveTeeAsync(existingCourse.CourseId, "Male", tee);
                            }
                        }

                        // Save female tees
                        if (golfCourse.Tees.Female != null)
                        {
                            foreach (var tee in golfCourse.Tees.Female)
                            {
                                await SaveTeeAsync(existingCourse.CourseId, "Female", tee);
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    
                    Console.WriteLine($"Successfully processed course {golfCourse.CourseId} with all tees");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Error saving course and tees: {ex.Message}");
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SaveCourseAndTeesAsync: {ex.Message}");
                throw;
            }
        }

        private async Task SaveTeeAsync(int courseId, string gender, SharedGolfClasses.Tee tee)
        {
            // Check if tee already exists
            var existingTee = await _context.Tees
                .FirstOrDefaultAsync(t => t.CourseId == courseId && 
                                        t.Gender == gender && 
                                        t.TeeName == tee.Tee_Name);

            if (existingTee == null)
            {
                var dbTee = new Models.Tee
                {
                    CourseId = courseId,
                    Gender = gender,
                    TeeName = tee.Tee_Name ?? string.Empty,
                    CourseRating = tee.Course_Rating,
                    SlopeRating = tee.Slope_Rating,
                    Par = tee.Par_Total
                };

                _context.Tees.Add(dbTee);
                await _context.SaveChangesAsync();

                // Save holes if they exist
                if (tee.Holes != null)
                {
                    for(int i = 0; i < tee.Holes.Length; i++)
                    {
                        var hole = tee.Holes[i];
                        await SaveHoleAsync(dbTee.TeeId, i + 1, hole); 
                    }
                }
                
                Console.WriteLine($"Added new tee {tee.Tee_Name} for course {courseId}");
            }
            else
            {
                Console.WriteLine($"Tee {tee.Tee_Name} already exists for course {courseId}, skipping");
            }
        }

        private async Task SaveHoleAsync(int teeId, int holeNumber, SharedGolfClasses.Hole hole)
        {
            // Check if hole already exists
            var existingHole = await _context.Holes
                .FirstOrDefaultAsync(h => h.TeeId == teeId && h.HoleNumber == hole.Handicap);

            if (existingHole == null)
            {
                var dbHole = new Models.Hole
                {
                    TeeId = teeId,
                    HoleNumber = holeNumber,
                    Handicap = hole.Handicap,
                    Par = hole.Par,
                    Length = hole.Yardage
                };

                _context.Holes.Add(dbHole);
            }
        }

        public async Task SaveRoundAsync(Round round)
        {
            if (round == null) throw new ArgumentNullException(nameof(round));
            if (round.Score < 18) return;
            // Check if round already exists
            var existingRound = await _context.Rounds
                .FirstOrDefaultAsync(r => r.RoundId == round.RoundId);
            if (existingRound == null)
                _context.Rounds.Add(round);
            else
                _context.Rounds.Update(round);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Round>> GetRoundsFromDatabaseAsync(int userId)
        {
            return await _context.Rounds
                .Include(r => r.Tee)
                    .ThenInclude(t => t.Course)
                .Include(r => r.User)
                .Include(r => r.Shots)
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Models.Hole>> GetHolesFromDatabaseAsync(int teeId)
        {
            return await _context.Holes
                .Where(h => h.TeeId == teeId)
                .OrderBy(h => h.HoleNumber)
                .ToListAsync();
        }

        public async Task<Models.Tee?> GetTeeFromDatabaseAsync(int courseId, string gender, string teeName)
        {
            return await _context.Tees
                .FirstOrDefaultAsync(t => t.CourseId == courseId && t.Gender == gender && t.TeeName == teeName);
        }

        public async Task<Course?> GetCourseFromDatabaseAsync(int courseId)
        {
            return await _context.Courses
                .Include(c => c.Tees)
                .ThenInclude(t => t.Holes)
                .FirstOrDefaultAsync(c => c.CourseId == courseId);
        }

        public async Task<List<Models.Tee>> GetTeesFromDatabaseAsync(int courseId, string gender)
        {
            return await _context.Tees
                .Include(t => t.Holes)
                .Where(t => t.CourseId == courseId && t.Gender == gender)
                .ToListAsync();
        }

        public async Task AddShotToDatabaseAsync(Shot shot)
        {
            if (shot == null) throw new ArgumentNullException(nameof(shot));
            if(_context.Shots.FirstOrDefault(s => s.ShotId == shot.ShotId) != null)
            {
                _context.Shots.Update(shot);
            }
            else _context.Shots.Add(shot);
            await _context.SaveChangesAsync();
        }
        public async Task AddUserToDatabaseAsync(User user)
        {
            if(user == null) throw new ArgumentNullException(nameof(user));
            if (await _context.Users.AnyAsync(u => u.UserId == user.UserId))
            {
                // User already exists, update instead
                _context.Users.Update(user);
            }
            else
            {
                // New user, add to database
                _context.Users.Add(user);
            }
            await _context.SaveChangesAsync();
        }

        public async Task AddClubToDatabaseAsync(Club club)
        {
            if (club == null) throw new ArgumentNullException(nameof(club));
            if (await _context.Clubs.AnyAsync(c => c.ClubId == club.ClubId))
            {
                // Club already exists, update instead
                _context.Clubs.Update(club);
            }
            else
            {
                // New club, add to database
                _context.Clubs.Add(club);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<Club>> GetClubsFromDatabaseAsync(int userId)
        {
            return await _context.Clubs
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<User?> GetUserFromDatabaseAsync(int userId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }
    }
} 