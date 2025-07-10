namespace SharedGolfClasses
{
    public class Tee
    {
        public string? Tee_Name { get; set; }
        public double Course_Rating { get; set; }
        public int Slope_Rating { get; set; }
        public double Bogey_Rating { get; set; }
        public int Total_Yards { get; set; }
        public int Total_Meters { get; set; }
        public int Number_Of_Holes { get; set; }
        public int Par_Total { get; set; }
        public double Front_Course_Rating { get; set; }
        public int Front_Slope_Rating { get; set; }
        public double Front_Bogey_Rating { get; set; }
        public double Back_Course_Rating { get; set; }
        public int Back_Slope_Rating { get; set; }
        public double Back_Bogey_Rating { get; set; }
        public Hole[]? Holes { get; set; }

        public string DisplayName => $"{Course_Rating}/{Slope_Rating}, {Total_Yards} yds";
    }
}
