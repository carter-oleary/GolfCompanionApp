using GolfCompanion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCompanion.Services
{
    public class RoundInputService
    {
        private static int userId = 1; // Placeholder for user ID, replace with actual user management logic
        private static List<Club> clubs = new List<Club>();
        private static Round round;
        private GolfDataService golfDataService;

        public RoundInputService(GolfDataService golfDataService)
        {
            this.golfDataService = golfDataService;
            clubs = golfDataService.GetClubsFromDatabaseAsync(userId).Result;
        }

        public static List<Club> GetClubs()
        {
            return clubs;
        }
        public static int GetUserId()
        {
            // In a real application, this would retrieve the user ID from the current session or authentication context
            return userId;
        }

        public static void SetUserId(int id)
        {
            // This method can be used to set the user ID, for example during login
            userId = id;
        }

        public static void SetRound(Round newRound)
        {
            if (newRound == null) throw new ArgumentNullException(nameof(newRound));
            round = newRound;
        }

        public static Round GetRound()
        {
            if (round == null)
            {
                throw new InvalidOperationException("Round has not been set. Please set the round before retrieving it.");
            }
            return round;
        }
    }
}
