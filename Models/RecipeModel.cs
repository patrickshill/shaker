using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shaker.Models {
    public class Recipe {
        [Key]
        public int id {get;set;}

        [Required(ErrorMessage="A name is required")]
        public string name {get;set;}

        [Required(ErrorMessage="A description is required")]
        public string description {get;set;}

        [Required(ErrorMessage="Directions are required")]
        public string directions {get;set;}

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime created_at {get;set;}
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime updated_at {get;set;}

        public List<RecipeIngredient> ingredients {get;set;}
        public Recipe() {
            ingredients = new List<RecipeIngredient>();
        }

    }
}