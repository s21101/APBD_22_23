using Cw9.DTO.Request;
using Cw9.DTO.Responce;

namespace Cw9.Services
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
