using cw7.DTO;
using cw7.DTO.Requests;
using cw7.Models;
using Microsoft.EntityFrameworkCore;

namespace cw7.Services
{
    public class MsSqlService : IDbService
    {
        private readonly Apbd07Context _context;
        public MsSqlService(Apbd07Context dbContext)
        { 
            _context = dbContext;
        }

        public async Task<IEnumerable<TripDTO>> GetDescSortedTripsAsync()
        {
            return await _context.Trips
                         .Include(e => e.IdCountries)
                         .Include(e => e.ClientTrips)
                         .ThenInclude(e => e.IdClientNavigation)
                         .Select(e => new TripDTO {
                                Name = e.Name, 
                                Description = e.Description, 
                                DateFrom = e.DateFrom, 
                                DateTo = e.DateTo, 
                                MaxPeople = e.MaxPeople,
                                Countries = e.IdCountries
                                           .Select(x => new CountryDTO 
                                           { 
                                               Name = x.Name 
                                           }),
                                Clients = e.ClientTrips
                                         .Select(x => new ClientDTO 
                                         { 
                                             FirstName = x.IdClientNavigation.FirstName,
                                             LastName = x.IdClientNavigation.LastName
                                         })
                         })

                         .OrderByDescending(e => e.DateFrom)
                         .ToListAsync();
        }

        public async Task DeleteClientAsync(int idClient)
        {
            int countClientTrip = await _context
                                       .ClientTrips
                                       .Where(e => e.IdClient == idClient)
                                       .CountAsync();

            if (countClientTrip > 0)
            {
                throw new ArgumentException("Client is assinged to some trip");
            }

            try
            {
                Client c = await _context
                                .Clients
                                .Where(e => e.IdClient == idClient)
                                .SingleAsync();

                _context.Clients.Remove(c);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Could not find client with id: " + idClient);
            }

            _context.SaveChanges();
        }

        public async Task AssignClientToTrip(RequestClientTripDTO requestClientTripDTO)
        {
            Client client = await _context.Clients.SingleOrDefaultAsync(e => e.Pesel == requestClientTripDTO.Pesel);

            if (client == null)
            {
                client = new Client
                {
                    FirstName = requestClientTripDTO.FirstName,
                    LastName = requestClientTripDTO.LastName,
                    Email = requestClientTripDTO.Email,
                    Telephone = requestClientTripDTO.Telephone,
                    Pesel = requestClientTripDTO.Pesel
                };
                await _context.Clients.AddAsync(client);
                _context.SaveChanges();
            }

            int countClientTrip =  await _context
                                        .ClientTrips
                                        .Where(e => e.IdTrip == requestClientTripDTO.IdTrip && e.IdClient == client.IdClient)
                                        .CountAsync();


            if (countClientTrip > 0)
            {
                throw new ArgumentException("Client already assigned to trip");
            }

            var isTripExists = await _context.Trips.Where(e => e.IdTrip == requestClientTripDTO.IdTrip).CountAsync();
            
            if (isTripExists == 0)
            {
                throw new ArgumentException($"Trip ({requestClientTripDTO.IdTrip} - {requestClientTripDTO.TripName}) does not exists");
            }

            await _context
                 .ClientTrips
                 .AddAsync(new ClientTrip
                 {
                     IdClient = client.IdClient,
                     IdTrip = requestClientTripDTO.IdTrip,
                     RegisteredAt = DateTime.Today,
                     PaymentDate = requestClientTripDTO.PaymentDate
                 });

            _context.SaveChanges();

        }
    }
}
