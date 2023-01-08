using Cw9.DTO.Responce;

namespace Cw9.Services
{
    public interface IPrescriptionService
    {
        Task<PrescriptionDTO> GetPrescriptionAsync(int idPrescription);
    }
}
