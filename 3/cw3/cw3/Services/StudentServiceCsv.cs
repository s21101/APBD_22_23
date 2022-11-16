using cw3.Models;
using System.Text.RegularExpressions;

namespace cw3.Services
{
    public class StudentServiceCsv : IStudentsServices
    {
        private string _filePath = "Data/studenci.csv";

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            FileInfo fi = new FileInfo(_filePath);
            ICollection<Student> _students = new List<Student>();

            using (StreamReader stream = new StreamReader(fi.OpenRead()))
            {
                string line = null;
                string[] tmpStud;
                while ((line = await stream.ReadLineAsync()) != null)
                {
                    tmpStud = line.Split(",");

                    _students.Add(new Student
                    {
                        FirstName = tmpStud[0],
                        LastName = tmpStud[1],
                        IndexNumber = tmpStud[2],
                        BirthDate = DateTime.Parse(tmpStud[3]),
                        StudyName = tmpStud[4],
                        StudyMode = tmpStud[5],
                        Email = tmpStud[6],
                        MothersName = tmpStud[7],
                        FathersName = tmpStud[8]
                    });
                }
            }

            return _students;
        }

        public async Task<Student> GetStudentByIndexAsync(string indexNumber)
        {
            FileInfo fi = new FileInfo(_filePath);

            using (StreamReader stream = new StreamReader(fi.OpenRead()))
            {
                string line = null;
                string[] tmpStud;
                while ((line = await stream.ReadLineAsync()) != null)
                {
                    tmpStud = line.Split(",");

                    if (string.Equals(tmpStud[2], indexNumber, StringComparison.OrdinalIgnoreCase))
                    {
                        return new Student
                        {
                            FirstName = tmpStud[0],
                            LastName = tmpStud[1],
                            IndexNumber = tmpStud[2],
                            BirthDate = DateTime.Parse(tmpStud[3]),
                            StudyName = tmpStud[4],
                            StudyMode = tmpStud[5],
                            Email = tmpStud[6],
                            MothersName = tmpStud[7],
                            FathersName = tmpStud[8]
                        };
                    }


                }
            }

            return null;
        }
        
        public async Task<Student> UpdateStudentAsync(Student student)
        {
            Student st = await GetStudentByIndexAsync(student.IndexNumber);
            if(st == null)
                return st;

            IList<Student> students = (IList<Student>)await GetStudentsAsync();
            students.Remove(st);
            students.Add(student);

            if (await WriteToFileAsync(string.Join("\n", students)))
            {
                return student;

            }
            else 
            {
                return null;
            }

        }

        public async Task<Student> AddStudent(Student student)
        {
            if (IsAnyNullOrEmpty(student))
            {
                throw new ArgumentException("All fields must be entered");
            }

            if (await GetStudentByIndexAsync(student.IndexNumber) != null)
            {
                throw new ArgumentException("Student with given index number already exists");
            }

            Regex rx = new Regex("s(\\d)+$");

            if (!rx.IsMatch(student.IndexNumber))
            {
                throw new ArgumentException("Invalid index number");
            }

            SaveOneStudent(student.ToString());

            return student;
        
        }

        public async Task DeleteStudent(string indexNumber)
        {
            Student st = await GetStudentByIndexAsync(indexNumber);
            if (st == null)
            {
                throw new ArgumentException("Cannot find student with given index number");
            }

            IList<Student> students = (IList<Student>)await GetStudentsAsync();
            students.Remove(st);


            if (!await WriteToFileAsync(string.Join("\n", students)))
            {
                throw new Exception();
            }

        }

        private void SaveOneStudent(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                using (StreamWriter sw = File.AppendText(_filePath))
                {
                    sw.WriteLine();
                    sw.Write(str);
                }
            }

        }

        private async Task<bool> WriteToFileAsync(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                using (StreamWriter stream = new StreamWriter(_filePath))
                {
                    await stream.WriteAsync(content);
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        private bool IsAnyNullOrEmpty(object myObject)
        {
            return myObject.GetType().GetProperties()
            .Where(pi => pi.PropertyType == typeof(string))
            .Select(pi => (string)pi.GetValue(myObject))
            .Any(value => string.IsNullOrEmpty(value));
        }
    }


}
