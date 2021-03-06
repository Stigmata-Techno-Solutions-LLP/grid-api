using System;
using System.ComponentModel.DataAnnotations;

namespace GridManagement.Model.Dto
{
    public class AddSubContractorModel
    {
        [Required]
        [Display(Name = "SubContractor Name")]        
        public string name {get;set;}   

        [Required]
        [StringLength(10)]
        [Display(Name = "SubContractor Code")]
        public string code {get;set;}
        [StringLength(200)]
        [Display(Name = "SubContractor contact person")]

        public string contact_person {get;set;}
        [StringLength(2000)]
        [Display(Name = "SubContractor Address")]
        public string contact_address {get;set;}
        
        [Display(Name = "SubContractor PhoneNo")]
        public string phone {get;set;}
        
        [Display(Name = "SubContractor EmailId")]
        public string email {get;set;}

        public int user_id{get;set;}

    }

    public class SubContractorDetails
    {             
        public string SubContractorId{get;set;}
        public string name {get;set;}           
        public string code {get;set;}

        public string? contact_person {get;set;}
       
        public string? contact_address {get;set;}
        
        public string? phone {get;set;}
                
        public string? email {get;set;}

        public string? createdBy{get;set;}
        public string? updatedBy{get;set;}

        public DateTime? createdDate{get;set;}
        public DateTime? updatedDate{get;set;}

    }

    public class SubContractorName {
        public int Id{get;set;}
        public string SubContName{get;set;}
        public string SubContCode{get;set;}
    }

}