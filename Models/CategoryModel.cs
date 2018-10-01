using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shaker.Models {
    public class Category {
        [Key]
        public int id {get;set;}

        public string category {get;set;}

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime created_at {get;set;}
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime updated_at {get;set;}

        public List<Ingredient> ingredients {get;set;}
        public Category() {
            ingredients = new List<Ingredient>();
        }

    }
}