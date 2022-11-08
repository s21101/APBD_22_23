using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace cw2.Models
{
    internal class Study
    {
        public static ICollection<Study> Studies = new HashSet<Study>();
        public ICollection<Student> Students = new HashSet<Student>();
        [JsonPropertyName("name")]
        public string StudiesName { get; }
        [JsonPropertyName("mode")]
        public string Mode { get;  }

        private Study(string StudiesName, string Mode)
        {
            this.StudiesName = StudiesName;
            this.Mode = Mode;
            Studies.Add(this);
        }

        public static Study CreateStudy (string study, string studyType) {

            if (Studies.Any(i => i.StudiesName == study && i.Mode == studyType))
            {
                return Studies.Where(i => i.StudiesName == study && i.Mode == studyType).First();
            }
            else
            {
                return new Study(study, studyType);
            }
        }

        public override string? ToString()
        {
            return $" {StudiesName}, {Mode} ";
        }
    }
}
