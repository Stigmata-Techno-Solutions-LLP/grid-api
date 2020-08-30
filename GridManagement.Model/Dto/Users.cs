using System;
using System.ComponentModel.DataAnnotations;

namespace GridManagement.Model.Dto
{
    public class AddUser
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "User Name")]
        public string Username { get; set; }
        
        [Required]
        [StringLength(100)]
        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Status")]
        public bool is_active { get; set; }

        [Required]
        [Display(Name = "User Id")]
        public int user_id { get; set; }

        [Required]
        [Display(Name = "Role Id")]
        public int role_id { get; set; }


        [Display(Name = "Contractor Id")]
        public int contractorId { get; set; }


        public string password { get; set; }

    }
}
