using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeAchievments.Migrations
{
    public partial class Achievments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AchievmentsCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoriesIdName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Award = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxAmount = table.Column<int>(type: "int", nullable: false),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    AchievedIconName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievmentsCollection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryCollection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IconsCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IconsCollection", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AchievmentsCollection");

            migrationBuilder.DropTable(
                name: "CategoryCollection");

            migrationBuilder.DropTable(
                name: "IconsCollection");
        }
    }
}
