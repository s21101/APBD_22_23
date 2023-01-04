using Cw8.DTO.Request;
using Cw8.DTO.Responce;
using Cw8.Models;
using Microsoft.EntityFrameworkCore;

namespace Cw8.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly PharmacyContext _context;
        public DoctorService(PharmacyContext pharmacyContext)
        { 
            _context = pharmacyContext;
        }

        public async Task<IEnumerable<DoctorDTO>> GetDoctorsAsync()
        {

            return await _context
                        .Doctors
                        .Select(e => new DoctorDTO
                        {
                            IdDoctor = e.IdDoctor,
                            FirstName = e.FirstName,
                            LastName = e.LastName,
                            Email = e.Email
                        })
                        .ToListAsync();
        }
        public async Task<DoctorDTO> GetDoctorAsync(int idDoctor)
        {
            Doctor res = await GetDoctor(idDoctor);
            if (res != null)
            {
                return new DoctorDTO
                {
                    IdDoctor = res.IdDoctor,
                    FirstName = res.FirstName,
                    LastName = res.LastName,
                    Email = res.Email
                };
            }

            return null;

        }

        public async Task AddDoctorAsync(DoctorDTOREQ newDoctor)
        {
            await _context
                 .Doctors
                 .AddAsync(new Doctor
                 { 
                    FirstName = newDoctor.FirstName,
                    LastName = newDoctor.LastName,
                    Email = newDoctor.Email,
                 });
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ModifyDoctorAsync(DoctorDTOREQ newDoctor, int idDoctor)
        {
            Doctor d = await GetDoctor(idDoctor);

            if (d == null)
            {
                return false;
            }

            d.FirstName = newDoctor.FirstName;
            d.LastName = newDoctor.LastName;
            d.Email = newDoctor.Email;
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDoctorAsync(int idDoctor)
        {
            Doctor d = await GetDoctor(idDoctor);
            if (d == null)
            {
                return false;
            }
            _context.Remove(d);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<Doctor> GetDoctor(int idDoctor)
        { 
            return await _context.Doctors.Where(e => e.IdDoctor == idDoctor).SingleOrDefaultAsync();
        }

    }
}
