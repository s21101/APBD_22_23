using Cw8.DTO.Responce;

namespace Cw8.Services
{
    public interface IPrescriptionService
    {
        Task<PrescriptionDTO> GetPrescriptionAsync(int idPrescription);
    }
}
