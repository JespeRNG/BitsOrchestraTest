using System;
using System.ComponentModel.DataAnnotations;

namespace BitsOrchestraTest.Models
{
    public class ContactModel
    {
        [Required]
        public string EncryptedId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name must be up to 100 characters long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/1900", "12/31/2025", ErrorMessage = "Date of Birth must be between 1900 and 2025.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Marital status is required.")]
        public bool Married { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\+\d{1,3}\s?\d{9,10}$", ErrorMessage = "Phone number must be in the format +1234567890.")]
        [MinLength(10, ErrorMessage = "Phone number must contain at least 10 digits.")]
        [MaxLength(13, ErrorMessage = "Phone number must not exceed 13 digits.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(0, 1000000, ErrorMessage = "Salary must be between 0 and 1,000,000.")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
    }
}