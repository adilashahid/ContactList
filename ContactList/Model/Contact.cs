using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Contactlist.Model
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        private string _Phonenumber;
        public string PhoneNumber {
            get => _Phonenumber;
            set {
                if (!Regex.IsMatch(value, @"^\d{10}$"))
                {
                    throw new FormatException("Phone number must be 10 digits");
                }
                _Phonenumber = value;
            }
        }
        public string? BirthDate { get; set; }
        public string Category { get; set; }
        public bool IsDeleted { get; set; }
    }
}
