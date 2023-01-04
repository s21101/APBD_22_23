using Cw8.DTO.Request;
using Cw8.DTO.Responce;

namespace Cw8.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDTO>> GetDoctorsAsync();
        Task<DoctorDTO> GetDoctorAsync(int idDoctor);
        Task AddDoctorAsync(DoctorDTOREQ newDoctor);
        Task<bool> ModifyDoctorAsync(DoctorDTOREQ newDoctor, int idDoctor);
        Task<bool> DeleteDoctorAsync(int idDoctor);
    }
}
