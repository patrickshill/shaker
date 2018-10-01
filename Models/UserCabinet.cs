using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shaker.Models {
    public class UserCabinet {
        [Key]
        public int id {get;set;}

        public User user {get;set;}
        [ForeignKey("user")]
        public int user_id {get;set;}

        public Ingredient ingredient {get;set;}
        [ForeignKey("ingredient")]
        public int ingredient_id {get;set;}

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime created_at {get;set;}
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime updated_at {get;set;}

    }
}