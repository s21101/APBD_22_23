using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace cw2.Models
{
    public class Student
    {
        [JsonPropertyName("activeStudies")]
        public static ICollection<Study> studies = new List<Study>();

        [Required]
        [JsonPropertyName("indexNumber")]
        public string IndexNumber { get; set; }
        [Required]
        [JsonPropertyName("fname")]
        public string FirstName { get; set; }
        [Required]
        [JsonPropertyName("lname")]
        public string LastName { get; set; }
        [Required]
        [JsonPropertyName("birthdate")]
        public DateTime BirthDate { get; set; }
        [Required]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [Required]
        [JsonPropertyName("mothersName")]
        public string MothersName { get; set; }
        [Required]
        [JsonPropertyName("fathersName")]
        public string FathersName { get; set; }
        [Required]
        private Study _Study;

        [JsonPropertyName("Studies")]
        public Study Study
        {
            get { return _Study; }
            set { 
                _Study = value;
                _Study.Students.Add(this);
            }
        }

        public override string? ToString()
        {
            return $"{IndexNumber},  {FirstName}, {LastName}, {BirthDate}, {Email}, {Study}, {MothersName}, {FathersName}, {_Study}";
        }
    }
}
