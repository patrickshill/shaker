using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shaker.Models {
    public class Ingredient {
        [Key]
        public int id {get;set;}

        [Required(ErrorMessage="A name for the ingredient is required")]
        public string name {get;set;}

        public string url {get;set;}

        public string description {get;set;}

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime created_at {get;set;}
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime updated_at {get;set;}

        public Category category {get;set;}
        [ForeignKey("category")]
        public int category_id {get;set;}

        public List<RecipeIngredient> recipeingredients {get;set;}
        public List<UserRecipeIngredient> userrecipeingredients {get;set;}
        public List<UserCabinet> usercabinets {get;set;}
        public Ingredient() {
            recipeingredients = new List<RecipeIngredient>();
            userrecipeingredients = new List<UserRecipeIngredient>();
            usercabinets = new List<UserCabinet>();
        }

    }
}