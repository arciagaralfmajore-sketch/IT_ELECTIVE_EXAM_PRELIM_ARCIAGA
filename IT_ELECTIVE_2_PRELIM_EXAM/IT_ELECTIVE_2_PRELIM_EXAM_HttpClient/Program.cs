using IT_ELECTIVE_2_PRELIM_EXAM_HttpClient.Exercises;

namespace IT_ELECTIVE_2_PRELIM_EXAM_HttpClient;

class Program
{
    static int passed = 0;
    static int failed = 0;

    static async Task Main(string[] args)
    {
        Console.WriteLine("============================================");
        Console.WriteLine("  IT ELECTIVE 2 - PRELIM EXAM (HttpClient)");
        Console.WriteLine("============================================");

        using var client = new System.Net.Http.HttpClient();
        client.Timeout = TimeSpan.FromSeconds(15);

        await RunExercise(1, "GET Random Meal", () => GetRandomMeal.Run(client));
        await RunExercise(2, "GET Search by Name", () => SearchMealByName.Run(client));
        await RunExercise(3, "GET Meal by ID", () => GetMealById.Run(client));
        await RunExercise(4, "GET Categories", () => GetCategories.Run(client));
        await RunExercise(5, "GET Filter Ingredient", () => FilterByIngredient.Run(client));
        await RunExercise(6, "POST Create Review", () => CreateReview.Run(client));
        await RunExercise(7, "PUT Update Review", () => UpdateReview.Run(client));
        await RunExercise(8, "DELETE Remove Review", () => DeleteReview.Run(client));
        await RunExercise(9, "GET Handle 404", () => HandleNotFound.Run(client));
        await RunExercise(10, "GET Deserialize List", () => DeserializeMeals.Run(client));

        Console.WriteLine("============================================");
        Console.WriteLine($"  Results: {passed}/10 passed");
        Console.WriteLine("============================================");
    }

    static async Task RunExercise(int number, string name, Func<Task> test)
    {
        Console.Write($"  Exercise {number,2}: {name,-25} - ");
        try
        {
            await test();
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
}
