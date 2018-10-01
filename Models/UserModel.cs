using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shaker.Models {
    public class User {
        
        [Key]
        public int id {get;set;}
        
        [Required(ErrorMessage="First name required")]
        [MinLength(2,ErrorMessage="First name must contain at least 2 characters")]
        [RegularExpression("^[a-zA-Z]*$",ErrorMessage="First name can only contain letters")]
        public string first_name {get;set;}

        [Required(ErrorMessage="Last name required")]
        [MinLength(2,ErrorMessage="Last name must contain at least 2 characters")]
        [RegularExpression("^[a-zA-Z]*$",ErrorMessage="Last name can only contain letters")]
        public string last_name {get;set;}

        [Required(ErrorMessage="Username is required")]
        [MinLength(4,ErrorMessage="Username must be atleast 4 characters long")]
        public string username {get;set;}

        [Required(ErrorMessage="Email required")]
        [EmailAddress(ErrorMessage="Email is invalid")]
        public string email {get;set;}

        [Required(ErrorMessage="Password required")]
        [MinLength(8,ErrorMessage="Password must contain at least 8 characters")]
        [RegularExpression("^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",ErrorMessage="Password must contain 1 letter, 1 number, and 1 special character")]
        public string password {get;set;}

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime created_at {get;set;}
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime updated_at {get;set;}

        public List<Comment> comments {get;set;}
        public List<UserRecipe> userrecipes {get;set;}
        public List<UserCabinet> usercabinet {get;set;}
        public User() {
            comments = new List<Comment>();
            userrecipes = new List<UserRecipe>();
            usercabinet = new List<UserCabinet>();
        }

    }
}