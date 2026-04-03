using MealPlannerApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApp.Data;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();
    public DbSet<MealPlan> MealPlans => Set<MealPlan>();
    public DbSet<Meal> Meals => Set<Meal>();
    public DbSet<MealPlanTemplate> MealPlanTemplates => Set<MealPlanTemplate>();
    public DbSet<MealPlanTemplateMeal> MealPlanTemplateMeals => Set<MealPlanTemplateMeal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .ToTable("Users");

        modelBuilder.Entity<User>()
            .Property(u => u.UserName)
            .HasColumnName("Username")
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasMany(u => u.MealPlans)
            .WithOne(mp => mp.User)
            .HasForeignKey(mp => mp.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Recipes)
            .WithOne(r => r.Owner)
            .HasForeignKey(r => r.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasMany(u => u.MealPlanTemplates)
            .WithOne(t => t.Owner)
            .HasForeignKey(t => t.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MealPlan>()
            .HasMany(mp => mp.Meals)
            .WithOne(m => m.MealPlan)
            .HasForeignKey(m => m.MealPlanId);

        modelBuilder.Entity<Meal>()
            .HasOne(m => m.Recipe)
            .WithMany(r => r.Meals)
            .HasForeignKey(m => m.RecipeId);

        modelBuilder.Entity<Meal>()
            .Property(m => m.PortionMultiplier)
            .HasDefaultValue(1.0);

        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(ri => ri.Recipe)
            .WithMany(r => r.RecipeIngredients)
            .HasForeignKey(ri => ri.RecipeId);

        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(ri => ri.Ingredient)
            .WithMany(i => i.RecipeIngredients)
            .HasForeignKey(ri => ri.IngredientId);

        modelBuilder.Entity<RecipeIngredient>()
            .HasIndex(ri => new { ri.RecipeId, ri.IngredientId })
            .IsUnique();

        modelBuilder.Entity<MealPlanTemplate>()
            .HasMany(t => t.Meals)
            .WithOne(m => m.MealPlanTemplate)
            .HasForeignKey(m => m.MealPlanTemplateId);

        modelBuilder.Entity<MealPlanTemplateMeal>()
            .HasOne(m => m.Recipe)
            .WithMany(r => r.MealPlanTemplateMeals)
            .HasForeignKey(m => m.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);

        var seedCreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                CreatedAt = seedCreatedAt,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@mealplanner.local",
                NormalizedEmail = "ADMIN@MEALPLANNER.LOCAL",
                EmailConfirmed = true,
                LockoutEnabled = true,
                SecurityStamp = "d6420f08-9b3e-4c53-bf64-cf9afc6ec3d8",
                ConcurrencyStamp = "c3e1c75f-159f-4e2d-a2eb-8b67800fc86f",
                Role = UserRole.Admin
            });

        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient { Id = 1, CreatedAt = seedCreatedAt, Name = "Chicken Breast", CaloriesPer100g = 165 },
            new Ingredient { Id = 2, CreatedAt = seedCreatedAt, Name = "Rice", CaloriesPer100g = 130 },
            new Ingredient { Id = 3, CreatedAt = seedCreatedAt, Name = "Broccoli", CaloriesPer100g = 35 },
            new Ingredient { Id = 4, CreatedAt = seedCreatedAt, Name = "Eggs", CaloriesPer100g = 155 },
            new Ingredient { Id = 5, CreatedAt = seedCreatedAt, Name = "Spinach", CaloriesPer100g = 23 },
            new Ingredient { Id = 6, CreatedAt = seedCreatedAt, Name = "Cheddar Cheese", CaloriesPer100g = 402 },
            new Ingredient { Id = 7, CreatedAt = seedCreatedAt, Name = "Rolled Oats", CaloriesPer100g = 389 },
            new Ingredient { Id = 8, CreatedAt = seedCreatedAt, Name = "Greek Yogurt", CaloriesPer100g = 59 },
            new Ingredient { Id = 9, CreatedAt = seedCreatedAt, Name = "Banana", CaloriesPer100g = 89 },
            new Ingredient { Id = 10, CreatedAt = seedCreatedAt, Name = "Salmon Fillet", CaloriesPer100g = 208 },
            new Ingredient { Id = 11, CreatedAt = seedCreatedAt, Name = "Sweet Potato", CaloriesPer100g = 86 },
            new Ingredient { Id = 12, CreatedAt = seedCreatedAt, Name = "Chickpeas", CaloriesPer100g = 164 },
            new Ingredient { Id = 13, CreatedAt = seedCreatedAt, Name = "Cherry Tomatoes", CaloriesPer100g = 18 },
            new Ingredient { Id = 14, CreatedAt = seedCreatedAt, Name = "Whole Wheat Pasta", CaloriesPer100g = 149 },
            new Ingredient { Id = 15, CreatedAt = seedCreatedAt, Name = "Parmesan", CaloriesPer100g = 431 },
            new Ingredient { Id = 16, CreatedAt = seedCreatedAt, Name = "Bell Pepper", CaloriesPer100g = 31 },
            new Ingredient { Id = 17, CreatedAt = seedCreatedAt, Name = "Onion", CaloriesPer100g = 40 },
            new Ingredient { Id = 18, CreatedAt = seedCreatedAt, Name = "Garlic", CaloriesPer100g = 149 },
            new Ingredient { Id = 19, CreatedAt = seedCreatedAt, Name = "Olive Oil", CaloriesPer100g = 884 },
            new Ingredient { Id = 20, CreatedAt = seedCreatedAt, Name = "Cucumber", CaloriesPer100g = 16 },
            new Ingredient { Id = 21, CreatedAt = seedCreatedAt, Name = "Avocado", CaloriesPer100g = 160 });

        modelBuilder.Entity<Recipe>().HasData(
            new Recipe
            {
                Id = 1,
                CreatedAt = seedCreatedAt,
                Name = "Grilled Chicken and Rice",
                Description = "A classic high-protein plate with grilled chicken, rice, and broccoli.",
                Instructions = """
1. Cook the rice until fluffy and set it aside.
2. Heat a pan, add the chicken breast, and cook until golden on both sides and fully cooked through.
3. Steam or saute the broccoli until just tender.
4. Slice the chicken and plate it with the rice and broccoli.
5. Serve everything warm.
""",
                CookingTime = 35,
                Calories = 520,
                IsVegetarian = false,
                OwnerId = 1,
                ApprovalStatus = ApprovalStatus.Approved
            },
            new Recipe
            {
                Id = 2,
                CreatedAt = seedCreatedAt,
                Name = "Broccoli Rice Bowl",
                Description = "A fast vegetarian bowl with rice, broccoli, and melted cheddar.",
                Instructions = """
1. Cook the rice and keep it warm.
2. Steam the broccoli until bright green and tender.
3. Put the rice into a bowl and top it with the broccoli.
4. Sprinkle cheddar cheese over the hot bowl so it softens slightly.
5. Serve immediately.
""",
                CookingTime = 20,
                Calories = 390,
                IsVegetarian = true,
                OwnerId = 1,
                ApprovalStatus = ApprovalStatus.Approved
            },
            new Recipe
            {
                Id = 3,
                CreatedAt = seedCreatedAt,
                Name = "Spinach Omelette",
                Description = "A quick savory breakfast with eggs, spinach, cheese, and onion.",
                Instructions = """
1. Heat olive oil in a non-stick pan and soften the onion for a few minutes.
2. Add the spinach and cook until wilted.
3. Beat the eggs, pour them into the pan, and let them begin to set.
4. Sprinkle cheddar over the top and fold the omelette in half.
5. Cook for another minute, then slide onto a plate and serve.
""",
                CookingTime = 15,
                Calories = 410,
                IsVegetarian = true,
                OwnerId = 1,
                ApprovalStatus = ApprovalStatus.Approved
            },
            new Recipe
            {
                Id = 4,
                CreatedAt = seedCreatedAt,
                Name = "Greek Yogurt Banana Oats",
                Description = "A no-cook breakfast bowl with oats, yogurt, and banana.",
                Instructions = """
1. Spoon the Greek yogurt into a bowl.
2. Stir in the rolled oats so they soften slightly.
3. Slice the banana and place it on top.
4. Let the bowl rest for a couple of minutes before serving.
""",
                CookingTime = 5,
                Calories = 360,
                IsVegetarian = true,
                OwnerId = 1,
                ApprovalStatus = ApprovalStatus.Approved
            },
            new Recipe
            {
                Id = 5,
                CreatedAt = seedCreatedAt,
                Name = "Salmon and Sweet Potato Tray Bake",
                Description = "An easy oven meal with salmon, sweet potato, broccoli, and garlic.",
                Instructions = """
1. Cut the sweet potato into cubes and place it on a baking tray with olive oil and garlic.
2. Roast the sweet potato until it starts to soften.
3. Add the salmon and broccoli to the tray.
4. Bake until the salmon flakes easily and the broccoli is tender.
5. Serve straight from the tray while hot.
""",
                CookingTime = 40,
                Calories = 610,
                IsVegetarian = false,
                OwnerId = 1,
                ApprovalStatus = ApprovalStatus.Approved
            },
            new Recipe
            {
                Id = 6,
                CreatedAt = seedCreatedAt,
                Name = "Chickpea Tomato Pasta",
                Description = "A hearty vegetarian pasta with chickpeas, tomatoes, spinach, and parmesan.",
                Instructions = """
1. Cook the pasta until al dente and reserve a little cooking water.
2. In a pan, warm olive oil and garlic, then add the cherry tomatoes.
3. Stir in the chickpeas and cook until heated through.
4. Add spinach and cook until wilted, then fold in the pasta.
5. Finish with parmesan and a splash of pasta water if needed before serving.
""",
                CookingTime = 25,
                Calories = 560,
                IsVegetarian = true,
                OwnerId = 1,
                ApprovalStatus = ApprovalStatus.Approved
            },
            new Recipe
            {
                Id = 7,
                CreatedAt = seedCreatedAt,
                Name = "Chicken Pepper Rice Skillet",
                Description = "A one-pan chicken and rice meal with peppers, onion, and garlic.",
                Instructions = """
1. Cook the rice first and set it aside.
2. Saute onion, bell pepper, and garlic in olive oil until softened.
3. Add the chicken pieces and cook until lightly browned and cooked through.
4. Stir the rice into the pan and mix everything together until hot.
5. Serve the skillet meal straight away.
""",
                CookingTime = 30,
                Calories = 545,
                IsVegetarian = false,
                OwnerId = 1,
                ApprovalStatus = ApprovalStatus.Approved
            },
            new Recipe
            {
                Id = 8,
                CreatedAt = seedCreatedAt,
                Name = "Mediterranean Chickpea Salad",
                Description = "A fresh no-cook salad with chickpeas, cucumber, tomatoes, and avocado.",
                Instructions = """
1. Drain the chickpeas and add them to a large bowl.
2. Dice the cucumber, halve the cherry tomatoes, and cube the avocado.
3. Add everything to the bowl with olive oil.
4. Toss gently so the avocado stays mostly intact.
5. Serve chilled or at room temperature.
""",
                CookingTime = 10,
                Calories = 430,
                IsVegetarian = true,
                OwnerId = 1,
                ApprovalStatus = ApprovalStatus.Approved
            });

        modelBuilder.Entity<RecipeIngredient>().HasData(
            new RecipeIngredient { Id = 1, CreatedAt = seedCreatedAt, RecipeId = 1, IngredientId = 1, QuantityInGrams = 200 },
            new RecipeIngredient { Id = 2, CreatedAt = seedCreatedAt, RecipeId = 1, IngredientId = 2, QuantityInGrams = 150 },
            new RecipeIngredient { Id = 3, CreatedAt = seedCreatedAt, RecipeId = 1, IngredientId = 3, QuantityInGrams = 120 },
            new RecipeIngredient { Id = 4, CreatedAt = seedCreatedAt, RecipeId = 2, IngredientId = 2, QuantityInGrams = 150 },
            new RecipeIngredient { Id = 5, CreatedAt = seedCreatedAt, RecipeId = 2, IngredientId = 3, QuantityInGrams = 180 },
            new RecipeIngredient { Id = 6, CreatedAt = seedCreatedAt, RecipeId = 2, IngredientId = 6, QuantityInGrams = 30 },
            new RecipeIngredient { Id = 7, CreatedAt = seedCreatedAt, RecipeId = 3, IngredientId = 4, QuantityInGrams = 180 },
            new RecipeIngredient { Id = 8, CreatedAt = seedCreatedAt, RecipeId = 3, IngredientId = 5, QuantityInGrams = 60 },
            new RecipeIngredient { Id = 9, CreatedAt = seedCreatedAt, RecipeId = 3, IngredientId = 6, QuantityInGrams = 30 },
            new RecipeIngredient { Id = 10, CreatedAt = seedCreatedAt, RecipeId = 3, IngredientId = 17, QuantityInGrams = 30 },
            new RecipeIngredient { Id = 11, CreatedAt = seedCreatedAt, RecipeId = 3, IngredientId = 19, QuantityInGrams = 10 },
            new RecipeIngredient { Id = 12, CreatedAt = seedCreatedAt, RecipeId = 4, IngredientId = 8, QuantityInGrams = 200 },
            new RecipeIngredient { Id = 13, CreatedAt = seedCreatedAt, RecipeId = 4, IngredientId = 7, QuantityInGrams = 60 },
            new RecipeIngredient { Id = 14, CreatedAt = seedCreatedAt, RecipeId = 4, IngredientId = 9, QuantityInGrams = 120 },
            new RecipeIngredient { Id = 15, CreatedAt = seedCreatedAt, RecipeId = 5, IngredientId = 10, QuantityInGrams = 180 },
            new RecipeIngredient { Id = 16, CreatedAt = seedCreatedAt, RecipeId = 5, IngredientId = 11, QuantityInGrams = 250 },
            new RecipeIngredient { Id = 17, CreatedAt = seedCreatedAt, RecipeId = 5, IngredientId = 3, QuantityInGrams = 120 },
            new RecipeIngredient { Id = 18, CreatedAt = seedCreatedAt, RecipeId = 5, IngredientId = 18, QuantityInGrams = 10 },
            new RecipeIngredient { Id = 19, CreatedAt = seedCreatedAt, RecipeId = 5, IngredientId = 19, QuantityInGrams = 15 },
            new RecipeIngredient { Id = 20, CreatedAt = seedCreatedAt, RecipeId = 6, IngredientId = 14, QuantityInGrams = 180 },
            new RecipeIngredient { Id = 21, CreatedAt = seedCreatedAt, RecipeId = 6, IngredientId = 12, QuantityInGrams = 160 },
            new RecipeIngredient { Id = 22, CreatedAt = seedCreatedAt, RecipeId = 6, IngredientId = 13, QuantityInGrams = 180 },
            new RecipeIngredient { Id = 23, CreatedAt = seedCreatedAt, RecipeId = 6, IngredientId = 5, QuantityInGrams = 60 },
            new RecipeIngredient { Id = 24, CreatedAt = seedCreatedAt, RecipeId = 6, IngredientId = 15, QuantityInGrams = 25 },
            new RecipeIngredient { Id = 25, CreatedAt = seedCreatedAt, RecipeId = 6, IngredientId = 18, QuantityInGrams = 10 },
            new RecipeIngredient { Id = 26, CreatedAt = seedCreatedAt, RecipeId = 6, IngredientId = 19, QuantityInGrams = 10 },
            new RecipeIngredient { Id = 27, CreatedAt = seedCreatedAt, RecipeId = 7, IngredientId = 1, QuantityInGrams = 180 },
            new RecipeIngredient { Id = 28, CreatedAt = seedCreatedAt, RecipeId = 7, IngredientId = 2, QuantityInGrams = 160 },
            new RecipeIngredient { Id = 29, CreatedAt = seedCreatedAt, RecipeId = 7, IngredientId = 16, QuantityInGrams = 120 },
            new RecipeIngredient { Id = 30, CreatedAt = seedCreatedAt, RecipeId = 7, IngredientId = 17, QuantityInGrams = 60 },
            new RecipeIngredient { Id = 31, CreatedAt = seedCreatedAt, RecipeId = 7, IngredientId = 18, QuantityInGrams = 10 },
            new RecipeIngredient { Id = 32, CreatedAt = seedCreatedAt, RecipeId = 7, IngredientId = 19, QuantityInGrams = 10 },
            new RecipeIngredient { Id = 33, CreatedAt = seedCreatedAt, RecipeId = 8, IngredientId = 12, QuantityInGrams = 180 },
            new RecipeIngredient { Id = 34, CreatedAt = seedCreatedAt, RecipeId = 8, IngredientId = 20, QuantityInGrams = 120 },
            new RecipeIngredient { Id = 35, CreatedAt = seedCreatedAt, RecipeId = 8, IngredientId = 13, QuantityInGrams = 120 },
            new RecipeIngredient { Id = 36, CreatedAt = seedCreatedAt, RecipeId = 8, IngredientId = 21, QuantityInGrams = 80 },
            new RecipeIngredient { Id = 37, CreatedAt = seedCreatedAt, RecipeId = 8, IngredientId = 19, QuantityInGrams = 10 });
    }
}
