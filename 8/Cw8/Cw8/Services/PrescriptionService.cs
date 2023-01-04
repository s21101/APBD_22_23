using Cw8.DTO.Responce;
using Cw8.Models;
using Microsoft.EntityFrameworkCore;

namespace Cw8.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly PharmacyContext _context;
        public PrescriptionService(PharmacyContext pharmacyContext)
        {
            _context = pharmacyContext;
        }
        public async Task<PrescriptionDTO> GetPrescriptionAsync(int idPrescription)
        {
            Prescription p = _context
                            .Prescriptions
                            .SingleOrDefault(e => e.IdPrescription == idPrescription);
            if (p == null)
            {
                return null;
            }

            return await _context
                        .Prescriptions
                        .Where(e => e.IdPrescription == idPrescription)
                        .Include(e => e.PrescriptionMedicaments)
                        .ThenInclude(e => e.IdMedicamentNavigation)
                        .Select(e => new PrescriptionDTO
                        {
                            IdPrescription = e.IdPrescription,
                            Date = e.Date,
                            DueDate = e.DueDate,
                            Patient = new PatientDTO
                            {
                                IdPatient = e.IdPatientNavigation.IdPatient,
                                FirstName = e.IdPatientNavigation.FirstName,
                                LastName = e.IdPatientNavigation.LastName,
                                Birthdate = e.IdPatientNavigation.Birthdate
                            },
                            Doctor = new DoctorDTO
                            {
                                IdDoctor = e.IdDoctorNavigation.IdDoctor,
                                FirstName = e.IdDoctorNavigation.FirstName,
                                LastName = e.IdDoctorNavigation.LastName,
                                Email = e.IdDoctorNavigation.Email
                            },
                            Medicaments = e.PrescriptionMedicaments
                                         .Select(e => new MedicamentDTO
                                         { 
                                            IdMedicament = e.IdMedicamentNavigation.IdMedicament,
                                            Name = e.IdMedicamentNavigation.Name,
                                            Description = e.IdMedicamentNavigation.Description,
                                            Type = e.IdMedicamentNavigation.Type,
                                            Details = e.Details,
                                            Dose = e.Dose
                                         })

                        })
                        .SingleAsync();
        }
    }
}
