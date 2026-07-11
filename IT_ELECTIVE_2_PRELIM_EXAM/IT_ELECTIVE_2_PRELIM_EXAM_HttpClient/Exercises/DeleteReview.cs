namespace IT_ELECTIVE_2_PRELIM_EXAM_HttpClient.Exercises;

// EXERCISE 8: DELETE Remove Review
// JSONPlaceholder API: https://jsonplaceholder.typicode.com/posts/{id}
//
// Your task:
// 1. Send a DELETE request to remove post with ID 1
// 2. Assert status code is 200 OK
//
// Hint: Use await client.DeleteAsync(url)

public static class DeleteReview
{
    public static async Task Run(System.Net.Http.HttpClient client)
    {
        var response = await client.DeleteAsync("https://jsonplaceholder.typicode.com/posts/1");

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            throw new Exception("Status code should be 200 OK.");
    }
}
