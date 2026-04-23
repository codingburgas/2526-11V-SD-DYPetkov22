# Meal Planner App

[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/U2LjJyKI)

Meal Planner App is an ASP.NET Core MVC web application for building weekly meal plans, managing recipes and ingredients, and sharing reusable meal plan templates.

## Features

- Create an account and keep personal meal plans private.
- Browse recipes and filter them by ingredient, vegetarian meals, or high-protein options.
- Create recipes as drafts and submit them for admin approval.
- Browse ingredients with calories, protein, carbs, and fat values.
- Build weekly meal plans with breakfast, lunch, and dinner.
- Generate personalized weekly plans from macro goals, body weight, meals per day, and food restrictions.
- Start from built-in plans like balanced, high-protein, or vegetarian weeks.
- Swap meals, view weekly nutrition totals, and track recent progress.
- Save a completed week as a reusable meal plan template.
- Share recipes and templates through an admin review workflow.
- Admins can manage ingredients and review submitted recipes or templates.

## Tech Stack

- ASP.NET Core MVC
- .NET 8
- Razor views
- ASP.NET Core Identity
- Entity Framework Core
- SQLite
- Bootstrap

## Project Structure

```text
project-DYPetkov22/
+-- .gitignore
+-- README.md
`-- meal planner/
    `-- MealPlannerApp/
        +-- Controllers/
        +-- Data/
        +-- Dtos/
        +-- Infrastructure/
        +-- Migrations/
        +-- Models/
        +-- Services/
        +-- Views/
        +-- wwwroot/
        +-- MealPlannerApp.csproj
        `-- MealPlannerApp.sln
```

## Getting Started

Install the .NET 8 SDK, then run the app from the project folder:

```bash
cd "meal planner/MealPlannerApp"
dotnet restore
dotnet run
```

Open one of the local URLs shown in the terminal. The included launch profiles use:

- `http://localhost:5120`
- `https://localhost:7072`

## Main Workflow

1. Register or log in.
2. Browse ingredients and recipes.
3. Create or generate a weekly meal plan.
4. Adjust meals and review nutrition totals.
5. Save a good week as a template.
6. Submit recipes or templates for admin review when they are ready to share.
