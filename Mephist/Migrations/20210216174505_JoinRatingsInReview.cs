using Microsoft.EntityFrameworkCore.Migrations;

namespace Mephist.Migrations
{
    public partial class JoinRatingsInReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Reviews",
                type: "INTEGER",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Reviews");

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CharacterScore = table.Column<int>(type: "INTEGER", nullable: false),
                    CharacterVotes = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: true),
                    ExamsScore = table.Column<int>(type: "INTEGER", nullable: false),
                    ExamsVotes = table.Column<int>(type: "INTEGER", nullable: false),
                    TeachingScore = table.Column<int>(type: "INTEGER", nullable: false),
                    TeachingVotes = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_EmployeeId",
                table: "Ratings",
                column: "EmployeeId",
                unique: true);
        }
    }
}
