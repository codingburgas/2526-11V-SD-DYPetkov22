using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealPlannerApp.Migrations
{
    /// <inheritdoc />
    public partial class AddModeratedSharing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovalStatus",
                table: "Recipes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Recipes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReviewNotes",
                table: "Recipes",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewedAt",
                table: "Recipes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedAt",
                table: "Recipes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MealPlanTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    WeekStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    ReviewNotes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    SubmittedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlanTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealPlanTemplates_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MealPlanTemplateMeals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MealPlanTemplateId = table.Column<int>(type: "INTEGER", nullable: false),
                    DayOffset = table.Column<int>(type: "INTEGER", nullable: false),
                    MealType = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    PortionMultiplier = table.Column<double>(type: "REAL", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlanTemplateMeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealPlanTemplateMeals_MealPlanTemplates_MealPlanTemplateId",
                        column: x => x.MealPlanTemplateId,
                        principalTable: "MealPlanTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealPlanTemplateMeals_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ApprovalStatus", "Instructions", "OwnerId", "ReviewNotes", "ReviewedAt", "SubmittedAt" },
                values: new object[] { 2, "1. Cook the rice until fluffy and set it aside.\n2. Heat a pan, add the chicken breast, and cook until golden on both sides and fully cooked through.\r\n3. Steam or saute the broccoli until just tender.\r\n4. Slice the chicken and plate it with the rice and broccoli.\r\n5. Serve everything warm.", 1, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ApprovalStatus", "OwnerId", "ReviewNotes", "ReviewedAt", "SubmittedAt" },
                values: new object[] { 2, 1, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ApprovalStatus", "OwnerId", "ReviewNotes", "ReviewedAt", "SubmittedAt" },
                values: new object[] { 2, 1, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ApprovalStatus", "OwnerId", "ReviewNotes", "ReviewedAt", "SubmittedAt" },
                values: new object[] { 2, 1, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ApprovalStatus", "OwnerId", "ReviewNotes", "ReviewedAt", "SubmittedAt" },
                values: new object[] { 2, 1, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ApprovalStatus", "OwnerId", "ReviewNotes", "ReviewedAt", "SubmittedAt" },
                values: new object[] { 2, 1, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ApprovalStatus", "OwnerId", "ReviewNotes", "ReviewedAt", "SubmittedAt" },
                values: new object[] { 2, 1, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ApprovalStatus", "OwnerId", "ReviewNotes", "ReviewedAt", "SubmittedAt" },
                values: new object[] { 2, 1, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_OwnerId",
                table: "Recipes",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlanTemplateMeals_MealPlanTemplateId",
                table: "MealPlanTemplateMeals",
                column: "MealPlanTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlanTemplateMeals_RecipeId",
                table: "MealPlanTemplateMeals",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlanTemplates_OwnerId",
                table: "MealPlanTemplates",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Users_OwnerId",
                table: "Recipes",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Users_OwnerId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "MealPlanTemplateMeals");

            migrationBuilder.DropTable(
                name: "MealPlanTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_OwnerId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ReviewNotes",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ReviewedAt",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "SubmittedAt",
                table: "Recipes");

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Instructions",
                value: "1. Cook the rice until fluffy and set it aside.\r\n2. Heat a pan, add the chicken breast, and cook until golden on both sides and fully cooked through.\r\n3. Steam or saute the broccoli until just tender.\r\n4. Slice the chicken and plate it with the rice and broccoli.\r\n5. Serve everything warm.");
        }
    }
}
