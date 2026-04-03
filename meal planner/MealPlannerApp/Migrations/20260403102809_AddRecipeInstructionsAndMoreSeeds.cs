using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MealPlannerApp.Migrations
{
    /// <inheritdoc />
    public partial class AddRecipeInstructionsAndMoreSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Instructions",
                table: "Recipes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "CaloriesPer100g", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { 4, 155, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Eggs" },
                    { 5, 23, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Spinach" },
                    { 6, 402, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cheddar Cheese" },
                    { 7, 389, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Rolled Oats" },
                    { 8, 59, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Greek Yogurt" },
                    { 9, 89, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Banana" },
                    { 10, 208, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Salmon Fillet" },
                    { 11, 86, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sweet Potato" },
                    { 12, 164, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Chickpeas" },
                    { 13, 18, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cherry Tomatoes" },
                    { 14, 149, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Whole Wheat Pasta" },
                    { 15, 431, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Parmesan" },
                    { 16, 31, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Bell Pepper" },
                    { 17, 40, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Onion" },
                    { 18, 149, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Garlic" },
                    { 19, 884, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Olive Oil" },
                    { 20, 16, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cucumber" },
                    { 21, 160, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Avocado" }
                });

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IngredientId", "QuantityInGrams", "RecipeId" },
                values: new object[] { 3, 120, 1 });

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IngredientId", "QuantityInGrams" },
                values: new object[] { 2, 150 });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "Id", "CreatedAt", "IngredientId", "QuantityInGrams", "RecipeId" },
                values: new object[] { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, 180, 2 });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Instructions" },
                values: new object[] { "A classic high-protein plate with grilled chicken, rice, and broccoli.", "1. Cook the rice until fluffy and set it aside.\n2. Heat a pan, add the chicken breast, and cook until golden on both sides and fully cooked through.\n3. Steam or saute the broccoli until just tender.\n4. Slice the chicken and plate it with the rice and broccoli.\n5. Serve everything warm." });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Calories", "Description", "Instructions" },
                values: new object[] { 390, "A fast vegetarian bowl with rice, broccoli, and melted cheddar.", "1. Cook the rice and keep it warm.\n2. Steam the broccoli until bright green and tender.\n3. Put the rice into a bowl and top it with the broccoli.\n4. Sprinkle cheddar cheese over the hot bowl so it softens slightly.\n5. Serve immediately." });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Calories", "CookingTime", "CreatedAt", "Description", "Instructions", "IsVegetarian", "Name" },
                values: new object[,]
                {
                    { 3, 410, 15, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A quick savory breakfast with eggs, spinach, cheese, and onion.", "1. Heat olive oil in a non-stick pan and soften the onion for a few minutes.\n2. Add the spinach and cook until wilted.\n3. Beat the eggs, pour them into the pan, and let them begin to set.\n4. Sprinkle cheddar over the top and fold the omelette in half.\n5. Cook for another minute, then slide onto a plate and serve.", true, "Spinach Omelette" },
                    { 4, 360, 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A no-cook breakfast bowl with oats, yogurt, and banana.", "1. Spoon the Greek yogurt into a bowl.\n2. Stir in the rolled oats so they soften slightly.\n3. Slice the banana and place it on top.\n4. Let the bowl rest for a couple of minutes before serving.", true, "Greek Yogurt Banana Oats" },
                    { 5, 610, 40, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "An easy oven meal with salmon, sweet potato, broccoli, and garlic.", "1. Cut the sweet potato into cubes and place it on a baking tray with olive oil and garlic.\n2. Roast the sweet potato until it starts to soften.\n3. Add the salmon and broccoli to the tray.\n4. Bake until the salmon flakes easily and the broccoli is tender.\n5. Serve straight from the tray while hot.", false, "Salmon and Sweet Potato Tray Bake" },
                    { 6, 560, 25, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A hearty vegetarian pasta with chickpeas, tomatoes, spinach, and parmesan.", "1. Cook the pasta until al dente and reserve a little cooking water.\n2. In a pan, warm olive oil and garlic, then add the cherry tomatoes.\n3. Stir in the chickpeas and cook until heated through.\n4. Add spinach and cook until wilted, then fold in the pasta.\n5. Finish with parmesan and a splash of pasta water if needed before serving.", true, "Chickpea Tomato Pasta" },
                    { 7, 545, 30, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A one-pan chicken and rice meal with peppers, onion, and garlic.", "1. Cook the rice first and set it aside.\n2. Saute onion, bell pepper, and garlic in olive oil until softened.\n3. Add the chicken pieces and cook until lightly browned and cooked through.\n4. Stir the rice into the pan and mix everything together until hot.\n5. Serve the skillet meal straight away.", false, "Chicken Pepper Rice Skillet" },
                    { 8, 430, 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A fresh no-cook salad with chickpeas, cucumber, tomatoes, and avocado.", "1. Drain the chickpeas and add them to a large bowl.\n2. Dice the cucumber, halve the cherry tomatoes, and cube the avocado.\n3. Add everything to the bowl with olive oil.\n4. Toss gently so the avocado stays mostly intact.\n5. Serve chilled or at room temperature.", true, "Mediterranean Chickpea Salad" }
                });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "Id", "CreatedAt", "IngredientId", "QuantityInGrams", "RecipeId" },
                values: new object[,]
                {
                    { 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, 30, 2 },
                    { 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, 180, 3 },
                    { 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, 60, 3 },
                    { 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, 30, 3 },
                    { 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 17, 30, 3 },
                    { 11, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 19, 10, 3 },
                    { 12, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8, 200, 4 },
                    { 13, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7, 60, 4 },
                    { 14, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9, 120, 4 },
                    { 15, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10, 180, 5 },
                    { 16, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 11, 250, 5 },
                    { 17, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, 120, 5 },
                    { 18, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 18, 10, 5 },
                    { 19, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 19, 15, 5 },
                    { 20, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 14, 180, 6 },
                    { 21, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12, 160, 6 },
                    { 22, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 13, 180, 6 },
                    { 23, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, 60, 6 },
                    { 24, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 15, 25, 6 },
                    { 25, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 18, 10, 6 },
                    { 26, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 19, 10, 6 },
                    { 27, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 180, 7 },
                    { 28, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, 160, 7 },
                    { 29, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 16, 120, 7 },
                    { 30, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 17, 60, 7 },
                    { 31, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 18, 10, 7 },
                    { 32, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 19, 10, 7 },
                    { 33, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12, 180, 8 },
                    { 34, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20, 120, 8 },
                    { 35, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 13, 120, 8 },
                    { 36, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 21, 80, 8 },
                    { 37, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 19, 10, 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "Instructions",
                table: "Recipes");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IngredientId", "QuantityInGrams", "RecipeId" },
                values: new object[] { 2, 150, 2 });

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IngredientId", "QuantityInGrams" },
                values: new object[] { 3, 180 });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Simple grilled chicken served with rice.");

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Calories", "Description" },
                values: new object[] { 340, "Steamed broccoli with rice." });
        }
    }
}
