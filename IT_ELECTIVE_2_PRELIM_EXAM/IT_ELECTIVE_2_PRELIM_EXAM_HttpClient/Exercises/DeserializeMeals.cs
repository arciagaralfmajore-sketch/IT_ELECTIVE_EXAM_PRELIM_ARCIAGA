namespace IT_ELECTIVE_2_PRELIM_EXAM_HttpClient.Exercises;

using System.Net;
using System.Text.Json;

public static class DeserializeMeals
{
    public static async Task Run(System.Net.Http.HttpClient client)
    {
        string url = "https://themealdb.com/api/json/v1/1/search.php?f=a";

        // Send GET request
        var response = await client.GetAsync(url);

        // Assert status code is 200 OK
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new Exception($"Expected 200 OK but got {response.StatusCode}");
        }

        // Read JSON response
        string json = await response.Content.ReadAsStringAsync();

        // Parse JSON
        using JsonDocument document = JsonDocument.Parse(json);

        // Get meals array
        JsonElement meals = document.RootElement.GetProperty("meals");

        // Assert array has more than 0 items
        if (meals.GetArrayLength() == 0)
        {
            throw new Exception("No meals found.");
        }

        // Loop through meals and print names
        foreach (JsonElement meal in meals.EnumerateArray())
        {
            string mealName = meal.GetProperty("strMeal").GetString()!;
            Console.WriteLine(mealName);
        }

        Console.WriteLine("Exercise 10 Passed: Meals successfully deserialized.");
    }
}
