using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaelyCenter.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        [Remote(action:"DepartmentExist",controller:"Error",AdditionalFields ="Id",ErrorMessage ="Department is already Exist")]
        public string Name { get; set; }

        [ForeignKey("Manager")]
        [Display(Name ="Manager Name")]
        [Required]
        public int Manager_Id  { get; set; }

        public Instructor Manager { get; set; }
    }
}
