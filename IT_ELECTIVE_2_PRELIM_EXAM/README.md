# IT ELECTIVE 2 — Prelim Exam

**Course:** Web Systems and Technologies  
**Institution:** Lyceum of Alabang  
**Language:** C# (.NET 10)  
**Topics:** OOP Principles & HttpClient / REST API Consumption

---

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- An internet connection (for HttpClient project — TheMealDB & JSONPlaceholder APIs)

## How to Run

### Run both projects

```bash
dotnet run --project IT_ELECTIVE_2_PRELIM_EXAM
dotnet run --project IT_ELECTIVE_2_PRELIM_EXAM_HttpClient
```

Each project runs its own test harness. Passing exercises print `[ PASS ]`, failing ones print `[ FAIL ]`, and unimplemented ones print `[ NOT IMPLEMENTED ]`.

---

## Project 1 — OOP (`IT_ELECTIVE_2_PRELIM_EXAM`)

### Exercises

| # | Topic | File | What to do |
|---|-------|------|------------|
| 1 | Encapsulation | `Models/Meal.cs` | Make fields private; wire up public property getters/setters |
| 2 | Validation | `Models/Ingredient.cs` | Guard `Quantity` against negatives; guard `Name` against empty |
| 3 | Default Constructor | `Models/Category.cs` | Initialize `Name` and `Description` to `""` |
| 4 | Parameterized Constructor | `Models/Category.cs` | Accept and assign `name` and `description` |
| 5 | Constructor Chaining | `Services/RecipeBook.cs` | Chain a single-parameter constructor to the two-parameter one |
| 6 | Method Overloads | `Services/RecipeBook.cs` | Add `Search(string, string)` and `Search(int)` overloads |
| 7 | Inheritance | `Models/MealRecipe.cs` | Extend `RecipeBase`; override `GetRecipeInfo()` |
| 8 | Override vs New | `Models/QuickRecipe.cs` | Change `new` to `override` for proper polymorphism |
| 9 | Interfaces | `Models/MealRecipe.cs` | Implement `IRecipeSearchable` |
| 10 | Access Modifiers | `Services/Kitchen.cs` | Fix public → private/protected per encapsulation |

### Hints

<details>
<summary>Exercise 1</summary>

Use **backing fields** (`private string name;`) and let the properties (`public string Name { get => ...; set => ...; }`) read/write those fields.
</details>

<details>
<summary>Exercise 2</summary>

In the property setter body, check the incoming value with an `if` and `throw new ArgumentOutOfRangeException(...)` or `throw new ArgumentException(...)` when the value is invalid.
</details>

<details>
<summary>Exercise 3</summary>

A parameterless constructor is `public Category() { }` — inside it, assign `Name = ""` and `Description = ""`.
</details>

<details>
<summary>Exercise 4</summary>

The two-parameter constructor receives `string name, string description` — store them into `this.Name` and `this.Description`.
</details>

<details>
<summary>Exercise 5</summary>

A chaining constructor looks like `public RecipeBook(string name) : this(name, 10) { }`. The `: this(...)` syntax calls the other constructor first.
</details>

<details>
<summary>Exercise 6</summary>

Overloads share the same method name but differ in parameters. The `Search(string, string)` overload should filter by both name **and** category. The `Search(int)` overload should filter by prep time.
</details>

<details>
<summary>Exercise 7</summary>

Use `: base(title, prepTime, difficulty)` in the constructor. Override `GetRecipeInfo()` with `override` keyword and append `Category`/`Area` to the base output.
</details>

<details>
<summary>Exercise 8</summary>

Replace the `new` keyword with `override` so that calling `GetRecipeInfo()` through a `RecipeBase` variable invokes `QuickRecipe`'s version at runtime.
</details>

<details>
<summary>Exercise 9</summary>

Add `, IRecipeSearchable` to the class declaration. Implement `SearchCriteria` (return `Title`) and `MatchesSearch` (check if `Title` contains the search term, case-insensitive).
</details>

<details>
<summary>Exercise 10</summary>

Fields that store internal state should be `private`. Methods that form the public API should stay `public`. `PrepareMeal` should be `protected` so only derived classes can call it.
</details>

---

## Project 2 — HttpClient (`IT_ELECTIVE_2_PRELIM_EXAM_HttpClient`)

### Exercises

| # | Method | Endpoint | File | What to do |
|---|--------|----------|------|------------|
| 1 | GET | TheMealDB `/random.php` | `Exercises/GetRandomMeal.cs` | Fetch a random meal; assert 200 + non-empty body |
| 2 | GET | TheMealDB `/search.php?s={name}` | `Exercises/SearchMealByName.cs` | Search "Arrabiata"; assert meals array has items |
| 3 | GET | TheMealDB `/lookup.php?i={id}` | `Exercises/GetMealById.cs` | Look up ID `52771`; assert name |
| 4 | GET | TheMealDB `/categories.php` | `Exercises/GetCategories.cs` | Fetch categories; assert array has items |
| 5 | GET | TheMealDB `/filter.php?i={ingredient}` | `Exercises/FilterByIngredient.cs` | Filter by "chicken_breast"; assert results |
| 6 | POST | JSONPlaceholder `/posts` | `Exercises/CreateReview.cs` | Create a review; assert 201 + `id` field |
| 7 | PUT | JSONPlaceholder `/posts/1` | `Exercises/UpdateReview.cs` | Update a review; assert 200 + title |
| 8 | DELETE | JSONPlaceholder `/posts/1` | `Exercises/DeleteReview.cs` | Delete a review; assert 200 |
| 9 | GET | TheMealDB `/lookup.php?i={id}` | `Exercises/HandleNotFound.cs` | Look up a non-existent ID; assert `meals` is null |
| 10 | GET | TheMealDB `/search.php?f=a` | `Exercises/DeserializeMeals.cs` | Fetch meals starting with "a"; loop and print names |

### Hints

<details>
<summary>Exercise 1</summary>

Use `await client.GetAsync(url)`. Check `response.StatusCode` and read the body with `await response.Content.ReadAsStringAsync()`.
</details>

<details>
<summary>Exercise 2</summary>

Parse JSON with `System.Text.Json.JsonDocument.Parse(body)`. Drill into the `"meals"` property with `doc.RootElement.GetProperty("meals")`.
</details>

<details>
<summary>Exercise 3</summary>

Same as above — after getting the `"meals"` array, access `[0]` and read `"strMeal"` with `.GetString()`.
</details>

<details>
<summary>Exercise 4</summary>

The response shape is `{ "categories": [...] }`. Get the array and check its length.
</details>

<details>
<summary>Exercise 5</summary>

Same pattern as exercises 2-4 — GET request, parse JSON, inspect array length.
</details>

<details>
<summary>Exercise 6</summary>

Create a JSON string manually, wrap in `StringContent` with `"application/json"` media type, and use `await client.PostAsync(url, content)`. Expect `201 Created`.
</details>

<details>
<summary>Exercise 7</summary>

Same as POST but use `await client.PutAsync(url, content)`. Check `200 OK` and verify the returned `"title"` matches what you sent.
</details>

<details>
<summary>Exercise 8</summary>

Use `await client.DeleteAsync(url)`. The simplest of the HTTP methods — just check status.
</details>

<details>
<summary>Exercise 9</summary>

TheMealDB always returns `200 OK` even when a meal isn't found. You must check the body — `"meals"` will be `null` (not an empty array). Compare `.ValueKind` against `JsonValueKind.Null`.
</details>

<details>
<summary>Exercise 10</summary>

After parsing, enumerate the `"meals"` array with `.EnumerateArray()`, read each meal's `"strMeal"`, and `Console.WriteLine` it.
</details>

---

## Reviewer's Guide

Use these checkpoints when reviewing a student's submission:

### OOP Project

| # | What to check |
|---|---------------|
| 1 | Fields are `private`; properties exist and actually read/write the backing fields (not stubs) |
| 2 | Setter throws `ArgumentOutOfRangeException` for negative quantity; throws `ArgumentException` for empty name |
| 3 | Default constructor sets both string properties to `""` |
| 4 | Two-parameter constructor assigns both arguments to properties |
| 5 | Single-parameter constructor uses `: this(name, 10)` |
| 6 | `Search(string, string)` filters by name **and** category; `Search(int)` filters by `PrepTimeMinutes` |
| 7 | `MealRecipe` extends `RecipeBase`; `GetRecipeInfo()` overridden to include Category/Area |
| 8 | `QuickRecipe` uses `override` (not `new`) on `GetRecipeInfo()` |
| 9 | `MealRecipe : ..., IRecipeSearchable`; `SearchCriteria` returns `Title`; `MatchesSearch` uses case-insensitive contains |
| 10 | Fields are `private`; `GetKitchenInfo`/`AddMeal`/`GetMeals`/`RemoveMeal` are `public`; `PrepareMeal` is `protected` |

### HttpClient Project

| # | What to check |
|---|---------------|
| 1 | `GetAsync` called; status compared to `200 OK`; body checked non-null/non-empty |
| 2 | `GetAsync` with query param; `meals` array parsed and asserted length ≥ 1 |
| 3 | `GetAsync` with query param; `strMeal` extracted and compared |
| 4 | `GetAsync`; `categories` array parsed and length checked |
| 5 | `GetAsync` with filter param; `meals` array length checked |
| 6 | `PostAsync` with `StringContent`; status `201 Created` asserted; `id` field exists in response |
| 7 | `PutAsync` with `StringContent`; status `200 OK`; `title` field value compared |
| 8 | `DeleteAsync`; status `200 OK` asserted |
| 9 | `GetAsync`; `200 OK`; `meals` property checked for `JsonValueKind.Null` |
| 10 | `GetAsync`; `meals` array enumerated; each `strMeal` printed to console |

### General

- Code compiles without errors (`dotnet build`)
- No `NotImplementedException` remains in any exercise file
- The student wrote the code themselves (AI may have been used for guidance, not generation — refer to `AGENTS.md`)
