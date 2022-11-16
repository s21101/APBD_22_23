using cw3.Models;

namespace cw3.Services
{
    public interface IStudentsServices
    {
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<Student> GetStudentByIndexAsync(string indexNumber);
        Task<Student> UpdateStudentAsync(Student student);
        Task<Student> AddStudent(Student student);
        Task DeleteStudent(string indexNumber);
    }
}
