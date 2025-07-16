using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GolfCompanion.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClubName = table.Column<string>(type: "TEXT", nullable: false),
                    CourseName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Handicap = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Tees",
                columns: table => new
                {
                    TeeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false),
                    Gender = table.Column<string>(type: "TEXT", nullable: false),
                    TeeName = table.Column<string>(type: "TEXT", nullable: false),
                    CourseRating = table.Column<double>(type: "REAL", nullable: false),
                    SlopeRating = table.Column<int>(type: "INTEGER", nullable: false),
                    Par = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tees", x => x.TeeId);
                    table.ForeignKey(
                        name: "FK_Tees_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    ClubId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClubName = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    ClubDistance = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.ClubId);
                    table.ForeignKey(
                        name: "FK_Clubs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Holes",
                columns: table => new
                {
                    HoleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    HoleNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Handicap = table.Column<int>(type: "INTEGER", nullable: true),
                    Par = table.Column<int>(type: "INTEGER", nullable: false),
                    Length = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holes", x => x.HoleId);
                    table.ForeignKey(
                        name: "FK_Holes_Tees_TeeId",
                        column: x => x.TeeId,
                        principalTable: "Tees",
                        principalColumn: "TeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    RoundId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    TeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false),
                    SG_Tee = table.Column<double>(type: "REAL", nullable: false),
                    SG_App = table.Column<double>(type: "REAL", nullable: false),
                    SG_Short = table.Column<double>(type: "REAL", nullable: false),
                    SG_Putt = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.RoundId);
                    table.ForeignKey(
                        name: "FK_Rounds_Tees_TeeId",
                        column: x => x.TeeId,
                        principalTable: "Tees",
                        principalColumn: "TeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rounds_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shots",
                columns: table => new
                {
                    ShotId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoundId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClubId = table.Column<int>(type: "INTEGER", nullable: false),
                    ShotType = table.Column<string>(type: "TEXT", nullable: false),
                    Distance = table.Column<int>(type: "INTEGER", nullable: false),
                    Lie = table.Column<string>(type: "TEXT", nullable: false),
                    Result = table.Column<string>(type: "TEXT", nullable: false),
                    StrokesGained = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shots", x => x.ShotId);
                    table.ForeignKey(
                        name: "FK_Shots_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "ClubId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shots_Holes_HoleId",
                        column: x => x.HoleId,
                        principalTable: "Holes",
                        principalColumn: "HoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shots_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "RoundId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "Handicap", "LastName" },
                values: new object[] { 1, "john_doe@fake.email", "John", 15.0, "Doe" });

            migrationBuilder.InsertData(
                table: "Clubs",
                columns: new[] { "ClubId", "ClubDistance", "ClubName", "UserId" },
                values: new object[,]
                {
                    { 1, 270.0, "Dr", 1 },
                    { 2, 240.0, "3w", 1 },
                    { 3, 220.0, "4h", 1 },
                    { 4, 205.0, "5i", 1 },
                    { 5, 195.0, "6i", 1 },
                    { 6, 180.0, "7i", 1 },
                    { 7, 165.0, "8i", 1 },
                    { 8, 150.0, "9i", 1 },
                    { 9, 135.0, "Pw", 1 },
                    { 10, 125.0, "Gw", 1 },
                    { 11, 115.0, "Aw", 1 },
                    { 12, 105.0, "Sw", 1 },
                    { 13, 95.0, "Lw", 1 },
                    { 14, 0.0, "Pu", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_UserId",
                table: "Clubs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Holes_TeeId",
                table: "Holes",
                column: "TeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_TeeId",
                table: "Rounds",
                column: "TeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_UserId",
                table: "Rounds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Shots_ClubId",
                table: "Shots",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Shots_HoleId",
                table: "Shots",
                column: "HoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Shots_RoundId",
                table: "Shots",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Tees_CourseId",
                table: "Tees",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shots");

            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropTable(
                name: "Holes");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "Tees");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
