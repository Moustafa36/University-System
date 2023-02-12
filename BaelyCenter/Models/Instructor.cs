using BaelyCenter.Enum;
using BaelyCenter.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaelyCenter.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        [Required]
        [Remote(action:"InstructorExist",controller:"Error" ,AdditionalFields = "Id" ,ErrorMessage ="Name is Already Used")]
        public string Name { get; set; }
        [Required]
        [Range(minimum:22,maximum:40)]
        public int Age { get; set; }
        [Required]
        [RegularExpression(pattern:@"\w+\.(jpg|png)",ErrorMessage ="image must be jpg or png Extanstion")]
        public string image { get; set; }
        [Required]
        [AddressZone]
        public string Address { get; set; }
        public Gender Gender { get; set; }
        [Required]
        [Range(minimum:10000,maximum:50000)]
        public int Salary { get; set; }
        [Display(Name = "Department " )]
        [ForeignKey("Dept")]
      
        public int Dept_Id { get; set; }
        public Department Dept { get; set; }

    }
}
