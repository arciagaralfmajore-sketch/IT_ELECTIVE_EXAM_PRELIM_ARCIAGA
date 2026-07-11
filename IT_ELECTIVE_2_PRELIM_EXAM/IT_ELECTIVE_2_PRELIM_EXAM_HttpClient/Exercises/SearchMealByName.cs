namespace IT_ELECTIVE_2_PRELIM_EXAM_HttpClient.Exercises;

// EXERCISE 2: GET Search by Name
// TheMealDB API: https://themealdb.com/api/json/v1/1/search.php?s={name}
//
// Your task:
// 1. Use the HttpClient to search for meals with name "Arrabiata"
// 2. Assert status code is 200 OK
// 3. Parse the JSON and assert the "meals" array has at least 1 result
//
// Hint: Use System.Text.Json.JsonDocument.Parse() to parse JSON
// Hint: The response format is { "meals": [...] } — meals can be null if no results

public static class SearchMealByName
{
    public static async Task Run(System.Net.Http.HttpClient client)
    {
        var response = await client.GetAsync("https://themealdb.com/api/json/v1/1/search.php?s=Arrabiata");

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            throw new Exception("Status code should be 200 OK.");

        var body = await response.Content.ReadAsStringAsync();

        using var document = System.Text.Json.JsonDocument.Parse(body);

        var meals = document.RootElement.GetProperty("meals");

        if (meals.ValueKind == System.Text.Json.JsonValueKind.Null)
            throw new Exception("Meals should not be null.");

        if (meals.GetArrayLength() < 1)
            throw new Exception("Meals array should contain at least one item.");
    }
}
