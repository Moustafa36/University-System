using System.ComponentModel.DataAnnotations;
using BaelyCenter.Models;
using BaelyCenter.Servcies;

namespace BaelyCenter.Validation
{
    public class StudentDegree: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            StudentCourse StudentCourse = validationContext.ObjectInstance as StudentCourse;
            var service =(ICourseService) validationContext.GetService(typeof(ICourseService));
            var Course = service.GetById(StudentCourse.Course_Id);
            var min = 0;
            var max = Course.CourseDegree;
            double degree = value==null ?0 : (double)value;
            if (degree >= min && degree <= max)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"Degree must be between {min} and {max}");
        }
    }
}
