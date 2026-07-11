namespace IT_ELECTIVE_2_PRELIM_EXAM_HttpClient.Exercises;

// EXERCISE 3: GET Lookup by ID
// TheMealDB API: https://themealdb.com/api/json/v1/1/lookup.php?i={id}
//
// Your task:
// 1. Use the HttpClient to look up meal with ID 52772
// 2. Assert status code is 200 OK
// 3. Parse the JSON and assert the meal name is "Arrabiata"
//
// Note: TheMealDB meal IDs are numeric (52771 = Arrabiata)

public static class GetMealById
{
    public static async Task Run(System.Net.Http.HttpClient client)
    {
        var response = await client.GetAsync("https://themealdb.com/api/json/v1/1/lookup.php?i=52771");

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            throw new Exception("Status code should be 200 OK.");

        var body = await response.Content.ReadAsStringAsync();

        using var document = System.Text.Json.JsonDocument.Parse(body);

        var meals = document.RootElement.GetProperty("meals");

        if (meals.ValueKind == System.Text.Json.JsonValueKind.Null)
            throw new Exception("Meal not found.");

        var mealName = meals[0].GetProperty("strMeal").GetString();

        if (mealName == null || !mealName.Contains("Arrabiata", StringComparison.OrdinalIgnoreCase))
            throw new Exception("Meal name should contain Arrabiata.");
    }
}
