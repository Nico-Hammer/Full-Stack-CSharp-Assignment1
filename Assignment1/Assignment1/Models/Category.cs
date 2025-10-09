// use the models namespace so that the Category object is available to the database
namespace Assignment1.Models;

/// <summary>
/// Class for the relationship category which has an ID for lookup and a name for printing to the user
/// </summary>
public class Category
{
    public int CategoryID { get; set; }
    public string CategoryName { get; set; }
}