using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shaker.Models {
    public class RecipeIngredient {
        [Key]
        public int id {get;set;}

        public Recipe recipe {get;set;}
        [ForeignKey("recipe")]
        public int recipe_id {get;set;}

        public Ingredient ingredient {get;set;}
        [ForeignKey("ingredient")]
        public int ingredient_id {get;set;}

        [Required(ErrorMessage="A measurement is required")]
        public float measurement {get;set;}

        [Required(ErrorMessage="A unit type is required")]
        public string units {get;set;}

    }
}