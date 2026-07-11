namespace IT_ELECTIVE_2_PRELIM_EXAM.Interfaces;

// EXERCISE 9: Interfaces
// IRecipeSearchable defines a contract for any class that can be searched.
// Your task:
// - Ensure MealRecipe (in Models/) implements this interface
// - The implementing class should use the recipe's Title as the search criterion
// - MatchesSearch should return true if the searchTerm is found in the Title (case-insensitive)

public interface IRecipeSearchable
{
    string SearchCriteria { get; }
    bool MatchesSearch(string searchTerm);
}
