using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaelyCenter.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double CourseDegree { get; set; }

        public double MinDegree { get; set; }

        [ForeignKey("Dept")]
        [Display(Name = "Department Name")]
        public int Dept_Id { get; set; }
        public Department Dept { get; set; }

        [ForeignKey("Inst")]
        [Display(Name="Instructor Name")]
        public int Inst_Id { get; set; }
        public Instructor Inst { get; set; }

        public ICollection<StudentCourse>  StudentCourse  { get; set; }

        public Course()
        {
            StudentCourse = new List<StudentCourse>();
        }
    }
}
