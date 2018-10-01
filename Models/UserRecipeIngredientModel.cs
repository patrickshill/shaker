using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shaker.Models {
    public class UserRecipeIngredient {
        [Key]
        public int id {get;set;}

        public UserRecipe userrecipe {get;set;}
        [ForeignKey("userrecipe")]
        public int userrecipe_id {get;set;}

        public Ingredient ingredient {get;set;}
        [ForeignKey("ingredient")]
        public int ingredient_id {get;set;}

        [Required(ErrorMessage="A measurement is required")]
        public float measurement {get;set;}

        [Required(ErrorMessage="A unit type is required")]
        public string units {get;set;}

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime created_at {get;set;}
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime updated_at {get;set;}

    }
}