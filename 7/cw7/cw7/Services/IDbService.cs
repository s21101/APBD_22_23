using cw7.DTO;
using cw7.DTO.Requests;
using cw7.Models;

namespace cw7.Services
{
    public interface IDbService
    {
        Task<IEnumerable<TripDTO>> GetDescSortedTripsAsync();
        Task DeleteClientAsync(int idClient);
        Task AssignClientToTrip(RequestClientTripDTO requestClientTripDTO);
    }
}
