using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealPlannerApp.Migrations
{
    /// <inheritdoc />
    public partial class AddMealPortionMultiplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PortionMultiplier",
                table: "Meals",
                type: "REAL",
                nullable: false,
                defaultValue: 1.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PortionMultiplier",
                table: "Meals");
        }
    }
}
