﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace MyNPO.Models
{   
    public class FamilyInfo
    {
        [Key]       
        public Guid PrimaryId { get; set; }
        
        public string FirstName { get; set; }      
        
        public string LastName { get; set; }

        [DisplayFormat(ApplyFormatInEditMode=true ,DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string MobileNo { get; set; }

        public string Email { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string MaritalStatus { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? MarriageDate { get; set; }
        public int NoOfDependents { get; set; }
        public bool IsVolunteer { get; set; }
        public List<DependentInfo> DependentDetails { get; set; }
        public DateTime CreateDate { get; set; }
        public string Donation { get; set; }
        public string DonationReason { get; set; }
    }
}