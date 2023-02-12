using BaelyCenter.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaelyCenter.Validation;
using Microsoft.AspNetCore.Mvc;

namespace BaelyCenter.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Display(Name ="Student Name")]
        [Required]
        [Remote(action: "NameExist",controller:"Error",ErrorMessage ="this name is already used", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required]
        [Range(minimum:18,maximum:30)]
        public int Age { get; set; }
        [Required]
        
        public string image { get; set; }

        [Required]
        [AddressZone]
        public string Address { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [ForeignKey("Department")]
        [Required]
        [Display(Name ="Department ")]
        public int? Dept_Id { get; set; }
        public Department Department { get; set; }

        public ICollection<StudentCourse> StudentCourse { get; set; }

        public Student()
        {
            StudentCourse = new List<StudentCourse>();
        }

    }

    }
