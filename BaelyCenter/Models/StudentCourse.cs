using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaelyCenter.Validation;

namespace BaelyCenter.Models
{
    public class StudentCourse
    {
        [ForeignKey("Student")]
        public int  Student_Id { get; set; }
        public Student student  { get; set; }

        [ForeignKey("Course")]
        public int Course_Id { get; set; }
        public Course Course { get; set; }
        [StudentDegree]
        [Required]
        
        public double Degree { get; set; }




    }
}
