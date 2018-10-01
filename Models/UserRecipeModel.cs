using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shaker.Models {
    public class UserRecipe {
        [Key]
        public int id {get;set;}

        public string name {get;set;}

        public string description {get;set;}

        public string directions {get;set;}

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime created_at {get;set;}
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime updated_at {get;set;}

        public User user {get;set;}
        [ForeignKey("user")]
        public int user_id {get;set;}

        List<UserRecipeIngredient> ingredients {get;set;}
        List<Comment> comments {get;set;}
        public UserRecipe() {
            ingredients = new List<UserRecipeIngredient>();
            comments = new List<Comment>();
        }

    }
}