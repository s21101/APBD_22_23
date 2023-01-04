namespace Cw8.DTO.Responce
{
    public class PrescriptionDTO
    {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public PatientDTO Patient { get; set; }
        public DoctorDTO Doctor { get; set; }

        public IEnumerable<MedicamentDTO> Medicaments { get; set; }
    }
}