using IT_ELECTIVE_2_PRELIM_EXAM.Models;
using IT_ELECTIVE_2_PRELIM_EXAM.Services;

namespace IT_ELECTIVE_2_PRELIM_EXAM;

class Program
{
    static int passed = 0;
    static int failed = 0;

    static void Main(string[] args)
    {
        Console.WriteLine("============================================");
        Console.WriteLine("  IT ELECTIVE 2 - PRELIM EXAM (OOP)");
        Console.WriteLine("============================================");

        RunExercise(1, "Private Fields", TestExercise1);
        RunExercise(2, "Validation", TestExercise2);
        RunExercise(3, "Default Constructor", TestExercise3);
        RunExercise(4, "Parameterized Constructor", TestExercise4);
        RunExercise(5, "Constructor Chaining", TestExercise5);
        RunExercise(6, "Method Overloads", TestExercise6);
        RunExercise(7, "Inheritance", TestExercise7);
        RunExercise(8, "Override vs New", TestExercise8);
        RunExercise(9, "Interfaces", TestExercise9);
        RunExercise(10, "Access Modifiers", TestExercise10);

        Console.WriteLine("============================================");
        Console.WriteLine($"  Results: {passed}/10 completed");
        Console.WriteLine("============================================");
    }

    static void RunExercise(int number, string name, Action test)
    {
        Console.Write($"  Exercise {number,2}: {name,-25} - ");
        try
        {
            test();
            Console.WriteLine("[ PASS ]");
            passed++;
        }
        catch (NotImplementedException)
        {
            Console.WriteLine("[ NOT IMPLEMENTED ]");
            failed++;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ FAIL ] {ex.Message}");
            failed++;
        }
    }

    static void Assert(bool condition, string message)
    {
        if (!condition)
            throw new Exception($"Assertion failed: {message}");
    }

    // ==========================================
    // EXERCISE 1: Encapsulation - Private Fields
    // Make Meal fields private and add properties
    // ==========================================
    static void TestExercise1()
    {
        var meal = new Meal("Arrabiata", "Pasta", "Italian");

        // These should work via properties
        Assert(meal.Name == "Arrabiata", "Name should be 'Arrabiata'");
        Assert(meal.Category == "Pasta", "Category should be 'Pasta'");
        Assert(meal.Area == "Italian", "Area should be 'Italian'");

        // Test setter
        meal.Name = "Carbonara";
        Assert(meal.Name == "Carbonara", "Name should be updated to 'Carbonara'");
    }

    // ==========================================
    // EXERCISE 2: Encapsulation - Validation
    // Add validation to Ingredient setters
    // ==========================================
    static void TestExercise2()
    {
        var ingredient = new Ingredient("Tomato", "pcs", 3);
        Assert(ingredient.Name == "Tomato", "Name should be 'Tomato'");
        Assert(ingredient.Quantity == 3, "Quantity should be 3");

        // Test valid update
        ingredient.Quantity = 5;
        Assert(ingredient.Quantity == 5, "Quantity should be 5");

        // Test validation - negative quantity should throw
        bool caughtNegative = false;
        try
        {
            ingredient.Quantity = -1;
        }
        catch (ArgumentOutOfRangeException)
        {
            caughtNegative = true;
        }
        Assert(caughtNegative, "Setting negative Quantity should throw ArgumentOutOfRangeException");

        // Test validation - empty name should throw
        bool caughtEmpty = false;
        try
        {
            ingredient.Name = "";
        }
        catch (ArgumentException)
        {
            caughtEmpty = true;
        }
        Assert(caughtEmpty, "Setting empty Name should throw ArgumentException");
    }

    // ==========================================
    // EXERCISE 3: Default Constructor
    // Add parameterless constructor to Category
    // ==========================================
    static void TestExercise3()
    {
        var category = new Category();
        Assert(category.Name == "", "Default Name should be empty string");
        Assert(category.Description == "", "Default Description should be empty string");
    }

    // ==========================================
    // EXERCISE 4: Parameterized Constructor
    // Add (string, string) constructor to Category
    // ==========================================
    static void TestExercise4()
    {
        var category = new Category("Dessert", "Sweet dishes");
        Assert(category.Name == "Dessert", "Name should be 'Dessert'");
        Assert(category.Description == "Sweet dishes", "Description should be 'Sweet dishes'");
    }

    // ==========================================
    // EXERCISE 5: Constructor Chaining
    // Add constructor that chains with : this()
    // ==========================================
    static void TestExercise5()
    {
        var book = new RecipeBook("My Favorites");
        Assert(book.Name == "My Favorites", "Name should be 'My Favorites'");
        Assert(book.Capacity == 10, "Default capacity should be 10");
    }

    // ==========================================
    // EXERCISE 6: Method Overloads
    // Add overloaded Search methods
    // ==========================================
    static void TestExercise6()
    {
        var book = new RecipeBook("Test Book", 20);
        book.AddMeal(new Meal("Pasta", "Main", "Italian"));
        book.AddMeal(new Meal("Pad Thai", "Main", "Thai"));
        book.AddMeal(new Meal("Tiramisu", "Dessert", "Italian"));

        // Original search by name
        var results = book.Search("Pasta");
        Assert(results.Count == 1, "Search by name should find 1 result");
        Assert(results[0].Name == "Pasta", "Found meal should be 'Pasta'");

        // Overload: search by name AND category
        var resultsFiltered = book.Search("Pasta", "Main");
        Assert(resultsFiltered.Count == 1, "Search by name+category should find 1");

        var resultsFilteredWrong = book.Search("Pasta", "Dessert");
        Assert(resultsFilteredWrong.Count == 0, "Search with wrong category should find 0");
    }

    // ==========================================
    // EXERCISE 7: Inheritance
    // Complete MealRecipe inheriting from RecipeBase
    // ==========================================
    static void TestExercise7()
    {
        var recipe = new MealRecipe("Spaghetti", 20, "Easy");
        recipe.Category = "Pasta";
        recipe.Area = "Italian";

        Assert(recipe.Title == "Spaghetti", "Title should be 'Spaghetti'");
        Assert(recipe.Category == "Pasta", "Category should be 'Pasta'");
        Assert(recipe.Area == "Italian", "Area should be 'Italian'");

        string info = recipe.GetRecipeInfo();
        Assert(info.Contains("Spaghetti"), "Info should contain title");
        Assert(info.Contains("Pasta"), "Info should contain category");
        Assert(info.Contains("Italian"), "Info should contain area");
    }

    // ==========================================
    // EXERCISE 8: Override vs New
    // Fix QuickRecipe to use 'override' instead of 'new'
    // ==========================================
    static void TestExercise8()
    {
        RecipeBase recipe = new QuickRecipe("Stir Fry", 15, "Easy", 20);

        // This should call QuickRecipe's GetRecipeInfo via polymorphism
        string info = recipe.GetRecipeInfo();
        Assert(info.Contains("Quick"), "Polymorphic call should include 'Quick'");
        Assert(info.Contains("20"), "Should include max minutes");

        // Verify it's NOT just the base version
        Assert(info != recipe.Title, "Should not just return base info");
    }

    // ==========================================
    // EXERCISE 9: Interfaces
    // Implement IRecipeSearchable on MealRecipe
    // ==========================================
    static void TestExercise9()
    {
        var recipe = new MealRecipe("Chicken Curry", 30, "Medium");

        // MealRecipe should implement IRecipeSearchable
        Assert(recipe is Interfaces.IRecipeSearchable,
            "MealRecipe should implement IRecipeSearchable");

        var searchable = (Interfaces.IRecipeSearchable)recipe;
        Assert(searchable.SearchCriteria == "Chicken Curry",
            "SearchCriteria should return the recipe title");

        Assert(searchable.MatchesSearch("Chicken") == true,
            "Should match 'Chicken' in 'Chicken Curry'");

        Assert(searchable.MatchesSearch("Beef") == false,
            "Should not match 'Beef' in 'Chicken Curry'");
    }

    // ==========================================
    // EXERCISE 10: Access Modifiers
    // Fix access modifiers in Kitchen class
    // ==========================================
    static void TestExercise10()
    {
        var kitchen = new Kitchen("Main Kitchen", "Chef Gordon");

        // Public members should be accessible
        Assert(kitchen.GetKitchenInfo().Contains("Main Kitchen"),
            "GetKitchenInfo should be accessible");

        var meal = new Meal("Risotto", "Main", "Italian");
        kitchen.AddMeal(meal);
        Assert(kitchen.GetMeals().Count == 1, "GetMeals should return 1 meal");

        // Private members should NOT be accessible
        // Uncomment the lines below to test - they should cause compile errors
        // If they DON'T cause compile errors, the access modifiers are wrong!
        //
        // kitchen.kitchenName = "Hacked";        // Should be PRIVATE
        // kitchen.headChef = "Unknown";           // Should be PRIVATE
        // kitchen.meals.Clear();                  // Should be PRIVATE
        // kitchen.mealCount = 999;                // Should be PRIVATE

        Assert(true, "Access modifier test passed (verify private members are inaccessible)");
    }
}
