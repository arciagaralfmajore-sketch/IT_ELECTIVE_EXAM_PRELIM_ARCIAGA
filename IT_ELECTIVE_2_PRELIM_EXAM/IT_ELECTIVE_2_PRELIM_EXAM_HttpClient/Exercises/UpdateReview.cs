namespace IT_ELECTIVE_2_PRELIM_EXAM_HttpClient.Exercises;

// EXERCISE 7: PUT Update Review
// JSONPlaceholder API: https://jsonplaceholder.typicode.com/posts/{id}
//
// Your task:
// 1. Create a JSON body: { "id": 1, "title": "Updated Review", "body": "Even better than before!", "userId": 1 }
// 2. Wrap it in StringContent with media type "application/json"
// 3. Send a PUT request to update post with ID 1
// 4. Assert status code is 200 OK
// 5. Parse the response JSON and assert the title is "Updated Review"
//
// Hint: Use await client.PutAsync(url, content)

public static class UpdateReview
{
    public static async Task Run(System.Net.Http.HttpClient client)
    {
        var json = """
    {
        "id": 1,
        "title": "Updated Review",
        "body": "Even better than before!",
        "userId": 1
    }
    """;

        var content = new StringContent(
            json,
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await client.PutAsync(
            "https://jsonplaceholder.typicode.com/posts/1",
            content);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            throw new Exception("Status code should be 200 OK.");

        var body = await response.Content.ReadAsStringAsync();

        using var document = System.Text.Json.JsonDocument.Parse(body);

        var title = document.RootElement.GetProperty("title").GetString();

        if (title != "Updated Review")
            throw new Exception("Title should be Updated Review.");
    }
}
