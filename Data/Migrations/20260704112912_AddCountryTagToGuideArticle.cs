using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CISConnect.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCountryTagToGuideArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryTag",
                table: "GuideArticles",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryTag",
                table: "GuideArticles");
        }
    }
}
