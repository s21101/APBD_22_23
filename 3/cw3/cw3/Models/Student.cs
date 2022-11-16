using System.ComponentModel.DataAnnotations;

namespace cw3.Models
{
    public class Student
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IndexNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string StudyName { get; set; }
        public string StudyMode { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string MothersName { get; set; }
        public string FathersName { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Student student &&
                   FirstName == student.FirstName &&
                   LastName == student.LastName &&
                   IndexNumber == student.IndexNumber;
        }

        public override string? ToString()
        {
            return $"{FirstName},{LastName},{IndexNumber},{BirthDate},{StudyName},{StudyMode},{Email},{MothersName},{FathersName}";
        }


    }

}
