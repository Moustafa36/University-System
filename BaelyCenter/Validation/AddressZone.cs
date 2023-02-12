using System.ComponentModel.DataAnnotations;

namespace BaelyCenter.Validation
{
    public class AddressZone : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            value = value.ToString().ToLower();
            if (value.Equals("cairo") || value.Equals("alexandria") || value.Equals("mansoura"))
            {
                return ValidationResult.Success;
            }
            else
                return new ValidationResult("Address must be Cairo or Alexandria or Mansoura");
        }
    }
}
