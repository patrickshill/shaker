using Microsoft.EntityFrameworkCore;
 
namespace shaker.Models
{
    public class ModelsContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public ModelsContext(DbContextOptions<ModelsContext> options) : base(options) { }

            //User stuff
            public DbSet<User> users {get;set;}
            public DbSet<UserCabinet> usercabinets {get;set;}
            public DbSet<UserRecipe> userrecipes {get;set;}
            public DbSet<UserRecipeIngredient> userrecipeingredients {get;set;}
            public DbSet<Comment> comments {get;set;}

            //Ingredients
            public DbSet<Category> categories {get;set;}
            public DbSet<Ingredient> ingredients {get;set;}

            //Recipes
            public DbSet<Recipe> recipes {get;set;}
            public DbSet<RecipeIngredient> recipeingredients {get;set;}
            
    }
}