using System;
using System.ComponentModel.DataAnnotations;

namespace GridManagement.Model.Dto
{
    public class AddUser
    {

        [Required]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "User Name")]
        public string username { get; set; }
        
        [Required]
        [StringLength(100)]
        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]        
        public string email { get; set; }


        [Required]
        [StringLength(15)]
        [Display(Name = "Contact No")]
        [Phone(ErrorMessage = "Invalid mobile No")]
        public string mobileNo {get;set;}

        [Required]
        [Display(Name = "Created By")]
        public int user_id { get; set; }

        [Required]
        [Display(Name = "Role Id")]
        public int role_id { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(20)]
        public string password { get; set; }

    }


    public class UpdateUser
    {

        [Required]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        
        [Required]
        [StringLength(100)]
        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]        
        public string email { get; set; }


        [Required]
        [StringLength(15)]
        [Display(Name = "Contact No")]
        [Phone(ErrorMessage = "Invalid mobile No")]
        public string mobileNo {get;set;}

        [Required]
        [Display(Name = "Updated By")]
        public int user_id { get; set; }

        [Required]
        [Display(Name = "Role Id")]
        public int role_id { get; set; }        

    }

    public class ChangePassword{
        public int userId{get;set;}
        public  string currentPassword{get;set;}
        public string newPassword{get;set;}

    }

    public class ForgotPassword{

        [Required]
        [StringLength(100)]
        [Display(Name = "User EmailId")]       
        public  string mailId{get;set;}
    }

     public class UsernameVerification{

        [Required]
        [StringLength(100)]
        [Display(Name = "Username")]       
        public  string userName{get;set;}

        [Required]
        [StringLength(100)]
        [Display(Name = "User EmailId")]       
        public  string mailId{get;set;}
    }

     public class UserDetails {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string mobileNo {get;set;}
        public int userId { get; set; }
        public string userName {get;set;}
        public string password {get;set;}
        public string roleName { get; set; }    
        public int roleId {get;set;}
        public bool isActive {get;set;}
        public int createdBy {get;set;}
        public int updatedBy {get;set;}
     }
    
     


     public class UserFilter {
        public int userId { get; set; }
        public string userName {get;set;}
        public string roleName { get; set; }    
        public int roleId {get;set;}

     }
    
}
