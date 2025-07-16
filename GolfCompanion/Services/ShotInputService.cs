using GolfCompanion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCompanion.Services
{
    public class ShotInputService
    {
        private static Hole? _selectedHole;
        private static int CurrentShotDistance;

        public static void SetCurrentShotDistance()
        {
            CurrentShotDistance = _selectedHole?.Shots.Count == 0 ? _selectedHole?.Length ?? 0 : _selectedHole?.Shots.Last().Distance ?? 0;
        }

        public static int GetCurrentShotDistance()
        {
            return CurrentShotDistance;
        }

        public static void SetSelectedHole(Hole hole)
        {
            _selectedHole = hole;
        }

        public static Hole? GetSelectedHole()
        {
            var hole = _selectedHole;
            return hole;
        }
    }
}
