﻿using GolfCompanion.Models;
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
